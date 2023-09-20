using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c = new Context();
        public ActionResult Index()
        {
            var deg1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deg1;

            var deg2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deg2;

            var deg3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = deg3;

            var deg4 = (from x in c.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deg4;


            var yap = c.Yapilacaks.ToList();
            return View(yap);
        }
    }
}