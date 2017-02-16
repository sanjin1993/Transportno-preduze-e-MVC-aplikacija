using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class TipOdrzavanja
    {
        public int TipOdrzavanjaId { get; set; }

        public string Naziv { get; set; }
        public ICollection<Odrzavanje> Odrzavanja { get; set; }
    }
}