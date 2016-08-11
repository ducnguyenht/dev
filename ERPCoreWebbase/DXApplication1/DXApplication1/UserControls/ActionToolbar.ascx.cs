using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DevExpress.Utils;
using DevExpress.Web.ASPxMenu;
using Utility;
using WebModule.Interfaces;
using System.Web.Security;
using Utility.OAuth;

namespace WebModule.UserControls
{
    public partial class ActionToolbar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchBox.Visible = SearchBoxSpacer.Visible = IsSearchBoxVisible();
                ActionMenuDataSource.XPath = string.Format("Pages/{0}/Item", ((IERPCoreWebModuleBase)Page).AccessObjectId);
                ActionMenu.ShowPopOutImages = InfoMenu.ShowPopOutImages = PersonalMenu.ShowPopOutImages = DefaultBoolean.False;
            }
            catch (Exception)
            {
                
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        protected void ActionMenu_ItemDataBound(object sender, MenuItemEventArgs e)
        {
            IHierarchyData itemHierarchyData = (IHierarchyData)e.Item.DataItem;
            var element = (XmlElement)itemHierarchyData.Item;

            var classAttr = element.Attributes["SpriteClassName"];
            if (classAttr != null)
                e.Item.Image.SpriteProperties.CssClass = classAttr.Value;

            if (e.Item.Parent == e.Item.Menu.RootItem)
                e.Item.ClientVisible = false;
        }

        protected void InfoMenu_OnItemDataBound(object sender, MenuItemEventArgs e)
        {
            IHierarchyData itemHierarchyData = (IHierarchyData)e.Item.DataItem;
            var element = (XmlElement)itemHierarchyData.Item;

            var classAttr = element.Attributes["SpriteClassName"];
            if (classAttr != null)
                e.Item.Image.SpriteProperties.CssClass = classAttr.Value;

            if (e.Item.Parent.Name == "theme" && e.Item.Name == Utils.CurrentTheme)
                e.Item.Selected = true;

            if (e.Item.Name == "print")
            {
                var url = GetPrintItemNavigationUrl();
                if (string.IsNullOrEmpty(url))
                    e.Item.Visible = false;
                else
                    e.Item.NavigateUrl = url;
            }
        }

        protected string GetPrintItemNavigationUrl()
        {
            try
            {
                //switch (((IERPCoreWebModuleBase)Page).AccessObjectId)
                //{
                //    case Constant.ACCESSOBJECT_SYSTEM_ID:
                //        return "../PrintMails.aspx";
                //    case "calendar":
                //        return "../PrintSchedule.aspx";
                //    case "contacts":
                //        return "../PrintContacts.aspx";
                //}
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        protected bool IsSearchBoxVisible()
        {
            try
            {
                switch (((IERPCoreWebModuleBase)Page).AccessObjectId)
                {
                    case "mail":
                    case "contacts":
                    case "feeds":
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        protected void pcPersonal_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {

        }

        protected void mnPersonal_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "MyAccount":
                    break;
                case "SignOut":
                    State.FriendlyLoginName = null;
                    State.ProfileFields = null;
                    System.Web.Security.FormsAuthentication.SignOut();
                    OAuthHelper.LogOff();
                    break;
                default:
                    break;
            }
        }
    }
}