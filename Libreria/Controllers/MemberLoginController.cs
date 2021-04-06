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
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace Libreria.Controllers
{
    public class MemberLoginController : Controller
    {
        private readonly MemberLoginService _memberLoginService;
        private readonly ShoppingService _shoppingService;
        private readonly MemberService _memberService;
        private readonly LineLoginService _lineLoginService;
        public MemberLoginController()
        {
            _memberLoginService = new MemberLoginService();
            _shoppingService = new ShoppingService();
            _memberService = new MemberService();
            _lineLoginService = new LineLoginService();

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

            MemberLoginViewModel result = new MemberLoginViewModel();
            return View(result);
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
            Session["ChangeMemberName"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ResetEmail()
        {
            return View();
        }

        [HttpPost]
        public string SendResetEmail(string Email)
        {
            var callbackUrl = Url.Action("ResetPassword", "MemberLogin", new {}, protocol:Request.Url.Scheme);
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
        public string ConfirmResetPassword(ResetPasswordViewModel resetVM)
        {
            var result = _memberService.UpdatePassword(resetVM.username, resetVM.password);

            if (result.IsSuccessful)
            {
                return "密码修改成功!";
            }
            else
            {
                return "密码修改失败,请重试";
            }
        }

       
        public ActionResult LineLoginDirect()
        {
            string response_type = "code";
            string client_id = "1655754480";
            //string redirect_uri = HttpUtility.UrlEncode("https://localhost:44330/MemberLogin/Callback");
            string redirect_uri = HttpUtility.UrlEncode("https://weblibreria.azurewebsites.net/MemberLogin/Callback");
            string state = "statePassword";
            string LineLoginUrl = string.Format("https://access.line.me/oauth2/v2.1/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}&scope=openid%20profile%20email&nonce=09876xyz",
                response_type,
                client_id,
                redirect_uri,
                state
                );
            return Redirect(LineLoginUrl);
        }

        public ActionResult Callback(string code, string state)
        {
                
            if (state == "statePassword")
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                NameValueCollection nvc = new NameValueCollection();

                try
                {
                    //取回Token
                    string ApiUrl_Token = "https://api.line.me/oauth2/v2.1/token";
                    nvc.Add("grant_type", "authorization_code");
                    nvc.Add("code", code);
                    //nvc.Add("redirect_uri", "https://localhost:44330/MemberLogin/Callback");
                    nvc.Add("redirect_uri", "https://weblibreria.azurewebsites.net/MemberLogin/Callback");
                    nvc.Add("client_id", "1655754480");
                    nvc.Add("client_secret", "01688ad326564fb2a0b8004c7c7fc94c");
                    string JsonStr = Encoding.UTF8.GetString(wc.UploadValues(ApiUrl_Token, "POST", nvc));
                    LineLoginTokenViewModel ToKenObj = JsonConvert.DeserializeObject<LineLoginTokenViewModel>(JsonStr);

                    var id_token = ToKenObj.id_token;

                    var jst = new JwtSecurityToken(id_token);
                    LineProfileViewModel user = new LineProfileViewModel();
                    user.userId = jst.Payload.Sub;
                    user.displayName = jst.Payload["name"].ToString();
                    if (jst.Payload.ContainsKey("email") && !string.IsNullOrEmpty(Convert.ToString(jst.Payload["email"])))
                    {
                        user.email = jst.Payload["email"].ToString();
                    }

                    //將Token解析回的使用者訊息做出一個LineLoginViewModel
                    LineLoginViewModel model = new LineLoginViewModel()
                    {
                        LineUserID = user.userId,
                        diaplayName = user.displayName,
                        Email = user.email
                    };

                    var result = _lineLoginService.CreateOrLoginLineMember(model, ModelState.IsValid);
                    if (ModelState.IsValid)
                    {
                        if (result.IsSuccessful==true)
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
                catch (Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
            return View();
        }
    }
}