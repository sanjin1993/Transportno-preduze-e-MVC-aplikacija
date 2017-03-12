using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Dobavljac
    {

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public ICollection<Nabavka> Nabavke { get; set; }

        public int ZaposlenikId { get; set; }
        public Zaposlenik Zaposlenik { get; set; }
    }
}