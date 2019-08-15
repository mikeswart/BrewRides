using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Data
{
    public class BrewRideUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public BrewRideUserDbContext(DbContextOptions<BrewRideUserDbContext> options)
            : base(options)
        {
        }
    }
}
