using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CapitalBreweryBikeClub.Internal.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-page")]
        public string Page { get; set; }

        private readonly IHttpContextAccessor _contextAccessor;

        public ActiveRouteTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll("is-active-route");
        }

        protected virtual string GetRequestPath()
        {
            return _contextAccessor.HttpContext.Request.Path.Value.TrimEnd('/');
        }

        private bool ShouldBeActive()
        {
            var page = Page;
            if (page.EndsWith("/index", StringComparison.InvariantCultureIgnoreCase))
            {
                page = page.Substring(0, page.LastIndexOf("/index", StringComparison.InvariantCultureIgnoreCase));
            }

            var requestPath = GetRequestPath();

            return page.Equals(requestPath, StringComparison.InvariantCultureIgnoreCase);
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }
    }
}