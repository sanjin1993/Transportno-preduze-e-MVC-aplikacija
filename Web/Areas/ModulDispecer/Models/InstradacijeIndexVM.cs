using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class InstradacijeIndexVM
    {

        public int InstradacijaId { get; set; }
        public String ImePrezime { get; set; }
        public DateTime Datum { get; set; }
        [Range(0, 70000, ErrorMessage = "Carinarnica mora biti u rasponu od 0 - 70000")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Unesite broj telefona u ispravnom formatu")]
        public int ? UlaznaCarinarnica { get; set; }
        [Range(0, 70000, ErrorMessage = "Carinarnica mora biti u rasponu od 0 - 70000")]
        public int ? IzlaznaCarinarnica { get; set; }
        public string Status{ get; set; }
        public string Vozilo { get; set; }
        public string PrikljucnoVozilo { get; set; }
        public string DrzavaOd { get; set; }
        public string DravaDo { get; set; }
        public int DispozicijaId { get; set; }






    }
}