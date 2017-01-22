using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Drzava
    {
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public string Kod { get; set; }

        public ICollection<Klijent> Klijenti { get; set; }
        public ICollection<Akcija> Akcije { get; set; }


   



    }
}