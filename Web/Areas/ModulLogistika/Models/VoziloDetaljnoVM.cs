using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulLogistika.Models
{
    public class VoziloDetaljnoVM : VozilaIndexVM
    {
        public int Nosivost { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public double Cijena { get; set; }
        public int Kilometraza { get; set; }
        public int godinaProizvodnje { get; set; }
        public string model { get; set; }
        public List<Instradacija> Instradacije { get; set; }
        public List<OdrzavanjeVM> Odrzavanja { get; set; }

        [Required(ErrorMessage = "Status vozila je obavezno polje.")]
        public int statusVozilaId { get; set; }
        public List<SelectListItem> StatusiVozila { get; set; }

        [Required(ErrorMessage = "Tip vozila je obavezno polje.")]
        public int tipVozilaId { get; set; }
        public List<SelectListItem> TipoviVozila { get; set; }

    }
}