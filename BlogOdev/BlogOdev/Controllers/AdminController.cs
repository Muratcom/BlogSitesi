using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOdev.Controllers
{
    using Models.data;
    public class AdminController : Controller
    {
        BlogDB ctx = new BlogDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult YazarOnaylari()
        {
            var data = ctx.Kullanicis.Where(x => x.Yazar == true && x.Onaylandi == false).ToList();
            return View(data);
        }

        public ActionResult OnayVer(int id)
        {
            Kullanici kl = ctx.Kullanicis.FirstOrDefault(x=>x.KullaniciId==id);
            kl.Onaylandi = true;
           
            ctx.SaveChanges();

            Rol yazar = ctx.Rols.FirstOrDefault(x => x.RolAdi == "Yazar");

            KullaniciRol kr = new KullaniciRol();
            kr.RolID = yazar.RolId;
            kr.KullaniciID = kl.KullaniciId;

            ctx.KullaniciRols.Add(kr);
            ctx.SaveChanges();

            return RedirectToAction("YazarOnaylari");
        }
    }
}