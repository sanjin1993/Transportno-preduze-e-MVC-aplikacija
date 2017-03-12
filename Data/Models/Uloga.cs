using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Uloga
    {
        public int UlogaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Zaposlenik> Zaposlenici { get; set; }
    }
}