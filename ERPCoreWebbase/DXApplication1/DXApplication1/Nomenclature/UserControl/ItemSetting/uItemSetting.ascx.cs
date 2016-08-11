using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.Nomenclature.UserControl.ItemSetting
{
    public partial class uItemSetting : System.Web.UI.UserControl
    {
        public List<NAS.DAL.CMS.ObjectDocument.Object> CustomFieldObjects
        {
            set { Session["CustomFieldObjects" + this.ClientID] = value; }
            get { return Session["CustomFieldObjects" + this.ClientID] as List<NAS.DAL.CMS.ObjectDocument.Object>; }
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }

        public string MainControlClientName
        {
            get { return cpItemEdit.ClientInstanceName; }
        }

        private Session session
        {
            get;
            set;
        }

        private NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session["GUIContext" + this.ClientID + ViewStateControlId] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session["GUIContext" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ManufactuerCboXDS.Session = session;
            ObjectTypeLbXDS.Session = session;
            ItemUnitTreeXDS.Session = session;
            SupplierListXDS.Session = session;
            UnitCboXDS.Session = session;
            TaxXDS.Session = session;
            ItemTaxXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                cpItemEdit.ClientInstanceName = cpItemEdit.ClientID;//string.Format("cpItemEdit{0}", ViewStateControlId);
                GUIContext = new NAS.GUI.Pattern.Context();
            }
        }

        protected void cpItemEdit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            throw new Exception("Hello world!");
        }
    }
}