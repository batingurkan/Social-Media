using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace Soscial_Media.Controllers
{
    public class WriterFriendProfileController : Controller
    {
        // GET: WriterFriendProfile
       
            HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
            WriterManager wm = new WriterManager(new EfWriterDal());
            Context c = new Context();
            public ActionResult FriendIndex(int id)
            {
               
                var WriterValues = wm.GetByID(id);
                return View(WriterValues);
            }
            public ActionResult FrWpImagePartial(int id)
            {
                
                var headingmg = hm.GetListByWriter(id);
                return PartialView(headingmg);
            }
       
    }
}