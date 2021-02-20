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
                         select new ProductViewModel()
                         {
                             Id = p.ProductId,
                             Name = p.ProductName,
                             UnitPrice = p.UnitPrice,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name,
                             Author = p.Author,
                             CreateTime = p.CreateTime,
                             Introduction = p.Introduction,
                             MainUrl = v.ImgUrl
                         }).ToList();

            return result;
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
                          .Where(p => p.CategoryId == 7)
                           .OrderBy(p => p.PublishDate)
                           .Take(2)
                           join v in _DbRepository.GetAll<Preview>()
                           on p.ProductId equals v.ProductId
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
        public List<ProductViewModel> PromoteTodayHome()
        {
            var products = (from p in _DbRepository.GetAll<Product>()
                          .Where(p => p.CategoryId == 7)
                           .OrderBy(p => p.PublishDate)
                           .Take(5)
                            join v in _DbRepository.GetAll<Preview>()
                            on p.ProductId equals v.ProductId
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
    }
}