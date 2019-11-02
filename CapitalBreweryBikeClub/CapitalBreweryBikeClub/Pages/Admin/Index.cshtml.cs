using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string RouteToAdd
        {
            get;
            set;
        }

        [BindProperty]
        public string DateToAdd
        {
            get;
            set;
        }

        [BindProperty]
        public Note CurrentNote
        {
            get;
            set;
        }

        [BindProperty]
        public string NoteText
        {
            get;
            set;
        }

        [BindProperty]
        public IEnumerable<SelectListItem> SelectableRoutes
        {
            get;
            private set;
        }

        [BindProperty]
        public IEnumerable<SelectListItem> SelectableDates
        {
            get;
            private set;
        }

        private readonly RouteDatabaseContext dbContext;

        public IndexModel(RouteDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet()
        {
            var routes = dbContext.Routes.ToList();
            SelectableRoutes = routes.Select(route => new SelectListItem(route.Name, route.Name));

            SelectableDates = DaysInWeek(DateTime.Today, DayOfWeek.Tuesday, DayOfWeek.Thursday).Take(20);
            CurrentNote =  dbContext.SiteState.FirstOrDefault()?.Note;
            NoteText = CurrentNote?.Text ?? string.Empty;
        }

        public IActionResult OnPostClearNote()
        {
            var state = dbContext.SiteState.Include(state => state.Note).First().Note = null;
            dbContext.SaveChanges();

            OnGet();
            return Page();
        }

        public IActionResult OnPostUpdateNote()
        {
            var note = new Note() { Text = NoteText, Created = DateTime.Now, CreatedBy = dbContext.Members.FirstOrDefault() };
            dbContext.SiteState.First().Note = note;
            dbContext.SaveChanges();

            OnGet();
            return Page();
        }

        public void OnPostRefreshRoutes()
        {
            // TODO: Not needed?
            // routeProvider.Refresh();
        }

        public async Task<IActionResult> OnPostAddCustomAsync(string customRouteName, string customRouteRideWithGPSId)
        {
            if (!DateTime.TryParse(DateToAdd, out var dateToAdd))
            {
                return Page();
            }

            // ScheduleProvider.AddOrReplace(new DailyRouteSchedule(dateToAdd, routeInfo));
            var routeData = new RouteData() { Name = customRouteName, RideWithGpsId = customRouteRideWithGPSId };
            await AddOrReplaceSchedule(dateToAdd, routeData);

            return Page();
        }

        private async Task AddOrReplaceSchedule(DateTime dateToAdd, RouteData routeData)
        {
            // TODO: This could be a large DB operation as everything needs to be queried.
            var existingRoute = await dbContext.Schedules.FirstOrDefaultAsync(schedule => schedule.Date.Date == dateToAdd.Date);
            if (existingRoute != null)
            {
                existingRoute.RouteData = routeData;
                dbContext.Schedules.Update(existingRoute);
            }
            else
            {
                await dbContext.Schedules.AddAsync(new ScheduleData() { Date = dateToAdd, RouteData = routeData });
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<IActionResult> OnPostScheduleRouteAsync()
        {
            if (!DateTime.TryParse(DateToAdd, out var dateToAdd))
            {
                return Page();
            }

            var routeToAdd = await dbContext.Routes.FirstOrDefaultAsync(info => info.Name.Equals(RouteToAdd));
            if (routeToAdd == null)
            {
                return Page();
            }

            // ScheduleProvider.AddOrReplace(new DailyRouteSchedule(dateToAdd.Date, routeToAdd));
            // TODO: This is rough
            var existingDate = await dbContext.Schedules.FirstOrDefaultAsync(schedule => schedule.Date.Date.Equals(dateToAdd.Date));
            if(existingDate != null)
            {
                existingDate.RouteData = routeToAdd;
            }
            else
            {
                dbContext.Schedules.Add(new ScheduleData
                {
                    Date = dateToAdd.Date,
                    RouteData = routeToAdd
                });
            }

            await dbContext.SaveChangesAsync();

            return RedirectToPage();
        }

        private IEnumerable<SelectListItem> DaysInWeek(DateTime startTime, params DayOfWeek[] daysOfWeek)
        {
            var currentDay = startTime.Date;
            SelectListGroup group = null;
            while (true)
            {
                if (currentDay.DayOfWeek == DayOfWeek.Sunday || group == null)
                {
                    var beginningOfWeek = DateTimeHelper.GetBeginningOfWeek(currentDay);
                    group = new SelectListGroup {Name = beginningOfWeek.ToString("MMM-dd")};
                }

                if (daysOfWeek.Contains(currentDay.DayOfWeek))
                {
                    var routeForDay = dbContext.Schedules.FirstOrDefault(schedule => schedule.Date.Date == currentDay.Date)?.RouteData.Name ?? string.Empty;
                    var existingRoutes = string.Join(", ", routeForDay);
                    var scheduledRoutes = string.IsNullOrEmpty(existingRoutes) ? string.Empty : $" - ({existingRoutes})";
                    yield return new SelectListItem($"{currentDay:dddd M-d}{scheduledRoutes}", currentDay.Date.ToString())
                    {
                        Group = group
                    };
                }

                currentDay = currentDay.AddDays(1);
            }
        }
    }
}
