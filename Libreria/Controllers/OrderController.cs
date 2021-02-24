using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingService _shoppingService;

        public OrderController()
        {
            _orderService = new OrderService();
            _shoppingService = new ShoppingService();
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
            var result = _shoppingService.AddToCart(ProductVM);
            

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
            _shoppingService.DeleteFromCart(ShoppingCartVM);

        }

        /// <summary>
        /// 購物車明細
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderDetail(OrderViewModel orderVM)
        {
            if (orderVM != null)
            {
                OperationResult result = _orderService.Create(orderVM);

                if(result.IsSuccessful)
                {

                }
                else
                {

                }
            }

            return View(orderVM);
        }

        public ActionResult Test()
        {
            var result = _orderService.GetAll();
            return View(result);
        }
    }
}