using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

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
            member updateMember = null;
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
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                ex.ToString();
            }
            return result;
        }

        public OperationResult SendEmail(string email, string callbackurl)
        {
            var result = new OperationResult();
            var member = _DbRepository.GetAll<member>().Where(x => x.Email == email).FirstOrDefault();
            
            if (member != null)
            {
                ForgotPasswordEmail(member, callbackurl).Wait();
                result.IsSuccessful = true;
            }
            else
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        public async Task ForgotPasswordEmail(member member, string callbackurl)
        {
            var apikey = "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";
            var client = new SendGridClient(apikey);
            var from = new EmailAddress("dezhengl@uci.edu", "Libreria");
            var to = new EmailAddress(member.Email, member.memberName);
            var subject = "Libreria密码重置";
            var plainTextContent = "";
            var htmlContent = "<p>请点击链接来重置您的密码" + "<a href = '" + callbackurl + "'>重置密码</a></p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public OperationResult UpdatePassword(string email, string password)
        {
            var result = new OperationResult();
            var member = _DbRepository.GetAll<member>().Where(x => x.Email == email).FirstOrDefault();

            try
            {
                if (member != null)
                {
                    member.memberPassword = password; //修改密码,需要经过加密！！！
                    _DbRepository.Update<member>(member);
                    result.IsSuccessful = true;
                }
                else
                {
                    result.IsSuccessful = false;
                }
            }
            catch
            {
                result.IsSuccessful = false;
            }
            
            return result;
        }




    }
}