using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.Entry;
using DevExpress.Web.ASPxGridView;
using NAS.BO;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Data.Filtering;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Accounting.UserControl
{
    public partial class ucAccountEntry : System.Web.UI.UserControl
    {
        Session session;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            AccountingPeriodXPO.Session = session;
            AccountXPO.Session = session;
            BookingEntryXPO.Session = session;
            GeneralJournalXPO.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //AccountingBO account = new AccountingBO();
            //bool test = account.ApproveTransaction(session, Guid.Parse("33feca80-a61a-414d-93b9-60c4040f4bf5"));
            //double total = account.TransactionTotalValue(session, Guid.Parse("323d6f90-3bcf-4166-be0c-35331330de11"));
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            Guid key = Guid.NewGuid();
            Session["BookingEntryId"] = key;                                                   
            e.NewValues["BookingEntryId"] = key;
            e.NewValues["CreateDate"] = DateTime.Now;
            e.NewValues["UpdateDate"] = DateTime.Now;            
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
        }

        protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            XPQuery<BookingEntry> BookingEntryQuery = session.Query<BookingEntry>();
            BookingEntry BkE = session.GetObjectByKey<BookingEntry>(Guid.Parse(Session["BookingEntryId"].ToString()));            
            AccountingBO accountBO = new AccountingBO();
            Guid trnId = accountBO.CreateBookingEntryTransaction(session, BkE.BookingEntryId);            
        }

        protected void ASPxGridView3_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdBookingDetail = (ASPxGridView)sender;
            Session["_BookingEntryId"] = grdBookingDetail.GetMasterRowKeyValue();
            XPQuery<BookingEntryTransaction> BookingEntryQuery = session.Query<BookingEntryTransaction>();
            BookingEntryTransaction BkTr = BookingEntryQuery.Where(r => r.BookingEntryId == Session["_BookingEntryId"]).FirstOrDefault();
            //if (BkTr.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
            //{
                
            //}
            Session["Transaction"] = BkTr;
            Session["TransactionId"] = BkTr.TransactionId;            
        }

        protected void ASPxGridView3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (AccountingBO.IsApproved(session, Guid.Parse(Session["TransactionId"].ToString())))
            {
                throw new Exception("Hạch toán đã được duyệt. Không thể thêm!!!");
                //e.Cancel = true;
            }
            //XPQuery<BookingEntryTransaction> BookingEntryQuery = session.Query<BookingEntryTransaction>();
            //BookingEntryTransaction BkTr = BookingEntryQuery.Where(r => r.TransactionId == (Guid)((ASPxGridView)sender).GetMasterRowKeyValue()).FirstOrDefault();                                    
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;           
            e.NewValues["TransactionId"] = Session["Transaction"];           
        }

        protected void ASPxGridView3_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            byte debit = 0;
            byte credit = 0;  
            if (e.NewValues["Debit"] == null)
            {
                debit = 0;
            }
            else if((double)(e.NewValues["Debit"]) != 0)
            {
                debit = 1;
            }
            if (e.NewValues["Credit"] == null)
            {
                credit = 0;
            }
            else if ((double)(e.NewValues["Credit"]) != 0)
            {
                credit = 1;
            }
            switch(debit + credit){
                case 0:{
                    e.RowError = "Phải nhập nợ/có";
                    break;
                }
                    case 1:{
                    break;
                }
                    case 2:{
                        e.RowError = "Chỉ được nhập nợ/có";
                    break;
                }
                    default:{
                    break;
                }
            };

        }

        protected void ASPxGridView3_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {            
   
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            short approveError = 0;
            Exception newEx = new Exception("Không thể hạch toán");
            try
            {                
                XPQuery<BookingEntryTransaction> BookingEntryQuery = session.Query<BookingEntryTransaction>();
                BookingEntryTransaction BkTr = BookingEntryQuery.Where(r => r.TransactionId == (Guid)(Session["Transaction"])).FirstOrDefault();
                //BkTr.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                //BkTr.Save();
                AccountingBO accountBO = new AccountingBO();                
                bool approved = accountBO.ApproveTransaction(session, BkTr.TransactionId);                
                if (approved == false)
                {
                    /// -1: OK. Balance forward. 
                    /// 0: OK. You can approve it. 
                    /// (order: Can't approve) 
                    /// 1: Credit != Debit. 
                    /// 2: Approved. 
                    /// 3: Credit = Debit = 0. 
                    /// 4: Credit &lt; 0. 
                    /// 5: Debit  &lt; 0. 
                    /// 6: Don't have any journal 
                    /// 7: Transaction not exist 
                    /// 8: Credit and Debit &lt; 0 
                    /// 9: Exist more than one Credit Account && more than one Debit Account
                    //approveError = accountBO.IsAvailableApprove(session, BkTr.TransactionId);
                    //switch (approveError)
                    //{
                    //    case -1: { break; }
                    //    case 0: { break; }
                    //    case 1:
                    //        {
                    //            newEx = new Exception("Tổng nợ không bằng tổng có");
                    //            break;
                    //        }
                    //    case 2:
                    //        {
                    //            newEx = new Exception("Đã hạch toán");
                    //            break;
                    //        }
                    //    case 3:
                    //        {
                    //            newEx = new Exception("Nợ và có cùng bằng không");
                    //            break;
                    //        }
                    //    case 4:
                    //        {
                    //            newEx = new Exception("Tổng có nhỏ hơn 0");
                    //            break;
                    //        }
                    //    case 5:
                    //        {
                    //            newEx = new Exception("Tổng nợ nhỏ hơn 0");
                    //            break;
                    //        }
                    //    case 6:
                    //        {
                    //            newEx = new Exception("Chưa có nợ và có");
                    //            break;
                    //        }
                    //    case 7:
                    //        {
                    //            newEx = new Exception("Không tồn tại bút toán");
                    //            break;
                    //        }
                    //    case 8:
                    //        {
                    //            newEx = new Exception("Tổng nợ và tổng có nhỏ hơn 0");
                    //            break;
                    //        }
                    //    case 9:
                    //        {
                    //            newEx = new Exception("Tồn tại nhiều nợ với nhiều có");
                    //            break;
                    //        }
                    //    default: { break; }
                    //}
                    //ASPxGridView1.DoRowValidation();
                    //ASPxGridView1_RowValidating(ASPxGridView1, new DevExpress.Web.Data.ASPxDataValidationEventArgs(true));                    
                    //throw newEx;                    
                }
                //XPQuery<GeneralJournal> GeneralJournalQuery = session.Query<GeneralJournal>();
                //foreach (GeneralJournal gj in GeneralJournalQuery.Where(r => r.TransactionId.TransactionId == (Guid)Session["TransactionId"]))
                //{
                //    accountBO.CreateGeneralLedger(session, gj.AccountId.AccountId, gj.TransactionId.TransactionId, gj.Description, gj.Debit, gj.Credit, true);
                //};
            }
            catch (Exception)
            {
                //throw newEx;
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid bookentryid = Guid.Parse(e.Keys["BookingEntryId"].ToString());
            XPQuery<BookingEntryTransaction> TransactionQuery = session.Query<BookingEntryTransaction>();
            BookingEntryTransaction transaction = (from c in TransactionQuery
                                                   where c.BookingEntryId.BookingEntryId == bookentryid && c.RowStatus >0
                                                   select c).FirstOrDefault();
            if (transaction != null)
            {
                if (AccountingBO.IsApproved(session, transaction.TransactionId) == true)
                {
                    throw new Exception("Hạch toán đã được duyệt. Không thể xóa!!!");
                    //e.Cancel = true;
                }
                try
                {
                    foreach (GeneralJournal gj in transaction.GeneralJournals)
                    {
                        if (gj != null)
                        {
                            gj.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally{
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                }                
            }
        }

        protected void ASPxGridView1_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {            
        }

        protected void grd_Journal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if(AccountingBO.IsApproved(session,Guid.Parse(Session["TransactionId"].ToString())))
            {
                throw new Exception("Hạch toán đã được duyệt. Không thể xóa!!!");
                //e.Cancel = true;
            }
        }

        protected void grd_Journal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (AccountingBO.IsApproved(session, Guid.Parse(Session["TransactionId"].ToString())))
            {
                throw new Exception("Hạch toán đã được duyệt. Không thể chỉnh sửa!!!");
                //e.Cancel = true;
            }
        }

        protected void grd_Journal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (AccountingBO.IsApproved(session, Guid.Parse(Session["TransactionId"].ToString())))
            {
                throw new Exception("Hạch toán đã được duyệt. Không thể thêm!!!");                
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxCallbackPanel cp = sender as ASPxCallbackPanel;
            ASPxButton bt1 = cp.FindControl("bt_Approve") as ASPxButton;
            if (AccountingBO.IsApproved(session, Guid.Parse(Session["TransactionId"].ToString())))
            {                
                if (bt1 != null)
                {
                    bt1.Visible = false;
                    bt1.Enabled = false;
                }
            }
            else
            {
                if (bt1 != null)
                {
                    bt1.Visible = true;
                    bt1.Enabled = true;
                }                
            }
        }

        protected void grd_Journal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("AccountId!Key"))
            {
                try
                {
                    ASPxComboBox accountCombobox = e.Editor as ASPxComboBox;
                    accountCombobox.TextField = "Code";
                    accountCombobox.DataSource = AccountingBO.getNotParentAccountCollection(session);
                    accountCombobox.DataBind();
                }
                catch (Exception)
                {

                }
            }
        }

        protected void grd_Journal_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            try
            {
                Guid id = Guid.Parse(e.CellValue.ToString());
                e.Cell.Text = session.GetObjectByKey<Account>(id).Code;
            }
            catch (Exception)
            {
            }
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            /// -1: OK. Balance forward. 
            /// 0: OK. You can approve it. 
            /// (order: Can't approve) 
            /// 1: Credit != Debit. 
            /// 2: Approved. 
            /// 3: Credit = Debit = 0. 
            /// 4: Credit &lt; 0. 
            /// 5: Debit  &lt; 0. 
            /// 6: Don't have any journal 
            /// 7: Transaction not exist 
            /// 8: Credit and Debit &lt; 0 
            /// 9: Exist more than one Credit Account && more than one Debit Account
            //switch (approveError)
            //{
            //    case -1: { break; }
            //    case 0: { break; }
            //    case 1: {
            //        e.RowError = "Tổng nợ không bằng tổng có";
            //        break; 
            //    }
            //    case 2: {
            //        e.RowError = "2";
            //        break; 
            //    }
            //    case 3: { 
            //        e.RowError = "3"; 
            //        break; 
            //    }
            //    case 4: {
            //        e.RowError = "4";
            //        break; 
            //    }
            //    case 5: {
            //        e.RowError = "5";
            //        break; 
            //    }
            //    case 6: {
            //        e.RowError = "6";
            //        break; 
            //    }
            //    case 7: {
            //        e.RowError = "7";
            //        break; 
            //    }
            //    case 8: {
            //        e.RowError = "8";
            //        break; 
            //    }
            //    case 9: {
            //        e.RowError = "9";
            //        break; 
            //    }
            //    default: { break; }
            //}
            
            AccountingBO acountingBO = new AccountingBO();
            string entryCode = null;
            string entryOldCode = null;
            try { 
                entryCode = e.NewValues["Code"].ToString(); 
            }
            catch { 
            }
            try
            {
                entryOldCode = e.OldValues["Code"].ToString();
            }
            catch
            {
            }
            if (entryCode != null)
            {
                if (acountingBO.IsExistTransactionCode(session, entryCode) == true && entryCode != entryOldCode)
                {
                    e.RowError = "Mã bút toán đã tồn tại";
                }
            }
        }

    }
}