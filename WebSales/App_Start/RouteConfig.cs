using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSales
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "ProductCategory",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "Product",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "DetailProduct",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "ShoppingCart",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "CheckOut",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "Blog",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "Blog", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "BlogDetail",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
            routes.MapRoute(
                name: "VnpayReturn",
                url: "{controller}/{action}/{alias}",
                defaults: new { controller = "ShoppingCart", action = "VnpayReturn", alias = UrlParameter.Optional },
                namespaces: new[] { "WebSales.Controllers" }
            );
        }
    }
}
