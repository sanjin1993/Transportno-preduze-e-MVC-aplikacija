using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.DAL;
namespace TransportnoPreduzece.Web.Reports
{
    public class DispozicijaRM
    {

        public class Primalac
        {
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string Država { get; set; }
            

        }

        public class Posiljalac
        {
            public string Naziv { get; set; }
            public string Adresa { get; set; }
            public string Drzava { get; set; }
            public string Kontakt { get; set; }
        }

        public class Header
        {
            public string NazivPreduzeca { get; set; }
            public string AdresaPreduzeca { get; set; }
            public string JIB { get; set; }
            public string PDV { get; set; }
            public string ZiroRacun { get; set; }
            public string DodatneInformacije { get; set; }
            public Posiljalac Posiljalac { get; set; }
            public Primalac Primalac { get; set; }
           
            public DateTime Datum { get; set; }
        }

        public class Stavka
        {
            public string Naziv { get; set; }
            public int Kolicina { get; set; }
            public string JedinicaMjere { get; set; }

        }

        public class DispozicijaInfo
        {
            public DateTime Datum { get; set; }
            public string RacunBroj { get; set; }
            public string DodatneInformacije { get; set; }
            public DateTime DatumIspostave { get; set; }
            public double Cijena { get; set; }
            public DateTime DatumPlacanja { get; set; }

        }

        public List<Stavka> Stavke { get; set; }


        public static List<DispozicijaInfo> GetDispozicijaInfo(int dispozicijaId)
        {
            TPContext ctx = new TPContext();
            DispozicijaInfo d = ctx.Dispozicije.Where(x => x.DispozicijaId == dispozicijaId).Select(y => new DispozicijaInfo
            {
                Datum=y.DatumDispozicije,
                DatumIspostave=y.DatumIspostave,
                DatumPlacanja=y.DatumPlacanja,
                Cijena=y.Cijena,
                RacunBroj=y.RowGuid,
                DodatneInformacije=y.DodatneInformacije
            }).FirstOrDefault();

            List<DispozicijaInfo> info = new List<DispozicijaInfo>();
            info.Add(d);

            return info;
        }

        public static List<Stavka> GetBody(int dispozicijaId)
        {
            TPContext ctx = new TPContext();
            return ctx.Stavke.Where(x => x.DispozicijaId == dispozicijaId).Select(
                y => new Stavka
                {
                    Naziv = y.Naziv,
                    Kolicina = y.Kolicina,
                    JedinicaMjere = y.KolicinaTip.Naziv
                }
                ).ToList();

        }

        public static List<Header> GetHeader(int dispozicijaId)
        {
            TPContext ctx = new TPContext();

            List<Header> header = new List<Header>();
            Header h = new Header();
            h.AdresaPreduzeca = "Mostarsko raskršće bb, Sarajevo";
            h.PDV = "123123456";
            h.JIB = "547574585696";
            h.NazivPreduzeca = "Cargo Trans";
            h.ZiroRacun = "1234 5678 9123 3456";
                      
            header.Add(h);
            return  header ;

        }

        public static List<Posiljalac> GetPosiljalacInfo(int dispozicijaId)
        {
            TPContext ctx = new TPContext();
            List<Posiljalac> p = new List<Posiljalac>();
            Posiljalac posiljalac = new Posiljalac();
            posiljalac = ctx.Dispozicije.Where(x => x.DispozicijaId == dispozicijaId).Select(y => new Posiljalac
            {
                Naziv = y.Klijent.Naziv,
                Adresa = y.Klijent.Adresa,
                Drzava = y.Klijent.Drzava.Naziv,
                Kontakt=y.Klijent.Telefon
            }).FirstOrDefault();


            p.Add(posiljalac);


            return p;
        }


        public static List<Primalac> GetPrimalacInfo(int dispozicijaId)
        {
            TPContext ctx = new TPContext();
            Primalac primalac = new Primalac();
            List<Primalac> p = new List<Primalac>();
            primalac = ctx.Dispozicije.Where(x => x.DispozicijaId == dispozicijaId).Select(y => new Primalac
            {
                Naziv = y.Primalac,
                Adresa = y.AdresaDo,
                Država = y.DrzavaDo.Naziv
                

            }).FirstOrDefault();
            p.Add(primalac);

            return p;
        }
    }
}