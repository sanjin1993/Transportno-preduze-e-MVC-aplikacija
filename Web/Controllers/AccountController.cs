﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransportnoPreduzece.Data.DAL;
using TransportnoPreduzece.Data.Models;
using TransportnoPreduzece.Web.Models;
using Web;

namespace TransportnoPreduzece.Web.Controllers
{
    public class AccountController : Controller
    {

        public static Zaposlenik objUser;
        

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
                    objUser = objContext.Zaposlenici.FirstOrDefault(x =>
                        x.Email == model.Username && x.Password == model.Password);
                    
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
                            if(roles[0] == "logističar")
                            {
                                return RedirectToAction("Index", "Vozilo", new { area = "ModulLogistika" });
                            }
                            if (roles[0] == "vozač")
                            {
                                Global.odabraniVozac = objUser;
                                return RedirectToAction("Prikazi", "Instradacije", new { area = "ModulVozac" });

                            }
                            if (roles[0] == "mehaničar")
                            {
                                Global.odabraniVozac = objUser;
                                return RedirectToAction("Prikazi", "Dobavljac", new { area = "ModulMehanicar" });

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