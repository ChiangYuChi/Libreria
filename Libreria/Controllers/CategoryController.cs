using Libreria.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class CategoryController : Controller
    {

        private readonly CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        // GET: Category
       
        public PartialViewResult CategoryPartial()
        {
            var result = _categoryService.GetAll();
            return PartialView(result);
        }

    }
}