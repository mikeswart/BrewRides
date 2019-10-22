using System;
using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public class ScheduleData
    {
        [Key]
        public int Id
        {
            get; set;
        }

        [Required]
        public DateTime Date
        {
            get; set;
        }

        [Required]
        public RouteData RouteData
        {
            get; set;
        }
    }
}
