using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulVozac.Models
{
    public class TroskoviPrikaziVM
    {

        public int TrosakId { get; set; }
        public double Ukupno { get; set; }

        public string TipTrosak { get; set; }


        public int InstradacijaId { get; set; }
    }
}