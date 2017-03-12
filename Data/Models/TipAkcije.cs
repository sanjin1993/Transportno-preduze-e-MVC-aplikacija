using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class TipAkcije
    {
        public int TipAkcijeId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Akcija> Akcije { get; set; }
    }
}