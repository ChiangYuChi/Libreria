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


        public List<FavoriteViewModel> GetAll()
        {
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            var result = (from f in _DbRepository.GetAll<Favorite>()
                          join p in _DbRepository.GetAll<Product>() on f.ProductId equals p.ProductId
                          join s in _DbRepository.GetAll<Supplier>() on p.SupplierId equals s.SupplierId
                          join v in _DbRepository.GetAll<Preview>() on p.ProductId equals v.ProductId
                          where v.Sort == 0 && f.memberId == MemberId
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

        public List<Favorite> GetFavorite()
        {
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            var result = _DbRepository.GetAll<Favorite>().ToList();
            return result;
        }

        public OperationResult CreateToFavorite(ProductViewModel ProductVM)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (_DbRepository.GetAll<Favorite>().Where(x => x.memberId == MemberId && x.ProductId == ProductVM.Id).FirstOrDefault() == null)
                {
                    Favorite entity = new Favorite() { ProductId = ProductVM.Id, memberId = MemberId };
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

        public OperationResult DeleteCartToFavorite(ShoppingCartViewModel shoppingCartVM)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (_DbRepository.GetAll<Favorite>().Where(x => x.memberId == MemberId && x.ProductId == shoppingCartVM.ProductId).FirstOrDefault() == null)
                {
                    Favorite entity = new Favorite() { ProductId = shoppingCartVM.ProductId, memberId = MemberId };
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

        public OperationResult CreateToCart(ProductViewModel ProductVM)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                if (_DbRepository.GetAll<ShoppingCart>().Where(x => x.memberId == MemberId && x.ProductId == ProductVM.Id).FirstOrDefault() == null)
                {
                    ShoppingCart entity = new ShoppingCart() { ProductId = ProductVM.Id, memberId = MemberId,Count = 1, };
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


        public OperationResult DeleteFromFavorite(FavoriteViewModel favoriteVM)
        {
            var result = new OperationResult();
            var MemberId = Convert.ToInt32(System.Web.HttpContext.Current.Session["MemberID"]);
            try
            {
                _DbRepository.Delete<Favorite>(_DbRepository.GetAll<Favorite>().Where(x => x.ProductId == favoriteVM.ProductId && x.memberId == MemberId).FirstOrDefault()); 
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