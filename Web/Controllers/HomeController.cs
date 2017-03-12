using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Data.DAL;
namespace TransportnoPreduzece.Web.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        
        public ActionResult Index()
        {

            return  RedirectToAction("Login", "Account");
        }
    }
}