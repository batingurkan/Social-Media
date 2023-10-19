using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using static System.Net.WebRequestMethods;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Soscial_Media.Controllers
{
    [AllowAnonymous]
    public class WriterLoginController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Writer p)
        {
            var writeruserinfo = wlm.GetWriter(p.WriterMail, p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("Index", "WriterProfile");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "WriterLogin");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Writer p)
        {

            WriterValidator writerValidator = new WriterValidator();
            ValidationResult result = writerValidator.Validate(p);
            if (result.IsValid)
            {
                p.WriterImage = "https://i.hizliresim.com/pyuqlss.jpg";
                p.WriterFollower = "0";
                p.WriterFollowed = "0";

                wm.WriterAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }
    }
}