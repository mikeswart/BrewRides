using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CapitalBreweryBikeClub.Internal.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route-folder")]
    public sealed class ActiveRouteFolderTagHelper : ActiveRouteTagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ActiveRouteFolderTagHelper(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override string GetRequestPath()
        {

            if (!_contextAccessor.HttpContext.Request.RouteValues.TryGetValue("page", out var path))
            {
                return base.GetRequestPath();
            }

            var requestPath = path as string ?? string.Empty;
            if (requestPath.Contains("/"))
            {
                requestPath = requestPath.Substring(0, requestPath.LastIndexOf("/", StringComparison.InvariantCulture));
            }

            return requestPath;
        }
    }
}