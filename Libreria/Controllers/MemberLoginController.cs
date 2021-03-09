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

        public ActionResult ResetEmail()
        {
            return View();
        }

        [HttpPost]
        public string SendResetEmail(string Email)
        {
            var callbackUrl = Url.Action("ResetPassword", "MemberLogin");
            var result = _memberService.SendEmail(Email, callbackUrl);

            if (result.IsSuccessful)
            {
                return "成功发出!";
            }
            else
            {
                return "发出失败,请检查后重试";
            }
            
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public string ConfirmResetPassword(string username, string password)
        {
            var result = _memberService.UpdatePassword(username, password);

            if (result.IsSuccessful)
            {
                return "密码修改成功!";
            }
            else
            {
                return "密码修改失败,请重试";
            }
        }



    }
}