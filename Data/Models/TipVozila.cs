using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class TipVozila
    {

        public int TipVozilaId { get; set; }
        public string Naziv { get; set; }
        public ICollection<Vozilo> Vozila { get; set; }
    }
}