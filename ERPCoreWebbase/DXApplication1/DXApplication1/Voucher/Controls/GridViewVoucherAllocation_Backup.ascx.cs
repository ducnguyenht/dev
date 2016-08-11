using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Accounting.Configure;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxCallbackPanel;
using NAS.DAL.Vouches;
using NAS.DAL.Staging.Accounting.Journal;
using WebModule.Accounting.AllocationConfigure.Controls;
using NAS.DAL.Vouches.Allocation;

namespace WebModule.Voucher.Controls
{
    public partial class GridViewVoucherAllocation_Backup : System.Web.UI.UserControl
    {
        public void SetAllocationGetter
            (NAS.BO.Accounting.Configure.AllocationGetter.AllocationGetter allocationGetter)
        {
            AllocationGetter = allocationGetter;
        }

        private NAS.BO.Accounting.Configure.AllocationGetter.AllocationGetter AllocationGetter
        {
            get;
            set;
        }

        public Guid VoucherId
        {
            get
            {
                return (Guid)Session["GridViewVoucherAllocation_VoucherId_" + ViewStateControlId];
            }
            set
            {
                Session["GridViewVoucherAllocation_VoucherId_" + ViewStateControlId] = value;
            }
        } 

        public void DataBind()
        {
            if(VoucherId == null || VoucherId.Equals(Guid.Empty))
            {
                //Clear gridview
                dsVoucherAllocation.Criteria = "[VouchesId!Key] = ?";
            }
            CriteriaOperator criteria = new BinaryOperator("VouchesId!Key", VoucherId);
            dsVoucherAllocation.Criteria = criteria.ToString();
            gridVoucherAllocation.DataBind();
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsVoucherAllocation.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                VoucherId = Guid.NewGuid();
            }
            DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
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

        private void comboboxAllocation_DataBind(ASPxComboBox combobox)
        {
            if (AllocationGetter == null)
            {
                throw new Exception("Please use SetAllocationGetter method to set AllocationGetter");
            }
            combobox.DataSource = AllocationGetter.GetAllocationCollection(session);
            combobox.DataBindItems();
        }

        protected void gridVoucherAllocation_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    if (VoucherId == null || VoucherId.Equals(Guid.Empty))
                    {
                        throw new Exception("The operation is not supported. Please set VoucherId for the gridview.");
                    }
                    //Code
                    //
                    e.NewValues["VouchesId!Key"] = VoucherId;

                    ASPxCallbackPanel panel = (ASPxCallbackPanel)gridVoucherAllocation
                                                .FindEditRowCellTemplateControl(
                                                    gridVoucherAllocation.Columns["MasterAccountActor"] as GridViewDataColumn,
                                                        "cpnMasterAcountActor");

                    AccountActorComboBox accountActorComboBox =
                        (AccountActorComboBox)panel.FindControl("accountActorComboBox");

                    WebModule.Accounting.AllocationConfigure.Controls.AccountActor accountActor =
                        accountActorComboBox.GetSelectedItem();

                    uow.CommitChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridVoucherAllocation.CancelEdit();
            }
        }

        protected void gridVoucherAllocation_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("AllocationId!Key"))
            {
                if (e.Value != null)
                {
                    Guid allocationId = (Guid)e.Value;
                    Allocation allocation = session.GetObjectByKey<Allocation>(allocationId);
                    e.DisplayText = allocation.Name;
                }
            }
            else if (e.Column.Name.Equals("MasterAccountActor"))
            {
                //Get name of master account actor
                var obj = gridVoucherAllocation.GetRowValues(e.VisibleRowIndex, "VoucherAllocationId");
                if (obj != null)
                {
                    Guid voucherAllocationId = (Guid)obj;
                    VoucherAllocation voucherAllocation = session.GetObjectByKey<VoucherAllocation>(voucherAllocationId);
                    VoucherAllocationRole voucherAllocationRoleMaster = 
                        voucherAllocation.VoucherAllocationRoles.Where(r => r.IsMaster).FirstOrDefault();
                    if (voucherAllocationRoleMaster == null)
                    {
                        e.DisplayText = String.Empty;
                    }
                    else
                    {
                        VoucherAllocationSubject subject = voucherAllocationRoleMaster.VoucherAllocationSubjectId;
                        e.DisplayText = String.Format("{0}: {1} - {2}",
                            subject.AccountActorTypeId.Name,
                            subject.Code,
                            subject.Name);
                    }
                }
            }
        }

        protected void gridVoucherAllocation_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            gridVoucherAllocation.JSProperties["cpEvent"] = "ListChanged";
        }

        protected void gridVoucherAllocation_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {
            gridVoucherAllocation.JSProperties["cpEvent"] = "ListChanged";
        }

        protected void gridVoucherAllocation_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            gridVoucherAllocation.JSProperties["cpEvent"] = "ListChanged";
        }

        protected void cpnMasterAcountActor_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            RefreshMasterAccountActorComboBox(sender);
        }

        private void RefreshMasterAccountActorComboBox(object sender)
        {
            //Clear selection
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            AccountActorComboBox accountActorComboBox =
                (AccountActorComboBox)panel.FindControl("accountActorComboBox");
            accountActorComboBox.ComboBox.SelectedIndex = -1;

            UpdateMasterAccountActorComboBox(sender);
        }

        private void UpdateMasterAccountActorComboBox(object sender)
        {
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            GridViewEditItemTemplateContainer itemContainer = (GridViewEditItemTemplateContainer)panel.NamingContainer;
            ASPxGridView grid = itemContainer.Grid;
            //Get allocation
            ASPxComboBox allocationComboBox =
                (ASPxComboBox)grid.FindEditRowCellTemplateControl(grid.Columns["Allocation!Key"] as GridViewDataColumn, "cboAllocation");
            if (allocationComboBox.Value == null)
            {
                return;
            }
            Guid allocationId = (Guid)allocationComboBox.Value;
            Allocation allocation = session.GetObjectByKey<Allocation>(allocationId);
            //Get account actor type
            AllocationAccountActorType masterAllocationAccountActorType =
                        allocation.AllocationAccountActorTypes.FirstOrDefault(r => r.IsMaster);
            if (masterAllocationAccountActorType == null)
            {
                throw new Exception("Invalid allocation. Make sure that the allocation has a master account actor type.");
            }
            AccountActorType accountActorType = masterAllocationAccountActorType.AccountActorTypeId;

            AccountActorTypeEnum accountActorTypeEnum =
                        (AccountActorTypeEnum)Enum.Parse(typeof(AccountActorTypeEnum), accountActorType.Code);
            //Get account actor combobox
            AccountActorComboBox accountActorComboBox =
                (AccountActorComboBox)panel.FindControl("accountActorComboBox"); //FindEditRowCellTemplateControl(grid.Columns["MasterAccountActor"] as GridViewDataColumn, "accountActorComboBox");
            //Validation setting
            accountActorComboBox.ComboBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
            accountActorComboBox.ComboBox.ValidationSettings.RequiredField.IsRequired = true;
            accountActorComboBox.ComboBox.ValidationSettings.RequiredField.ErrorText =
                (string)HttpContext.GetGlobalResourceObject("MessageResource", "Msg_Required_Select");
            //Set account actor combobox strategy
            accountActorComboBox.SetAccountActorComboBoxStrategy(
                AccountActorComboBoxStrategyCreators.GetCreator(accountActorTypeEnum).Create());
        }

        protected void cboAllocation_Init(object sender, EventArgs e)
        {
            if (gridVoucherAllocation.IsEditing)
            {
                ASPxComboBox combo = sender as ASPxComboBox;
                comboboxAllocation_DataBind(combo);
            }
        }

        protected void cpnMasterAcountActor_Init(object sender, EventArgs e)
        {
            //UpdateMasterAccountActorComboBox(sender);
        }

        protected void cpnMasterAcountActor_Load(object sender, EventArgs e)
        {
            if (gridVoucherAllocation.IsEditing)
            {
                UpdateMasterAccountActorComboBox(sender);
            }
        }

        protected void gridVoucherAllocation_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //var panel = grid.FindEditRowCellTemplateControl(grid.Columns["MasterAccountActor"] as GridViewDataColumn, "cpnMasterAcountActor");
            //RefreshMasterAccountActorComboBox(panel);
            gridVoucherAllocation.JSProperties["cpEvent"] = "StartRowEditing";
        }
        
    }
}