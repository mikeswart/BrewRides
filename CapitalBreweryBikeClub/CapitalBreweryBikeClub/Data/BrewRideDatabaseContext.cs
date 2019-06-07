using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Data
{
    public class BrewRideDatabaseContext : IdentityDbContext
    {
        public DbSet<RouteData> Routes { get; set; }

        public BrewRideDatabaseContext(DbContextOptions<BrewRideDatabaseContext> options)
            : base(options)
        {
        }
    }
}
