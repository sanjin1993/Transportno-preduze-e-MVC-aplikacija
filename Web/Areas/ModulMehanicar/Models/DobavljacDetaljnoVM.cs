using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulMehanicar.Models
{
    public class DobavljacDetaljnoVM
    {

        public int DobavljacId { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }


        public List<Nabavka> Nabavke { get; set; }


    }
}