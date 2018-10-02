using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulMehanicar.Models
{

    public class DobavljacDetaljnoVM
    {

        public int DobavljacId { get; set; }
        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Adresa je obavezno polje.")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Telefon je obavezno polje.")]
        public string Telefon { get; set; }


        public List<Nabavka> Nabavke { get; set; }


    }
}