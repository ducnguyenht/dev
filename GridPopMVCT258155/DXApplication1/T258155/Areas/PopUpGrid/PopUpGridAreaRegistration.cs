using System.Web.Mvc;

namespace T258155.Areas.PopUpGrid
{
    public class PopUpGridAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PopUpGrid";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PopUpGrid_default",
                "PopUpGrid/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}