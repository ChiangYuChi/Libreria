using Libreria.Models.EntityModel;
using Libreria.Repository;
using Libreria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Helpers;
using System.Data.Entity.Validation;

namespace Libreria.Service
{
    public class LineLoginService
    {
        public readonly LibreriaRepository _libreriaRepository;
        public LineLoginService()
        {
            _libreriaRepository = new LibreriaRepository();
        }
        public OperationResult CreateOrLoginLineMember(LineLoginViewModel model, bool isValid)
        {
            var result = new OperationResult();
            member member = new member();

            try
            {
                //後端判斷帳號是否已註冊
                if (IsExistLineMember(model).IsSuccessful == true)
                {
                    if (isValid && model.Email != null)
                    {
                        member = new member
                        {
                            LineUserID = model.LineUserID,
                            memberUserName = model.diaplayName,
                            Email = model.Email,
                            //以下為未能從TokenID解析出的資訊預設值
                            memberName = string.Empty,
                            MobileNumber = string.Empty,
                            //密碼預設LineUserID，若LineLogin使用者需直接使用忘記密碼修改
                            memberPassword = Utility.GetSha512(model.LineUserID),
                            //性別預設不透漏
                            Gender = 2,
                            //LineLogin註冊使用者可修改帳號一次
                            Change = true

                        };
                    }
                    else if (isValid && model.Email == null)
                    {
                        member = new member
                        {
                            LineUserID = model.LineUserID,
                            memberUserName = model.diaplayName,
                            //以下為未能從TokenID解析出的資訊預設值
                            Email = string.Empty,
                            memberName = string.Empty,
                            MobileNumber = string.Empty,
                            //密碼預設LineUserID，若LineLogin使用者需直接使用忘記密碼修改
                            memberPassword = model.LineUserID,
                            //性別預設不透漏
                            Gender = 2,
                            //LineLogin註冊使用者可修改帳號一次
                            Change = true
                        };

                    }
                    try
                    {
                        _libreriaRepository.Create<member>(member);
                        result.IsSuccessful = true;
                    }

                    catch (DbEntityValidationException ex)
                    {
                        var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                        var getFullMessage = string.Join("; ", entityError);
                        var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);

                        result.IsSuccessful = false;
                    }

                }
                else
                {
                    getLineMember(model);
                    result.IsSuccessful = true;

                }

            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var getFullMessage = string.Join("; ", entityError);
                var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);

                result.IsSuccessful = false;
            }
            return result;
        }
        public member getLineMember(LineLoginViewModel model)
        {
            member member = null;
            try
            {

                member = _libreriaRepository.GetAll<member>().Where(u => u.LineUserID == model.LineUserID).FirstOrDefault();
                HttpContext.Current.Session["memberUserName"] = member.memberUserName;
                HttpContext.Current.Session["MemberID"] = member.memberId;
                return member;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return member;
        }
        public OperationResult IsExistLineMember(LineLoginViewModel memberLineID)
        {
            var result = new OperationResult();
            var checkMemberLineUserID = _libreriaRepository.GetAll<member>().Where(m =>m.LineUserID == memberLineID.LineUserID)
                                        .FirstOrDefault();
            if (checkMemberLineUserID != null)
            {
                result.IsSuccessful = false;
            }
            else
            {
                result.IsSuccessful = true;
            }
            return result;

        }
    }
}