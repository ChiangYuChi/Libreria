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


    }
}