using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapitalBreweryBikeClub.ViewComponents
{
    public class RouteView : ViewComponent
    {
        public IViewComponentResult Invoke(RouteData routeData)
        {
            return View(routeData);
        }
    }
}
