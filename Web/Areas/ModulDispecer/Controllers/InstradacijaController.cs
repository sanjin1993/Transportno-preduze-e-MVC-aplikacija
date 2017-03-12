using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Web.Areas.ModulDispecer.Models;
using PagedList;
using System.Net;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Controllers
{
    public class InstradacijaController : Controller
    {

         TPContext ctx = new TPContext();
            public List<SelectListItem> statusi = new List<SelectListItem>();

        // GET: ModulDispecer/Instradacija
        public ActionResult Index(string  datumOd, string  datumDo, string vozac, int ? statusId, string currentFilter, int page=1)

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



            if (!String.IsNullOrEmpty(vozac))
            {
               vozac= vozac.Replace(" ", string.Empty);
            }
        

            
            var Model = ctx.Instradacije
                        .Where(x =>( ((x.Vozac.Ime + x.Vozac.Prezime).Contains(vozac) || String.IsNullOrEmpty(vozac))
                               && x.Datum>=DateFrom && x.Datum<=DateTo
                               && (!statusId.HasValue || x.StatusInstradacijeId==statusId) && x.IsDeleted==false
                              )
                                
                            )
                        .OrderByDescending(x => x.Datum)
                        .Select(x => new InstradacijeIndexVM()
                        {
                            ImePrezime=x.Vozac.Ime + " "+x.Vozac.Prezime,
                            Datum =  x.Datum,
                            Status = x.StatusInstradacije.Naziv,
                            Vozilo=x.Vozilo.RegistarskeOznake,
                            PrikljucnoVozilo=x.PrikljucnoVozilo.RegistarskeOznake,
                            DrzavaOd=x.Dispozicija.DrzavaOd.Naziv.ToString(),
                            DravaDo= x.Dispozicija.DrzavaDo.Naziv.ToString(),
                            InstradacijaId=x.InstradacijaId

                        }).ToPagedList(page, 15);



            statusi = getStatusi();
            ViewData["statusInstradacije"] = statusi;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_InstradacijeList",Model);
            }

           
            return View(Model);
        }


        public ActionResult DispozicijaInstradacije(int dispozicijaId, int page=1)
        {
            var Model = ctx.Instradacije
                        .Where(x => x.DispozicijaId==dispozicijaId && x.IsDeleted==false

                            )
                        .OrderByDescending(x => x.Datum)
                        .Select(x => new InstradacijeIndexVM()
                        {
                            ImePrezime = x.Vozac.Ime + " " + x.Vozac.Prezime,
                            Datum = x.Datum,
                            Status = x.StatusInstradacije.Naziv,
                            Vozilo = x.Vozilo.RegistarskeOznake,
                            PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
                            InstradacijaId=x.InstradacijaId,
                            DispozicijaId=x.DispozicijaId

                        }).ToPagedList(page, 15);

            
            return View(Model);
        }

        [HttpGet]
        public ActionResult Dodaj(int dispozicijaId)
        {
            InstradacijaDetaljnoVM Model = new InstradacijaDetaljnoVM
            {
                DispozicijaId = dispozicijaId,

            };




            getInstradacijaFormInfo(ref Model,dispozicijaId);


          
            return View (Model);
        }

        [HttpPost]
        public ActionResult Dodaj(InstradacijaDetaljnoVM instradacija)
        {
            if (ModelState.IsValid)
            {
                Instradacija i = new Instradacija
                {
                    UlaznaCarinarnica = instradacija.UlaznaCarinarnica,
                    IzlaznaCarinarnica = instradacija.IzlaznaCarinarnica,
                    VozacId = instradacija.VozacId,
                    StatusInstradacijeId = 1,
                    Datum = DateTime.Now,
                    VoziloId = instradacija.VoziloId,
                    PrikljucnoVoziloId = instradacija.PrikljucnoVoziloId,
                    DispozicijaId = instradacija.DispozicijaId,
                    IsDeleted=false
                    

                };

                ctx.Instradacije.Add(i);
                ctx.SaveChanges();

                return RedirectToAction("DispozicijaInstradacije", new { dispozicijaId = instradacija.DispozicijaId });
            }
            else
            {
                getInstradacijaFormInfo(ref instradacija, instradacija.DispozicijaId);
                return View("Dodaj",instradacija);
            }
            
        }

        public ActionResult Details(int instradacijaId)
        {

            InstradacijaDetaljnoVM Model = ctx.Instradacije.Where(x => x.InstradacijaId == instradacijaId)
                .Select(x => new InstradacijaDetaljnoVM() {
                    InstradacijaId=x.InstradacijaId,
                    Datum = x.Datum,
                    ImePrezime = x.Vozac.Ime + " " + x.Vozac.Prezime,
                    DrzavaOd = x.Dispozicija.DrzavaOd.Naziv,
                    DravaDo = x.Dispozicija.DrzavaDo.Naziv,
                    UlaznaCarinarnica = x.UlaznaCarinarnica,
                    IzlaznaCarinarnica = x.IzlaznaCarinarnica,
                    Vozilo = x.Vozilo.RegistarskeOznake,
                    PrikljucnoVozilo = x.PrikljucnoVozilo.RegistarskeOznake,
                    Status = x.StatusInstradacije.Naziv,
                    Troskovi = ctx.Troskovi.ToList(),
                    DispozicijaInfo = new DispozicijaDetaljnoVM
                    {
                        DrzavaOd=x.Dispozicija.DrzavaOd.Naziv,
                        DrzavaDo = x.Dispozicija.DrzavaDo.Naziv,
                        RowGuid = x.Dispozicija.RowGuid

                    }


                }).FirstOrDefault();



            return View(Model);
               

        }
        private List<SelectListItem> getStatusi()
        {
            List<SelectListItem> statusi = new List<SelectListItem>();
            statusi.AddRange(ctx.StatusInstradacije
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList());
            return statusi;

        }

        [HttpGet]
        public ActionResult Uredi(int instradacijaId)
        {
            InstradacijaDetaljnoVM instradacija = ctx.Instradacije.Where(x => x.InstradacijaId == instradacijaId)
                .Select(
                x => new InstradacijaDetaljnoVM()
                {
                    InstradacijaId=x.InstradacijaId,
                    VoziloId = x.VoziloId,
                    PrikljucnoVoziloId = x.PrikljucnoVoziloId,
                    VozacId = x.VozacId,
                    StatusInstradacijeId=x.StatusInstradacijeId,
                    UlaznaCarinarnica=x.UlaznaCarinarnica,
                    IzlaznaCarinarnica=x.IzlaznaCarinarnica

                }

                ).FirstOrDefault();
            getInstradacijaFormInfo(ref instradacija, instradacijaId);
            instradacija.Statusi = new List<SelectListItem>();
            instradacija.Statusi = ctx.StatusInstradacije.Select(x => new SelectListItem()
            {
                Value=x.Id.ToString(),
                Text=x.Naziv
            }).ToList();

            instradacija.Statusi.Find(x => x.Value == instradacija.StatusInstradacijeId.ToString()).Selected = true;
            return PartialView("_Uredi", instradacija);

        }

        [HttpPost]
        public JsonResult Uredi(InstradacijaDetaljnoVM i, int instradacijaId)
        {

            Instradacija instradacija = ctx.Instradacije.Find(instradacijaId);

            if (ModelState.IsValid)
            {

                instradacija.VoziloId = i.VoziloId;
                instradacija.VozacId = i.VozacId;
                instradacija.PrikljucnoVoziloId = i.PrikljucnoVoziloId;
                instradacija.UlaznaCarinarnica = i.UlaznaCarinarnica;
                instradacija.IzlaznaCarinarnica = i.IzlaznaCarinarnica;
                instradacija.StatusInstradacijeId = i.StatusInstradacijeId;

                ctx.SaveChanges();
                return Json(new { Url = "Details?instradacijaId="+ instradacijaId });
               
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

        public ActionResult Obrisi(int instradacijaId)
        {

            Instradacija i = ctx.Instradacije.Find(instradacijaId);
            if (i != null)
            {
                i.IsDeleted = true;
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        private void getInstradacijaFormInfo(ref InstradacijaDetaljnoVM Model, int id)
        {
            Model.Vozaci = ctx.Vozaci.Select(
              x => new SelectListItem()
              {
                  Value = x.ZaposlenikId.ToString(),
                  Text = x.Ime + " " + x.Prezime


              }
              )
             .ToList();


            Model.PrikljucnaVozila = ctx.PrikljucnaVozila
                .Where(x => x.StatusVozilaId != 4)
                .Select(
                x => new SelectListItem()
                {
                    Text = x.RegistarskeOznake,
                    Value = x.Id.ToString()
                }
                )
                .ToList();


            Model.Vozila = ctx.Vozila
                .Where(x => x.StatusVozilaId != 4)
                .Select(
                x => new SelectListItem()
                {
                    Value = x.VoziloId.ToString(),
                    Text = x.RegistarskeOznake
                }).ToList();

            Model.DispozicijaInfo= ctx.Dispozicije.Where(x => x.DispozicijaId == id)
                .Select(
                x => new DispozicijaDetaljnoVM()
                {
                    RowGuid = x.RowGuid,
                    NazivKlijenta = x.Klijent.Naziv

                })
                .FirstOrDefault();
        }

    }


}