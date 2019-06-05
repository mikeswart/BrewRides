using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace CapitalBreweryBikeClub
{
    public class RouteProvider
    {
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory scopeFactory;

        public RouteProvider(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            this.configuration = configuration;
            this.scopeFactory = scopeFactory;
        }

        public void Refresh()
        {
            using var scope = scopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<BrewRideDatabaseContext>();

            var routes = new RouteWebProvider(configuration).GetRoutesFromWeb().Result;
            dbContext.Routes.Clear();
            dbContext.Routes.AddRange(routes.ToList());

            dbContext.SaveChanges();
        }

        private sealed class RouteWebProvider
        {
            private readonly string apiKey;

            public RouteWebProvider(IConfiguration configuration)
            {
                apiKey = configuration["GoogleSheets:ApiKey"];
            }

            public async Task<IEnumerable<RouteInfo>> GetRoutesFromWeb()
            {
                return (await Task.WhenAll(GetRouteInfo("long"), GetRouteInfo("short"))).SelectMany(infos => infos.ToArray());
            }

            private async Task<IEnumerable<RouteInfo>> GetRouteInfo(string tableName)
            {
                using var httpClient = new HttpClient();
                var resultsJson = await httpClient.GetStringAsync($"https://sheets.googleapis.com/v4/spreadsheets/1zgT74PMvCWC4LGKk_lVo7qN1gmpuNXWmcsuYg4PU7Fo/values/{tableName}?key={apiKey}");
                var jObject = JObject.Parse(resultsJson);

                return jObject["values"].Skip(1).Select(RouteInfoFactory);

                RouteInfo RouteInfoFactory(JToken token)
                {
                    return new RouteInfo((string)token[0], (string)token[1], (string)token[2], (string)token[3], (string)token[4], true);
                }
            }
        }
    }

    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
