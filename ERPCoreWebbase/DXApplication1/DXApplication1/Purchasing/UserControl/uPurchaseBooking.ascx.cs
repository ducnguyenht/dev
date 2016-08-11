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
using NAS.BO.Accounting;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uPurchaseBooking : System.Web.UI.UserControl
    {
        Session session;

        static short ROW_NEW = 1;
        static short ROW_DELETE = -1;
        static short ROW_NOT_DELETE = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
   
            GeneralJournalXDS.Session = session;
            PurchaseInvoiceTransactionXDS.Session = session;
        }

        protected void grdBooking_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }

        protected void grdBooking_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
            grdBooking.DetailRows.ExpandRow(grdBooking.FocusedRowIndex);
            
        }

        protected void grdBooking_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)

        {
            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
            

            AccountingBO account = new AccountingBO();
            //e.NewValues["AccountingPeriodId"] = AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId;
            TimeSpan time= DateTime.Now.TimeOfDay;
            e.NewValues["IssueDate"] = DateTime.Parse(e.NewValues["IssueDate"].ToString()) + time;           
            e.NewValues["UpdateDate"] = DateTime.Now;
            e.NewValues["CreateDate"] = DateTime.Now;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //Transaction prevTran = account.FindPreviousTransaction(session, DateTime.Parse(e.NewValues["IssueDate"].ToString()));
            //Transaction nextTran = account.FindNextTransaction(session, DateTime.Parse(e.NewValues["IssueDate"].ToString()));
            //if (nextTran != null)
            //{
            //    nextTran.PreviousTransactionId = Guid.Empty;
            //    Session["nextTranId"] = nextTran.TransactionId;
            //}
            //else
            //{
            //    Session["nextTranId"] = Guid.Empty;
            //}
            //if (prevTran != null)
            //{
            //    Session["prevTranId"] = prevTran.TransactionId;
            //}
            //else
            //{
            //    Session["prevTranId"] = Guid.Empty;
            //}
            //e.NewValues["PreviousTransactionId"] = (prevTran == null)? Guid.Empty : prevTran.TransactionId;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
            e.NewValues["TransactionId"] = Guid.NewGuid();
        }
    
        protected void grdBooking_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            
            Transaction nextTran = null;
            try
            {
                nextTran = session.GetObjectByKey<Transaction>(Guid.Parse(Session["nextTranId"].ToString()));
            }
            catch (Exception)
            {
                nextTran = null;
            }
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //XPQuery<Transaction> transactionQuery = session.Query<Transaction>();
            //Guid a = Guid.Parse(Session["prevTranId"].ToString());
            //Transaction newstransaction = (from c in transactionQuery
            //                               where c.PreviousTransactionId == a
            //                               select c).FirstOrDefault();
            //if (nextTran != null)
            //{
            //    nextTran.PreviousTransactionId = (newstransaction == null) ? Guid.Empty : newstransaction.TransactionId;
            //}
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            Guid id = (Guid)e.NewValues["TransactionId"];
            grdBooking.DetailRows.ExpandRowByKey(id);

        }

        protected void grdBooking_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
        
        protected void cpBooking_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {               
                case "view":

                    grdBooking.DetailRows.CollapseAllRows();

                    if (Session["BillId"] == null)
                    {
                        return;
                    }

                    session = XpoHelper.GetNewSession();
                    PurchaseInvoice purchaseInvoice = session.GetObjectByKey<PurchaseInvoice>(Guid.Parse(Session["BillId"].ToString()));

                    CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
                    XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

                    if (purchaseInvoiceTransaction.Count > 0)
                    {
                        foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                        {
                            if (AccountingBO.IsApproved(session, item.TransactionId) == true)
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

                    break;

                case "booking":
                    purchaseInvoice = session.GetObjectByKey<PurchaseInvoice>(Guid.Parse(Session["BillId"].ToString()));

                    filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
                    purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);
                                        
                    foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
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

                    foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
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
                            //general.BalanceUpdate(session, account, x, false, x.Debit - x.Credit);
                            AccountingBO accountBO = new AccountingBO();
                            accountBO.CreateGeneralLedger(session, x.TransactionId.TransactionId, x.GeneralJournalId,milsec);                            
                            milsec += 10;
                        }
                    }

                    //purchaseInvoice.RowStatus = ROW_NOT_DELETE;
                    purchaseInvoice.Save();

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
            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (AccountingBO.IsApproved(session, item.TransactionId) == true)
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

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {  
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (AccountingBO.IsApproved(session, item.TransactionId) == true)
                    {                        
                        grdBookingDetail.Columns["Thao tác"].Visible = false;
                        break;
                    }
                }
            }
        }

        protected void grdBookingDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
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

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
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
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
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

            GridViewDataColumn col2 = ((ASPxGridView)sender).Columns["Debit"] as GridViewDataColumn;
            ASPxSpinEdit spin2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "colBookingDetailDebit") as ASPxSpinEdit;

            if (double.Parse(spin1.Value.ToString()) > 0 & double.Parse(spin2.Value.ToString()) > 0)
            {
                e.Errors.Add(col1, "The dummy error");
                e.RowError = "Nợ hoặc Có phải = 0";

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
            Bill bill = session.GetObjectByKey<Bill>(Guid.Parse(Session["BillId"].ToString()));
            if (bill != null)
            {
                if (e.Column.FieldName == "PurchaseInvoiceId.Code")
                {
                    e.Editor.Value = bill.Code;
                }
                if (e.Column.FieldName == "IssueDate")
                {
                    e.Editor.Value = bill.IssuedDate;
                }
                if (e.Column.FieldName == "AccountingPeriodId!Key")
                {
                    e.Editor.Value = AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId;
                }
                if (e.Column.FieldName == "PurchaseInvoiceId!Key")
                {
                    e.Editor.Value = Session["BillId"].ToString();
                }
                if (e.Column.FieldName == "CreateDate")
                {
                    e.Editor.Value = DateTime.Now;
                }
                if (e.Column.FieldName == "Code")
                {
                    e.Editor.Focus();
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

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria =
                CriteriaOperator.Or(
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

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

        protected void grdBookingDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView) sender;

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (AccountingBO.IsApproved(session, item.TransactionId) == true)
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

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBooking_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
        }
     

        protected void grdBooking_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("PurchaseInvoiceId", Guid.Parse(Session["BillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<PurchaseInvoiceTransaction> purchaseInvoiceTransaction = new XPCollection<PurchaseInvoiceTransaction>(session, filter);

            if (purchaseInvoiceTransaction.Count > 0)
            {
                foreach (PurchaseInvoiceTransaction item in purchaseInvoiceTransaction)
                {
                    if (item.RowStatus == 2)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
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

        
    }
}