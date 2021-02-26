using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;
using System.Web.Routing;


namespace Libreria.Filters
{
    public class CustomAuthenticationFilter:ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["MemberName"]))&&filterContext.HttpContext.Request.Cookies["MemberName"]!=null)
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["MemberName"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                //filterContext.HttpContext.Session["MemberName"] = filterContext.HttpContext.Request.Cookies["MemberName"].Value;
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller","MemberLogin" },
                        {"action","Index"}
                    }
                    );
                
                //string controllerName = filterContext.RouteData.Values["controller"].ToString();
                //string actionName = filterContext.RouteData.Values["action"].ToString();
                //var a = HttpContext.Current.Session["MemberID"]; 測試用
            }

        }

        //ovverride CustomAuthenticationFilter to implement AllowAnonymous
        public class CustomAllowAnonymous : FilterAttribute, IOverrideFilter
        {
            public Type FiltersToOverride
            {
                get
                {
                    return typeof(IAuthenticationFilter);
                }

            }
        }

    }

}