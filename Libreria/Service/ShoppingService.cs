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

       public List<ShoppingCartViewModel> GetAll()
        {
            var result = (from s in _DbRepository.GetAll<ShoppingCart>()
                         join p in _DbRepository.GetAll<Product>()
                         on s.ProductId equals p.ProductId
                         where s.memberId == 1
                         join v in _DbRepository.GetAll<Preview>()
                         on s.ProductId equals v.ProductId
                         where v.Sort == 0
                         select new ShoppingCartViewModel()
                         {
                             ProductId = s.ProductId,
                             ProductName = p.ProductName,
                             Count = s.Count,
                             Price = p.UnitPrice,
                             PicUrl = v.ImgUrl
                         }).ToList();

            return result;
        }

        public OperationResult Create(ProductViewModel ProductVM)
        {
            var result = new OperationResult();

            try
            {
                ShoppingCart entity = new ShoppingCart() { ProductId = ProductVM.Id, memberId = 1, Count = 1 }; //memberID后面需要修改成真实资料
                _DbRepository.Create<ShoppingCart>(entity);
                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult DeleteFromCart(ShoppingCartViewModel ShoppingCartVM)
        {
            var result = new OperationResult();

            try
            {
                _DbRepository.Delete<ShoppingCart>(_DbRepository.GetAll<ShoppingCart>().Where(x => x.ProductId == ShoppingCartVM.ProductId && x.memberId == 1).FirstOrDefault()); //memberID后面需要修改成真实资料
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