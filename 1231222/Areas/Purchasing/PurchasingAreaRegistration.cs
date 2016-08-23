using System.Web.Mvc;

namespace MVC.Areas.Purchasing
{
    public class PurchasingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Purchasing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Purchasing_default",
                "Purchasing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            //context.MapRoute(
            //    name: "Purchas",
            //    url: "Purchasing",
            //    defaults: new { controller = "Purchasing", action = "Index", id = UrlParameter.Optional }
            //);

           // context.MapRoute(
           //    name: "Purchasing", // Route name
           //    url: "Purchasing", // URL with parameters
           //    defaults: new { controller = "Purchasing", action = "Index", id = UrlParameter.Optional } // Parameter defaults
           //);
        }
    }
}