using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulVozac.Models
{
    public class TroskoviPrikaziVM
    {

        public int TrosakId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Iznos mora biti u rasponu od 0 BAM - 10000 BAM")]
        public double Ukupno { get; set; }

        public string TipTrosak { get; set; }


        public int InstradacijaId { get; set; }
    }
}