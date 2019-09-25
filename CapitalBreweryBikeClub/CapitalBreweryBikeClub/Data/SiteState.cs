using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public class SiteState
    {
        [Key]
        public int Id {get;set;}

        public virtual Note Note
        {
            get;
            set;
        }
    }
}
