using Libreria.Service;
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

        public OrderController()
        {
            _orderService = new OrderService();
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 購物車明細
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetail()
        {
            return View();
        }
    }
}