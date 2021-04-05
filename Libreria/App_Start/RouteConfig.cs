using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Libreria
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
          routes.MapRoute(
              name: "FindCategory",
              url: "Product/Category/{CategoryId}",
              defaults: new { controller = "Product", action = "ProductCategory", CategoryId = UrlParameter.Optional }
          );

          routes.MapRoute(
               name: "FindPage",
               url: "Product/Category/{CategoryId}/{Page}",
               defaults: new { controller = "Product", action = "ProductCategory", CategoryId ="CategoryId",Page= UrlParameter.Optional }
           );




            routes.MapRoute(
             name: "FindOrder",
             url: "Product/Order/{Order}",
             defaults: new { controller = "Product", action= "ProductOrder",Order = UrlParameter.Optional }
         );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            



        }
    }
}
