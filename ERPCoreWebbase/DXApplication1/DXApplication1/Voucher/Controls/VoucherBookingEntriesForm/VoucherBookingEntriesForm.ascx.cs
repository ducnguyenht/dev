using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.JournalAllocation;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting.Journal;
using WebModule.Voucher.Controls.VoucherBookingEntriesForm.Strategy;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxEditors;
using WebModule.Voucher.Controls.VoucherBookingEntriesForm.State;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm
{
    public partial class VoucherBookingEntriesForm : System.Web.UI.UserControl
    {

        private VoucherBookingEntriesFormStrategy Strategy
        {
            get;
            set;
        }

        public void SetVoucherBookingEntriesFormStrategy(
            VoucherBookingEntriesFormStrategySimpleEnum strategy)
        {
            Strategy =
                VoucherBookingEntriesFormStrategySimpleFactory
                    .CreateVoucherBookingEntriesFormStrategy(strategy);
            InitDataSource();
        }

        private void InitDataSource()
        {
            if (Strategy != null)
            {
                dsVoucherTransaction.TypeName = Strategy.GetConcreteVoucherTransactionType().FullName;
                CriteriaOperator criteria = Strategy.GetVoucherTransactionCriteria(VoucherId);
                dsVoucherTransaction.Criteria = criteria.ToString();
                gridviewVoucherBookingEntries.DataBind();
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsVoucherTransaction.Session = session;
            dsGeneralJournal.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                VoucherId = Guid.Empty;
                TransactionId = Guid.Empty;
                GUIContext = new Context();
            }
            InitDataSource();
            TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        public Guid VoucherId
        {
            get
            {
                return (Guid)Session["VoucherId_" + ViewStateControlId];
            }
            set
            {
                Session["VoucherId_" + ViewStateControlId] = value;
            }
        }

        public Guid TransactionId
        {
            get
            {
                return (Guid)Session["TransactionId_" + ViewStateControlId];
            }
            set
            {
                Session["TransactionId_" + ViewStateControlId] = value;
            }
        }

        public bool IsRaiseDataUpdated
        {
            get
            {
                if (Session["IsRaiseDataUpdated_" + ViewStateControlId] == null)
                    return false;
                return (bool)Session["IsRaiseDataUpdated_" + ViewStateControlId];
            }
            set
            {
                Session["IsRaiseDataUpdated_" + ViewStateControlId] = value;
            }
        }

        private Guid GetCMSObjectIdOfTransaction(Guid transactionId)
        {
            if (transactionId != null && !transactionId.Equals(Guid.Empty))
            {
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = null;
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    Transaction transaction = uow.GetObjectByKey<Transaction>(transactionId);
                    TransactionObject transactionObject =
                        transaction.TransactionObjects.FirstOrDefault();
                    if (transactionObject == null)
                    {
                        ObjectBO objectBO = new ObjectBO();
                        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum objectType = ObjectTypeEnum.VOUCHER_PAYMENT; ;
                        if (Strategy.GetConcreteVoucherTransactionType().Equals(typeof(NAS.DAL.Vouches.ReceiptVouches)))
                        {
                            objectType = NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_RECEIPT;
                        }
                        else if (Strategy.GetConcreteVoucherTransactionType().Equals(typeof(NAS.DAL.Vouches.PaymentVouches)))
                        {
                            objectType = NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT;
                        }
                        else
                        {
                            throw new Exception("Create object the specific type is out of scope");
                        }
                        cmsObject = objectBO.CreateCMSObject(uow, objectType);
                        TransactionObject newTransactionObject = new TransactionObject(uow)
                        {
                            ObjectId = cmsObject,
                            TransactionId = transaction
                        };
                        uow.CommitChanges();
                    }
                    else
                    {
                        cmsObject = transactionObject.ObjectId;
                    }
                    return cmsObject.ObjectId;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        private Guid GetCMSObjectIdOfGeneralJounal(Guid generalJounalId)
        {
            if (generalJounalId != null && !generalJounalId.Equals(Guid.Empty))
            {
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = null;
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    GeneralJournal generalJournal = uow.GetObjectByKey<GeneralJournal>(generalJounalId);
                    GeneralJournalObject generalJournalObject =
                        generalJournal.GeneralJournalObjects.FirstOrDefault();
                    if (generalJournalObject == null)
                    {
                        ObjectBO objectBO = new ObjectBO();
                        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum objectType = ObjectTypeEnum.VOUCHER_PAYMENT; ;
                        if (Strategy.GetConcreteVoucherTransactionType().Equals(typeof(NAS.DAL.Accounting.Journal.ReceiptVouchesTransaction)))
                        {
                            objectType = NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_RECEIPT;
                        }
                        else if (Strategy.GetConcreteVoucherTransactionType().Equals(typeof(NAS.DAL.Accounting.Journal.PaymentVouchesTransaction)))
                        {
                            objectType = NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT;
                        }
                        else
                        {
                            throw new Exception("Create object the specific type is out of scope");
                        }
                        cmsObject = objectBO.CreateCMSObject(uow, objectType);
                        GeneralJournalObject newTransactionObject = new GeneralJournalObject(uow)
                        {
                            ObjectId = cmsObject,
                            GeneralJournalId = generalJournal
                        };
                        uow.CommitChanges();
                    }
                    else
                    {
                        cmsObject = generalJournalObject.ObjectId;
                    }
                    return cmsObject.ObjectId;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        private void TransactionCustomFieldDataGridView_BindDataUpdatedEvent()
        {
            if (!IsRaiseDataUpdated)
            {
                transactionCustomFieldDataGridView.DataUpdated -=
                    new CustomFieldControlDataUpdatedEventHandler(transactionCustomFieldDataGridView_DataUpdated);
            }
            else
            {
                transactionCustomFieldDataGridView.DataUpdated +=
                    new CustomFieldControlDataUpdatedEventHandler(transactionCustomFieldDataGridView_DataUpdated);
            }
        }

        protected void cpnTransactionAllocationObjects_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];

            #region Transaction allocation
            if (command.Equals("AllocateTransaction"))
            {
                int visibleIndex;
                //Get CMS object of transaction
                if (args.Length > 1)
                {
                    visibleIndex = int.Parse(args[1]);
                }
                else
                {
                    throw new Exception("Invalid parameter");
                }
                TransactionId = (Guid)gridviewVoucherBookingEntries.GetRowValues(visibleIndex, "TransactionId");
                transactionCustomFieldDataGridView.CMSObjectId = GetCMSObjectIdOfTransaction(TransactionId);
                transactionCustomFieldDataGridView.DataBind();
                IsRaiseDataUpdated = true;
                TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
            }
            #endregion

            #region GeneralJournal allocation
            else if (command.Equals("AllocateGeneralJournal"))
            {
                Guid generalJournalId = Guid.Empty;
                //Get CMS object of transaction
                if (args.Length > 1)
                {
                    generalJournalId = Guid.Parse(args[1]);
                }
                else
                {
                    throw new Exception("Invalid parameter");
                }
                transactionCustomFieldDataGridView.CMSObjectId = GetCMSObjectIdOfGeneralJounal(generalJournalId);
                transactionCustomFieldDataGridView.DataBind();
                IsRaiseDataUpdated = false;
                TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
            }
            #endregion

        }

        private void UpdateState()
        {
            if (!VoucherId.Equals(Guid.Empty))
            {
                if (Strategy.VoucherTransactionBO.IsVoucherLockedBookingEntry(VoucherId))
                {
                    GUIContext.State = new VoucherLockedBookingdEntries(this);
                }
                else if (Strategy.VoucherTransactionBO.CanBookEntries(VoucherId))
                {
                    GUIContext.State = new VoucherCanBookingEntries(this);
                }
                else
                {
                    GUIContext.State = new VoucherCanNotBookingEntries(this);
                }
            }
        }

        protected void cpnVoucherBookingEntriesForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            bool isSuccess = false;
            try
            {
                switch (command)
                {
                    case "Edit":
                        if (args.Length < 2)
                        {
                            throw new Exception("Invalid parameters");
                        }
                        VoucherId = Guid.Parse(args[1]);
                        UpdateState();
                        break;
                    case "ForceRefresh":
                        UpdateState();
                        break;
                    case "Cancel":
                        GUIContext.Request(command, this);
                        break;
                    case "Book":
                        GUIContext.Request(command, this);
                        UpdateState();
                        break;
                    default:
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
                    cpnVoucherBookingEntriesForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            command, isSuccess.ToString().ToLower()));
                }
            }
        }

        protected void transactionCustomFieldDataGridView_DataUpdated(object sender,
            ERPSystem.CustomField.GUI.Control.CustomFieldControlEventArgs args)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                //Get all journal of transaction
                Transaction transaction = uow.GetObjectByKey<Transaction>(TransactionId);
                var cmsObjects =
                    transaction.GeneralJournals
                        .Where(r => r.RowStatus >= 0)
                        .Select(r => r.GeneralJournalObjects.FirstOrDefault())
                        .Select(r => r.ObjectId);

                ObjectTypeCustomField objectTypeCustomField =
                    uow.GetObjectByKey<ObjectTypeCustomField>(args.ObjectTypeCustomFieldId);

                if (cmsObjects != null)
                {
                    ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                    foreach (var cmsObject in cmsObjects)
                    {
                        ObjectCustomField objectCustomField =
                            cmsObject.ObjectCustomFields
                                .Where(r => r.ObjectTypeCustomFieldId == objectTypeCustomField)
                                .FirstOrDefault();
                        if (objectCustomField != null)
                        {
                            //Copy new data to all jounal of the transaction
                            switch (args.CustomFieldCategory)
                            {
                                case CustomFieldControlEventArgs.CustomFieldCategoryEnum.BASIC:
                                    objectCustomFieldBO.UpdateBasicData(
                                        objectCustomField.ObjectCustomFieldId,
                                        args.NewBasicDataValue,
                                        args.BasicCustomFieldType);
                                    break;
                                case CustomFieldControlEventArgs.CustomFieldCategoryEnum.LIST:
                                    objectCustomFieldBO.UpdateUserDefinedListData(
                                        objectCustomField.ObjectCustomFieldId,
                                        args.NewCustomFieldDataIds);
                                    break;
                                case CustomFieldControlEventArgs.CustomFieldCategoryEnum.BUILT_IN:
                                    NASCustomFieldPredefinitionData temp = args.NewBuiltInData.FirstOrDefault();
                                    if (temp != null)
                                    {
                                        PredefinitionCustomFieldTypeEnum predefinitionType =
                                            (PredefinitionCustomFieldTypeEnum)Enum
                                                .Parse(typeof(PredefinitionCustomFieldTypeEnum), temp.PredefinitionType);
                                        objectCustomFieldBO.UpdatePredefinitionData(
                                            objectCustomField.ObjectCustomFieldId,
                                            args.NewBuiltInData.Select(r => r.RefId).ToList(),
                                            predefinitionType);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
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

        private NAS.DAL.Vouches.Vouches GetCurrentVoucher(Session _session)
        {
            if(VoucherId.Equals(Guid.Empty))
                return null;
            return _session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VoucherId);
        }

        #region UpdateGUI
        public bool VoucherBookingEntriesCanceling_UpdateGUI()
        {
            popupVoucherBookingEntriesForm.ShowOnPageLoad = false;
            return true;
        }

        private ASPxButton Button_BookEntries
        {
            get
            {
                return (ASPxButton)popupVoucherBookingEntriesForm.FindControl("btnBookEntries");
            }
        }

        public bool VoucherCanBookingEntries_UpdateGUI()
        {
            popupVoucherBookingEntriesForm.ShowOnPageLoad = true;
            gridviewVoucherBookingEntries.Enabled = true;

            Button_BookEntries.Enabled = true;
            //Set popup header text
            NAS.DAL.Vouches.Vouches voucher = GetCurrentVoucher(session);
            if (voucher is NAS.DAL.Vouches.ReceiptVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu thu - {0}", voucher.Code);
            }
            else if (voucher is NAS.DAL.Vouches.PaymentVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu chi - {0}", voucher.Code);
            }

            return true;
        }
        public bool VoucherCanNotBookingEntries_UpdateGUI()
        {
            popupVoucherBookingEntriesForm.ShowOnPageLoad = true;
            gridviewVoucherBookingEntries.Enabled = true;

            Button_BookEntries.Enabled = true;
            Button_BookEntries.ClientSideEvents.Click = "function(s, e) { alert('Không thể ghi sổ được vì số tiền chưa được cân đối. Vui lòng kiểm tra lại.'); }";
            //Set popup header text
            NAS.DAL.Vouches.Vouches voucher = GetCurrentVoucher(session);
            if (voucher is NAS.DAL.Vouches.ReceiptVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu thu - {0}", voucher.Code);
            }
            else if (voucher is NAS.DAL.Vouches.PaymentVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu chi - {0}", voucher.Code);
            }
            return true;
        }
        public bool VoucherLockedBookingdEntries_UpdateGUI()
        {
            popupVoucherBookingEntriesForm.ShowOnPageLoad = true;
            gridviewVoucherBookingEntries.Enabled = true;

            Button_BookEntries.Visible = false;

            //Set popup header text
            NAS.DAL.Vouches.Vouches voucher = GetCurrentVoucher(session);
            if (voucher is NAS.DAL.Vouches.ReceiptVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu thu - {0}", voucher.Code);
            }
            else if (voucher is NAS.DAL.Vouches.PaymentVouches)
            {
                popupVoucherBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu chi - {0}", voucher.Code);
            }
            return true;
        }
        #endregion

        #region CRUD
        public bool VoucherBookingEntriesCanceling_CRUD()
        {
            return true;
        }
        public bool VoucherCanBookingEntries_CRUD()
        {
            InitDataSource();
            return true;
        }
        public bool VoucherCanNotBookingEntries_CRUD()
        {
            InitDataSource();
            return true;
        }
        public bool VoucherLockedBookingdEntries_CRUD()
        {
            InitDataSource();
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool VoucherBookingEntriesCanceling_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool VoucherCanBookingEntries_PreTransitionCRUD(string transition)
        {
            if (transition.ToUpper()
                .Equals(VoucherBookingEntriesFormStateConstant.TRANSITION_BOOK_ENTRIES))
            {
                return Strategy.VoucherTransactionBO.BookEntries(VoucherId);
            }
            return true;
        }
        public bool VoucherCanNotBookingEntries_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool VoucherLockedBookingdEntries_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        protected void gridviewGeneralJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
            dsGeneralJournal.Criteria = CriteriaOperator.And(
                    new BinaryOperator("TransactionId!Key", transactionId),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_TEMP, BinaryOperatorType.GreaterOrEqual)
                ).ToString();
        }

        protected void gridviewGeneralJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            try
            {
                GeneralJournalBO generalJournalBO = new GeneralJournalBO();
                Guid generalJournalId = (Guid)e.Keys["GeneralJournalId"];
                Strategy.DeleteGeneralJournal(generalJournalId);
                grid.JSProperties["cpEvent"] = "GeneralJournalChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void gridviewGeneralJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                GeneralJournalBO generalJournalBO = new GeneralJournalBO();
                ObjectBO objectBO = new ObjectBO();

                double amount = 0;
                string description;
                Side side;

                Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
                Guid accountId = (Guid)e.NewValues["AccountId!Key"];
                description = (string)e.NewValues["Description"];

                if (e.NewValues["Debit"] != null && (double)e.NewValues["Debit"] > 0)
                {
                    amount = (double)e.NewValues["Debit"];
                    side = Side.DEBIT;
                }
                else if (e.NewValues["Credit"] != null && (double)e.NewValues["Credit"] > 0)
                {
                    amount = (double)e.NewValues["Credit"];
                    side = Side.CREDIT;
                }
                else
                {
                    throw new Exception("Invaild parameter");
                }

                GeneralJournal generalJournal = Strategy.CreateGeneralJournal(
                    uow,
                    transactionId,
                    accountId,
                    side,
                    amount,
                    description,
                    JounalTypeFlag.ACTUAL);

                uow.FlushChanges();

                //Copy readonly data from transaction to journal
                //Get transaction object
                NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject
                    = uow.GetObjectByKey<Transaction>(transactionId).TransactionObjects.First().ObjectId;
                //Get general journal object
                NAS.DAL.CMS.ObjectDocument.Object generalJournalCMSObject = 
                    generalJournal.GeneralJournalObjects.First().ObjectId;

                objectBO.CopyReadOnlyCustomFieldData(
                    transactionCMSObject.ObjectId,
                    generalJournalCMSObject.ObjectId);

                uow.CommitChanges();

                grid.JSProperties["cpEvent"] = "GeneralJournalChanged";


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grid.CancelEdit();
                if (uow != null) uow.Dispose();
            }
        }

        protected void gridviewGeneralJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                double amount = 0;
                string description;
                Side side;

                Guid generalJournalId = (Guid)e.Keys["GeneralJournalId"];
                Guid accountId = (Guid)e.NewValues["AccountId!Key"];
                description = (string)e.NewValues["Description"];

                if (e.NewValues["Debit"] != null && (double)e.NewValues["Debit"] > 0)
                {
                    amount = (double)e.NewValues["Debit"];
                    side = Side.DEBIT;
                }
                else if (e.NewValues["Credit"] != null && (double)e.NewValues["Credit"] > 0)
                {
                    amount = (double)e.NewValues["Credit"];
                    side = Side.CREDIT;
                }
                else
                {
                    throw new Exception("Invaild parameter");
                }

                Strategy.UpdateGeneralJournal(
                    uow,
                    generalJournalId,
                    accountId,
                    side,
                    amount,
                    description);

                uow.CommitChanges();

                grid.JSProperties["cpEvent"] = "GeneralJournalChanged";

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                grid.CancelEdit();
                if (uow != null) uow.Dispose();
            }
        }

        protected void gridviewGeneralJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AccountId!Key")
            {
                AccountingBO accountingBO = new AccountingBO();
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.DataSource = accountingBO.getLeafAccounts(session);
                combo.DataBindItems();
            }
        }

        protected void gridviewGeneralJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("AccountId!Key"))
            {
                Guid accountId = (Guid)e.Value;
                NAS.DAL.Accounting.AccountChart.Account account =
                    session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(accountId);
                e.DisplayText = String.Format("{0} - {1}", account.Code, account.Name);
            }
            else if (e.Column.Name.Equals("DynamicObjectList"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                //Get TransactionId
                var generalJournalId = grid.GetRowValues(e.VisibleRowIndex, "GeneralJournalId");
                if (generalJournalId == null) return;
                //Get transction
                NAS.DAL.Accounting.Journal.GeneralJournal generalJournal =
                    session.GetObjectByKey<NAS.DAL.Accounting.Journal.GeneralJournal>(generalJournalId);
                GeneralJournalObject generalJournalObject = generalJournal.GeneralJournalObjects.FirstOrDefault();
                if (generalJournalObject != null)
                {
                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerialize dynamicObjectList =
                        objectBO.GetDynamicObjectList(generalJournalObject.ObjectId.ObjectId);
                    if (dynamicObjectList != null)
                        e.DisplayText = dynamicObjectList.ToString();
                }
            }
        }

        protected void gridviewVoucherBookingEntries_DataBinding(object sender, EventArgs e)
        {
            //(sender as ASPxGridView).ForceDataRowType(Strategy.GetConcreteVoucherType();
        }

        protected void gridviewGeneralJournal_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (GUIContext.State is VoucherLockedBookingdEntries && GUIContext.State != null)
            {
                    e.Enabled = false;
            }
        }

        protected void gridviewGeneralJournal_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (GUIContext.State is VoucherLockedBookingdEntries && GUIContext.State != null)
            {
                e.Enabled = false;
            }
        }

        protected void gridviewVoucherBookingEntries_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (GUIContext.State is VoucherLockedBookingdEntries && GUIContext.State != null)
            {
                e.Enabled = false;
            }
        }

        protected void gridviewVoucherBookingEntries_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name.Equals("DynamicObjectList"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                //Get TransactionId
                var transactionId = grid.GetRowValues(e.VisibleRowIndex, "TransactionId");
                if (transactionId == null) return;
                //Get transction
                NAS.DAL.Accounting.Journal.Transaction transaction =
                    session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.FirstOrDefault();
                if (transactionObject != null)
                {
                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerialize dynamicObjectList =
                        objectBO.GetDynamicObjectList(transactionObject.ObjectId.ObjectId);
                    if (dynamicObjectList != null)
                        e.DisplayText = dynamicObjectList.ToString();
                }
            }
        }
    }
}