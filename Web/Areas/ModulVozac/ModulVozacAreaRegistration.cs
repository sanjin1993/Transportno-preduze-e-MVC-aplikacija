using System.Web.Mvc;

namespace TransportnoPreduzece.Areas.ModulVozac
{
    public class ModulVozacAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulVozac";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulVozac_default",
                "ModulVozac/{controller}/{action}/{id}",
                new { controller= "Instradacije", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TransportnoPreduzece.Areas.ModulVozac.Controllers" }

            );

          


        }
    }
}