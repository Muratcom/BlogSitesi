using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BlogOdev.Ap_Classes
{
    public class Settings
    {
        //küçükboy resimlerin boyut ayarları
       public static Size ResimKucukBoyut
        {
            //web.config içine yazığım boyutları burada çekerek her resim için tek tek boyutlandırma işlemi ile uğraşmak zorunda kalmıyorum.
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32( ConfigurationManager.AppSettings["sw"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["sh"]);
                return sonuc;
            }
        }

        //orta boy resimlerin boyut ayarları
        public static Size ResimOrtaBoyut
        {
            
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["mw"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["mh"]);
                return sonuc;
            }
        }

        //buyuk boy resimlerin boyut ayarları
        public static Size ResimBuyukBoyut
        {
            
            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["lw"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["lh"]);
                return sonuc;
            }
        }


        public static Size ResimYazar
        {

            get
            {
                Size sonuc = new Size();
                sonuc.Width = Convert.ToInt32(ConfigurationManager.AppSettings["yazar"]);
                sonuc.Height = Convert.ToInt32(ConfigurationManager.AppSettings["yazar"]);
                return sonuc;
            }
        }
    }
}
