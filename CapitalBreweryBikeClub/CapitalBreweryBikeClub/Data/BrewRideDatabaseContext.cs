using CapitalBreweryBikeClub.Model;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Data
{
    public class BrewRideDatabaseContext : DbContext
    {
        public DbSet<RouteInfo> Routes { get; set; }

        public BrewRideDatabaseContext(DbContextOptions<BrewRideDatabaseContext> options)
            : base(options)
        {
        }
    }
}
