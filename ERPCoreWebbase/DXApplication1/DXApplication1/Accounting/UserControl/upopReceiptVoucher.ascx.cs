using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Utility;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO.System.ArtifactCode;

namespace WebModule.Accounting.UserControl
{
    public partial class upopReceiptVoucher : System.Web.UI.UserControl
    {
        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.VoucherId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["WebModule_Accounting_UserControl_upopReceiptVoucher"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["WebModule_Accounting_UserControl_upopReceiptVoucher"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("WebModule_Accounting_UserControl_upopReceiptVoucher");
            }

            /////Declares all session properties here
            public Guid VoucherId { get; set; }
        }


        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            dsAccount.Session = session;
            dsGeneralJournal.Session = session;
            dsOriginArtifact.Session = session;
            dsTransaction.Session = session;
        }

        private ArtifactCodeRuleBO artifactCodeRuleBO;

        protected void Page_Load(object sender, EventArgs e)
        {
            artifactCodeRuleBO = new ArtifactCodeRuleBO();
            dsOriginArtifact.CriteriaParameters["VoucherId"].DefaultValue = PrivateSession.Instance.VoucherId.ToString();
            dsTransaction.CriteriaParameters["VoucherId"].DefaultValue = PrivateSession.Instance.VoucherId.ToString();
            frmCosting.DataBind();

            if (PrivateSession.Instance.VoucherId != Guid.Empty)
            {
                NAS.DAL.Vouches.ReceiptVouches receiptVoucher =
                            session.GetObjectByKey<NAS.DAL.Vouches.ReceiptVouches>(PrivateSession.Instance.VoucherId);
                string sumOfDebitFormatted = String.Format("{0:#,###}", receiptVoucher.SumOfDebit);
                lblSumOfDebit.Text = sumOfDebitFormatted;
            }

            this.HideGridViewColumnsWhenApprovedCosting(grdTransaction, "CommonOperations");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void popCosting_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "edit":
                    PrivateSession.Instance.VoucherId = Guid.Parse(args[1]);

                    dsOriginArtifact.CriteriaParameters["VoucherId"].DefaultValue = PrivateSession.Instance.VoucherId.ToString();
                    dsTransaction.CriteriaParameters["VoucherId"].DefaultValue = PrivateSession.Instance.VoucherId.ToString();

                    if (PrivateSession.Instance.VoucherId != Guid.Empty)
                    {
                        NAS.DAL.Vouches.ReceiptVouches receiptVoucher =
                                    session.GetObjectByKey<NAS.DAL.Vouches.ReceiptVouches>(PrivateSession.Instance.VoucherId);
                        string sumOfDebitFormatted = String.Format("{0:#,###}", receiptVoucher.SumOfDebit);
                        lblSumOfDebit.Text = sumOfDebitFormatted;
                    }

                    break;

                case "approveCosting":
                    try
                    {
                        TransactionBO.ProcessApproveCosting<NAS.DAL.Vouches.ReceiptVouches>
                        (session, PrivateSession.Instance.VoucherId);

                        this.HideGridViewColumnsWhenApprovedCosting(grdTransaction, "CommonOperations");
                    }
                    catch (Exception ex)
                    {
                        popCosting.JSProperties["cpException"] = ex.Message;
                    }
                    finally
                    {
                        popCosting.JSProperties["cpEvent"] = "approveComplete";
                    }
                    break;

                default:
                    break;
            }

            bool isApprovedCosting =
                        this.HideGridViewColumnsWhenApprovedCosting(grdTransaction, "CommonOperations");

            if (isApprovedCosting)
            {
                lblIsApprovedCosting.Text = "Đã hạch toán";
            }
            else
            {
                lblIsApprovedCosting.Text = "Chưa hạch toán";
            }

            popCosting.JSProperties["cpIsApprovedCosting"] = isApprovedCosting;
        }

        protected void lblSumOfDebit_PreRender(object sender, EventArgs e)
        {
            //ASPxLabel label = sender as ASPxLabel;
            //label.Text = String.Format("{0:#,###}", label.Text);
        }

        protected void grdTransaction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["ReceiptVouchesId!Key"] = PrivateSession.Instance.VoucherId;
            e.NewValues["AccountingPeriodId!Key"] =
                NAS.BO.Accounting.Journal.AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId;
            e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
            e.NewValues["CreateDate"] = DateTime.Now;
            e.NewValues["UpdateDate"] = DateTime.Now;
            e.NewValues["TransactionId"] = Guid.NewGuid();
        }

        protected void grdTransaction_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
        }

        protected void grdTransaction_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
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

        private bool HideGridViewColumnsWhenApprovedCosting(ASPxGridView grid, params string[] columnsName)
        {
            if (PrivateSession.Instance.VoucherId != Guid.Empty)
            {
                bool isApprovedCosting =
                    TransactionBO.isApprovedCosting<NAS.DAL.Vouches.ReceiptVouches>(session, PrivateSession.Instance.VoucherId);
                if (isApprovedCosting)
                {
                    foreach (var columnName in columnsName)
                    {
                        grid.Columns[columnName].Visible = false;
                    }
                }
                else
                {
                    foreach (var columnName in columnsName)
                    {
                        grid.Columns[columnName].Visible = true;
                    }
                }
                return isApprovedCosting;
            }
            return false;
        }

        protected void grdGeneralJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            dsGeneralJournal.CriteriaParameters["TransactionId"].DefaultValue = grid.GetMasterRowKeyValue().ToString();
            this.HideGridViewColumnsWhenApprovedCosting(grid, "CommonOperations");
        }

        protected void grdGeneralJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            e.NewValues["RowStatus"] = Utility.Constant.ROWSTATUS_ACTIVE;
            e.NewValues["TransactionId!Key"] = grid.GetMasterRowKeyValue();
            if (AccountingBO.IsApproved(session, Guid.Parse(grid.GetMasterRowKeyValue().ToString())))
            {
                throw new Exception("Hạch toán đã được duyệt. Không thể thêm!!!");
                e.Cancel = true;
            }
            if (e.NewValues["Credit"] == null)
            {
                e.NewValues["Credit"] = (double)0;
            }
            if (e.NewValues["Debit"] == null)
            {
                e.NewValues["Debit"] = (double)0;
            }
        }

        protected void grdGeneralJournal_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
            Guid accountId = (Guid)e.NewValues["AccountId!Key"];

            //Validate duuplicate account in transaction
            //Insert mode
            if (grid.IsNewRowEditing)
            {
                bool isExist = TransactionBO.isExistAccountInTransaction(transactionId, accountId);
                if (isExist)
                {
                    Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["AccountId!Key"],
                            String.Format("Tài khoản này đã được chọn"));
                }
            }
            //Edit mode
            else
            {
                Guid oldAccountId = (Guid)e.OldValues["AccountId!Key"];
                if (!oldAccountId.Equals(accountId))
                {
                    bool isExist = TransactionBO.isExistAccountInTransaction(transactionId, accountId);
                    if (isExist)
                    {
                        Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["AccountId!Key"],
                                String.Format("Tài khoản này đã được chọn"));
                    }
                }
            }

            //Check negative
            if (e.NewValues["Credit"] != null
                    && (double)e.NewValues["Credit"] < 0)
            {
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Credit"], "Không thể nhập số âm");
            }
            if (e.NewValues["Debit"] != null
                    && (double)e.NewValues["Debit"] < 0)
            {
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Debit"], "Không thể nhập số âm");
            }
            //Check input both credit and debit
            if ((e.NewValues["Credit"] != null
                //&& e.NewValues["Credit"].ToString().Length > 0
                    && ((double)e.NewValues["Credit"]) > 0) && (e.NewValues["Debit"] != null
                //&& e.NewValues["Debit"].ToString().Length > 0
                    && ((double)e.NewValues["Debit"]) > 0))
            {
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Credit"], "Chỉ cho phép nhập Nợ hoặc nhập Có");
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Debit"], "Chỉ cho phép nhập Nợ hoặc nhập Có");
            }
            //Check leave blank both credit and debit
            else
                if ((e.NewValues["Credit"] == null
                    //|| e.NewValues["Credit"].ToString().Length > 0
                    || ((double)e.NewValues["Credit"]) == 0) && (e.NewValues["Debit"] == null
                    //|| e.NewValues["Debit"].ToString().Length > 0
                    || ((double)e.NewValues["Debit"]) == 0))
                {
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Credit"], "Chưa nhập Nợ hoặc nhập Có");
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Debit"], "Chưa nhập Nợ hoặc nhập Có");
                }
        }

        protected void grdGeneralJournal_Load(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            this.HideGridViewColumnsWhenApprovedCosting(grid, "CommonOperations");
        }

        protected void btnApproveCosting_Load(object sender, EventArgs e)
        {
            //if (PrivateSession.Instance.VoucherId != Guid.Empty)
            //{
            //    ASPxButton button = sender as ASPxButton;
            //    bool isApprovedCosting =
            //        TransactionBO.isApprovedCosting<NAS.DAL.Vouches.ReceiptVouches>(PrivateSession.Instance.VoucherId);
            //    if (isApprovedCosting)
            //    {
            //        button.Visible = false;
            //        grdTransaction.Visible = false;
            //    }
            //    else
            //    {
            //        button.Visible = true;
            //        grdTransaction.Visible = true;
            //    }
            //}
        }

        protected void grdGeneralJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["Credit"] == null)
            {
                e.NewValues["Credit"] = (double)0;
            }
            if (e.NewValues["Debit"] == null)
            {
                e.NewValues["Debit"] = (double)0;
            }
        }

        protected void grdTransaction_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("Code"))
            {
                e.Editor.Focus();
            }
        }

        protected void grdGeneralJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("Description"))
            {
                e.Editor.Focus();
            }
            else if (e.Column.FieldName.Equals("AccountId!Key"))
            {
                ASPxComboBox accountComboBox = (ASPxComboBox)e.Editor;
                if (accountComboBox != null)
                {
                    accountComboBox.DataSource = AccountingBO.getNotParentAccountList(session);
                    accountComboBox.DataBindItems();
                }
            }
        }

        protected void grdTransaction_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            grdTransaction.DetailRows.ExpandRow(grdTransaction.FocusedRowIndex);
        }

        protected void grdTransaction_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            Guid id = (Guid)e.NewValues["TransactionId"];
            grdTransaction.DetailRows.ExpandRowByKey(id);
        }

        protected void grdGeneralJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("AccountId!Key"))
            {
                Guid accountId = (Guid)e.Value;
                Account account = session.GetObjectByKey<Account>(accountId);
                e.DisplayText = String.Format("{0} - {1}", account.Code, account.Name);
            }
        }

        protected void grdTransaction_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Code"] = lblCode.Text + "_" + artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.TRANSACTION);
        }
    }
}
