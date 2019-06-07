using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public sealed class RouteData
    {
        public bool Available { get; set; }

        public string Info { get; set; }

        public string Link { get; set; }

        public string Mileage { get; set; }

        public string Name { get; set; }

        [Key]
        public string RideWithGpsId { get; set; }
    }
}
