using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Web.ASPxGridView;
using Utility;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Vouches;

namespace WebModule.Voucher.Controls.GridViewVoucherAllocation
{
    public partial class GridViewVoucherAllocation : System.Web.UI.UserControl
    {
        public void AddNewRow()
        {
            gridviewAllocation.AddNewRow();
        }

        public ASPxGridView GridView { get { return gridviewAllocation; } }

        private GridViewVoucherAllocationStrategy Strategy
        {
            get;
            set;
        }

        public void SetGridViewVoucherAllocationStrategy
            (GridViewVoucherAllocationStrategy strategy)
        {
            Strategy = strategy;
            InitDataSource();
        }

        public void SetGridViewVoucherAllocationStrategy
            (GridViewVoucherAllocationStrategyEnum defaultStrategyType)
        {
            Strategy =
                GridViewVoucherAllocationStrategySimpleFactory
                    .CreateGridViewVoucherAllocationStrategy(defaultStrategyType);
            InitDataSource();
        }

        private void InitDataSource()
        {
            if (Strategy != null)
            {
                dsVoucherTransaction.TypeName = Strategy.GetConcreteVoucherType().FullName;
                CriteriaOperator criteria = Strategy.GetVoucherTransactionCriteria(VoucherId);
                dsVoucherTransaction.Criteria = criteria.ToString();
                gridviewAllocation.DataBind();
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsVoucherTransaction.Session = session;
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
            }
            //
            
            //
            if (!IsPostBack)
            {
                VoucherId = Guid.Empty;
                TransactionId = Guid.Empty;
            }
            InitDataSource();
            //voidcustomFieldDataGridView_DataBind();
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

        protected void gridviewAllocation_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string code = (string)e.NewValues["Code"];
                string description = (string)e.NewValues["Description"];
                //DateTime issuedDate = (DateTime)e.NewValues["IssueDate"];
                double amount = (double)e.NewValues["Amount"];
                //Create voucher transaction
                Vouches voucher = session.GetObjectByKey<Vouches>(VoucherId);
                Strategy.CreateVoucherTransaction(VoucherId, code, voucher.IssuedDate, amount, description);
                gridviewAllocation.JSProperties["cpEvent"] = "ListChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridviewAllocation.CancelEdit();
            }
        }

        protected void gridviewAllocation_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                //Delete voucher transaction
                Guid transactionId = (Guid)e.Keys["TransactionId"];
                Strategy.DeleteVoucherTransaction(transactionId);
                gridviewAllocation.JSProperties["cpEvent"] = "ListChanged";
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

        protected void gridviewAllocation_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                Guid transactionId = (Guid)e.Keys["TransactionId"];
                string code = (string)e.NewValues["Code"];
                string description = (string)e.NewValues["Description"];
                //DateTime issuedDate = (DateTime)e.NewValues["IssueDate"];
                double amount = (double)e.NewValues["Amount"];
                //Update voucher transaction
                Vouches voucher = session.GetObjectByKey<Vouches>(VoucherId);
                Strategy.UpdateVoucherTransaction(transactionId, code, voucher.IssuedDate, amount, description);
                gridviewAllocation.JSProperties["cpEvent"] = "ListChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridviewAllocation.CancelEdit();
            }
        }

        protected void popupAllocationObjects_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            //string[] args = e.Parameter.Split('|');
            //string command = args[0];
            //if (command.Equals("Allocate"))
            //{
            //    int visibleIndex;
            //    //Get CMS object of transaction
            //    if (args.Length > 1)
            //    {
            //        visibleIndex = int.Parse(args[1]);
            //    }
            //    else
            //    {
            //        throw new Exception("Invalid parameter");
            //    }
            //    Guid transactionId = (Guid)gridviewAllocation.GetRowValues(visibleIndex, "TransactionId");
            //    TransactionId = transactionId;

            //    voidcustomFieldDataGridView_DataBind();
            //}
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
                        if (Strategy.GetConcreteVoucherType().Equals(typeof(NAS.DAL.Vouches.ReceiptVouches)))
                        {
                            objectType = NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_RECEIPT;
                        }
                        else if (Strategy.GetConcreteVoucherType().Equals(typeof(NAS.DAL.Vouches.PaymentVouches)))
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

        protected void customFieldDataGridView_DataUpdated(object sender,
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
                                    if(temp != null)
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

        protected void cpnAllocationObjects_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //popupAllocationObjects.ShowOnPageLoad = true;
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            if (command.Equals("Allocate"))
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
                Guid transactionId = (Guid)gridviewAllocation.GetRowValues(visibleIndex, "TransactionId");
                TransactionId = transactionId;
                customFieldDataGridView.CMSObjectId = GetCMSObjectIdOfTransaction(TransactionId);
                customFieldDataGridView.DataBind();
            }
        }

        protected void gridviewAllocation_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            //Validate Debit 
            if (e.NewValues["Amount"] != null)
            {
                double amount = (double)e.NewValues["Amount"];
                if (amount <= 0)
                {
                    Helpers.AddErrorToGridViewColumn(e.Errors,
                                grid.Columns["Amount"],
                                "Số tiền phải lớn hơn 0");
                }
            }


        }

        public delegate void InitNewRowWithDefaultDataHandler(TransactionInitRowData data);

        public event InitNewRowWithDefaultDataHandler InitNewRowWithDefaultDataEvent;

        protected void gridviewAllocation_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //Set default data
            if (InitNewRowWithDefaultDataEvent != null)
            {
                TransactionInitRowData defaultData = new TransactionInitRowData();
                InitNewRowWithDefaultDataEvent(defaultData);
                SetTransactionInitRowData(e, defaultData);
            }
        }

        private void SetTransactionInitRowData(DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e, TransactionInitRowData data)
        {
            if (data != null)
            {
                if (data.Amount != double.MinValue)
                {
                    e.NewValues["Amount"] = data.Amount;
                }
                if (data.Code != null && data.Code.Trim().Length > 0)
                {
                    e.NewValues["Code"] = data.Code;
                }
                if (data.Description != null && data.Description.Trim().Length > 0)
                {
                    e.NewValues["Description"] = data.Description;
                }
                if (data.IssuedDate != DateTime.MinValue)
                {
                    e.NewValues["IssueDate"] = data.IssuedDate;
                }
            }
        }

        public class TransactionInitRowData
        {
            public string Code { get; set; }
            public DateTime IssuedDate { get; set; } 
            public double Amount { get; set; }
            public string Description { get; set; }
        }

        protected void gridviewAllocation_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
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