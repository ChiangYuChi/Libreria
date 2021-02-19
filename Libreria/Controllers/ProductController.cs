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


        public ActionResult ProductCategory(int? CategoryId, int? Order)
        {
            List<ProductViewModel> result;

            if (CategoryId != null)
            {
                result = _productService.GetByCategory(Convert.ToInt32(CategoryId));
                ViewBag.CategoryId = CategoryId;
            }
            else
            {
                result = _productService.GetAll();
            }

            if (Order == 1)
            {
                result = result.OrderBy(x => x.UnitPrice).ToList();
            }
            else if (Order == 2)
            {
                result = result.OrderBy(x => x.CreateTime).ToList();
            }

            return View(result);

        }

        public ActionResult ProductDetail(int id)
        {
            var product = _productService.GetById(id);   

            return View(product);
        }

        public PartialViewResult PromoteByPublishDatePartial()
        {
            var product = _productService.GetByPublishDate();
            return PartialView(product);
        }

        public PartialViewResult PromoteByTotalSalesPartial()
        {
            var product = _productService.GetByTotalSales();
            return PartialView(product);
        }
        
        public ActionResult testLayout()
        {
            return View();
        }

        

        
    }
}