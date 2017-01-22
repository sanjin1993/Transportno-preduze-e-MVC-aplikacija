using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Vozac:Zaposlenik
    {

  
        public string BrojVozacke { get; set; }
        public DateTime RokVazenjaDozvole { get; set; }
        public bool ADRLicenca { get; set; }
        public DateTime DatumLjekarskog { get; set; }

      
       
       

        public ICollection<KarticaVozac> KarticaVozaci { get; set; }

        public int StatusVozacaId { get; set; }
        public StatusVozaca StatusVozaca { get; set; }


        public ICollection<VozacKategorije> VozacKategorije { get; set; }
        public ICollection<Instradacija> Instradacije { get; set; }

       
    }
}