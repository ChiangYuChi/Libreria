using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Libreria.Service
{
    public class PreviewService
    {

        private readonly LibreriaRepository _DbRepository;

        public PreviewService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<PreviewViewModel> GetAll()
        {
            return _DbRepository.GetAll<Preview>().Select(x => new PreviewViewModel()
            {
                ProductId = x.ProductId,
                ImgUrl = x.ImgUrl,
                Sort=x.Sort
            }).ToList();
        }


    }
}