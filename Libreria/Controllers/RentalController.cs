using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ViewBag.PickDateRange = _rentalService.GetPickDateRange();
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
        public async Task<ActionResult> ConfirmBooling(RentalConfirmViewModel model)
        {
            if (ModelState.IsValid) {

                await _rentalService.ConfirmBooling(model);
                var result = _rentalService.ECPay(model);
                TempData["RentalPayOrderResult"] = result;
                
                return RedirectToAction("PayOrder","Rental");
                
            }
            else
            {
                return View("Confirm", model);
            }
        }

        public ActionResult PayOrder()
        {
            ViewBag.html = (string)TempData["RentalPayOrderResult"];
            return View("PayOrder");
        }

        public ActionResult PayReturnDetail(int orderId, FormCollection form)
        {

            var RtnCode = form["RtnCode"];
            List<RentalConfirmViewModel> orderVMList = _rentalService.GetByOrderId(orderId);
            RentalConfirmViewModel orderVM = orderVMList.FirstOrDefault();
            _rentalService.SetState(orderVM, RtnCode);

            ViewData["OrderNum"] = form["MerchantTradeNo"];
            return View();
        }

        public PartialViewResult CartMsgPartial()
        {
            return PartialView();
        }

    }
}