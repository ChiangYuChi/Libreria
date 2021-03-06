using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECPay.Payment.Integration;

namespace Libreria.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingService _shoppingService;
        private readonly MemberService _memberService;
        private readonly FavoriteService _favoriteService;

        public OrderController()
        {
            _orderService = new OrderService();
            _shoppingService = new ShoppingService();
            _memberService = new MemberService();
            _favoriteService = new FavoriteService();
        }

        /// <summary>
        /// 购物车首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<ShoppingCartViewModel> result;
            if (System.Web.HttpContext.Current.Session["MemberID"] == null)
            {
                result = _shoppingService.GetAnonymousAll();
            }
            else
            {
                result = _shoppingService.GetAll();
            }

            return View(result);
        }

        [HttpPost]
        public string AddToCart(ProductViewModel ProductVM)
        {
            var result = _shoppingService.AddToCart(ProductVM.Id);


            if (result.IsSuccessful)
            {
                return "加入成功!";
            }
            else
            {
                return "加入失败";
            }
        }

        [HttpPost]
        public string FavoriteToCart(ProductViewModel ProductVM)
        {
            var result = _favoriteService.CreateToCart(ProductVM);


            if (result.IsSuccessful)
            {
                return "加入成功!";
            }
            else
            {
                return "加入失败";
            }
        }

        [HttpPost]
        public void DeleteFromCart(ShoppingCartViewModel ShoppingCartVM)
        {
            _shoppingService.DeleteFromCart(ShoppingCartVM.ProductId);

        }


        [HttpPost]
        public void PlusOne(ShoppingCartViewModel ShoppingCartVM)
        {
            _shoppingService.AddOne(ShoppingCartVM.ProductId);
        }

        [HttpPost]
        public void MinusOne(ShoppingCartViewModel ShoppingCartVM)
        {
            _shoppingService.MinusOne(ShoppingCartVM.ProductId);
        }

        [HttpPost]
        public int Redirect()
        {
            return _shoppingService.Redirect();  
        }

        /// <summary>
        /// 購物車明細
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetail()
        {
            int UserMemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            var result = _shoppingService.GetAll();
            ViewBag.MemberVMList = _memberService.GetByMemberId(UserMemberId);

            return View(result);
        }

        [HttpPost]
        public ActionResult Checkout(OrderViewModel orderVM)
        {
            //取得購物車並放入訂單
            orderVM = _orderService.PutShoppingCartsToOrderVM(orderVM);

            //訂單存進資料庫
            if (orderVM != null && orderVM.OrderDetailList.Any())
            {
                OperationResult result = _orderService.Create(orderVM);

                if (result.IsSuccessful)
                {
                    orderVM = _orderService.GetByOrderId(orderVM.OrderId).FirstOrDefault();
                }
                else
                {

                }
            }

            return PayOrder(orderVM);
        }

        public ActionResult PayOrder(OrderViewModel orderVM)
        {
            var result = _orderService.ECPay(orderVM);
            ViewBag.Html = result;
            return View("PayOrder");

        }

        /// <summary>
        /// 取得付款回傳結果並放入訂單欄位
        /// <returns></returns>

        public ActionResult PayReturnDetail(int orderId, FormCollection form)
        {
   
            var RtnCode = form["RtnCode"];
            List<OrderViewModel> orderVMList = _orderService.GetByOrderId(orderId);
            OrderViewModel orderVM = orderVMList.FirstOrDefault();

            ViewData["OrderNum"] = form["MerchantTradeNo"];
            return View();
        }

        public PartialViewResult CartMsgPartial()
        {
            return PartialView();
        }
    }
}