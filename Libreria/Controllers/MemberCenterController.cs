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
        private readonly OrderService _orderService;

        public MemberCenterController()
        {
            _orderService = new OrderService();
            _favoriteService = new FavoriteService();
            _libreriaDataModel = new LibreriaDataModel();
        }

        public ActionResult MemberLogin()
        {
            return View();
        }


        // GET: MemberCenter

        public ActionResult MemberInfo()
        {
            return View();
        }
        public ActionResult ChangePassward()
        {
            return View();
        }

        
        public ActionResult MemberOrderInquery(string Inquire, int? TransactionId)
        {
            int memberId = 1; //假資料

            List<OrderViewModel> result = null;
            if (Inquire == "history")
            {
                result = _orderService.GetBymemberId(memberId);
            }
            else if(Inquire == "oneMonth")
            {
                result = _orderService.GetBymemberId(memberId, TimeSpan.FromDays(30));
            }
            else if (Inquire == "sixMonths")
            {
                result = _orderService.GetBymemberId(memberId, TimeSpan.FromDays(30*6));
            }
            else if (Inquire == "notShipped")
            {
                // 未完成
                result = _orderService.GetBymemberId(memberId);
            }
            else if(Inquire == "return")
            {
                //未完成
                result = null;
            }
            else
            {
                //預設代入一個月
                result = _orderService.GetBymemberId(memberId, TimeSpan.FromDays(30));
            }

            return View(result);
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
                
                member member = new member
                {
                    memberName = HttpUtility.HtmlEncode(model.memberName),
                    MobileNumber = HttpUtility.HtmlEncode(model.MobileNumber),
                    HomeNumber = HttpUtility.HtmlEncode(model.HomeNumber),
                    Address = HttpUtility.HtmlEncode(model.Address),
                    Email = HttpUtility.HtmlEncode(model.Email),
                    memberUserName = HttpUtility.HtmlEncode(model.memberUserName),
                    memberPassword = HttpUtility.HtmlEncode(model.memberPassword),
                    birthday = DateTime.Parse(HttpUtility.HtmlEncode(model.birthday)),
                    Gender = int.Parse(HttpUtility.HtmlEncode(model.Gender)),
                    IDnumber = HttpUtility.HtmlEncode(model.IDnumber)
                };
                //EF
                try
                {
                    _libreriaDataModel.members.Add(member);
                    _libreriaDataModel.SaveChanges();
                    return Redirect("MemberLogin");
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

        public ActionResult ContactUs()
        {
            return View();
        }
       
    }
}