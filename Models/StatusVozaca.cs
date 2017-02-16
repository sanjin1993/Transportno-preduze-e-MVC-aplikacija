using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class StatusVozaca
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public ICollection<Vozac> Vozaci { get; set; }
    }
}