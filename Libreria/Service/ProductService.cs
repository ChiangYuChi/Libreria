using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Libreria.Service
{
    public class ProductService
    {

        private readonly LibreriaRepository _DbRepository;

        public ProductService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<ProductViewModel> GetAll()
        {
            var result = (from p in _DbRepository.GetAll<Product>()
                         join v in _DbRepository.GetAll<Preview>()
                         on p.ProductId equals v.ProductId
                         where v.Sort == 0
                         join c in _DbRepository.GetAll<Category>()
                         on p.CategoryId equals c.CategoryId
                         join s in _DbRepository.GetAll<Supplier>()
                         on p.SupplierId equals s.SupplierId
                         select new ProductViewModel()
                         {
                             Id = p.ProductId,
                             Name = p.ProductName,
                             UnitPrice = p.UnitPrice,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name,
                             Author = p.Author,
                             Supplier = s.Name,
                             PublishDate = p.PublishDate,
                             CreateTime = p.CreateTime,
                             Introduction = p.Introduction,
                             MainUrl = v.ImgUrl
                         }).ToList();

            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            if (MemberId == 0)
            {
                result.ForEach(x => x.isFav = false);
                return result;
            }
            else
            {
                var fav = _DbRepository.GetAll<Favorite>().Where(x => x.memberId == MemberId).ToList();
                foreach (var item in result)
                {
                    if (fav.Any(x => x.ProductId == item.Id))
                    {
                        item.isFav = true;
                    }
                    else
                    {
                        item.isFav = false;
                    }
                }

                return result;
            }
        }

        public ProductViewModel GetById(int id)
        {
            var result = (from p in _DbRepository.GetAll<Product>()
                          join c in _DbRepository.GetAll<Category>()
                          on p.CategoryId equals c.CategoryId
                          where p.ProductId == id
                          select new ProductViewModel()
                          {
                              Id = p.ProductId,
                              Name = p.ProductName,
                              UnitPrice = p.UnitPrice,
                              CategoryId = p.CategoryId,
                              Author = p.Author,
                              CreateTime = p.CreateTime,
                              Introduction = p.Introduction,
                              CategoryName = c.Name
                          }).FirstOrDefault();

            var PreviewList = _DbRepository.GetAll<Preview>()
                .Where(x => x.ProductId == id)
                .OrderBy(x => x.Sort)
                .ToList();

            var stringlist = new List<string>();

            foreach (var item in PreviewList)
            {
                if (item.Sort == 0)
                {
                    result.MainUrl = item.ImgUrl;
                }
                else
                {
                    stringlist.Add(item.ImgUrl);
                }
            }

            result.PreviewUrls = stringlist;

            return result;
        }

        public List<ProductViewModel> GetByCategory(int CategoryId)
        {
            var result = (from p in _DbRepository.GetAll<Product>()
                          join v in _DbRepository.GetAll<Preview>()
                          on p.ProductId equals v.ProductId
                          where p.CategoryId == CategoryId && v.Sort == 0
                          join c in _DbRepository.GetAll<Category>()
                          on p.CategoryId equals c.CategoryId
                          select new ProductViewModel()
                          {
                              Id = p.ProductId,
                              Name = p.ProductName,
                              UnitPrice = p.UnitPrice,
                              CategoryId = p.CategoryId,
                              Author = p.Author,
                              CreateTime = p.CreateTime,
                              Introduction = p.Introduction,
                              MainUrl = v.ImgUrl,
                              CategoryName = c.Name
                          }).ToList();

            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            if (MemberId == 0)
            {
                result.ForEach(x => x.isFav = false);
            }
            else
            {
                var fav = _DbRepository.GetAll<Favorite>().Where(x => x.memberId == MemberId).ToList();
                foreach (var item in result)
                {
                    if (fav.Any(x => x.ProductId == item.Id))
                    {
                        item.isFav = true;
                    }
                    else
                    {
                        item.isFav = false;
                    }
                }
            }

            return result;
        }

        public List<ProductViewModel> GetByPublishDate()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                           .OrderByDescending(p => p.PublishDate)
                           .Take(4)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                             Id = p.ProductId,
                             Name = p.ProductName,
                             UnitPrice = p.UnitPrice,
                             CategoryId = p.CategoryId,
                             Author = p.Author,
                             CreateTime = p.CreateTime,
                             Introduction = p.Introduction,
                             MainUrl = v.ImgUrl,
                         });
            var result = products.ToList();
            return result;

        }
        /// <summary>
        /// 首頁用，取得依照上市日期由新至舊排序之商品資料
        /// </summary>
        /// <returns>
        /// 回傳為集合
        /// </returns>
        public List<ProductViewModel> GetByPublishDateHome()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                           .OrderByDescending(p => p.PublishDate)
                           .Take(3)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                                MainUrl = v.ImgUrl,
                            });
            var result = products.ToList();
            return result;

        }
        /// <summary>
        /// 首頁用，取得總銷售前N名之商品資料
        /// </summary>
        /// <returns>
        /// 回傳為暢銷商品集合
        /// </returns>
        public List<ProductViewModel> GetByTotalSalesHome()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                           .OrderByDescending(p => p.TotalSales)
                           .Take(3)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                                MainUrl = v.ImgUrl,
                            });
            var result = products.ToList();
            return result;

        }
        public List<ProductViewModel> GetByTotalSales()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                           .OrderByDescending(p => p.TotalSales)
                           .Take(6)
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                            });
            var result = products.ToList();
            return result;
        }

        public List<ProductViewModel> PromoteToday()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                              .OrderByDescending(p => p.CategoryId==6)
                              .Take(2)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                                MainUrl = v.ImgUrl,
                            });
            var result = products.ToList();
            return result;
        }

        public List<ProductViewModel> PromoteEditor()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                                          .OrderByDescending(p => p.CategoryId == 5)
                                          .OrderBy(p=>p.UnitPrice)
                                          .Take(4)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice ,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                                MainUrl = v.ImgUrl,
                            });
            var result = products.ToList();
            return result;
        }


        public List<ProductViewModel> PromoteMajor()
        {

            var products = (from p in _DbRepository.GetAll<Product>()
                              .OrderBy(p => p.Inventory)
                              .Skip(2)
                              .Take(2)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
                            where v.Sort == 0
                            select new ProductViewModel()
                            {
                                Id = p.ProductId,
                                Name = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryId = p.CategoryId,
                                Author = p.Author,
                                CreateTime = p.CreateTime,
                                Introduction = p.Introduction,
                                MainUrl = v.ImgUrl,
                            });
            var result = products.ToList();
            return result;
        }
        
    }
}