using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.Ajax.Utilities;
using Context = DataAccessLayer.Concrete.Context;

namespace Soscial_Media.Controllers
{

    public class AdminController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
        AdminManager adm = new AdminManager(new EfAdminDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        Context c = new Context();

        public ActionResult _AdminLayout(string p)
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();
            var admvalue = adm.GetByID(adminidinfo);
            return View(admvalue);
        }

        public ActionResult Index()
        {
            string p = (string)Session["AdmiMail"];
          
            var heading = hm.GetList().Select(x => new Heading()
            {
               HeadingDate = x.HeadingDate, 
                WriterID = x.Writer.ToString().Count(),
                CategoryID = x.Category.ToString().Count(),
                HeadingID = x.HeadingImage.ToString().Count(),
               
            
            }).FirstOrDefault();
            return View(heading);
        }
        public ActionResult AdminProfile(string p)
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();
            var admvalue = adm.GetByID(adminidinfo);
            return View(admvalue);
        }
        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var admininfo = adm.GetByID(id);
            return View(admininfo);
        }
        [HttpPost]
        public ActionResult EditProfile(Admin p)
        {
            adm.AdminUpdate(p);
            return RedirectToAction("AdminProfile");
        }
        public ActionResult Headings()
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();
            var heading = hm.GetList();

            return View(heading);
        }
        public ActionResult HeadingsEdit(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            ViewBag.vlc = valuecategory;


            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }
        [HttpPost]
        public ActionResult HeadingsEdit(Heading p)
        {
            hm.HeadingUpdateBL(p);
            return RedirectToAction("Headings");
        }
        public ActionResult RemoveHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            HeadingValue.HeadingStatus = false;
            hm.HeadingDeleteBL(HeadingValue);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult AdminContact()
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();

            var admlist = adm.GetList();

            return View(admlist);
        }
       
        public ActionResult AdmProfileDetail(int id)
        {
            var adminfo = adm.GetByID(id);
            return View(adminfo);
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminfo = adm.GetByID(id);
            return View(adminfo);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {

            adm.AdminUpdate(p);

            return RedirectToAction("AdminContact");
        }
        public ActionResult WriterContact()
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();

            var writerlist = wm.GetList();
            return View(writerlist);
        }
        public ActionResult WrProfileDetail(int id)
        {
           var wrinfo = wm.GetByID(id);
            return View(wrinfo);
        }
        public ActionResult DeleteWriter(int id)
        {
            var wrdelete = wm.GetByID(id);
            wrdelete.WriterStatus = false;
            wm.WriterDelete(wrdelete);
            return RedirectToAction("WrProfileDetail");
        }
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetByID(id);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {

            wm.WriterUpdate(p);
            return RedirectToAction("WriterContact");
        }

    }
}