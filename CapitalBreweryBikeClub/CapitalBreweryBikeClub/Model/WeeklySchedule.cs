using System;
using System.Collections.Generic;

namespace CapitalBreweryBikeClub.Model
{
    public class WeeklySchedule
    {
        public DateTime BeginningOfWeek { get; }

        public IEnumerable<ScheduleData> Routes { get; }

        public WeeklySchedule(DateTime beginningOfWeek, IEnumerable<ScheduleData> routes)
        {
            BeginningOfWeek = beginningOfWeek;
            Routes = routes;
        }
    }
}