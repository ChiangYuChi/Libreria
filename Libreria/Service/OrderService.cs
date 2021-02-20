using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class OrderService
    {
        private readonly LibreriaRepository _DbRepository;

        public OrderService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<OrderViewModel> GetAll()
        {
            return _DbRepository.GetAll<Product>().Select(x => new OrderViewModel()
            {
                Id = x.ProductId,
                Name = x.ProductName
            }).ToList();
        }

    }
}