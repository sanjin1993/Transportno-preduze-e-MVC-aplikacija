using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulLogistika.Models
{
    public class VozacOdsustvaVM
    {
        public int odsustvoId { get; set; }

        [Required(ErrorMessage = "Unesite datumOd odsustva.")]
        public DateTime datumOd { get; set; }

        [Required(ErrorMessage = "Unesite datumDo odsustva.")]
        public DateTime datumDo { get; set; }
        public string tipOdsustva { get; set; }
        public int vozacId { get; set; }

        [Required(ErrorMessage = "Odaberite tip odsustva.")]
        public int tipId { get; set; }
        public List<SelectListItem> tipoviOdsustva { get; set; }

    }
}