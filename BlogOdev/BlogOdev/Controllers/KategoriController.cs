using BlogOdev.Models;
using BlogOdev.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOdev.Controllers
{
    public class KategoriController : Controller
    {
        BlogDB ctx = new BlogDB();
        // GET: Kategori
        public ActionResult Index( int id)
        {
            return View(id);
        }

        public ActionResult Liste()
        {
            var data = ctx.Kategoris.ToList();
            return View(data);
        }


        public PartialViewResult KategoriWidget()
        {
            return PartialView(ctx.Kategoris.ToList());
        }

        public ActionResult MakaleListele(int id)
        {
            var data = ctx.Makales.Where(x => x.KategoriID == id).ToList();
            return View("MakaleListeleWidget",data);
        }

        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Yazar")]
        [HttpPost]
        public ActionResult KategoriEkle(Kategori model)
        {
            ctx.Kategoris.Add(model);
            ctx.SaveChanges();
            return RedirectToAction("Liste");
            
        }

        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult Duzenle(int id)
        {
            var data = ctx.Kategoris.Find(id);
            return View(data);
        }
        [Authorize(Roles = "Admin,Yazar")]
        [HttpPost]
        public ActionResult Duzenle(Kategori model)
        {
            var data = ctx.Kategoris.Find(model.KategoriId);
            data.KategoriAdi = model.KategoriAdi;
            data.Aciklama = model.Aciklama;

            ctx.SaveChanges();
            return RedirectToAction("Liste");
        }
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult Sil(int id)
        {
            var data = ctx.Kategoris.Find(id);
            ctx.Kategoris.Remove(data);
            ctx.SaveChanges();

            return RedirectToAction("Liste");
        }

    }
}