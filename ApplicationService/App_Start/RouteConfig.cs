using System.Web.Mvc;
using System.Web.Routing;

namespace ApplicationService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
                name: "list",
                url: "list/",
                defaults: new { controller = "Home", action = "List", id = UrlParameter.Optional }
            );


			routes.MapRoute(
                name: "deliver",
                url: "deliver/",
                defaults: new { controller = "Home", action = "Deliver", id = UrlParameter.Optional }
            );

			routes.MapRoute(
                name: "testimonial",
				url: "testimonial/",
				defaults: new { controller = "Home", action = "GenerateTestimonial", id = UrlParameter.Optional }
            );

			routes.MapRoute(
                name: "login",
                url: "login/",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );

			routes.MapRoute(
                name: "logout",
                url: "logout/",
                defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
            );

			routes.MapRoute(
                name: "index",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}
