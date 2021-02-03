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

        public ActionResult MemberLogout()
        {
            return View();
        }
        
    }
}