using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context c = new Context();
        // GET: Urun
        public ActionResult Index(string p, int sayfa = 1)
        {
            var degerler = from x in c.Uruns select x;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(y => y.UrunAd.Contains(p));
            }

            return View(degerler.ToList().ToPagedList(sayfa, 6));
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            c.Uruns.Add(urun);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var p = c.Uruns.Find(id);
            p.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            var p = c.Uruns.Find(id);
            return View("UrunGetir", p);
        }

        public ActionResult UrunGuncelle(Urun urun)
        {
            var p = c.Uruns.Find(urun.UrunID);
            p.AlisFiyat = urun.AlisFiyat;
            p.Durum = urun.Durum;
            p.Kategoriid = urun.Kategoriid;
            p.Marka = urun.Marka;
            p.SatisFiyat = urun.SatisFiyat;
            p.Stok = urun.Stok;
            p.UrunAd = urun.UrunAd;
            p.UrunGorsel = urun.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi()
        {
            var deg = c.Uruns.ToList();
            return View(deg);
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {

            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;

            var deger2 = c.Uruns.Find(id);
            ViewBag.dgr2 = deger2.UrunID;

            ViewBag.dgr3 = deger2.SatisFiyat;

            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}