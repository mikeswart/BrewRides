namespace CapitalBreweryBikeClub.Model
{
    public sealed class RouteInfo
    {
        public string Name { get; }

        public string RideWithGpsId { get; }

        public bool Available { get; }

        public string Link { get; }

        public string Mileage { get; }

        public string Info { get; }

        public RouteInfo(string name, string link, string mileage, string info, string rideWithGpsId, bool available)
        {
            Name = name;
            RideWithGpsId = rideWithGpsId;
            Available = available;
            Link = link;
            Mileage = mileage;
            Info = info;
        }

        public static string GetWebFriendlyName(string name)
        {
            return name.ToLower().Replace(" ", "_");
        }
    }
}