using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class VozacKategorije
    {
        public int VozackaKategorijaId { get; set; }
        public VozackaKategorija VozackaKategorija { get; set; }

        public int VozacId { get; set; }
        public Vozac Vozac { get; set; }
    }
}