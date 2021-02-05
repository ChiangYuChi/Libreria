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
        private readonly DBRepository _dBRepository;

        public ProductService()
        {
            _dBRepository = new DBRepository();
        }

        public List<ProductViewModel> GetAll()
        {
            return _dBRepository.GetAll<Product>().Select(x => new ProductViewModel()
            {
                Id = x.PartNo,
                Name = x.PartName,
                UnitPrice = 500
            }).ToList();
        }
    }
}