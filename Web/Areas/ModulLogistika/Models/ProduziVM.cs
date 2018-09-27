using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulLogistika.Models
{
    public class ProduziVM
    {
        public int voziloId { get; set; }

        [Required(ErrorMessage = "Datum je obavezan")]
        public DateTime datum { get; set; }
    }
}