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
    public class NabavkaController : Controller
    {
        TPContext ctx = new TPContext();
        // GET: ModulMehanicar/Nabavka
        public ActionResult Index(int page = 1)
        {

            var Model = ctx.Nabavke.OrderBy(x => x.Datum).Select(x =>
                 new NabavkaPrikaziVM()
                 {
                     NabavkaId = x.Id,
                     Sifra = x.Sifra,
                     DatumNabavke = x.Datum,
                     DobavljacNaziv = x.Dobavljac.Naziv,
                     BrojStavki = ctx.StavkeNabavke.Where(y => y.NabavkaId == x.Id).Count()


                 }).ToPagedList(page, 15);
            return View("Index", Model);



        }



        public ActionResult Details(int? nabavkaId)
        {
            var Model = ctx.Nabavke.Where(z => z.Id == nabavkaId)
                .Select(x =>
                  new NabavkaDetaljnoVM()
                  {
                      NabavkaId = x.Id,
                      Sifra = x.Sifra,
                      DatumNabavke = x.Datum,
                      DobavljacNaziv = x.Dobavljac.Naziv,
                      BrojStavki = ctx.StavkeNabavke.Where(y => y.NabavkaId == x.Id).Count(),
                      Stavke = ctx.StavkeNabavke.Where(z => z.NabavkaId == nabavkaId).Select(s => new StavkaNabavkaVM()
                      {
                          NabavkaStavkaId = s.Id,
                          Naziv = s.Naziv,
                          Cijena = s.Cijena,
                          NabavkaId = nabavkaId.Value

                      }).ToList()


                  }).FirstOrDefault();

            return View("Details", Model);
        }
        public ActionResult DodajNabavku()
        {

            NabavkaDetaljnoVM Model = new NabavkaDetaljnoVM();

            Model.DobavljaciStavke = BindDobavljaci();
            Model.nabavke = new List<NabavkaStavka>();

            return View("DodajNabavku", Model);
        }

        private List<SelectListItem> BindDobavljaci()
        {
            var dobavljaci = new List<SelectListItem>();
            dobavljaci.Add(new SelectListItem { Value = null, Text = "Odaberite dobavljaca" });
            dobavljaci.AddRange(ctx.Dobavljaci.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());
            return dobavljaci;
        }

        public ActionResult Snimi(NabavkaDetaljnoVM nabavka)
        {
            Nabavka n = new Nabavka
            {
                Id = nabavka.NabavkaId,
                Datum = nabavka.DatumNabavke,
                Sifra = nabavka.Sifra,
                DobavljacId = nabavka.DobavljacId,

            };
            n.Stavke = new List<NabavkaStavka>();

            int NabavkaStavkaId = 0;

            foreach (NabavkaStavka item in nabavka.nabavke)
            {
                NabavkaStavka stavka = new NabavkaStavka
                {
                    Naziv = item.Naziv,
                    Cijena = item.Cijena,
                    NabavkaId = item.NabavkaId,
                    Id = NabavkaStavkaId--
                };
                nabavka.nabavke.Add(stavka);
            }
            ctx.Nabavke.Add(n);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SnimiS(NabavkaDetaljnoVM nabavka)
        {
            Nabavka n = new Nabavka
            {
                Id = nabavka.NabavkaId,
                Datum = nabavka.DatumNabavke,
                Sifra = nabavka.Sifra,
                DobavljacId = nabavka.DobavljacId,

            };
            n.Stavke = new List<NabavkaStavka>();


            ctx.Nabavke.Add(n);
            ctx.SaveChanges();

            return PartialView("_DodajStavku");
        }
        public ActionResult DodajStavku(int? nabavkaId)
        {

            StavkaNabavkaVM Model = new StavkaNabavkaVM();
            Model.NabavkaId = (int)nabavkaId;
            Model.Cijena = 0;



            return PartialView("_DodajStavku", Model);
        }
        public ActionResult UrediStavku(int stavkaId, int? nabavkaId)
        {

            StavkaNabavkaVM Model = ctx.StavkeNabavke.Where(y => y.Id == stavkaId && y.NabavkaId == nabavkaId).Select(x => new StavkaNabavkaVM()
            {
                Naziv = x.Naziv,
                Cijena = x.Cijena,
                NabavkaStavkaId = stavkaId,
                NabavkaId = nabavkaId.Value

            }).FirstOrDefault();

            ctx.SaveChanges();



            return PartialView("_UrediStavku", Model);
        }

        public ActionResult Lista(int? nabavkaId, string naziv)
        {
            ViewBag.id = nabavkaId;
            if (naziv == null)
            {
                var Model = ctx.StavkeNabavke.Where(x => x.NabavkaId == nabavkaId)

                   .Select(y => new StavkaNabavkaVM
                   {


                       NabavkaStavkaId = y.Id,
                       Cijena = y.Cijena,
                       Naziv = y.Naziv
                   }).ToList();
                return PartialView("_ListStavke", Model);
            }

            else
            {
                ViewBag.id = nabavkaId;
                var Model = ctx.StavkeNabavke.Where(x => x.NabavkaId == nabavkaId && x.Naziv.Contains(naziv))
               .Select(y => new StavkaNabavkaVM
               {


                   NabavkaStavkaId = y.Id,
                   Cijena = y.Cijena,
                   Naziv = y.Naziv
               }).ToList();
                return PartialView("_ListStavke", Model);
            }



        }

        public ActionResult Obrisi(int stavkaId)
        {
            NabavkaStavka s = ctx.StavkeNabavke.Where(x => x.Id == stavkaId).FirstOrDefault();

            int Id = s.NabavkaId;

            ctx.StavkeNabavke.Remove(s);

            ctx.SaveChanges();





            return RedirectToAction("Details", new { nabavkaId = Id });
        }


        public ActionResult SnimiStavku(StavkaNabavkaVM Model)
        {
            int nabavkaId = 0;
            if (!ModelState.IsValid)
            {
                return View("Details");
            }

            NabavkaStavka stavka;

            if (Model.NabavkaStavkaId == 0)
            {
                if (Model.NabavkaId != 0)
                {


                    stavka = new NabavkaStavka();
                    stavka.NabavkaId = Model.NabavkaId;

                    stavka.Naziv = Model.Naziv;
                    stavka.Cijena = Model.Cijena;
                    ctx.StavkeNabavke.Add(stavka);
                    ctx.SaveChanges();

                    return RedirectToAction("Lista", new { nabavkaId = stavka.NabavkaId });
                }

                Nabavka n = ctx.Nabavke.OrderByDescending(x => x.Id).FirstOrDefault();
                nabavkaId = n.Id;
                stavka = new NabavkaStavka();
                stavka.NabavkaId = nabavkaId;
                ctx.StavkeNabavke.Add(stavka);
                stavka.Naziv = Model.Naziv;
                stavka.Cijena = Model.Cijena;
                ctx.SaveChanges();



                return RedirectToAction("Lista", new { nabavkaId = stavka.NabavkaId });
            }
            else
            {
                stavka = ctx.StavkeNabavke.Find(Model.NabavkaStavkaId);
                stavka.Naziv = Model.Naziv;
                stavka.Cijena = Model.Cijena;
                ctx.SaveChanges();
                return RedirectToAction("Details", new { nabavkaId = Model.NabavkaId });
            }







            //return View("DodajNabavku");
        }

    }
}