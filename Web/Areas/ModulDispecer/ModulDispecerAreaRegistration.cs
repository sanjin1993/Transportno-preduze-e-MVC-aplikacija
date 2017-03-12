using System.Web.Mvc;

namespace TransportnoPreduzece.Web.Areas.ModulDispecer
{
    public class ModulDispecerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulDispecer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulDispecer_default",
                "ModulDispecer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}