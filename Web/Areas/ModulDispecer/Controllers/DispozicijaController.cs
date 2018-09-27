using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Data.DAL;
using System.Data.Entity;
using TransportnoPreduzece.Web.Areas.ModulDispecer.Models;
using System.Net;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Controllers
{
    public class DispozicijaController : Controller
    {
        // GET: ModulDispecer/Dispozicija
        TPContext ctx = new TPContext();
        #region Dispozicija
        [Authorize (Roles ="dispečer")]
        public ActionResult Index(DateTime? datumOd, DateTime? datumDo,string brojRacuna, string nazivKlijenta, int page = 1)
        {


            if (datumDo == null) { 

            datumOd = DateTime.MinValue;
            datumDo = DateTime.MaxValue;

            }






            var Model = ctx.Dispozicije
                        .Where(x => (x.DatumDispozicije > datumOd && x.DatumDispozicije<datumDo) &&
                        (String.IsNullOrEmpty(nazivKlijenta) || x.Klijent.Naziv.Contains(nazivKlijenta))
                        && (String.IsNullOrEmpty(brojRacuna) || x.RowGuid.ToString().Contains(brojRacuna)) && x.IsDeleted==false)
                                .OrderBy(x => x.DatumIspostave)
                                .Select(x => new DispozicijeIndexVM()
                                {

                                    DispozicijaId = x.DispozicijaId,
                                    Cijena = x.Cijena,
                                    DatumDispozicije = x.DatumDispozicije,
                                    DatumIspostave = x.DatumIspostave,
                                    Primalac = x.Primalac,
                                    NazivKlijenta = x.Klijent.Naziv,
                                    RacunGuid=x.RowGuid,
                                    DrzavaOd=x.DrzavaOd.Naziv,
                                    DrzavaDo=x.DrzavaDo.Naziv




                                }

                                ).ToPagedList(page, 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_DispozicijeList", Model);
            }

            return View(Model);
        }
        [Authorize(Roles = "dispečer")]
        [HttpGet]
        public ActionResult Dodaj()
        {

            DispozicijaDetaljnoVM Model = new DispozicijaDetaljnoVM();
            
            Model.TipKolicine = ctx.KolicinaTipovi.ToList();
            Model.Drzave = ctx.Drzave.ToList();
            Model.Klijenti = ctx.Klijenti.Select(
                x=> new KlijentVM()
                {
                    KlijentId=x.KlijentId,
                    Naziv=x.Naziv,
                        
             
                }
                ).ToList();

            Model.DatumDispozicije = null;


            return View(Model);
        }

        [Authorize(Roles = "dispečer")]
        [HttpPost]
        public ActionResult Dodaj(DispozicijaDetaljnoVM d)
        {
           
            d.DatumDispozicije = DateTime.Now;
            
            if (ModelState.IsValid) {
                Dispozicija dispozicija = new Dispozicija()
                {
                    KlijentId=d.KlijentId,
                    Primalac = d.Primalac,
                    DrzavaOdId = d.DrzavaOdId,
                    AdresaOd = d.AdresaOd,
                    DrzavaDoId = d.DrzavaDoId,
                    AdresaDo = d.AdresaDo,
                    DatumDispozicije = DateTime.Now,
                    DatumIspostave = d.DatumIspostave,
                    DatumPlacanja = d.DatumPlacanja,
                    DodatneInformacije = d.DodatneInformacije,
                    Cijena = d.Cijena,
                    RowGuid = Guid.NewGuid().ToString().Substring(0,15)
                 
                    



                };

                dispozicija.Stavke = new List<Stavka>();

                foreach (StavkaVM item in d.Stavke)
                {
                    dispozicija.Stavke.Add(new Stavka
                    {
                        DispozicijaId = dispozicija.DispozicijaId,
                        KolicinaTipId=item.KolicinaTipId,
                        Kolicina=item.Kolicina,
                        Naziv=item.Naziv

                    });
                }


                ctx.Dispozicije.Add(dispozicija);
                ctx.SaveChanges();
                return RedirectToAction("Dodaj","Dispozicija");
            }

          
            else
            {
                         
                return View(d);
            }
           
            
        }

       


        [Authorize(Roles = "dispečer")]
        public ActionResult Details (int dispozicijaId)
        {

            var Model = ctx.Dispozicije
                .Where(x => x.DispozicijaId == dispozicijaId)
                .Select(
                x => new DispozicijaDetaljnoVM()
                {
                    DispozicijaId = x.DispozicijaId,
                    Cijena = x.Cijena,
                    DatumDispozicije = x.DatumDispozicije,
                    DatumIspostave = x.DatumIspostave,
                    Primalac = x.Primalac,
                    NazivKlijenta = x.Klijent.Naziv,
                    RacunGuid = x.RowGuid,
                    BrojInstradacija = x.Instradacije.Count,
                    Stavke = x.Stavke.Select(y=> new StavkaVM
                    {
                        Naziv=y.Naziv,
                        Kolicina=y.Kolicina,
                        KolicinaTipId=y.KolicinaTipId,
                        StavkaId=y.StavkaId  
                    }).ToList(),
                    Instradacije = x.Instradacije.ToList(),
                    DatumPlacanja = x.DatumPlacanja,
                    DodatneInformacije = x.DodatneInformacije,
                    AdresaOd = x.AdresaOd,
                    AdresaDo=x.AdresaDo,
                    DrzavaOd=x.DrzavaOd.Naziv,
                    DrzavaDo=x.DrzavaDo.Naziv

                }).FirstOrDefault();
            return View(Model);


        }
        [Authorize(Roles = "dispečer")]
        [HttpGet]
        public ActionResult Uredi(int dispozicijaId)
        {
            DispozicijaDetaljnoVM d = ctx.Dispozicije.Where(x => x.DispozicijaId == dispozicijaId)
                                        .Select(
                                            x=> new DispozicijaDetaljnoVM()
                                            {
                                                DispozicijaId=x.DispozicijaId,
                                                KlijentId = x.KlijentId,
                                                Primalac = x.Primalac,
                                                DrzavaOdId = x.DrzavaOdId,
                                                AdresaOd = x.AdresaOd,
                                                DrzavaDoId = x.DrzavaDoId,
                                                AdresaDo = x.AdresaDo,
                                                DatumIspostave = x.DatumIspostave,
                                                DatumPlacanja = x.DatumPlacanja,
                                                DodatneInformacije = x.DodatneInformacije,
                                                Cijena = x.Cijena,
                                                RowGuid=x.RowGuid,
                                                
                                            }
                                            ).FirstOrDefault();
            d.DrzaveIndex = new List<SelectListItem>();
            d.DrzaveIndex = GetDrzave();

            return PartialView("_Uredi",d);

        }
        [Authorize(Roles = "dispečer")]
        [HttpPost]
        public JsonResult Uredi(DispozicijaDetaljnoVM dispozicija,int dispozicijaId, int klijentId)
        {

            Dispozicija d = ctx.Dispozicije.Find(dispozicijaId);
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
               
                d.KlijentId = klijentId;
                d.Primalac = dispozicija.Primalac;
                d.DrzavaOdId = dispozicija.DrzavaOdId;
                d.AdresaOd = dispozicija.AdresaOd;
                d.DrzavaDoId = dispozicija.DrzavaDoId;
                d.AdresaDo = dispozicija.AdresaDo;
                d.DatumIspostave = dispozicija.DatumIspostave;
                d.DatumPlacanja = dispozicija.DatumPlacanja;
                d.DodatneInformacije = dispozicija.DodatneInformacije;
                d.Cijena = dispozicija.Cijena;
               

                ctx.SaveChanges();
                return Json(new { Url = "Details?dispozicijaId=" + d.DispozicijaId });
            }



        }

        [HttpGet]
        public ActionResult Stavke(int id)
        {

            var Model = ctx.Stavke.Where(x => x.DispozicijaId == id).
                Select(y => new StavkaVM
                {
                    StavkaId=y.StavkaId,
                    KolicinaTipId=y.KolicinaTipId,
                    Naziv=y.Naziv,
                    Kolicina=y.Kolicina,
                    TipKolicine=y.KolicinaTip.Naziv
                }).ToList();

           

            return View("_Stavke",Model);
        }

        [HttpGet]
        public ActionResult UrediStavku(int id, int ? dispozicijaId)
        {

            if (dispozicijaId == null)
            {
                var Model = ctx.Stavke.Where(x => x.StavkaId == id).Select(y => new StavkaVM
                {
                    DispozicijaId = y.DispozicijaId,
                    Naziv = y.Naziv,
                    StavkaId = y.StavkaId,
                    Kolicina = y.Kolicina,
                    KolicinaTipId = y.KolicinaTipId,
                    TipKolicine = y.KolicinaTip.Naziv,
                    TipoviKolicine = ctx.KolicinaTipovi.Select(z => new SelectListItem
                    {
                        Value = z.KolicinaTipId.ToString(),
                        Text = z.Naziv
                    }).ToList()
                }).FirstOrDefault();

                return View("_UrediStavku", Model);
            }

            else
            {
                var Model = new StavkaVM
                {
                    DispozicijaId=dispozicijaId.Value,
                    TipoviKolicine = ctx.KolicinaTipovi.Select(z => new SelectListItem
                    {
                        Value = z.KolicinaTipId.ToString(),
                        Text = z.Naziv
                    }).ToList()
                };

                return View("_DodajStavku", Model);

            }
           
        }

        [HttpPost]
        public ActionResult SnimiStavku(StavkaVM s)
        {

            if (s.StavkaId != 0)
            {
                if (ModelState.IsValid)
                {
                    Stavka stavka = ctx.Stavke.Find(s.StavkaId);
                    stavka.Kolicina = s.Kolicina;
                    stavka.KolicinaTipId = s.KolicinaTipId;
                    stavka.Naziv = s.Naziv;
                    ctx.SaveChanges();

                    return RedirectToAction("Stavke", new { id = s.DispozicijaId });
                }
                else
                {
                    s.TipoviKolicine = ctx.KolicinaTipovi.Select(y => new SelectListItem
                    {
                        Value = y.KolicinaTipId.ToString(),
                        Text = y.Naziv
                    }).ToList();
                    return View("_UrediStavku", s);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    ctx.Stavke.Add(new Stavka
                    {
                        DispozicijaId = s.DispozicijaId,
                        Naziv = s.Naziv,
                        Kolicina = s.Kolicina,
                        KolicinaTipId = s.KolicinaTipId
                    });
                    ctx.SaveChanges();
                    return RedirectToAction("Stavke", new { id = s.DispozicijaId });

                }
                else
                {
                    s.TipoviKolicine = ctx.KolicinaTipovi.Select(y => new SelectListItem
                    {
                        Value = y.KolicinaTipId.ToString(),
                        Text = y.Naziv
                    }).ToList();
                    return View("_DodajStavku", s);
                }
            }
           

        }

        [HttpPost]
        public ActionResult ObrisiStavku(int id)
        {
           
            
                Stavka s = ctx.Stavke.Find(id);

                ctx.Stavke.Remove(s);
                ctx.SaveChanges();
            

            return RedirectToAction("Stavke", new { id = s.DispozicijaId });
        }
        public ActionResult Obrisi(int dispozicijaId)
        {

            Dispozicija d = ctx.Dispozicije.Find(dispozicijaId);
            if (d != null)
            {
                d.IsDeleted = true;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

        }

        private List<SelectListItem> GetDrzave()
        {

            List<SelectListItem> drzave = new List<SelectListItem>();

            drzave.AddRange(ctx.Drzave
                .Select(x=> new SelectListItem
                {
                    Value= x.DrzavaId.ToString(),
                    Text= x.Naziv
                })
                );

            return drzave;
            
        }

        #endregion
    }
}
