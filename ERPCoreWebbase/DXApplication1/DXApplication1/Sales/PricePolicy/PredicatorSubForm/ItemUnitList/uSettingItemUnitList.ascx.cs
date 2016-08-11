using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.Sale.PricePolicy;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;
using WebModule.Sales.PricePolicy.PredicatorSubForm.Interface;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxLoadingPanel;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList
{
    public partial class uSettingItemUnitList : System.Web.UI.UserControl, IWizardPolicy
    {
        #region IWizardPolicy

        public void settingJavascript()
        {
            if (RuleObject != null)
            {
                ButtonNextItemUnitSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Next');}";
                ButtonBackItemUnitSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Back');}";
                ButtonFinishItemUnitSetting.ClientSideEvents.Click = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('Save');}";

                ASPxLoadingPanel loadingPanel = new ASPxLoadingPanel();
                loadingPanel.ID = this.RuleObject.MainCpClientInstanceName + "loadingPanel";
                if (this.FindControl(loadingPanel.ID) == null)
                {
                    loadingPanel.ClientInstanceName = loadingPanel.ID;
                    loadingPanel.LoadingDivStyle.BackColor = System.Drawing.Color.Transparent;
                    loadingPanel.Text = "Đang xử lý";
                    loadingPanel.Modal = true;
                    this.Controls.Add(loadingPanel);

                    cpSettingItemUnitList.ClientSideEvents.BeginCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Show(); }";
                    cpSettingItemUnitList.ClientSideEvents.EndCallback = "function(s, e){ " + loadingPanel.ClientInstanceName + ".Hide(); }";
                }
            }
        }

        public void settingInit(Guid pricePolicy, RuleCondition rule)
        {
            this.KeyValue = pricePolicy;
            this.RuleObject = rule;
            this.cpSettingItemUnitList.ClientInstanceName = rule.MainCpClientInstanceName;
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

        public void CRUD_Opening()
        {
            SelectionLST = RuleObject.LoadRuleCondition(session, KeyValue) as List<DataGrdItemUnitListSelection>;
            grdItemUnitListPreview.DataSource = SelectionLST;
            grdItemUnitListPreview.DataBind();
        }

        public void CRUD_Preview()
        {
            List<object> fieldValues = grdItemUnitList.GetSelectedFieldValues(new string[] { "ItemUnitId", "ItemId.Code", "ItemId.Name", "UnitId.Name" });
            SelectionLST = new List<DataGrdItemUnitListSelection>();
            foreach (object[] item in fieldValues)
            {
                DataGrdItemUnitListSelection o = new DataGrdItemUnitListSelection();
                /*2013-11-24 ERP-1118 Khoa.Truong MOD START*/
                //o.ItemUnitId = Guid.Parse(item[0].ToString());
                //o.ItemCode = item[1].ToString();
                //o.ItemName = item[2].ToString();
                //o.UnitName = item[3].ToString();
                o.ItemUnitId = item[0] != null ? Guid.Parse(item[0].ToString()) : Guid.Empty;
                o.ItemCode = item[1] != null ? item[1].ToString() : String.Empty;
                o.ItemName = item[2] != null ? item[2].ToString() : String.Empty;
                o.UnitName = item[3] != null ? item[3].ToString() : String.Empty;
                /*2013-11-24 ERP-1118 Khoa.Truong MOD END*/
                SelectionLST.Add(o);
            }

            grdItemUnitListPreview.DataSource = SelectionLST;
            grdItemUnitListPreview.DataBind();
        }

        public void CRUD_Saving()
        {
            if (ChkSelectedItemUnitAll.Checked)
            {
                SelectionLST = new List<DataGrdItemUnitListSelection>();
                DataGrdItemUnitListSelection o = new DataGrdItemUnitListSelection();
                NAS.DAL.Nomenclature.Item.ItemUnit.Populate();
                NAS.DAL.Nomenclature.Item.ItemUnit iu =
                    session.FindObject<NAS.DAL.Nomenclature.Item.ItemUnit>(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL, BinaryOperatorType.Equal));
                o.ItemUnitId = iu.ItemUnitId;
                SelectionLST.Add(o);
            }

            RuleObject.SaveRuleCondition(session, KeyValue, SelectionLST);
        }

        public void UpdateGUI_Opening()
        {
            popup_settingItemUnitList.ShowOnPageLoad = true;
            popup_settingItemUnitList.HeaderText = this.RuleObject.Description;
            ButtonFinishItemUnitSetting.Visible = false;
            ButtonBackItemUnitSetting.Visible = false;
            ButtonNextItemUnitSetting.Visible = true;
            pcSettingItemUnitList.TabPages[0].ClientEnabled = true;
            pcSettingItemUnitList.TabPages[1].ClientEnabled = false;
            pcSettingItemUnitList.ActiveTabIndex = 0;
            UpdateGUI_LoadSelection();
        }

        public void UpdateGUI_SelectUnselectAll()
        {
            if (ChkSelectedItemUnitAll.Checked)
            {
                grdItemUnitList.Selection.SelectAll();
            }
            else
            {
                grdItemUnitList.Selection.UnselectAll();
            }

            popup_settingItemUnitList.ShowOnPageLoad = true;
            popup_settingItemUnitList.HeaderText = this.RuleObject.Description;
            ButtonFinishItemUnitSetting.Visible = false;
            ButtonBackItemUnitSetting.Visible = false;
            ButtonNextItemUnitSetting.Visible = true;
            pcSettingItemUnitList.TabPages[0].ClientEnabled = true;
            pcSettingItemUnitList.TabPages[1].ClientEnabled = false;
            pcSettingItemUnitList.ActiveTabIndex = 0;
        }

        public void UpdateGUI_LoadSelection()
        {
            grdItemUnitList.Selection.UnselectAll();
            if (SelectionLST == null)
                return;

            if (SelectionLST.Count == 1 && SelectionLST[0].RowStatus == Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL)
            {
                grdItemUnitList.Selection.SelectAll();
            }
            else
                foreach (DataGrdItemUnitListSelection d in SelectionLST)
                    grdItemUnitList.Selection.SetSelectionByKey(d.ItemUnitId, true);

            if (this.grdItemUnitList.Selection.Count == grdItemUnitList.VisibleRowCount)
                ChkSelectedItemUnitAll.Checked = true;
            else
                ChkSelectedItemUnitAll.Checked = false;
        }

        public void UPdateGUI_Preview()
        {
            popup_settingItemUnitList.ShowOnPageLoad = true;
            popup_settingItemUnitList.HeaderText = this.RuleObject.Description;
            ButtonFinishItemUnitSetting.Visible = true;
            ButtonBackItemUnitSetting.Visible = true;
            ButtonNextItemUnitSetting.Visible = false;
            pcSettingItemUnitList.TabPages[0].ClientEnabled = false;
            pcSettingItemUnitList.TabPages[1].ClientEnabled = true;
            pcSettingItemUnitList.ActiveTabIndex = 1;
        }

        public void UPdateGUI_Saving()
        {
            popup_settingItemUnitList.ShowOnPageLoad = false;
        }

        #endregion

        #region Properties

        private ASPxCheckBox ChkSelectedItemUnitAll
        {
            get
            {
                return grdItemUnitList.FindFilterCellTemplateControl(grdItemUnitList.Columns[0], "chkItemUnitAll") as ASPxCheckBox;
            }

            set
            {
                ASPxCheckBox c = grdItemUnitList.FindFilterCellTemplateControl(grdItemUnitList.Columns[0], "chkItemUnitAll") as ASPxCheckBox;
                c = value;
            }
        }

        public List<DataGrdItemUnitListSelection> SelectionLST
        {
            get
            {
                return Session["uSettingItemUnitList_SelectionLST" + this.ClientID + this.Session.SessionID] as List<DataGrdItemUnitListSelection>;
            }

            set
            {
                Session["uSettingItemUnitList_SelectionLST" + this.ClientID + this.Session.SessionID] = value;
            }

        }

        public ASPxButton ButtonFinishItemUnitSetting
        {
            get
            {
                ASPxButton button = popup_settingItemUnitList.FindControl("buttonFinishItemUnitSetting") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonNextItemUnitSetting
        {
            get
            {
                ASPxButton button = popup_settingItemUnitList.FindControl("buttonNextItemUnitSetting") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonBackItemUnitSetting
        {
            get
            {
                ASPxButton button = popup_settingItemUnitList.FindControl("buttonBackItemUnitSetting") as ASPxButton;
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
                this.cpSettingItemUnitList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.cpSettingItemUnitList.ClientID;
                this.grdItemUnitList.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + this.grdItemUnitList.ClientID;
            }
            if (RuleObject != null)
                cpSettingItemUnitList.ClientInstanceName = RuleObject.MainCpClientInstanceName;
            session = XpoHelper.GetNewSession();
            ItemUnitListXDS.Session = session;
            settingJavascript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GUIContext == null)
            {
                GUIContext = new NAS.GUI.Pattern.Context();
            }

            grdItemUnitListPreview.DataSource = SelectionLST;
            grdItemUnitListPreview.DataBind();
        }

        protected void cpSettingItemUnitList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string trs = e.Parameter.ToString();
            switch (trs)
            {
                case NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition.OPEN_TRANSIT:
                    GUIContext.State = new PricePolicy.PredicatorSubForm.ItemUnitList.State.RuleConditionItemUnitOpening(this);
                    break;
                case "SelectUnselectAll":
                case "Next":
                case "Back":
                case "Save":
                    GUIContext.Request(trs, this);
                    break;
            }
        }

        protected void chkItemUnitAll_Init(object sender, EventArgs e)
        {
            if (RuleObject != null)
            {
                ASPxCheckBox c = sender as ASPxCheckBox;
                c.ClientInstanceName = this.Parent.Parent.Parent.Parent.Parent.ClientID + c.ClientID;
                c.ClientSideEvents.CheckedChanged = "function(s, e){" + this.RuleObject.MainCpClientInstanceName + ".PerformCallback('SelectUnselectAll');}";
                grdItemUnitList.ClientSideEvents.SelectionChanged = "function(s, e){ if(e.isChangedOnServer) return; if (!e.isSelected) {" + c.ClientInstanceName + ".SetChecked(false);  }}";
            }
           
        }
    }
}