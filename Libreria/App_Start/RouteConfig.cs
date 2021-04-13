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
          
         //依據產品類別編號搜尋
         // Procudt/FindCagegory/1
          routes.MapRoute(
              name: "FindCategory",
              url: "Product/FindCategory/{CategoryId}/{Order}",
              defaults: new { controller = "Product", action = "ProductCategory", CategoryId = UrlParameter.Optional,Order = UrlParameter.Optional}
          );
          //依據產品中文類別搜尋
          //Product/FindName/旅遊
          routes.MapRoute(
                name: "FindCategoryName",
                url: "Product/FindName/{CategoryName}",
                defaults: new { controller = "Product", action = "ProductCategoryName", CategoryName = UrlParameter.Optional }
         );
         //依據產品價格區間搜尋
         //Product/FindCategory/1/300-600/1
            routes.MapRoute(
                  name: "BooksByRange",
                  url: "Product/FindCategory/{CategoryName}/{min_price}-{max_price}/{ActivePageNum}",
                  defaults: new { controller = "Product", action = "ProductCategoryName", CategoryName = "All",min_price =0,max_price=9999,ActivePageNum=1}
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            



        }
    }
}
