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

            return View();
        }

        [HttpPost]
        public string AddToCart(ProductViewModel ProductVM)
        {
            var result = _shoppingService.Create(ProductVM);

            return "Success";
        }

        /// <summary>
        /// 購物車明細
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetail()
        {
            return View();
        }

        public ActionResult Test()
        {
            var result = _orderService.GetAll();
            return View(result);
        }
    }
}