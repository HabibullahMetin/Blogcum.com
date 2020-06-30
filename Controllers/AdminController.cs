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
        private RelatedClass model = new RelatedClass();
       
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
            
            model.Blogcular = db.Blogcular.ToList();

            return View(model);
        }
        public ActionResult Iletisim()    // Bizimle iletişime gecmek icin kullanılan sayfa
        {
            
            model.Iletisim = db.Iletisim.ToList();

            return View(model);
        }
        public ActionResult Yorumlar()    // Bizimle iletişime gecmek icin kullanılan sayfa
        {
            
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

            return View(new Yorumlar());
        }
        [HttpPost]  
        public ActionResult YorumEkle(Yorumlar yorum)
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == yorum.Blogcular.BlogcuID).FirstOrDefault();
            Bloglar blog = db.Bloglar.Where(x => x.BlogYazisiID == yorum.Bloglar.BlogYazisiID).FirstOrDefault();

            /*if(!ModelState.IsValid)
            { 
            List<Blogcular> blogcular = db.Blogcular.ToList();

            List<SelectListItem> blogcularList =
                (from blogcu2 in db.Blogcular.ToList()
                 select new SelectListItem()
                 {
                     Text = blogcu2.BlogcuAdi + " " + blogcu2.BlogcuSoyadi,
                     Value = blogcu2.BlogcuID.ToString()

                 }).ToList();

            TempData["Blogcular"] = blogcularList;
            ViewBag.Blogcular = blogcularList;

            List<SelectListItem> bloglarList =
                (from blog2 in db.Bloglar.ToList()
                 select new SelectListItem()
                 {
                     Text = blog2.BlogYazisi,
                     Value = blog2.BlogYazisiID.ToString()
                 }).ToList();

            TempData["Bloglar"] = bloglarList;
            ViewBag.Bloglar = bloglarList;

                return View("YorumEkle", new Yorumlar());

            }*/

            if (blogcu != null || blog != null)
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

            return View(yorum);
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

             return View(new Iletisim());
         } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IletiEkle(Iletisim ileti)  // Response
        {
            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == ileti.Blogcular.BlogcuID).FirstOrDefault();

            /*if (!ModelState.IsValid)
            {
                List<Blogcular> blogcular = db.Blogcular.ToList();

                List<SelectListItem> blogcularList =
                     (from blogcu2 in db.Blogcular.ToList()
                      select new SelectListItem()
                      {
                          Text = blogcu2.BlogcuAdi + " " + blogcu2.BlogcuSoyadi,
                          Value = blogcu2.BlogcuID.ToString()

                      }).ToList();

                TempData["Blogcular"] = blogcularList;
                ViewBag.Blogcular = blogcularList;


                return View("IletiEkle", new Iletisim());
            }*/

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

            return View(ileti);
        } 
        public ActionResult Bloglar()    // Bloglar burada listelenicek
        {
           
            model.Bloglar = db.Bloglar.ToList();
            return View(model);
        }
        // GET
        public ActionResult BlogcuEkle()  // Request
        { 
             return View(new Blogcular());
        }
        [HttpPost]
        public ActionResult BlogcuEkle(Blogcular blogcu)    //Response
        {
            /*if (!ModelState.IsValid)
            {

                return View("BlogcuEkle", new Blogcular());
            }*/

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
            return View(blogcu);
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


            return View(new Bloglar());
        }
        [HttpPost]
        public ActionResult BlogEkle(Bloglar blog)  //Response
        {

            Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == blog.Blogcular.BlogcuID).FirstOrDefault();

            /*if(!ModelState.IsValid)
            {
                List<Blogcular> blogcular = db.Blogcular.ToList();

                List<SelectListItem> blogcularList =
                    (from blogcu2 in db.Blogcular.ToList()
                     select new SelectListItem()
                     {
                         Text = blogcu2.BlogcuAdi + " " + blogcu2.BlogcuSoyadi,
                         Value = blogcu2.BlogcuID.ToString()

                     }).ToList();

                List<Bloglar> bloglar = db.Bloglar.ToList();

                return View("BlogEkle", new Bloglar());
            }*/


            if (blogcu != null)
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
            return View(blog);
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

            /*if (!ModelState.IsValid)
            {
                return View("BlogcuGuncelle", new Blogcular());
            }*/

            if (blogcu != null)
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

            /*if (!ModelState.IsValid)
            {
                List<Blogcular> blogcular = db.Blogcular.ToList();

                List<SelectListItem> blogcularList =
                     (from blogcu2 in db.Blogcular.ToList()
                      select new SelectListItem()
                      {
                          Text = blogcu2.BlogcuAdi + " " + blogcu2.BlogcuSoyadi,
                          Value = blogcu2.BlogcuID.ToString()

                      }).ToList();

                TempData["Blogcular"] = blogcularList;
                ViewBag.Blogcular = blogcularList;


                return View("BlogGuncelle", new Bloglar());
            }*/

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

           /* if(!ModelState.IsValid)
            { 
            List<Blogcular> blogcular = db.Blogcular.ToList();

            List<SelectListItem> blogcularList =
                (from blogcu2 in db.Blogcular.ToList()
                 select new SelectListItem()
                 {
                     Text = blogcu2.BlogcuAdi + " " + blogcu2.BlogcuSoyadi,
                     Value = blogcu2.BlogcuID.ToString()

                 }).ToList();

            List<Bloglar> bloglar = db.Bloglar.ToList();

            List<SelectListItem> bloglarList =
           (from blog2 in db.Bloglar.ToList()
            select new SelectListItem()
            {
                Text = blog2.BlogYazisi,
                Value = blog2.BlogYazisiID.ToString()
            }).ToList();

            TempData["Blogcular"] = blogcularList;
            ViewBag.Blogcular = blogcularList;

            TempData["Bloglar"] = bloglarList;
            ViewBag.Bloglar = bloglarList;

                return View("YorumGuncelle", new Yorumlar());
            }*/

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
        [HttpGet]
        public ActionResult BlogcuSil(int? blogcuid)
        {
            Blogcular blogcu = null;

            if(blogcuid != null)
            {
                Blogcum4Entities db = new Blogcum4Entities();
                blogcu = db.Blogcular.Where(x => x.BlogcuID == blogcuid).FirstOrDefault();
            }


            return View(blogcu);
        }

        [HttpPost,ActionName("BlogcuSil")]
        public ActionResult BlogcuSilOnay(int? blogcuid)
        {
            if (blogcuid != null)
            {
                Blogcular blogcu = db.Blogcular.Where(x => x.BlogcuID == blogcuid).FirstOrDefault();

                db.Blogcular.Remove(blogcu);
                db.SaveChanges();
            }

            return RedirectToAction("Blogcular","Admin");
            
        }
        // GET
        public ActionResult BlogSil(int? blogid)
        {
            Bloglar blog = null;

            if (blogid != null)
            {
                blog = db.Bloglar.Where(x => x.BlogYazisiID == blogid).FirstOrDefault();
            }
            return View(blog);
        }
        [HttpPost,ActionName("BlogSil")]
        public ActionResult BlogSilOnay(int? blogid)
        {
            if (blogid != null)
            {
                Bloglar blog = db.Bloglar.Where(x => x.BlogYazisiID == blogid).FirstOrDefault();

                db.Bloglar.Remove(blog);
                db.SaveChanges();
            }

            return RedirectToAction("Bloglar", "Admin");
            
        }
        // GET
        public ActionResult IletiSil(int? iletiid)
        {
            Iletisim ileti = null;

            if (iletiid != null)
            {
                ileti = db.Iletisim.Where(x => x.IletiID == iletiid).FirstOrDefault();
            }
            return View(ileti);
        }
        [HttpPost,ActionName("IletiSil")]
        public ActionResult IletiSilOnay(int? iletiid)
        {
            if (iletiid != null)
            {
                Iletisim ileti = db.Iletisim.Where(x => x.IletiID == iletiid).FirstOrDefault();

                db.Iletisim.Remove(ileti);
                db.SaveChanges();
            }

            return RedirectToAction("Iletisim", "Admin");
            
        }

        // GET
        public ActionResult YorumSil(int? yorumid)
        {
            Yorumlar yorum = null;

            if (yorumid != null)
            {
                yorum = db.Yorumlar.Where(x => x.YorumID == yorumid).FirstOrDefault();
            }
            return View(yorum);
        }

        [HttpPost, ActionName("YorumSil")]
        public ActionResult YorumSilOnay(int? yorumid)
        {
            if (yorumid != null)
            {
                Yorumlar yorum = db.Yorumlar.Where(x => x.YorumID == yorumid).FirstOrDefault();

                db.Yorumlar.Remove(yorum);
                db.SaveChanges();
            }

            return RedirectToAction("Yorumlar", "Admin");

        }

    }
        }





      

        


    