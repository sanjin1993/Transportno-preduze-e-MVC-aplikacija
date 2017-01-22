using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class Dispozicija
    {

        public int DispozicijaId { get; set; }
       
        public string RowGuid { get; set; }
        public double Cijena { get; set; }
        public DateTime DatumPlacanja { get; set; }
        public DateTime DatumDispozicije { get; set; }
        public DateTime DatumIspostave { get; set; }
        public string Primalac { get; set; }
        public string DodatneInformacije { get; set; }



      
        public int DrzavaOdId { get; set; }
        [ForeignKey("DrzavaOdId")]
        public Drzava DrzavaOd { get; set; }

        public int DrzavaDoId { get; set; }
        [ForeignKey("DrzavaDoId")]
        public Drzava DrzavaDo { get; set; }



        public string AdresaOd { get; set; }
        public string AdresaDo { get; set; }

        public ICollection<Instradacija> Instradacije { get; set; }

        public int KlijentId { get; set; }
        public Klijent Klijent { get; set; }
        public ICollection<Stavka> Stavke { get; set; }

        public bool IsDeleted { get; set; }

    }
} 