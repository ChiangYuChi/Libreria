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


        public ProductController()
        {

        }

        /// <summary>
        /// 商品列表頁
        /// </summary>
        /// <returns></returns>
      
        


        public ActionResult ProductIndex()
        {
            return View();
        }
    }
}