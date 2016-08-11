using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.ETLBO.System.Object;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Web.ASPxGridView;
using NAS.BO.CMS.ObjectDocument;
using WebModule.Accounting.Journal.Transaction.Control.Strategy;
using NAS.DAL.Accounting.JournalAllocation;
using WebModule.ERPSystem.CustomField.GUI.Control;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;



namespace WebModule.Accounting.GeneralBookingEntries
{
    public partial class GeneralBookingEditingForm : System.Web.UI.UserControl
    {
        //--------------------------------------------------------------

        Session m_Session;

        private Guid m_TransactionId = Guid.Empty;
        private string[] m_ParamCallBack = null;
        string m_Code = "";
        string m_Description = "";
        DateTime m_IssuedDate = DateTime.Now;
        double m_Amount = 0;

        private CriteriaOperator m_Filter = "";

        private Transaction m_Transaction = null;

        private ManualBookingTransactionBO m_ManualBookingTransactionBO = new ManualBookingTransactionBO();
        private BusinessObjectBO m_BusinessObjectBO = new BusinessObjectBO();
        private TransactionBOBase m_TransactionBOBase = new TransactionBOBase();


        //---------------------------------------------------------------

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
                        return Guid.Empty;
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

        public bool CanBookEntry(NAS.DAL.Accounting.Journal.Transaction transaction, out string message)
        {
            message = null;
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            CanBookingEntryReturnValue canBookingEntryReturnValue =
                    transactionBOBase.CanBookingEntry(transaction.TransactionId, true);
            if (canBookingEntryReturnValue != CanBookingEntryReturnValue.BALANCED)
            {
                switch (canBookingEntryReturnValue)
                {
                    case CanBookingEntryReturnValue.HAVE_NO_JOURNAL:
                        message = String.Format("Chưa có phát sinh nào trong bút toán '{0}'",
                            transaction.Code);
                        break;
                    case CanBookingEntryReturnValue.DEBIT_CREDIT_ZERO:
                        message = String.Format("Phát sinh 'Nợ' và phát sinh 'Có' cùng bằng 0 trong bút toán '{0}'",
                            transaction.Code);
                        break;
                    case CanBookingEntryReturnValue.NOT_BALANCED:
                        message = String.Format("Phát sinh 'Nợ' và phát sinh 'Có' không cân bằng trong bút toán '{0}'",
                            transaction.Code);
                        break;
                    case CanBookingEntryReturnValue.NOT_EQUAL_WITH_TOTAL:
                        message = String.Format("Phát sinh 'Nợ' và phát sinh 'Có' không bằng với tổng tiền trong bút toán '{0}'",
                            transaction.Code);
                        break;
                    case CanBookingEntryReturnValue.INVALID_TRANSACTION_STATUS:
                        message = String.Format("Trạng thái của bút toán '{0}' không hợp lệ", transaction.Code);
                        break;
                    case CanBookingEntryReturnValue.MANY_SIDE:
                        message = String.Format("Bút toán '{0}' vừa có nhiều Phát sinh 'Nợ' và nhiều phát sinh 'Có'",
                            transaction.Code);
                        break;
                    default:
                        break;
                }
            }
            if (message != null)
                return false;
            return true;
        }

        private GridViewBookingEntriesStrategy _Strategy;
        private GridViewBookingEntriesStrategy Strategy
        {
            get
            {
                if (_Strategy == null)
                    _Strategy = new GridViewBookingEntriesDefaultStrategy();
                return _Strategy;
            }
            set
            {
                _Strategy = value;
            }
        }

        private void TransactionCustomFieldDataGridView_BindDataUpdatedEvent()
        {
            if (!IsRaiseDataUpdated)
            {
                customFieldDataGridView.DataUpdated -=
                    new CustomFieldControlDataUpdatedEventHandler(customFieldDataGridView_DataUpdated);
            }
            else
            {
                customFieldDataGridView.DataUpdated +=
                    new CustomFieldControlDataUpdatedEventHandler(customFieldDataGridView_DataUpdated);
            }
            customFieldDataGridView.BeforeDataEditing += new CustomFieldControlBeforeDataEditingEventHandler(customFieldDataGridView_BeforeDataEditing);
        }

        protected void customFieldDataGridView_BeforeDataEditing(object sender, EventArgs args)
        {
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            Transaction transaction = null;
            string message;
            bool isBooked = transactionBOBase.IsBookedTransaction(m_Session, m_TransactionId, out transaction);
            if (isBooked)
            {
                message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                throw new Exception(message);
            }
        }

        protected void customFieldDataGridView_DataUpdated(object sender,
          ERPSystem.CustomField.GUI.Control.CustomFieldControlEventArgs args)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                //Get all journal of transaction
                NAS.DAL.Accounting.Journal.Transaction transaction =
                    uow.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(m_TransactionId);
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

        //---------------------------------------------------------------

        protected void Page_Init(object sender, EventArgs e)
        {
            m_Session = XpoHelper.GetNewSession();

            gridviewGeneralJournal.Session = m_Session;
            gridviewGeneralJournal.Criteria = "TransactionId.TransactionId = ? And RowStatus >= ?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TransactionId"] != null)
            {
                gridviewGeneralJournal.CriteriaParameters.Add("TransactionId", Session["TransactionId"].ToString());
                gridviewGeneralJournal.CriteriaParameters.Add("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE.ToString());
            }
        }

        private Guid GetCMSObjectIdOfTransaction(Guid transactionId)
        {
            if (transactionId != null && !transactionId.Equals(Guid.Empty))
            {
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = null;
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    NAS.DAL.Accounting.Journal.Transaction transaction =
                        uow.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId);
                    TransactionObject transactionObject =
                        transaction.TransactionObjects.FirstOrDefault();
                    if (transactionObject == null)
                    {
                        return Guid.Empty;
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

        protected void PanelGeneralBooking_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            m_ParamCallBack = e.Parameter.Split('|');

            switch (m_ParamCallBack[0])
            {
                case "edit":

                    txtGeneralBookingCode.Text = "";
                    txtGeneralBookingDate.Value = DateTime.Now;
                    txtGeneralBookingStatus.Text = "Chưa ghi sổ";
                    txtGeneralBookingAmount.Value = null;
                    txtGeneralBookingDescription.Text = "";

                    m_TransactionId = Guid.Parse(m_ParamCallBack[1].ToString());
                    m_Transaction = m_Session.GetObjectByKey<Transaction>(m_TransactionId);
                    if (m_Transaction != null)
                    {
                        txtGeneralBookingCode.Text = m_Transaction.Code;
                        txtGeneralBookingAmount.Value = m_Transaction.Amount;
                        txtGeneralBookingDate.Value = m_Transaction.IssueDate;
                        txtGeneralBookingDescription.Text = m_Transaction.Description;
                        txtGeneralBookingStatus.Text = "";

                        gridviewGeneralJournal.CriteriaParameters.Add("TransactionId", m_TransactionId.ToString());
                        gridviewGeneralJournal.CriteriaParameters.Add("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE.ToString());

                        grdGeneralBookingJournal.DataBind();
                    }
                    else
                    {
                        m_TransactionId = Guid.NewGuid();
                    }

                    Session["TransactionId"] = m_TransactionId;
                    PopupGeneralBooking.ShowOnPageLoad = true;

                    gridviewGeneralJournal.CriteriaParameters.Add("TransactionId", m_TransactionId.ToString());
                    gridviewGeneralJournal.CriteriaParameters.Add("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE.ToString());

                    break;

                case "cancel":
                    Session["TransactionId"] = null;
                    PopupGeneralBooking.ShowOnPageLoad = false;
                    break;

                case "book":
                    UnitOfWork uow = XpoHelper.GetNewUnitOfWork();


                    string message;
                    Transaction transaction = null;
                    bool isBooked = m_TransactionBOBase.IsBookedTransaction(m_Session, (Guid)Session["TransactionId"], out transaction);
                    if (isBooked)
                    {
                        message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                        throw new Exception(message);
                    }
                    if (!CanBookEntry(transaction, out message))
                    {
                        throw new Exception(message);
                    }
                    if (transaction != null)
                    {
                        if (!m_TransactionBOBase.BookEntry(uow, transaction.TransactionId))
                            throw new Exception("Xử lý ghi sổ phát sinh lỗi");

                        m_BusinessObjectBO.CreateBusinessObject(uow,
                                Utility.Constant.BusinessObjectType_FinancialTransaction,
                                transaction.TransactionId,
                                transaction.IssueDate);
                    }

                    uow.CommitChanges();
                    PanelGeneralBooking.JSProperties.Add("cpBooked", "Complete");
                    Session["TransactionId"] = null;
                    break;

                case "save":

                    if (txtGeneralBookingCode.Text == ""
                            || txtGeneralBookingDate.Value == null
                                || txtGeneralBookingAmount.Value == null)
                    {
                        return;
                    }

                    m_TransactionId = (Guid)Session["TransactionId"];

                    m_Code = txtGeneralBookingCode.Text;
                    m_Description = txtGeneralBookingDescription.Text;
                    m_IssuedDate = (DateTime)txtGeneralBookingDate.Value;
                    m_Amount = double.Parse(txtGeneralBookingAmount.Value.ToString());

                    m_ManualBookingTransactionBO = new ManualBookingTransactionBO();

                    m_Transaction = m_Session.GetObjectByKey<Transaction>(m_TransactionId);
                    if (m_Transaction != null)
                    {
                        isBooked = m_TransactionBOBase.IsBookedTransaction(m_Session, (Guid)Session["TransactionId"], out transaction);
                        if (isBooked)
                        {
                            message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                            throw new Exception(message);
                        }

                        m_ManualBookingTransactionBO.UpdateTransaction(m_TransactionId, m_Code, m_IssuedDate, m_Amount, m_Description);
                    }
                    else
                    {
                        m_TransactionId = m_ManualBookingTransactionBO.CreateTransaction(m_Code, m_IssuedDate, m_Amount, m_Description);
                    }

                    Session["TransactionId"] = m_TransactionId;

                    gridviewGeneralJournal.CriteriaParameters.Add("TransactionId", m_TransactionId.ToString());
                    gridviewGeneralJournal.CriteriaParameters.Add("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE.ToString());

                    break;

            }
        }

        protected void colAccount_OnItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            try
            {
                Account obj = m_Session.GetObjectByKey<Account>(Guid.Parse(e.Value.ToString()));

                if (obj != null)
                {
                    comboBox.DataSource = new Account[] { obj };
                    comboBox.DataBindItems();
                }
            }
            catch
            {

            }
        }

        protected void colAccount_OnItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            XPCollection<Account> collection = new XPCollection<Account>(m_Session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;
            collection.Criteria = new GroupOperator(GroupOperatorType.And,
                        CriteriaOperator.Or(
                                new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)),
                        new BinaryOperator("RowStatus", "1", BinaryOperatorType.GreaterOrEqual));

            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            comboBox.DataSource = collection;
            comboBox.DataBindItems();
        }

        protected void grdGeneralBookingJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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

                Guid transactionId = (Guid)Session["TransactionId"];

                TransactionBOBase transactionBOBase = new TransactionBOBase();
                Transaction transaction = null;
                string message;
                bool isBooked = transactionBOBase.IsBookedTransaction(m_Session, (Guid)transactionId, out transaction);
                if (isBooked)
                {
                    message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                    throw new Exception(message);
                }

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

                Strategy.CreateGeneralJournal(
                    uow,
                    transactionId,
                    accountId,
                    side,
                    amount,
                    description,
                    JounalTypeFlag.ACTUAL);

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

        protected void grdGeneralBookingJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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

        protected void grdGeneralBookingJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            try
            {
                Guid transactionId = (Guid)Session["TransactionId"];
                string message;
                bool isBooked = m_TransactionBOBase.IsBookedTransaction(m_Session, (Guid)transactionId, out m_Transaction);
                if (isBooked)
                {
                    message = String.Format("Bút toán '{0}' đã được ghi sổ", m_Transaction.Code);
                    throw new Exception(message);
                }

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

        protected void grdGeneralBookingJournal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            bool isBooked = m_TransactionBOBase.IsBookedTransaction(m_Session, (Guid)Session["TransactionId"], out m_Transaction);
            if (isBooked)
            {
                string message = String.Format("Bút toán '{0}' đã được ghi sổ", m_Transaction.Code);
                throw new Exception(message);
            }
        }

        protected void grdGeneralBookingJournal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            Guid transactionId = (Guid)Session["TransactionId"];

            m_Transaction = m_Session.GetObjectByKey<Transaction>(transactionId);
            if (m_Transaction == null)
            {
                Session["TransactionId"] = m_ManualBookingTransactionBO.CreateTransaction(m_Code, m_IssuedDate, m_Amount, m_Description);
                transactionId = (Guid)Session["TransactionId"];
            }
            else
            {
                bool isBooked = m_TransactionBOBase.IsBookedTransaction(m_Session, (Guid)Session["TransactionId"], out m_Transaction);
                if (isBooked)
                {
                    string message = String.Format("Bút toán '{0}' đã được ghi sổ", m_Transaction.Code);
                    throw new Exception(message);
                }
            }
        }

        protected void cpnGeneralBookingAllocationObjects_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
                m_TransactionId = (Guid)Session["TransactionId"];
                customFieldDataGridView.CMSObjectId = GetCMSObjectIdOfTransaction(m_TransactionId);
                customFieldDataGridView.DataBind();
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
                    m_TransactionId = m_Session.GetObjectByKey<GeneralJournal>(generalJournalId).TransactionId.TransactionId;
                }
                else
                {
                    throw new Exception("Invalid parameter");
                }
                customFieldDataGridView.CMSObjectId = GetCMSObjectIdOfGeneralJounal(generalJournalId);
                customFieldDataGridView.DataBind();
                IsRaiseDataUpdated = false;
                TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
            }
            #endregion
        }


        protected void grdGeneralBookingJournal_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.ClientSideEvents.CustomButtonClick = String.Format(
                "function(s ,e) {{"
                + "switch (e.buttonID) {{"
                + "case 'AllocateGeneralJournal':"
                + "if (!{0}.InCallback()) {{"
                + "{1}.Show();"
                + "var args = 'AllocateGeneralJournal|' + s.GetRowKey(e.visibleIndex);"
                + "{0}.PerformCallback(args);"
                + "}}"
                + "break;"
                + "default:"
                + "break;"
                + "}}"
                + "}}",
                cpnGeneralBookingAllocationObjects.ClientInstanceName,
                popupGeneralBookingAllocationObjects.ClientInstanceName
            );
        }

        
    }
}