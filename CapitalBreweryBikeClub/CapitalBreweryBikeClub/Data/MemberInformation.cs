using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public class MemberInformation
    {
        [Key]
        public string Email { get; set; }
    }
}
