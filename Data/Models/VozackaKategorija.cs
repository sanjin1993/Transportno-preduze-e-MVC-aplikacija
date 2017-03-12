using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class VozackaKategorija
    {
        public int VozackaKategorijaId { get; set; }
        public string Naziv { get; set; }
        public ICollection<VozacKategorije> VozacKategorije { get; set; }
    }
}