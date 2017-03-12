using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class StatusVozila
    {
        public int StatusVozilaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<PrikljucnoVozilo> PrikljucnaVozila { get; set; }
        public ICollection<Vozilo> Vozila { get; set; }
    }
}