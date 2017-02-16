using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Stavka
    {
        public int StavkaId { get; set; }
        public string Naziv { get; set; }
        public int Kolicina { get; set; }

        public int DispozicijaId { get; set; }
        public Dispozicija Dispozicija { get; set; }
        public int KolicinaTipId { get; set; }
        public KolicinaTip KolicinaTip { get; set; }


    }
}