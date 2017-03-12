using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Vozilo
    {

        public int VoziloId { get; set; }
        public string BrojSasije { get; set; }
        public string RegistarskeOznake { get; set; }
        public string Proizvodzac { get; set; }
        public string Model { get; set; }
        public int Nosivost { get; set; }
        public double Cijena { get; set; }
        public int Kilometraza { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public int GodinaProizvodnje { get; set; }

        public int TipVozilaId { get; set; }
        public TipVozila TipVozila { get; set; }

        public ICollection<Odrzavanje> Odrzavanje { get; set; }
        public ICollection<Instradacija> Instradacije { get; set; }

        public int StatusVozilaId { get; set; }
        public StatusVozila StatusVozila { get; set; }
    }
}