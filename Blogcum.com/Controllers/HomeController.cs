using Blogcum.Models;
using Blogcum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }

        public ActionResult Kayit()
        {
            return View();
        }
        public ActionResult AdminSayfasi()
        {

            return RedirectToActionPermanent("Index","Admin");
        }
    }

    }
