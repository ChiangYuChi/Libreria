﻿using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libreria.Controllers
{
    public class MemberLoginController : Controller
    {
        // GET: MemberLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(member model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new LibreriaDataModel())
                {
                    member member = context.members
                                       .Where(u => u.memberId == model.memberId && u.memberPassword == model.memberPassword)
                                       .FirstOrDefault();
                    if (User != null)
                    {
                        Session["MemberName"] = member.memberName;
                        Session["MemberId"] = member.memberId;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "帳號或密碼輸入錯誤");
                        return View(model);
                    }

                }
            }
            else
            {
                return View(model);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["MemberName"] = string.Empty;
            Session["MemberId"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }

    }
}