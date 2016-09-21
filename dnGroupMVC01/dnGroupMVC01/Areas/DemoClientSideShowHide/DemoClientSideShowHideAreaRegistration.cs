using System.Web.Mvc;

namespace dnGroupMVC01.Areas.DemoClientSideShowHide
{
    public class DemoClientSideShowHideAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DemoClientSideShowHide";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DemoClientSideShowHide_default",
                "DemoClientSideShowHide/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}