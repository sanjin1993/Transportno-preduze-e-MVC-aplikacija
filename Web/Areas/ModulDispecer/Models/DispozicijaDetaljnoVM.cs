using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.Models;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class DispozicijaDetaljnoVM:DispozicijeIndexVM
    {
        public DispozicijaDetaljnoVM()
        {
            Stavke = new List<StavkaVM>();
            Drzave = new List<Drzava>();
            Klijenti = new List<KlijentVM>();
        }



        public string RowGuid { get; set; }
        public string DodatneInformacije { get; set; }

        public List<Drzava> Drzave { get; set; }
        [Required(ErrorMessage = "Država od je obavezno polje.")]
        public int DrzavaOdId { get; set; }
        [Required(ErrorMessage = "Adresa od je obavezno polje.")]
        public string AdresaOd { get; set; }
        [Required(ErrorMessage ="Država do je obavezno polje.")]
        public int DrzavaDoId { get; set; }
        [Required(ErrorMessage = "Adresa do je obavezno polje.")]
        public string AdresaDo { get; set; }

        public List<KolicinaTip> TipKolicine { get; set; }
        public List<StavkaVM> Stavke { get; set; }
        public List<Instradacija> Instradacije { get; set; }
        public List<KlijentVM> Klijenti { get; set; }

       
        public string NazivStavke { get; set; }
        public int Kolicina { get; set; }
        public int KolicinaTipId { get; set; }
    }
}