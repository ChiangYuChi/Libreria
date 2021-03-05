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
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Forms;
using Xamarin.Forms;

namespace Libreria.Controllers
{

    [CustomAuthenticationFilter]
    public class MemberCenterController : Controller
    {
        private readonly FavoriteService _favoriteService;
        private readonly MemberRegisterPageService _memberRegisterPageService;
        private readonly OrderService _orderService;
        private readonly MemberService _memberService;


        public MemberCenterController()
        {
            _orderService = new OrderService();
            _favoriteService = new FavoriteService();
            _memberRegisterPageService = new MemberRegisterPageService();
            _memberService = new MemberService();
        }
        
        public ActionResult MemberLogin()
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            List<OrderViewModel> OrderVMList = _orderService.GetBymemberId(UserMemberId);
            ViewBag.OrderVMList = OrderVMList;

            return View();
        }
      
      

    // GET: MemberCenter
        [HttpGet]

        public ActionResult MemberInfo()
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            ViewBag.member = _memberService.GetByMemberId(UserMemberId);
            return View();
        }
        [HttpPost]

        public ActionResult MemberInfo(MemberViewModel member)
        {                    

            var result = _memberService.UpdateMember(member);

            if (result.IsSuccessful)
            {
                return Redirect("MemberLogin");

            }
            else
            {
                ModelState.AddModelError("", "修改失敗");
                return Redirect("MemberLogin");
            }

        }

            public ActionResult ChangePassward()
        {
            return View();
        }

        
        public ActionResult MemberOrderInquery(string Inquire, int? TransactionId)
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            List<OrderViewModel> result = new List<OrderViewModel>();
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

            result.Reverse();

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
            var result =_memberRegisterPageService.CreateMember(model, ModelState.IsValid);

            if (result.IsSuccessful)
            {
               return Redirect("MemberLogin");                  
               
            }
            else
            {
                ModelState.AddModelError("memberName", "帳號已存在。");
                ModelState.AddModelError("", "帳號已存在。");
                return View();
            }
        }   
       


        public ActionResult Favorite()
        {

            var result = _favoriteService.GetAll();
            return View(result);
        }

        [HttpPost]
        public void AddToFavorite(ProductViewModel ProductVM)
        {
            
            _favoriteService.CreateToFavorite(ProductVM);
 
        }

        [HttpPost]
        public string CartToFavorite(ShoppingCartViewModel shoppingCartVM)
        {

            var result = _favoriteService.DeleteCartToFavorite(shoppingCartVM);


            if (result.IsSuccessful)
            {
                return "加入成功";
            }
            else
            {
                return "加入失败";
            }
        }


        [HttpPost]
        public void DeleteFavorite(FavoriteViewModel favoriteVM)
        {
            _favoriteService.DeleteFromFavorite(favoriteVM);

        }

        [HttpPost]
        public void DeleteFromFavorite(FavoriteViewModel favoriteVM)
        {
            _favoriteService.DeleteFromFavorite(favoriteVM);

        }



        public ActionResult ContactUs()
        {
            return View();
        }



        public ActionResult MemberBenefits()
        {
            return View();
        }
       
        

        
       
        
    }
}