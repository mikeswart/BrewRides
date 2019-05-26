namespace CapitalBreweryBikeClub.Model
{
    public sealed class RideWithGpsLinks
    {
        public string EmbedLink { get; }

        public string FitLink { get; }

        public string CueLink { get; }

        public RideWithGpsLinks(RouteInfo route)
        {
            EmbedLink = $"https://ridewithgps.com/routes/{route.RideWithGpsId}/embed";
            FitLink = $"https://ridewithgps.com/routes/{route.RideWithGpsId}.fit";
            CueLink = $"https://ridewithgps.com/routes/{route.RideWithGpsId}/cue_sheet.html";
        }
    }
}