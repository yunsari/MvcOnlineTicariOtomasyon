using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();

        public ActionResult Index()
        {
            var deg = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(deg);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniCari(Cariler cariler)
        {
            cariler.Durum = true;
            c.Carilers.Add(cariler);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cari = c.Carilers.Find(id);
            cari.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cariler cariler)
        {

            var cari = c.Carilers.Find(cariler.CariID);
            cari.CariAd = cariler.CariAd;
            cari.CariSoyAd = cariler.CariSoyAd;
            cari.CariSehir = cariler.CariSehir;
            cari.CariMail = cariler.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatis(int id)
        {
            var deg = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.CariID == id).Select(x => x.CariAd + " " + x.CariSoyAd).FirstOrDefault();
            ViewBag.cari = cr;
            return View(deg);
        }
    }
}