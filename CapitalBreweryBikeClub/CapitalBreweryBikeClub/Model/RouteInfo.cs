namespace CapitalBreweryBikeClub.Model
{
    public sealed class RouteInfo
    {
        public string Name { get; set; }

        public string RideWithGpsId { get; set; }

        public bool Available { get; set; }

        public string Link { get; set; }

        public string Mileage { get; set; }

        public string Info { get; set; }

        public RouteInfo(string name, string link, string mileage, string info, string rideWithGpsId, bool available)
        {
            Available = available;
            Info = info;
            Link = link;
            Mileage = mileage;
            Name = name;
            RideWithGpsId = rideWithGpsId;
        }

        public RouteInfo(RouteData routeData)
        {
            Available = routeData.Available;
            Info = routeData.Info;
            Link = routeData.Link;
            Mileage = routeData.Mileage;
            Name = routeData.Name;
            RideWithGpsId = routeData.RideWithGpsId;
        }

        public static string GetWebFriendlyName(string name)
        {
            return name.ToLower().Replace(" ", "_");
        }

        public RouteData GetRouteData()
        {
            return new RouteData
            {
                Available = Available,
                Info = Info,
                Link = Link,
                Mileage = Mileage,
                Name = Name,
                RideWithGpsId = RideWithGpsId
            };
        }
    }
}
