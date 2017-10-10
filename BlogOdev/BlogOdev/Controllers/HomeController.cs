using BlogOdev.Models;
using BlogOdev.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOdev.Controllers
{
    public class HomeController : Controller
    {
        BlogDB ctx = new BlogDB();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

      public ActionResult MakaleListele()
        {
            var data = ctx.Makales.ToList();
            return View("MakaleListeleWidget", data);
        }
       

        public PartialViewResult SonEklenenlerWidget()
        {
            var model = ctx.Makales.OrderByDescending(x => x.MakaleEklenmeTarihi).Take(3).ToList();
            return PartialView(model);
            
        }
    }
}