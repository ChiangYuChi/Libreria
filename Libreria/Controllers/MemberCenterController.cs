﻿using Libreria.Filters;
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
        private readonly MemberRegisterPageService _memberRegisterPageService;
        private readonly OrderService _orderService;

        public MemberCenterController()
        {
            _orderService = new OrderService();
            _favoriteService = new FavoriteService();
            _memberRegisterPageService = new MemberRegisterPageService();
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
            var result = _memberRegisterPageService.CreateMember(model, ModelState.IsValid);

            if (result.IsSuccessful)
            {  
               return Redirect("MemberLogin");                   
               
            }
            //還需登入失敗的頁面跳轉
            return View();
        }

        //[Authorize]
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