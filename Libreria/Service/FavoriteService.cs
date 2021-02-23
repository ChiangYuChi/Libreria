﻿using Libreria.Models.EntityModel;
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
        public List<FavoriteViewModel> GetFavoriteInfo(IEnumerable<int> products)
        {
            var result = (
                          from p in _DbRepository.GetAll<Product>()
                          join s in _DbRepository.GetAll<Supplier>() on p.SupplierId equals s.SupplierId
                          join v in _DbRepository.GetAll<Preview>() on p.ProductId equals v.ProductId
                          where v.Sort == 0 && products.Contains(p.ProductId)
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


        public OperationResult Create(ProductViewModel ProductVM)
        {
            var result = new OperationResult();

            try
            {
                if (_DbRepository.GetAll<Favorite>().Where(x => x.memberId == 1 && x.ProductId == ProductVM.Id).FirstOrDefault() == null)
                {
                    Favorite entity = new Favorite() { ProductId = ProductVM.Id, memberId = 1 };
                    _DbRepository.Create<Favorite>(entity);
                }
                    

                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult DeleteFromFavorite(FavoriteViewModel favoriteVM)
        {
            var result = new OperationResult();

            try
            {
                _DbRepository.Delete<Favorite>(_DbRepository.GetAll<Favorite>().Where(x => x.ProductId == favoriteVM.ProductId && x.memberId == 1).FirstOrDefault()); //memberID后面需要修改成真实资料
                result.IsSuccessful = true;
            }
            catch
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public OperationResult CreateToCart(FavoriteViewModel favoriteVM)
        {
            var result = new OperationResult();

            try
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