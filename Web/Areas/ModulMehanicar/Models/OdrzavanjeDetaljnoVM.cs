using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulMehanicar.Models
{
    public class OdrzavanjeDetaljnoVM:OdrzavanjePrikaziVM
    {
        [Required(ErrorMessage = "Odaberite vozilo.")]
        public int VoziloId { get; set; }
        public int PrikljucnoVoziloId { get; set; }
        [Required(ErrorMessage = "Odaberite tip odrzavanja.")]
        public int TipOdrzavanjaId { get; set; }
        public int StatusId { get; set; }


        [Range(1000, 100000, ErrorMessage = "Kilometraza mora biti u rasponu od 1000 - 100000")]
        public int Kilometraza { get; set; }
        public string Detaljno { get; set; }


        public Vozilo Voziloo { get; set; }
        public PrikljucnoVozilo PVozilo { get; set; }
        public List<SelectListItem> VoziloStavke { get; set; }

        public List<SelectListItem> PrikljucnoVoziloStavke { get; set; }


        public List<SelectListItem> TipOdrzavanjaStavke { get; set; }

        public List<SelectListItem> Statusi { get; set; }


    }
}