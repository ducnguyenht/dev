using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using DevExpress.Web.ASPxGridView;

using NAS.BO;
using NAS.BO.Accounting.Journal;
using System.Collections;
using NAS.DAL.Invoice;
using ERPCore.Accounting;
using DevExpress.Web.ASPxCallbackPanel;
using NAS.DAL.Inventory.Journal;
using NAS.BO.Accounting;
using Utility;

namespace ERPCore.Warehouse.UserControl
{
    public partial class uListOutCommBooking : System.Web.UI.UserControl
    {
        Session session;

        static short ROW_NEW = 1;
        static short ROW_DELETE = -1;
        static short ROW_NOT_DELETE = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hWarehouseBookingId.Count > 0)
            {
                Session["SaleBillId"] = hWarehouseBookingId.Get("id").ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            //AccountXDS.Session = session;
            GeneralJournalXDS.Session = session;
            InventoryTransactionXDS.Session = session;
            
        }

        protected void grdBooking_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            string transactionCode = (string)e.NewValues["Code"];
            //Insert mode
            if (grid.IsNewRowEditing)
            {
                //Check dupplicate code
                bool isExist = Util.isExistXpoObject<Transaction>("Code", transactionCode,
                                Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                                Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_TEMP);
                if (isExist)
                {
                    Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Code"],
                        String.Format("Mã bút toán {0} đã tồn tại", transactionCode));
                }
            }
            else
            {
                string oldTransactionCode = (string)e.OldValues["Code"];
                if (!transactionCode.Equals(oldTransactionCode))
                {
                    //Check dupplicate code
                    bool isExist = Util.isExistXpoObject<Transaction>("Code", transactionCode,
                                    Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                                    Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_TEMP);
                    if (isExist)
                    {
                        Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Code"],
                            String.Format("Mã bút toán {0} đã tồn tại", transactionCode));
                    }
                }
            }
        }

        protected void grdBooking_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> saleInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }

            grdBooking.DetailRows.ExpandRowByKey(Guid.Parse(e.NewValues["TransactionId"].ToString()));
        }

        protected void grdBooking_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> saleInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }

            filter = new BinaryOperator("InventoryTransactionId", Guid.Parse(e.NewValues["SalesInvoiceInventoryTransactionId!Key"].ToString()), BinaryOperatorType.Equal);
            SalesInvoiceInventoryTransaction salesInvoiceInventoryTransaction = session.FindObject<SalesInvoiceInventoryTransaction>(filter);
            
            e.NewValues["SalesInvoiceInventoryTransactionId"] = salesInvoiceInventoryTransaction;
            e.NewValues["TransactionId"] = Guid.NewGuid();
        }


        protected void grdBooking_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            grdBooking.Selection.SelectRowByKey(Guid.Parse(e.NewValues["TransactionId"].ToString()));

            grdBooking.DetailRows.CollapseAllRows();
            grdBooking.DetailRows.ExpandRowByKey(Guid.Parse(e.NewValues["TransactionId"].ToString()));

            int position = grdBooking.FindVisibleIndexByKeyValue(Guid.Parse(e.NewValues["TransactionId"].ToString()));
            ASPxGridView grdBooking_Detail = (ASPxGridView)grdBooking.FindDetailRowTemplateControl(position, "grdBookingDetail");
            grdBooking_Detail.AddNewRow();
            grdBooking_Detail.Focus();


            grdBooking.FocusedRowIndex = position;
        }


        protected void grdBooking_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> saleInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);
          
            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            filter = new BinaryOperator("TransactionId", Guid.Parse(e.Values["TransactionId"].ToString()), BinaryOperatorType.Equal);
            SalesInvoiceInventoryAccountingTransaction salesInvoiceInventoryTransaction = session.FindObject<SalesInvoiceInventoryAccountingTransaction>(filter);            

            if (salesInvoiceInventoryTransaction != null)
            {
                salesInvoiceInventoryTransaction.RowStatus = ROW_DELETE;
                salesInvoiceInventoryTransaction.Save();
            }

            //e.Cancel = true;
        }
        
        protected void cpBooking_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            session = XpoHelper.GetNewSession();
          

            switch (e.Parameter)
            {               
                case "view":

                    grdBooking.DetailRows.CollapseAllRows();

                    if (Session["SaleBillId"] == null)
                    {
                        return;
                    }

                    session = XpoHelper.GetNewSession();
                    SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));

                    CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    XPCollection<SalesInvoiceInventoryAccountingTransaction> saleInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

                    if (saleInvoiceTransaction.Count > 0)
                    {
                        foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                        {
                            if (item.RowStatus == ROW_NOT_DELETE)
                            {
                                grdBooking.Columns["Thao tác"].Visible = false;
                                break;
                            }
                            else
                            {
                                grdBooking.Columns["Thao tác"].Visible = true;
                            }
                        }
                    }
                    else
                    {
                        grdBooking.AddNewRow();
                        grdBooking.Columns["Thao tác"].Visible = true;
                    }

                    cpBooking.JSProperties.Add("cpEnable", "true");


                    /////


                    break;

                case "booking":

                    salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));

                    filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    saleInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

                    foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                    {
                        double credit = 0;
                        double debit = 0;

                        filter = new GroupOperator(GroupOperatorType.And,
                                                new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal),
                                                new BinaryOperator("RowStatus", ROW_NEW, BinaryOperatorType.Equal));
                        XPCollection<GeneralJournal> generalJournal = new XPCollection<GeneralJournal>(session, filter);

                        foreach (GeneralJournal x in generalJournal)
                        {
                            filter = new GroupOperator(GroupOperatorType.And,
                                                new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal),
                                                new BinaryOperator("AccountId", x.AccountId, BinaryOperatorType.Equal),
                                                new BinaryOperator("RowStatus", ROW_NEW, BinaryOperatorType.Equal));

                            XPCollection<GeneralJournal> generalJ = new XPCollection<GeneralJournal>(session, filter);
                            if (generalJ.Count >= 2)
                            {
                                cpBooking.JSProperties.Add("cpUnbooked", "1");
                                return;
                            }

                            credit += x.Credit;
                            debit += x.Debit;
                        }

                        if (credit != debit)
                        {
                            cpBooking.JSProperties.Add("cpUnbooked", "2");
                            return;
                        }
                    }

                    foreach (SalesInvoiceInventoryAccountingTransaction item in saleInvoiceTransaction)
                    {
                        item.RowStatus = ROW_NOT_DELETE;
                        item.Save();
                        
                        filter = new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal);
                        XPCollection<GeneralJournal> generalJournal = new XPCollection<GeneralJournal>(session, filter);
                        double milsec = 0;
                        foreach (GeneralJournal x in generalJournal)
                        {
                            NAS.DAL.Accounting.AccountChart.Account account = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(x.AccountId.AccountId);
                            //General general = new General();  
                            //general.BalanceUpdate(session, account, x, false, x.Debit-x.Credit);
                            AccountingBO accountBO = new AccountingBO();
                            accountBO.CreateGeneralLedger(session, x.TransactionId.TransactionId, x.GeneralJournalId,milsec);
                            
                            milsec += 10;
                        }
                    }


                    filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    SalesInvoiceInventoryAccountingTransaction purchaseInvoiceInventoryTransaction = session.FindObject<SalesInvoiceInventoryAccountingTransaction>(filter);

                    purchaseInvoiceInventoryTransaction.RowStatus = ROW_NOT_DELETE;
                    purchaseInvoiceInventoryTransaction.Save();

                    grdBooking.DetailRows.CollapseAllRows();
                    grdBooking.Columns["Thao tác"].Visible = false;

                    cpBooking.JSProperties.Add("cpEnable", "true");                              

                    break;
                       
                
                default:
                    break;
            }
        }

        protected void cpBookingCommand_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> invoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (invoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in invoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        ASPxCallbackPanel cpp = (ASPxCallbackPanel)formBooking.FindControl("cpBookingCommand");
                        ASPxButton button = (ASPxButton)cpp.FindControl("buttonBoookingApprove");
                        button.Visible = false;

                        break;
                    }
                }
            }
        }

        protected void colBookingDetailDebit_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Debit").ToString();
            }           

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                            "colBookingDetailCredit.SetValue(0);" +
                                                  "}";
        }

        protected void colBookingDetailCredit_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Credit").ToString();
            }           

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                            "colBookingDetailDebit.SetValue(0);" +
                                                  "}";
        }

        protected void grdBookingDetail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;
            Session["TransactionId"] = grdBookingDetail.GetMasterRowKeyValue();

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);


            grdBookingDetail.CancelEdit();

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBookingDetail.Columns["Thao tác"].Visible = false;
                        break;
                    }
                    else
                    {
                        grdBookingDetail.AddNewRow();                        
                    }
                }
            }
          
        }

        protected void grdBookingDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdBookingDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdBookingDetail.Columns["Credit"], "colBookingDetailCredit");
            e.NewValues["Credit"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdBookingDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdBookingDetail.Columns["Debit"], "colBookingDetailDebit");
            e.NewValues["Debit"] = c.Value.ToString();            
        }

        protected void grdBookingDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }

            e.NewValues["TransactionId!Key"] = ((ASPxGridView)sender).GetMasterRowKeyValue();

            e.NewValues["GeneralJournalId"] = Guid.NewGuid();    

            ASPxSpinEdit c = (ASPxSpinEdit)grdBookingDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdBookingDetail.Columns["Credit"], "colBookingDetailCredit");
            e.NewValues["Credit"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdBookingDetail.FindEditRowCellTemplateControl((GridViewDataColumn)grdBookingDetail.Columns["Debit"], "colBookingDetailDebit");
            e.NewValues["Debit"] = c.Value.ToString();
        }

        protected void grdBookingDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            GeneralJournal generalJournal = session.GetObjectByKey<GeneralJournal>(Guid.Parse(e.Values["GeneralJournalId"].ToString()));
            generalJournal.RowStatus = ROW_DELETE;
            generalJournal.Save();
            
        }

        protected void grdBookingDetail_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            GridViewDataColumn col = ((ASPxGridView)sender).Columns["AccountId!Key"] as GridViewDataColumn;
            if (e.NewValues["AccountId!Key"] == null)
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Chưa chọn tài khoản";

                return;
            }

            GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["Credit"] as GridViewDataColumn;
            ASPxSpinEdit spin1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "colBookingDetailCredit") as ASPxSpinEdit;

            if (spin1.Value == null)
            {
                spin1.Value = 0;
            }

            GridViewDataColumn col2 = ((ASPxGridView)sender).Columns["Debit"] as GridViewDataColumn;
            ASPxSpinEdit spin2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "colBookingDetailDebit") as ASPxSpinEdit;

            if (spin2.Value == null)
            {
                spin2.Value = 0;
            }

            if (double.Parse(spin1.Value.ToString()) > 0 & double.Parse(spin2.Value.ToString()) > 0)
            {
                e.Errors.Add(col1, "The dummy error");
                e.RowError = "Nợ hoặc Có phải <= 0";

                return;
            }

            if (double.Parse(spin1.Value.ToString()) <= 0 & double.Parse(spin2.Value.ToString()) <= 0)
            {
                e.Errors.Add(col1, "The dummy error");
                e.RowError = "Nợ hoặc Có phải > 0";
                
                return;
            }
            
        }

        protected void grdBooking_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("InventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            SalesInvoiceInventoryTransaction inventoryTransaction = session.FindObject<SalesInvoiceInventoryTransaction>(filter);
            
            if (inventoryTransaction != null)
            {
                if (e.Column.FieldName == "Code")
                {
                    e.Editor.Focus();
                }
                if (e.Column.FieldName == "SalesInvoiceInventoryTransactionId.Code")
                {
                    InventoryTransaction inventory = session.GetObjectByKey<InventoryTransaction>(inventoryTransaction.InventoryTransactionId);
                    e.Editor.Value = inventory.Code;
                }
                if (e.Column.FieldName == "IssueDate")
                {
                    e.Editor.Value = inventoryTransaction.IssueDate;
                }
                if (e.Column.FieldName == "AccountingPeriodId!Key")
                {
                    e.Editor.Value = "5eaa2bf5-34ba-47c6-88df-48ad25bc6e18";
                }
                if (e.Column.FieldName == "SalesInvoiceInventoryTransactionId!Key")
                {
                    e.Editor.Value = Session["SaleBillId"].ToString();
                }
                if (e.Column.FieldName == "CreateDate")
                {
                    e.Editor.Value = DateTime.Now;
                }
              
                
            }
            
        }
      

        protected void grdBookingDetail_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
        }

        protected void grdBooking_Init(object sender, EventArgs e)
        {
        }

        protected void grdBookingDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "RowStatus")
            {
                e.Editor.Value = 1;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["Credit"] as GridViewDataColumn;
                ASPxSpinEdit spin1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "colBookingDetailCredit") as ASPxSpinEdit;
                spin1.Value = 0;

                GridViewDataColumn col2 = ((ASPxGridView)sender).Columns["Debit"] as GridViewDataColumn;
                ASPxSpinEdit spin2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "colBookingDetailDebit") as ASPxSpinEdit;
                spin1.Value = 0;
            }

            if (e.Column.FieldName == "AccountId!Key")
            {
                ASPxComboBox combo = (ASPxComboBox)e.Editor;

                //combo.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(AccountItemsRequestedByFilterCondition);
                //combo.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(AccountItemRequestedByValue);

                ASPxComboBox accountCombobox = e.Editor as ASPxComboBox;
                accountCombobox.TextField = "Code";
                accountCombobox.DataSource = AccountingBO.getNotParentAccountList(session);
                accountCombobox.DataBind();

                e.Editor.Focus();

            }          
        }

        protected void grdBooking_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            try
            {
                Guid id = Guid.Parse(e.CellValue.ToString());
                e.Cell.Text = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(id).Code;
            }
            catch
            {
            }
        }

        void AccountItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            
            CriteriaOperator filter = new BinaryOperator("Code", "0", BinaryOperatorType.Equal);
            XPCollection<NAS.DAL.Accounting.AccountChart.Account> collection = new XPCollection<NAS.DAL.Accounting.AccountChart.Account>(session, filter);

            foreach (NAS.DAL.Accounting.AccountChart.Account account in AccountingBO.getNotParentAccountList(session))
            {
                collection.Add(account);
            }

            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //CriteriaOperator criteria =
            //    CriteriaOperator.Or(
            //        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
            //        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            //);

            //collection.Criteria = criteria;
            //collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
            
        }

        void AccountItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            NAS.DAL.Accounting.AccountChart.Account obj = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new NAS.DAL.Accounting.AccountChart.Account[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void grdBookingDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AccountId!Key")
            {
                Guid itemUnitId = (Guid)e.Value;
                NAS.DAL.Accounting.AccountChart.Account itemUnit = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(itemUnitId);
                e.DisplayText = itemUnit.Code;
            }
        }

        protected void grdBooking_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBooking_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBookingDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBookingDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceInventoryTransactionId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceTransaction = new XPCollection<SalesInvoiceInventoryAccountingTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (SalesInvoiceInventoryAccountingTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }
        }


    

    }
}