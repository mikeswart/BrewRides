using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public sealed class RouteInfo
    {
        public string Name { get; set; }

        [Key]
        public string RideWithGpsId { get; set; }

        public bool Available { get; set; }

        public string Link { get; set; }

        public string Mileage { get; set; }

        public string Info { get; set; }

        public RouteInfo(string name, string link, string mileage, string info, string rideWithGpsId, bool available)
        {
            Name = name;
            RideWithGpsId = rideWithGpsId;
            Available = available;
            Link = link;
            Mileage = mileage;
            Info = info;
        }

        public RouteInfo()
        {
        }

        public static string GetWebFriendlyName(string name)
        {
            return name.ToLower().Replace(" ", "_");
        }
    }
}