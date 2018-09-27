using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.DAL;
namespace TransportnoPreduzece.Web.Reports
{
    public class VozacRM
    {
        public class VozacInformacije
        {
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string JMBG { get; set; }
            public string Telefon { get; set; }
        }

        public static List<VozacInformacije> getVozaciInformacije(int vozacId)
        {
            TPContext ctx = new TPContext();
            List<VozacInformacije> p = new List<VozacInformacije>();
            VozacInformacije vozac = new VozacInformacije();
            vozac = ctx.Vozaci.Where(x => x.ZaposlenikId == vozacId).Select(y => new VozacInformacije
            {
                Ime = y.Ime,
                Prezime = y.Prezime,
                Adresa = y.Adresa,
                JMBG = y.JMBG,
                Telefon = y.Telefon
            }).FirstOrDefault();

            p.Add(vozac);

            return p;
        }

        public class Kartice
        {
            public DateTime datumKoristenja { get; set; }
            public double kolicina { get; set; }
            public double ukupanIznos { get; set; }
            public string benzinskaNaziv { get; set; }

        }

        public class Odsustva
        {
            public DateTime datumOd { get; set; }
            public DateTime datumDo { get; set; }
            public string tipOdsustva { get; set; }
        }

        public static List<Kartice> GetBody(int vozacId)
        {
            TPContext ctx = new TPContext();
            return ctx.KarticaVozaci.Where(x => x.VozacId == vozacId).Select(
                y => new Kartice
                {
                    datumKoristenja = y.DatumKoristenja,
                    kolicina = y.KolicinaLitara,
                    ukupanIznos = y.UkupanIznos,
                    benzinskaNaziv = y.BenzinskaPumpa.Adresa
                }
            ).ToList();
        }

        public static List<Odsustva> GetBodyOdsustva(int vozacId)
        {
            TPContext ctx = new TPContext();
            return ctx.Odsustva.Where(x => x.ZaposlenikId == vozacId).Select(
                y => new Odsustva
                {
                    datumDo = y.DatumDo,
                    datumOd = y.DatumOd,
                    tipOdsustva = y.TipOdsustva.Naziv
                }
            ).ToList();
        }


        public class Header
        {
            public string NazivPreduzeca { get; set; }
            public string AdresaPreduzeca { get; set; }
            public string JIB { get; set; }
            public string PDV { get; set; }
            public string ZiroRacun { get; set; }
            public string DodatneInformacije { get; set; }
            public DateTime Datum { get; set; }
        }

        public static List<Header> GetHeader(int vozacId)
        {
            TPContext ctx = new TPContext();

            List<Header> header = new List<Header>();
            Header h = new Header();
            h.AdresaPreduzeca = "Mostarsko raskršće bb, Sarajevo";
            h.PDV = "123123456";
            h.JIB = "547574585696";
            h.NazivPreduzeca = "Cargo Trans";
            h.ZiroRacun = "1234 5678 9123 3456";
            h.Datum = DateTime.Now;

            header.Add(h);
            return header;

        }
    }
}
