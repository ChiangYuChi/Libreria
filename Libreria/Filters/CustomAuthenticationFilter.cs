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
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["MemberName"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterConxt)
        {
            if(filterConxt.Result == null || filterConxt.Result is HttpUnauthorizedResult)
            {
                filterConxt.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller","MemberLogin" },
                        {"action","Index"}
                    }
                    );
            }
        }
    
    }

}