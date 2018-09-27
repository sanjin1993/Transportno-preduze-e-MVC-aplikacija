using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulLogistika.Models
{
    public class VozilaIndexVM
    {
        public int voziloId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public string brojSasije { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        public string regOznake { get; set; }
        public string proizvodjac { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? datumRegistracije { get; set; }
        public string statusVozila { get; set; }
        public string tipVozila { get; set; }
    }
}