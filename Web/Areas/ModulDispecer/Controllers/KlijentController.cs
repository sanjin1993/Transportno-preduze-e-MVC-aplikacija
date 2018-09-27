using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Web.Areas.ModulDispecer.Models;
using PagedList;
using System.Web.Script.Serialization;
using TransportnoPreduzece.Data;
namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Controllers
{
    public class KlijentController : Controller
    {
        // GET: ModulDispecer/Klijent
        TPContext ctx = new TPContext();
        public List<SelectListItem> drzave = new List<SelectListItem>();

        public ActionResult Index(string nazivKlijenta, int? drzavaId, string telefon, string adresa,string currentFilter, int page=1)
        {

            
        




            var Model = ctx.Klijenti
            .Where(x => (String.IsNullOrEmpty(nazivKlijenta) || x.Naziv.Contains(nazivKlijenta)) &&
                        (!drzavaId.HasValue || x.DrzavaId == drzavaId) &&
                        (String.IsNullOrEmpty(telefon) || x.Telefon.Contains(telefon)) &&
                        (String.IsNullOrEmpty(adresa) || x.Adresa.Contains(adresa)) && x.IsDeleted == false
                            )
                          .OrderByDescending(r=>r.Dispozicije.Where(d => d.KlijentId == r.KlijentId).OrderByDescending(o => o.DatumDispozicije).Select(i => i.DatumDispozicije).FirstOrDefault())
                          .Select(x => new KlijentiIndexVM()
                          {
                              KlijentId = x.KlijentId,
                              Naziv = x.Naziv,
                              Mail = x.Mail,
                              Telefon = x.Telefon,
                              Adresa = x.Adresa,
                              Fax = x.Fax,
                              Drzava = x.Drzava.Naziv,
                              DrzavaId = x.DrzavaId,
                              PosljednjaDispozicija = ctx.Dispozicije.Where(d => d.KlijentId == x.KlijentId).OrderByDescending(o => o.DatumDispozicije).Select(i => i.DatumDispozicije).FirstOrDefault()



                          }


                    ).ToPagedList(page,15);


            drzave = GetDrzave();

            ViewData["drzave"] = drzave;
  

            if (Request.IsAjaxRequest())
            {
                return PartialView("_KlijentList", Model);
            }


            return View(Model);
        }




        [HttpPost]
        public JsonResult Vozac(string name)
        {
            List<Vozac> vozaci = ctx.Vozaci
                                            .Select(

                x => new Vozac()
                {
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    ZaposlenikId = x.ZaposlenikId
                }).ToList();

            return Json(vozaci, JsonRequestBehavior.AllowGet);
        }
        

        [HttpGet]
        public ActionResult Dodaj()
        {

            var Model = new KlijentProfilVM
            {
                Drzave = GetDrzave()
            };

            return View(Model);
        }

        [HttpPost]
        public ActionResult Dodaj(KlijentProfilVM k)
        {

          

            if (ModelState.IsValid)
            {
                Klijent klijent = new Klijent()
                {
                    Naziv = k.Naziv,
                    DrzavaId = k.DrzavaId,
                    Adresa = k.Adresa,
                    Telefon = k.Telefon,
                    Fax = k.Fax,
                    Mail = k.Mail,
                    IdBroj = k.IDBroj

                };
                ctx.Klijenti.Add(klijent);
                ctx.SaveChanges();
               return RedirectToAction("Dodaj");
            }

            k.Drzave = GetDrzave();

            return  View(k);
        }


        public ActionResult Profil(int? klijentId, int page=1)
        {

            if(klijentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }





            KlijentProfilVM Model = ctx.Klijenti
                  .Where(x => x.KlijentId == klijentId && x.IsDeleted==false)
                  .Select(
                    x => new KlijentProfilVM()
                    {
                        KlijentId = x.KlijentId,
                        Naziv = x.Naziv,
                        Mail = x.Mail,
                        Telefon = x.Telefon,
                        Adresa = x.Adresa,
                        Fax = x.Fax,
                        Drzava = x.Drzava.Naziv,
                        DrzavaId = x.DrzavaId,

                        
                   }
                ).FirstOrDefault();

           

            return View(Model);
           

        }

        public ActionResult KlijentDispozicije(int klijentId=26, int page = 1)
        {


            var Model = ctx.Dispozicije

          .Where(y => y.KlijentId == klijentId)
          .OrderByDescending(d=>d.DatumDispozicije)
       .Select(y => new DispozicijeIndexVM()
       {


           DatumDispozicije = y.DatumDispozicije,
           Primalac = y.Primalac,
           Cijena = y.Cijena,
             RacunGuid=y.RowGuid,
           DispozicijaId=y.DispozicijaId



       }).ToPagedList(page, 2);



            return PartialView("_ProfilDispozicije",Model);

        }

        [HttpGet]
        public ActionResult Uredi(int klijentId)
        {


            KlijentProfilVM klijent =ctx.Klijenti.Where(x=>x.KlijentId==klijentId)
                .Select(
                x=> new KlijentProfilVM()
                {
                    KlijentId = x.KlijentId,
                    Naziv = x.Naziv,
                    Mail = x.Mail,
                    Telefon = x.Telefon,
                    Adresa = x.Adresa,
                    Fax = x.Fax,
                    Drzava = x.Drzava.Naziv,
                    DrzavaId = x.DrzavaId
                }).FirstOrDefault();

            klijent.Drzave = GetDrzave();

            klijent.Drzave.Where(x => x.Value == klijent.DrzavaId.ToString()).FirstOrDefault().Selected = true;
            
            return PartialView("_Uredi",klijent);
        }


        public ActionResult Obrisi(int klijentId)
        {
            Klijent k = ctx.Klijenti.Find(klijentId);

            if (k != null)
            {
                k.IsDeleted = true;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        [HttpPost]
        public JsonResult Uredi(KlijentProfilVM k, int klijentId)
        {
            Klijent klijent = ctx.Klijenti.Find(klijentId);

           

        
            if (!ModelState.IsValid) {
                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).Select(x => new ErrorHelper()
                {
                    Message = x.Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault(),
                    Name = x.Key
                }
            ).ToList();
                return Json(new { Errors = errors });
            }
            else {
                klijent.Naziv = k.Naziv;
                klijent.Mail = k.Mail;
                klijent.Telefon = k.Telefon;
                klijent.Adresa = k.Adresa;
                klijent.Fax = k.Fax;
                klijent.DrzavaId = k.DrzavaId;

                ctx.SaveChanges();
                return Json(new { Url = "Profil?klijentId=" + k.KlijentId });
            }
        }
        private List<SelectListItem> GetDrzave()
        {

            List<SelectListItem> Drzave = new List<SelectListItem>();
           
            Drzave.AddRange(ctx.Drzave
                .Select(x=> new SelectListItem {Value=x.DrzavaId.ToString(), Text=x.Kod }).ToList());

            return Drzave;
    }

        
    }
}