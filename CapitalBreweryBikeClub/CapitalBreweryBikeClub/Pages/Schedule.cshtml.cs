using System;
using System.Collections.Generic;
using System.Linq;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages
{
    public class ScheduleModel : PageModel
    {
        public RouteProvider RouteProvider { get; }
        public DateTime BeginningOfWeek { get; }

        public IEnumerable<DailyRouteSchedule> Routes { get; }


        public ScheduleModel(RouteProvider routeProvider, ScheduleProvider scheduleProvider)
        {
            RouteProvider = routeProvider;
            BeginningOfWeek = DateTime.Today;

            BeginningOfWeek = DateTimeHelper.GetBeginningOfWeek(DateTime.Now);

            //Routes = new[]
            //{
            //    new DailyRouteSchedule(BeginningOfWeek.AddDays(2), routeProvider.Routes.First()),
            //    new DailyRouteSchedule(BeginningOfWeek.AddDays(2), routeProvider.Routes.Last())
            //};

            Routes = scheduleProvider.Get(DateTime.Today.Date, TimeSpan.FromDays(7 - (int) DateTime.Today.DayOfWeek));
        }

        public void OnGet()
        {
        }
    }
}