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



    }
}