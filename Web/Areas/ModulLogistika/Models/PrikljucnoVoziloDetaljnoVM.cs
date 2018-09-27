using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulLogistika.Models
{
    public class PrikljucnoVoziloDetaljnoVM : PrikljucnaVozilaIndexVM
    {
        public double cijena { get; set; }
        public List<Instradacija> instradacije { get; set; }
        public int nosivost { get; set; }
        public float visina { get; set; }
        public float duzina { get; set; }
        public int tezina { get; set; }
        public List<OdrzavanjeVM> odrzavanja { get; set; }

        [Required(ErrorMessage = "Status vozila je obavezno polje.")]
        public int statusVozilaId { get; set; }
        public List<SelectListItem> StatusiVozila { get; set; }

        [Required(ErrorMessage = "Tip prikljucnog vozila je obavezno polje.")]
        public int tipPrikljucnogVozilaId { get; set; }
        public List<SelectListItem> TipoviPrikljcnogVozila { get; set; }
    }
}