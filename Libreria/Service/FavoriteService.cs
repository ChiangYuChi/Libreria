using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class FavoriteService
    {
        private readonly LibreriaRepository _DbRepository;

        public FavoriteService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public OperationResult Create(ProductViewModel ProductVM)
        {
            var result = new OperationResult();

            try
            {
                Favorite entity = new Favorite() { ProductId = ProductVM.Id,memberId = 1 };
                _DbRepository.Create<Favorite>(entity);
                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }
        

        public List<FavoriteViewModel> GetAll()
        {
            var result = (from f in _DbRepository.GetAll<Favorite>()
                          join p in _DbRepository.GetAll<Product>()
                          on f.ProductId equals p.ProductId
                          join s in _DbRepository.GetAll<Supplier>()
                          on p.SupplierId equals s.SupplierId
                          join v in _DbRepository.GetAll<Preview>()
                          on p.ProductId equals v.ProductId
                          where v.Sort == 0
                          select new FavoriteViewModel()
                          {
                              ProductId = p.ProductId,
                              Name = p.ProductName,
                              Author = p.Author,
                              Supplier = s.Name,
                              PublishDate = p.PublishDate,
                              Img = v.ImgUrl
                          }).ToList();

            return result;
        }

        public OperationResult AddToCart(FavoriteViewModel favoriteVM)
        {
            var result = new OperationResult();

            foreach (var item in PreviewList)
            {
                ShoppingCart entity = new ShoppingCart() { ProductId = favoriteVM.ProductId, memberId = 1, Count = 1 }; //memberID后面需要修改成真实资料
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