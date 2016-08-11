using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Inventory;
using NAS.BO.Inventory.Jouranl;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxGridView;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.Inventory.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Nomenclature.Item;
using System.ComponentModel;
using DevExpress.Web.ASPxEditors;
using System.Collections;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Data.Filtering;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Lot;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;
using DevExpress.Web.ASPxClasses;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.Accounting.Period;
using NAS.ETLBO.System.Object;
using NAS.BO.Inventory.Command;

namespace WebModule.Warehouse
{
    public partial class InitInventory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_INITINVENTORY_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        // //////////////////////////////////////////////////////////////////////////

        InventoryTransactionBO itBO = new InventoryTransactionBO();
        InventoryTransactionBO bo = new InventoryTransactionBO();

        Guid m_ItemUnitId;
        Guid m_InventoryId;
        Guid m_AccountingPeriodId;

        Session session;

        public List<InitInventoryItemUnitObject> AllItemUnits
        {
            get
            {
                if (Session["AllItemUnits_InitInventory"] == null)
                    return new List<InitInventoryItemUnitObject>();
                return Session["AllItemUnits_InitInventory"] as List<InitInventoryItemUnitObject>;
            }
            set
            {
                Session["AllItemUnits_InitInventory"] = value;
            }
        }

        public List<InventoryBalancePriceBO> InventoryBalancePrices
        {
            get
            {
                if (Session["InventoryBalancePrices__InitInventory"] == null)
                    return new List<InventoryBalancePriceBO>();
                return Session["InventoryBalancePrices__InitInventory"] as List<InventoryBalancePriceBO>;
            }
            set
            {
                Session["InventoryBalancePrices__InitInventory"] = value;
            }
        }

        public int ActiveRowGrdataproduct
        {
            get
            {
                if (Session["ActiveRowGrdataproduct"] == null)
                    return -1;
                return int.Parse(Session["ActiveRowGrdataproduct"].ToString());
            }

            set
            {
                Session["ActiveRowGrdataproduct"] = value;
            }
        }

        public AccountingPeriod currentAP
        {
            get
            {
                if (Session["currentAP"] == null)
                {                    
                    currentAP = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    if (currentAP == null)
                        throw new Exception("Current accounting period is null");
                    return currentAP;
                }
                return Session["currentAP"] as AccountingPeriod;
            }
            set
            {
                Session["currentAP"] = value;
            }
        }


        protected void BindData()
        {
            //m_ItemUnitId = Guid.Parse(Session["ItemUnitId"].ToString());
            //m_InventoryId = Guid.Parse(Session["InventoryId"].ToString());

            InventoryJournalBalanceForwardXDS.CriteriaParameters.Add("ItemUnitId.ItemUnitId", System.Data.DbType.Guid, m_ItemUnitId.ToString());
            InventoryJournalBalanceForwardXDS.CriteriaParameters.Add("InventoryId.InventoryId", System.Data.DbType.Guid, m_InventoryId.ToString());
            InventoryJournalBalanceForwardXDS.CriteriaParameters.Add("InventoryTransactionId.AccountingPeriodId.AccountingPeriodId", System.Data.DbType.Guid, m_AccountingPeriodId.ToString());

            //CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
            //        new BinaryOperator("InventoryId.InventoryId", m_InventoryId, BinaryOperatorType.Equal),
            //        new BinaryOperator("ItemUnitId.ItemUnitId", m_ItemUnitId, BinaryOperatorType.Equal),
            //        new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));

            //InventoryJournalBalanceForward _inventoryJournalBalanceForward = session.FindObject<InventoryJournalBalanceForward>(_filter);
            //if (_inventoryJournalBalanceForward != null && _inventoryJournalBalanceForward.InventoryTransactionId.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE)
            //{
            //    if (_inventoryJournalBalanceForward.AccountId != null)
            //    {
            //        cboInitInventoryAccount.Value = _inventoryJournalBalanceForward.AccountId.Code;
            //    }

            //    if (_inventoryJournalBalanceForward.InventoryTransactionId != null)
            //    {
            //        txtInitInventoryCode.Text = _inventoryJournalBalanceForward.InventoryTransactionId.Code;
            //    }
            //}
            //else
            //{
            //    //grdBalanceOfItemsNoInventory.JSProperties.Clear();
            //    //grdBalanceOfItemsNoInventory.JSProperties.Add("cpEmptyControl", "empty");
            //}


            grdBalanceOfItemsNoInventory.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            dsInventory.Session = session;
            dsItemUnit.Session = session;
            dsItem.Session = session;

            InventoryJournalBalanceForwardXDS.Session = session;
            InventoryJournalBalanceForwardXDS.Criteria = "ItemUnitId.ItemUnitId = ? And InventoryId.InventoryId = ?  And InventoryTransactionId.AccountingPeriodId.AccountingPeriodId = ? And RowStatus >= 1";
        
            CriteriaOperator _filter = CriteriaOperator.And(                        
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal),
                        new BinaryOperator("CurrencyTypeId.IsMaster", true, BinaryOperatorType.Equal));
            Currency _currency = session.FindObject<Currency>(_filter);

            if (_currency != null)
            {
                lblCurrencyDefault.Text = _currency.Name;
            }

            
            
            //if (Page.IsPostBack)
            //{
            //if (Session["ItemUnitId"] != null)
            //{
            //InventoryJournalBalanceForwardXDS.CriteriaParameters.Add("ItemUnidId.ItemUnidId", System.Data.DbType.Guid, Convert.ToString(Session["ItemUnitId"]));
            //}
            //}



            dsInventory.CriteriaParameters["OrganizationId"].DefaultValue = "aab237b6-763c-484d-b1a2-0e040087940d";

         

            //if (Session["ItemUnitId"] == null)
            //{
            //    grdBalanceOfItemsNoInventory.DataSource = null;
            //}


        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblFirstDateOfAccountCycle.Text = lblFirstDateOfAccountCycle.Text.Replace("{0}", currentAP.FromDateTime.ToString("dd/MM/yyyy"));

                //try
                //{
                //    //lblFirstDateOfAccountCycle.Text = lblFirstDateOfAccountCycle.Text.Replace("{0}", AccountingPeriodBO.getCurrentAccountingPeriod(session).FromDateTime.ToString("dd/MM/yyyy"));
                //}
                //catch
                //{
                //    cpInitInventoryDetail.JSProperties.Clear();
                //    MessageBox1.Message.Text = String.Format("Chưa tạo chu kỳ tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year);
                //    cpInitInventoryDetail.JSProperties.Add("cpWarning", "Chưa tạo chu kỳ tháng " + DateTime.Now.Month.ToString());
                //}

                Session["InventorySelected"] = Guid.Empty; //"3f386a2b-c1b5-4766-b241-10431b34a8ed"; //Khoa mặc định   
        
                //cboInitInventoryAccount.ReadOnly = true;
                //txtInitInventoryCode.ReadOnly = true;

              
            }

            //System.Diagnostics.Debug.WriteLine(String.Format("Pageload InventoryId: {0}", Session["InventorySelected"].ToString()));

            Guid _accountPeriod = Guid.Empty;

            if (cboAccountPeriod.Value != null)
            {
                _accountPeriod = Guid.Parse(cboAccountPeriod.Value.ToString());
            }


            if (cboAccountPeriod.Value != null)
            {
                AllItemUnits = itBO.getAllItemUnits(session, Guid.Parse(Session["InventorySelected"].ToString()), _accountPeriod);
                if (AllItemUnits != null || AllItemUnits.Count == 0)
                {
                    Session["ItemUnitId"] = AllItemUnits[0].ItemUnitId;

                    this.grdataproduct.DataSource = AllItemUnits;               
                    grdataproduct.Focus();
                    //grdataproduct.FocusedRowIndex = 0;

                    if (!Page.IsPostBack)
                    {
                        grdataproduct.SettingsBehavior.AllowFocusedRow = false;
                        grdataproduct.SettingsBehavior.AllowFocusedRow = true;

                        grdataproduct.FocusedRowIndex = 0;
                    }
                }
            }

            string defaultKey = Guid.Parse(Session["InventorySelected"].ToString()).ToString().Replace("-", string.Empty);
            TreeListNode node = treeInventory.FindNodeByKeyValue(defaultKey);
            if (node != null)
            {
                Inventory _Inventory = (Inventory)node.DataItem;
                if (_Inventory != null)
                {
                    m_InventoryId = _Inventory.InventoryId;
                }

                m_ItemUnitId = (Guid)grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, "ItemUnitId");

                if (cboAccountPeriod.Value != null)
                {
                    m_AccountingPeriodId = Guid.Parse(cboAccountPeriod.Value.ToString());
                }
                node.Focus();
            }          

            //loadBalanceInfo(Guid.Parse(Session["InventorySelected"].ToString()), Guid.Parse(Session["ItemUnitId"].ToString()));                

            //BindData();
            BindData();

            this.grdataproduct.DataBind();
            

            //if (Page.IsPostBack)
            //{
            //    Inventory _Inventory = (Inventory)treeInventory.FocusedNode.DataItem;
            //    if (_Inventory != null)
            //    {
            //        m_InventoryId = _Inventory.InventoryId;
            //    }

            //    m_ItemUnitId = (Guid)grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, "ItemUnitId");

            //    BindData();
            //}
        }



        protected void treeInventory_SelectionChanged(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            Session["InventorySelected"] = treeList.GetSelectedNodes()[0].Key;
        }

        protected object GetMasterRowKeyValue(ASPxTreeList treeList)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = treeList;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;
        }

        protected void trlItemUnit_OnInit(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            object keyValue = GetMasterRowKeyValue(treeList);
            dsItemUnit.CriteriaParameters["ItemId"].DefaultValue = keyValue.ToString();
            treeList.DataBind();
        }

        protected void grdataproduct_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Inventory inventory = (Inventory)treeInventory.FocusedNode.DataItem;
            if (inventory != null)
            {
                Guid _accountPeriod = Guid.Empty;

                if (cboAccountPeriod.Value != null)
                {
                    _accountPeriod = Guid.Parse(cboAccountPeriod.Value.ToString());
                }


                System.Diagnostics.Debug.WriteLine(String.Format("InventoryId: {0}, Name: {1}", inventory.InventoryId, inventory.Name));
                Session["InventorySelected"] = inventory.InventoryId.ToString();
                AllItemUnits = itBO.getAllItemUnits(session, Guid.Parse(Session["InventorySelected"].ToString()), _accountPeriod);
                this.grdataproduct.DataSource = AllItemUnits;
                this.grdataproduct.DataBind();
            }
        }



        protected void loadBalanceInfo(Guid InventoryId, Guid ItemUnitId)
        {
            //Session["ItemUnitId"] = ItemUnitId;
            //try
            //{
            //    InventoryBalancePrices = bo.getInventoryBanlanceForward(session, InventoryId, ItemUnitId);
            //}
            //catch (Exception ex)
            //{

            //}
            ////grdBalanceOfItemsNoInventory.DataSource = InventoryBalancePrices;
            ////grdBalanceOfItemsNoInventory.DataBind();
          
            //if (InventoryBalancePrices == null || InventoryBalancePrices.Count == 0)
            //{
            //    grdBalanceOfItemsNoInventory.AddNewRow();
            //    ASPxTextBox txtCode = grdBalanceOfItemsNoInventory.FindEditRowCellTemplateControl(grdBalanceOfItemsNoInventory.Columns["Code"] as GridViewDataColumn, "txtCode") as ASPxTextBox;
            //    //txtCode.Focus();
            //    grdBalanceOfItemsNoInventory.Columns["Action"].Visible = true;
            //}
            //else
            //{
            //    grdBalanceOfItemsNoInventory.CancelEdit();
            //    grdBalanceOfItemsNoInventory.Columns["Action"].Visible = false;
            //}
        }



        protected void grdBalanceOfItemsNoInventory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {            
            if (txtInitInventoryCode.Text.Trim() == "")
            {
                grdBalanceOfItemsNoInventory.JSProperties.Clear();
                grdBalanceOfItemsNoInventory.JSProperties.Add("cpCodeNull", "empty");                               
                e.Cancel = true;
                return;
            }

            if (cboInitInventoryAccount.Value == null)
            {
                grdBalanceOfItemsNoInventory.JSProperties.Clear();
                grdBalanceOfItemsNoInventory.JSProperties.Add("cpAccountNull", "empty");   
                e.Cancel = true;
                return;
            }

            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox txtCode = grid.FindEditRowCellTemplateControl(grid.Columns["Code"] as GridViewDataColumn, "txtCode") as ASPxTextBox;
            ASPxSpinEdit txtBalance = grid.FindEditRowCellTemplateControl(grid.Columns["Balance"] as GridViewDataColumn, "txtBalance") as ASPxSpinEdit;
            ASPxSpinEdit txtPrice = grid.FindEditRowCellTemplateControl(grid.Columns["Price"] as GridViewDataColumn, "txtPrice") as ASPxSpinEdit;
            InventoryBalancePriceBO tmp = new InventoryBalancePriceBO();

            try
            {
                Inventory _Inventory = (Inventory)treeInventory.FocusedNode.DataItem;
                if (_Inventory != null)
                {
                    m_InventoryId = _Inventory.InventoryId;
                }

                m_ItemUnitId = (Guid)grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, "ItemUnitId");


                InventoryTransactionBalanceForward _inventoryTransactionBalanceForward = new InventoryTransactionBalanceForward(session);
                _inventoryTransactionBalanceForward.InventoryTransactionId = Guid.NewGuid();
                _inventoryTransactionBalanceForward.Code = txtInitInventoryCode.Text;
                _inventoryTransactionBalanceForward.Description = "Tồn kho ban đầu";
                _inventoryTransactionBalanceForward.RowStatus = 1;
                _inventoryTransactionBalanceForward.IssueDate = DateTime.Now;

                if (cboAccountPeriod.Value == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

                if (_accountingPeriod == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");                                
                
                _inventoryTransactionBalanceForward.AccountingPeriodId = _accountingPeriod;           
                _inventoryTransactionBalanceForward.Save();

                e.NewValues["InventoryJournalId"] = Guid.NewGuid();
                e.NewValues["RowStatus"] = "1";
                e.NewValues["CreateDate"] = DateTime.Now.ToString();

                e.NewValues["InventoryJournalId"] = Guid.NewGuid().ToString();

                CriteriaOperator _filter = new BinaryOperator("Code", cboInitInventoryAccount.Value.ToString(), BinaryOperatorType.Equal);
                Account _account = session.FindObject<Account>(_filter);
                if (_account != null)
                {
                    e.NewValues["AccountId!Key"] = _account.AccountId;
                }

                e.NewValues["InventoryTransactionId!Key"] = _inventoryTransactionBalanceForward.InventoryTransactionId.ToString();
                e.NewValues["ItemUnitId!Key"] = m_ItemUnitId;
                e.NewValues["InventoryId!Key"] = m_InventoryId;

                ASPxSpinEdit c = (ASPxSpinEdit)grdBalanceOfItemsNoInventory.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceOfItemsNoInventory.Columns["Debit"], "txtPrice");
                e.NewValues["Debit"] = c.Text;

                c = (ASPxSpinEdit)grdBalanceOfItemsNoInventory.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceOfItemsNoInventory.Columns["Balance"], "txtBalance");
                e.NewValues["Balance"] = c.Text;


                BusinessObjectBO BusinessObjectBO = new BusinessObjectBO();
                COGSBussinessBO COGSInventoryCommandBO = new COGSBussinessBO();
                CurrencyBO currencyBO = new CurrencyBO();

                BusinessObjectBO.CreateBusinessObject(session,
                            Utility.Constant.BusinessObjectType_InventoryTransaction,
                            _inventoryTransactionBalanceForward.InventoryTransactionId,
                            _inventoryTransactionBalanceForward.IssueDate);

                COGSInventoryCommandBO.CreateCOGS(
                                        session,
                                        0,
                                        double.Parse(txtBalance.Text.Trim()),
                                        DateTime.Now,
                                        double.Parse(txtPrice.Text.Trim()),
                                        _inventoryTransactionBalanceForward.IssueDate,
                                        _inventoryTransactionBalanceForward.InventoryTransactionId,
                                        _Inventory.InventoryId,
                                        m_ItemUnitId,
                                        currencyBO.GetDefaultCurrency(session).CurrencyId);

                DataBind();

                grdBalanceOfItemsNoInventory.JSProperties.Clear();
                grdBalanceOfItemsNoInventory.JSProperties.Add("cpRefresh", "refresh");                
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grdBalanceOfItemsNoInventory_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

            if (txtInitInventoryCode.Text == "" || cboInitInventoryAccount.Value == null)
            {
                return;
            }

            // Create BalanceForwardTransaction

            BalanceForwardTransaction balanceForwardTransaction = new BalanceForwardTransaction(session);

            balanceForwardTransaction.TransactionId = Guid.Parse(e.NewValues["InventoryTransactionId!Key"].ToString());
            balanceForwardTransaction.Code = txtInitInventoryCode.Text;
            balanceForwardTransaction.Description = "Tồn kho ban đầu";
            balanceForwardTransaction.RowStatus = 1;
            balanceForwardTransaction.IssueDate = balanceForwardTransaction.CreateDate = DateTime.Now;
            
            if (cboAccountPeriod.Value == null)
                throw new Exception("Chưa chọn kỳ kế toán !");

            AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

            if (_accountingPeriod == null)
                throw new Exception("Chưa chọn kỳ kế toán !");

            balanceForwardTransaction.AccountingPeriodId = _accountingPeriod;

            balanceForwardTransaction.Save();

            // Create GeneralJournalBalanceForward

            GeneralJournalBalanceForward _generalJournalBalanceForward = new GeneralJournalBalanceForward(session);
            _generalJournalBalanceForward.GeneralJournalId = Guid.NewGuid();
            _generalJournalBalanceForward.Debit = double.Parse(e.NewValues["Debit"].ToString()) * double.Parse(e.NewValues["Balance"].ToString());            

            CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
                                                   new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal),
                                                   new BinaryOperator("CurrencyTypeId.IsMaster", 1, BinaryOperatorType.Equal));
            Currency _currency = session.FindObject<NAS.DAL.Accounting.Currency.Currency>(_filter);
            if (_currency != null)
            {
                _generalJournalBalanceForward.CurrencyId = _currency;
            }                
                                      
            _generalJournalBalanceForward.Credit = 0;
            _generalJournalBalanceForward.RowStatus = 1;

            _filter = new BinaryOperator("Code", cboInitInventoryAccount.Value.ToString(), BinaryOperatorType.Equal);
            Account _account = session.FindObject<Account>(_filter);
            if (_account != null)
            {
                _generalJournalBalanceForward.AccountId = session.GetObjectByKey<Account>(_account.AccountId);                
            }
                                    
            _generalJournalBalanceForward.TransactionId = balanceForwardTransaction;
            
            _generalJournalBalanceForward.Save();

            // Create ObjectBO
            
            ObjectBO objectBO = new ObjectBO();            
            NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject = objectBO.CreateCMSObject(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);
            
            GeneralJournalObject debitGeneralJournalObject = null;

            GeneralJournal debitGeneralJournal = session.GetObjectByKey<GeneralJournal>(_generalJournalBalanceForward.GeneralJournalId);
            debitGeneralJournalObject = new GeneralJournalObject(session);
            debitGeneralJournalObject.GeneralJournalId = debitGeneralJournal;
            debitGeneralJournalObject.ObjectId = debitJounalCMSObject;
            debitGeneralJournalObject.Save();

            // Create ObjectType

            ObjectType objectType = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(session, ObjectTypeEnum.OPENBALANCE_ACCOUTING);

            GeneralJournalCustomType generalJournalCustomType = new GeneralJournalCustomType(session);
            generalJournalCustomType.GeneralJournalId = debitGeneralJournal;
            generalJournalCustomType.ObjectTypeId = objectType;

            generalJournalCustomType.Save();


            // Custom Field Inventory

            CriteriaOperator filter = new BinaryOperator("Code", "INVENTORY_OPEN_BALANCE", BinaryOperatorType.Equal);
            ObjectTypeCustomField _objectTypeCustomField = session.FindObject<ObjectTypeCustomField>(filter);
            
            ObjectCustomFieldBO _objectCustomFieldBO = new ObjectCustomFieldBO();
            ObjectCustomField objectCustomField = _objectCustomFieldBO.GetObjectCustomField(session, debitJounalCMSObject.ObjectId, _objectTypeCustomField.ObjectTypeCustomFieldId);

            List<Guid> _ref = new List<Guid>();
            _ref.Add(m_InventoryId);

            _objectCustomFieldBO.UpdatePredefinitionData(objectCustomField.ObjectCustomFieldId, _ref, PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVENTORY, CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY);


            // Custom Filed Item

            filter = new BinaryOperator("Code", "BALANCE_FORWARD_TRANSACTION_ITEM", BinaryOperatorType.Equal);
            _objectTypeCustomField = session.FindObject<ObjectTypeCustomField>(filter);

            _objectCustomFieldBO = new ObjectCustomFieldBO();
            objectCustomField = _objectCustomFieldBO.GetObjectCustomField(session, debitJounalCMSObject.ObjectId, _objectTypeCustomField.ObjectTypeCustomFieldId);

            _ref = new List<Guid>();
            _ref.Add(m_InventoryId);

            _objectCustomFieldBO.UpdatePredefinitionData(objectCustomField.ObjectCustomFieldId, _ref, PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVENTORY, CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY);
        }

        protected void grdBalanceOfItemsNoInventory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            CriteriaOperator _filter;
            Account _account;

            if (txtInitInventoryCode.Text.Trim() == "")
            {
                grdBalanceOfItemsNoInventory.JSProperties.Clear();
                grdBalanceOfItemsNoInventory.JSProperties.Add("cpCodeNull", "empty");
                e.Cancel = true;
                return;
            }

            if (cboInitInventoryAccount.Value == null)
            {
                grdBalanceOfItemsNoInventory.JSProperties.Clear();
                grdBalanceOfItemsNoInventory.JSProperties.Add("cpAccountNull", "empty");
                e.Cancel = true;
                return;
            }

            InventoryTransactionBalanceForward _inventoryTransactionBalanceForward = session.GetObjectByKey<InventoryTransactionBalanceForward>(Guid.Parse(e.OldValues["InventoryTransactionId!Key"].ToString()));
            if (_inventoryTransactionBalanceForward != null)
            {
                if (cboAccountPeriod.Value == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                AccountingPeriod _accountingPeriod = session.GetObjectByKey<AccountingPeriod>(Guid.Parse(cboAccountPeriod.Value.ToString()));

                if (_accountingPeriod == null)
                    throw new Exception("Chưa chọn kỳ kế toán !");

                _inventoryTransactionBalanceForward.AccountingPeriodId = _accountingPeriod;
            }

            _filter = new BinaryOperator("Code", cboInitInventoryAccount.Value.ToString(), BinaryOperatorType.Equal);
            _account = session.FindObject<Account>(_filter);
            if (_account != null)
            {
                e.NewValues["AccountId!Key"] = _account.AccountId;
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdBalanceOfItemsNoInventory.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceOfItemsNoInventory.Columns["Debit"], "txtPrice");
            e.NewValues["Debit"] = c.Text;

            c = (ASPxSpinEdit)grdBalanceOfItemsNoInventory.FindEditRowCellTemplateControl((GridViewDataColumn)grdBalanceOfItemsNoInventory.Columns["Balance"], "txtBalance");
            e.NewValues["Balance"] = c.Text;

            BalanceForwardTransaction _balanceForwardTransaction = session.GetObjectByKey<BalanceForwardTransaction>(Guid.Parse(e.OldValues["InventoryTransactionId!Key"].ToString()));
            if (_balanceForwardTransaction != null)
            {
                _balanceForwardTransaction.Code = txtInitInventoryCode.Text;
                _balanceForwardTransaction.Save();

                // GeneralJournal

                _filter = new BinaryOperator("TransactionId", _balanceForwardTransaction.TransactionId, BinaryOperatorType.Equal);
                GeneralJournalBalanceForward _generalJournalBalanceForward = session.FindObject<GeneralJournalBalanceForward>(_filter);

                if (_generalJournalBalanceForward != null)
                {
                    _generalJournalBalanceForward.Debit = double.Parse(e.NewValues["Debit"].ToString()) * double.Parse(e.NewValues["Balance"].ToString());

                    _filter = new GroupOperator(GroupOperatorType.And,
                                                           new BinaryOperator("IsDefault", 1, BinaryOperatorType.Equal),
                                                           new BinaryOperator("CurrencyTypeId.IsMaster", 1, BinaryOperatorType.Equal));
                    Currency _currency = session.FindObject<NAS.DAL.Accounting.Currency.Currency>(_filter);
                    if (_currency != null)
                    {
                        _generalJournalBalanceForward.CurrencyId = _currency;
                    }

                    _generalJournalBalanceForward.Credit = 0;
                    _generalJournalBalanceForward.RowStatus = 1;

                    _filter = new BinaryOperator("Code", cboInitInventoryAccount.Value.ToString(), BinaryOperatorType.Equal);

                    _account = session.FindObject<Account>(_filter);
                    if (_account != null)
                    {
                        _generalJournalBalanceForward.AccountId = session.GetObjectByKey<Account>(_account.AccountId);
                    }

                    _generalJournalBalanceForward.Save();
                }
                //
            }


            DataBind();
            grdBalanceOfItemsNoInventory.JSProperties.Clear();
            grdBalanceOfItemsNoInventory.JSProperties.Add("cpRefresh", "refresh"); 
            
        }

        protected void grdBalanceOfItemsNoInventory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Inventory _Inventory = (Inventory)treeInventory.FocusedNode.DataItem;
            if (_Inventory != null)
            {
                m_InventoryId = _Inventory.InventoryId;
            }

            m_ItemUnitId = (Guid)grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, "ItemUnitId");

            // Delete InventoryJournalBalanceForward
            CriteriaOperator _filter = new BinaryOperator("InventoryTransactionId", Guid.Parse(e.Values["InventoryTransactionId!Key"].ToString()), BinaryOperatorType.Equal);                    
            InventoryJournalBalanceForward _inventoryJournalBalanceForward = session.FindObject<InventoryJournalBalanceForward>(_filter);
            
            if (_inventoryJournalBalanceForward != null)
            {
                _inventoryJournalBalanceForward.RowStatus = -1;
                _inventoryJournalBalanceForward.InventoryTransactionId.RowStatus = -1; 
                _inventoryJournalBalanceForward.Save();
            }


            // Delete BalanceForwardTransaction
            BalanceForwardTransaction _balanceForwardTransaction = session.GetObjectByKey<BalanceForwardTransaction>(Guid.Parse(e.Values["InventoryTransactionId!Key"].ToString()));
            if (_balanceForwardTransaction != null)
            {
                _balanceForwardTransaction.RowStatus = -1;
                _balanceForwardTransaction.Save();
            }
             

            // Delete GeneralJournalBalanceForward
            CriteriaOperator filter = new BinaryOperator("TransactionId", _balanceForwardTransaction.TransactionId, BinaryOperatorType.Equal);
            GeneralJournalBalanceForward _generalJournalBalanceForward = session.FindObject<GeneralJournalBalanceForward>(filter);

            if (_generalJournalBalanceForward != null)
            {
                _generalJournalBalanceForward.RowStatus = -1;
                _generalJournalBalanceForward.Save();
            }
            

            //e.Values["RowStatus"] = "-1";
            e.Cancel = true;

            DataBind();

            grdBalanceOfItemsNoInventory.JSProperties.Clear();
            grdBalanceOfItemsNoInventory.JSProperties.Add("cpRefresh", "refresh");
        }

        protected void grdBalanceOfItemsNoInventory_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {



            //loadBalanceInfo(Guid.Parse(Session["InventorySelected"].ToString()), Guid.Parse(e.Parameters.ToString()));
        }

        protected void grdBalanceOfItemsNoInventory_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox txtCode = grid.FindEditRowCellTemplateControl(grid.Columns["Code"] as GridViewDataColumn, "txtCode") as ASPxTextBox;
            ASPxSpinEdit txtBalance = grid.FindEditRowCellTemplateControl(grid.Columns["Balance"] as GridViewDataColumn, "txtBalance") as ASPxSpinEdit;
            ASPxSpinEdit txtPrice = grid.FindEditRowCellTemplateControl(grid.Columns["Price"] as GridViewDataColumn, "txtPrice") as ASPxSpinEdit;
            //if (txtCode.Text.Trim().Equals(string.Empty))
            //{
            //    throw new Exception("Bắt buộc phải có mã phiếu nhập");
            //    //Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Code"], "Bắt buộc phải có mã phiếu nhập");
            //}

            if (float.Parse(txtBalance.Text.Trim()) <= 0)
            {
                throw new Exception("Số lượng tồn phải lớn hơn 0");
                //Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Code"], "Bắt buộc phải có mã phiếu nhập");
            }

            if (float.Parse(txtPrice.Text.Trim()) <= 0)
            {
                throw new Exception("Giá thành sản phẩm phải lớn hơn 0");
                //Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Code"], "Bắt buộc phải có mã phiếu nhập");
            }
        }

        protected void grdBalanceOfItemsNoInventory_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "LotId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "if (s.GetSelectedIndex() < 0) {return;}" +
                                                           "var a = s.GetSelectedItem().GetColumnText('ExpireDate').substring(0,10).split('/');" +
                                                           "var d = new Date(a[2], a[1]-1, a[0]);" +                                                           
                                                           "grdBalanceOfItemsNoInventory.GetEditor('LotId.ExpireDate').SetValue(d);" +
                                                       "}";
                combo.Callback += new CallbackEventHandlerBase(comboLots_OnCallback);
                combo.ClientSideEvents.EndCallback = "function(s,e){ " +
                "if(s.cpSelectedValue != null){ s.SetValue(s.cpSelectedValue); delete s.cpSelectedValue; " +
                "var date = Date.parseLocale(s.cpSelectedValueDate, 'dd/MM/yyyy'); delete s.cpSelectedValueDate; " +
                grdBalanceOfItemsNoInventory.ClientInstanceName + ".GetEditor('LotId.ExpireDate').SetDate(date); " +
                grdBalanceOfItemsNoInventory.ClientInstanceName + ".Refresh(); " +
                "}" +
                "}";
            }
        }

        //protected void colLot_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        //{
        //    try
        //    {
        //        ASPxComboBox comboItemUnit = source as ASPxComboBox;
        //        NAS.DAL.Inventory.Lot.Lot obj = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(e.Value);

        //        if (obj != null)
        //        {
        //            comboItemUnit.DataSource = new NAS.DAL.Inventory.Lot.Lot[] { obj };
        //            comboItemUnit.DataBindItems();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //protected void cboSelectLotItem_OnItemRequestedByValue(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        //{

        //}

        //protected void cboSelectLotItem_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        //{
           
        //}

        void comboLots_OnCallback(object source, CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            ASPxComboBox comboLots = source as ASPxComboBox;
            loadCBOLot(comboLots);
            comboLots.Value = Guid.Parse(para[0]);
            NAS.DAL.Inventory.Lot.Lot lot = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(Guid.Parse(para[0]));

            comboLots.JSProperties.Clear();
            comboLots.JSProperties.Add("cpSelectedValue", lot.LotId);

            comboLots.JSProperties.Clear();
            comboLots.JSProperties.Add("cpSelectedValueDate", lot.ExpireDate.ToString("dd/MM/yyyy"));
        }



        private void loadCBOLot(ASPxComboBox cbo)
        {
            //cbo.Items.Clear();
            //NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = 
            //    session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(Guid.Parse(grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, "ItemUnitId").ToString()));
            //CriteriaOperator criteria = CriteriaOperator.Or(CriteriaOperator.And(
            //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual),
            //        new BinaryOperator("ItemId!Key", itemUnit.ItemId.ItemId, BinaryOperatorType.Equal)),
            //    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal));

            //XPCollection<NAS.DAL.Inventory.Lot.Lot> collection = new XPCollection<NAS.DAL.Inventory.Lot.Lot>(session);
            ////collection.Criteria = criteria;
            //collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            //cbo.DataSource = collection;
            //cbo.DataBindItems();
        }

        protected void grdBalanceOfItemsNoInventory_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //if (e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Cancel)
            //    e.Visible = false;
        }

        protected void cboInitInventoryAccount_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;          

            CriteriaOperator _filter = new BinaryOperator("Code", e.Value.ToString(), BinaryOperatorType.Equal);
            Account obj = session.FindObject<Account>(_filter);

            if (obj != null)
            {
                combo.DataSource = new Account[] { obj };
                combo.DataBindItems();
            }
        }

        protected void cboInitInventoryAccount_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Account> collection = new XPCollection<Account>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            XPCollection<Account> _childAccount = AccountingBO.getNotParentAccountCollection(session);

            CriteriaOperator criteria = CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),
                    CriteriaOperator.Or(
                    new BinaryOperator("AccountTypeId.AccountCategoryId.Code", "OFFBALANCE", BinaryOperatorType.Equal),
                    new BinaryOperator("AccountTypeId.AccountCategoryId.Code", "ASSET", BinaryOperatorType.Equal)),
                    new InOperator("this", _childAccount)
                    );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void grdBalanceOfItemsNoInventory_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Amount")
            {
                double _balance = Convert.ToDouble(e.GetListSourceFieldValue("Balance"));
                double _debit = Convert.ToDouble(e.GetListSourceFieldValue("Debit"));

                e.Value = _balance * _debit;
            }
        }

        protected void cboSelectLotItem_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                ASPxComboBox comboItemUnit = source as ASPxComboBox;
                NAS.DAL.Inventory.Lot.Lot obj = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(e.Value);

                if (obj != null)
                {
                    comboItemUnit.DataSource = new NAS.DAL.Inventory.Lot.Lot[] { obj };
                    comboItemUnit.DataBindItems();
                }
            }
            catch
            {
            }
        }

        protected void cboSelectLotItem_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            int rowIdx = 0;
            if (grdataproduct.VisibleRowCount == 0)
                return;
            if (grdataproduct.FocusedRowIndex < 0)
                rowIdx = 0;
            else
                rowIdx = grdataproduct.FocusedRowIndex;

            object ItemUnitId = grdataproduct.GetRowValues(rowIdx, "ItemUnitId");
            if (ItemUnitId == null)
                return;

            NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(Guid.Parse(ItemUnitId.ToString()));
            if (itemUnit == null)
                return;

            ASPxComboBox cbo = source as ASPxComboBox;
            CriteriaOperator criteria =
                CriteriaOperator.Or(
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemId!Key", itemUnit.ItemId.ItemId, BinaryOperatorType.Equal),
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal));

            XPCollection<NAS.DAL.Inventory.Lot.Lot> collection = new XPCollection<NAS.DAL.Inventory.Lot.Lot>(session);
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            cbo.DataSource = collection;
            cbo.DataBind();
        }

        protected void txtPrice_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Debit").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "grdBalanceOfItemsNoInventory.GetEditor('Amount').SetValue(txtBalance.GetValue()*s.GetValue());" +
                                                  "}";
        }

        protected void txtBalance_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Balance").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "grdBalanceOfItemsNoInventory.GetEditor('Amount').SetValue(txtPrice.GetValue()*s.GetValue());" +
                                                  "}";
        }

        protected void cpInitInventoryDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            Inventory _inventory;


            switch (para[0])
            {
                case "itemchange":
                    if (cboAccountPeriod.Value == null)
                    {
                        throw new Exception("Chưa chọn chu kỳ kế toán !");
                    }

                    _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(para[2]));
                    if (_inventory != null)
                    {
                        Session["InventorySelected"] = _inventory.InventoryId.ToString();
                        m_InventoryId = _inventory.InventoryId;
                        m_ItemUnitId = Guid.Parse(para[1]);

                        if (cboAccountPeriod.Value != null)
                        {
                            m_AccountingPeriodId = Guid.Parse(cboAccountPeriod.Value.ToString());
                        }
                    }

                    cboInitInventoryAccount.Value = null;
                    txtInitInventoryCode.Text = "";

                    grdBalanceOfItemsNoInventory.CancelEdit();            
   
                    CriteriaOperator _filter = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator("InventoryId.InventoryId", m_InventoryId, BinaryOperatorType.Equal),
                    new BinaryOperator("ItemUnitId.ItemUnitId", m_ItemUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryTransactionId.AccountingPeriodId.AccountingPeriodId", m_AccountingPeriodId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));

                    InventoryJournalBalanceForward _inventoryJournalBalanceForward = session.FindObject<InventoryJournalBalanceForward>(_filter);
                    if (_inventoryJournalBalanceForward != null)
                    {
                        if (_inventoryJournalBalanceForward.AccountId != null)
                        {
                            cboInitInventoryAccount.Value = _inventoryJournalBalanceForward.AccountId.Code;
                        }

                        if (_inventoryJournalBalanceForward.InventoryTransactionId != null)
                        {
                            txtInitInventoryCode.Text = _inventoryJournalBalanceForward.InventoryTransactionId.Code;
                        }
                    }
                    else
                    {
                        m_InventoryId = Guid.Empty;
                        m_ItemUnitId =  Guid.Empty;
                        m_AccountingPeriodId =  Guid.Empty;                                               
                    }

                    BindData();

                    break;

                case "inventorychange":
                    if (cboAccountPeriod.Value == null)
                    {
                        throw new Exception("Chưa chọn chu kỳ kế toán !");
                    }

                     _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(para[2]));
                    if (_inventory != null)
                    {
                        Guid _accountPeriod = Guid.Empty;

                        if (cboAccountPeriod.Value != null)
                        {
                            _accountPeriod = Guid.Parse(cboAccountPeriod.Value.ToString());
                        }

                        System.Diagnostics.Debug.WriteLine(String.Format("InventoryId: {0}, Name: {1}", _inventory.InventoryId, _inventory.Name));
                        Session["InventorySelected"] = _inventory.InventoryId.ToString();
                        AllItemUnits = itBO.getAllItemUnits(session, Guid.Parse(Session["InventorySelected"].ToString()), _accountPeriod);
                        this.grdataproduct.DataSource = AllItemUnits;
                        this.grdataproduct.DataBind();

                        m_InventoryId = _inventory.InventoryId;
                        //if (para[1] != "null")
                        //{
                        m_ItemUnitId = Guid.Parse(grdataproduct.GetRowValues(0, "ItemUnitId").ToString());
                        //}

                        //m_ItemUnitId = Guid.Parse(grdataproduct.GetRowValues(grdataproduct.FocusedRowIndex, grdataproduct.KeyFieldName).ToString());

                        if (cboAccountPeriod.Value != null)
                        {
                            m_AccountingPeriodId = Guid.Parse(cboAccountPeriod.Value.ToString());
                        }
                    }

                    cboInitInventoryAccount.Value = null;
                    txtInitInventoryCode.Text = "";

                    grdBalanceOfItemsNoInventory.CancelEdit();            
   
                    _filter = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator("InventoryId.InventoryId", m_InventoryId, BinaryOperatorType.Equal),
                    new BinaryOperator("ItemUnitId.ItemUnitId", m_ItemUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryTransactionId.AccountingPeriodId.AccountingPeriodId", m_AccountingPeriodId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));

                    _inventoryJournalBalanceForward = session.FindObject<InventoryJournalBalanceForward>(_filter);
                    if (_inventoryJournalBalanceForward != null)
                    {
                        if (_inventoryJournalBalanceForward.AccountId != null)
                        {
                            cboInitInventoryAccount.Value = _inventoryJournalBalanceForward.AccountId.Code;
                        }

                        if (_inventoryJournalBalanceForward.InventoryTransactionId != null)
                        {
                            txtInitInventoryCode.Text = _inventoryJournalBalanceForward.InventoryTransactionId.Code;
                        }
                    }          

                    BindData();

                    //grdataproduct.Selection.SelectRow(0);

                    break;     

                case "comboChange":

                    //if (cboAccountPeriod.Value != null)
                    //{
                    //    Guid _accountPeriod = Guid.Empty;

                    //    if (cboAccountPeriod.Value != null)
                    //    {
                    //        _accountPeriod = Guid.Parse(cboAccountPeriod.Value.ToString());
                    //        m_AccountingPeriodId = Guid.Parse(cboAccountPeriod.Value.ToString());
                    //    }
                      

                    //    AllItemUnits = itBO.getAllItemUnits(session, Guid.Parse(Session["InventorySelected"].ToString()), _accountPeriod);
                    //    if (AllItemUnits != null || AllItemUnits.Count == 0)
                    //    {
                    //        Session["ItemUnitId"] = AllItemUnits[0].ItemUnitId;

                    //        m_ItemUnitId = AllItemUnits[0].ItemUnitId;
                    //        m_InventoryId = Guid.Parse(Session["InventorySelected"].ToString());

                    //        this.grdataproduct.DataSource = AllItemUnits;
                    //        this.grdataproduct.DataBind();
                    //        grdataproduct.Selection.SelectRow(0);

                    //        grdataproduct.SettingsBehavior.AllowFocusedRow = false;
                    //        grdataproduct.SettingsBehavior.AllowFocusedRow = true;

                    //        grdataproduct.FocusedRowIndex = 0;
                    //    }
                    //}

                    //grdataproduct.Selection.SelectRow(0);

                    break;
            }

            cpInitInventoryDetail.JSProperties.Clear();
            cpInitInventoryDetail.JSProperties.Add("cpRefresh", "refresh");
        }

        protected void grdataproduct_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }

        protected void treeInventory_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {

        }

        protected void treeInventory_CustomDataCallback(object sender, TreeListCustomDataCallbackEventArgs e)
        {

        }

        protected void btnAddLot_Load(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            uAddNewLotsToItem1.SettingInit(button, grdataproduct);
            DevExpress.Web.ASPxGridView.ASPxGridView grd = (button.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewHeaderTemplateContainer).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as
                DevExpress.Web.ASPxGridView.ASPxGridView;

            if (grd.IsNewRowEditing || grd.IsEditing)
                button.ClientVisible = true;
            else
                button.ClientVisible = false;
        }

        protected void grdBalanceOfItemsNoInventory_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (cboAccountPeriod.Value == null)
                throw new Exception("Chưa chọn kỳ kế toán !");

            grdBalanceOfItemsNoInventory.JSProperties.Clear();
            grdBalanceOfItemsNoInventory.JSProperties.Add("cpEnableMasterControl", "true");          
        }

        protected void grdBalanceOfItemsNoInventory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (cboAccountPeriod.Value == null)            
                throw new Exception("Chưa chọn kỳ kế toán !");


            grdBalanceOfItemsNoInventory.JSProperties.Clear();
            grdBalanceOfItemsNoInventory.JSProperties.Add("cpEnableMasterControl", "true");            
        }

        protected void grdBalanceOfItemsNoInventory_Init(object sender, EventArgs e)
        {
            grdBalanceOfItemsNoInventory.JSProperties.Clear();
            grdBalanceOfItemsNoInventory.JSProperties.Add("cpEnableMasterControl", "false");   
        }

        protected void cboAccountPeriod_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<AccountingPeriod> collection = new XPCollection<AccountingPeriod>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            AccountingPeriodTypeBO typeBO = new AccountingPeriodTypeBO();
            AccountingPeriodType minAccountingPeriodType = typeBO.GetMinAccountingPeriodType(session);
                            
            CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_IsActive = new BinaryOperator("IsActive", true, BinaryOperatorType.Equal);
            CriteriaOperator criteria_Type = new BinaryOperator("AccountingPeriodTypeId", minAccountingPeriodType, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.And(criteria_IsActive, criteria_RowStatus, criteria_Type);
            XPCollection<AccountingPeriod> AccountingPeriodCol = new XPCollection<AccountingPeriod>(session, criteria);



            criteria = CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Description", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),                    
                    new InOperator("this", AccountingPeriodCol)
                    );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();
        }

        protected void cboAccountPeriod_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                ASPxComboBox combo = source as ASPxComboBox;
                AccountingPeriod obj = session.GetObjectByKey<AccountingPeriod>(e.Value);

                if (obj != null)
                {
                    combo.DataSource = new AccountingPeriod[] { obj };
                    combo.DataBindItems();
                }
            }
            catch
            {
            }
        }

        protected void cboAccountPeriod_Init(object sender, EventArgs e)
        {
            cboAccountPeriod.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                                    " if (grdataproduct.GetFocusedRowIndex() >= 0) {" +
                                                                        " var id = grdataproduct.GetRowKey(grdataproduct.GetFocusedRowIndex());" +
                                                                        " cpInitInventoryDetail.PerformCallback('itemchange|' + id + '|' + treeInventory.GetFocusedNodeKey());" +
                                                                     " }" +
                                                                " }";
        }

        protected void grdataproduct_DataBound(object sender, EventArgs e)
        {
            if (grdataproduct.VisibleRowCount > 0 && grdataproduct.FocusedRowIndex == -1)
            {
                //grdataproduct.Selection.SelectRow(0);
                grdataproduct.FocusedRowIndex = 0;
            }
        }       
    }
}