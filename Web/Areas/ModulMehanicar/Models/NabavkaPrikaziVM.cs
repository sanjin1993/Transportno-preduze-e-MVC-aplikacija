using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.ModulMehanicar.Models
{
    public class NabavkaPrikaziVM
    {
        public int NabavkaId { get; set; }
        public string Sifra { get; set; }

        public DateTime DatumNabavke { get; set; }
        public string DobavljacNaziv { get; set; }
        public int BrojStavki { get; set; }

        public int DobavljacId { get; set; }

        public List<StavkaNabavkaVM> Stavke { get; set; }
    }
}