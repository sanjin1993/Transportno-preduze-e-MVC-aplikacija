using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulMehanicar.Models
{
    public class StavkaNabavkaVM
    {
        public int NabavkaStavkaId { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public int NabavkaId { get; set; }
    }
}