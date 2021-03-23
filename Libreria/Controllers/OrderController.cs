using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECPay.Payment.Integration;
using Libreria.Filters;

namespace Libreria.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingService _shoppingService;
        private readonly MemberService _memberService;
        private readonly FavoriteService _favoriteService;
        private readonly ProductService _productService;

        public OrderController()
        {
            _orderService = new OrderService();
            _shoppingService = new ShoppingService();
            _memberService = new MemberService();
            _favoriteService = new FavoriteService();
            _productService = new ProductService();
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

            return View("Index", result);
        }

        [HttpPost]
        public string AddToCart(ProductViewModel ProductVM)
        {
            OperationResult result = new OperationResult() {
                IsSuccessful = false,
            };

            //查詢庫存
            ProductVM = _productService.GetById(ProductVM.Id);

            if(ProductVM.Count >= 1) //檢查庫存
            {
                result = _shoppingService.AddToCart(ProductVM.Id);
            }
            else //庫存小於1
            {
                return "商品庫存不足！";
            }

            if (result.IsSuccessful)
            {
                return "商品加入成功！";
            }
            else
            {
                return "商品加入失敗！";
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


        //若有錯誤，回傳錯誤訊息
        [HttpPost]
        public string PlusOne(ShoppingCartViewModel ShoppingCartVM)
        {

            //查詢庫存
            ProductViewModel ProductVM = _productService.GetById(ShoppingCartVM.ProductId);

            if (ProductVM.Count >= ShoppingCartVM.Count+1) //檢查庫存
            {
                _shoppingService.AddOne(ShoppingCartVM.ProductId); //商品數量+1
                return "";
            }
            else
            {
                return $"《{ProductVM.Name}》庫存不足";
            }
        }

        [HttpPost]
        public void MinusOne(ShoppingCartViewModel ShoppingCartVM)
        {
            _shoppingService.MinusOne(ShoppingCartVM.ProductId);
        }

        [HttpPost]
        public string Redirect()
        {
            //檢查庫存
            List<ShoppingCartViewModel> shoppingCartVMs = _shoppingService.GetAll();
            foreach(var shoppingCartVM in shoppingCartVMs)
            {
                ProductViewModel ProductVM = _productService.GetById(shoppingCartVM.ProductId);

                if (ProductVM.Count < shoppingCartVM.Count) //檢查庫存
                {
                    return $"《{ProductVM.Name}》庫存不足"; //回傳錯誤訊息
                }
            }

            return _shoppingService.Redirect().ToString();  
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
            //檢查庫存
            List<ShoppingCartViewModel> shoppingCartVMs = _shoppingService.GetAll();
            foreach (var shoppingCartVM in shoppingCartVMs)
            {
                ProductViewModel ProductVM = _productService.GetById(shoppingCartVM.ProductId);

                if (ProductVM.Count < shoppingCartVM.Count) //檢查庫存
                {
                    ViewBag.ErrorMsg = $"《{ProductVM.Name}》庫存不足"; //回傳錯誤訊息
                    return Index(); //回到購物車
                }
            }

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

        [CustomAuthenticationFilter]
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
           _orderService.SetState(orderVM, RtnCode);


 
            ViewData["OrderNum"] = form["MerchantTradeNo"];
            return View();
        }

        public PartialViewResult CartMsgPartial()
        {
            return PartialView();
        }
    }
}