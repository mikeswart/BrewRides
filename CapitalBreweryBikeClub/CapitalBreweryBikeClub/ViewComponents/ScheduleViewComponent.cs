using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.ViewComponents
{
    public class ScheduleViewComponent : ViewComponent
    {
        private readonly RouteDatabaseContext databaseContext;

        public ScheduleViewComponent(RouteDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(DateTime dateTime)
        {
            var beginningOfWeek = DateTimeHelper.GetBeginningOfWeek(dateTime);
            var routes = await GetRoutes(beginningOfWeek, TimeSpan.FromDays(7));

            return View(new WeeklySchedule(beginningOfWeek, routes));
        }

        private async Task<IEnumerable<ScheduleData>> GetRoutes(DateTime startTime, TimeSpan timeSpan)
        {
            var endTime = startTime.Add(timeSpan);

            return await databaseContext.Schedules
             .Where(schedule => schedule.Date >= startTime && schedule.Date < endTime)
             .OrderBy(schedule => schedule.Date)
             .ToListAsync();
        }
    }
}
