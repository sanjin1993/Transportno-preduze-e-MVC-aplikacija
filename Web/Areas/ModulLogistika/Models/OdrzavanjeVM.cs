using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulLogistika.Models
{
    public class OdrzavanjeVM
    {
        public int odrzavanjeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Datum je obavezan.")]
        public DateTime datum { get; set; }

        [Range(1000, 100000, ErrorMessage = "Kilometraza mora biti u rasponu od 1000 - 100000")]
        public int kilometraza { get; set; }

        [Required(ErrorMessage = "Trosak0 je obavezan.")]
        public double troskovi { get; set; }
        public string tipOdrzavanja { get; set; }

        [Required(ErrorMessage = "Odaberite tip odrzavanja.")]
        public int tipOdrzavanjaId { get; set; }
        public List<SelectListItem> tipoviOdrzavanja { get; set; }
        public int voziloId { get; set; }
        public string priljucno { get; set; }

        [StringLength(100, MinimumLength = 10 , ErrorMessage = "Unesite par rijeci vise za detaljno!!")]
        public string detaljno { get; set; }

    }
}