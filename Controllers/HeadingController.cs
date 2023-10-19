using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace Soscial_Media.Controllers
{
    public class HeadingController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddHeading()
        {


            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();



            ViewBag.vlc = valuecategory;

            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            string mail = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writeridinfo;
            p.HeadingStatus = true;
            hm.HeadingAddBL(p);
            return RedirectToAction("Index","WriterProfile");
        }
    }
}