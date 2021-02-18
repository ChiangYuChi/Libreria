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
            return _DbRepository.GetAll<Product>().Select(x => new ProductViewModel()
            {
                Id = x.ProductId,
                Name = x.ProductName,
                UnitPrice = x.UnitPrice,
                Author = x.Author,
                CreateTime = x.CreateTime,
                CategoryId = x.CategoryId,
                Introduction = x.Introduction
            }).ToList();
        }

        public ProductViewModel GetById(int id)
        {
            var result = _DbRepository.GetAll<Product>()
                .Where(x => x.ProductId == id)
                .Select(x => new ProductViewModel()
                {
                    Id = x.ProductId,
                    Name = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    CategoryId = x.CategoryId,
                    Author = x.Author,
                    CreateTime = x.CreateTime,
                    Introduction = x.Introduction
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
            var joindb = from p in _DbRepository.GetAll<Product>()
                         join v in _DbRepository.GetAll<Preview>()
                         on p.ProductId equals v.ProductId
                         where p.CategoryId == CategoryId && v.Sort == 0
                         select new ProductViewModel()
                         {
                             Id = p.ProductId,
                             Name = p.ProductName,
                             UnitPrice = p.UnitPrice,
                             CategoryId = p.CategoryId,
                             Author = p.Author,
                             CreateTime = p.CreateTime,
                             Introduction = p.Introduction,
                             MainUrl = v.ImgUrl
                         };
            var result = joindb.ToList();

            return result;
        }
    }
}