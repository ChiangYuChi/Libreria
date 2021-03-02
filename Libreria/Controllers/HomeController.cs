using Libreria.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.ViewModels;
using Libreria.Models.EntityModel;
using Libreria.Service;

namespace Libreria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly ExhibitionService _exhibitionService;
        public HomeController()
        {
            _productService = new ProductService();
            _exhibitionService = new ExhibitionService();
        }
        public ActionResult Index()
        {
            var result = _exhibitionService.GetExhibitioning();
            var result2 = _productService.GetByTotalSalesHome();
            var result3 = _productService.GetByPublishDateHome();

            
            ViewBag.TotalSales = result2;

            ViewBag.GetByPublishDateHome = result3;



            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Un Authorized Page!";

            return View();

        }
    }
}