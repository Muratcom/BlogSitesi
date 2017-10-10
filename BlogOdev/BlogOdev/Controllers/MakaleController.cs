using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogOdev.Controllers
{
    using BlogOdev.Ap_Classes;
    using Models.data;
    using System.Drawing;

    [Authorize]
    public class MakaleController : Controller
    {
        BlogDB ctx = new BlogDB();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = ctx.Makales.FirstOrDefault(x => x.MakaleId == id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Detay(string YorumcuAdSoyad,string YorumcuMail,string YorumcuWebAdresi,string Yorum,int id)
        {
            Yorum yorum = new Models.data.Yorum();
            yorum.Yorum1 = Yorum;
            yorum.MakaleID = id;
            yorum.YorumEklenmeTarihi = DateTime.Now;
            yorum.YorumcuAdSoyad = YorumcuAdSoyad;
            yorum.YorumBegeni = 0;
            yorum.YorumcuMail = YorumcuMail;
            yorum.YorumcuWebAdresi = YorumcuWebAdresi;

            ctx.Yorums.Add(yorum);

            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult MakaleEkle()
        {
            ViewBag.Kategoriler = ctx.Kategoris.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Yazar")]
        [HttpPost]
        public ActionResult MakaleEkle(Makale mkl,HttpPostedFileBase resim)
        {
            Image img = Image.FromStream(resim.InputStream);
            Bitmap kucukboy = new Bitmap(img, Settings.ResimKucukBoyut);
            Bitmap ortaboy = new Bitmap(img, Settings.ResimOrtaBoyut);
            Bitmap buyukboy = new Bitmap(img, Settings.ResimBuyukBoyut);

            kucukboy.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/"+resim.FileName));
            ortaboy.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
            buyukboy.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));
            
            Resim rsm = new Resim();
            rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;
            rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
            rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;

            ctx.Resims.Add(rsm);
            ctx.SaveChanges();

            mkl.ResimID = rsm.ResimId;
            mkl.MakaleEklenmeTarihi = DateTime.Now;
            mkl.GoruntulenmeSayisi = 0;
            mkl.BegeniSayisi = 0;

            int yazarId = ctx.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
            mkl.YazarID = yazarId;

            ctx.Makales.Add(mkl);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");

        }



     

        [AllowAnonymous]
        public string Begen(int id)
        {
            Makale mkl = ctx.Makales.FirstOrDefault(x => x.MakaleId == id);
            mkl.BegeniSayisi++;
            ctx.SaveChanges();
            return mkl.BegeniSayisi.ToString();
        }


        [AllowAnonymous]
        public string  Goruntule(int id)
        {
            Makale mkl = ctx.Makales.FirstOrDefault(x => x.MakaleId == id);
            mkl.GoruntulenmeSayisi++;
            ctx.SaveChanges();

            return mkl.GoruntulenmeSayisi.ToString();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Duzenle(int id)
        {
            ViewBag.Kategoriler = ctx.Kategoris.ToList();
            var model = ctx.Makales.Find(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Duzenle(Makale model)
        {
            var data = ctx.Makales.Find(model.MakaleId);
            data.Baslik = model.Baslik;
            data.Detay = model.Detay;
            data.MakaleEklenmeTarihi = DateTime.Now;
           
            ctx.SaveChanges();

            return RedirectToAction("Index","Home");
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Sil(int id)
        {
            var data = ctx.Makales.Find(id);        
            ctx.Makales.Remove(data);
            
            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        
    }
}