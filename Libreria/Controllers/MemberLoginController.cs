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
        public readonly MemberService _memberService;
        public MemberLoginController()
        {
            _memberLoginService = new MemberLoginService();
            _shoppingService = new ShoppingService();
            _memberService = new MemberService();
        }
        // GET: MemberLogin
        [HttpGet]
        public ActionResult Index()
        {
           return View();
        }
        
        [HttpPost]
        public ActionResult Index(MemberLoginViewModel model, FormCollection form)
        {
            string gRecaptchaResponse = form["g-recaptcha-response"]; //"g-recaptcha-response無法透過ViewModel接收
            ReCaptchaViewModel reCaptchaVM = Utility.GetRecaptchaVaildation(gRecaptchaResponse);
            if(reCaptchaVM.success != true)
            {
                ModelState.AddModelError("", "請勾選我不是機器人");
                return View(model);
            }

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