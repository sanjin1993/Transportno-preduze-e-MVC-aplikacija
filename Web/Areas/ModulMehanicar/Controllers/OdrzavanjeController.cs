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
    [Authorize(Roles = "mehaničar")]
    public class OdrzavanjeController : Controller
    {
        TPContext ctx = new TPContext();
        // GET: ModulMehanicar/Odrzavanje
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Prikazi(int page = 1)
        {


            var Model = ctx.Odrzavanja.OrderBy(x => x.Datum).Select
                (x => new OdrzavanjePrikaziVM
                {

                    OdrzavanjeId = x.OdrzavanjeId,
                    Troskovi = x.Troskovi,
                    Datum = x.Datum,

                    Tip_Odrzavanja = x.TipOdrzavanja.Naziv,
                    StatusVozila =  x.Vozilo.StatusVozila.Naziv
                }).ToPagedList(page, 15);

            return View("Prikazi", Model);
        }

        public ActionResult Uredi(int odrzavanjeId)
        {
            OdrzavanjeDetaljnoVM odrzavanje = ctx.Odrzavanja.Where(x => x.OdrzavanjeId == odrzavanjeId)
                .Select(
                x => new OdrzavanjeDetaljnoVM()
                {
                    OdrzavanjeId = odrzavanjeId,
                    VoziloId = (int)x.VoziloId,
                    PrikljucnoVoziloId = (int)x.PrikljucnoVoziloId,
                    TipOdrzavanjaId = x.TipOdrzavanjaId,
                    Kilometraza = x.Kilometraza,
                    Detaljno = x.Detaljno,
                    Datum = x.Datum,
                    Troskovi = x.Troskovi


                }).FirstOrDefault();

            odrzavanje.TipOdrzavanjaStavke = BindTipOdrzavanja();
            odrzavanje.VoziloStavke = BindVozila();
            odrzavanje.PrikljucnoVoziloStavke = BindPrikljucnaVozila();

            return PartialView("_Uredi", odrzavanje);
        }
        public ActionResult UrediStatus(OdrzavanjeDetaljnoVM o)
        {
            OdrzavanjeDetaljnoVM model = new OdrzavanjeDetaljnoVM();
            Vozilo Vozilo = ctx.Vozila.Where(x => x.VoziloId == o.VoziloId).FirstOrDefault();

            model = ctx.Odrzavanja.Select(x => new OdrzavanjeDetaljnoVM()
            {
                OdrzavanjeId = x.OdrzavanjeId,
                VoziloId =(int) x.VoziloId,
                PrikljucnoVoziloId = (int)x.PrikljucnoVoziloId,
                TipOdrzavanjaId = x.TipOdrzavanjaId,
                Kilometraza = x.Kilometraza,
                Detaljno = x.Detaljno,
                Datum = x.Datum,
                Troskovi = x.Troskovi,
                Voziloo = x.Vozilo
            }).FirstOrDefault();


            model.Statusi = BindStatusi();


            return PartialView("Statusi", model);
        }

        public ActionResult ZakljuciStatus(int statusID, OdrzavanjeDetaljnoVM vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    Model.Statusi = BindStatusi();
            //    return View("Statusi", Model);
            //}
            Vozilo vozilo;


            if (vm.Voziloo.VoziloId != 0)

            {
                vozilo = ctx.Vozila.Where(x => x.VoziloId == vm.Voziloo.VoziloId).FirstOrDefault();

                vozilo.StatusVozilaId = statusID;

            }



            ctx.SaveChanges();
            return Redirect("/ModulMehanicar/Odrzavanje/Details?odrzavanjeID=" + vm.OdrzavanjeId);
        }

        private List<SelectListItem> BindStatusi()
        {
            var statusi = new List<SelectListItem>();

            statusi.Add(new SelectListItem { Value = null, Text = "Odaberite status vozila" });
            statusi.AddRange(ctx.StatusiVozila.Select(x => new SelectListItem { Value = x.StatusVozilaId.ToString(), Text = x.Naziv }).ToList());
            return statusi;
        }

      
        public ActionResult DodajOdrzavanje()
        {
            OdrzavanjeDetaljnoVM Model = new OdrzavanjeDetaljnoVM();
            Model.Datum = DateTime.Now;

            Model.VoziloStavke = BindVozila();

            Model.PrikljucnoVoziloStavke = BindPrikljucnaVozila();

            Model.TipOdrzavanjaStavke = BindTipOdrzavanja();

            return View("_Dodaj", Model);
        }

        public ActionResult Snimi(OdrzavanjeDetaljnoVM Model)
        {
            if (!ModelState.IsValid)
            {
                Model.VoziloStavke = BindVozila();
                Model.PrikljucnoVoziloStavke = BindPrikljucnaVozila();
                Model.TipOdrzavanjaStavke = BindTipOdrzavanja();
                return View("_Dodaj", Model);
            }
            Odrzavanje odrzavanje;

            if (Model.OdrzavanjeId == 0)
            {
                odrzavanje = new Odrzavanje();
                ctx.Odrzavanja.Add(odrzavanje);
            }
            else
            {
                odrzavanje = ctx.Odrzavanja.Where(x => x.OdrzavanjeId == Model.OdrzavanjeId).FirstOrDefault();
            }

            odrzavanje.Troskovi = Model.Troskovi;
            odrzavanje.Kilometraza = Model.Kilometraza;
            odrzavanje.Detaljno = Model.Detaljno;
            odrzavanje.Datum = Model.Datum;
            odrzavanje.TipOdrzavanjaId = Model.TipOdrzavanjaId;

            odrzavanje.VoziloId = Model.VoziloId;

            odrzavanje.PrikljucnoVoziloId = Model.PrikljucnoVoziloId;

            ctx.SaveChanges();

            return RedirectToAction("Details", new { odrzavanjeID = odrzavanje.OdrzavanjeId });

        }

        public ActionResult Details(int odrzavanjeID)
        {
            var Model = ctx.Odrzavanja.Where(x => x.OdrzavanjeId == odrzavanjeID)
                .Select(
                x => new OdrzavanjeDetaljnoVM()
                {
                    OdrzavanjeId = x.OdrzavanjeId,
                    Troskovi = x.Troskovi,

                    Datum = x.Datum,

                    Tip_Odrzavanja = x.TipOdrzavanja.Naziv,
                    Kilometraza = x.Kilometraza,
                    Detaljno = x.Detaljno,
                    PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
                    Vozilo = x.Vozilo.RegistarskeOznake,
                    StatusVozila =  x.Vozilo.StatusVozila.Naziv

                }
                ).FirstOrDefault();
            if (Request.IsAjaxRequest())
            {
                return PartialView(Model);
            }

            return View(Model);
        }

        private List<SelectListItem> BindTipOdrzavanja()
        {
            var tipovi = new List<SelectListItem>();

            tipovi.Add(new SelectListItem { Value = null, Text = "Odaberite tip pdrzavanja" });
            tipovi.AddRange(ctx.TipoviOdrzavanja.Select(x => new SelectListItem { Value = x.TipOdrzavanjaId.ToString(), Text = x.Naziv }).ToList());
            return tipovi;
        }



        private List<SelectListItem> BindPrikljucnaVozila()
        {
            var prikljucnavozila = new List<SelectListItem>();

            prikljucnavozila.Add(new SelectListItem { Value = null, Text = "Odaberite prikljucno vozilo" });

            prikljucnavozila.AddRange(ctx.PrikljucnaVozila.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TipPrikljucnog.Naziv + " - " + x.BrojSasije }).ToList());
            return prikljucnavozila;
        }

        private List<SelectListItem> BindVozila()
        {
            var vozila = new List<SelectListItem>();
            vozila.Add(new SelectListItem { Value = null, Text = "Odaberite vozilo" });

            vozila.AddRange(ctx.Vozila.Select(x => new SelectListItem { Value = x.VoziloId.ToString(), Text = x.Proizvodzac + " - " + x.Model }).ToList());

            return vozila;
        }
    }
}