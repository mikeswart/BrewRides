using Microsoft.AspNetCore.Identity;

namespace CapitalBreweryBikeClub.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string AdditionalData { get; set; }
    }
}
