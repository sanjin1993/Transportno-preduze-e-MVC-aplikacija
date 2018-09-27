using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.DAL;

namespace TransportnoPreduzece.Web.Reports
{
    public class VoziloRM
    {
        public class VoziloInformacije
        {
            public string brojSasije { get; set; }
            public string regOznake { get; set; }
            public string Proizvodjac { get; set; }
            public int nosivost{ get; set; }
            public DateTime DatumRegistracije { get; set; }
            public int godinaProizvodnje{ get; set; }
            public string statusVozila { get; set; }
            public string tipVozila{ get; set; }
        }
        public static List<VoziloInformacije> getVozilaInformacije(int voziloId)
        {
            TPContext ctx = new TPContext();
            List<VoziloInformacije> p = new List<VoziloInformacije>();
            VoziloInformacije vozilo = new VoziloInformacije();
            vozilo = ctx.Vozila.Where(x => x.VoziloId == voziloId).Select(y => new VoziloInformacije
            {
               brojSasije = y.BrojSasije,
                regOznake = y.RegistarskeOznake,
                Proizvodjac = y.Proizvodzac,
                nosivost = y.Nosivost,
                DatumRegistracije = y.DatumRegistracije,
                godinaProizvodnje = y.GodinaProizvodnje,
                statusVozila = y.StatusVozila.Naziv,
                tipVozila = y.TipVozila.Naziv
            }).FirstOrDefault();

            p.Add(vozilo);

            return p;
        }
        public class Odrzavanja
        {
            public DateTime datum { get; set; }
            public double kilometraza { get; set; }
            public double troskovi { get; set; }
            public string tipOdrzavanja{ get; set; }

        }
        public class Instradacija
        {
            public DateTime datum { get; set; }
            public int ulaznaCarina { get; set; }
            public int izlaznaCarina { get; set; }
            public string statusInstradacije { get; set; }
        }
        public static List<Odrzavanja> GetBody(int voziloId)
        {
            TPContext ctx = new TPContext();
            return ctx.Odrzavanja.Where(x => x.VoziloId == voziloId).Select(
                y => new Odrzavanja
                {
                    datum = y.Datum,
                    kilometraza = y.Kilometraza,
                    tipOdrzavanja = y.TipOdrzavanja.Naziv,
                    troskovi = y.Troskovi
                }
            ).ToList();
        }

        public static List<Instradacija> GetBodyInstradacije(int voziloId)
        {
            TPContext ctx = new TPContext();
            return ctx.Instradacije.Where(x => x.VoziloId == voziloId).Select(
                y => new Instradacija
                {
                    datum = y.Datum,
                    izlaznaCarina = y.IzlaznaCarinarnica?? 1,
                    ulaznaCarina = y.UlaznaCarinarnica??1,
                    statusInstradacije = y.StatusInstradacije.Naziv
                }
            ).ToList();
        }

    }
}