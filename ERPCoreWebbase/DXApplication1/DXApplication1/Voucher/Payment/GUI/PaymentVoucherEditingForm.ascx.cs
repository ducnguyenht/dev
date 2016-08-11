using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
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
using WebModule.Voucher.Payment.State;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Invoice;
using NAS.BO.System.ArtifactCode;
using DevExpress.Web.ASPxFormLayout;
using NAS.DAL.Accounting.Currency;
using NAS.BO.Accounting;
using NAS.BO.Accounting.NSCurrency;

namespace WebModule.Voucher.Payment.GUI
{
    public partial class PaymentVoucherEditingForm : System.Web.UI.UserControl
    {
        private Guid VoucherId
        {
            get
            {
                return (Guid)Session["PaymentVoucherEditingForm_VoucherId_" + ViewStateControlId];
            }
            set
            {
                Session["PaymentVoucherEditingForm_VoucherId_" + ViewStateControlId] = value;
            }
        }
        private Guid BillId
        {
            get
            {
                return (Guid)Session["PaymentVoucherEditingForm_BillId_" + ViewStateControlId];
            }
            set
            {
                Session["PaymentVoucherEditingForm_BillId_" + ViewStateControlId] = value;
            }
        }
        private Session session;
        private VoucherBO voucherBO;
        private PaymentVouchesBO paymentVouchesBO;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsReceiptVoucher.Session = session;
            dsForeignCurrency.Session = session;
            dsVoucherAmount.Session = session;

            voucherBO = new VoucherBO();
            paymentVouchesBO = new PaymentVouchesBO();

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
                .SetGridViewVoucherAllocationStrategy(GridViewVoucherAllocationStrategyEnum.PAYMENT_VOUCHER);

        }

        protected void gridviewReceiptVoucherAllocation_InitNewRowWithDefaultDataEvent(Controls.GridViewVoucherAllocation.GridViewVoucherAllocation.TransactionInitRowData data)
        {
            if (VoucherId != null && !VoucherId.Equals(Guid.Empty))
            {
                data.IssuedDate = txtIssueDate.Date;
                data.Description = txtDescription.Text;

                PaymentVouches voucher = session.GetObjectByKey<PaymentVouches>(VoucherId);
                int sequence = 0;
                sequence =
                    voucher.PaymentVouchesTransactions.Count(r => r.RowStatus == Constant.ROWSTATUS_ACTIVE) + 1;
                data.Code = String.Format("{0}_{1}", txtCode.Text, sequence);

                double amount = 0;
                if (voucher.SumOfCredit != 0)
                {
                    amount = voucher.SumOfCredit - voucher.PaymentVouchesTransactions.Sum(r => r.Amount);
                    if (amount < 0)
                        amount = 0;
                }
                else
                {
                    amount = (double)spinAmount.Number - voucher.PaymentVouchesTransactions.Sum(r => r.Amount);
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
                .SetVoucherBookingEntriesFormStrategy(VoucherBookingEntriesFormStrategySimpleEnum.PAYMENT_VOUCHER);
        }

        private void UpdateBookingEntryState()
        {
            if (GUIContext.State is PaymentVoucherEditing)
            {
                if (voucherBO.CanBookingEntry(VoucherId))
                {
                    GUIContext.State = new PaymentVoucherCanBookingEntry(this);
                }
                else
                {
                    GUIContext.State = new PaymentVoucherCannotBookingEntry(this);
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
            gridlookupCurrency.IsValid = true;
            //cboCurrency.IsValid = true;
            spinExchangeRate.IsValid = true;
        }

        private void LinkVoucherWithBill(Session _session, Guid _billId, Vouches voucher)
        {
            ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();

            ObjectTypeCustomField defaultObjectTypeCustomField =
                ObjectTypeCustomField.GetDefault(_session, DefaultObjectTypeCustomFieldEnum.PAYMENT_VOUCHER_PURCHASE_INVOICE);

            ObjectCustomField objectCustomField = voucher.VoucherObjects.First().ObjectId.ObjectCustomFields
                .Where(r => r.ObjectTypeCustomFieldId.Equals(defaultObjectTypeCustomField)).First();

            List<Guid> billId = new List<Guid>();
            billId.Add(_billId);

            objectCustomFieldBO.UpdatePredefinitionData(
                objectCustomField.ObjectCustomFieldId,
                billId,
                PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY);
        }

        private void FillBillDataIntoVoucher(Session _session, Guid _billId)
        {
            //Get bill
            Bill bill = _session.GetObjectByKey<Bill>(_billId);

            //Fill description
            txtDescription.Text = "Trả tiền hàng";

            //Fill voucher amount
            PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();
            double amount = 0;
            var genaralJournal = paymentVoucherTransactionBO.GetActuallyCollectedOfBill(_session, bill.BillId);
            if (genaralJournal == null)
            {
                amount = bill.Total;
            }
            else
            {
                double actualPaymentAmount = genaralJournal.Sum(r => r.Credit);
                amount = bill.Total - actualPaymentAmount;
                if (amount <= 0)
                {
                    GUIContext.State = new PaymentVoucherCanceling(this);
                    throw new Exception(String.Format(
                        "Không thể tạo thêm phiếu chi vì phiếu mua '{0}' đã được thanh toán đủ", bill.Code));
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
        public bool PaymentVoucherCanBookingEntry_UpdateGUI()
        {
            Button_DisplatBookingEntries.Enabled = true;
            return true;
        }
        public bool PaymentVoucherCannotBookingEntry_UpdateGUI()
        {
            Button_DisplatBookingEntries.Enabled = false;
            return true;
        }
        public bool PaymentVoucherCanceling_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = false;
            return true;
        }
        public bool PaymentVoucherCreating_UpdateGUI()
        {
            //Set default date for issue date
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = "Thông tin phiếu chi - Thêm mới";

            Button_DisplatBookingEntries.Enabled = false;

            InitGridViewVoucherAllocation();
            return true;
        }
        public bool PaymentVoucherEditing_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = String.Format("Thông tin phiếu chi - {0}",
                GetCurrentPaymentVoucher().Code);

            InitGridViewVoucherAllocation();

            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
            return true;
        }
        public bool PaymentVoucherLocked_UpdateGUI()
        {
            popupReceiptVoucherEditingForm.ShowOnPageLoad = true;
            popupReceiptVoucherEditingForm.HeaderText = String.Format("Thông tin phiếu chi - {0}",
                GetCurrentPaymentVoucher().Code);

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
        public bool PaymentVoucherCanceling_CRUD()
        {
            return true;
        }
        public bool PaymentVoucherCreating_CRUD()
        {
            ClearForm();
            //Create temp voucher
            PaymentVouches paymentVouches = paymentVouchesBO.CreateNewObject(session);
            //Update VoucherId
            VoucherId = paymentVouches.VouchesId;
            txtIssueDate.Date = DateTime.Now;

            //Get default code
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.VOUCHER_PAYMENT);

            //Link to the bill
            if (BillId != Guid.Empty)
            {
                //Link bill to voucher
                LinkVoucherWithBill(session, BillId, paymentVouches);
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
        public bool PaymentVoucherEditing_CRUD()
        {
            ClearFormValidation();
            //Bind data to voucher information form
            formlayoutReceiptVoucherEditingForm_DataBind();
            //Bind data to voucher ammount form
            formlayoutVoucherAmount_DataBind();
            //Bind data to VoucherAllocation gridview
            gridviewVoucherAllocation_DataBind();

            PaymentVouches paymentVoucher = GetCurrentPaymentVoucher();
            txtCode.Text = paymentVoucher.Code;
            //spinAmount.Number = (decimal)receiptVoucher.SumOfDebit;
            return true;
        }
        public bool PaymentVoucherLocked_CRUD()
        {
            ClearFormValidation();
            //Bind data to voucher information form
            formlayoutReceiptVoucherEditingForm_DataBind();
            //Bind data to voucher ammount form
            formlayoutVoucherAmount_DataBind();
            //Bind data to VoucherAllocation gridview
            gridviewVoucherAllocation_DataBind();

            PaymentVouches paymentVoucher = GetCurrentPaymentVoucher();
            txtCode.Text = paymentVoucher.Code;
            //spinAmount.Number = (decimal)receiptVoucher.SumOfDebit;
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool PaymentVoucherCanceling_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool PaymentVoucherCreating_PreTransitionCRUD(string transition)
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
                string payee = txtPayer.Text;
                Guid sourceOrgId = Utility.CurrentSession.Instance.AccessingOrganizationId;
                Guid targetOrgId = cboSourceOrganization.Value != null ? (Guid)cboSourceOrganization.Value : Guid.Empty;
                double credit = (double)spinAmount.Number;
                //Guid currencyId = (Guid)cboCurrency.Value;
                Guid currencyId = (Guid)gridlookupCurrency.GetSelectedCurrencyKey();
                double exchangeRate = (double)spinExchangeRate.Number;

                //Insert data to database
                PaymentVouchesBO paymentVoucherBO = new PaymentVouchesBO();
                paymentVoucherBO.Insert(VoucherId,
                                        code,
                                        issueDate,
                                        description,
                                        address,
                                        payee,
                                        sourceOrgId,
                                        targetOrgId,
                                        credit,
                                        currencyId,
                                        exchangeRate);
            }
            else if (transition.ToUpper().Equals(VoucherStateTransition.CancelTransition.TransitionName.ToUpper()))
            {
                voucherBO.DeleteTempObject(VoucherId);
            }
            return true;
        }
        public bool PaymentVoucherEditing_PreTransitionCRUD(string transition)
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
                string payee = txtPayer.Text;
                Guid sourceOrgId = cboSourceOrganization.Value != null ? (Guid)cboSourceOrganization.Value : Guid.Empty;
                Guid targetOrgId = Utility.CurrentSession.Instance.AccessingOrganizationId;
                double credit = (double)spinAmount.Number;
                //Guid currencyId = (Guid)cboCurrency.Value;
                Guid currencyId = (Guid)gridlookupCurrency.GetSelectedCurrencyKey();
                double exchangeRate = (double)spinExchangeRate.Number;

                //Insert data to database
                PaymentVouchesBO paymentVoucherBO = new PaymentVouchesBO();
                paymentVoucherBO.Update(VoucherId,
                                        code,
                                        issueDate,
                                        description,
                                        address,
                                        payee,
                                        sourceOrgId,
                                        targetOrgId,
                                        credit,
                                        currencyId,
                                        exchangeRate);
            }
            return true;
        }
        public bool PaymentVoucherLocked_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

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

        private void formlayoutVoucherAmount_DataBind()
        {
            CriteriaOperator criteria = new BinaryOperator("VouchesId!Key", VoucherId);
            dsVoucherAmount.Criteria = criteria.ToString();
            formlayoutVoucherAmount.DataBind();

            PaymentVouches paymentVoucher = GetCurrentPaymentVoucher();
            VouchesAmount vouchesAmount = paymentVoucher.VouchesAmounts.FirstOrDefault();
            if (vouchesAmount != null)
            {
                if (vouchesAmount.CurrencyId != null)
                    gridlookupCurrency.SetSelectedCurrencyByKey(vouchesAmount.CurrencyId.CurrencyId);
                else
                    gridlookupCurrency.ResetToDefault();
                //UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
            }
            else
            {
                gridlookupCurrency.ResetToDefault();
                //UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
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
            PaymentVoucherTransactionBO paymentVoucherTransactionBO;
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
                        else
                        {
                            BillId = Guid.Empty;
                        }
                        GUIContext.State = new PaymentVoucherCreating(this);
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
                        paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();
                        if (paymentVoucherTransactionBO.IsVoucherLockedBookingEntry(VoucherId))
                        {
                            GUIContext.State = new PaymentVoucherLocked(this);
                        }
                        else
                        {
                            GUIContext.State = new PaymentVoucherEditing(this);
                            UpdateBookingEntryState();
                        }
                        number = null;
                        break;
                    case "ForceRefresh":
                        if (GUIContext.State is PaymentVoucherCreating)
                        {
                            ClearFormValidation();
                            PaymentVoucherCreating_UpdateGUI();
                            UpdateGUIByCurrency(gridlookupCurrency.GetSelectedCurrency(session));
                        }
                        else
                        {
                            paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();
                            //if (GUIContext.State is ReceiptVoucherCreating)
                            //{
                            //    GUIContext.State = new ReceiptVoucherCreating(this);
                            //}
                            //else
                            //{
                            if (paymentVoucherTransactionBO.IsVoucherLockedBookingEntry(VoucherId))
                            {
                                GUIContext.State = new PaymentVoucherLocked(this);
                            }
                            else
                            {
                                UpdateBookingEntryState();
                            }
                        }
                        //}
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

            ////Get CUSTOMER trading type
            //TradingCategory customerTradingCategory =
            //    session.FindObject<TradingCategory>(new BinaryOperator("Code", "CUSTOMER"));
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
                //CriteriaOperator.And(
                //new BinaryOperator("TradingCategoryId.TradingCategoryId", customerTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                //)
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
                txtCode_SetValidation(e, false, String.Format("Mã phiếu chi '{0}' đã được sử dụng", e.Value));
            }
            else
            {
                txtCode_SetValidation(e, true, String.Empty);
            }
        }

        private PaymentVouches GetCurrentPaymentVoucher()
        {
            if (VoucherId.Equals(Guid.Empty))
            {
                throw new Exception("Invalid key");
            }
            return session.GetObjectByKey<PaymentVouches>(VoucherId);
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String vouchesCode = e.Value.ToString().Trim();
            //New mode
            if (GUIContext.State is PaymentVoucherCreating)
            {
                bool isExist = Util.isExistXpoObject<PaymentVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                txtCode_SetExistValidation(e, isExist);
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!vouchesCode.Equals(GetCurrentPaymentVoucher().Code))
                {
                    bool isExist = Util.isExistXpoObject<PaymentVouches>("Code", vouchesCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT,
                            Constant.ROWSTATUS_INACTIVE, Constant.ROWSTATUS_BOOKED_ENTRY);
                    txtCode_SetExistValidation(e, isExist);
                }
            }
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
    }
}