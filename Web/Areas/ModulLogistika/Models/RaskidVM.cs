using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.ModulLogistika.Models
{
    public class RaskidVM
    {
        public int vozacId { get; set; }
        public DateTime? datumRaskida { get; set; }

        [Required(ErrorMessage = "Status vozaca je obavezno polje.")]
        public int statusId{ get; set; }
        public List<SelectListItem> statusi  { get; set; }
    }
}