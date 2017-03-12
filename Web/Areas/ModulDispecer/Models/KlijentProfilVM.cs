using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PagedList;
using TransportnoPreduzece.Data.Models;


namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class KlijentProfilVM
    {





        public int KlijentId { get; set; }
        [Required (ErrorMessage = "Polje je obavezno")]
        [StringLength(200)]
        public string Naziv { get; set; }
        [StringLength(70)]
        public string Mail { get; set; }
        [Required (ErrorMessage ="Polje je obavezno")]
        [StringLength(30, ErrorMessage = "Telefon može sadržavati minimalno 9, maksimalno 30 cifara.", MinimumLength = 9)]
        [RegularExpression("^[0-9]+$",ErrorMessage ="Unesite broj telefona u ispravnom formatu")]
        public string Telefon { get; set; }
        [StringLength(30,ErrorMessage ="Fax može sadržavati minimalno 9, maksimalno 30 cifara.",MinimumLength =9)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Unesite broj faxa u ispravnom formatu")]
        public string Fax { get; set; }
        [StringLength(200,ErrorMessage ="Adresa moze sadrzavati maksimalno 200 znakova.")]
       // [RegularExpression("^[a-zA-Z0-9]+$",ErrorMessage ="Adresa može sadržavati samo slova i brojeve.")]
        public string Adresa { get; set; }
        
        public string Drzava { get; set; }
        [RegularExpression("^[0-9]+$",ErrorMessage ="ID broj može sadržavati samo brojeve.")]
        public string IDBroj { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PosljednjaDispozicija { get; set; }
        [Required(ErrorMessage ="Odaberite državu")]
        public int DrzavaId { get; set; }


        public List<SelectListItem> Drzave { get; set; }




    }
}