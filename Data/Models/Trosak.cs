using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Trosak
    {
        public int TrosakId { get; set; }
        public double Ukupno { get; set; }

        public int TipTroskaId { get; set; }
        public TipTroska TipTroska { get; set; }

        public int InstradacijaId { get; set; }
        public Instradacija Instradacija { get; set; }



    }
}