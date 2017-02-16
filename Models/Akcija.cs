using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Akcija
    {
        public int AkcijaId { get; set; }
        public string Adresa { get; set; }
        public DateTime Vrijeme { get; set; }

        public int DispozicijaId { get; set; }
        public Dispozicija Dispozicija { get; set; }

        public int TipAkcijeId { get; set; }
        public TipAkcije TipAkcije { get; set; }

        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }
    }
}