﻿using Libreria.Helpers;
using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.Service
{
    public class MemberRegisterPageService
    {
        public readonly LibreriaRepository _libreriaRepository;

        public MemberRegisterPageService()
        {
            _libreriaRepository = new LibreriaRepository();
        }
        public OperationResult CreateMember(MemberViewModel model, bool IsValid)
        {
            var result = new OperationResult();
            member member = null;
            if (IsValid)
            {
                member = new member
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
            }
            try
            {
                //後端判斷帳號是否有重複
                if (IsExistMember(member).IsSuccessful == false)
                {

                    _libreriaRepository.Create<member>(member);
                    result.IsSuccessful = true;
                }
                else
                {
                    //如果後斷判斷有重複資料需要執行?
                }

            }
            //下列部分需要再處裡
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccessful = false;
            }
            return result;
        }
        public OperationResult IsExistMember(member membername)
        {
            var result = new OperationResult();
            member member = null;
            membername = _libreriaRepository.GetAll<member>().Where(m => member.memberName == membername.memberName)
                                        .FirstOrDefault();
            if (member != null)
            {
                result.IsSuccessful = true;
            }
            else
            {
                result.IsSuccessful = false;
            }
            return result;
                
        }
    }
      
}