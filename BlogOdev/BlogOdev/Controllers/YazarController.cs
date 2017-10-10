using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogOdev.Controllers
{
    using Models.data;
    public class YazarController : Controller
    {
        BlogDB ctx = new BlogDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult YazarOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YazarOl(Kullanici kl,string rdBay,string rdBayan)
        {

            if (!string.IsNullOrEmpty(rdBay))
            {
                kl.KullaniciCinsiyet = true;
            }

            if (!string.IsNullOrEmpty(rdBayan))
            {
                kl.KullaniciCinsiyet = false ;
            }
            kl.Yazar = true;
            kl.Onaylandi = false; //ilk olarak kayıt olanlar kendini doğrudan yazar olarak atayamasın önce admin sayfasında yazar olmak isteyen görüntülensin admin onaylarsa yazarın onaylandi alanını true a ya çekecem.o yüzden bu alanı defaultolarak false yaptım.
            kl.Aktif = true;
            kl.KayitTarihi = DateTime.Now;
            ctx.Kullanicis.Add(kl);
            ctx.SaveChanges();

            //kullanıcı kayıt olduktan sonra veritabnındaki yapım gereği kullanıcının id sini ve rolün  id sini alıp kullanıcı rol tabloma atmam gerekli. bu yüzden aşağıdaki gibi rol ve kullanıcı rol tablolarına erişip işlemleri yaptım.

            //Rol yazar = ctx.Rols.FirstOrDefault(x=>x.RolAdi=="Yazar");

            //KullaniciRol kr = new KullaniciRol();
            //kr.RolID = yazar.RolId;
            //kr.KullaniciID = kl.KullaniciId;

            //ctx.KullaniciRols.Add(kr);
            //ctx.SaveChanges();

            return RedirectToAction("Login","Kullanici");
        }

    }
}