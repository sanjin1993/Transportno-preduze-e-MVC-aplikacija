using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Nabavka
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string Sifra { get; set; }
        public ICollection<NabavkaStavka> Stavke { get; set; }

        public int DobavljacId { get; set; }
        public Dobavljac Dobavljac { get; set; }
    }
}