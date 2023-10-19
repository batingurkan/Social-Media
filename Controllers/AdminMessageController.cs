using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace Soscial_Media.Controllers
{
    public class AdminMessageController : Controller
    {
        AdminManager adm = new AdminManager(new EfAdminDal());
        MessageValidator messagevalidator = new MessageValidator();
        WriterManager wm = new WriterManager(new EfWriterDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        Context c = new Context();

        public PartialViewResult AdminMessagePartial()
        {
            string p = (string)Session["AdminMail"];
          
            return PartialView();
        }

        public ActionResult AdminContent()
        {
            string p = (string)Session["AdminMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult AdminInBox()
        {
            string p = (string)Session["AdminMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult AdminGetInboxMessageDetails(int id)
        {
            string p = (string)Session["AdminMail"];
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult AdminSendbox()
        {
            string p = (string)Session["AdminMail"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }

        public ActionResult AdminGetSendboxMessageDetails(int id)
        {
            string p = (string)Session["AdminMail"];
            var values = mm.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult AdminNewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminNewMessage(Message p)
        {
            string mail = (string)Session["AdminMail"];
            var adminidinfo = c.Admins.Where(x => x.AdminMail == mail).Select(y => y.AdminID).FirstOrDefault();


            ValidationResult result = messagevalidator.Validate(p);
            if (result.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.SenderMail = mail;
                mm.MessageAddBL(p);
                return RedirectToAction("AdminSendbox");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

     
    }
}