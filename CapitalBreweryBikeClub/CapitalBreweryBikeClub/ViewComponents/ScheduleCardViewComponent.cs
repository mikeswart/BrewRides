using System;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapitalBreweryBikeClub.ViewComponents
{
    public class ScheduleCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(DailyRouteSchedule routeSchedule)
        {
            return View(routeSchedule);
        }
    }
}
