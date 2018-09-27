    using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class VoziloController : Controller
    {
        // GET: ModulLogistika/Vozilo

        TPContext ctx = new TPContext();
        public List<SelectListItem> statusi = new List<SelectListItem>();
        public List<SelectListItem> tipovi = new List<SelectListItem>();

        #region Vozilo
        public ActionResult Index(DateTime? datumRegistracije, string brojReg, int? tipVozilaId, int? statusVozilaId, int page = 1)
        {

            if (datumRegistracije == null)
            {
                datumRegistracije = DateTime.MaxValue;
            }
            var Model = ctx.Vozila.Where(x => (String.IsNullOrEmpty(brojReg) || x.RegistarskeOznake.Contains(brojReg))
                                             && (!tipVozilaId.HasValue || x.TipVozilaId == tipVozilaId) &&
                                             (!statusVozilaId.HasValue || x.StatusVozilaId == statusVozilaId) &&
                                             (x.DatumRegistracije < datumRegistracije)
            ).OrderBy(x => x.DatumRegistracije).Select(x => new VozilaIndexVM()
            {
                voziloId = x.VoziloId,
                brojSasije = x.BrojSasije,
                regOznake = x.RegistarskeOznake,
                proizvodjac = x.Proizvodzac,
                datumRegistracije = x.DatumRegistracije,
                statusVozila = x.StatusVozila.Naziv,
                tipVozila = x.TipVozila.Naziv

            }).ToPagedList(page, 10);

            var brojVozila = ctx.Vozila.Count(x => (String.IsNullOrEmpty(brojReg) || x.RegistarskeOznake.Contains(brojReg))
                                                   && (!tipVozilaId.HasValue || x.TipVozilaId == tipVozilaId) &&
                                                   (!statusVozilaId.HasValue || x.StatusVozilaId == statusVozilaId) &&
                                                   (x.DatumRegistracije < datumRegistracije));

            statusi = getStatusi();
            ViewData["statusVozila"] = statusi;

            tipovi = getTipovi();
            ViewData["tipVozila"] = tipovi;

            ViewData["brojVozila"] = brojVozila;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_VozilaList", Model);
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult Dodaj()
        {
            var Model = new VoziloDetaljnoVM
            {
                TipoviVozila = getTipovi(),
                StatusiVozila = getStatusi(),
                datumRegistracije = DateTime.Now
            };
            return View(Model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Dodaj(VoziloDetaljnoVM vm)
        {
            if (ModelState.IsValid)
            {
                Vozilo vozilo = new Vozilo
                {
                    BrojSasije = vm.brojSasije,
                    RegistarskeOznake = vm.regOznake,
                    Proizvodzac = vm.proizvodjac,
                    Model = vm.model,
                    Nosivost = vm.Nosivost,
                    Cijena = vm.Cijena,
                    Kilometraza = vm.Kilometraza,
                    DatumRegistracije = vm.datumRegistracije.Value,
                    GodinaProizvodnje = vm.godinaProizvodnje,
                    TipVozilaId = vm.tipVozilaId,
                    StatusVozilaId = vm.statusVozilaId
                };

                ctx.Vozila.Add(vozilo);
                await ctx.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Dodaj", new VoziloDetaljnoVM
            {
                TipoviVozila = getTipovi(),
                StatusiVozila = getStatusi(),
                datumRegistracije = DateTime.Now
            });

        }
        public async Task<ActionResult> Details(int voziloID)
        {
            var Model = await ctx.Vozila
                .Where(x => x.VoziloId == voziloID)
                .Select(
                    x => new VoziloDetaljnoVM()
                    {
                        voziloId = x.VoziloId,
                        brojSasije = x.BrojSasije,
                        regOznake = x.RegistarskeOznake,
                        proizvodjac = x.Proizvodzac,
                        statusVozila = x.StatusVozila.Naziv,
                        tipVozila = x.TipVozila.Naziv,
                        Cijena = x.Cijena,
                        datumRegistracije = x.DatumRegistracije,
                        godinaProizvodnje = x.GodinaProizvodnje,
                        Instradacije = x.Instradacije.ToList(),
                        Kilometraza = x.Kilometraza,
                        model = x.Model,
                        Nosivost = x.Nosivost,
                        Odrzavanja = x.Odrzavanje.Select(o => new OdrzavanjeVM
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

        [HttpGet]
        public async Task<ActionResult> Uredi(int voziloID)
        {
            VoziloDetaljnoVM Model = await ctx.Vozila.Where(x => x.VoziloId == voziloID)
                .Select(
                    x => new VoziloDetaljnoVM
                    {
                        voziloId = x.VoziloId,
                        brojSasije = x.BrojSasije,
                        regOznake = x.RegistarskeOznake,
                        proizvodjac = x.Proizvodzac,
                        model = x.Model,
                        Nosivost = x.Nosivost,
                        Cijena = x.Cijena,
                        Kilometraza = x.Kilometraza,
                        datumRegistracije = x.DatumRegistracije,
                        godinaProizvodnje = x.GodinaProizvodnje,
                        tipVozilaId = x.TipVozilaId,
                        statusVozilaId = x.StatusVozilaId
                    }).FirstOrDefaultAsync();


            Model.StatusiVozila = getStatusi();
            Model.TipoviVozila = getTipovi();

            Model.StatusiVozila.Where(x => x.Value == Model.statusVozilaId.ToString()).FirstOrDefault().Selected = true;
            Model.TipoviVozila.Where(x => x.Value == Model.tipVozilaId.ToString()).FirstOrDefault().Selected = true;

            return PartialView("_Uredi", Model);

        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Uredi(VoziloDetaljnoVM vozilo)
        {

            Vozilo v = await ctx.Vozila.FindAsync(vozilo.voziloId);

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
                v.Proizvodzac = vozilo.proizvodjac;
                v.Model = vozilo.model;
                v.Nosivost = vozilo.Nosivost;
                v.Cijena = vozilo.Cijena;
                v.Kilometraza = vozilo.Kilometraza;
                v.DatumRegistracije = vozilo.datumRegistracije.Value;
                v.GodinaProizvodnje = vozilo.godinaProizvodnje;
                v.TipVozilaId = vozilo.tipVozilaId;
                v.StatusVozilaId = vozilo.statusVozilaId;

                await ctx.SaveChangesAsync();
                return Json(new { Url = "Details?voziloID=" + v.VoziloId });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Obrisi(int voziloID)
        {

            Vozilo vozilo = await ctx.Vozila.FindAsync(voziloID);
            List<Instradacija> instradacije = ctx.Instradacije.Where(x => x.VoziloId == vozilo.VoziloId).ToList();
            List<Odrzavanje> odrzavanja = ctx.Odrzavanja.Where(x => x.VoziloId == vozilo.VoziloId).ToList();

            if (vozilo != null)
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
                ctx.Vozila.Remove(vozilo);
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult OdrzavanjaVozila(int id, int? kilometraza, int? troskovi)
        {
            var Model =  ctx.Odrzavanja.Where(x => (x.VoziloId == id) && (!kilometraza.HasValue || x.Kilometraza <= kilometraza) && (!troskovi.HasValue || x.Troskovi <= troskovi)).
                Select(y => new OdrzavanjeVM()
                {
                    odrzavanjeId = y.OdrzavanjeId,
                    datum = y.Datum,
                    kilometraza = y.Kilometraza,
                    troskovi = y.Troskovi,
                    tipOdrzavanja = y.TipOdrzavanja.Naziv,
                    voziloId = y.VoziloId.Value
                }).ToList();

             ViewData["voziloId"] = id;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Odrzavanja", Model);
            }

            return View("OdrzavanjaIndex", Model);
        }

        [HttpGet]
        public ActionResult UrediOdrzavanje(int id, int? voziloID)
        {

            if (voziloID == null)
            {
                var Model = ctx.Odrzavanja.Where(x => x.OdrzavanjeId == id).Select(y => new OdrzavanjeVM()
                {
                    voziloId = y.VoziloId.Value,
                    datum = y.Datum,
                    kilometraza = y.Kilometraza,
                    troskovi = y.Troskovi,
                    tipOdrzavanjaId = y.TipOdrzavanjaId,
                    detaljno = y.Detaljno,
                    odrzavanjeId = y.OdrzavanjeId,
                    tipoviOdrzavanja = ctx.TipoviOdrzavanja.Select(z => new SelectListItem
                    {
                        Value = z.TipOdrzavanjaId.ToString(),
                        Text = z.Naziv
                    }).ToList()
                }).FirstOrDefault();

                return View("_UrediOdrzavanje", Model);
            }

            else
            {
                var Model = new OdrzavanjeVM()
                {
                    voziloId = voziloID.Value,
                    tipoviOdrzavanja = ctx.TipoviOdrzavanja.Select(z => new SelectListItem
                    {
                        Value = z.TipOdrzavanjaId.ToString(),
                        Text = z.Naziv
                    }).ToList()
                };

                return View("_DodajOdrzavanje", Model);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SnimiOdrzavanje(OdrzavanjeVM o)
        {

            if (o.odrzavanjeId != 0)
            {
                if (ModelState.IsValid)
                {
                    Odrzavanje odrzavanje = ctx.Odrzavanja.Find(o.odrzavanjeId);
                    odrzavanje.Datum = o.datum;
                    odrzavanje.Kilometraza = o.kilometraza;
                    odrzavanje.Troskovi = o.troskovi;
                    odrzavanje.Detaljno = o.detaljno;
                    odrzavanje.TipOdrzavanjaId = o.tipOdrzavanjaId;
                    ctx.SaveChanges();

                    return RedirectToAction("OdrzavanjaVozila", new { id = o.voziloId });
                }
                else
                {
                    o.tipoviOdrzavanja = ctx.TipoviOdrzavanja.Select(y => new SelectListItem
                    {
                        Value = y.TipOdrzavanjaId.ToString(),
                        Text = y.Naziv
                    }).ToList();
                    return View("_UrediOdrzavanje", o);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ctx.Odrzavanja.Add(new Odrzavanje
                    {
                        VoziloId = o.voziloId,
                        Datum = o.datum,
                        Kilometraza = o.kilometraza,
                        Troskovi = o.troskovi,
                        Detaljno = o.detaljno,
                        TipOdrzavanjaId = o.tipOdrzavanjaId
                    });
                    ctx.SaveChanges();
                    return RedirectToAction("OdrzavanjaVozila", new { id = o.voziloId });

                }
                else
                {
                    o.tipoviOdrzavanja = ctx.TipoviOdrzavanja.Select(y => new SelectListItem
                    {
                        Value = y.TipOdrzavanjaId.ToString(),
                        Text = y.Naziv
                    }).ToList();
                    return View("_DodajOdrzavanje", o);
                }
            }
        }

        [HttpPost]
        public ActionResult ObrisiOdrzavanje(int id)
        {
            Odrzavanje o = ctx.Odrzavanja.Find(id);

            ctx.Odrzavanja.Remove(o);
            ctx.SaveChanges();

            return RedirectToAction("OdrzavanjaVozila", new { id = o.VoziloId });
        }

        public ActionResult InstradacijeVozila(int voziloID)
        {
            var Model = ctx.Instradacije
                .Where(x => x.VoziloId == voziloID && x.IsDeleted == false
                )
                .OrderByDescending(x => x.Datum)
                .Select(x => new InstradacijeIndexVM()
                {
                    ImePrezime = x.Vozac.Ime + " " + x.Vozac.Prezime,
                    Datum = x.Datum,
                    Status = x.StatusInstradacije.Naziv,
                    Vozilo = x.Vozilo.RegistarskeOznake,
                    PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
                    InstradacijaId = x.InstradacijaId,
                    DispozicijaId = x.DispozicijaId

                }).ToList();

            return View("VoziloInstradacije", Model);
        }
        private List<SelectListItem> getTipovi()
        {
            List<SelectListItem> tipovi = new List<SelectListItem>();

            tipovi.AddRange(ctx.TipoviVozila
                .Select(x => new SelectListItem { Value = x.TipVozilaId.ToString(), Text = x.Naziv }).ToList());

            return tipovi;
        }

        private List<SelectListItem> getStatusi()
        {
            List<SelectListItem> statusi = new List<SelectListItem>();

            statusi.AddRange(ctx.StatusiVozila
                .Select(x => new SelectListItem { Value = x.StatusVozilaId.ToString(), Text = x.Naziv }).ToList());

            return statusi;
        }
        
        public ActionResult Produzi(int voziloID)
        {
            ProduziVM Model = ctx.Vozila.Where(x => x.VoziloId == voziloID)
                .Select(
                    x => new ProduziVM
                    {
                       voziloId = x.VoziloId,
                        datum = x.DatumRegistracije
                    }).FirstOrDefault();

            return PartialView("_Produzi", Model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Produzi(ProduziVM vm)
        {
            Vozilo vozilo = await ctx.Vozila.FindAsync(vm.voziloId);
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
            vozilo.DatumRegistracije = vm.datum;
            await ctx.SaveChangesAsync();
            return Json(new { Url = "Details?voziloID=" + vozilo.VoziloId });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ctx.Dispose();
        }
        #endregion
    }
}