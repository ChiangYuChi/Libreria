using Libreria.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        /// <summary>
        /// 商品列表頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var result = _productService.GetAll();
            return View(result);
        }
    }
}