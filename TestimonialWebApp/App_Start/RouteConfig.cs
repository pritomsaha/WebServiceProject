using System.Web.Mvc;
using System.Web.Routing;

namespace TestimonialWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
                name: "apply",
				url: "apply/",
				defaults: new { controller = "Home", action = "Apply"}
            );

			routes.MapRoute(
                name: "status",
                url: "status/",
                defaults: new { controller = "Home", action = "Status" }
            );

			routes.MapRoute(
                name: "getStatus",
                url: "getStatus/",
                defaults: new { controller = "Home", action = "GetStatus" }
            );

			routes.MapRoute(
                name: "submit",
                url: "submit/",
                defaults: new { controller = "Home", action = "Submit" }
            );

			routes.MapRoute(
				name: "verifyStudent",
                url: "verifyStudent/",
                defaults: new { controller = "Home", action = "VerifyStudent", id = UrlParameter.Optional }
            );

			routes.MapRoute(
                name: "index",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

        }
    }
}
