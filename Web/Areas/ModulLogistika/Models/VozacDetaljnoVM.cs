using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulLogistika.Models
{
    public class VozacDetaljnoVM : VozacIndexVM
    {
        public string ADRlicenca { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime datumLjekarskog { get; set; }

        [Required(ErrorMessage = "Status vozaca je obavezno polje.")]
        public int statusId { get; set; }
        public List<SelectListItem> statusi { get; set; }
        [StringLength(15, MinimumLength = 13, ErrorMessage = "JMBG mora imati 13 karaktera a max 15!!")]
        public string JMBG { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Datum zaposlenja je obavezno polje.")]
        public DateTime datumZaposlenja { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


    }
}