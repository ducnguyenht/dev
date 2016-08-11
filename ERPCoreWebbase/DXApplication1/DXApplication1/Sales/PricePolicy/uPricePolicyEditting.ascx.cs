using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Web.ASPxEditors;
using System.Threading;
using NAS.DAL;
using NAS.BO.Sale.PricePolicy;
using WebModule.Sales.PricePolicy.PredicatorSubForm;
using NAS.BO.Sale.PricePolicy.RulesWiz.Rules;
using NAS.BO.Sale.PricePolicy.RulesWiz.Predicator;
using System.Text;
using WebModule.UserControls;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Sales.PricePolicy
{
    public partial class uPricePolicyEditting : System.Web.UI.UserControl
    {
        #region properties

        public List<dynamic> DemoPriceCOGS
        {
            get
            {
                if (Session["DemoPriceCOGS_COGSData" + this.ClientID + this.Session.SessionID] == null)
                    return null;
                return Session["DemoPriceCOGS_COGSData" + this.ClientID + this.Session.SessionID] as List<dynamic>;
            }

            set
            {
                Session["DemoPriceCOGS_COGSData" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        public List<COGS> COGSData
        {
            get
            {
                if (Session["uPricePolicyEditting_COGSData" +  this.ClientID + this.Session.SessionID] == null)
                    return null;
                return Session["uPricePolicyEditting_COGSData" + this.ClientID + this.Session.SessionID] as List<COGS>;
            }

            set
            {
                Session["uPricePolicyEditting_COGSData" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        public Rule RuleObject
        {
            set
            {
                Session["RuleObject" + this.ClientID + this.Session.SessionID] = value;
            }
            get
            {
                return Session["RuleObject" + this.ClientID + this.Session.SessionID] as Rule;
            }
        }

        private System.Web.UI.UserControl UBuildingConditionMenu
        {
            get
            {
                return uBuildingMenuConstruction1;
            }
        }

        private System.Web.UI.UserControl UBuildingExceptionMenu
        {
            get
            {
                return uBuildingMenuConstruction2;
            }
        }

        private ASPxButton ButtonHelp
        {
            get
            {
                ASPxButton button = popupPricePolicyEditting.FindControl("buttonHelp") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonFinish
        {
            get
            {
                ASPxButton button = popupPricePolicyEditting.FindControl("buttonFinish") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonNext
        {
            get
            {
                ASPxButton button = popupPricePolicyEditting.FindControl("buttonNext") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonBack
        {
            get
            {
                ASPxButton button = popupPricePolicyEditting.FindControl("buttonBack") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonSave
        {
            get
            {
                ASPxButton button = popupPricePolicyEditting.FindControl("buttonSave") as ASPxButton;
                return button;
            }
        }

        private NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session["uPricePolicyEditting_context" + this.ClientID + this.Session.SessionID] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session["uPricePolicyEditting_context" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private Guid PricePolicyId {
            get {
                if (Session["PricePolicyId" + this.ClientID + this.Session.SessionID] == null)
                    return Guid.Empty;
                return  Guid.Parse(Session["PricePolicyId" + this.ClientID + this.Session.SessionID].ToString());
            }
            set {
                Session["PricePolicyId" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        private NAS.DAL.Sales.Price.PricePolicy PricePolicy
        {
            get
            {
                return Session["NAS.DAL.Sales.Price.PricePolicy" + this.ClientID + this.Session.SessionID] as NAS.DAL.Sales.Price.PricePolicy;
            }
            set
            {
                Session["NAS.DAL.Sales.Price.PricePolicy" + this.ClientID + this.Session.SessionID] = value;
            }
        }

        public Session session
        {
            get;
            set;
        }

        #endregion

        #region State Pattern and GUI/CRUD/PreTransit

        //mode Adding -- start
        public bool PreTransitionCRUD_ConfigPrice(out string errorMessage)
        {
            this.uEvaluantCalculator1.getFormToPriceCaculator();
            bool rs = this.uEvaluantCalculator1.PriceCaculator.checkIsValid(out errorMessage);
            if (!rs)
            {
                cpPricePolicyEditting.JSProperties.Add("cpTxtFormulaExpress", true);
            }
            return rs;
        }

        public void UpdateGUI_resetForm()
        {
            txtPricePolicyCode.Text = string.Empty;
            txtPricePolicyName.Text = string.Empty;
            txtValidFrom.Text = string.Empty;
            txtValidTo.Text = string.Empty;
            cboPricePolicyType.Text = string.Empty;
            cboPricePolicyType.SelectedItem = null;
            cboRowStatus.Text = string.Empty;
            cboRowStatus.SelectedItem = null;
            txtDescription.Text = string.Empty;

            ASPxCheckBox chkApplyManufacturer = navBarCondition.Groups[0].FindControl("chkApplyManufacturer")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarCondition.Groups[1].FindControl("chkApplySupplier")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarCondition.Groups[2].FindControl("chkApplyItemUnit")
                                            as ASPxCheckBox;

            chkApplyManufacturer.Checked = false;
            chkApplySupplier.Checked = false;
            chkApplyItemUnit.Checked = false;
        }

        public void UpdateGUI_InitAdding()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 0;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            popupPricePolicyEditting.HeaderText = string.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;

            UpdateGUI_resetForm();

            txtPricePolicyCode.IsValid = true;
            txtPricePolicyName.IsValid = true;
            txtDescription.IsValid = true;
            txtValidFrom.IsValid = true;
            txtValidTo.IsValid = true;
            cboPricePolicyType.IsValid = true;
            cboRowStatus.IsValid = true;

            txtPricePolicyCode.Focus();
        }

        public void UpdateGUI_TabCondition()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 1;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = true;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            /////////////////////////////////
            ASPxCheckBox chkApplyManufacturer = navBarCondition.Groups[0].FindControl("chkApplyManufacturer")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarCondition.Groups[1].FindControl("chkApplySupplier")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarCondition.Groups[2].FindControl("chkApplyItemUnit")
                                            as ASPxCheckBox;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                chkApplySupplier.Checked = false;
            else
                chkApplySupplier.Checked = true;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                chkApplyManufacturer.Checked = false;
            else
                chkApplyManufacturer.Checked = true;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                chkApplyItemUnit.Checked = false;
            else
                chkApplyItemUnit.Checked = true;
            /////////////////////////////////
            uBuildingMenuConstruction buildingForm = UBuildingConditionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.CONDITION, RuleObject.Conditions, PricePolicyId);
        }

        public void UpdateGUI_TabException()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 2;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = true;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;


            //////////////////////////////////////////////////////
            ASPxCheckBox chkApplyManufacturer = navBarException.Groups[0].FindControl("chkApplyManufacturerException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarException.Groups[1].FindControl("chkApplySupplierException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarException.Groups[2].FindControl("chkApplyItemUnitException")
                                            as ASPxCheckBox;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                chkApplySupplier.Checked = false;
            else
                chkApplySupplier.Checked = true;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                chkApplyManufacturer.Checked = false;
            else
                chkApplyManufacturer.Checked = true;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                chkApplyItemUnit.Checked = false;
            else
                chkApplyItemUnit.Checked = true;
            uBuildingMenuConstruction buildingForm = UBuildingExceptionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.EXCEPTION, RuleObject.Exceptions, PricePolicyId);
        }

        public void UpdateGUI_TabConfigPrice()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 3;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = true;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            if (PricePolicy.RowStatus > Utility.Constant.ROWSTATUS_TEMP)
                ButtonSave.Visible = true;
            else
                ButtonSave.Visible = false;
        }

        public void UpdateGUI_TabDemoPrice()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 4;
            grdItemUnitListReviewPolicy.DataSource = null;
            grdItemUnitListReviewPolicy.DataBind();
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = true;
            ButtonNext.Visible = false;
            if (PricePolicy.RowStatus > Utility.Constant.ROWSTATUS_TEMP)
                ButtonFinish.Visible = false;
            else
                ButtonFinish.Visible = true;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
        }

        public void UpdateGUI_PoupClosing()
        {
            popupPricePolicyEditting.ShowOnPageLoad = false;
        }

        public void UpdateGUI_Saving()
        {
            popupPricePolicyEditting.ShowOnPageLoad = false;
            UpdateGUI_resetForm();
        }

        public void UpdateGUI_TabCommonInfo()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 0;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            if (PricePolicy.RowStatus > Utility.Constant.ROWSTATUS_TEMP)
                ButtonSave.Visible = true;
            else
                ButtonSave.Visible = false;

            txtPricePolicyCode.Focus();
        }

        public void UpdateGUI_ConditionMenuUpdating()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 1;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = true;
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            getSelectedPredicatorConditionForm();
            uBuildingMenuConstruction buildingForm = UBuildingConditionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.CONDITION, RuleObject.Conditions, PricePolicyId);
        }

        public void UpdateGUI_ExceptionMenuUpdating()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 2;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonNext.Visible = true;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            getSelectedPredicatorExceptionForm();
            uBuildingMenuConstruction buildingForm = UBuildingExceptionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.EXCEPTION, RuleObject.Exceptions, PricePolicyId);
        }

        public void CRUD_initAdding()
        {
            try
            {
                RuleObject = new Rule();
                PricePolicyBO bo = new PricePolicyBO();
                PricePolicy = bo.addDefaultPricePolicy(session);
                PricePolicyId = PricePolicy.PricePolicyId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CRUD_Saving()
        {
            try
            {
                PricePolicyBO bo = new PricePolicyBO();
                bo.SavingPricePolicy(
                    session,
                    PricePolicyId,
                    txtPricePolicyCode.Text.Trim(),
                    txtDescription.Text.Trim(),
                    txtPricePolicyName.Text.Trim(),
                    1,
                    txtValidFrom.Date,
                    txtValidFrom.Date,
                    short.Parse(cboRowStatus.SelectedItem.Value.ToString()),
                    Guid.Parse(cboPricePolicyType.SelectedItem.Value.ToString()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CRUD_TabCommonInfoSaving()
        {
            try
            {
                PricePolicyBO bo = new PricePolicyBO();
                bo.SavingPricePolicy(
                    session,
                    PricePolicyId,
                    txtPricePolicyCode.Text.Trim(),
                    txtDescription.Text.Trim(),
                    txtPricePolicyName.Text.Trim(),
                    1,
                    txtValidFrom.Date,
                    txtValidFrom.Date,
                    short.Parse(cboRowStatus.SelectedItem.Value.ToString()),
                    Guid.Parse(cboPricePolicyType.SelectedItem.Value.ToString()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CRUD_TabConfigPrice()
        {
            PricePolicyBO bo = new PricePolicyBO();
            List<TaxTypeSelection> TaxTypeSelections = new List<TaxTypeSelection>();
            NAS.DAL.Sales.Price.PriceCaculator pc = bo.LoadFormulaPricePolicy(session, PricePolicyId, ref TaxTypeSelections);
            this.uEvaluantCalculator1.resetForm();
            if (pc != null)
                this.uEvaluantCalculator1.settingInit(Encoding.ASCII.GetString(pc.PriceExpression), TaxTypeSelections);
        }

        public void CRUD_TabConfigPriceSaving()
        {
            PricePolicyBO bo = new PricePolicyBO();
            byte[] arr = Encoding.ASCII.GetBytes(this.uEvaluantCalculator1.PriceCaculator.ExpressionStr);
            bo.SaveFormulaPricePolicy(session,
                PricePolicyId,
                arr,
                this.uEvaluantCalculator1.PriceCaculator.TaxTypeSelections);
        }

        public void CRUD_TabDemoPrice()
        {
            PricePolicyBO bo = new PricePolicyBO();
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
            {
                byte[] arr = Encoding.ASCII.GetBytes(this.uEvaluantCalculator1.PriceCaculator.ExpressionStr);
                bo.SaveFormulaPricePolicy(session,
                    PricePolicyId,
                    arr,
                    this.uEvaluantCalculator1.PriceCaculator.TaxTypeSelections);
            }
            this.loadRuleObjectInEditting();
        }
        //mode Adding  -- end

        //mode Editting -- start
        public void UpdateGUI_ClickTabCommonEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 0;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = true;
            txtPricePolicyCode.Focus();
        }

        public void UpdateGUI_ClickTabConditionEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 1;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            /////////////////////////////////
            ASPxCheckBox chkApplyManufacturer = navBarCondition.Groups[0].FindControl("chkApplyManufacturer")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarCondition.Groups[1].FindControl("chkApplySupplier")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarCondition.Groups[2].FindControl("chkApplyItemUnit")
                                            as ASPxCheckBox;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                chkApplySupplier.Checked = false;
            else
                chkApplySupplier.Checked = true;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                chkApplyManufacturer.Checked = false;
            else
                chkApplyManufacturer.Checked = true;

            if (RuleObject.Conditions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                chkApplyItemUnit.Checked = false;
            else
                chkApplyItemUnit.Checked = true;
            /////////////////////////////////
            uBuildingMenuConstruction buildingForm = UBuildingConditionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.CONDITION, RuleObject.Conditions, PricePolicyId);
        }

        public void UpdateGUI_ClickTabExceptionEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 2;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;


            //////////////////////////////////////////////////////
            ASPxCheckBox chkApplyManufacturer = navBarException.Groups[0].FindControl("chkApplyManufacturerException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarException.Groups[1].FindControl("chkApplySupplierException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarException.Groups[2].FindControl("chkApplyItemUnitException")
                                            as ASPxCheckBox;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                chkApplySupplier.Checked = false;
            else
                chkApplySupplier.Checked = true;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                chkApplyManufacturer.Checked = false;
            else
                chkApplyManufacturer.Checked = true;

            if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                chkApplyItemUnit.Checked = false;
            else
                chkApplyItemUnit.Checked = true;
            uBuildingMenuConstruction buildingForm = UBuildingExceptionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.EXCEPTION, RuleObject.Exceptions, PricePolicyId);
        }

        public void UpdateGUI_ClickTabConfigPriceEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 3;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = true;
        }

        public void UpdateGUI_ClickTabDemoPriceEditting() {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 4;
            grdItemUnitListReviewPolicy.DataSource = null;
            grdItemUnitListReviewPolicy.DataBind();
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
        }

        public void UpdateGUI_InitEditting()
        {
            //pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            //pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = false;
            //pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = false;
            //pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = false;
            //pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = false;
            //pcWizardCreaterPricePolicy.ActiveTabIndex = 0;
            //popupPricePolicyEditting.ShowOnPageLoad = true;
            //popupPricePolicyEditting.HeaderText = string.Format("Chính Sách Giá Bán - {0}", this.PricePolicy.Code);
            //ButtonBack.Visible = false;
            //ButtonNext.Visible = true;
            //ButtonFinish.Visible = false;
            //ButtonHelp.Visible = true;
            //ButtonSave.Visible = true;

            //txtPricePolicyCode.IsValid = true;
            //txtPricePolicyName.IsValid = true;
            //txtDescription.IsValid = true;
            //txtValidFrom.IsValid = true;
            //txtValidTo.IsValid = true;
            //cboPricePolicyType.IsValid = true;
            //cboRowStatus.IsValid = true;

            ////loading data to form
            //txtPricePolicyCode.Text = PricePolicy.Code;
            //txtPricePolicyName.Text = PricePolicy.Name;
            //txtDescription.Text = PricePolicy.Description;
            //txtValidFrom.Date = PricePolicy.ValidFrom;
            //txtValidTo.Date = PricePolicy.ValidTo;
            //txtPricePolicyCode.Focus();
            //try
            //{
            //    cboPricePolicyType.Value = PricePolicy.PricePolicyTypeId.PricePolicyTypeId;
            //    cboPricePolicyType.Text = PricePolicy.PricePolicyTypeId.Name;
            //    cboRowStatus.Value = PricePolicy.RowStatus;
            //    cboRowStatus.Text = PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ? "Sử dụng" : "Tạm ngưng";
            //}
            //catch (Exception)
            //{ 

            //}

            UpdateGUI_TabCondition();
            UpdateGUI_TabException();
            UpdateGUI_TabConfigPrice();
            UpdateGUI_TabDemoPrice();

            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 0;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            popupPricePolicyEditting.HeaderText = string.Format("Chính Sách Giá Bán - {0}", this.PricePolicy.Code);

            txtPricePolicyCode.IsValid = true;
            txtPricePolicyName.IsValid = true;
            txtDescription.IsValid = true;
            txtValidFrom.IsValid = true;
            txtValidTo.IsValid = true;
            cboPricePolicyType.IsValid = true;
            cboRowStatus.IsValid = true;

            //loading data to form
            txtPricePolicyCode.Text = PricePolicy.Code;
            txtPricePolicyName.Text = PricePolicy.Name;
            txtDescription.Text = PricePolicy.Description;
            txtValidFrom.Date = PricePolicy.ValidFrom;
            txtValidTo.Date = PricePolicy.ValidTo;
            txtPricePolicyCode.Focus();
            try
            {
                cboPricePolicyType.Value = PricePolicy.PricePolicyTypeId.PricePolicyTypeId;
                cboPricePolicyType.Text = PricePolicy.PricePolicyTypeId.Name;
                cboRowStatus.Value = PricePolicy.RowStatus;
                cboRowStatus.Text = PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ? "Sử dụng" : "Tạm ngưng";
            }
            catch (Exception)
            {

            }

            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = true;
        }

        public void UpdateGUI_ConditionMenuUpdatingEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 1;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonBack.Visible = false;
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            getSelectedPredicatorConditionForm();
            uBuildingMenuConstruction buildingForm = UBuildingConditionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.CONDITION, RuleObject.Conditions, PricePolicyId);
        }

        public void UpdateGUI_ExceptionMenuUpdatingEditting()
        {
            pcWizardCreaterPricePolicy.TabPages[0].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[1].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[2].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[3].ClientEnabled = true;
            pcWizardCreaterPricePolicy.TabPages[4].ClientEnabled = true;
            pcWizardCreaterPricePolicy.ActiveTabIndex = 2;
            popupPricePolicyEditting.ShowOnPageLoad = true;
            if (PricePolicy.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", PricePolicy.Code);
            else
                popupPricePolicyEditting.HeaderText = String.Format("Chính Sách Giá Bán - {0}", "Thêm Mới");
            ButtonNext.Visible = false;
            ButtonFinish.Visible = false;
            ButtonBack.Visible = false;
            ButtonHelp.Visible = true;
            ButtonSave.Visible = false;
            getSelectedPredicatorExceptionForm();
            uBuildingMenuConstruction buildingForm = UBuildingExceptionMenu as uBuildingMenuConstruction;
            buildingForm.settingInit(uBuildingMenuConstruction.TYPEOFFORM.EXCEPTION, RuleObject.Exceptions, PricePolicyId);
        }

        public void CRUD_initEditting()
        {
            //try
            //{
            //    PricePolicy = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(PricePolicyId);
            //    NAS.DAL.Nomenclature.Item.ItemUnit.Populate();
            //    loadRuleObjectInEditting();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            try
            {
                PricePolicy = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(PricePolicyId);
                NAS.DAL.Nomenclature.Item.ItemUnit.Populate();
                loadRuleObjectInEditting();
                CRUD_TabConfigPrice();
                CRUD_TabDemoPrice();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //mode Editting  -- end

        #endregion

        public void loadRuleObjectInEditting()
        {
            RuleObject = new Rule();
            PricePolicyBO bo = new PricePolicyBO();
            
            ///////////////////////////////
            List<DataGrdSupplierListSelection> list = bo.loadSupplierConditionInPolicy(session, PricePolicyId);
            if (list != null && list.Count > 0) {
                RuleConditionSupplier r = new RuleConditionSupplier();
                r.Value = list;
                r.settingRuleCondition(
                    @"Áp dụng cho hàng hóa thuộc", 
                    @"Nhà cung cấp", 
                    "cpSupplierConditionSetting", 
                    KEYTYPE.SUPPLIER, 
                    @"Thiết lập điều kiện áp dụng đối với nhà cung cấp");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                    RuleObject.Conditions.Add(r);
            }

            list = bo.loadSupplierExceptionInPolicy(session, PricePolicyId);
            if (list != null && list.Count > 0)
            {
                RuleExceptionSupplier r = new RuleExceptionSupplier();
                r.Value = list;
                r.settingRuleCondition(
                    @"Loại trừ hàng hóa thuộc", 
                    @"Nhà cung cấp", 
                    "cpSupplierExceptionSetting", 
                    KEYTYPE.SUPPLIER,
                    @"Thiết lập điều kiện loại trừ đối với nhà cung cấp");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                    RuleObject.Exceptions.Add(r);
            }
            //////////////////////////////////

            List<DataGrdManufacturerListSelection> list2 = bo.loadManufacturerConditionInPolicy(session, PricePolicyId);
            if (list2 != null && list2.Count > 0)
            {
                RuleConditionManufacturer r = new RuleConditionManufacturer();
                r.Value = list2;
                r.settingRuleCondition(
                    @"Áp dụng cho hàng hóa thuộc", 
                    @"Nhà sản xuất", 
                    "cpManufactuerConditionSetting", 
                    KEYTYPE.MANUFACTURER,
                    @"Thiết lập điều kiện áp dụng đối với nhà sản xuất");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                    RuleObject.Conditions.Add(r);
            }

            list2 = bo.loadManufacturerExceptionInPolicy(session, PricePolicyId);
            if (list2 != null && list2.Count > 0)
            {
                RuleExceptionManufacturer r = new RuleExceptionManufacturer();
                r.Value = list2;
                r.settingRuleCondition(
                    @"Loại trừ hàng hóa thuộc", 
                    @"Nhà sản xuất", 
                    "cpManufactuerExceptionSetting", 
                    KEYTYPE.MANUFACTURER,
                    @"Thiết lập điều kiện loại trừ đối với nhà sản xuất");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                    RuleObject.Exceptions.Add(r);
            }
            ////////////////////////////////

            List<DataGrdItemUnitListSelection> list3 = bo.loadItemUnitConditionInPolicy(session, PricePolicyId);
            if (list3 != null && list3.Count > 0)
            {
                RuleConditionItemUnit r = new RuleConditionItemUnit();
                r.Value = list3;
                r.settingRuleCondition(
                    @"Áp dụng cho các", 
                    @"Hàng hóa", 
                    "cpItemUnitConditionSetting",
                    KEYTYPE.ITEMUNIT, 
                    @"Thiết lập điều kiện áp dụng đối với hàng hóa");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                    RuleObject.Conditions.Add(r);
            }

            list3 = bo.loadItemUnitExceptionInPolicy(session, PricePolicyId);
            if (list3 != null && list3.Count > 0)
            {
                RuleExceptionItemUnit r = new RuleExceptionItemUnit();
                r.Value = list3;
                r.settingRuleCondition(
                    @"Loại trừ các", 
                    @"Hàng hóa", 
                    "cpItemUnitExceptionSetting", 
                    KEYTYPE.ITEMUNIT,
                    @"Thiết lập điều kiện loại trừ đối với hàng hóa");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                    RuleObject.Exceptions.Add(r);
            }
        }

        public void getSelectedPredicatorConditionForm()
        {
            ASPxCheckBox chkApplyManufacturer = navBarCondition.Groups[0].FindControl("chkApplyManufacturer")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarCondition.Groups[1].FindControl("chkApplySupplier")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarCondition.Groups[2].FindControl("chkApplyItemUnit")
                                            as ASPxCheckBox;

            if (chkApplySupplier.Checked)
            {
                RuleConditionSupplier r = new RuleConditionSupplier();
                r.settingRuleCondition(
                    @"Áp dụng cho hàng hóa thuộc", 
                    @"Nhà cung cấp", 
                    "cpSupplierConditionSetting", 
                    KEYTYPE.SUPPLIER,
                    @"Thiết lập điều kiện áp dụng cho nhà cung cấp");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                    RuleObject.Conditions.Add(r);
            }
            else
            {
                RuleCondition r = RuleObject.Conditions.FindByKeyType(KEYTYPE.SUPPLIER);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Conditions.Remove(r);
                }
            }

            if (chkApplyManufacturer.Checked)
            {
                RuleConditionManufacturer r = new RuleConditionManufacturer();
                r.settingRuleCondition(
                    @"Áp dụng cho hàng hóa thuộc", 
                    @"Nhà sản xuất", 
                    "cpManufactuerConditionSetting", 
                    KEYTYPE.MANUFACTURER,
                    @"Thiết lập điều kiện áp dụng cho nhà sản xuất");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                    RuleObject.Conditions.Add(r);
            }

            else
            {
                RuleCondition r = RuleObject.Conditions.FindByKeyType(KEYTYPE.MANUFACTURER);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Conditions.Remove(r);
                }
            }

            if (chkApplyItemUnit.Checked)
            {
                RuleConditionItemUnit r = new RuleConditionItemUnit();
                r.settingRuleCondition(
                    @"Áp dụng cho các", 
                    @"Hàng hóa", 
                    "cpItemUnitConditionSetting", 
                    KEYTYPE.ITEMUNIT,
                    @"Thiết lập điều kiện áp dụng cho hàng hóa");
                if (RuleObject.Conditions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                    RuleObject.Conditions.Add(r);
            }
            else
            {
                RuleCondition r = RuleObject.Conditions.FindByKeyType(KEYTYPE.ITEMUNIT);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Conditions.Remove(r);
                }
            }

        }

        public void getSelectedPredicatorExceptionForm()
        {
            ASPxCheckBox chkApplyManufacturer = navBarException.Groups[0].FindControl("chkApplyManufacturerException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplySupplier = navBarException.Groups[1].FindControl("chkApplySupplierException")
                                            as ASPxCheckBox;
            ASPxCheckBox chkApplyItemUnit = navBarException.Groups[2].FindControl("chkApplyItemUnitException")
                                            as ASPxCheckBox;

            if (chkApplySupplier.Checked)
            {
                RuleExceptionSupplier r = new RuleExceptionSupplier();
                r.settingRuleCondition(
                    @"Loại trừ hàng hóa thuộc", 
                    @"Nhà cung cấp", 
                    "cpSupplierExceptionSetting", 
                    KEYTYPE.SUPPLIER,
                    @"Áp dụng điều kiện loại trừ đối với nhà cung cấp");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.SUPPLIER) == null)
                    RuleObject.Exceptions.Add(r);
            }
            else
            {
                RuleCondition r = RuleObject.Exceptions.FindByKeyType(KEYTYPE.SUPPLIER);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Exceptions.Remove(r);
                }
            }

            if (chkApplyManufacturer.Checked)
            {
                RuleExceptionManufacturer r = new RuleExceptionManufacturer();
                r.settingRuleCondition(
                    @"Loại trừ hàng hóa thuộc", 
                    @"Nhà sản xuất", 
                    "cpManufactuerExceptionSetting", 
                    KEYTYPE.MANUFACTURER,
                    @"Áp dụng điều kiện loại trừ đối với nhà sản xuất");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.MANUFACTURER) == null)
                    RuleObject.Exceptions.Add(r);
            }
            else
            {
                RuleCondition r = RuleObject.Exceptions.FindByKeyType(KEYTYPE.MANUFACTURER);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Exceptions.Remove(r);
                }
            }

            if (chkApplyItemUnit.Checked)
            {
                RuleExceptionItemUnit r = new RuleExceptionItemUnit();
                r.settingRuleCondition(
                    @"Loại trừ các", 
                    @"Hàng hóa", 
                    "cpItemUnitExceptionSetting", 
                    KEYTYPE.ITEMUNIT,
                    @"Áp dụng điều kiện loại trừ đối với hàng hóa");
                if (RuleObject.Exceptions.FindByKeyType(KEYTYPE.ITEMUNIT) == null)
                    RuleObject.Exceptions.Add(r);
            }
            else
            {
                RuleCondition r = RuleObject.Exceptions.FindByKeyType(KEYTYPE.ITEMUNIT);
                if (r != null)
                {
                    r.RemoveRuleCondition(session, PricePolicyId);
                    RuleObject.Exceptions.Remove(r);
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RuleObject = null;
                GUIContext = null;
                COGSData = null;
                DemoPriceCOGS = null;
                PricePolicy = null;
            }
            session = XpoHelper.GetNewSession();
            PricePolicyTypeLstXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GUIContext = new NAS.GUI.Pattern.Context();
                PricePolicyBO bo = new PricePolicyBO();
                COGSData = bo.getCOGSInAllInventory(session);
            }
            grdItemUnitListSelectionTesting.DataSource = COGSData;
            grdItemUnitListSelectionTesting.DataBind();
        }

        protected void cpPricePolicyEditting_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];
            switch(trs)
            {
                case "Edit":
                    PricePolicyId = Guid.Parse(para[1]);
                    GUIContext.State = new PricePolicy.State.PricePolicyInitEditting(this);
                    break;
                case "New":
                    PricePolicyId = Guid.Empty;
                    GUIContext.State = new PricePolicy.State.PricePolicyInitAdding(this);
                    break;
                default:
                    GUIContext.Request(trs, this);
                    break;
            }

            if (trs.Equals("Save") || trs.Equals("SaveCommonInfo") || trs.Equals("SaveConfigPrice"))
                cpPricePolicyEditting.JSProperties.Add("cpIsSaved", true);
        }

        protected void grdItemUnitListReviewPolicy_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            PricePolicyBO bo = new PricePolicyBO();
            List<object> fieldValues = grdItemUnitListSelectionTesting.GetSelectedFieldValues(new string[] { "COGSId", "COGSPrice" });
            List<DemoPrice> list = new List<DemoPrice>();
            foreach (object[] item in fieldValues)
            {
                DemoPrice o = new DemoPrice();
                o.COGSId = Guid.Parse(item[0].ToString());
                list.Add(o);
            }

            DemoPriceCOGS = bo.getListOfDemoPrices(session, list, RuleObject, PricePolicyId);
            grdItemUnitListReviewPolicy.DataSource = DemoPriceCOGS;

            grdItemUnitListReviewPolicy.KeyFieldName = "cogsid".ToLower();
            if (DemoPriceCOGS != null && DemoPriceCOGS.Count > 0)
            {

                foreach (KeyValuePair<string, object> entry in ((DynamicPriceReview)DemoPriceCOGS[0]).Dictionary)
                {
                    GridViewDataTextColumn col = new GridViewDataTextColumn();
                    col.Caption = ((DynamicPriceReview)DemoPriceCOGS[0]).getCaption(entry.Key);
                    col.FieldName = entry.Key;
                    col.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    col.VisibleIndex = grdItemUnitListReviewPolicy.VisibleColumns.Count;
                    col.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    col.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;

                    if (col.FieldName.ToLower().Equals("cogsid"))
                        col.Visible = false;
                    grdItemUnitListReviewPolicy.Columns.Add(col);
                }
            }

            grdItemUnitListReviewPolicy.DataBind();
        }

        protected void grdItemUnitListReviewPolicy_Init(object sender, EventArgs e)
        {
            
        }

        protected void grdItemUnitListReviewPolicy_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.ListSourceRowIndex >= 0)
            {
                try
                {
                    e.Value = ((DynamicPriceReview)DemoPriceCOGS[e.ListSourceRowIndex]).Dictionary[e.Column.FieldName].ToString();
                }
                catch (Exception)
                {
                    e.Value = "Ngoài phạm vi áp dụng";
                }
            }
        }   
    }
}