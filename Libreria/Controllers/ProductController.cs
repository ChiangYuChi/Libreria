using Libreria.Filters;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
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
        private readonly FavoriteService _favoriteService;
        private readonly ShoppingService _shoppingService;



        public ProductController()
        {
            _productService = new ProductService();
            _shoppingService = new ShoppingService();
            _favoriteService = new FavoriteService();
        }

       


        public ActionResult Index()
        {
            return View();   
        }



        public ActionResult ProductCategory(int? CategoryId, int? Order)
        {
            List<ProductViewModel> result;
            ViewBag.shoppincart = _shoppingService.GetAnonymousAll();
            ViewBag.shoppingCart = _shoppingService.GetAll();

            if (CategoryId != null)
            {
                result = _productService.GetByCategory(Convert.ToInt32(CategoryId));
                ViewBag.CategoryId = CategoryId;
            }
            else
            {
                result = _productService.GetAll();
            }

            if (Order == 1)
            {
                result = result.OrderBy(x => x.UnitPrice).ToList();
            }
            else if (Order == 2)
            {
                result = result.OrderBy(x => x.CreateTime).ToList();
            }

            return View(result);

        }

        public ActionResult ProductDetail(int id)
        {
            var product = _productService.GetById(id);   

            return View(product);
        }

        public PartialViewResult PromoteByPublishDatePartial()
        {
            var product = _productService.GetByPublishDate();
            return PartialView(product);
        }

        public PartialViewResult PromoteByTotalSalesPartial()
        {
            var product = _productService.GetByTotalSales();
            return PartialView(product);
        }

        public PartialViewResult PromoteTodayPartial()
        {
            var product = _productService.PromoteToday();
            return PartialView(product);
        }

        public PartialViewResult PromoteByEditor()
        {
            var product = _productService.PromoteEditor();
            return PartialView(product);
        }
        public PartialViewResult PromoteMajor()
        {
            var product = _productService.PromoteMajor();
            return PartialView(product);
        }
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]


        /// <summary>
        /// 於商品分類業的購物車icon，購物車功能，將GetAnonymousAll()之方法所取得之senssion資料以json格式回傳
        /// </summary>
        /// <returns>
        /// 回傳為senssion轉換為json格式之資料。
        /// </returns>
        public ActionResult GetToCartPartial()
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

            return Json(result);

            
        }
    }
}