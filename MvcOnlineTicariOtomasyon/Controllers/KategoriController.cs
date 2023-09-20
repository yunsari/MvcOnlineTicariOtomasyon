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
    public class KategoriController : Controller
    {

        Context c = new Context();

        // GET: Kategori
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);
            var deg = (string)Session["KullanıcıAd"];
            var adminNick = c.Admins.Where(x => x.KullanıcıAd == deg).ToList();
            ViewBag.adminNick = deg;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var cat = c.Kategoris.Find(id);
            c.Kategoris.Remove(cat);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var cat = c.Kategoris.Find(id);
            return View("KategoriGetir", cat);
        }

        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var cat = c.Kategoris.Find(kategori.KategoriID);
            cat.KategoriAd = kategori.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Deneme()
        {
            Class2 cs = new Class2();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }

        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.UrunID.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}