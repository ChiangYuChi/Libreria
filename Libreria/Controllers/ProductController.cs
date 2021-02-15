using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        public ActionResult Index()
        {
            return View();
                
        }


        public ActionResult ProductCategory(string Sorting_Order)
        {

            ViewBag.SortingPrice = string.IsNullOrEmpty(Sorting_Order) ? "Price_Description" : "";
            ViewBag.SortingPublishTime = string.IsNullOrEmpty(Sorting_Order) ? "PublishTime_Description" : "";
            var products = from product in _productService.GetAll()
                           select product;
            switch (Sorting_Order)
            {
                case "Price_Description":

                   products= products.OrderBy(p => p.UnitPrice);
                    break;
                case "PublishTime_Description":
                    products = products.OrderBy(p => p.CreateTime);
                    break;
                default:
                    products = products.OrderBy(product => product.Name);
                    break;
            }

            return View(products.ToList());

        }

         public PartialViewResult ProductPartial(int?category)
        {
            if (category != null)
            {
                ViewBag.category = category;
                var products = _productService.GetAll();
                var productList = products.Where((x) => x.CategoryId == category);
                return PartialView(productList.ToList());
            }
            else
            {
                var result = _productService.GetAll();
                return PartialView(result);
            }
           
        }
        
    }
}