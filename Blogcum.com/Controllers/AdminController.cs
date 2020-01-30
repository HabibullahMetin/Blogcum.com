using Blogcum.Models;
using Blogcum.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogcum.Controllers
{
    public class AdminController : Controller
    {
        private Blogcum4Entities db = new Blogcum4Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kategoriler()  // Kategoriler Sayfası
        {
            return View();
        }
        public ActionResult Blogcular()     // Blogcu isimleri bu sayfada listelenecek.
        {
            RelatedClass model = new RelatedClass();
            model.Blogcular = db.Blogcular.ToList();

            return View(model);
        }
        public ActionResult Iletisim()    // Bizimle iletişime gecmek icin kullanılan sayfa
        {
            RelatedClass model = new RelatedClass();
            model.Iletisim = db.Iletisim.ToList();

            return View(model);
        }
        public ActionResult Yorumlar()    // Bizimle iletişime gecmek icin kullanılan sayfa
        {
            RelatedClass model = new RelatedClass();
            model.Yorumlar = db.Yorumlar.ToList();

            return View(model);
        }
        public ActionResult YorumEkle()
        {
            List<Blogcular> blogcular = db.Blogcular.ToList();

            List<SelectListItem> blogcularList =
                (from blogcu in db.Blogcular.ToList()
                 select new SelectListItem()
                 {
                     Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                     Value = blogcu.BlogcuID.ToString()

                 }).ToList();

            TempData["Blogcular"] = blogcularList;
            ViewBag.Blogcular = blogcularList;

            List<SelectListItem> bloglarList =
                (from blog in db.Bloglar.ToList()
                 select new SelectListItem()
                 {
                     Text = blog.BlogYazisi,
                     Value = blog.BlogYazisiID.ToString()
                 }).ToList();

            TempData["Bloglar"] = bloglarList;
            ViewBag.Bloglar = bloglarList;

            return View();
        }
        [HttpPost]
        public ActionResult YorumEkle(Yorumlar yorum)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == yorum.Blogcular.BlogcuID).FirstOrDefault();
            Bloglar blog = db.Bloglar.Where(x => x.BlogYazisiID == yorum.Bloglar.BlogYazisiID).FirstOrDefault();

            if(blogcu != null || blog != null)
            {
                yorum.Blogcular = blogcu;
                yorum.Bloglar = blog;
                db.Yorumlar.Add(yorum);
                int sonuc = db.SaveChanges();

                if(sonuc > 0)
                {
                    ViewBag.Result = "Yorum kaydedilmistir.";
                    ViewBag.Status = "success";

                }

                else
                {
                    ViewBag.Resut = "İleti kaydedilememistir.";
                    ViewBag.Status = "danger";

                }

                ViewBag.Blogcular = TempData["Blogcular"];
                ViewBag.Bloglar = TempData["Bloglar"];

            }

            return View();
        }
        // : GET
         public ActionResult IletiEkle()
         {

             List<Blogcular> blogcular = db.Blogcular.ToList();

             List<SelectListItem> blogcularList =
                 (from blogcu in db.Blogcular.ToList()
                  select new SelectListItem()
                  {
                      Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                      Value = blogcu.BlogcuID.ToString()

                  }).ToList();

             TempData["Blogcular"] = blogcularList;
             ViewBag.Blogcular = blogcularList;

             return View();
         } 
        [HttpPost]
        public ActionResult IletiEkle(Iletisim ileti)  // Response
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == ileti.Blogcular.BlogcuID).FirstOrDefault();

            if (blogcu != null)
            {

                ileti.Blogcular = blogcu;
                db.Iletisim.Add(ileti);
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "İleti kaydedilmistir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "İleti kaydedilememistir.";
                    ViewBag.Status = "danger";
                }

                ViewBag.Blogcular = TempData["Blogcular"];
            }

            return View();
        } 
        public ActionResult Bloglar()    // Bloglar burada listelenicek
        {
            RelatedClass model = new RelatedClass();
            model.Bloglar = db.Bloglar.ToList();
            return View(model);
        }
        // GET
        public ActionResult BlogcuEkle()  // Request
        { 
             return View();
        }
        [HttpPost]
        public ActionResult BlogcuEkle(Blogcular blogcu)    //Response
        {
            
            db.Blogcular.Add(blogcu);

            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.Result = "Blogcu kaydedilmistir.";
                ViewBag.Status = "success";
            }
            else
            {
                ViewBag.Resut = "Blogcu kaydedilememistir.";
                ViewBag.Status = "danger";
            }     
            return View();
        }
        // : GET
        public ActionResult BlogEkle()      //Request
        {
           
            List<Blogcular> blogcular = db.Blogcular.ToList();

            List<SelectListItem> blogcularList = 
                (from blogcu in db.Blogcular.ToList()
            select new SelectListItem()
            {
                Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                Value = blogcu.BlogcuID.ToString()

            }).ToList();

            TempData["Blogcular"] = blogcularList;
            ViewBag.Blogcular = blogcularList;


            return View();
        }
        [HttpPost]
        public ActionResult BlogEkle(Bloglar blog)  //Response
        {

            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == blog.Blogcular.BlogcuID).FirstOrDefault();

            if(blogcu != null)
            {

                blog.Blogcular = blogcu;
                db.Bloglar.Add(blog);
                int sonuc = db.SaveChanges();
            
                if (sonuc > 0)
                {
                    ViewBag.Result = "Blog kaydedilmistir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "Blog kaydedilememistir.";
                    ViewBag.Status = "danger";
                }

                ViewBag.Blogcular = TempData["Blogcular"];

            }
            return View();
        }
        // GET
        public ActionResult BlogcuGuncelle(int? blogcuid)
        {
            Blogcular blogcu = null;

            if(blogcuid != null)
            {
                blogcu = db.Blogcular.Where(x => x.BlogcuID == blogcuid).FirstOrDefault();
            }

            return View(blogcu);
        }
        [HttpPost]
        public ActionResult BlogcuGuncelle(Blogcular blogger, int? blogcuid)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == blogcuid).FirstOrDefault();

            if(blogcu != null)
            {
                blogcu.BlogcuAdi = blogger.BlogcuAdi;
                blogcu.BlogcuSoyadi = blogger.BlogcuSoyadi;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Blogcu güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "Blogcu güncellenememiştir.";
                    ViewBag.Status = "danger";
                }

            }
            return RedirectToAction("Blogcular");
        }
        // GET
        public ActionResult BlogGuncelle(int? blogid)
        {
            Bloglar blog = null;
            if(blogid != null)
            {

            List<Blogcular> blogcular = db.Blogcular.ToList();

            List<SelectListItem> blogcularList =
                (from blogcu in db.Blogcular.ToList()
                 select new SelectListItem()
                 {
                     Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                     Value = blogcu.BlogcuID.ToString()

                 }).ToList();

            TempData["Blogcular"] = blogcularList;
            ViewBag.Blogcular = blogcularList;

             blog = db.Bloglar.Where(x => x.BlogYazisiID == blogid).FirstOrDefault();
            }

            return View(blog);
        }
        [HttpPost]
        public ActionResult BlogGuncelle(Bloglar blog2, int? blogid)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == blog2.Blogcular.BlogcuID).FirstOrDefault();
            Bloglar blog = db.Bloglar.Where(x => x.BlogYazisiID == blogid).FirstOrDefault();

            if (blog != null)
            {
                blog.Kategori = blog2.Kategori;
                blog.BlogYazisi = blog2.BlogYazisi;
                blog.Blogcular = blogcu;


                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Blogcu güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "Blogcu güncellenememiştir.";
                    ViewBag.Status = "danger";
                }   
            }
            ViewBag.Blogcular = TempData["Blogcular"];
            return RedirectToAction("Bloglar");
        }
        // GET
        public ActionResult IletiGuncelle(int? iletiid)
        {
            Iletisim ileti = null;
            if (iletiid != null)
            {
                List<Blogcular> blogcular = db.Blogcular.ToList();

                List<SelectListItem> blogcularList =
                    (from blogcu in db.Blogcular.ToList()
                     select new SelectListItem()
                     {
                         Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                         Value = blogcu.BlogcuID.ToString()

                     }).ToList();

                TempData["Blogcular"] = blogcularList;
                ViewBag.Blogcular = blogcularList;

                ileti = db.Iletisim.Where(x => x.IletiID == iletiid).FirstOrDefault();
            }
                return View(ileti);
        }
        [HttpPost]
        public ActionResult IletiGuncelle(Iletisim message, int? iletiid)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == message.Blogcular.BlogcuID).FirstOrDefault();
            Iletisim ileti = db.Iletisim.Where(x => x.IletiID == iletiid).FirstOrDefault();
            
            if(ileti != null)
            {
                ileti.Konu = message.Konu;
                ileti.Ileti = message.Ileti;
                ileti.Blogcular = blogcu;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Blogcu güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "Blogcu güncellenememiştir.";
                    ViewBag.Status = "danger";
                }
                ViewBag.Blogcular = TempData["Blogcular"];
            }
            return RedirectToAction("Iletisim");
        }
        // GET
        public ActionResult YorumGuncelle(int? yorumid)
        {
            Yorumlar yorum = null;
            if (yorumid != null)
            {
                List<Blogcular> blogcular = db.Blogcular.ToList();

                List<SelectListItem> blogcularList =
                    (from blogcu in db.Blogcular.ToList()
                     select new SelectListItem()
                     {
                         Text = blogcu.BlogcuAdi + " " + blogcu.BlogcuSoyadi,
                         Value = blogcu.BlogcuID.ToString()

                     }).ToList();

                List<Bloglar> bloglar = db.Bloglar.ToList();

                List<SelectListItem> bloglarList =
               (from blog in db.Bloglar.ToList()
                select new SelectListItem()
                {
                    Text = blog.BlogYazisi,
                    Value = blog.BlogYazisiID.ToString()
                }).ToList();

                TempData["Blogcular"] = blogcularList;
                ViewBag.Blogcular = blogcularList;

                TempData["Bloglar"] = bloglarList;
                ViewBag.Bloglar = bloglarList;

                yorum = db.Yorumlar.Where(x => x.YorumID == yorumid).FirstOrDefault();

            }
                return View(yorum);
        }
        [HttpPost]
        public ActionResult YorumGuncelle(Yorumlar comment, int? yorumid)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == comment.Blogcular.BlogcuID).FirstOrDefault();
            Bloglar blog = db.Bloglar.Where(x => x.BlogYazisiID == comment.Bloglar.BlogYazisiID).FirstOrDefault();
            Yorumlar yorum = db.Yorumlar.Where(x => x.YorumID == yorumid).FirstOrDefault();

            if (yorum!= null  )
            {
                yorum.Yorum = comment.Yorum;
                yorum.Blogcular = blogcu;
                yorum.Bloglar = blog;

                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Blogcu güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Resut = "Blogcu güncellenememiştir.";
                    ViewBag.Status = "danger";
                }

                ViewBag.Blogcular = TempData["Blogcular"];
                ViewBag.Bloglar = TempData["Bloglar"];
            }

                return RedirectToAction("Yorumlar");
        }
        // GET
        public ActionResult BlogcuSil()
        {

            return View(); 
        }
        [HttpPost]
        public ActionResult BlogcuSil2()
        {

            return View();
        }
        // GET
        public ActionResult BlogSil()
        {

            return View();
        }
        [HttpPost]
        public ActionResult BlogSil2()
        {

            return View();
        }
        // GET
        public ActionResult IletiSil()
        {

            return View();
        }
        [HttpPost]
        public ActionResult IletiSil2()
        {

            return View();
        }

        // GET
        public ActionResult YorumSil()
        {

            return View();
        }
        [HttpPost]
        public ActionResult YorumSil2()
        {

            return View();
        }



    }
}
