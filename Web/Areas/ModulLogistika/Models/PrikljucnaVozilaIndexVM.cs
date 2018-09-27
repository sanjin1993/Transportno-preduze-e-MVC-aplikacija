using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulLogistika.Models
{
    public class PrikljucnaVozilaIndexVM
    {
        public int prikljucnoVoziloId { get; set; }

        [Range(1, 4, ErrorMessage = "Broj osovina mora biti u rasponu od 1 - 4")]
        public int brojOsovina { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public string brojSasije { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public string regOznake { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? datumRegistracije { get; set; }
        public string statusVozila { get; set; }
        public string tipPrikljucnog { get; set; }
    }
}