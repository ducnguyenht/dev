using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Sale.PricePolicy.RulesWiz.Predicator;

namespace WebModule.Sales.PricePolicy.PredicatorSubForm
{
    public partial class uBuildingMenuConstruction : System.Web.UI.UserControl
    {
        public enum TYPEOFFORM
        {
            EXCEPTION, CONDITION
        }

        private Guid PricePolicyId
        {
            get
            {
                return Guid.Parse(Session["PricePolicyId"].ToString());
            }

            set
            {
                Session["PricePolicyId"] = value;
            }
        }

        private TYPEOFFORM typeForm;

        private List<NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition> AddedPredicatorConditionlst
        {
           get{
               return Session["AddedPredicatorConditionlst" + this.ClientID + this.Session.SessionID] as List<NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition>;
           }
            set {
                Session["AddedPredicatorConditionlst" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private List<NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition> AddedPredicatorExceptionlst
        {
            get
            {
                return Session["AddedPredicatorExceptionlst" + this.ClientID + this.Session.SessionID] as List<NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition>;
            }
            set
            {
                Session["AddedPredicatorExceptionlst" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session["uBuildingMenuConstruction_context" + this.ClientID + this.Session.SessionID] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session["uBuildingMenuConstruction_context" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void loadDynamicConditionForm()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].GetType() == typeof(ASPxTextBox)){
                    this.Controls.Remove(this.Controls[i]);
                }
            }
            int cnt = 0;
            if (AddedPredicatorConditionlst != null && AddedPredicatorConditionlst.Count != 0)
                foreach (NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition r in AddedPredicatorConditionlst)
                {
                    if (r is RuleConditionSupplier)
                    {
                        uSettingSuppliersList1.settingInit(PricePolicyId, r);
                    }

                    else if (r is RuleConditionManufacturer)
                    {
                        uSettingManufacturersList1.settingInit(PricePolicyId, r);
                    }

                    else if (r is RuleConditionItemUnit)
                    {
                        uSettingItemUnitList1.settingInit(PricePolicyId, r);
                    }

                    if (cnt > 0)
                        this.Controls.Add(new LiteralControl("Và <br />"));
                    addRuleConditionControls(r);
                    cnt++;
                }
        }

        private void loadDynamicExceptionForm()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {

                if (this.Controls[i].GetType() == typeof(ASPxTextBox))
                {
                    this.Controls.Remove(this.Controls[i]);
                }
            }
            int cnt = 0;
            if (AddedPredicatorExceptionlst != null && AddedPredicatorExceptionlst.Count != 0)
                foreach (NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition r in AddedPredicatorExceptionlst)
                {
                    if (r is RuleExceptionSupplier)
                    {
                        uSettingSuppliersList1.settingInit(PricePolicyId, r);
                    }

                    else if (r is RuleExceptionManufacturer)
                    {
                        uSettingManufacturersList1.settingInit(PricePolicyId, r);
                    }

                    else if (r is RuleExceptionItemUnit)
                    {
                        uSettingItemUnitList1.settingInit(PricePolicyId, r);
                    }

                    if (cnt > 0)
                        this.Controls.Add(new LiteralControl("Và <br />"));
                    addRuleExceptionControls(r);
                    cnt++;
                }
        }

        private void addRuleConditionControls(NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition item)
        {
            ASPxLabel label = new ASPxLabel();
            label.Text = item.LabelGUIText;
            ASPxHyperLink link = new ASPxHyperLink();
            link.Text = item.HyperlinkGUIText;
            link.ID = item.MainCpClientInstanceName + "ConditionLink";
            link.ClientInstanceName = item.MainCpClientInstanceName + "ConditionLink";
            link.Cursor = "pointer";
            link.Init += new EventHandler(link_init);
            this.Controls.Add(label);
            this.Controls.Add(new LiteralControl("&nbsp;"));
            this.Controls.Add(link);
            this.Controls.Add(new LiteralControl("<br/>"));
        }

        protected void link_init(object sender, EventArgs e)
        {
            ASPxHyperLink l = sender as ASPxHyperLink;
            string cpname = string.Empty;
            if (typeForm == TYPEOFFORM.CONDITION)
                cpname = l.ClientInstanceName.Replace("ConditionLink", string.Empty);
            else if (typeForm == TYPEOFFORM.EXCEPTION)
                cpname = l.ClientInstanceName.Replace("ExceptionLink", string.Empty);

            l.ClientSideEvents.Click = "function(s, e){" + cpname +
                ".PerformCallback('" + NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition.OPEN_TRANSIT + "');}";
        }

        private void addRuleExceptionControls(NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition item)
        {
            ASPxLabel label = new ASPxLabel();
            label.Text = item.LabelGUIText;
            ASPxHyperLink link = new ASPxHyperLink();
            link.Text = item.HyperlinkGUIText;
            link.ID = item.MainCpClientInstanceName + "ExceptionLink";
            link.ClientInstanceName = item.MainCpClientInstanceName + "ExceptionLink";
            link.Cursor = "pointer";
            link.Init += new EventHandler(link_init);
            this.Controls.Add(label);
            this.Controls.Add(new LiteralControl("&nbsp;"));
            this.Controls.Add(link);
            this.Controls.Add(new LiteralControl("<br/>"));
        }

        public void settingInit(TYPEOFFORM type, List<NAS.BO.Sale.PricePolicy.RulesWiz.Rules.RuleCondition> ExceptionOrConditionMenu, Guid pricePolicy)
        {
            typeForm = type;
            PricePolicyId = pricePolicy;
            switch (type)
            {
                case TYPEOFFORM.CONDITION:
                    AddedPredicatorConditionlst = ExceptionOrConditionMenu;
                    loadDynamicConditionForm();
                    break;
                case TYPEOFFORM.EXCEPTION:
                    AddedPredicatorExceptionlst = ExceptionOrConditionMenu;
                    loadDynamicExceptionForm();
                    break;
            }
        }
    }
}