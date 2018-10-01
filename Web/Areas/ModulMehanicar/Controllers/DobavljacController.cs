using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Data.Models;
using Web.Areas.ModulMehanicar.Models;

namespace Web.Areas.ModulMehanicar.Controllers
{
    public class DobavljacController : Controller
    {
        TPContext ctx = new TPContext();
        // GET: ModulMehanicar/Dobavljac
      

        public ActionResult Prikazi(int page = 1)
        {
            var Model = ctx.Dobavljaci.OrderBy(d => d.Naziv).Select(x => new DobavljacPrikaziVM()
            {
                DobavljacId = x.Id,
                Naziv = x.Naziv,
                Adresa = x.Adresa,
                Telefon = x.Telefon,
            }).ToPagedList(page, 15);


            return View(Model);
        }

        public ActionResult Uredi(int dobavljacId)
        {
            DobavljacDetaljnoVM Model = ctx.Dobavljaci.Where(x => x.Id == dobavljacId)
                .Select(x => new DobavljacDetaljnoVM()
                {
                    DobavljacId = x.Id,
                    Naziv = x.Naziv,
                    Adresa = x.Adresa,
                    Telefon = x.Telefon

                }).FirstOrDefault();

            return View("Uredi", Model);
        }

        public ActionResult Obrisi(int dobavljacId)
        {
            Dobavljac dobavljac = ctx.Dobavljaci.Find(dobavljacId);
            ctx.Dobavljaci.Remove(dobavljac);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");

        }
        //public ActionResult Dodaj()
        //{
        //    DobavljacDetaljnoVM Model = new DobavljacDetaljnoVM();

        //    return View("Uredi", Model);

        //    ctx.SaveChanges();

        //    return RedirectToAction("Prikazi");

        //}

        public ActionResult Snimi(DobavljacDetaljnoVM Model)
        {
            if (!ModelState.IsValid)
            {
                return View("Uredi", Model);
            }
            Dobavljac dobavljac;

            if (Model.DobavljacId == 0)
            {
                dobavljac = new Dobavljac();
                ctx.Dobavljaci.Add(dobavljac);
            }

            else
            {
                dobavljac = ctx.Dobavljaci.Where(x => x.Id == Model.DobavljacId)
                    .FirstOrDefault();
            }

            dobavljac.Naziv = Model.Naziv;
            dobavljac.Adresa = Model.Adresa;
            dobavljac.Telefon = Model.Telefon;

            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        [HttpGet]
        public ActionResult NabavkaStavke(int dobavljacID, int page = 1)
        {

            var Model = ctx.Nabavke.OrderBy(x => x.Datum).Where(y => y.DobavljacId == dobavljacID).Select(z =>
                  new NabavkaPrikaziVM()
                  {
                      NabavkaId = z.Id,
                      //Sifra = z.Sifra,
                      DatumNabavke = z.Datum,
                      DobavljacNaziv = z.Dobavljac.Naziv,
                      BrojStavki = ctx.StavkeNabavke.Where(y => y.NabavkaId == z.Id).Count()
                  }).ToPagedList(page, 15);
            return PartialView("_NabavkaStavke", Model);
        }
    }
}