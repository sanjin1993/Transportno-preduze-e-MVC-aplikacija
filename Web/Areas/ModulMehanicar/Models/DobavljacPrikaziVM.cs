using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulMehanicar.Models
{
    public class DobavljacPrikaziVM
    {
        public int DobavljacId { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

    }
}