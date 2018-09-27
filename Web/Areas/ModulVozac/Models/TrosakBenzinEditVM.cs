using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace TransportnoPreduzece.Areas.ModulVozac.Models
{
    public class TrosakBenzinEditVM
    {
        public int KarticaVozacId { get; set; }
        public double DpdijeljenIznos { get; set; }
        public KarticaZaposlenik kartica { get; set; }
        public double TrenutniIznos { get; set; }
        public bool Aktivna { get; set; }

        public DateTime DatumKoristenja { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Litraža mora biti u rasponu od 1  - 500 L")]
        public double KolicinaLitara { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Iznos mora biti u rasponu od 1 BAM - 10000 BAM")]
        public double UkupanIznos { get; set; }
        public int KarticaId { get; set; }

        public int VozacId { get; set; }
    

        public int BenzinskaPumpaId { get; set; }
        public List<SelectListItem> BenzinskePumpe { get; set; }
    }
}