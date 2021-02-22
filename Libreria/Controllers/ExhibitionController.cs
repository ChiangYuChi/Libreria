using Libreria.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class ExhibitionController : Controller
    {
        private readonly ExhibitionService _exhibitionService;
        public ExhibitionController()
        {
            
            _exhibitionService = new ExhibitionService();
        }
        // GET: Exhibition
        public ActionResult Index()
        {
            var result = _exhibitionService.GetExhibitioning();
            var result2 = _exhibitionService.OverdueExhibitioning();
            var result3 = _exhibitionService.NotYetExhibitioning();
            ViewBag.OverdueExhibitioning = result2;
            ViewBag.NotYetExhibitioning = result3;


            return View(result);
        }
    }
}