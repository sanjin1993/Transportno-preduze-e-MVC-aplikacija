using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Nabavka
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }

        public ICollection<NabavkaStavka> Stavke { get; set; }

        public int DobavljacId { get; set; }
        public Dobavljac Dobavljac { get; set; }
    }
}