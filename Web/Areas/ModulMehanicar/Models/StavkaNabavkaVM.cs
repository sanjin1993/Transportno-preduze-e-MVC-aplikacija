using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulMehanicar.Models
{
    public class StavkaNabavkaVM
    {
        public int NabavkaStavkaId { get; set; }
        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Cijena je obavezno polje.")]
        [Range(1, 10000, ErrorMessage = "Iznos mora biti u rasponu od 1 - 10000")]
        public float Cijena { get; set; }
        public int NabavkaId { get; set; }
    }
}