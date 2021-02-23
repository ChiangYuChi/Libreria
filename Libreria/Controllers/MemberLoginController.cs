using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class MemberLoginController : Controller
    {
        private readonly LibreriaDataModel _libreriaDataModel;
        public MemberLoginController()
        {
            _libreriaDataModel = new LibreriaDataModel();
        }
        // GET: MemberLogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(MemberLoginViewModel model)
        {
            string passwordSha512 = Utility.GetSha512(model.MemberPassword);
            if (ModelState.IsValid)
            {

                member member = _libreriaDataModel.members
                                               .Where(u => u.memberName == model.MemberName && u.memberPassword == passwordSha512)
                                               .FirstOrDefault();

                if (member != null)
                {
                    Session["MemberName"] = member.memberName;
                    Session["MemberPassword"] = member.memberPassword;
                    Session["MemberID"] = member.memberId;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "帳號或密碼輸入錯誤");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["MemberName"] = string.Empty;
            Session["MemberPassword"] = string.Empty;
            Session["MemberID"] = string.Empty; 
            return RedirectToAction("Index", "Home");
        }



    }
}