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


        public OperationResult Create(ProductViewModel ProductVM)
        {
            var result = new OperationResult();

            try
            {
                Favorite entity = new Favorite() { ProductId = ProductVM.Id, memberId = 1 };
                _DbRepository.Create<Favorite>(entity);
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



    }
}