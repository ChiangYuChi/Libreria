//using Libreria.Models.EntityModel;
//using Libreria.Repository;
//using Libreria.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http;

//namespace Libreria.Controllers
//{
//    public static class SessionVariables
//    {
//        private static object GetValue(string AttrName)
//        {
//            return HttpContext.Current.Session[AttrName];
//        }
//        private static void SetValue(string AttrName, object AttrValue)
//        {
//            HttpContext.Current.Session[AttrName] = AttrValue;

//        }

//        public static int PageIndex
//        {
//            get => (int?)GetValue("pageIndex") ?? 0;
//            set => SetValue("pageIndex", value);
//        }
//    }
//    //    public class MemberApiController : ApiController
//    //    {
//    //        static LibreriaDataModel context = null;
//    //        //static void Main(string[] args)
//    //        //{
//    //        //    try
//    //        //    {
//    //        //        var test = GetMember();
//    //        //    }
//    //        //    finally
//    //        //    {
//    //        //        context.Dispose();
//    //        //    }
//    //        //}
//    //        public IQueryable<MemberLoginViewModel> GetMember(MemberLoginViewModel model)
//    //        {

//    //            context = new MemberLoginViewModel();



//    //            //if (ModelState.IsValid)
//    //            //{
//    //            //    member member = context.members
//    //            //                                   .Where(u => u.memberName == model.MemberName && u.memberPassword == model.MemberPassword)
//    //            //                                   .FirstOrDefault();
//    //            //    return member;
//    //            //}
//    //            //else
//    //            //{

//    //            //}

//    //        }
//    //        internal class InternalLibreriaDataModel : LibreriaDataModel
//    //        {
//    //            public virtual DbSet<member> MembersforWebApi { get; set; }
//    //        }
//    //        internal class MemberLoginViewModel : InternalLibreriaDataModel
//    //        {
//    //            public string MemberName { get; set; }

//    //            public string MemberPassword { get; set; }
//    //        }
//    //    }
//}
