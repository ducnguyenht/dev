using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPopupControl;
using NAS.BO.Sale.PricePolicy.RulesWiz.Predicator;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Sale.PricePolicy;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;
using WebModule.Sales.PricePolicy.PredicatorSubForm.Interface;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxLoadingPanel;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.SupplierList
{
    public partial class uSettingSuppliersList : System.Web.UI.UserControl, IWizardPolicy
    {
        #region IWizardPolicy

        public void settingJavascript()
        {
            if (RuleObject != null)
            {
                ButtonNextSupplierSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Next');}";
                ButtonBackSupplierSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Back');}";
                ButtonFinishSupplierSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Save');}";

                ASPxLoadingPanel loadingPanel = new ASPxLoadingPanel();
                loadingPanel.ID = this.RuleObject.MainCpClientInstanceName + "loadingPanel";
                if (this.FindControl(loadingPanel.ID) == null)
                {
                    loadingPanel.ClientInstanceName = loadingPanel.ID;
                    loadingPanel.LoadingDivStyle.BackColor = System.Drawing.Color.Transparent;
                    loadingPanel.Text = "Đang xử lý";
                    loadingPanel.Modal = true;
                    this.Controls.Add(loadingPanel);

                    cpSettingSuppliersList.ClientSideEvents.BeginCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Show(); }";
                    cpSettingSuppliersList.ClientSideEvents.EndCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Hide(); }";
                }
            }
        }

        public void settingInit(Guid pricePolicy, RuleCondition rule){
            this.KeyValue = pricePolicy;
            this.RuleObject = rule;
            this.cpSettingSuppliersList.ClientInstanceName = rule.MainCpClientInstanceName;
            settingJavascript();
        }
        
        public RuleCondition RuleObject{
            set {
                Session["RuleObject" + this.ClientID + this.Session.SessionID] = value;
            }
            get{
                return Session["RuleObject" + this.ClientID + this.Session.SessionID] as RuleCondition;
            }
        }

        public Guid KeyValue
        {
            get
            {
                return Guid.Parse(Session["KeyValue" + this.ClientID + this.Session.SessionID].ToString());
            }

            set
            {
                Session["KeyValue" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private ASPxCheckBox ChkSelectedSuppliersAll {
            get {
                return grdSupplierList.FindFilterCellTemplateControl(grdSupplierList.Columns[0], "chkSupplierAll") as ASPxCheckBox;
            }

            set {
                ASPxCheckBox c = grdSupplierList.FindFilterCellTemplateControl(grdSupplierList.Columns[0], "chkSupplierAll") as ASPxCheckBox;
                c = value;
            }
        }

        public NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session["GUI" + this.ClientID + this.Session.SessionID] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session["GUI" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        #endregion

        #region State Pattern

        public void CRUD_Opening() {
            SelectionLST = RuleObject.LoadRuleCondition(session, KeyValue) as List<DataGrdSupplierListSelection>;
            grdSupplierListPreview.DataSource = SelectionLST;
            grdSupplierListPreview.DataBind();
        }

        public void CRUD_Preview() {

            List<object> fieldValues = grdSupplierList.GetSelectedFieldValues(new string[] { "OrganizationId", "Code", "Name" });
            SelectionLST = new List<DataGrdSupplierListSelection>();
            foreach (object[] item in fieldValues)
            {
                DataGrdSupplierListSelection o = new DataGrdSupplierListSelection();
                /*2013-11-24 ERP-1118 Khoa.Truong MOD START*/
                //o.OrganizationId = Guid.Parse(item[0].ToString());
                //o.Code = item[1].ToString();
                //o.Name = item[2].ToString();
                o.OrganizationId = item[0] != null ? Guid.Parse(item[0].ToString()) : Guid.Empty;
                o.Code = item[1] != null ? item[1].ToString() : String.Empty;
                o.Name = item[2] != null ? item[2].ToString() : String.Empty;
                /*2013-11-24 ERP-1118 Khoa.Truong MOD END*/
                SelectionLST.Add(o);
            }

            grdSupplierListPreview.DataSource = SelectionLST;
            grdSupplierListPreview.DataBind();
        }

        public void CRUD_Saving() {
            if (ChkSelectedSuppliersAll.Checked)
            {
                SelectionLST = new List<DataGrdSupplierListSelection>();
                DataGrdSupplierListSelection o = new DataGrdSupplierListSelection();
                NAS.DAL.Nomenclature.Organization.SupplierOrg.Populate();
                NAS.DAL.Nomenclature.Organization.SupplierOrg s = 
                    session.FindObject<NAS.DAL.Nomenclature.Organization.SupplierOrg>(
                        new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL, BinaryOperatorType.Equal));
                o.OrganizationId = s.OrganizationId;
                o.Code = s.Code;
                o.Name = s.Name;
                SelectionLST.Add(o);
            }

            RuleObject.SaveRuleCondition(session, KeyValue, SelectionLST);
        }

        public void UpdateGUI_Opening()
        {
            popup_settingSuppliersList.ShowOnPageLoad = true;
            popup_settingSuppliersList.HeaderText = this.RuleObject.Description;
            ButtonFinishSupplierSetting.Visible = false;
            ButtonBackSupplierSetting.Visible = false;
            ButtonNextSupplierSetting.Visible = true;
            pcSettingSuppliersList.TabPages[0].ClientEnabled = true;
            pcSettingSuppliersList.TabPages[1].ClientEnabled = false;
            pcSettingSuppliersList.ActiveTabIndex = 0;
            UpdateGUI_LoadSelection();
        }

        public void UpdateGUI_LoadSelection() {
            grdSupplierList.Selection.UnselectAll();
            if (SelectionLST == null)
                return;

            if (SelectionLST.Count == 1 && SelectionLST[0].Code.Equals(Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
            {
                grdSupplierList.Selection.SelectAll();
            }
            else
                foreach(DataGrdSupplierListSelection d in SelectionLST)
                    grdSupplierList.Selection.SetSelectionByKey(d.OrganizationId, true);

            if (this.grdSupplierList.Selection.Count == grdSupplierList.VisibleRowCount)
                ChkSelectedSuppliersAll.Checked = true;
            else
                ChkSelectedSuppliersAll.Checked = false;
        }

        public void UPdateGUI_Preview()
        {
            popup_settingSuppliersList.ShowOnPageLoad = true;
            popup_settingSuppliersList.HeaderText = this.RuleObject.Description;
            ButtonFinishSupplierSetting.Visible = true;
            ButtonBackSupplierSetting.Visible = true;
            ButtonNextSupplierSetting.Visible = false;
            pcSettingSuppliersList.TabPages[0].ClientEnabled = false;
            pcSettingSuppliersList.TabPages[1].ClientEnabled = true;
            pcSettingSuppliersList.ActiveTabIndex = 1;
        }

        public void UPdateGUI_Saving()
        {
            popup_settingSuppliersList.ShowOnPageLoad = false;
        }

        public void UpdateGUI_SelectUnSelectAll()
        {
            if (ChkSelectedSuppliersAll.Checked)
            {
                grdSupplierList.Selection.SelectAll();
            }
            else
            {
                grdSupplierList.Selection.UnselectAll();
            }

            popup_settingSuppliersList.ShowOnPageLoad = true;
            popup_settingSuppliersList.HeaderText = this.RuleObject.Description;
            ButtonFinishSupplierSetting.Visible = false;
            ButtonBackSupplierSetting.Visible = false;
            ButtonNextSupplierSetting.Visible = true;
            pcSettingSuppliersList.TabPages[0].ClientEnabled = true;
            pcSettingSuppliersList.TabPages[1].ClientEnabled = false;
            pcSettingSuppliersList.ActiveTabIndex = 0;
        }

        #endregion

        #region Properties

        private List<DataGrdSupplierListSelection> SelectionLST
        {
            get
            {
                return Session["uSettingSuppliersList_SelectionLST" + this.ClientID + this.Session.SessionID] as List<DataGrdSupplierListSelection>;
            }

            set
            {
                Session["uSettingSuppliersList_SelectionLST" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private ASPxButton ButtonFinishSupplierSetting
        {
            get
            {
                ASPxButton button = popup_settingSuppliersList.FindControl("buttonFinishSupplierSetting") as ASPxButton;
                return button;
            }

            set
            {
                ASPxButton button = popup_settingSuppliersList.FindControl("buttonFinishSupplierSetting") as ASPxButton;
                button = value;
            }
        }

        private ASPxButton ButtonNextSupplierSetting
        {
            get
            {
                ASPxButton button = popup_settingSuppliersList.FindControl("buttonNextSupplierSetting") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonBackSupplierSetting
        {
            get
            {
                ASPxButton button = popup_settingSuppliersList.FindControl("buttonBackSupplierSetting") as ASPxButton;
                return button;
            }
        }

        Session session;

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RuleObject = null;
                GUIContext = null;
                this.cpSettingSuppliersList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.cpSettingSuppliersList.ClientID;
                this.grdSupplierList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.grdSupplierList.ClientID;
            }
            session = XpoHelper.GetNewSession();
            SupplierListXDS.Session = session;
            settingJavascript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GUIContext == null)
            {
                GUIContext = new NAS.GUI.Pattern.Context();
            }
            SetCriteriaForOrganization();
            grdSupplierListPreview.DataSource = SelectionLST;
        }

        protected void cpSettingSuppliersList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];
            switch (trs)
            {
                case NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition.OPEN_TRANSIT:
                    GUIContext.State = new PricePolicy.PredicatorSubForm.SupplierList.State.RuleConditionSupplierOpening(this);
                    break;
                case "SelectUnselectAll":
                case "Next":
                case "Back":
                case "Save":
                    GUIContext.Request(trs, this);
                    break;
            }
        }

        protected void chkSupplierAll_Init(object sender, EventArgs e)
        {
            if (RuleObject != null)
            {
                ASPxCheckBox c = sender as ASPxCheckBox;
                c.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + c.ClientID;
                c.ClientSideEvents.CheckedChanged = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('SelectUnselectAll');}";
                grdSupplierList.ClientSideEvents.SelectionChanged = "function(s, e){ if(e.isChangedOnServer) return; if (!e.isSelected) {" + c.ClientInstanceName + ".SetChecked(false);  }}";
            }
        }

        private void SetCriteriaForOrganization()
        {
            //Get SUPPLIER trading type
            TradingCategory supplierTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "SUPPLIER").FirstOrDefault();

            CriteriaOperator criteria = CriteriaOperator.And(
                new ContainsOperator("OrganizationCategories",
                    CriteriaOperator.And(
                        new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );

            SupplierListXDS.Criteria = criteria.ToString();
            grdSupplierList.DataBind();

        }
    }
}