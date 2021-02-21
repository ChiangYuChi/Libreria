using Libreria.Filters;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static Libreria.Filters.CustomAuthenticationFilter;

namespace Libreria.Controllers
{
    //[CustomAuthenticationFilter]
    public class MemberCenterController : Controller
    {
        private readonly FavoriteService _favoriteService;
        private readonly LibreriaDataModel _libreriaDataModel;


        public MemberCenterController()
        {
            _favoriteService = new FavoriteService();
            _libreriaDataModel = new LibreriaDataModel();
        }


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
        [CustomAllowAnonymous]
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
        [HttpGet]
        [CustomAllowAnonymous]
        public ActionResult MemberRegisterPage()
        {
            return View();
        }        
        [HttpPost]
        [CustomAllowAnonymous]
        public ActionResult MemberRegisterPage(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string memberName = HttpUtility.HtmlEncode(model.memberName); //這是帳號
                //string MobileNumber = HttpUtility.HtmlEncode(model.MobileNumber);
                //string HomeNumber = HttpUtility.HtmlEncode(model.HomeNumber);
                //string Address = HttpUtility.HtmlEncode(model.Address);
                //string Email = HttpUtility.HtmlEncode(model.Email);
                //string memberUserName = HttpUtility.HtmlEncode(model.memberUserName);
                //string memberPassword = HttpUtility.HtmlEncode(model.memberPassword);
                //string birthday = HttpUtility.HtmlEncode(model.birthday);
                //string Gender = HttpUtility.HtmlEncode(model.Gender);
                //string IDnumber = HttpUtility.HtmlEncode(model.IDnumber);

                member member = new member
                {
                    memberName = HttpUtility.HtmlEncode(model.memberName),//這是帳號
                    MobileNumber = HttpUtility.HtmlEncode(model.MobileNumber),
                    HomeNumber = HttpUtility.HtmlEncode(model.HomeNumber),
                    Address = HttpUtility.HtmlEncode(model.Address),
                    Email = HttpUtility.HtmlEncode(model.Email),
                    memberUserName = HttpUtility.HtmlEncode(model.memberUserName),
                    memberPassword = HttpUtility.HtmlEncode(model.memberPassword),
                    birthday = DateTime.Parse(HttpUtility.HtmlEncode(model.birthday)),
                    Gender = Int32.Parse(HttpUtility.HtmlEncode(model.Gender)),
                    IDnumber = HttpUtility.HtmlEncode(model.IDnumber)
                };
                //EF
                try
                {
                    _libreriaDataModel.members.Add(member);
                    _libreriaDataModel.SaveChanges();
                    return View("歡迎光臨!");
                }
                //下列部分需要再處裡
                catch(Exception ex)
                {
                    return Content("新增帳號失敗:" + ex.ToString());
                }
            }
            return View();
        }
        [CustomAllowAnonymous]
        public ActionResult Favorite()
        {
            var result = _favoriteService.GetAll();
            return View(result);
        }

        [HttpPost]
        public string AddToFavorite(ProductViewModel ProductVM)
        {
            var result = _favoriteService.Create(ProductVM);

            if (result.IsSuccessful)
            {
                return "加入成功!";
            }
            else
            {
                return "加入失败";
            }
        }



        //[HttpPost]
        //public string AddToCart(FavoriteViewModel favoriteVM)
        //{
        //    var result = _favoriteService.AddToCart(favoriteVM);
        //    var CanTakeMemberNameFromThisVariable = System.Web.HttpContext.Current.Session["MemberID"];
        //    //var result = _favoriteService.Create(productVM);

        //    if (result.IsSuccessful)
        //    {
        //        return "加入成功!";
        //    }
        //    else
        //    {
        //        return "加入失败";
        //    }
        //}

        
       
    }
}