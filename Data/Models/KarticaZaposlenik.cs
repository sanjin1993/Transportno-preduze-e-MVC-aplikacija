using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class KarticaZaposlenik
    {
        public int KarticaZaposlenikId { get; set; }
        public float Iznos { get; set; }
        public DateTime Datum { get; set; }

        public int ZaposlenikId { get; set; }
        public Zaposlenik Zaposlenik { get; set; }

        public int KarticaId { get; set; }
        public Kartica Kartica { get; set; }
    }
}