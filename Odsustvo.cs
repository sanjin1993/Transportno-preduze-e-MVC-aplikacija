using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Odsustvo
    {
        public int OdsustvoId { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }

        public int TipOdsustvaId { get; set; }
        public TipOdsustva TipOdsustva { get; set; }

        public int ZaposlenikId { get; set; }
        public Zaposlenik Zaposlenik { get; set; }
    }
}