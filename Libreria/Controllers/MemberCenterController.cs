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