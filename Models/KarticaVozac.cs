using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class KarticaVozac
    {

        public int KarticaVozacId { get; set; }

        public DateTime DatumKoristenja { get; set; }
        public double KolicinaLitara { get; set; }
        public double UkupanIznos { get; set; }

        public int VozacId { get; set; }
        public Vozac Vozac { get; set; }

        public int BenzinskaPumpaId { get; set; }
        public BenzinskaPumpa BenzinskaPumpa { get; set; }

    }
}