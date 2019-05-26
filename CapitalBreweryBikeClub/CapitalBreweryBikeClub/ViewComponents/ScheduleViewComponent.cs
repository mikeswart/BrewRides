using System;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapitalBreweryBikeClub.ViewComponents
{
    public class ScheduleViewComponent : ViewComponent
    {
        private readonly ScheduleProvider _scheduleProvider;

        public ScheduleViewComponent(ScheduleProvider scheduleProvider)
        {
            _scheduleProvider = scheduleProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime dateTime)
        {
            var beginningOfWeek = DateTimeHelper.GetBeginningOfWeek(DateTime.Now);
            return View(
                new WeeklySchedule(
                    beginningOfWeek,
                    _scheduleProvider.Get(beginningOfWeek, TimeSpan.FromDays(7))));
        }
    }
}
