using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace CapitalBreweryBikeClub.Internal
{
    public class RouteProvider
    {
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory scopeFactory;

        public IImmutableDictionary<string, RouteInfo> Routes
        {
            get;
            private set;
        }

        public RouteProvider(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            this.configuration = configuration;
            this.scopeFactory = scopeFactory;

            using var _ = scopeFactory.CreateDatabaseContextScope(out BrewRideDatabaseContext dbContext);
            ReloadRoutesFromDatabase(dbContext);
        }

        public void Refresh()
        {
            using var _ = scopeFactory.CreateDatabaseContextScope(out BrewRideDatabaseContext dbContext);

            var routes = new RouteWebProvider(configuration).GetRoutesFromWeb().Result;
            dbContext.Routes.Clear();
            dbContext.Routes.AddRange(routes.Select(info => info.GetRouteData()).ToList());
            dbContext.SaveChanges();

            ReloadRoutesFromDatabase(dbContext);
        }

        private void ReloadRoutesFromDatabase(BrewRideDatabaseContext dbContext)
        {
            Routes = dbContext.Routes.Select(routeData => new RouteInfo(routeData))
                .ToImmutableDictionary(info => RouteInfo.GetWebFriendlyName(info.Name));
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
}