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
        //private readonly ProductService _productService;
        private readonly ExhibitionService _exhibitionService;
        public HomeController()
        {
            //_productService = new ProductService();
            _exhibitionService = new ExhibitionService();
        }
        public ActionResult Index()
        {
            var result = _exhibitionService.GetExhibitioning();
            return View(result);
        }
    }
}