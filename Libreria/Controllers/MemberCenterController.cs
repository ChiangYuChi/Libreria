using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class MemberCenterController : Controller
    {

        // GET: MemberCenter
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

        
        public ActionResult MemberOrderInquery()
        {
            return View();
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