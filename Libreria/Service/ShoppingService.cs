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
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);

            var result = (from s in _DbRepository.GetAll<ShoppingCart>()
                         join p in _DbRepository.GetAll<Product>()
                         on s.ProductId equals p.ProductId
                         where s.memberId == MemberId
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
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (_DbRepository.GetAll<ShoppingCart>().Where(x => x.memberId == MemberId && x.ProductId == ProductVM.Id).FirstOrDefault() == null)
                {
                    ShoppingCart entity = new ShoppingCart() { ProductId = ProductVM.Id, memberId = MemberId, Count = 1 };
                    _DbRepository.Create<ShoppingCart>(entity);
                }

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
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                _DbRepository.Delete<ShoppingCart>(_DbRepository.GetAll<ShoppingCart>().Where(x => x.ProductId == ShoppingCartVM.ProductId && x.memberId == MemberId).FirstOrDefault());
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