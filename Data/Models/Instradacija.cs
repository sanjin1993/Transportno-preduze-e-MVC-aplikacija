using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Data.Models
{
    public class Instradacija
    {
        public int InstradacijaId { get; set; }
        public int ?  UlaznaCarinarnica { get; set; }
        public int ? IzlaznaCarinarnica { get; set; }
        public DateTime Datum { get; set; }

        public int VoziloId { get; set; }
        public Vozilo Vozilo { get; set; }

        public int VozacId { get; set; }
        [ForeignKey("VozacId")]
        public Vozac Vozac { get; set; }
       

        public int PrikljucnoVoziloId { get; set; }
        public PrikljucnoVozilo PrikljucnoVozilo { get; set; }

        public int DispozicijaId { get; set; }
        public Dispozicija Dispozicija { get; set; }

        public int StatusInstradacijeId { get; set; }
        public StatusInstradacije StatusInstradacije { get; set; }

        public ICollection<Trosak> Troskovi { get; set; }

        public bool IsDeleted { get; set; }
    }
}