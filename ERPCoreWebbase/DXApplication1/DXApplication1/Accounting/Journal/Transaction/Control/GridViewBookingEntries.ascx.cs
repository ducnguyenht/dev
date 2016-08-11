using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.ERPSystem.CustomField.GUI.Control;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.JournalAllocation;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Accounting.Journal;
using WebModule.Accounting.Journal.Transaction.Control.Strategy;

namespace WebModule.Accounting.Journal.Transaction.Control
{
    public partial class GridViewBookingEntries : System.Web.UI.UserControl
    {
        public void SetDataSource(IEnumerable<NAS.DAL.Accounting.Journal.Transaction> datasource)
        {
            Strategy.SetTransactionsDataSource(datasource);
            InitDataSource();
        }

        public IEnumerable<NAS.DAL.Accounting.Journal.Transaction> GetDataSource()
        {
            return Strategy.TransactionsDataSource;
        }

        public string ClientInstanceName
        {
            get;
            set;
        }

        public string _ClientInstanceName
        {
            get
            {
                if (ClientInstanceName == null || ClientInstanceName.Trim().Length == 0)
                    return ClientID;
                return ClientInstanceName;
            }
        }

        public string GridViewDataChanged
        {
            get;
            set;
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

        public bool CanBookEntries(out IEnumerable<string> messages)
        {
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            messages = new List<string>();
            var transactions = Strategy.TransactionsDataSource;
            foreach (var transaction in transactions)
            {
                CanBookingEntryReturnValue canBookingEntryReturnValue =
                    transactionBOBase.CanBookingEntry(transaction.TransactionId, true);
                if (canBookingEntryReturnValue != CanBookingEntryReturnValue.BALANCED)
                {
                    string message = null;
                    if (!CanBookEntry(transaction, out message))
                    {
                        ((List<string>)messages).Add(message);
                    }
                }
            }
            if (messages.Count() != 0)
                return false;
            return true;
        }

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

        public void SetGridViewBookingEntriesStrategy(
            GridViewBookingEntriesStrategyEnum builtInStrategy)
        {
            Strategy = GridViewBookingEntriesStrategySimpleFactory.Create(builtInStrategy);
            InitDataSource();
        }

        public void SetGridViewBookingEntriesStrategy(
            GridViewBookingEntriesStrategy strategy)
        {
            Strategy = strategy;
            InitDataSource();
        }

        private void InitDataSource()
        {
            if (Strategy != null)
            {
                gridBookingEntries.DataSource = Strategy.TransactionsDataSource;
                gridBookingEntries.DataBind();
            }
        }

        private void InitClientScript()
        {
            popupAllocationObjects.ClientInstanceName = ClientID + "_popupAllocationObjects";
            cpnAllocationObjects.ClientInstanceName = ClientID + "_cpnAllocationObjects";
            gridBookingEntries.ClientInstanceName = ClientID + "_gridBookingEntries";

            gridBookingEntries.ClientSideEvents.CustomButtonClick = String.Format(
                "function(s ,e) {{"
                + "switch (e.buttonID) {{"
                + "case 'AllocateTransaction':"
                + "if (!{0}.InCallback()) {{"
                + "{1}.Show();"
                + "var args = 'AllocateTransaction|' + e.visibleIndex;"
                + "{0}.PerformCallback(args);"
                + "}}"
                + "break;"
                + "default:"
                + "break;"
                + "}}"
                + "}}",
                cpnAllocationObjects.ClientInstanceName,
                popupAllocationObjects.ClientInstanceName
            );

            popupAllocationObjects.ClientSideEvents.Closing = String.Format(
                "function(s, e) {{"
                + "{0}.Refresh();"
                + "}}",
                gridBookingEntries.ClientInstanceName
            );

        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsGeneralJournal.Session = session;
            InitClientScript();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                TransactionId = Guid.Empty;
                IsRaiseDataUpdated = false;
            }
            InitDataSource();
            TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
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

        protected void customFieldDataGridView_DataUpdated(object sender,
            ERPSystem.CustomField.GUI.Control.CustomFieldControlEventArgs args)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                //Get all journal of transaction
                NAS.DAL.Accounting.Journal.Transaction transaction =
                    uow.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(TransactionId);
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

        protected void customFieldDataGridView_BeforeDataEditing(object sender, EventArgs args)
        {
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            NAS.DAL.Accounting.Journal.Transaction transaction = null;
            string message;
            bool isBooked = transactionBOBase.IsBookedTransaction(session, TransactionId, out transaction);
            if (isBooked)
            {
                message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                throw new Exception(message);
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
                        //ObjectBO objectBO = new ObjectBO();
                        //NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum objectType = ObjectTypeEnum.NAAN_DEFAULT;
                        //objectType = Strategy.GetObjectTypeEnum();
                        //cmsObject = objectBO.CreateCMSObject(uow, objectType);
                        //TransactionObject newTransactionObject = new TransactionObject(uow)
                        //{
                        //    ObjectId = cmsObject,
                        //    TransactionId = transaction
                        //};
                        //uow.CommitChanges();
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
                        return Guid.Empty;
                        //ObjectBO objectBO = new ObjectBO();
                        //NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum objectType = ObjectTypeEnum.NAAN_DEFAULT; ;
                        //objectType = Strategy.GetObjectTypeEnum();
                        //cmsObject = objectBO.CreateCMSObject(uow, objectType);
                        //GeneralJournalObject newTransactionObject = new GeneralJournalObject(uow)
                        //{
                        //    ObjectId = cmsObject,
                        //    GeneralJournalId = generalJournal
                        //};
                        //uow.CommitChanges();
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

        protected void cpnAllocationObjects_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
                TransactionId = (Guid)gridBookingEntries.GetRowValues(visibleIndex, "TransactionId");
                customFieldDataGridView.CMSObjectId = GetCMSObjectIdOfTransaction(TransactionId);
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
                    TransactionId =
                        session.GetObjectByKey<GeneralJournal>(generalJournalId).TransactionId.TransactionId;
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

        protected void gridviewGeneralJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
            dsGeneralJournal.Criteria = CriteriaOperator.And(
                    new BinaryOperator("TransactionId!Key", transactionId),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_TEMP, BinaryOperatorType.GreaterOrEqual)
                ).ToString();
        }

        protected void gridviewGeneralJournal_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AccountId!Key")
            {
                AccountingBO accountingBO = new AccountingBO();
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.DataSource = accountingBO.getLeafAccounts(session);
                combo.DataBindItems();
            }
        }

        protected void gridviewGeneralJournal_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
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
            else if (e.Column.FieldName.Equals("Credit") || e.Column.FieldName.Equals("Debit"))
            {
                double amount = double.Parse(e.Value.ToString());
                if (amount == 0)
                    e.DisplayText = string.Empty;
                else
                    e.DisplayText = string.Format("{0:#,###} VNĐ", amount);
            }
        }

        protected void gridviewGeneralJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            try
            {
                Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
                TransactionBOBase transactionBOBase = new TransactionBOBase();
                NAS.DAL.Accounting.Journal.Transaction transaction = null;
                string message;
                bool isBooked = transactionBOBase.IsBookedTransaction(session, (Guid)transactionId, out transaction);
                if (isBooked)
                {
                    message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
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

        protected void gridviewGeneralJournal_Init(object sender, EventArgs e)
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
                cpnAllocationObjects.ClientInstanceName,
                popupAllocationObjects.ClientInstanceName
            );

            grid.ClientSideEvents.EndCallback = String.Format(
                "function(s, e) {{"
                + "if (s.cpEvent == 'GeneralJournalChanged') {{"
                + "{0}.RaiseGridViewDataChanged();"
                + "delete s.cpEvent;"
                + "}}"
                + "}}",
                ClientInstanceName != null ? ClientInstanceName : ClientID
            );
        }

        protected void gridBookingEntries_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //if (e.ButtonID.Equals("AllocateTransaction"))
            //{
            //    var transactionId = grid.GetRowValues(e.VisibleIndex, "TransactionId");
            //    if (transactionId == null)
            //        return;

            //    NAS.DAL.Accounting.Journal.Transaction transaction =
            //        session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId);

            //    if (transaction.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //    else
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.Default;
            //    }
            //}
        }

        protected void gridviewGeneralJournal_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //if (e.ButtonID.Equals("AllocateGeneralJournal"))
            //{
            //    var transactionId = grid.GetMasterRowKeyValue();
            //    if (transactionId == null)
            //        return;

            //    NAS.DAL.Accounting.Journal.Transaction transaction =
            //        session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId);
            //    if (transaction.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //    else
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.Default;
            //    }
            //}
        }

        protected void gridviewGeneralJournal_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //if (e.ButtonType == ColumnCommandButtonType.Edit
            //    || e.ButtonType == ColumnCommandButtonType.New
            //    || e.ButtonType == ColumnCommandButtonType.Delete)
            //{
            //    var transactionId = grid.GetMasterRowKeyValue();
            //    if (transactionId == null)
            //        return;

            //    NAS.DAL.Accounting.Journal.Transaction transaction =
            //        session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId);
            //    if (transaction.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            //    {
            //        e.Visible = false;
            //    }
            //    else
            //    {
            //        e.Visible = true;
            //    }
            //}

        }

        protected void gridviewGeneralJournal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            NAS.DAL.Accounting.Journal.Transaction transaction = null;
            string message;
            bool isBooked = transactionBOBase.IsBookedTransaction(session, (Guid)transactionId, out transaction);
            if (isBooked)
            {
                message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                throw new Exception(message);
            }
        }

        protected void gridviewGeneralJournal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            Guid transactionId = (Guid)grid.GetMasterRowKeyValue();
            TransactionBOBase transactionBOBase = new TransactionBOBase();
            NAS.DAL.Accounting.Journal.Transaction transaction = null;
            string message;
            bool isBooked = transactionBOBase.IsBookedTransaction(session, (Guid)transactionId, out transaction);
            if (isBooked)
            {
                message = String.Format("Bút toán '{0}' đã được ghi sổ", transaction.Code);
                throw new Exception(message);
            }
        }

        protected void gridBookingEntries_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
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

            else if (e.Column.FieldName.Equals("Amount"))
            {
                double amount = double.Parse(e.Value.ToString());
                if (amount == 0)
                    e.DisplayText = "0";
                else
                    e.DisplayText = string.Format("{0:#,###} VNĐ", amount);
            }
        }

    }
}