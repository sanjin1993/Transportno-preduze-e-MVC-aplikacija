using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class NabavkaStavka
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cijena { get; set; }

        public int NabavkaId { get; set; }
        public Nabavka Nabavka { get; set; }
    }
}