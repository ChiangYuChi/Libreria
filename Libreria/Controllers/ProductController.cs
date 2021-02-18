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
        private readonly CategoryService _categoryService;


        public ProductController()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();
        }

       


        public ActionResult Index()
        {
            return View();   
        }


        public ActionResult ProductCategory(string Sorting_Order)
        {

            ViewBag.SortingPrice = string.IsNullOrEmpty(Sorting_Order) ? "Price_Description" : "";
            ViewBag.SortingPublishTime = string.IsNullOrEmpty(Sorting_Order) ? "PublishTime_Description" : "";
            var products = _productService.GetAll();


            return View(products);

        }

         public PartialViewResult ProductPartial(int? category)
        {
            if (category != null)
            {
                var result = _productService.GetByCategory(Convert.ToInt32(category));
                return PartialView(result);
            }
            else
            {
                var result = _productService.GetAll();
                return PartialView(result);
            }
        }

        public ActionResult ProductDetail(int id)
        {

            var product = _productService.GetById(id);

            
            return View(product);
        }


      
       

    }
}