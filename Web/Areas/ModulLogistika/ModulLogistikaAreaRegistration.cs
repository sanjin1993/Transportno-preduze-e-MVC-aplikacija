using System.Web.Mvc;

namespace Web.Areas.ModulLogistika
{
    public class ModulLogistikaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulLogistika";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulLogistika_default",
                "ModulLogistika/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}