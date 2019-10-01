namespace CapitalBreweryBikeClub.Model
{
    public sealed class RideWithGpsLinks
    {
        public string Page { get; }

        public string EmbedLink { get; }

        public string FitLink { get; }

        public string CueLink { get; }

        public RideWithGpsLinks(RouteInfo route)
            : this(route?.RideWithGpsId ?? "")
        {
        }

        public RideWithGpsLinks(string rideWithGpsId)
        {
            if(string.IsNullOrWhiteSpace(rideWithGpsId))
            {
                return;
            }

            Page = $"https://ridewithgps.com/routes/{rideWithGpsId}";
            EmbedLink = $"https://ridewithgps.com/routes/{rideWithGpsId}/embed";
            FitLink = $"https://ridewithgps.com/routes/{rideWithGpsId}.fit";
            CueLink = $"https://ridewithgps.com/routes/{rideWithGpsId}/cue_sheet.html";
        }
    }
}
