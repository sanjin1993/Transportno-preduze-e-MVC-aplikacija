using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Klijent
    {
        public int KlijentId { get; set; }

        public string Naziv { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string IdBroj { get; set; }
        public string Fax { get; set; }

        public ICollection<Dispozicija> Dispozicije { get; set; }

        public int DrzavaId { get; set; }
        public Drzava Drzava { get; set; }

        public bool IsDeleted { get; set; }
    }
}