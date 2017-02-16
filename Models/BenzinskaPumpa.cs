using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class BenzinskaPumpa
    {
        public int BenzinskaPumpaId { get; set; }
        public string Adresa { get; set; }

        public ICollection<KarticaVozac> KarticaVozaci { get; set; }
    }
}