using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class NabavkaStavka
    {
        public int Id { get; set; }
        public int Naziv { get; set; }
        public float Cijena { get; set; }

        public int NabavkaId { get; set; }
        public Nabavka Nabavka { get; set; }
    }
}