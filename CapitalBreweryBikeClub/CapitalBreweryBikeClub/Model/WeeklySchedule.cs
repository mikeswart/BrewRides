using System;
using System.Collections.Generic;

namespace CapitalBreweryBikeClub.Model
{
    public class WeeklySchedule
    {
        public DateTime BeginningOfWeek { get; }

        public IEnumerable<DailyRouteSchedule> Routes { get; }

        public WeeklySchedule(DateTime beginningOfWeek, IEnumerable<DailyRouteSchedule> routes)
        {
            BeginningOfWeek = beginningOfWeek;
            Routes = routes;
        }
    }
}