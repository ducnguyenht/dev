using System.Web.Mvc;

namespace MVC.Areas.Item
{
    public class ItemAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Item";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Item",
                url: "Item/{controller}/{action}/{id}",
                defaults: new { controller = "Items", action = "ItemsIndex", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "ItemsCategory",
                url: "items-category",
                defaults: new { controller = "ItemsCategory", action = "ItemsCategoryIndex", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "ItemsUnit",
                url: "items-unit",
                defaults: new { controller = "ItemsUnit", action = "ItemsUnitIndex", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "ItemType",
                url: "items-type",
                defaults: new { controller = "ItemsType", action = "ItemsTypeIndex", id = UrlParameter.Optional }
            );
        }
    }
}