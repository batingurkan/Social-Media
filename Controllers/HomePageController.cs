using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace Soscial_Media.Controllers
{
    public class HomePageController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        HeadingMenager hm = new HeadingMenager(new EfHeadingDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        public ActionResult Index()
        {
            var heading = hm.GetList();
            return View(heading);
        }
       public ActionResult HomePartial()
        {
            
            var writer = wm.GetListByProfile();
            return PartialView(writer);
        }
        public ActionResult HomeCommentPartial(int id=1)
        {
           
            var content = cm.GetListByHeadingID(id);
            return PartialView(content);
        }
    }
}