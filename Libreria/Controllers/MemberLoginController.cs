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
using isRock.LineLoginV21;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;



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
            //記得我
            var memberLogin = Request.Cookies["MemberLogin"];
            if (memberLogin != null)
            {
                ViewBag.memberName = memberLogin["MemberName"];
                ViewBag.memberPassword = memberLogin["MemberPassword"];
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult Index(MemberLoginViewModel model, FormCollection form)
        {
            string gRecaptchaResponse = form["g-recaptcha-response"]; //"g-recaptcha-response無法透過ViewModel接收
            ReCaptchaViewModel reCaptchaVM = Helpers.Utility.GetRecaptchaVaildation(gRecaptchaResponse);
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

        //登出
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["MemberName"] = string.Empty;
            Session["MemberPassword"] = string.Empty;
            Session["MemberID"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        [HttpGet]
        public ActionResult Callback()
        {
            string code = Request.QueryString["code"];
            if (code == null)
            {
                ViewBag.access_token = "沒有正確的code...";
                return View("Index");
            }
            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code,
            "1655754480",  //client_id
            "01688ad326564fb2a0b8004c7c7fc94c", //client_secret
            "https://localhost:44330/MemberCenter/MemberLogin");  //Call back URL相同
            var email = "";

            //利用access_token取得用戶資料
            var user = isRock.LineLoginV21.Utility.GetUserProfile(token.access_token);
            //利用id_token取得Claim資料
            var JwtSecurityToken = new JwtSecurityToken(token.id_token);
            //如果有email取得emil
            if (JwtSecurityToken.Claims.ToList().Find(c => c.Type == "email") != null)
                email = JwtSecurityToken.Claims.First(c => c.Type == "email").Value;

            ViewBag.email = email;
            ViewBag.access_token = token.access_token;
            ViewBag.displayName = user.displayName;
            return View("index");
        }
        [HttpPost]
        public ActionResult GetUserProfile(string Token, string email)
        {
            var user = isRock.LineLoginV21.Utility.GetUserProfile(Token);
            ViewBag.UserProfileJSON = JsonConvert.SerializeObject(user);

            ViewBag.email = email;
            ViewBag.access_token = Token;
            return View("Index");
        }
    }
}