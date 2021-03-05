
using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Libreria.Controllers
{
    public class MemberLoginController : Controller
    {     
        public readonly MemberLoginService _memberLoginService;
        public readonly ShoppingService _shoppingService;
        public MemberLoginController()
        {
            _memberLoginService = new MemberLoginService();
            _shoppingService = new ShoppingService();
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
            var result = _memberLoginService.GetMember(model, ModelState.IsValid);
            if (ModelState.IsValid)
            {
                if (result != null)
                {
                    _shoppingService.CombineCarts();
                    return RedirectToAction("MemberLogin", "MemberCenter");
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
            Session["MemberID"] = null;
            return RedirectToAction("Index", "Home");
        }



    }
}