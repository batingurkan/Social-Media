using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace Soscial_Media.Controllers
{
   
    public class DiscoverController : Controller
    {
        HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
        public ActionResult Index()
        {
            string p = (string)Session["WriterMail"];
            var hmget = hm.GetList();
            return View(hmget);
        }
    }
}