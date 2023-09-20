using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
            var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamurunsayisi = toplamurunsayisi;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var carifoto = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariGorsel).FirstOrDefault();
            ViewBag.carifoto = carifoto;

            return View(degerler);
        }

        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(degerler);
        }

        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(z => z.MesajID).ToList();
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(degerler);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var mail = (string)Session["CariMail"];
            var k = from x in c.kargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(k.ToList());
        }

        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
            ViewBag.m = mail;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyAd).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(degerler);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1", caribul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                cr.CariGorsel = "/Image/" + dosyaadi + uzanti;
            }
            var cari = c.Carilers.Find(cr.CariID);
            cari.CariAd = cr.CariAd;
            cari.CariSoyAd = cr.CariSoyAd;
            cari.CariSifre = cr.CariSifre;
            cari.CariGorsel = cr.CariGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}