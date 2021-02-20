using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class ShoppingService
    {
        private readonly LibreriaRepository _DbRepository;

        public ShoppingService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public OperationResult Create(ProductViewModel ProductVM)
        {
            var result = new OperationResult();

            try
            {
                ShoppingCart entity = new ShoppingCart() { ProductId = ProductVM.Id, memberId = 1 }; //memberID后面需要修改成真实资料
                _DbRepository.Create<ShoppingCart>(entity);
                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }
    }
}