//using Libreria.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http.ExceptionHandling;
//using System.Web.Http.Filters;
//using System.Web.Http.Results;
//using System.Web.Routing;

//namespace Libreria.Filters
//{
//    public class ExceptionAttribute : ExceptionFilterAttribute
//    {
//        public void OnException(ExceptionContext filterContext)
//        {
//            //if (!filterContext.Exception && filterContext.Exception is NullReferenceException)
//            //{
//            //    filterContext.Result = new RedirectResult("customErrorPage.html");
//            //    filterContext.ExceptionHandled = true;
//            //}
//            var result = new ResultModel
//            {
//                isException = true,
//                ExceptionMsg = filterContext.Exception.Message

//            };
//            filterContext.Response = filterContext.Request.CreateResponse(result);

//        }    
//}