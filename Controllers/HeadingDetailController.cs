using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace Soscial_Media.Controllers
{
  
    public class HeadingDetailController : Controller
    {
        HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index(int id=1)
        {
            var byheading = hm.GetByID(id);
            return View(byheading);
        }
        public ActionResult HdPartialView(int id=1)
        {
            var bycontent = cm.GetListByHeadingID(id);
            return PartialView(bycontent);
        }
        [HttpGet]
        public ActionResult HeadingAddCommentlView()
        {
           
        
            return View();
        }
        [HttpPost]
        public ActionResult HeadingAddCommentlView(Content p)
        {
       
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            cm.ContentAddBL(p);
            return RedirectToAction("Index");
        }
    }
}