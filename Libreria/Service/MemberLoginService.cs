using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ubiety.Dns.Core;

namespace Libreria.Service
{
    public class MemberLoginService
    {
        public readonly LibreriaRepository _libreriaRepository;

        public MemberLoginService()
        {
            _libreriaRepository = new LibreriaRepository();
        }

        public member GetMember(MemberLoginViewModel model, bool IsValid)
        {
            string passwordSha512 = Utility.GetSha512(model.MemberPassword);
            member member=null;
            try
            {
                if (IsValid)
                {
                    member = _libreriaRepository.GetAll<member>().Where(u => u.memberName == model.MemberName && u.memberPassword == passwordSha512)
                                               .FirstOrDefault();
                    if (member != null)
                    {
                        HttpContext.Current.Session["MemberName"] = member.memberName;
                        HttpContext.Current.Session["MemberPassword"] = member.memberPassword;
                        HttpContext.Current.Session["MemberID"] = member.memberId;

                        if (model.Remember)
                        {
                            HttpCookie cookie = new HttpCookie("MemberName");

                            cookie["MemberName"] = (HttpContext.Current.Session["MemberName"]).ToString();

                            cookie.Expires = DateTime.Now.AddDays(7);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                        
                        //var CookiesessionID = HttpContext.Request.Cookies["SesssionID"];

                     
                        return member;
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return member;
        }
    }


}