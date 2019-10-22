using System;

namespace CapitalBreweryBikeClub.Model
{
    public sealed class DailyRouteSchedule
    {
        public DateTime DateTime { get; }

        public RouteData Route { get; }

        public RideWithGpsLinks Links { get; }

        public DailyRouteSchedule(ScheduleData scheduleData)
        {
            DateTime = scheduleData.Date;
            Route = scheduleData.RouteData;
            Links = new RideWithGpsLinks(scheduleData.RouteData.RideWithGpsId);
        }
    }
}