using Libreria.Filters;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    [CustomAuthenticationFilter]
    public class MemberCenterController : Controller
    {
        private readonly OrderService _orderService;
        // GET: MemberCenter

        public MemberCenterController()
        {
            _orderService = new OrderService();
        }

        public ActionResult MemberLogin()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult MemberLogin(member model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        using (var context = new LibreriaDataModel())
        //        {
        //            member member = context.members
        //                               .Where(u => u.memberId == model.memberId && u.memberPassword== model.memberPassword)
        //                               .FirstOrDefault();
        //            if (User != null)
        //            {
        //                Session["MemberName"] = member.memberName;
        //                Session["MemberId"] = member.memberId;
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "帳號或密碼輸入錯誤");
        //                return View(model);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        return View(model);
        //    }

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    Session["MemberName"] = string.Empty;
        //    Session["MemberId"] = string.Empty;
        //    return RedirectToAction("Index", "Home");
        //}


        public ActionResult MemberInfo()
        {
            return View();
        }
        public ActionResult ChangePassward()
        {
            return View();
        }

        
        public ActionResult MemberOrderInquery(string Inquire, int? TransactionId)
        {
            List<OrderViewModel> result = null;
            if (Inquire == "history")
            {
                int memberId = 1;
                result = _orderService.GetBymemberId(memberId);
            }

            return View(result);
        }

        public ActionResult MemberPasswordConfirm()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }

        public ActionResult Subscribe_info()
        {
            return View();
        }

        public ActionResult Subscribe_cancel()
        {
            return View();
        }
        public ActionResult MemberContact()
        {
            return View();
        }

        public ActionResult MemberRegisterPage()
        {
            return View();
        }

    }
}