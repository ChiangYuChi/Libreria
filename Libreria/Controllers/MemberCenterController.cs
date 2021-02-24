using Libreria.Filters;
using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.Mvc.Filters;
using static Libreria.Filters.CustomAuthenticationFilter;

namespace Libreria.Controllers
{
    [CustomAuthenticationFilter]
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
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            List<OrderViewModel> result = null;
            if (Inquire == "history")
            {
                result = _orderService.GetBymemberId(UserMemberId);
            }
            else if(Inquire == "oneMonth")
            {
                result = _orderService.GetBymemberId(UserMemberId, TimeSpan.FromDays(30));
            }
            else if (Inquire == "sixMonths")
            {
                result = _orderService.GetBymemberId(UserMemberId, TimeSpan.FromDays(30*6));
            }
            else if (Inquire == "notShipped")
            {
                // 未完成
                result = _orderService.GetByProgress(UserMemberId, "準備出貨中");
            }
            else if(Inquire == "return")
            {
                //未完成
                result = new List<OrderViewModel>();
            }
            else if(Inquire == "transactionId")
            {
                result = _orderService.GetByOrderId(TransactionId);
            }
            else
            {
                //預設代入一個月
                result = _orderService.GetBymemberId(UserMemberId, TimeSpan.FromDays(30));
            }
            ViewBag.Inquire = Inquire;

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
                    memberPassword = Utility.GetSha512(HttpUtility.HtmlEncode(model.memberPassword)),
                    birthday = DateTime.Parse(HttpUtility.HtmlEncode(model.birthday)),
                    Gender = int.Parse(HttpUtility.HtmlEncode(model.Gender)),
                    IDnumber = HttpUtility.HtmlEncode(model.IDnumber)
                };
                 //var a = member = _libreriaDataModel.members
                 //                  .Where(u => u.memberName == model.memberName)
                 //                  .SingleOrDefault();

                //EF
                //if (a == null)
                //{
                    try
                    {
                        _libreriaDataModel.members.Add(member);
                        _libreriaDataModel.SaveChanges();
                        return Redirect("MemberLogin");
                    }
                    //下列部分需要再處裡
                    catch (Exception ex)
                    {
                        return Content("新增帳號失敗:" + ex.ToString());
                    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "*帳號已被使用");
                //    return View(model);
                //}
                
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



        public ActionResult ContactUs()
        {
            return View();
        }
       
        [HttpPost]
        public void DeleteFavorite(FavoriteViewModel favoriteVM)
        {
            _favoriteService.DeleteFromFavorite(favoriteVM);

        }

    }
}