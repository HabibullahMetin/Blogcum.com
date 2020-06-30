using Blogcum.Models;
using Blogcum.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace Blogcum.Controllers
{
    public class HomeController : Controller
    {
        private Blogcum4Entities db = new Blogcum4Entities();
        // GET: Home
        public ActionResult Index()
        {
            RelatedClass model = new RelatedClass();
            model.Bloglar = db.Bloglar.ToList();

            HttpCookie cerez = new HttpCookie("blogcu", "text1");
            HttpContext.Response.Cookies.Add(cerez);   // Kullanıcının bilgisayarinda eklenmesi için yanit
            cerez.Expires = DateTime.Now.AddDays(2);
            ViewBag.Blogcu = Session["BlogcuGiris"]!=null ? Session["BlogcuGiris"].ToString() : "";        // Diyelim ki kullanici cıkıs yapmadi, direkt sayfayi kapatti yaptıgı islemler
            // sonucu üretilen cookielerin ömrü 2 gün olarak belirledik.  // Cikis yapsa bile 2 gündür süresi.

            return View(model);   
        }
        public ActionResult Giris()
        {
          return View();
        }
        [HttpPost]
        public ActionResult Giris(string text1)
        {
            Session.Add("BlogcuGiris", text1);       // Kullanici giris yaptiginda session olusturulur. 
                // 2 günlük süreyle cookiler de clientta tutulur.
            ViewBag.Blogcu = Session["BlogcuGiris"].ToString();
            return RedirectToAction("Index");
        }

        public ActionResult Cikis()
        {
           if(Session["BlogcuGiris"] != null) 
            {
                Session.Remove("BlogcuGiris");

            } 
           if(HttpContext.Request.Cookies["blogcu"] != null)
            {
                //HttpContext.Request.Cookies.Remove("blogcu");   // Sıkıntı verebilir. Silinmeyebilir.
                HttpContext.Request.Cookies["blogcu"].Expires = DateTime.Now.AddSeconds(3);
                // Silinmesi için istek gönderilir
            }

            return RedirectToAction("Index");
        }
        public ActionResult Kayit()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(string text2)
        {
           Session.Add("BlogcuKayit", text2);
            return RedirectToAction("Index");
        }

        public ActionResult DilDegistir(string secilenDil)
        {
            if (secilenDil != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(secilenDil);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(secilenDil);
            }

            HttpCookie cookie = new HttpCookie("dil")
            {
                Value = secilenDil
            };
            Response.Cookies.Add(cookie);

            return RedirectToAction("Giris","Home");
        }

        public ActionResult AdminSayfasi()
        {

            return RedirectToActionPermanent("Index","Admin");
        }
    }

    }
