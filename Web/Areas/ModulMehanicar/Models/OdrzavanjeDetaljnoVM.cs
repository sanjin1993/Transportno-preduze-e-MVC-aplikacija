using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulMehanicar.Models
{
    public class OdrzavanjeDetaljnoVM:OdrzavanjePrikaziVM
    {
        public int VoziloId { get; set; }
        public int PrikljucnoVoziloId { get; set; }
        public int TipOdrzavanjaId { get; set; }
        public int StatusId { get; set; }



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