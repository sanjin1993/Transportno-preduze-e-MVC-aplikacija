using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class KolicinaTip
    {
        public int KolicinaTipId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Stavka> Stavke { get; set; }
    }
}