using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Accounting.Configure;
using DevExpress.Data.Filtering;
using NAS.DAL.Staging.Accounting.Journal;
using WebModule.Accounting.AllocationConfigure.State.AllocationForm;
using NAS.BO.Accounting;

namespace WebModule.Accounting.AllocationConfigure.GUI
{
    public partial class AllocationEditingForm : System.Web.UI.UserControl
    {

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsAllocation.Session = session;
            dsAllocationAccountTemplate.Session = session;
            dsAllocationType.Session = session;
            dsAccountActorType.Session = session;
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
                AllocationId = Guid.Empty;
            }
            gridviewAllocationAccountTemplate_BindData();
        }

        private Guid AllocationId
        {
            get
            {
                return (Guid)Session["AllocationId_" + ViewStateControlId];
            }
            set
            {
                Session["AllocationId_" + ViewStateControlId] = value;
            }
        }

        public void ClearForm()
        {
            gridviewAllocationAccountTemplate.CancelEdit();
            ASPxEdit.ClearEditorsInContainer(formlayoutAllocationEditingForm);
            gridlookupAccountActorType.GridView.Selection.UnselectAll();
            ClearFormValidation();
        }

        private void ClearFormValidation()
        {
            txtCode.IsValid = true;
            txtName.IsValid = true;
            cboAllocationType.IsValid = true;
            cbIsMasterAccountActorType.IsValid = true;
        }

        private Allocation CurrentAllocation()
        {
            if (AllocationId == null)
            {
                return null;
            }
            else
            {
                Guid objectId = AllocationId;
                if (objectId.Equals(Guid.Empty))
                {
                    return null;
                }
                else
                {
                    return session.GetObjectByKey<Allocation>(objectId);
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

        #region UpdateGUI
        public bool AllocationCanceling_UpdateGUI()
        {
            popupAllocationEditingForm.ShowOnPageLoad = false;
            return true;
        }
        public bool AllocationCreating_UpdateGUI()
        {
            ClearForm();
            popupAllocationEditingForm.ShowOnPageLoad = true;
            popupAllocationEditingForm.HeaderText = "Cấu hình phân bổ - Thêm mới";
            return true;
        }
        public bool AllocationEditing_UpdateGUI()
        {
            ClearFormValidation();
            gridviewAllocationAccountTemplate.CancelEdit();
            popupAllocationEditingForm.ShowOnPageLoad = true;
            popupAllocationEditingForm.HeaderText = "Cấu hình phân bổ - " + CurrentAllocation().Code;
            gridlookupAccountActorType_UpdateSelection();
            return true;
        }
        #endregion

        #region CRUD
        public bool AllocationCanceling_CRUD()
        {
            return true;
        }
        public bool AllocationCreating_CRUD()
        {
            //Bind data form
            formlayoutAllocationEditingForm.DataSourceID = null;
            formlayoutAllocationEditingForm.DataBind();
            //Bind data to gridview AllocationAccountTemplate
            gridviewAllocationAccountTemplate_BindData();
            return true;
        }
        public bool AllocationEditing_CRUD()
        {
            //Bind data form
            formlayoutAllocationEditingForm.DataSourceID = dsAllocation.ID;
            CriteriaOperator criteria = new BinaryOperator("AllocationId", AllocationId);
            dsAllocation.Criteria = criteria.ToString();
            formlayoutAllocationEditingForm.DataBind();
            cbIsMasterAccountActorType.Value = 
                CurrentAllocation().AllocationAccountActorTypes.First(r => r.IsMaster).AccountActorTypeId.AccountActorTypeId;
            //Bind data to gridview AllocationAccountTemplate
            gridviewAllocationAccountTemplate_BindData();
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool AllocationCanceling_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool AllocationCreating_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "SAVE":
                    if (ASPxEdit.ValidateEditorsInContainer(formlayoutAllocationEditingForm))
                    {
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {
                            
                            Guid selectedAllocationTypeId;
                            Guid selectedAccountActorTypeId;
                            AllocationType selectedAllocationType = null;
                            AccountActorType selectedAccountActorType = null;
                            Allocation allocation = null;
                            bool isParseSuccess;

                            //Get selected allocation type
                            isParseSuccess = Guid.TryParse(cboAllocationType.Value.ToString(), out selectedAllocationTypeId);
                            if(!isParseSuccess) 
                            {
                                throw new Exception("The string is invalid for parsing to GUID");
                            }
                            selectedAllocationType = uow.GetObjectByKey<AllocationType>(selectedAllocationTypeId);

                            //Create new allocation
                            allocation = new Allocation(uow)
                            {
                                AllocationId = Guid.NewGuid(),
                                AllocationTypeId = selectedAllocationType,
                                Code = txtCode.Text,
                                Description = txtDescription.Text,
                                Name = txtName.Text,
                                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                                //OwnerOrgId
                            };

                            //Get selected IsMaster AccountActorType
                            isParseSuccess =
                                Guid.TryParse(cbIsMasterAccountActorType.Value.ToString()
                                    , out selectedAccountActorTypeId);
                            if (!isParseSuccess)
                            {
                                throw new Exception("The string is invalid for parsing to GUID");
                            }
                            selectedAccountActorType = uow.GetObjectByKey<AccountActorType>(selectedAccountActorTypeId);

                            //Create new IsMaster AllocationAccountActorType
                            AllocationAccountActorType allocationAccountActorType
                                = new AllocationAccountActorType(uow)
                                {
                                    AccountActorTypeId = selectedAccountActorType,
                                    AllocationId = allocation,
                                    IsMaster = true
                                };

                            //Get selected ids in gridlookupAccountActorType
                            var relatedAccountActorTypeIds = gridlookupAccountActorType.GridView
                                .GetSelectedFieldValues("AccountActorTypeId")
                                .Select(r => Guid.Parse(r.ToString()));
                            //Create new related AllocationAccountActorTypes
                            foreach (var relatedAccountActorTypeId in relatedAccountActorTypeIds)
                            {
                                AllocationAccountActorType relatedAllocationAccountActorType
                                = new AllocationAccountActorType(uow)
                                {
                                    AccountActorTypeId = uow.GetObjectByKey<AccountActorType>(relatedAccountActorTypeId),
                                    AllocationId = allocation,
                                    IsMaster = false
                                };
                                relatedAllocationAccountActorType.Save();
                            }
   
                            //Set new Id to session variable
                            AllocationId = allocation.AllocationId;
                            uow.CommitChanges();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        public bool AllocationEditing_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "SAVE":
                    if (ASPxEdit.ValidateEditorsInContainer(formlayoutAllocationEditingForm))
                    {
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {

                            Guid selectedAllocationTypeId;
                            Guid selectedAccountActorTypeId;
                            AllocationType selectedAllocationType = null;
                            AccountActorType selectedAccountActorType = null;
                            Allocation allocation = null;
                            bool isParseSuccess;

                            //Get selected allocation type
                            isParseSuccess = Guid.TryParse(cboAllocationType.Value.ToString(), out selectedAllocationTypeId);
                            if (!isParseSuccess)
                            {
                                throw new Exception("The string is invalid for parsing to GUID");
                            }
                            selectedAllocationType = uow.GetObjectByKey<AllocationType>(selectedAllocationTypeId);

                            //Update allocation
                            allocation = uow.GetObjectByKey<Allocation>(AllocationId);
                            allocation.Code = txtCode.Text;
                            allocation.Name = txtName.Text;
                            allocation.Description = txtDescription.Text;
                            allocation.AllocationTypeId = selectedAllocationType;

                            //Get selected IsMaster AccountActorType
                            isParseSuccess =
                                Guid.TryParse(cbIsMasterAccountActorType.Value.ToString()
                                    , out selectedAccountActorTypeId);
                            if (!isParseSuccess)
                            {
                                throw new Exception("The string is invalid for parsing to GUID");
                            }
                            selectedAccountActorType = uow.GetObjectByKey<AccountActorType>(selectedAccountActorTypeId);

                            //Update isMaster AllocationAccountActorType
                            AllocationAccountActorType isMasterAllocationAccountActorType = 
                                allocation.AllocationAccountActorTypes.FirstOrDefault(r => r.IsMaster);
                            if (isMasterAllocationAccountActorType != null)
                            {
                                isMasterAllocationAccountActorType.AccountActorTypeId = selectedAccountActorType;
                            }
                            else
                            {
                                //Create new IsMaster AllocationAccountActorType
                                AllocationAccountActorType allocationAccountActorType
                                    = new AllocationAccountActorType(uow)
                                    {
                                        AccountActorTypeId = selectedAccountActorType,
                                        AllocationId = allocation,
                                        IsMaster = true
                                    };
                            }
                            
                            //Get selected ids in gridlookupAccountActorType
                            var relatedAccountActorTypeIds = gridlookupAccountActorType.GridView
                                .GetSelectedFieldValues("AccountActorTypeId")
                                .Select(r => Guid.Parse(r.ToString()));
                            List<AllocationAccountActorType> relatedAccountActorTypes = 
                                allocation.AllocationAccountActorTypes.Where(r => !r.IsMaster).ToList();
                            uow.Delete(relatedAccountActorTypes);

                            //Create new related AllocationAccountActorTypes
                            foreach (var relatedAccountActorTypeId in relatedAccountActorTypeIds)
                            {
                                AllocationAccountActorType relatedAllocationAccountActorType
                                = new AllocationAccountActorType(uow)
                                {
                                    AccountActorTypeId = uow.GetObjectByKey<AccountActorType>(relatedAccountActorTypeId),
                                    AllocationId = allocation,
                                    IsMaster = false
                                };
                                relatedAllocationAccountActorType.Save();
                            }

                            //Set new Id to session variable
                            AllocationId = allocation.AllocationId;
                            uow.CommitChanges();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        #endregion

        private void gridlookupAccountActorType_UpdateSelection()
        {
            gridlookupAccountActorType.GridView.Selection.UnselectAll();
            Allocation currentAllocation = CurrentAllocation();
            if (currentAllocation != null)
            {
                var relatedAllocationAccountActorTypes =
                    currentAllocation.AllocationAccountActorTypes.Where(r => r.IsMaster == false);
                if (relatedAllocationAccountActorTypes != null)
                {
                    foreach (var relatedAllocationAccountActorType in relatedAllocationAccountActorTypes)
                    {
                        gridlookupAccountActorType.GridView.Selection
                            .SetSelectionByKey(relatedAllocationAccountActorType.AccountActorTypeId.AccountActorTypeId, true);
                    }
                }
            }
        }

        private void gridviewAllocationAccountTemplate_BindData()
        {
            if (AllocationId == Guid.Empty)
            {
                gridviewAllocationAccountTemplate.DataSourceID = null;
                gridviewAllocationAccountTemplate.DataBind();
            }
            else
            {
                gridviewAllocationAccountTemplate.DataSourceID = dsAllocationAccountTemplate.ID;
                CriteriaOperator criteria = new BinaryOperator("AllocationId.AllocationId", AllocationId);
                dsAllocationAccountTemplate.Criteria = criteria.ToString();
                gridviewAllocationAccountTemplate.DataBind();
            }

        }

        protected void cpnAllocationEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            bool isSuccess = false;
            try
            {
                switch (command)
                {
                    case "Create":
                        AllocationId = Guid.NewGuid();
                        GUIContext.State = new AllocationCreating(this);
                        break;
                    case "Edit":
                        if (args.Length > 1)
                        {
                            AllocationId = Guid.Parse(args[1]);
                        }
                        else
                        {
                            throw new Exception("Invalid parameters");
                        }
                        GUIContext.State = new AllocationEditing(this);
                        break;
                    default:
                        GUIContext.Request(command, this);
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
                    cpnAllocationEditingForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            command, isSuccess.ToString().ToLower()));
                }
            }
        }
        #endregion

        protected void gridviewAllocationAccountTemplate_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (GUIContext.State is AllocationCreating)
            {
                throw new Exception("Click 'Lưu lại' để lưu thông tin chung trước");
            }
        }

        protected void gridviewAllocationAccountTemplate_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["AllocationId!Key"] = AllocationId;
        }

        protected void gridviewAllocationAccountTemplate_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("CardSite"))
            {
                if (e.Value.Equals('D'))
                {
                    e.DisplayText = "Nợ";
                }
                else if (e.Value.Equals('C'))
                {
                    e.DisplayText = "Có";
                }
            }
            else if (e.Column.FieldName.Equals("AccountId!Key")) 
            {
                Guid accountId = (Guid)e.Value;
                e.DisplayText = session.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(accountId).Code;
            }
        }

        protected void gridviewAllocationAccountTemplate_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AccountId!Key")
            {
                AccountingBO accountingBO = new AccountingBO();
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.DataSource = accountingBO.getLeafAccounts(session);
                combo.DataBindItems();
            }
        }

    }
}