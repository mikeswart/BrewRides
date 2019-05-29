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
    public class TestModel : PageModel
    {
        public RouteProvider RouteProvider { get; }
        public ScheduleProvider ScheduleProvider { get; }

        public TestModel(RouteProvider routeProvider, ScheduleProvider scheduleProvider)
        {
            RouteProvider = routeProvider;
            ScheduleProvider = scheduleProvider;
        }

        public void OnGet()
        {
            Schedule = new DailyRouteSchedule(DateTime.Today, RouteProvider.Routes.First());
        }

        public DailyRouteSchedule Schedule { get; set; }
    }
}