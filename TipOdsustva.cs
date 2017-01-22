using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class TipOdsustva
    {

        public int TipOdsustvaId { get; set; }
        public string Naziv { get; set; }
        public ICollection<Odsustvo> Odsustva { get; set; }
    }
}