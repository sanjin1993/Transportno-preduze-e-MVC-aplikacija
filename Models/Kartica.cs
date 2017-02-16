using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Kartica
    {

        public int KarticaId { get; set; }
        public float TrenutniIznos { get; set; }
        public bool Aktivna { get; set; }

        public ICollection<KarticaZaposlenik> KarticaZaposlenici { get; set; }
    }
}