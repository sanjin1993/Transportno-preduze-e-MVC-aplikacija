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
    public class VozacController : Controller
    {
        // GET: ModulLogistika/Vozac

        TPContext ctx = new TPContext();
        public List<SelectListItem> statusi = new List<SelectListItem>();
        
        #region Vozaci
        public ActionResult Index(DateTime? datumOd, DateTime? datumDo, string brojVozacke, int? statusId, int page = 1)
        {

            if (datumDo == null)
            {
                datumOd = DateTime.MinValue;
                datumDo = DateTime.MaxValue;
            }

            var Model = ctx.Vozaci.Where(x => (String.IsNullOrEmpty(brojVozacke) || x.BrojVozacke.Contains(brojVozacke))
                                              && (!statusId.HasValue || x.StatusVozacaId == statusId) &&
                                              (x.RokVazenjaDozvole > datumOd && x.RokVazenjaDozvole < datumDo)
            ).OrderBy(x => x.RokVazenjaDozvole).Select(x => new VozacIndexVM()
            {
                vozacId = x.ZaposlenikId,
                ime = x.Ime,
                prezime = x.Prezime,
                statusVozaca = x.StatusVozaca.Naziv,
                brojVozacke = x.BrojVozacke,
                datumVazenjaVozacke = x.RokVazenjaDozvole,
                datumRaskida = x.DatumOtkaza.Value
            }).ToPagedList(page, 10);

            var brojVozaca = ctx.Vozaci.Count(x =>
                (String.IsNullOrEmpty(brojVozacke) || x.BrojVozacke.Contains(brojVozacke))
                && (!statusId.HasValue || x.StatusVozacaId == statusId) &&
                (x.RokVazenjaDozvole > datumOd && x.RokVazenjaDozvole < datumDo));

            statusi = getStatusi();
            ViewData["statusVozaca"] = statusi;

            ViewData["brojVozaca"] = brojVozaca;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_VozaciList", Model);
            }

            return View(Model);
        }
       



        public async Task<ActionResult> Detalji(int vozacID)
        {
            var Model = await ctx.Vozaci.Where(x => x.ZaposlenikId == vozacID).Select(
                    x => new VozacDetaljnoVM()
                    {
                        ADRlicenca = x.ADRLicenca ? "da" : "ne",
                        datumLjekarskog = x.DatumLjekarskog,
                        JMBG = x.JMBG,
                        Email = x.Email,
                        datumZaposlenja = x.DatumZaposlenja,
                        datumVazenjaVozacke = x.RokVazenjaDozvole,
                        brojVozacke = x.BrojVozacke,
                        ime = x.Ime,
                        prezime = x.Prezime,
                        statusVozaca = x.StatusVozaca.Naziv,
                        vozacId = x.ZaposlenikId
                    }).FirstOrDefaultAsync();

            return View(Model);
        }



        private List<SelectListItem> getStatusi()
        {
            List<SelectListItem> statusi = new List<SelectListItem>();

            statusi.AddRange(ctx.StatusiVozaca
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());

            return statusi;
        }



        [HttpGet]
        public ActionResult Dodaj()
        {
            var Model = new VozacDetaljnoVM()
            {
                statusi = getStatusi(),
                datumZaposlenja = DateTime.Now,
                datumLjekarskog = DateTime.Now,
                datumVazenjaVozacke = DateTime.Now
            };
            return View(Model);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Dodaj(VozacDetaljnoVM vm)
        {
            if (ModelState.IsValid)
            {
                Vozac vozac = new Vozac
                {
                    ADRLicenca = vm.ADRlicenca.Equals("da") ? true : false,
                    DatumLjekarskog = vm.datumLjekarskog,
                    JMBG = vm.JMBG,
                    Email = vm.Email,
                    DatumZaposlenja = vm.datumZaposlenja,
                    RokVazenjaDozvole = vm.datumVazenjaVozacke,
                    BrojVozacke = vm.brojVozacke,
                    Ime = vm.ime,
                    Prezime = vm.prezime,
                    StatusVozacaId = vm.statusId,
                    UlogaId = 2
                };

                ctx.Vozaci.Add(vozac);
                await ctx.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Dodaj", new VozacDetaljnoVM()
            {
                statusi = getStatusi(),
                datumZaposlenja = DateTime.Now,
                datumLjekarskog = DateTime.Now,
                datumVazenjaVozacke = DateTime.Now
            });
        }

        public async Task<ActionResult> Uredi(int vozacID)
        {
            VozacDetaljnoVM Model = await ctx.Vozaci.Where(x => x.ZaposlenikId == vozacID)
                .Select(
                    x => new VozacDetaljnoVM
                    {
                        ADRlicenca = x.ADRLicenca ? "da" : "ne",
                        datumLjekarskog = x.DatumLjekarskog,
                        JMBG = x.JMBG,
                        Email = x.Email,
                        datumZaposlenja = x.DatumZaposlenja,
                        datumVazenjaVozacke = x.RokVazenjaDozvole,
                        brojVozacke = x.BrojVozacke,
                        ime = x.Ime,
                        prezime = x.Prezime,
                        statusVozaca = x.StatusVozaca.Naziv,
                        vozacId = x.ZaposlenikId,
                        statusId = x.StatusVozacaId
                    }).FirstOrDefaultAsync();


            Model.statusi = getStatusi();

            Model.statusi.Where(x => x.Value == Model.statusId.ToString()).FirstOrDefault().Selected = true;

            return PartialView("_Uredi", Model);
        }



        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Uredi(VozacDetaljnoVM vozac)
        {

            Vozac v = await ctx.Vozaci.FindAsync(vozac.vozacId);

            //if (!ModelState.IsValid || !vozac.ADRlicenca.Equals("da")  || !vozac.ADRlicenca.Equals("ne"))
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
                v.ADRLicenca = vozac.ADRlicenca.Equals("da") ? true : false;
                v.DatumLjekarskog = vozac.datumLjekarskog;
                v.JMBG = vozac.JMBG;
                v.Email = vozac.Email;
                v.DatumZaposlenja = vozac.datumZaposlenja;
                v.RokVazenjaDozvole = vozac.datumVazenjaVozacke;
                v.BrojVozacke = vozac.brojVozacke;
                v.Ime = vozac.ime;
                v.Prezime = vozac.prezime;
                v.ZaposlenikId = vozac.vozacId;
                v.StatusVozacaId = vozac.statusId;

                await ctx.SaveChangesAsync();
                return Json(new { Url = "Detalji?vozacID=" + v.ZaposlenikId });
            }
        }


        public async Task<ActionResult> Obrisi(int vozacID)
        {
            Vozac vozac = await ctx.Vozaci.FindAsync(vozacID);

            List<Instradacija> instradacije = ctx.Instradacije.Where(x => x.VozacId == vozac.ZaposlenikId).ToList();
            List<KarticaVozac> kartice = ctx.KarticaVozaci.Where(x => x.VozacId == vozac.ZaposlenikId).ToList();
            List<Odsustvo> odsustva = ctx.Odsustva.Where(x => x.ZaposlenikId == vozac.ZaposlenikId).ToList();
            if (vozac != null)
            {
                //provjeriti cascade delete ali oako radi !!!
                foreach (var inst in instradacije)
                {
                    ctx.Instradacije.Remove(inst);
                }
                foreach (var kar in kartice)
                {
                    ctx.KarticaVozaci.Remove(kar);
                }
                foreach (var ods in odsustva)
                {
                    ctx.Odsustva.Remove(ods);
                }
                ctx.Vozaci.Remove(vozac);
                await ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        //VozacKartice
        public ActionResult VozacKartice(int vozacid)
        {
            var Model = ctx.KarticaVozaci.Where(x => x.VozacId == vozacid)
                .Select(x => new VozacKarticaVM
                {
                    karticaId = x.KarticaVozacId,
                    datumKoristenja = x.DatumKoristenja,
                    kolicinaLitara = x.KolicinaLitara,
                    ukupniIznos = x.UkupanIznos,
                    adresaPumpe = x.BenzinskaPumpa.Adresa
                }).ToList();

            return View("_VozacKartice", Model);
        }


        [HttpGet]
        public ActionResult UrediKarticu(int id, int? vozacID)
        {

            if (vozacID == null)
            {
                var Model = ctx.KarticaVozaci.Where(x => x.KarticaVozacId == id).Select(x => new VozacKarticaVM
                {
                    vozacId = x.VozacId,
                    karticaId = x.KarticaVozacId,
                    datumKoristenja = x.DatumKoristenja,
                    kolicinaLitara = x.KolicinaLitara,
                    ukupniIznos = x.UkupanIznos,
                    benzinskaId = x.BenzinskaPumpaId,
                    benzinske = ctx.BenzinskePumpe.Select(z => new SelectListItem
                    {
                        Value = z.BenzinskaPumpaId.ToString(),
                        Text = z.Adresa
                    }).ToList()
                }).FirstOrDefault();

                return View("_UrediKarticu", Model);
            }

            else
            {
                var Model = new VozacKarticaVM
                {
                    vozacId = vozacID.Value,
                    benzinske = ctx.BenzinskePumpe.Select(z => new SelectListItem
                    {
                        Value = z.BenzinskaPumpaId.ToString(),
                        Text = z.Adresa
                    }).ToList()
                };

                return View("_DodajKarticu", Model);

            }
        }


        [HttpPost]
        public ActionResult SnimiKarticu(VozacKarticaVM s)
        {

            if (s.karticaId != 0)
            {
                if (ModelState.IsValid)
                {
                    KarticaVozac kartica = ctx.KarticaVozaci.Find(s.karticaId);

                    kartica.KolicinaLitara = s.kolicinaLitara;
                    kartica.BenzinskaPumpaId = s.benzinskaId;
                    kartica.DatumKoristenja = s.datumKoristenja;
                    kartica.UkupanIznos = s.ukupniIznos;
                    ctx.SaveChanges();

                    return RedirectToAction("VozacKartice", new { vozacid = s.vozacId });
                }
                else
                {

                    s.benzinske = ctx.BenzinskePumpe.Select(z => new SelectListItem
                    {
                        Value = z.BenzinskaPumpaId.ToString(),
                        Text = z.Adresa
                    }).ToList();

                    return View("_UrediKarticu", s);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ctx.KarticaVozaci.Add(new KarticaVozac()
                    {
                        VozacId = s.vozacId,
                        DatumKoristenja = s.datumKoristenja,
                        KolicinaLitara = s.kolicinaLitara,
                        BenzinskaPumpaId = s.benzinskaId,
                        UkupanIznos = s.ukupniIznos

                    });
                    ctx.SaveChanges();
                    return RedirectToAction("VozacKartice", new { vozacid = s.vozacId });

                }
                else
                {
                    s.benzinske = ctx.BenzinskePumpe.Select(z => new SelectListItem
                    {
                        Value = z.BenzinskaPumpaId.ToString(),
                        Text = z.Adresa
                    }).ToList();
                    return View("_DodajKarticu", s);
                }
            }
        }


        [HttpPost]
        public ActionResult ObrisiKarticu(int id)
        {

            KarticaVozac k = ctx.KarticaVozaci.Find(id);
            ctx.KarticaVozaci.Remove(k);
            ctx.SaveChanges();

            return RedirectToAction("VozacKartice", new { id = k.VozacId });
        }


        //Vozac odsustva
        public ActionResult VozacOdsustva(int vozacid)
        {
            var Model = ctx.Odsustva.Where(x => x.ZaposlenikId == vozacid)
                .Select(x => new VozacOdsustvaVM
                {
                    odsustvoId = x.OdsustvoId,
                    datumOd = x.DatumOd,
                    datumDo = x.DatumDo,
                    tipOdsustva = x.TipOdsustva.Naziv

                }).ToList();
            return View("_VozacOdsustva", Model);
        }

        [HttpGet]
        public ActionResult UrediOdsustvo(int id, int? vozacID)
        {
            if (vozacID == null)
            {
                var Model = ctx.Odsustva.Where(x => x.OdsustvoId == id).Select(x => new VozacOdsustvaVM
                {
                    vozacId = x.ZaposlenikId,
                    odsustvoId = x.OdsustvoId,
                    datumOd = x.DatumOd,
                    datumDo = x.DatumDo,
                    tipId = x.TipOdsustvaId,
                    tipoviOdsustva = ctx.TipoviOdsustva.Select(z => new SelectListItem
                    {
                        Value = z.TipOdsustvaId.ToString(),
                        Text = z.Naziv
                    }).ToList()

                }).FirstOrDefault();

                return View("_UrediOdsustvo", Model);
            }
            else
            {
                var Model = new VozacOdsustvaVM
                {
                    vozacId = vozacID.Value,
                    tipoviOdsustva = ctx.TipoviOdsustva.Select(z => new SelectListItem
                    {
                        Value = z.TipOdsustvaId.ToString(),
                        Text = z.Naziv
                    }).ToList()
                };

                return View("_DodajOdsustvo", Model);

            }
        }

       

        [HttpPost]
        public ActionResult SnimiOdsustvo(VozacOdsustvaVM s)
        {

            if (s.odsustvoId != 0)
            {
                if (ModelState.IsValid)
                {
                    Odsustvo odsustvo = ctx.Odsustva.Find(s.odsustvoId);

                    odsustvo.DatumOd = s.datumOd;
                    odsustvo.DatumDo = s.datumDo;
                    odsustvo.TipOdsustvaId = s.tipId;
                    ctx.SaveChanges();

                    return RedirectToAction("VozacOdsustva", new { vozacid = s.vozacId });
                }
                else
                {

                    s.tipoviOdsustva = ctx.TipoviOdsustva.Select(z => new SelectListItem
                    {
                        Value = z.TipOdsustvaId.ToString(),
                        Text = z.Naziv
                    }).ToList();

                    return View("_UrediOdsustvo", s);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ctx.Odsustva.Add(new Odsustvo()
                    {
                        ZaposlenikId = s.vozacId,
                        TipOdsustvaId = s.tipId,
                        DatumOd = s.datumOd,
                        DatumDo = s.datumDo

                    });
                    ctx.SaveChanges();
                    return RedirectToAction("VozacOdsustva", new { vozacid = s.vozacId });

                }
                else
                {
                    s.tipoviOdsustva = ctx.TipoviOdsustva.Select(z => new SelectListItem
                    {
                        Value = z.TipOdsustvaId.ToString(),
                        Text = z.Naziv
                    }).ToList();
                    return View("_DodajOdsustvo", s);
                }
            }
        }


        [HttpPost]
        public ActionResult ObrisiOdsustvo(int id)
        {
            Odsustvo o = ctx.Odsustva.Find(id);
            ctx.Odsustva.Remove(o);
            ctx.SaveChanges();

            return RedirectToAction("VozacOdsustva", new { id = o.ZaposlenikId });
        }



        //Raskid Ugovora
        public ActionResult Raskid(int vozacID)
        {
            RaskidVM Model = ctx.Vozaci.Where(x => x.ZaposlenikId == vozacID)
                .Select(
                    x => new RaskidVM
                    {
                        datumRaskida = DateTime.Now,
                        vozacId = x.ZaposlenikId,
                        statusId = x.StatusVozacaId
                    }).FirstOrDefault();

            Model.statusi = getStatusi();

            Model.statusi.Where(x => x.Value == Model.statusId.ToString()).FirstOrDefault().Selected = true;

            return PartialView("_Raskid", Model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Raskid(RaskidVM vm)
        {
            Vozac vozac = await ctx.Vozaci.FindAsync(vm.vozacId);
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
            vozac.DatumOtkaza = vm.datumRaskida;
            vozac.StatusVozacaId = vm.statusId;
            await ctx.SaveChangesAsync();
            return Json(new { Url = "Detalji?vozacID=" + vozac.ZaposlenikId });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ctx.Dispose();
        }
        #endregion
    }
}