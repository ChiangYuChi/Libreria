using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Libreria.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var memberId = Convert.ToInt32(httpContext.Session["MemberId"]);
            if(!string.IsNullOrEmpty(memberId.ToString()))
                using(var context = new LibreriaDataModel())
                {
                    var memberRole =
                    (
                        from m in context.members
                        join r in context.Roles on m.RoleId equals r.RoleID
                        where m.memberId == memberId
                        select new
                        {
                            r.RoleName

                        }).FirstOrDefault();
                    foreach(var role in allowedroles)
                    {
                        if (role == memberRole.RoleName) return true;
                    }
                    
                }
            return authorize; 
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"controller","Home"},
                    {"action","UnAuthorized"  }
                }

             );
            
        }


    }

}