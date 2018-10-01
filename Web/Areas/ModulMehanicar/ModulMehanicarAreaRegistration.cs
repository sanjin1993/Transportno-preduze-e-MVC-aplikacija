using System.Web.Mvc;

namespace Web.Areas.ModulMehanicar
{
    public class ModulMehanicarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulMehanicar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulMehanicar_default",
                "ModulMehanicar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}