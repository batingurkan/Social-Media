using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Web.Security;

namespace Soscial_Media.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        AdminManager adm = new AdminManager(new EfAdminDal());


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin p)
        {
            var admininfo = adm.GetAdmin(p.AdminMail, p.AdminPassword);
            if (admininfo != null)
            {
                FormsAuthentication.SetAuthCookie(admininfo.AdminMail, false);
                Session["AdminMail"] = admininfo.AdminMail;
                return RedirectToAction("AdminProfile", "Admin");
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        public ActionResult LogOut()
        {
             FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "AdminLogin");
        }
    }
}