using Libreria.Repository;
using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.ViewModels;

namespace Libreria.Service
{
   public class CategoryService
    {
        private readonly LibreriaRepository _DbRepository;

        public CategoryService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<CategoryViewModel> GetAll()
        {
            return _DbRepository.GetAll<Category>().Select(x => new CategoryViewModel()
            {
                Name = x.Name,
                CategoryId = x.CategoryId,
                Sort = x.Sort
            }).ToList();
        }
    }
}