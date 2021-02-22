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
        public ActionResult Confirm(ConfirmModel model)
        {
            var viewModel = new RentalConfirmViewModel();
            viewModel.ExhibitionEndTime = model.ExhibitionEndTime;
            viewModel.ExhibitionStartTime = model.ExhibitionStartTime;
            viewModel.StartDate = model.StartDate;
            viewModel.EndDate = model.EndDate;
            viewModel.StartTime = model.StartTime;
            viewModel.EndTime = model.EndTime;
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult ConfirmBooling(ConfirmBoolingModel model)
        {
            var service = new RentalService();
            service.ConfirmBooling(model);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }


        public class ConfirmBoolingModel
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime ExhibitionStartTime { get; set; }
            public DateTime ExhibitionEndTime { get; set; }
            public decimal Price { get; set; }
            public string ExCustomerName { get; set; }
            public string ExCustomerPhone { get; set; }
            public string ExCustomerEmail { get; set; }
            public string ExhibitionIntro { get; set; }
            public string MasterUnit { get; set; }
            public decimal ExhibitionPrice { get; set; }
            public string ExPhoto { get; set; }
            public string ExName { get; set; }
        }

        public class ConfirmModel
        {
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime ExhibitionStartTime { get; set; }
            public DateTime ExhibitionEndTime { get; set; }
        }
    }
}