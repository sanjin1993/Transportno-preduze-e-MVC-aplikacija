using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulVozac.Models
{
    public class TrosakBenzinEditVM
    {
        public int KarticaVozacId { get; set; }
        public double DpdijeljenIznos { get; set; }
        public KarticaZaposlenik kartica { get; set; }
        public double TrenutniIznos { get; set; }
        public bool Aktivna { get; set; }

        public DateTime DatumKoristenja { get; set; }
        public double KolicinaLitara { get; set; }
        public double UkupanIznos { get; set; }
        public int KarticaId { get; set; }

        public int VozacId { get; set; }


        public int BenzinskaPumpaId { get; set; }
        public List<SelectListItem> BenzinskePumpe { get; set; }
    }
}