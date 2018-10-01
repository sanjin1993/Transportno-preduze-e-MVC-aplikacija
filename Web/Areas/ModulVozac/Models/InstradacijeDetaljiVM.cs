using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulVozac.Models
{
    public class InstradacijeDetaljiVM: InstradacijePrikaziVM
    {
        public InstradacijeDetaljiVM()
        {
            Troskovi = new List<Trosak>();
        }
        [Required(ErrorMessage = "Vozilo je obavezno.")]
        public int VoziloId { get; set; }
        [Required(ErrorMessage = "Priključno vozilo je obavezno.")]
        public int PrikljucnoVoziloId { get; set; }

        public int DispozicijaId { get; set; }

        [Required(ErrorMessage = "Vozac je obavezan.")]
        public int VozacId { get; set; }

        public List<SelectListItem> Vozaci { get; set; }
        public List<SelectListItem> Vozila { get; set; }
        public List<SelectListItem> PrikljucnaVozila { get; set; }

        public List<SelectListItem> Statusi { get; set; }


        public List<Trosak> Troskovi { get; set; }




        public enum TipTroska { putarina = 1, prenociste, ostalo }
    }
}