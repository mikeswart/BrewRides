using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CapitalBreweryBikeClub.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public RouteProvider RouteProvider { get; }
        public ScheduleProvider ScheduleProvider { get; }

        [BindProperty] public string RouteToAdd { get; set; }

        [BindProperty] public string DateToAdd { get; set; }

        public IEnumerable<SelectListItem> SelectableRoutes { get; private set; }
        public IEnumerable<SelectListItem> SelectableDates { get; private set; }

        public IndexModel(RouteProvider routeProvider, ScheduleProvider scheduleProvider)
        {
            RouteProvider = routeProvider;
            ScheduleProvider = scheduleProvider;
        }

        public void OnGet()
        {
            SelectableRoutes = RouteProvider.Routes.Select(info => new SelectListItem(info.Name, info.Name));
            SelectableDates = DaysInWeek(DateTime.Today, DayOfWeek.Tuesday, DayOfWeek.Thursday).Take(20);
        }

        public void OnPostRefreshRoutes()
        {
            RouteProvider.Refresh();
        }

        public async Task<IActionResult> OnPostScheduleRouteAsync()
        {
            if (!DateTime.TryParse(DateToAdd, out var dateToAdd))
            {
                return Page();
            }

            var routeToAdd = RouteProvider.Routes.FirstOrDefault(info => info.Name.Equals(RouteToAdd));
            if (routeToAdd == null)
            {
                return Page();
            }

            ScheduleProvider.AddOrReplace(new DailyRouteSchedule(dateToAdd.Date, routeToAdd));

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
                    var existingRoutes = string.Join(", ", ScheduleProvider.Get(currentDay, TimeSpan.FromDays(1)).Select(schedule => schedule.Route.Name));
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