using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Sale.PricePolicy;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;
using WebModule.Sales.PricePolicy.PredicatorSubForm.Interface;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxLoadingPanel;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList
{
    public partial class uSettingManufacturersList : System.Web.UI.UserControl, IWizardPolicy
    {
        #region IWizardPolicy

        public void settingJavascript()
        {
            if (RuleObject != null)
            {
                ButtonNextManufacturerSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Next');}";
                ButtonBackManufacturerSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Back');}";
                ButtonFinishManufacturerSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Save');}";

                ASPxLoadingPanel loadingPanel = new ASPxLoadingPanel();
                loadingPanel.ID = this.RuleObject.MainCpClientInstanceName + "loadingPanel";
                if (this.FindControl(loadingPanel.ID) == null)
                {
                    loadingPanel.ClientInstanceName = loadingPanel.ID;
                    loadingPanel.LoadingDivStyle.BackColor = System.Drawing.Color.Transparent;
                    loadingPanel.Text = "Đang xử lý";
                    loadingPanel.Modal = true;
                    this.Controls.Add(loadingPanel);

                    cpSettingManufacturerList.ClientSideEvents.BeginCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Show(); }";
                    cpSettingManufacturerList.ClientSideEvents.EndCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Hide(); }";
                }
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

        public void settingInit(Guid pricePolicy, RuleCondition rule)
        {
            this.KeyValue = pricePolicy;
            this.RuleObject = rule;
            this.cpSettingManufacturerList.ClientInstanceName = rule.MainCpClientInstanceName;
            settingJavascript();
        }

        public RuleCondition RuleObject
        {
            set
            {
                Session["RuleObject" + this.ClientID + this.Session.SessionID] = value;
            }
            get
            {
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

        #endregion

        #region State Pattern

        public void CRUD_Opening()
        {
            SelectionLST = RuleObject.LoadRuleCondition(session, KeyValue) as List<DataGrdManufacturerListSelection>;
            grdManufacturerListPreview.DataSource = SelectionLST;
            grdManufacturerListPreview.DataBind();
        }

        public void CRUD_Preview()
        {
            List<object> fieldValues = grdManufacturerList.GetSelectedFieldValues(new string[] { "OrganizationId", "Code", "Name" });
            SelectionLST = new List<DataGrdManufacturerListSelection>();
            foreach (object[] item in fieldValues)
            {
                DataGrdManufacturerListSelection o = new DataGrdManufacturerListSelection();
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

            grdManufacturerListPreview.DataSource = SelectionLST;
            grdManufacturerListPreview.DataBind();
        }

        public void CRUD_Saving()
        {
            if (ChkSelectedManufacturerAll.Checked)
            {
                SelectionLST = new List<DataGrdManufacturerListSelection>();
                DataGrdManufacturerListSelection o = new DataGrdManufacturerListSelection();
                NAS.DAL.Nomenclature.Organization.ManufacturerOrg.Populate();
                NAS.DAL.Nomenclature.Organization.ManufacturerOrg m =
                    session.FindObject<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>(
                        new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL, BinaryOperatorType.Equal));
                o.OrganizationId = m.OrganizationId;
                o.Code = m.Code;
                o.Name = m.Name;
                SelectionLST.Add(o);
            }

            RuleObject.SaveRuleCondition(session, KeyValue, SelectionLST);
        }

        public void UpdateGUI_Opening()
        {
            popup_settingManufacturersList.ShowOnPageLoad = true;
            popup_settingManufacturersList.HeaderText = this.RuleObject.Description;
            ButtonFinishManufacturerSetting.Visible = false;
            ButtonBackManufacturerSetting.Visible = false;
            ButtonNextManufacturerSetting.Visible = true;
            pcSettingManufacturerList.TabPages[0].ClientEnabled = true;
            pcSettingManufacturerList.TabPages[1].ClientEnabled = false;
            pcSettingManufacturerList.ActiveTabIndex = 0;
            UpdateGUI_LoadSelection();
        }

        public void UpdateGUI_LoadSelection()
        {
            grdManufacturerList.Selection.UnselectAll();
            if (SelectionLST == null)
                return;

            if (SelectionLST.Count == 1 && SelectionLST[0].Code.Equals(Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
            {
                grdManufacturerList.Selection.SelectAll();
            }
            else
                foreach (DataGrdManufacturerListSelection d in SelectionLST)
                {
                    grdManufacturerList.Selection.SetSelectionByKey(d.OrganizationId, true);
                }

            if (this.grdManufacturerList.Selection.Count == grdManufacturerList.VisibleRowCount)
                ChkSelectedManufacturerAll.Checked = true;
            else
                ChkSelectedManufacturerAll.Checked = false;
        }

        public void UPdateGUI_Preview()
        {
            popup_settingManufacturersList.ShowOnPageLoad = true;
            popup_settingManufacturersList.HeaderText = this.RuleObject.Description;
            ButtonFinishManufacturerSetting.Visible = true;
            ButtonBackManufacturerSetting.Visible = true;
            ButtonNextManufacturerSetting.Visible = false;
            pcSettingManufacturerList.TabPages[0].ClientEnabled = false;
            pcSettingManufacturerList.TabPages[1].ClientEnabled = true;
            pcSettingManufacturerList.ActiveTabIndex = 1;
        }

        public void UPdateGUI_Saving()
        {
            popup_settingManufacturersList.ShowOnPageLoad = false;
        }

        public void UpdateGUI_SelectUnSelectAll()
        {
            if (ChkSelectedManufacturerAll.Checked)
            {
                grdManufacturerList.Selection.SelectAll();
            }
            else
            {
                grdManufacturerList.Selection.UnselectAll();
            }

            popup_settingManufacturersList.ShowOnPageLoad = true;
            popup_settingManufacturersList.HeaderText = this.RuleObject.Description;
            ButtonFinishManufacturerSetting.Visible = false;
            ButtonBackManufacturerSetting.Visible = false;
            ButtonNextManufacturerSetting.Visible = true;
            pcSettingManufacturerList.TabPages[0].ClientEnabled = true;
            pcSettingManufacturerList.TabPages[1].ClientEnabled = false;
            pcSettingManufacturerList.ActiveTabIndex = 0;
        }

        #endregion

        #region Properties

        public List<DataGrdManufacturerListSelection> SelectionLST
        {
            get
            {
                return Session["uSettingManufacturersList_SelectionLST" + this.ClientID + this.Session.SessionID] as List<DataGrdManufacturerListSelection>;
            }

            set
            {
                Session["uSettingManufacturersList_SelectionLST" + this.ClientID + this.Session.SessionID] = value;
            }

        }

        private ASPxCheckBox ChkSelectedManufacturerAll
        {
            get
            {
                return grdManufacturerList.FindFilterCellTemplateControl(grdManufacturerList.Columns[0], "chkSelectedManufacturerAll") as ASPxCheckBox;
            }

            set
            {
                ASPxCheckBox c = grdManufacturerList.FindFilterCellTemplateControl(grdManufacturerList.Columns[0], "chkSelectedManufacturerAll") as ASPxCheckBox;
                c = value;
            }
        }

        public ASPxButton ButtonFinishManufacturerSetting
        {
            get
            {
                ASPxButton button = popup_settingManufacturersList.FindControl("buttonFinishManufacturerSetting") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonNextManufacturerSetting
        {
            get
            {
                ASPxButton button = popup_settingManufacturersList.FindControl("buttonNextManufacturerSetting") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonBackManufacturerSetting
        {
            get
            {
                ASPxButton button = popup_settingManufacturersList.FindControl("buttonBackManufacturerSetting") as ASPxButton;
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
                this.cpSettingManufacturerList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.cpSettingManufacturerList.ClientID;
                this.grdManufacturerList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.grdManufacturerList.ClientID;
            }

            if (RuleObject != null)
                cpSettingManufacturerList.ClientInstanceName = RuleObject.MainCpClientInstanceName;
            session = XpoHelper.GetNewSession();
            ManufacturerListXDS.Session = session;
            settingJavascript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GUIContext == null)
            {
                GUIContext = new NAS.GUI.Pattern.Context();
            }

            grdManufacturerListPreview.DataSource = SelectionLST;
            grdManufacturerListPreview.DataBind();
        }

        protected void cpSettingManufacturerList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string trs = e.Parameter.ToString();
            switch (trs)
            {
                case NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition.OPEN_TRANSIT:
                    GUIContext.State = new PricePolicy.PredicatorSubForm.ManufacturerList.State.RuleConditionManufacturerOpening(this);
                    break;
                case "SelectUnselectAll":
                case "Next":
                case "Back":
                case "Save":
                    GUIContext.Request(trs, this);
                    break;
            }
        }

        protected void ChkSelectedManufacturerAll_Init(object sender, EventArgs e)
        {
            if (RuleObject != null)
            {
                ASPxCheckBox c = sender as ASPxCheckBox;
                c.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + c.ClientID;
                c.ClientSideEvents.CheckedChanged = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('SelectUnselectAll');}";
                grdManufacturerList.ClientSideEvents.SelectionChanged = "function(s, e){ if(e.isChangedOnServer) return; if (!e.isSelected) {" + c.ClientInstanceName + ".SetChecked(false);  }}";
            }
        }
    }
}