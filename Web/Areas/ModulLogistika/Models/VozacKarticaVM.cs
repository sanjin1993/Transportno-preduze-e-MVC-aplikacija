using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulLogistika.Models
{
    public class VozacKarticaVM
    {
        public int karticaId { get; set; }

        [Required(ErrorMessage = "Unesite datum koristenja.")]
        public DateTime datumKoristenja { get; set; }

        [Required(ErrorMessage = "Unesite ukupno.")]
        public double ukupniIznos { get; set; }

        [Required(ErrorMessage = "Unesite kolicinu u litrima.")]
        public double kolicinaLitara { get; set; }
        public string adresaPumpe { get; set; }
        public int vozacId { get; set; }

        [Required(ErrorMessage = "Odaberite benzinsku.")]
        public int benzinskaId { get; set; }
        public List<SelectListItem> benzinske { get; set; }
    }
}