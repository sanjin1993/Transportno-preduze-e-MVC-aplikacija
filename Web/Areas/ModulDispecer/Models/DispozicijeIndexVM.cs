using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class DispozicijeIndexVM
    {



       public  int DispozicijaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public double Cijena { get; set; }
    
       
        public DateTime ? DatumDispozicije { get; set; }
    
        [Required(ErrorMessage ="Datum ispostave je obavezno polje.")]     
        public DateTime DatumIspostave { get; set; }
    
        public DateTime DatumPlacanja { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Primalac { get; set; }
    
        public int KlijentId { get; set; }
        public string RacunGuid { get; set; }
        public string NazivKlijenta { get; set; }
        public int BrojInstradacija { get; set; }
        public string DrzavaOd { get; set; }
        public string DrzavaDo { get; set; }
        public List<SelectListItem> DrzaveIndex { get; set; }
      




    }
}