using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CapitalBreweryBikeClub
{
    public class RouteProvider
    {
        private Dictionary<string, RouteInfo> allRoutes;

        public IEnumerable<RouteInfo> Routes => allRoutes.Values;

        private readonly Action<bool> refreshAction;

        public RouteProvider(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            var fileCache = new LocalJsonFile<RouteInfo>(webHostEnvironment.ContentRootFileProvider, "routes.json");

            refreshAction = (force) =>
            {
                IEnumerable<RouteInfo> routes = null;
                if (fileCache.CacheExists())
                {
                    routes = fileCache.Load().Result;
                }

                if (force || routes == null)
                {
                    routes = new RouteWebProvider(configuration).GetRoutesFromWeb().Result;
                    fileCache.Save(routes);
                }

                allRoutes = routes.ToDictionary(info => info.Name.ToLower().Replace(' ', '-'));
            };

            refreshAction(false);
        }

        public RouteInfo Get(string routeName)
        {
            return allRoutes.TryGetValue(RouteInfo.GetWebFriendlyName(routeName), out var routeInfo) ? routeInfo : null;
        }

        public void Refresh()
        {
            refreshAction(true);
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
