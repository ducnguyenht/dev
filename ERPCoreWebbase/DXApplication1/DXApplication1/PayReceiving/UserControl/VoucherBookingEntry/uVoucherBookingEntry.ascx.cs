using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.PayReceiving.UserControl.VoucherBookingEntry.State;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting;
using NAS.BO.Vouches;
using DevExpress.Web.ASPxEditors;
using Utility;
using NAS.DAL.Vouches.Allocation;
using DevExpress.Data.Filtering;

namespace WebModule.PayReceiving.UserControl.VoucherBookingEntry
{
    public partial class uVoucherBookingEntry : System.Web.UI.UserControl
    {
        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        public void SettingInit(Guid VoucherId, ASPxButton SourceButton, string completeEventName)
        {
            this.VoucherId = VoucherId;
            dsOriginArtifact.CriteriaParameters["VoucherId"].DefaultValue = VoucherId.ToString();
            dsVoucherAllocation.CriteriaParameters["VoucherId"].DefaultValue = VoucherId.ToString();
            frmCosting.DataBind();

            if (!MainControlClientName.Equals(string.Empty))
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('open');", MainControlClientName) +
                    " }";

                cpVoucherBookingEntry.ClientSideEvents.BeginCallback = "function(s, e){ " +
                    string.Format("{0}.Show();", ldpnCostingEditForm.ClientInstanceName) +
                    " }";

                cpVoucherBookingEntry.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnCostingEditForm.ClientInstanceName) +
                    "target.fire({ type: '" + completeEventName + "' }); " +
                    " }";

                ButtonApproveCosting.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Book');", MainControlClientName)
                    + " }";

                ButtonCancel.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Cancel');", MainControlClientName)
                    + " }";
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsOriginArtifact.Session = session;
            dsVoucherAllocation.Session = session;
            dsAllocation.Session = session;
            dsVoucherAllocationBookingAccount.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                cpVoucherBookingEntry.ClientInstanceName = string.Format("cpVoucherBookingEntry{0}", ViewStateControlId);
                ldpnCostingEditForm.ClientInstanceName = string.Format("ldpnCostingEditForm{0}", ViewStateControlId);
                GUIContext = new NAS.GUI.Pattern.Context();
            }

            dsOriginArtifact.CriteriaParameters["VoucherId"].DefaultValue = VoucherId.ToString();
            dsVoucherAllocation.CriteriaParameters["VoucherId"].DefaultValue = VoucherId.ToString();
        }

        protected void cpVoucherBookingEntry_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];

            switch (trs)
            {
                case "open":
                    NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
                    if (v == null)
                        throw new Exception("The Voucher is not exist");

                    if (v.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        GUIContext.State = new ReadyBookingVoucherState(this);
                    else
                        GUIContext.State = new BookedVoucherState(this);
                    break;
                default:
                    GUIContext.Request(trs, this);
                    break;
            }

            //if (trs.Equals("Book"))
            //    cpVoucherBookingEntry.JSProperties.Add("cp"
        }

        protected void grdDetailEntry_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName.Equals("AccountId!Key"))
            {
                ASPxComboBox accountComboBox = (ASPxComboBox)e.Editor;
                if (accountComboBox != null)
                {
                    accountComboBox.DataSource = AccountingBO.getNotParentAccountList(session);
                    accountComboBox.DataBindItems();
                    e.Editor.Focus();
                }
            }
        }

        protected void grdDetailEntry_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            dsVoucherAllocationBookingAccount.CriteriaParameters["VoucherAllocationId"].DefaultValue = grid.GetMasterRowKeyValue().ToString();
        }

        protected void grdTransaction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["VouchesId!Key"] = VoucherId;
        }

        protected void grdDetailEntry_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
            if (v == null)
                throw new Exception("The Voucher is not exist");

            if (v.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu đã đã được duyệt. Không thể thêm thông tin!");

            ASPxGridView grid = sender as ASPxGridView;
            e.NewValues["VoucherAllocationId!Key"] = grid.GetMasterRowKeyValue().ToString();
        }

        #region Properties

        private ASPxButton ButtonApproveCosting
        {
            get
            {
                ASPxButton button = popCosting.FindControl("btnApproveCosting") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonCancel
        {
            get
            {
                ASPxButton button = popCosting.FindControl("btnCancel") as ASPxButton;
                return button;
            }
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }

        private Guid VoucherId
        {
            get
            {
                if (Session["VoucherId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["VoucherId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["VoucherId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private Session session
        {
            get;
            set;
        }

        public string MainControlClientName
        {
            get
            {
                return cpVoucherBookingEntry.ClientInstanceName;
            }
        }
        #endregion 

        #region State Pattern and GUI/CRUD/PreTransit

        private NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session["GUIContext" + this.ClientID + ViewStateControlId] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session["GUIContext" + this.ClientID + ViewStateControlId] = value;
            }
        }

        public void UpdateGUI_BookedVoucherState()
        {
            NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
            popCosting.ShowOnPageLoad = true;
            ButtonApproveCosting.ClientVisible = false;
            lblIsApprovedCosting.Text = "Đã hạch toán";
            lblSumOfDebit.Text = v.SumOfCredit.ToString();
        }

        public void UpdateGUI_ReadyBookingVoucherState()
        {
            NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
            popCosting.ShowOnPageLoad = true;
            ButtonApproveCosting.ClientVisible = true;
            lblIsApprovedCosting.Text = "Chưa hạch toán";
            lblSumOfDebit.Text = v.SumOfCredit.ToString();
        }

        public void UpdateGUI_CancelBookingVoucherState()
        {
            popCosting.ShowOnPageLoad = false;
        }

        public void CRUD_BookedVoucherState()
        {
            NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
            if (v.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
            {
                v.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                v.Save();
            }
        }

        public void CRUD_ReadyBookingVoucherState()
        {
            VoucherBookingEntryBO bo = new VoucherBookingEntryBO();
            bo.GenerateTemplateVoucherForBookingEntry(VoucherId);
        }

        public void PreCRUD_ReadyBookingVoucherState()
        {
            VoucherBookingEntryBO bo = new VoucherBookingEntryBO();
            bo.ValidateVoucherForBookingEntry(VoucherId);
        }

        #endregion

        protected void grdDetailEntry_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name.Equals("AccountId"))
            { 
                ASPxGridView grid = sender as ASPxGridView;
                if (grid != null)
                    e.Cell.Text = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(grid.GetRowValuesByKeyValue(e.KeyValue, "AccountId!Key")).Code;
            }
        }

        protected void grdDetailEntry_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            Guid voucherAllocationId = (Guid)grid.GetMasterRowKeyValue();
            Guid accountId = (Guid)e.NewValues["AccountId!Key"];

            VoucherAllocationBookingAccount vaba = session.FindObject<VoucherAllocationBookingAccount>(
                    CriteriaOperator.And(
                        new BinaryOperator("AccountId!Key", accountId, BinaryOperatorType.Equal),
                        new BinaryOperator("VoucherAllocationId!Key", voucherAllocationId, BinaryOperatorType.Equal)
                            ));

            //Validate duuplicate account in transaction
            //Insert mode
            if (grid.IsNewRowEditing)
            {
                
                if (vaba != null)
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
                    if (vaba != null)
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
                    && ((double)e.NewValues["Credit"]) > 0) && (e.NewValues["Debit"] != null
                    && ((double)e.NewValues["Debit"]) > 0))
            {
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Credit"], "Chỉ cho phép nhập Nợ hoặc nhập Có");
                Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Debit"], "Chỉ cho phép nhập Nợ hoặc nhập Có");
            }
            //Check leave blank both credit and debit
            else
                if ((e.NewValues["Credit"] == null
                    || ((double)e.NewValues["Credit"]) == 0) && (e.NewValues["Debit"] == null
                    || ((double)e.NewValues["Debit"]) == 0))
                {
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Credit"], "Chưa nhập Nợ hoặc nhập Có");
                    Utility.Helpers.AddErrorToGridViewColumn(e.Errors, grid.Columns["Debit"], "Chưa nhập Nợ hoặc nhập Có");
                }
        }

        protected void grdDetailEntry_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            NAS.DAL.Vouches.Vouches v = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
            if (v == null)
                throw new Exception("The Voucher is not exist");

            if (v.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu đã đã được duyệt. Không thể xóa thông tin!");
        }
    }
}