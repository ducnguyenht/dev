using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxRoundPanel;
using Utility;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Collections.ObjectModel;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.Sales.PricePolicy
{
    public partial class PricePolicyPage : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        NAS.GUI.Pattern.Context GUIContext
        {
            get {
                return Session["PricePolicy_context"] as NAS.GUI.Pattern.Context;
            }
            set {
                Session["PricePolicy_context"] = value;
            }
        }

        public Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            PricePolicyXDS.Session = session;
            PricePolicyTypeXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                GUIContext = new NAS.GUI.Pattern.Context();
                GUIContext.State = new WebModule.Sales.PricePolicy.State.PricePolicyListing(this);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_POLICY_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }

        protected void grdPricePolicy_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            GUIContext.Request(e.ButtonID, this);
        }
    }
}