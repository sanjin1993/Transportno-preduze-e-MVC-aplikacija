using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportnoPreduzece.Data.Models;

namespace Web.Areas.ModulMehanicar.Models
{
    public class NabavkaDetaljnoVM:NabavkaPrikaziVM
    {

        public List<SelectListItem> DobavljaciStavke { get; set; }



        public List<NabavkaStavka> nabavke { get; set; }
    }
}