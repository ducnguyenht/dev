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
using DevExpress.Web.ASPxCallbackPanel;
using NAS.BO.Accounting;

namespace ERPCore.Accounting.UserControl
{
    public partial class uPopupphieumuaban : System.Web.UI.UserControl
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
            SaleInvoiceTransactionXDS.Session = session;            

        }

        protected void grdBooking_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }

        protected void grdBooking_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
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
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }

            e.NewValues["TransactionId"] = Guid.NewGuid();
            e.NewValues["AccountingPeriodId!Key"] =
                NAS.BO.Accounting.Journal.AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId;


        }

        protected void grdBooking_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

            }

            SaleInvoiceTransaction saleInvoiceTransaction1 = session.GetObjectByKey<SaleInvoiceTransaction>(Guid.Parse(e.Values["SalesInvoiceId!Key"].ToString()));
            if (saleInvoiceTransaction1 != null)
            {
                saleInvoiceTransaction1.RowStatus = -1;
            }
            else
            {
                grdBooking.CancelEdit();
            }



            //Transaction transaction = session.GetObjectByKey<Transaction>(Guid.Parse(e.Values["TransactionId"].ToString()));
            //saleInvoiceTransaction.Delete();
        }
        
        protected void cpBooking_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "view":

                    //grdBooking.DetailRows.CollapseAllRows();

                    //if (Session["SaleBillId"] == null)
                    //{
                    //    return;
                    //}
                    
                    SaleInvoiceTransactionBO saleInvoiceTransactionBO = new SaleInvoiceTransactionBO();
                    XPCollection<Transaction> collectTransaction = saleInvoiceTransactionBO.GetTransactionsAndRelatedTransactions(session, Guid.Parse(Session["SaleBillId"].ToString()));


                    //session = XpoHelper.GetNewSession();
                    //SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
                    
                    //CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    //XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

                    grdBooking.DataSource = collectTransaction;
                    grdBooking.DataBind();                   

                    //cpBooking.JSProperties.Add("cpEnable", "true");

                    break;

                case "booking":
                    //salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));

                    //filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    //saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

                    //foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                    //{
                    //    double credit = 0;
                    //    double debit = 0;

                    //    filter = new GroupOperator(GroupOperatorType.And,
                    //                            new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal),
                    //                            new BinaryOperator("RowStatus", ROW_NEW, BinaryOperatorType.Equal));
                    //    XPCollection<GeneralJournal> generalJournal = new XPCollection<GeneralJournal>(session, filter);

                    //    foreach (GeneralJournal x in generalJournal)
                    //    {
                    //        filter = new GroupOperator(GroupOperatorType.And,
                    //                            new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal),
                    //                            new BinaryOperator("AccountId", x.AccountId, BinaryOperatorType.Equal),
                    //                            new BinaryOperator("RowStatus", ROW_NEW, BinaryOperatorType.Equal));

                    //        XPCollection<GeneralJournal> generalJ = new XPCollection<GeneralJournal>(session, filter);
                    //        if (generalJ.Count >= 2)
                    //        {
                    //            cpBooking.JSProperties.Add("cpUnbooked", "1");
                    //            return;
                    //        }

                    //        credit += x.Credit;
                    //        debit += x.Debit;
                    //    }

                    //    if (credit != debit)
                    //    {
                    //        cpBooking.JSProperties.Add("cpUnbooked", "2");
                    //        return;
                    //    }

                    //}

                    //foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                    //{
                    //    item.RowStatus = ROW_NOT_DELETE;
                    //    item.Save();
                        
                    //    filter = new BinaryOperator("TransactionId", item.TransactionId, BinaryOperatorType.Equal);
                    //    XPCollection<GeneralJournal> generalJournal = new XPCollection<GeneralJournal>(session, filter);

                    //    double milsec = 0;
                    //    foreach (GeneralJournal x in generalJournal)
                    //    {
                    //        NAS.DAL.Accounting.AccountChart.Account account = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(x.AccountId.AccountId);
                    //        //General general = new General();
                    //        //general.BalanceUpdate(session, account, x, false, x.Debit-x.Credit);
                    //        AccountingBO accountBO = new AccountingBO();
                    //        accountBO.CreateGeneralLedger(session, x.TransactionId.TransactionId, x.GeneralJournalId,milsec);                            
                    //        milsec += 10;
                    //    }
                    //}


                    ////salesInvoice.RowStatus = ROW_NOT_DELETE;
                    //salesInvoice.Save();


                    //grdBooking.DetailRows.CollapseAllRows();
                    //grdBooking.Columns["Thao tác"].Visible = false;

                    //cpBooking.JSProperties.Add("cpSuccess", "true");            

                    break;

                default:
                    break;
            }
        }

        protected void cpBookingCommand_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
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

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            grdBookingDetail.CancelEdit();

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
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

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
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

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
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
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
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

            e.Cancel = true;
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
            Bill bill = session.GetObjectByKey<Bill>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (e.Column.FieldName == "SalesInvoiceId.Code")
                {
                    e.Editor.Value = bill.Code;
                }
                if (e.Column.FieldName == "IssueDate")
                {
                    e.Editor.Value = bill.IssuedDate;
                }
                if (e.Column.FieldName == "AccountingPeriodId!Key")
                {
                    e.Editor.Value = "5eaa2bf5-34ba-47c6-88df-48ad25bc6e18";
                }
                if (e.Column.FieldName == "SalesInvoiceId!Key")
                {
                    e.Editor.Value = Session["SaleBillId"].ToString();
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
            //grdBooking.JSProperties.Add("cpRefresh", "refresh");
            //NAS.DAL.Accounting.AccountChart.Account account = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(Guid.Parse(e.NewValues["AccountId!Key"].ToString()));

            //GeneralJournal generalJournal = session.GetObjectByKey<GeneralJournal>(Guid.Parse(e.NewValues["GeneralJournalId"].ToString()));

            //General general = new General();
            //general.BalanceUpdate(session, account, generalJournal, false, double.Parse(e.NewValues["Debit"].ToString()) - double.Parse(e.NewValues["Credit"].ToString()));

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

            //CriteriaOperator filter = new BinaryOperator("Code", "0", BinaryOperatorType.Equal);
            //XPCollection<NAS.DAL.Accounting.AccountChart.Account> collection = new XPCollection<NAS.DAL.Accounting.AccountChart.Account>(session, filter);

            //foreach (NAS.DAL.Accounting.AccountChart.Account account in AccountingBO.getNotParentAccountList(session))
            //{
            //    collection.Add(account);
            //}

            //XPCollection<NAS.DAL.Accounting.AccountChart.Account> collection = AccountingBO.getNotParentAccountCollection(session);

            //collection.SkipReturnedObjects = e.BeginIndex;
            //collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //CriteriaOperator criteria =
            //    CriteriaOperator.Or(
            //        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
            //        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
            //);

            //collection.Criteria = criteria;
            //collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            //comboItemUnit.DataSource = collection;
            //comboItemUnit.DataBindItems();
            

        }

        void AccountItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            //ASPxComboBox comboItemUnit = source as ASPxComboBox;
            //NAS.DAL.Accounting.AccountChart.Account obj = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(e.Value);

            //if (obj != null)
            //{
            //    comboItemUnit.DataSource = new NAS.DAL.Accounting.AccountChart.Account[] { obj };
            //    comboItemUnit.DataBindItems();
            //}
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
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
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
            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBooking.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBookingDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }
        }

        protected void grdBookingDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;

            CriteriaOperator filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<SaleInvoiceTransaction> saleInvoiceTransaction = new XPCollection<SaleInvoiceTransaction>(session, filter);

            if (saleInvoiceTransaction.Count > 0)
            {
                foreach (SaleInvoiceTransaction item in saleInvoiceTransaction)
                {
                    if (item.RowStatus == ROW_NOT_DELETE)
                    {
                        grdBookingDetail.CancelEdit();
                        return;
                    }
                }
            }
        }

        //Boolean isExpanded = false;
        //int index = -1;

        protected void grdBookingDetail_DataBound(object sender, EventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //GridViewDetailRowTemplateContainer container = grid.NamingContainer as GridViewDetailRowTemplateContainer;

            //if (isExpanded &&
            //    container.VisibleIndex == index &&
            //    grid.VisibleRowCount == 0)
            //{
            //    grid.AddNewRow();
                
            //}
        }

        protected void grdBooking_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            //isExpanded = e.Expanded;
            //index = e.VisibleIndex;
        }

        protected void grdBookingDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

      
      
    }
}