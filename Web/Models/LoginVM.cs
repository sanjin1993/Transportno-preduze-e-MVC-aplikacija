using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Web.Models
{
    public class LoginVM
    {
        [Required (ErrorMessage ="Unesite korisničko ime.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Unesite lozinku.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}