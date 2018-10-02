using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulVozac.Models
{
    public class TroskoviEditVM
    {
        public int TrosakId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Iznos mora biti u rasponu od 0 BAM - 10000 BAM")]
        public double Ukupno { get; set; }
        [Required(ErrorMessage = "Odaberite tip troška.")]
        public int TipTroskaId { get; set; }


        public int InstradacijaId { get; set; }

        public List<SelectListItem> Troskovi { get; set; }
    }
}