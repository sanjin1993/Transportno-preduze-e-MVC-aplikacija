using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Web.Areas.ModulDispecer.Models;
using Web.Areas.ModulLogistika.Models;

namespace Web.Areas.ModulLogistika.Controllers
{
    [Authorize(Roles = "logističar")]
    public class PrikljucnaVozilaController : Controller
    {
        // GET: ModulLogistika/PrikljucnaVozila

        TPContext ctx = new TPContext();
        public List<SelectListItem> statusi = new List<SelectListItem>();
        public List<SelectListItem> tipovi = new List<SelectListItem>();

        #region PrikljucnoVozilo
        public ActionResult Index(DateTime? datumRegistracije, int? brojOsovina, int? tipPrikljucnogId, int? statusVozilaId, int page = 1)
        {

            if (datumRegistracije == null)
            {
                datumRegistracije = DateTime.MaxValue;
            }

            var Model = ctx.PrikljucnaVozila.Where(x => (!brojOsovina.HasValue || x.BrojOsovina <= brojOsovina)
                                              && (!tipPrikljucnogId.HasValue || x.TipPrikljucnogId == tipPrikljucnogId) &&
                                              (!statusVozilaId.HasValue || x.StatusVozilaId == statusVozilaId) &&
                                              (x.DatumRegistracije < datumRegistracije)
            ).OrderBy(x => x.DatumRegistracije).Select(x => new PrikljucnaVozilaIndexVM()
            {
                prikljucnoVoziloId = x.Id,
                brojSasije = x.BrojSasije,
                regOznake = x.RegistarskeOznake,
                brojOsovina = x.BrojOsovina,
                datumRegistracije = x.DatumRegistracije,
                statusVozila = x.StatusVozila.Naziv,
                tipPrikljucnog = x.TipPrikljucnog.Naziv
            }).ToPagedList(page, 10);

            statusi = getStatusi();
            ViewData["statusVozila"] = statusi;

            tipovi = getTipovi();
            ViewData["tipVozila"] = tipovi;

            var brojVozila = ctx.PrikljucnaVozila.Count(x => (!brojOsovina.HasValue || x.BrojOsovina == brojOsovina)
                                                             && (!tipPrikljucnogId.HasValue ||
                                                                 x.TipPrikljucnogId == tipPrikljucnogId) &&
                                                             (!statusVozilaId.HasValue ||
                                                              x.StatusVozilaId == statusVozilaId) &&
                                                             (x.DatumRegistracije < datumRegistracije));

            ViewData["brojVozila"] = brojVozila;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PrikljucnaVozilaList", Model);
            }

            return View(Model);
        }

        public async Task<ActionResult> Details(int prikljucnoVoziloID)
        {
            var Model = await ctx.PrikljucnaVozila
                .Where(x => x.Id == prikljucnoVoziloID)
                .Select(
                    x => new PrikljucnoVoziloDetaljnoVM()
                    {
                        prikljucnoVoziloId = x.Id,
                        brojSasije = x.BrojSasije,
                        regOznake = x.RegistarskeOznake,
                        brojOsovina = x.BrojOsovina,
                        statusVozila = x.StatusVozila.Naziv,
                        tipPrikljucnog = x.TipPrikljucnog.Naziv,
                        cijena = x.Cijena,
                        datumRegistracije = x.DatumRegistracije,
                        instradacije = x.Instradacije.ToList(),
                        nosivost = x.Nosivost,
                        visina = x.Visina,
                        duzina = x.Duzina,
                        tezina = x.Tezina,
                        odrzavanja = x.Odrzavanja.Select(o => new OdrzavanjeVM
                        {
                            odrzavanjeId = o.OdrzavanjeId,
                            datum = o.Datum,
                            kilometraza = o.Kilometraza,
                            troskovi = o.Troskovi,
                            tipOdrzavanjaId = o.TipOdrzavanjaId
                        }).ToList()

                    }).FirstOrDefaultAsync();
            return View(Model);
        }

        public ActionResult Dodaj()
        {
            var Model = new PrikljucnoVoziloDetaljnoVM
            {
                TipoviPrikljcnogVozila = getTipovi(),
                StatusiVozila = getStatusi(),
                datumRegistracije = DateTime.Now
            };
            return View(Model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Dodaj(PrikljucnoVoziloDetaljnoVM vm)
        {
            if (ModelState.IsValid)
            {
                PrikljucnoVozilo vozilo = new PrikljucnoVozilo
                {
                    BrojSasije = vm.brojSasije,
                    RegistarskeOznake = vm.regOznake,
                    BrojOsovina = vm.brojOsovina,
                    Visina = vm.visina,
                    Nosivost = vm.nosivost,
                    Cijena = vm.cijena,
                    Duzina = vm.duzina,
                    Tezina = vm.tezina,
                    DatumRegistracije = vm.datumRegistracije.Value,
                    TipPrikljucnogId = vm.tipPrikljucnogVozilaId,
                    StatusVozilaId = vm.statusVozilaId
                };

                ctx.PrikljucnaVozila.Add(vozilo);
                await ctx.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View("Dodaj", new PrikljucnoVoziloDetaljnoVM()
            {
                TipoviPrikljcnogVozila = getTipovi(),
                StatusiVozila = getStatusi(),
                datumRegistracije = DateTime.Now
            });

        }

        [HttpGet]
        public async Task<ActionResult> Uredi(int prikljucnovoziloID)
        {
            PrikljucnoVoziloDetaljnoVM Model = await ctx.PrikljucnaVozila.Where(x => x.Id == prikljucnovoziloID)
                .Select(
                    x => new PrikljucnoVoziloDetaljnoVM
                    {
                        prikljucnoVoziloId = x.Id,
                        brojSasije = x.BrojSasije,
                        regOznake = x.RegistarskeOznake,
                        brojOsovina = x.BrojOsovina,
                        cijena = x.Cijena,
                        datumRegistracije = x.DatumRegistracije,
                        nosivost = x.Nosivost,
                        visina = x.Visina,
                        duzina = x.Duzina,
                        tezina = x.Tezina ,
                        statusVozilaId = x.StatusVozilaId,
                        tipPrikljucnogVozilaId =  x.TipPrikljucnogId
                    }).FirstOrDefaultAsync();


            Model.StatusiVozila = getStatusi();
            Model.TipoviPrikljcnogVozila = getTipovi();

            Model.StatusiVozila.Where(x => x.Value == Model.statusVozilaId.ToString()).FirstOrDefault().Selected = true;
            Model.TipoviPrikljcnogVozila.Where(x => x.Value == Model.tipPrikljucnogVozilaId.ToString()).FirstOrDefault().Selected = true;

            return PartialView("_Uredi", Model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Uredi(PrikljucnoVoziloDetaljnoVM vozilo)
        {

            PrikljucnoVozilo v = await ctx.PrikljucnaVozila.FindAsync(vozilo.prikljucnoVoziloId);

            if (!ModelState.IsValid)
            {
                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).Select(x => new ErrorHelper()
                    {
                        Message = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault(),
                        Name = x.Key
                    }
                ).ToList();
                return Json(new { Errors = errors });
            }
            else
            {

                v.BrojSasije = vozilo.brojSasije;
                v.RegistarskeOznake = vozilo.regOznake;
                v.BrojOsovina = vozilo.brojOsovina;
                v.Cijena = vozilo.cijena;
                v.DatumRegistracije = vozilo.datumRegistracije.Value;
                v.Nosivost = vozilo.nosivost;
                v.Visina = vozilo.visina;
                v.Duzina = vozilo.duzina;
                v.Tezina = vozilo.tezina;
                v.StatusVozilaId = vozilo.statusVozilaId;
                v.TipPrikljucnogId = vozilo.tipPrikljucnogVozilaId;

                await ctx.SaveChangesAsync();
                return Json(new { Url = "Details?prikljucnoVoziloID=" + v.Id });
            }
        }

        public async Task<ActionResult> Obrisi(int prikljucnovoziloID)
        {
            PrikljucnoVozilo prikljucno = await ctx.PrikljucnaVozila.FindAsync(prikljucnovoziloID);

            List<Instradacija> instradacije = ctx.Instradacije.Where(x => x.PrikljucnoVoziloId == prikljucno.Id).ToList();
            List<Odrzavanje> odrzavanja = ctx.Odrzavanja.Where(x => x.PrikljucnoVoziloId == prikljucno.Id).ToList();

            if (prikljucno != null)
            {
                //provjeriti cascade delete ali oako radi !!!
                foreach (var inst in instradacije)
                {
                    ctx.Instradacije.Remove(inst);
                }
                foreach (var odrz in odrzavanja)
                {
                    ctx.Odrzavanja.Remove(odrz);
                }
                ctx.PrikljucnaVozila.Remove(prikljucno);
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult OdrzavanjaPrikljucnoVozilo(int id)
        {
            var Model = ctx.Odrzavanja.
                Select(y => new OdrzavanjeVM()
                {
                    odrzavanjeId = y.OdrzavanjeId,
                    datum = y.Datum,
                    kilometraza = y.Kilometraza,
                    troskovi = y.Troskovi,
                    tipOdrzavanja = y.TipOdrzavanja.Naziv,
                    voziloId = y.VoziloId.Value , 
                    priljucno = y.PrikljucnoVoziloId.Value == null ? "ne" : "da" 
                }).ToList();

            ViewData["prikljucnoId"] = id;
            
            return View("OdrzavanjaList", Model);
        }

        public ActionResult DodajPrikljucnoOdrzavanju(int id , int prikljucnoid)
        {
            Odrzavanje odrzavanje = ctx.Odrzavanja.Find(id);
            odrzavanje.PrikljucnoVoziloId = prikljucnoid;
            ctx.Odrzavanja.AddOrUpdate(odrzavanje);
             ctx.SaveChangesAsync();

            return RedirectToAction("OdrzavanjaPrikljucnoVozilo", new { id = prikljucnoid});
        }
        public ActionResult UkloniPrikljucnoSaOdrzavanja(int id , int prikljucnoid)
        {
            Odrzavanje odrzavanje = ctx.Odrzavanja.Find(id);
            odrzavanje.PrikljucnoVoziloId = null;
            ctx.Odrzavanja.AddOrUpdate(odrzavanje);
            ctx.SaveChanges();

            return RedirectToAction("OdrzavanjaPrikljucnoVozilo", new { id = prikljucnoid });
        }

        private List<SelectListItem> getStatusi()
        {
            List<SelectListItem> statusi = new List<SelectListItem>();

            statusi.AddRange(ctx.StatusiVozila
                .Select(x => new SelectListItem { Value = x.StatusVozilaId.ToString(), Text = x.Naziv }).ToList());

            return statusi;
        }
        private List<SelectListItem> getTipovi()
        {
            List<SelectListItem> tipovi = new List<SelectListItem>();

            tipovi.AddRange(ctx.TipoviPrikljucnog
                .Select(x => new SelectListItem { Value = x.TipPrikljucnogId.ToString(), Text = x.Naziv }).ToList());

            return tipovi;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ctx.Dispose();
        }

        #endregion
    }
}