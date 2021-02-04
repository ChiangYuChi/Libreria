using Libreria.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class SalesManController : Controller
    {
        private readonly SalesManService _salesManService;
        public SalesManController()
        {
            _salesManService = new SalesManService();
        }
        public ActionResult Index()
        {
            var result = _salesManService.GetAll();
            return View(result);
        }
    }
}