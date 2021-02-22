using Libreria.Filters;
using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.Service;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using static Libreria.Filters.CustomAuthenticationFilter;

namespace Libreria.Controllers
{
    [CustomAuthenticationFilter]
    public class MemberCenterController : Controller
    {
        private readonly FavoriteService _favoriteService;
        private readonly LibreriaDataModel _libreriaDataModel;
        


        public MemberCenterController()
        {
            _favoriteService = new FavoriteService();
            _libreriaDataModel = new LibreriaDataModel();
        }


        // GET: MemberCenter

        public ActionResult MemberLogin()
        {
            return View();
        }

        public ActionResult MemberInfo()
        {
            return View();
        }
        public ActionResult ChangePassward()
        {
            return View();
        }

        
        public ActionResult MemberOrderInquery()
        {
            return View();
        }

        public ActionResult MemberPasswordConfirm()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return View();
        }
        [CustomAllowAnonymous]
        public ActionResult Subscribe()
        {
            return View();
        }

        public ActionResult Subscribe_info()
        {
            return View();
        }

        public ActionResult Subscribe_cancel()
        {
            return View();
        }
        public ActionResult MemberContact()
        {
            return View();
        }
        [HttpGet]
        [CustomAllowAnonymous]
        public ActionResult MemberRegisterPage()
        {
            return View();
        }        
        [HttpPost]
        [CustomAllowAnonymous]
        public ActionResult MemberRegisterPage(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {             

                member member = new member
                {
                    memberName = HttpUtility.HtmlEncode(model.memberName),
                    MobileNumber = HttpUtility.HtmlEncode(model.MobileNumber),
                    HomeNumber = HttpUtility.HtmlEncode(model.HomeNumber),
                    Address = HttpUtility.HtmlEncode(model.Address),
                    Email = HttpUtility.HtmlEncode(model.Email),
                    memberUserName = HttpUtility.HtmlEncode(model.memberUserName),
                    memberPassword = Utility.GetSha512(HttpUtility.HtmlEncode(model.memberPassword)),
                    birthday = DateTime.Parse(HttpUtility.HtmlEncode(model.birthday)),
                    Gender = int.Parse(HttpUtility.HtmlEncode(model.Gender)),
                    IDnumber = HttpUtility.HtmlEncode(model.IDnumber)
                };
                 //var a = member = _libreriaDataModel.members
                 //                  .Where(u => u.memberName == model.memberName)
                 //                  .FirstOrDefault();

                //EF
                //if (a == null)
                //{
                    try
                    {
                        _libreriaDataModel.members.Add(member);
                        _libreriaDataModel.SaveChanges();
                        return Redirect("MemberLogin");
                    }
                    //下列部分需要再處裡
                    catch (Exception ex)
                    {
                        return Content("新增帳號失敗:" + ex.ToString());
                    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "*帳號已被使用");
                //    return View(model);
                //}
                
            }
            return View();
        }
        [CustomAllowAnonymous]
        public ActionResult Favorite()
        {
            
            return View();
        }



        [HttpPost]
        public string AddToFavorite(ProductViewModel productVM)
        {
            var CanTakeMemberNameFromThisVariable = System.Web.HttpContext.Current.Session["MemberName"];
            var result = _favoriteService.Create(productVM);

            if (result.IsSuccessful)
            {
                return "加入成功!";
            }
            else
            {
                return "加入失败";
            }
        }
    }
}