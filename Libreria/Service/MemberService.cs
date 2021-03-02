using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class MemberService
    {
        private readonly LibreriaRepository _DbRepository;

        public MemberService()
        {
            _DbRepository = new LibreriaRepository();
        }

        public List<MemberViewModel> GetAll()
        {
            var result = (
                from member in _DbRepository.GetAll<member>()
                select new MemberViewModel()
                {
                    memberUserName = member.memberUserName,
                    MobileNumber = member.MobileNumber,
                    HomeNumber = member.HomeNumber,
                    Address = member.Address,
                    Email = member.Email,
                    memberName = member.memberName,
                    memberPassword = member.memberPassword,
                    birthday = member.birthday,
                    Gender = member.Gender,
                    IDnumber = member.IDnumber,
                }
            ).ToList();

            foreach (var memberVM in result)
            {
                
            }

            return result;
        }

        public List<MemberViewModel> GetByMemberId(int memberId)
        {
            var result = (
                from member in _DbRepository.GetAll<member>()
                where member.memberId == memberId
                select new MemberViewModel()
                {
                    memberUserName = member.memberUserName,
                    MobileNumber = member.MobileNumber,
                    HomeNumber = member.HomeNumber,
                    City = member.City,
                    Region = member.Region,
                    Address = member.Address,
                    Email = member.Email,
                    memberName = member.memberName,
                    memberPassword = member.memberPassword,
                    birthday = member.birthday,
                    Gender = member.Gender,
                    IDnumber = member.IDnumber,
                }
            ).ToList();

            foreach (var memberVM in result)
            {

            }

            return result;
        }

        //update MemberInfo
        public OperationResult UpdateMember(MemberViewModel model,bool isValid)
        {
            var result = new OperationResult();
            member updateMember = null;
            var originalMember = _DbRepository.GetAll<member>().Where(m => m.memberName == model.memberName).FirstOrDefault();

            if (isValid)
            {

                updateMember = new member
                {
                    MobileNumber = HttpUtility.HtmlEncode(model.MobileNumber),
                    HomeNumber = HttpUtility.HtmlEncode(model.HomeNumber),
                    Address = HttpUtility.HtmlEncode(model.Address),
                    Email = HttpUtility.HtmlEncode(model.Email),
                    memberUserName = HttpUtility.HtmlEncode(model.memberUserName),
                    birthday = DateTime.Parse(HttpUtility.HtmlEncode(model.birthday)),
                    Gender = int.Parse(HttpUtility.HtmlEncode(model.Gender)),
                    IDnumber = HttpUtility.HtmlEncode(model.IDnumber)
                };
                originalMember = updateMember;
            }
            try
            {
                _DbRepository.Update<member>(originalMember);
                result.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                result.IsSuccessful = false;
                ex.ToString();
            }
            return result;
        }


    }
}