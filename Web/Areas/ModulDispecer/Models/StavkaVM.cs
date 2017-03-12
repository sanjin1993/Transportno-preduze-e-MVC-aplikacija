using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class StavkaVM
    {
        public int StavkaId { get; set; }
        [Required(ErrorMessage ="Naziv stavke je obavezan.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Količina je obavezna.")]
        public int Kolicina { get; set; }
        [Required(ErrorMessage = "Odaberite tip količine.")]
        public int KolicinaTipId { get; set; }
        public string TipKolicine { get; set; }
        public int DispozicijaId { get; set; }

        public List<SelectListItem> TipoviKolicine { get; set; }
    }
}