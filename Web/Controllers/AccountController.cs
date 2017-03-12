using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Web.Models;

namespace TransportnoPreduzece.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }



        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginVM model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (TPContext objContext = new TPContext())
                {
                    var objUser = objContext.Zaposlenici.FirstOrDefault(x => x.Email == model.Username && x.Password == model.Password);
                    if (objUser == null)
                    {
                        ModelState.AddModelError("LogOnError", "Korisničko ime ili šifra su neispravni.");
                        
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            string[] roles = new string[1];
                            roles = Roles.GetRolesForUser(objUser.Email);
                            if (roles[0] == "dispečer") { 
                            return RedirectToAction("Index","Dispozicija",new { area = "ModulDispecer" });
                            }


                        }
                    }
                }
            }
          
            return View(model);
        }


       

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}