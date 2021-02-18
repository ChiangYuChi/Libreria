using Libreria.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class RentalController : Controller
    {
        private readonly RentalService _rentalService;

        public RentalController()
        {
            _rentalService = new RentalService();
        }
        // GET: Rental
        public ActionResult Index()
        {
            //var result = _rentalService.GetAll();
            return View();
        }


    }
}