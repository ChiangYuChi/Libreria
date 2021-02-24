using Libreria.Service;
using Libreria.ViewModels;
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
            return View();
        }



        [HttpPost]
        public ActionResult Confirm(RentalConfirmViewModel model)
        {
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmBooling(RentalConfirmViewModel model)
        {

            if (ModelState.IsValid) {

                _rentalService.ConfirmBooling(model);
                return View("index");
            }
            else
            {
                return View("Confirm", model);
            }
        }
    }
}