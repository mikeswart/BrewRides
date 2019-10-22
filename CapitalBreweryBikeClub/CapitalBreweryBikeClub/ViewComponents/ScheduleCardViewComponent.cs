using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapitalBreweryBikeClub.ViewComponents
{
    public class ScheduleCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ScheduleData scheduleData)
        {
            return View(new DailyRouteSchedule(scheduleData));
        }
    }
}
