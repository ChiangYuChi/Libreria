﻿using Libreria.Helpers;
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
using Uti
using Libreria.Helpers;

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
#pragma warning disable CS0219 // 已指派變數 'updateMember'，但是從未使用過它的值
            member updateMember = null;
#pragma warning restore CS0219 // 已指派變數 'updateMember'，但是從未使用過它的值
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
            originalMember.Address = model.Address;

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

        public OperationResult UpdatePassword(string username, string password)
        {
            var result = new OperationResult();
            var member = _DbRepository.GetAll<member>().Where(x => x.memberUserName == username).FirstOrDefault();

            try
            {
                if (member != null)
                {
                    var newpassword = Utility.GetSha512(password);
                    member.memberPassword = newpassword;
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

        public OperationResult ChangePassword(PasswordViewModel model,bool isValid)
        {
            var result = new OperationResult();
            if (isValid)
            {
                var original = Utility.GetSha512(model.OriginalPassword);
                var member = _DbRepository.GetAll<member>().Where(m => m.memberPassword == original).FirstOrDefault();
                member.memberPassword = Utility.GetSha512(model.NewPassword);

                try
                {
                    _DbRepository.Update<member>(member);
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    ex.ToString();
                }
            }
            return result;
                        
        }




    }
}