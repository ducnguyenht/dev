using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using WebModule.Voucher.Receipt.State;
using NAS.BO.Vouches;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.Vouches;
using DevExpress.Web.ASPxEditors;
using WebModule.Voucher.State;
using Utility;
using WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy;
using WebModule.Voucher.Controls.VoucherBookingEntriesForm.Strategy;
using NAS.BO.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Invoice;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.Accounting.Currency;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxFormLayout;
using NAS.BO.Accounting.NSCurrency;

namespace WebModule.Voucher.Receipt.GUI
{
    public partial class ReceiptVoucherEditingForm : System.Web.UI.UserControl
    {
        private Guid VoucherId
        {
            get
            {
                return (Guid)Session["ReceiptVoucherEditingForm_VoucherId_" + ViewStateControlId];
            }
            set
            {
                Session["ReceiptVoucherEditingForm_VoucherId_" + ViewStateControlId] = value;
            }
        }
        private Guid BillId
        {
            get
            {
                return (Guid)Session["ReceiptVoucherEditingForm_BillId_" + ViewStateControlId];
            }
            set
            {
                Session["ReceiptVoucherEditingForm_BillId_" + ViewStateControlId] = value;
            }
        }
        private Session session;
        private VoucherBO voucherBO;
        private ReceiptVouchesBO receiptVouchesBO;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsReceiptVoucher.Session = session;
            dsForeignCurrency.Session = session;
            dsVoucherAmount.Session = session;

            voucherBO = new VoucherBO();
            receiptVouchesBO = new ReceiptVouchesBO();
            //gridviewVoucherAllocation.SetAllocationGetter(
            //    new NAS.BO.Accounting.Configure.AllocationGetter.ReceiptVoucherAllocationGetter());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
                VoucherId = Guid.Empty;
                BillId = Guid.Empty;
                hiddenField["voucherId"] = null;

            }
            if (!VoucherId.Equals(Guid.Empty))
            {
                InitGridViewVoucherAllocation();
                InitVoucherBookingEntriesForm();
            }
            hiddenField["voucherId"] = VoucherId;
            //UpdateBookingEntryState();
            gridviewReceiptVoucherAllocation.InitNewRowWithDefaultDataEvent += new Voucher.Controls.GridViewVoucherAllocation.GridViewVoucherAllocation.InitNewRowWithDefaultDataHandler(gridviewReceiptVoucherAllocation_InitNewRowWithDefaultDataEvent);
        }

        private void InitGridViewVoucherAllocation()
        {
            gridviewReceiptVoucherAllocation.VoucherId = VoucherId;
            gridviewReceiptVoucherAllocation
                .SetGridViewVoucherAllocationStrategy(GridViewVoucherAllocationStrategyEnum.RECEIPT_VOUCHER);
        }

        protected void gridviewReceiptVoucherAllocation_InitNewRowWithDefaultDataEvent(Controls.GridViewVoucherAllocation.GridViewVoucherAllocation.TransactionInitRowData data)
        {
            if (VoucherId != null && !VoucherId.Equals(Guid.Empty))
            {
                data.IssuedDate = txtIssueDate.Date;
                data.Description = txtDescription.Text;

                ReceiptVouches voucher = session.GetObjectByKey<ReceiptVouches>(VoucherId);
                int sequence = 0;
                sequence =
                    voucher.ReceiptVouchesTransactions.Count(r => r.RowStatus == Constant.ROWSTATUS_ACTIVE) + 1;
                data.Code = String.Format("{0}_{1}", txtCode.Text, sequence);

                double amount = 0;
                if (voucher.SumOfDebit != 0)
                {
                    amount = voucher.SumOfCredit - voucher.ReceiptVouchesTransactions.Sum(r => r.Amount);
                    if (amount < 0)
                        amount = 0;
                }
                else
                {
                    amount = (double)spinAmount.Number - voucher.ReceiptVouchesTransactions.Sum(r => r.Amount);
                    if (amount < 0)
                        amount = 0;
                }
                data.Amount = amount;
            }
        }

        private void InitVoucherBookingEntriesForm()
        {
            voucherBookingEntriesForm.VoucherId = VoucherId;
            voucherBookingEntriesForm
                .SetVoucherBookingEntriesFormStrategy(VoucherBookingEntriesFormStrategySimpleEnum.RECEIPT_VOUCHER);
        }

        private void UpdateBookingEntryState()
        {
            if (GUIContext.State is ReceiptVoucherEditing)
            {
                if (voucherBO.CanBookingEntry(VoucherId))
                {
                    GUIContext.State = new ReceiptVoucherCanBookingEntry(this);
                }
                else
                {
                    GUIContext.State = new ReceiptVoucherCannotBookingEntry(this);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private void ClearForm()
        {
            ASPxEdit.ClearEditorsInContainer(cpnReceiptVoucherEditingForm);
            ClearFormValidation();
        }

        private void ClearFormValidation()
        {
            txtCode.IsValid = true;
            txtIssueDate.IsValid = true;
            spinAmount.IsValid = true;
            //cboCurrency.IsValid = true;
            gridlookupCurrency.IsValid = true;
            spinExchangeRate.IsValid = true;
        }

        #region State Pattern
        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
        }

        private ASPxButton Button_DisplatBookingEntries
        {
            get
            {
                return (ASPxButton)popupReceiptVoucherEditingForm.FindControl("btnBookingEntry");
            }
        }

        private ASPxButton Button_Save
        {
            get
            {
                return (ASPxButton)popupReceiptVoucherEditingForm.FindControl("btnSave");
            }
        }

        #region UpdateGUI
        public bool ReceiptVoucherCanBookingEntry_UpdateGUI()
        {
            Button_DisplatBookingEntries.Enabled = true;
            return true;
        }
        public bool ReceiptVoucherCannotBookingEntry_UpdateGUI()
        {
            Button_DisplatBookingEntries.Enabled = false;
            return true;
        }
        public bool ReceiptVoucherCanceling_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = false;
            return true;
        }
        public bool ReceiptVoucherCreating_UpdateGUI()
        {
            //Set default date for issue date
            //txtIssueDate.Date = DateTime.Now;
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = "Thông tin phiếu thu - Thêm mới";

            Button_DisplatBookingEntries.Enabled = false;

            InitGridViewVoucherAllocation();
            return true;
        }
        public bool ReceiptVoucherEditing_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = String.Format("Thông tin phiếu thu - {0}",
                GetCurrentReceiptVoucher().Code);

            InitGridViewVoucherAllocation();

            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
            return true;
        }
        public bool ReceiptVoucherLocked_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = String.Format("Thông tin phiếu thu - {0}",
                GetCurrentReceiptVoucher().Code);

            formlayoutReceiptVoucherEditingForm.Enabled = false;
            formlayoutVoucherAmount.Enabled = false;
            formlayoutExtendedInfomation.Enabled = false;

            Button_Save.Visible = false;

            InitGridViewVoucherAllocation();

            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
            return true;
        }
        #endregion

        #region CRUD
        public bool ReceiptVoucherCanceling_CRUD()
        {
            return true;
        }
        public bool ReceiptVoucherCreating_CRUD()
        {
            ClearForm();
            //Create temp voucher
            ReceiptVouches receiptVouches = receiptVouchesBO.CreateNewObject(session);
            //Update VoucherId
            VoucherId = receiptVouches.VouchesId;
            txtIssueDate.Date = DateTime.Now;

            //Get default code
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.VOUCHER_RECEIPT);

            //Link to the bill
            if (BillId != Guid.Empty)
            {
                //Link bill to voucher
                LinkVoucherWithBill(session, BillId, receiptVouches);
                //Fill bill data into voucher
                FillBillDataIntoVoucher(session, BillId);
            }

            //Get default currency...
            gridlookupCurrency.ResetToDefault();
            spinExchangeRate.Number = 1;
            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));

            //Bind data to VoucherAllocation gridview
            gridviewVoucherAllocation_DataBind();
            return true;
        }
        public bool ReceiptVoucherEditing_CRUD()
        {
            ClearFormValidation();
            //Bind data to voucher information form
            formlayoutReceiptVoucherEditingForm_DataBind();
            //Bind data to voucher ammount form
            formlayoutVoucherAmount_DataBind();
            //Bind data to VoucherAllocation gridview
            gridviewVoucherAllocation_DataBind();

            ReceiptVouches receiptVoucher = GetCurrentReceiptVoucher();
            txtCode.Text = receiptVoucher.Code;
            //spinAmount.Number = (decimal)receiptVoucher.SumOfDebit;
            return true;
        }
        public bool ReceiptVoucherLocked_CRUD()
        {
            ClearFormValidation();
            //Bind data to voucher information form
            formlayoutReceiptVoucherEditingForm_DataBind();
            //Bind data to voucher ammount form
            formlayoutVoucherAmount_DataBind();
            //Bind data to VoucherAllocation gridview
            gridviewVoucherAllocation_DataBind();

            ReceiptVouches receiptVoucher = GetCurrentReceiptVoucher();
            txtCode.Text = receiptVoucher.Code;
            //spinAmount.Number = (decimal)receiptVoucher.SumOfDebit;
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool ReceiptVoucherCanceling_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool ReceiptVoucherCreating_PreTransitionCRUD(string transition)
        {
            //Save data before transit by 'Save' transition 
            if (transition.ToUpper().Equals(VoucherStateTransition.SaveTransition.TransitionName.ToUpper()))
            {
                gridviewReceiptVoucherAllocation.GridView.UpdateEdit();
                //Validate input data
                if (!ASPxEdit.ValidateEditorsInContainer(cpnReceiptVoucherEditingForm, "ReceiptVoucherEditingForm", false))
                {
                    return false;
                }
                //Collects input data
                string code = txtCode.Text;
                DateTime issueDate = txtIssueDate.Date;
                string description = txtDescription.Text;
                string address = txtAddress.Text;
                string payer = txtPayer.Text;
                Guid sourceOrgId = cboSourceOrganization.Value != null ? (Guid)cboSourceOrganization.Value : Guid.Empty;
                Guid targetOrgId = Utility.CurrentSession.Instance.AccessingOrganizationId;
                double debit = (double)spinAmount.Number;
                //Guid currencyId = (Guid)cboCurrency.Value;
                Guid currencyId = (Guid)gridlookupCurrency.GetSelectedCurrencyKey();
                double exchangeRate = (double)spinExchangeRate.Number;

                //Insert data to database
                ReceiptVouchesBO receiptVoucherBO = new ReceiptVouchesBO();
                receiptVoucherBO.Insert(VoucherId,
                                        code,
                                        issueDate,
                                        description,
                                        address,
                                        payer,
                                        sourceOrgId,
                                        targetOrgId,
                                        debit,
                                        currencyId,
                                        exchangeRate);
            }
            else if (transition.ToUpper().Equals(VoucherStateTransition.CancelTransition.TransitionName.ToUpper()))
            {
                voucherBO.DeleteTempObject(VoucherId);
            }
            return true;
        }
        public bool ReceiptVoucherEditing_PreTransitionCRUD(string transition)
        {
            //Save data before transit by 'Save' transition 
            if (transition.ToUpper().Equals(VoucherStateTransition.SaveTransition.TransitionName.ToUpper()))
            {
                gridviewReceiptVoucherAllocation.GridView.UpdateEdit();
                //Validate input data
                if (!ASPxEdit.ValidateEditorsInContainer(cpnReceiptVoucherEditingForm, "ReceiptVoucherEditingForm", false))
                {
                    return false;
                }
                //Collects input data
                string code = txtCode.Text;
                DateTime issueDate = txtIssueDate.Date;
                string description = txtDescription.Text;
                string address = txtAddress.Text;
                string payer = txtPayer.Text;
                Guid sourceOrgId = cboSourceOrganization.Value != null ? (Guid)cboSourceOrganization.Value : Guid.Empty;
                Guid targetOrgId = Utility.CurrentSession.Instance.AccessingOrganizationId;
                double debit = (double)spinAmount.Number;
                //Guid currencyId = (Guid)cboCurrency.Value;
                Guid currencyId = (Guid)gridlookupCurrency.GetSelectedCurrencyKey();
                double exchangeRate = (double)spinExchangeRate.Number;

                //Insert data to database
                ReceiptVouchesBO receiptVoucherBO = new ReceiptVouchesBO();
                receiptVoucherBO.Update(VoucherId,
                                        code,
                                        issueDate,
                                        description,
                                        address,
                                        payer,
                                        sourceOrgId,
                                        targetOrgId,
                                        debit,
                                        currencyId,
                                        exchangeRate);
            }
            return true;
        }
        public bool ReceiptVoucherLocked_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        private void LinkVoucherWithBill(Session _session, Guid _billId, Vouches voucher)
        {
            ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();

            ObjectTypeCustomField defaultObjectTypeCustomField =
                ObjectTypeCustomField.GetDefault(_session, DefaultObjectTypeCustomFieldEnum.RECEIPT_VOUCHER_SALE_INVOICE);

            ObjectCustomField objectCustomField = voucher.VoucherObjects.First().ObjectId.ObjectCustomFields
                .Where(r => r.ObjectTypeCustomFieldId.Equals(defaultObjectTypeCustomField)).First();

            List<Guid> billId = new List<Guid>();
            billId.Add(_billId);

            objectCustomFieldBO.UpdatePredefinitionData(
                objectCustomField.ObjectCustomFieldId,
                billId,
                PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY);
        }

        private void FillBillDataIntoVoucher(Session _session, Guid _billId)
        {
            //Get bill
            Bill bill = _session.GetObjectByKey<Bill>(_billId);

            //Fill description
            txtDescription.Text = "Thu tiền hàng";

            //Fill voucher amount
            ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
            double amount = 0;
            var genaralJournal = receiptVoucherTransactionBO.GetActuallyCollectedOfBill(_session, bill.BillId);
            if (genaralJournal == null)
            {
                amount = bill.Total;
            }
            else
            {
                double actualPaymentAmount = genaralJournal.Sum(r => r.Debit);
                amount = bill.Total - actualPaymentAmount;
                if (amount <= 0)
                {
                    GUIContext.State = new ReceiptVoucherCanceling(this);
                    throw new Exception(String.Format(
                        "Không thể tạo thêm phiếu thu vì phiếu bán '{0}' đã được thanh toán đủ", bill.Code));
                }
            }
            spinAmount.Number = (decimal)amount;

            //Fill organization information
            if (bill.SourceOrganizationId != null)
            {
                cboSourceOrganization.Value = bill.SourceOrganizationId.OrganizationId;
                cboSourceOrganization.DataBindItems();
                txtAddress.Text = bill.SourceOrganizationId.Address;
            }

            gridviewReceiptVoucherAllocation.AddNewRow();
        }

        private void formlayoutVoucherAmount_DataBind()
        {
            CriteriaOperator criteria = new BinaryOperator("VouchesId!Key", VoucherId);
            dsVoucherAmount.Criteria = criteria.ToString();
            formlayoutVoucherAmount.DataBind();

            ReceiptVouches receiptVouches = GetCurrentReceiptVoucher();
            VouchesAmount vouchesAmount = receiptVouches.VouchesAmounts.FirstOrDefault();
            if (vouchesAmount != null)
            {
                if (vouchesAmount.CurrencyId != null)
                    gridlookupCurrency.SetSelectedCurrencyByKey(vouchesAmount.CurrencyId.CurrencyId);
                else
                    gridlookupCurrency.ResetToDefault();
            }
            else
            {
                gridlookupCurrency.ResetToDefault();
            }
        }

        private void formlayoutReceiptVoucherEditingForm_DataBind()
        {
            CriteriaOperator criteria = new BinaryOperator("VouchesId", VoucherId);
            dsReceiptVoucher.Criteria = criteria.ToString();
            formlayoutReceiptVoucherEditingForm.DataBind();
        }

        protected void cpnReceiptVoucherEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            bool isSuccess = false;
            ReceiptVoucherTransactionBO receiptVoucherTransactionBO;
            try
            {
                switch (command)
                {
                    case "Create":
                        VoucherId = Guid.Empty;
                        //If create voucher from bill
                        if (args.Length > 1)
                        {
                            BillId = Guid.Parse(args[1]);
                        }
                        GUIContext.State = new ReceiptVoucherCreating(this);
                        hiddenField["voucherId"] = VoucherId;
                        number = null;
                        break;
                    case "Edit":
                        if (args.Length < 2)
                        {
                            throw new Exception("Invalid parameters");
                        }
                        VoucherId = Guid.Parse(args[1]);
                        hiddenField["voucherId"] = VoucherId;
                        receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                        if (receiptVoucherTransactionBO.IsVoucherLockedBookingEntry(VoucherId))
                        {
                            GUIContext.State = new ReceiptVoucherLocked(this);
                        }
                        else
                        {
                            GUIContext.State = new ReceiptVoucherEditing(this);
                            UpdateBookingEntryState();
                        }
                        number = null;
                        break;
                    case "ForceRefresh":
                        if (GUIContext.State is ReceiptVoucherCreating)
                        {
                            ClearFormValidation();
                            ReceiptVoucherCreating_UpdateGUI();
                            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
                        }
                        else
                        {
                            receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                            if (receiptVoucherTransactionBO.IsVoucherLockedBookingEntry(VoucherId))
                            {
                                GUIContext.State = new ReceiptVoucherLocked(this);
                            }
                            else
                            {
                                UpdateBookingEntryState();
                            }
                        }
                        break;
                    case "Cancel":
                        GUIContext.Request(command, this);
                        number = null;
                        break;
                    default:
                        GUIContext.Request(command, this);
                        UpdateBookingEntryState();
                        number = null;
                        break;
                }
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (command != null)
                {
                    cpnReceiptVoucherEditingForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            command, isSuccess.ToString().ToLower()));
                }
            }
        }

        private void gridviewVoucherAllocation_DataBind()
        {
            //gridviewVoucherAllocation.VoucherId = VoucherId;
            //gridviewVoucherAllocation.DataBind();
        }

        protected void cboSourceOrganization_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            Organization obj = session.GetObjectByKey<Organization>(e.Value);

            if (obj != null)
            {
                cboSourceOrganization.DataSource = new Organization[] { obj };
                cboSourceOrganization.DataBindItems();
            }
        }

        protected void cboSourceOrganization_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<Organization> collection = new XPCollection<Organization>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                session.FindObject<TradingCategory>(new BinaryOperator("Code", "CUSTOMER"));
            //Get SUPPLIER trading type
            TradingCategory supplierTradingCategory =
                session.FindObject<TradingCategory>(new BinaryOperator("Code", "SUPPLIER"));

            CriteriaOperator criteria = CriteriaOperator.And(
                //row status is active
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                CriteriaOperator.Or(
                //find code contains the filter
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                //find name contains the filter
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                ),
                //find customer and supplier
                CriteriaOperator.Or(
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", customerTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    ),
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    )
                )
            );

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboSourceOrganization.DataSource = collection;
            cboSourceOrganization.DataBindItems();

        }

        private void txtCode_SetValidation(ValidationEventArgs e, bool isValid, string errorText)
        {
            e.IsValid = isValid;
            e.ErrorText = errorText;
        }

        private void txtCode_SetExistValidation(ValidationEventArgs e, bool isCodeExist)
        {
            if (isCodeExist)
            {
                txtCode_SetValidation(e, false, String.Format("Mã phiếu thu '{0}' đã được sử dụng", e.Value));
            }
            else
            {
                txtCode_SetValidation(e, true, String.Empty);
            }
        }

        private ReceiptVouches GetCurrentReceiptVoucher()
        {
            if (VoucherId.Equals(Guid.Empty))
            {
                throw new Exception("Invalid key");
            }
            return session.GetObjectByKey<ReceiptVouches>(VoucherId);
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String vouchesCode = e.Value.ToString().Trim();
            //New mode
            if (GUIContext.State is ReceiptVoucherCreating)
            {
                bool isExist = Util.isExistXpoObject<ReceiptVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                txtCode_SetExistValidation(e, isExist);
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!vouchesCode.Equals(GetCurrentReceiptVoucher().Code))
                {
                    bool isExist = Util.isExistXpoObject<PaymentVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                    txtCode_SetExistValidation(e, isExist);
                }
            }
        }

        private void lbl_text_NumberToString()
        {
            NAS.BO.Accounting.CurrencyBO BO = new NAS.BO.Accounting.CurrencyBO();

            string c = "";
            if (gridlookupCurrency.Value != null)
                c = gridlookupCurrency.Value.ToString();

            string sotien = "0", tygia = "0";
            if (spinAmount.Number != null)
                if (double.Parse(spinAmount.Number.ToString()) > 0)
                    sotien = spinAmount.Number.ToString();

            if (spinExchangeRate.Number != null)
                if (double.Parse(spinExchangeRate.Number.ToString()) > 0)
                    tygia = spinExchangeRate.Number.ToString();
                else if (number != null)

                    tygia = number;

                

                if (c != null && !c.Equals(""))
                    lbl_text_Number.Text = BO.NumberToString(session, double.Parse(sotien), c.ToString(), double.Parse(tygia)).ToString();


        }

        private string number
        {
            get { return (string)Session["number"]; }
            set { Session["number"] = value; }
        }

        protected void CPNumberString_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            lbl_text_NumberToString();
        }

        protected void panelVoucherAmount_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter.Equals("CurrencyChanged"))
            {
                CurrencyChanged();
            }
        }

        private void CurrencyChanged()
        {
            //Get selected currency
            Currency currency = gridlookupCurrency.GetSelectedCurrency(session);
            UpdateGUIByCurrency(currency);
            SetAmountByCurrency(currency);
        }

        private void UpdateGUIByCurrency(Currency currency)
        {
            //Set visible
            SetVoucherAmountVisibleByCurrency(currency);
        }

        private void SetVoucherAmountVisibleByCurrency(Currency currency)
        {
            //Get default currency
            CurrencyBO currencyBO = new CurrencyBO();
            CurrencyType defaultCurrencyType = currencyBO.GetDefaultCurrencyType(session);
            //If selected currency has type is default type
            if (currency == null || defaultCurrencyType.Equals(currency.CurrencyTypeId))
            {
                LayoutItemExchangeRate.ClientVisible = false;
                spinExchangeRate.ValidationSettings.RequiredField.IsRequired = false;
                LayoutItemConvertedAmount.ClientVisible = false;
            }
            else
            {
                LayoutItemExchangeRate.ClientVisible = true;
                spinExchangeRate.ValidationSettings.RequiredField.IsRequired = true;
                LayoutItemConvertedAmount.ClientVisible = true;
            }
        }

        private void SetAmountByCurrency(Currency currency)
        {
            if (currency == null) return;
            //Get default currency
            CurrencyBO currencyBO = new CurrencyBO();
            CurrencyType defaultCurrencyType = currencyBO.GetDefaultCurrencyType(session);
            //If selected currency has type is default type
            if (defaultCurrencyType.Equals(currency.CurrencyTypeId))
            {
                spinExchangeRate.Number = 1;
            }
            else
            {
                ExchangeRateBO exchangeRateBO = new ExchangeRateBO();
                ExchangeRate exchangeRate = exchangeRateBO.GetLatest(session, currency.CurrencyId);
                if (exchangeRate != null)
                {
                    spinExchangeRate.Number = (decimal)exchangeRate.Rate;
                    if (spinAmount.Number >= 0 && spinExchangeRate.Number >= 0)
                    {
                        spinConvertedAmount.Number = spinAmount.Number * spinExchangeRate.Number;
                        number = spinExchangeRate.Number.ToString();
                    }
                    else
                        spinConvertedAmount.Text = null;
                }
                else
                {
                    spinExchangeRate.Text = null;
                    spinConvertedAmount.Text = null;
                }
            }
        }

        private LayoutItem LayoutItemExchangeRate
        {
            get
            {
                return (LayoutItem)formlayoutVoucherAmount.FindItemOrGroupByName("ExchangeRate");
            }
        }

        private LayoutItem LayoutItemConvertedAmount
        {
            get
            {
                return (LayoutItem)formlayoutVoucherAmount.FindItemOrGroupByName("ConvertedAmount");
            }
        }


    }
}