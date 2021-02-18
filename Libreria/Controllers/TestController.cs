using Libreria.Service;
using Libreria.ViewModels;
using Libreria.ViewModels.apibase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Libreria.Controllers
{
    public class TestController : ApiController
    {
        private readonly ProductService _productService;

        public TestController(){
            _productService = new ProductService();
            }

        [HttpGet]
        public ApiResult GetAll()
        {

            try
            {
                throw new Exception();
                var result = _productService.GetAll();
                return new ApiResult(ApiStatus.Success, string.Empty, result);
            }
            catch(Exception ex)
            {
                return new ApiResult(ApiStatus.Fail, ex.Message, null); ;
            }
            
        }
    }
}
