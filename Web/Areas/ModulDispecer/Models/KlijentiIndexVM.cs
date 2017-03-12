using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer.Models
{
    public class KlijentiIndexVM
    {

       
            public int KlijentId { get; set; }
            public string Naziv { get; set; }
            public string Mail { get; set; }
            public string Telefon { get; set; }
            public string Fax { get; set; }
            public string Adresa { get; set; }
            public string Drzava { get; set; }
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
            public DateTime PosljednjaDispozicija { get; set; }
            public int DrzavaId { get; set; }
        

     
  

     

    }
}