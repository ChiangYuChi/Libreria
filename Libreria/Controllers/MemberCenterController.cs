using Libreria.Filters;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Libreria.Filters.CustomAuthenticationFilter;

namespace Libreria.Controllers
{
    [CustomAuthenticationFilter]
    public class MemberCenterController : Controller
    {
        private readonly FavoriteService _favoriteService;

        public MemberCenterController()
        {
            _favoriteService = new FavoriteService();
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
        [CustomAllowAnonymous]
        public ActionResult MemberRegisterPage()
        {
            return View();
        }
        [CustomAllowAnonymous]
        public ActionResult Favorite()
        {
            
            return View();
        }



        [HttpPost]
        public string AddToFavorite(ProductViewModel productVM)
        {
            var CanTakeMemberNameFromThisVariable = System.Web.HttpContext.Current.Session["MemberID"];
            var result = _favoriteService.Create(productVM);

            if (result.IsSuccessful)
            {
                return "加入成功!";
            }
            else
            {
                return "加入失败";
            }
        }
    }
}