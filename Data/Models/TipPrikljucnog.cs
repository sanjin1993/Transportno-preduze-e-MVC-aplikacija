using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class TipPrikljucnog
    {
        public int TipPrikljucnogId { get; set; }
        public string Naziv { get; set; }


        public ICollection<PrikljucnoVozilo> PrikljucnaVozila { get; set; }

    }
}