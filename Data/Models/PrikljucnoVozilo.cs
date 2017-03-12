using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class PrikljucnoVozilo
    {


        public int Id { get; set; }
        public string BrojSasije { get; set; }
        public int BrojOsovina { get; set; }
        public string RegistarskeOznake { get; set; }
        public int Nosivost { get; set; }
        public double Cijena { get; set; }
        public float Visina { get; set; }
        public float Duzina { get; set; }
        public int Tezina { get; set; }
        public DateTime DatumRegistracije { get; set; }

        public ICollection<Odrzavanje> Odrzavanja { get; set; }
        public ICollection<Instradacija> Instradacije { get; set; }

        public int TipPrikljucnogId { get; set; }
        public TipPrikljucnog TipPrikljucnog { get; set; }

        public int StatusVozilaId { get; set; }
        public StatusVozila StatusVozila { get; set; }
    }
}