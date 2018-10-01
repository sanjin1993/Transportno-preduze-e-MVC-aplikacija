using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulMehanicar.Models
{
    public class OdrzavanjePrikaziVM
    {

        public int OdrzavanjeId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double Troskovi { get; set; }
        public DateTime Datum { get; set; }

        public string Vozilo { get; set; }
        public string PrikljucnoVozilo { get; set; }
        public string Tip_Odrzavanja { get; set; }

    }
}