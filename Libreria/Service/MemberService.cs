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
        public OperationResult UpdateMember(MemberViewModel model)
        {
            var result = new OperationResult();
            var originalMember = _DbRepository.GetAll<member>().Where(m => m.memberName == model.memberName).FirstOrDefault();
            originalMember.memberUserName = model.memberUserName;
            originalMember.birthday = model.birthday;
            originalMember.IDnumber = model.IDnumber;
            originalMember.Gender = model.Gender;
            originalMember.Email = model.Email;
            originalMember.MobileNumber = model.MobileNumber;
            originalMember.HomeNumber = model.HomeNumber;
            originalMember.City = model.City;
            originalMember.Region = model.Region;

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

        public OperationResult ChangePassword(PasswordViewModel model)
        {
            var result = new OperationResult();
            return result;
        }

    }
}