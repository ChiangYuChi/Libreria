using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class MemberLoginService
    {
        public readonly LibreriaRepository _libreriaRepository;

        public MemberLoginService()
        {
            _libreriaRepository = new LibreriaRepository();
        }

        public List<MemberLoginViewModel> GetAll()
        {
            return _libreriaRepository.GetAll<member>().Select(m => new MemberLoginViewModel()
            {
                MemberName = m.memberName,
                MemberPassword = m.memberPassword

            }).ToList();
        }
        //public MemberLoginViewModel GetMember(MemberLoginViewModel model)
        //{
        //    string passwordSha512 = Utility.GetSha512(model.MemberPassword);

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var result = _libreriaRepository.GetAll<member>().Where(u => u.memberName == model.MemberName && u.memberPassword == passwordSha512)
        //                                       .FirstOrDefault();
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }


        //}
    }


}