using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Zaposlenik
    {

        public int ZaposlenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public DateTime ?  DatumOtkaza { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public int UlogaId { get; set; }
        public Uloga Uloga { get; set; }

        public string Password { get; set; }

        public ICollection<KarticaZaposlenik> KarticaZaposlenici { get; set; }
        public ICollection<Odsustvo> Odsustva { get; set; }

        public string ImePrezime { get { return Ime + " " + Prezime; }}
    }
}