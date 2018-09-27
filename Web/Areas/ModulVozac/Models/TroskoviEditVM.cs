using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransportnoPreduzece.Areas.ModulVozac.Models
{
    public class TroskoviEditVM
    {

        public int TrosakId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Iznos mora biti u rasponu od 1 BAM - 10000 BAM")]
        public double Ukupno { get; set; }

        public int TipTroskaId { get; set; }
       

        public int InstradacijaId { get; set; }

        public List<SelectListItem> Troskovi { get; set; }

    }
}