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

        public FavoriteViewModel GetById(int id)
        {
            var result = (from p in _DbRepository.GetAll<Product>()
                          join s in _DbRepository.GetAll<Supplier>()                          
                          on p.SupplierId equals s.SupplierId
                          join w in _DbRepository.GetAll<Preview>()
                          on p.ProductId equals w.ProductId
                          where p.ProductId == id
                          select new FavoriteViewModel()
                          {
                              Id = p.ProductId,
                              Name = p.ProductName,
                              Author = p.Author,
                              SupName = s.Name,
                              PublishDate  = p.PublishDate
                          }).FirstOrDefault();

            var PreviewList = _DbRepository.GetAll<Preview>()
                .Where(x => x.ProductId == id)
                .OrderBy(x => x.Sort)
                .ToList();

            foreach (var item in PreviewList)
            {
                if (item.Sort == 0)
                {
                    result.Pic = item.ImgUrl;
                }
               
            }
                return result;
        }
    }
}