using System.IO;
using System.Linq;
using CapitalBreweryBikeClub.Model;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Data
{
    public class RouteDatabaseContext : DbContext
    {
        public DbSet<Note> Notes
        {
            get; set;
        }

        public DbSet<RouteData> Routes
        {
            get; set;
        }

        public DbSet<MemberInformation> Members
        {
            get; set;
        }

        public DbSet<SiteState> SiteState
        {
            get; set;
        }

        public RouteDatabaseContext(DbContextOptions<RouteDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (File.Exists(SeedFileNames.Members))
            {
                modelBuilder.Entity<MemberInformation>().HasData(
                    File.ReadLines(SeedFileNames.Members)
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line => new MemberInformation { Email = line }));
            }

            modelBuilder.Entity<SiteState>().HasData(new SiteState() { Id = 1 });
        }

        private static class SeedFileNames
        {
            public static readonly string Members = "members.seed";
        }
    }
}
