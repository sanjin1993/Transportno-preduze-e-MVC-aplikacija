using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportnoPreduzece.Models
{
    public class StatusInstradacije
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public ICollection<Instradacija> Instradacije { get; set; }

    }
}