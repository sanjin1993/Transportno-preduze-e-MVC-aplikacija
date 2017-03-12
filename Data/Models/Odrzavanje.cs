using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Odrzavanje
    {
        public int OdrzavanjeId { get; set; }

        public DateTime Datum { get; set; }
        public int Kilometraza { get; set; }
        public double Troskovi { get; set; }
        public string Detaljno { get; set; }

        public int ? VoziloId { get; set; }
        public Vozilo Vozilo { get; set; }

        public int TipOdrzavanjaId { get; set; }
        public TipOdrzavanja TipOdrzavanja { get; set; }

        public int ? PrikljucnoVoziloId { get; set; }
        public PrikljucnoVozilo PrikljucnoVozilo { get; set; }
    }
}