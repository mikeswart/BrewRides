using CapitalBreweryBikeClub.Model;
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

    public class RouteDatabaseContext : DbContext
    {
        public DbSet<RouteData> Routes { get; set; }

        public RouteDatabaseContext(DbContextOptions<RouteDatabaseContext> options)
            : base(options)
            {
            }
    }
}
