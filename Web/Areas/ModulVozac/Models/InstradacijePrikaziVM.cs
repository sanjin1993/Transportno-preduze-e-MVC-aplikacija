using System;
using TransportnoPreduzece.Data.Models;

namespace TransportnoPreduzece.Areas.ModulVozac.Models
{
    public class InstradacijePrikaziVM
    {
        public int InstradacijaId { get; set; }
        public int? UlaznaCarinarnica { get; set; }
        public int? IzlaznaCarinarnica { get; set; }
        public DateTime Datum { get; set; }

        
        public string Vozilo { get; set; }

       
        
        public string PrikljucnoVozilo { get; set; }

        public int StatusInstradacijeId { get; set; }
        public StatusInstradacije StatusInstradacije { get; set; }
        public string Status { get; set; }
        public string DrzavaOd { get; set; }
        public string DrzavaDo { get; set; }

        

        public bool IsDeleted { get; set; }
    }
}