using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulLogistika.Models
{
    public class VozacIndexVM
    {
        public int vozacId { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string statusVozaca { get; set; }

        [StringLength(13, MinimumLength = 6, ErrorMessage = "broj Vozacke mora imati 6 karaktera a maximalno 13!!")]
        public string brojVozacke { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime datumVazenjaVozacke { get; set; }
        public DateTime? datumRaskida { get; set; }
    }
}