using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxMenu;
using DevExpress.Utils;
using Utility;
using WebModule.Interfaces;

namespace WebModule.UserControls
{
    public partial class NavigationToolbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void NavigationMenu_Init(object sender, EventArgs e)
        {
            try
            {
                var menu = (ASPxMenu)sender;
                var rootItem = Utils.NavigationItems.First(i => i.AccessObjectGroupId.ToLower()
                                                == ((IERPCoreWebModuleBase)Page).AccessObjectGroupId.ToLower());
                string cssClass = String.Format("{0} {1}", rootItem.SpriteClassName, Utils.IsDarkTheme ? "DarkTheme" : "LightTheme");
                var rootMenuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                menu.Items.Add(rootMenuItem);
                rootMenuItem.Text = rootItem.Text;
                rootMenuItem.Image.SpriteProperties.CssClass = cssClass;
                rootMenuItem.PopOutImage.SpriteProperties.CssClass = "Sprite_Arrow";

                foreach (var item in Utils.NavigationItems)
                {
                    var menuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                    rootMenuItem.Items.Add(menuItem);
                    menuItem.Text = item.Text;
                    menuItem.NavigateUrl = item.NavigationUrl;
                    menuItem.Selected = item == rootItem;
                }
                menu.ShowPopOutImages = DefaultBoolean.True;
            }
            catch (Exception)
            {

            }
            
        }
    }
}