using BlogOdev.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogOdev.Controllers
{
    public class KullaniciController : Controller
    {
        BlogDB ctx = new BlogDB();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici kl)
        {
            string ka = ValidateUser(kl.KullaniciAdi, kl.Parola);
            if (!string.IsNullOrEmpty(ka))
            {
                //FormsAuthenticationTicket :Role göre kullanıcıya giriş yaptırmamızı sağlar
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "usercookie", DateTime.Now, DateTime.Now.AddMinutes(15), true, ka, FormsAuthentication.FormsCookiePath);

                //cookie leri tutacaz
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);
                


                if (ticket.IsPersistent)
                {
                    ck.Expires = ticket.Expiration; //cookie nin ölme süresi ticketin expirationuna bağlı olsun.
                }
                Response.Cookies.Add(ck);

                FormsAuthentication.RedirectFromLoginPage(kl.KullaniciAdi, true);
            }
            return RedirectToAction("Index","Home");
        }

        string ValidateUser(string kadi, string sifre)
        {
            //aşağıdaki yaptığım işlemler ile siteye login yapıldığında kullanıcı adı ve parola eşleşmesinde önce kullanıcı tablosundakiile karşılaştırıyorum eğe kullanıcı değilse yazardır diyorum

            Kullanici kl = ctx.Kullanicis.SingleOrDefault(x => x.KullaniciAdi == kadi && x.Parola == sifre);
            if (kl != null)
            {
                return kl.KullaniciAdi;
            }
            else
            {
                return "";
            }
        }

        public ActionResult CikisYap( string redirectUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(redirectUrl);
        }

        public ActionResult UyeOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeOl(Kullanici kl,string rdBay, string rdBayan)
        {
            if (!string.IsNullOrEmpty(rdBay))
            {
                kl.KullaniciCinsiyet = true;
            }
            if (!string.IsNullOrEmpty(rdBayan))
            {
                kl.KullaniciCinsiyet = false;
            }
            kl.Yazar = false;
            kl.Onaylandi = true;
            kl.Aktif = true;
            kl.DogumTarihi = kl.DogumTarihi.Value.Date;
            kl.ResimID = 1;
            kl.KayitTarihi = DateTime.Now;
            ctx.Kullanicis.Add(kl);
            ctx.SaveChanges();
            return RedirectToAction("Login");
        }


    }
}