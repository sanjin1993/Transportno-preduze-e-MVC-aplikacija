using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Web.Areas.ModulDispecer.Models;
using Web.Areas.ModulVozac.Models;

namespace Web.Areas.ModulVozac.Controllers
{
    [Authorize(Roles = "vozač")]
    public class InstradacijeController : Controller
    {

        TPContext ctx = new TPContext();
        // GET: ModulVozac/Instradacije
        public ActionResult Prikazi(string datumOd, string datumDo, int page = 1)

        {

            DateTime DateFrom = DateTime.MinValue;
            DateTime DateTo = DateTime.MaxValue;

            if (!String.IsNullOrEmpty(datumOd))
            {
                DateFrom = Convert.ToDateTime(datumOd);
            }


            if (!String.IsNullOrEmpty(datumDo))
            {
                DateTo = Convert.ToDateTime(datumDo);
            }

            var Model = ctx.Instradacije
                       .Where(x => (
                               x.Datum >= DateFrom && x.Datum <= DateTo
                             && x.IsDeleted == false && x.VozacId == Global.odabraniVozac.ZaposlenikId
                             )

                           )
                       .OrderByDescending(x => x.Datum)
                       .Select(x => new InstradacijePrikaziVM()
                       {

                           Datum = x.Datum,
                           Status = x.StatusInstradacije.Naziv,
                           Vozilo = x.Vozilo.RegistarskeOznake,
                           PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
                           DrzavaOd = x.Dispozicija.DrzavaOd.Naziv.ToString(),
                           DrzavaDo = x.Dispozicija.DrzavaDo.Naziv.ToString(),
                           InstradacijaId = x.InstradacijaId

                       }).ToPagedList(page, 15);

            if (Request.IsAjaxRequest())
            {
                return Json(Model, JsonRequestBehavior.AllowGet);
            }


            return View(Model);
        }
        #region json
        //public JsonResult Search(string datumOd, string datumDo, int page = 1)

        //{

        //    DateTime DateFrom = DateTime.MinValue;
        //    DateTime DateTo = DateTime.MaxValue;

        //    if (!String.IsNullOrEmpty(datumOd))
        //    {
        //        DateFrom = Convert.ToDateTime(datumOd);
        //    }


        //    if (!String.IsNullOrEmpty(datumDo))
        //    {
        //        DateTo = Convert.ToDateTime(datumDo);
        //    }

        //    var Model = ctx.Instradacije
        //               .Where(x => (
        //                       x.Datum >= DateFrom && x.Datum <= DateTo
        //                      && x.StatusInstradacijeId == 1 && x.IsDeleted == false
        //                     )

        //                   )
        //               .OrderByDescending(x => x.Datum)
        //               .Select(x => new InstradacijePrikaziVM()
        //               {

        //                   Datum = x.Datum,
        //                   Status = x.StatusInstradacije.Naziv,
        //                   Vozilo = x.Vozilo.RegistarskeOznake,
        //                   PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
        //                   DrzavaOd = x.Dispozicija.DrzavaOd.Naziv.ToString(),
        //                   DrzavaDo = x.Dispozicija.DrzavaDo.Naziv.ToString(),
        //                   InstradacijaId = x.InstradacijaId

        //               }).ToList();



        //    return Json(Model, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        public ActionResult PrikaziTroskove(int instradacijaId)

        {



            var Model = ctx.Troskovi
                       .Where(x => (x.InstradacijaId == instradacijaId)).Select(x => new TroskoviPrikaziVM()
                       {
                           TipTrosak = x.TipTroska.Naziv,
                           InstradacijaId = x.InstradacijaId,
                           Ukupno = x.Ukupno,
                           TrosakId = x.TrosakId


                       }).ToList();
            ViewBag.Suma = ctx.Troskovi.Where(x => x.InstradacijaId == instradacijaId).Sum(x => x.Ukupno);


            return PartialView("_TroskoviList", Model);
        }

        // GET: ModulVozac/Vozac/Details/5
        public ActionResult Details(int id)
        {
            var Model = ctx.Instradacije.Where(x => x.InstradacijaId == id)
                .Include(x => x.PrikljucnoVozilo)
                .Include(x => x.Vozilo)
                .Select(x =>
                  new InstradacijeDetaljiVM()
                  {
                      InstradacijaId = id,
                      Datum = x.Datum,
                      DispozicijaId = x.Dispozicija.DispozicijaId,
                      UlaznaCarinarnica = x.UlaznaCarinarnica,
                      IzlaznaCarinarnica = x.IzlaznaCarinarnica,
                      StatusInstradacijeId = x.StatusInstradacijeId,
                      PrikljucnoVozilo = x.PrikljucnoVozilo.TipPrikljucnog.Naziv,
                      Vozilo = x.Vozilo.Model,
                      VozacId = x.VozacId,
                      Status = x.StatusInstradacije.Naziv



                  }).FirstOrDefault();

            //Model.Statusi = BindStatusi();

            return View(Model);
        }

        public ActionResult PrikaziKartice(int vozacId, int page = 1)
        {
            var Model = ctx.KarticaVozaci.Where(x => x.VozacId == vozacId).Include(x => x.BenzinskaPumpa)
                .OrderByDescending(x => x.DatumKoristenja)

                .Select(x =>
                  new TrosakBenzinPrikaziVM()
                  {
                      DpdijeljenIznos = (double)ctx.KarticaZaposlenici.Where(y => y.ZaposlenikId == vozacId).FirstOrDefault().Iznos,
                      TrenutniIznos = ctx.KarticaZaposlenici.Where(h => h.ZaposlenikId == vozacId).FirstOrDefault().Kartica.TrenutniIznos,
                      Aktivna = ctx.KarticaZaposlenici.Where(y => y.ZaposlenikId == x.VozacId).FirstOrDefault().Kartica.Aktivna,
                      DatumKoristenja = DateTime.Now,
                      KolicinaLitara = x.KolicinaLitara,
                      UkupanIznos = x.UkupanIznos,
                      VozacId = vozacId,
                      BenzinskaPumpaId = x.BenzinskaPumpaId,
                      BenzinskaPumpa = x.BenzinskaPumpa.Adresa,
                      KarticaVozacId = x.KarticaVozacId


                  }).ToPagedList(page, 15);

            //Model.Statusi = BindStatusi();

            return View("_KarticeList", Model);
        }

        private List<SelectListItem> BindStatusi()
        {
            var statusi = new List<SelectListItem>();

            statusi.Add(new SelectListItem { Value = null, Text = "Odaberite status" });
            statusi.AddRange(ctx.StatusInstradacije.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());
            return statusi;
        }
        [HttpGet]
        public ActionResult Statusi(int instradacijaId)
        {
            InstradacijeDetaljiVM model = new InstradacijeDetaljiVM();


            model = ctx.Instradacije.Where(x => x.InstradacijaId == instradacijaId).Select(x => new InstradacijeDetaljiVM()
            {
                InstradacijaId = x.InstradacijaId,
                PrikljucnoVozilo = x.PrikljucnoVozilo.TipPrikljucnog.Naziv,
                Vozilo = x.Vozilo.Model,
                IzlaznaCarinarnica = x.IzlaznaCarinarnica,
                UlaznaCarinarnica = x.UlaznaCarinarnica,
                Datum = x.Datum,
                StatusInstradacijeId = x.StatusInstradacijeId


            }).FirstOrDefault();



            model.Statusi = BindStatusi();


            return PartialView(model);
        }

        public ActionResult ZakljuciStatus(Instradacija Model, int statusInstradacijeId)
        {

            Instradacija instradacija;


            if (Model.InstradacijaId != 0)

            {
                instradacija = ctx.Instradacije.Where(x => x.InstradacijaId == Model.InstradacijaId).FirstOrDefault();

                instradacija.StatusInstradacijeId = statusInstradacijeId;

            }



            ctx.SaveChanges();
            return RedirectToAction("Details", new { id = Model.InstradacijaId });
        }

        [HttpGet]
        public ActionResult Troskovi(int instradacijaId)
        {

            TroskoviEditVM model = new TroskoviEditVM();


            model = ctx.Troskovi.Select(x => new TroskoviEditVM()
            {
                TipTroskaId = x.TipTroskaId,
                InstradacijaId = instradacijaId,
                Ukupno = 0


            }).FirstOrDefault();



            model.Troskovi = BindTroskovi();


            return PartialView(model);

        }

        [HttpGet]
        public ActionResult TrosakBenzin(int vozacId)
        {

            TrosakBenzinEditVM model = new TrosakBenzinEditVM();


            model = ctx.KarticaVozaci.Select(x => new TrosakBenzinEditVM()
            {
                DpdijeljenIznos = (double)ctx.KarticaZaposlenici.Where(y => y.ZaposlenikId == vozacId).FirstOrDefault().Iznos,
                TrenutniIznos = ctx.KarticaZaposlenici.Where(h => h.ZaposlenikId == vozacId).FirstOrDefault().Kartica.TrenutniIznos,
                Aktivna = ctx.KarticaZaposlenici.Where(y => y.ZaposlenikId == x.VozacId).FirstOrDefault().Kartica.Aktivna,
                DatumKoristenja = DateTime.Now,
                KolicinaLitara = 0,
                UkupanIznos = 0,
                VozacId = vozacId,
                KarticaId = ctx.KarticaZaposlenici.Where(y => y.ZaposlenikId == vozacId).FirstOrDefault().Kartica.KarticaId
            }).FirstOrDefault();


            model.BenzinskePumpe = BindBenzinskePumpe();

            return PartialView(model);

        }

        private List<SelectListItem> BindBenzinskePumpe()
        {

            var benzinske = new List<SelectListItem>();

            benzinske.Add(new SelectListItem { Value = null, Text = "Odaberite benzinsku pumpu" });
            benzinske.AddRange(ctx.BenzinskePumpe.Select(x => new SelectListItem { Value = x.BenzinskaPumpaId.ToString(), Text = x.Adresa }).ToList());
            return benzinske;
        }

        public JsonResult ZakljuciTrosak(TroskoviEditVM model)
        {

            if (ModelState.IsValid)
            {
                Trosak trosak = new Trosak();

                trosak.InstradacijaId = model.InstradacijaId;
                trosak.TipTroskaId = model.TipTroskaId;
                trosak.Ukupno = model.Ukupno;

                ctx.Troskovi.Add(trosak);

                ctx.SaveChanges();
                return Json(new {Url = "Details?id=" + trosak.InstradacijaId});
           
            }
            else
            {

                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).Select(x => new ErrorHelper()
                {
                    Message = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault(),
                    Name = x.Key
                }
            ).ToList();
                return Json(new { Errors = errors });
            }


        }

        public ActionResult ZakljuciBenzinTrosak(TrosakBenzinEditVM model)
        {

            if (ModelState.IsValid)
            {
                KarticaVozac trosak = new KarticaVozac();

                trosak.BenzinskaPumpaId = model.BenzinskaPumpaId;
                trosak.VozacId = model.VozacId;
                trosak.KolicinaLitara = model.KolicinaLitara;
                trosak.DatumKoristenja = DateTime.Now;
                trosak.UkupanIznos = model.UkupanIznos;
                trosak.VozacId = model.VozacId;

                Kartica k = ctx.Kartice.Where(x => x.KarticaId == model.KarticaId).FirstOrDefault();
                if (k.TrenutniIznos > model.UkupanIznos)
                {
                    k.TrenutniIznos = k.TrenutniIznos - (float)model.UkupanIznos;
                    ctx.KarticaVozaci.Add(trosak);



                    ctx.SaveChanges();
                    return RedirectToAction("PrikaziKartice", new { vozacId = model.VozacId });
                }
                else
                {

                    var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).Select(x => new ErrorHelper()
                    {
                        Message = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault(),
                        Name = x.Key
                    }).ToList();
                    return Json(new { Errors = errors }, JsonRequestBehavior.AllowGet);


                }

            }
            else
            {

                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).Select(x => new ErrorHelper()
                {
                    Message = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault(),
                    Name = x.Key
                }).ToList();
                return Json(new { Errors = errors }, JsonRequestBehavior.AllowGet);


            }
        }

        private List<SelectListItem> BindTroskovi()
        {
            var troskovi = new List<SelectListItem>();

            troskovi.Add(new SelectListItem { Value = null, Text = "Odaberite tip troška" });
            troskovi.AddRange(ctx.TipoviTroskova.Select(x => new SelectListItem { Value = x.TipTroskaId.ToString(), Text = x.Naziv }).ToList());
            return troskovi;
        }


        // POST: ModulVozac/Vozac/Delete/5
        public ActionResult ObrisiKarticu(int karticaId)
        {
            KarticaVozac kartica = ctx.KarticaVozaci.Find(karticaId);
            int vozacId = kartica.VozacId;
            ctx.KarticaVozaci.Remove(kartica);
            ctx.SaveChanges();

            return RedirectToAction("PrikaziKartice", new { vozacId = vozacId });

        }

        public ActionResult ObrisiTrosak(int trosakId)
        {
            Trosak trosak = ctx.Troskovi.Find(trosakId);
            int instradacijaID = trosak.InstradacijaId;
            ctx.Troskovi.Remove(trosak);
            ctx.SaveChanges();

            return RedirectToAction("Details", new { id = instradacijaID });

        }
    }
}