using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.DAL;

namespace TransportnoPreduzece.Web.Reports
{
    public class InstradacijaRM
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

        public class InstradacijaInfo
        {
            public DateTime Datum { get; set; }
            public int ulaznaCarinarnica { get; set; }
            public int izlaznaCarinarnica { get; set; }
            public string prikljucnoVozilo { get; set; }
            public string vozilo { get; set; }
            public string status { get; set; }

        }

        public List<Stavka> Stavke { get; set; }


        public static List<InstradacijaInfo> GetInstradacijaInfos(int instradacijaId)
        {
            TPContext ctx = new TPContext();
            InstradacijaInfo i = ctx.Instradacije.Where(x => x.InstradacijaId == instradacijaId).Select(y => new InstradacijaInfo
            {
                Datum = y.Datum,
                ulaznaCarinarnica = (int)y.UlaznaCarinarnica,
                izlaznaCarinarnica = (int)y.IzlaznaCarinarnica,
                prikljucnoVozilo = y.PrikljucnoVozilo.RegistarskeOznake,
                vozilo = y.Vozilo.RegistarskeOznake,
                status = y.StatusInstradacije.Naziv
            }).FirstOrDefault();

            List<InstradacijaInfo> info = new List<InstradacijaInfo>();
            info.Add(i);

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

        public static List<Header> GetHeader(int instradacijaId)
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
            return header;

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
                Kontakt = y.Klijent.Telefon
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