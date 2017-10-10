using BlogOdev.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOdev.Controllers
{
    
    public class EtiketController : Controller
    {
        BlogDB ctx = new BlogDB();
        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public PartialViewResult EtiketWidget()
        {
            return PartialView(ctx.Etikets.ToList());
        } 

        public ActionResult MakaleListele(int id)
        {
            var data = ctx.Makales.Where(x => x.Etikets.Any(y => y.EtiketId == id)).ToList();
            return View("MakaleListeleWidget",data);
        }
    }
}