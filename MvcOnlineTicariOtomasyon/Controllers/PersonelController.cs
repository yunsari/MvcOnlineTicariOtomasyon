using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();

        public ActionResult Index()
        {
            var per = c.Personels.Where(x => x.Durum == true).ToList();
            return View(per);
        }

        [HttpGet]
        public ActionResult YeniPersonel()
        {

            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult YeniPersonel(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            personel.Durum = true;
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(int id)
        {
            var per = c.Personels.Find(id);
            per.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            var u = c.Personels.Find(id);
            return View("PersonelGetir", u);
        }

        public ActionResult PersonelGuncelle(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            var per = c.Personels.Find(personel.PersonelID);
            per.PersonelAd = personel.PersonelAd;
            per.PersonelSoyad = personel.PersonelSoyad;
            per.PersonelGorsel = personel.PersonelGorsel;
            per.Departmanid = personel.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult PersonelSatis(int id)
        {
            var deg = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var per = c.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(deg);
        }

        public ActionResult PersonelListe()
        {
            var per = c.Personels.ToList();
            return View(per);
        }
    }
}