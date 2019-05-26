using System;

namespace CapitalBreweryBikeClub.Model
{
    public sealed class DailyRouteSchedule
    {
        public DateTime DateTime { get; }

        public RouteInfo Route { get; }

        public RideWithGpsLinks Links { get; }

        public DailyRouteSchedule(DateTime dateTime, RouteInfo route)
        {
            DateTime = dateTime;
            Route = route;
            Links = new RideWithGpsLinks(route);
        }
    }
}