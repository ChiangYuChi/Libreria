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
        public List<FavoriteViewModel> GetAll()
        {
            return _DbRepository.GetAll<Favorite>().Select(x => new FavoriteViewModel()
            {
                FavoriteId = x.FavoriteId.ToString(),
                ProductId = x.ProductId,
                memberId = x.memberId,
            }).ToList();
        }

    }
}