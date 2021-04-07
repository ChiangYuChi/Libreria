using Libreria.Filters;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using MvcSiteMapProvider;
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

        public ActionResult FindCategory(string CategoryName)
        {
         
            if (string.IsNullOrEmpty(CategoryName))
            {
                return Content("請提供分類名稱");
            }
                var allProduct = _productService.GetAll();
                List<ProductViewModel> booksByCategory = (from p in allProduct
                                   where p.CategoryName== CategoryName
                                   select p).ToList();
            if (booksByCategory.Count == 0)
            {
                return Content("找不到此類型的車");
            }
            return View(booksByCategory);
         }


        public ActionResult Find(string CategoryName)
        {
            var allProduct = _productService.GetAll();
            var ProductInCategory = (from p in allProduct
                                     where p.CategoryName == CategoryName
                                     select p).ToList();
            return View(ProductInCategory);
        }

        public ActionResult ProductCategory(int? CategoryId, int? Order, int NowPage = 1, string search="")
        {
            string CategoryName = "";
            if (CategoryId == 1)
            {
                CategoryName = "人文社科";
            }
            else if (CategoryId == 2)
            {
                CategoryName = "旅遊";
            }
            else if (CategoryId == 3)
            {
                CategoryName = "童書";
            }
            else if (CategoryId == 4)
            {
                CategoryName = "電腦";
            }
            else if (CategoryId == 5)
            {
                CategoryName = "自然科普";
            }
            else if (CategoryId == 6)
            {
                CategoryName = "文學小說";
            }
            else if (CategoryId == 7)
            {
                CategoryName = "商業";
            }



            SiteMaps.Current.CurrentNode.Title = CategoryName;
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
            ViewBag.Order = Order;

            //搜尋
            result = result.Where(product => product.Name.Contains(search)).ToList();




            //商品總數
            int totalAmount = result.Count;
            ViewBag.TotalAmount = totalAmount;

            //分頁
            int perPageAmount = 8;
            int totalPage = (int)Math.Ceiling((double)totalAmount / perPageAmount);
            result = result.Skip((NowPage - 1) * perPageAmount).Take(perPageAmount).ToList();
            ViewBag.NowPage = NowPage;
            ViewBag.TotalPage = totalPage;

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

        /// <summary>
        /// 於商品分類業的購物車icon，購物車功能，將GetAnonymousAll()之方法所取得之senssion資料以json格式回傳
        /// </summary>
        /// <returns>
        /// 回傳為senssion轉換為json格式之資料。
        /// </returns>

        [HttpPost]
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
        public PartialViewResult CartMsgPartial()
        {
            return PartialView();
        }

        public PartialViewResult FavMsgPartial()
        {
            return PartialView();
        }
    }
}