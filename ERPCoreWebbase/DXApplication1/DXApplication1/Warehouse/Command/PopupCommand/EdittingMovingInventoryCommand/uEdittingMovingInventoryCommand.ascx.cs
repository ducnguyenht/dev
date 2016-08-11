using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Inventory.Command;
using DevExpress.Web.ASPxEditors;
using WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.State;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Command;
using DevExpress.Web.ASPxClasses;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Inventory;
using WebModule.ERPSystem.CustomField.GUI.Control;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using DevExpress.Web.ASPxFormLayout;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.Inventory.Journal;
using NAS.BO.Inventory.Ledger;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Ledger;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand
{
    public partial class uEdittingMovingInventoryCommand : System.Web.UI.UserControl
    {
        #region method

        private void SettingCommonInfoGUI()
        {
            NAS.DAL.Inventory.Command.InventoryCommand OutCommand = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(OutputInventoryCommandId);
            IEnumerable<InventoryJournal> OuputJournals = null;
            if (OutCommand != null && OutCommand.InventoryCommandItemTransactions != null && OutCommand.InventoryCommandItemTransactions.Count > 0)
            {
                OuputJournals = OutCommand.InventoryCommandItemTransactions.SelectMany(r => r.InventoryJournals).
                    Where(r => (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                          r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                          r.Credit > 0 &&
                          r.JournalType.Equals('A') && r.InventoryId != null
                    );

                if (OuputJournals != null && OuputJournals.Count() > 0)
                    cboFromInventory.ReadOnly = true;
                else
                    cboFromInventory.ReadOnly = false;
            }
            else
                cboFromInventory.ReadOnly = false;

            NAS.DAL.Inventory.Command.InventoryCommand InputCommand = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InputInventoryCommandId);
            IEnumerable<InventoryJournal> InputJournals = null;
            if (InputCommand != null && InputCommand.InventoryCommandItemTransactions != null && InputCommand.InventoryCommandItemTransactions.Count > 0)
            {
                InputJournals = InputCommand.InventoryCommandItemTransactions.SelectMany(r => r.InventoryJournals).
                    Where(r => (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                          r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                          r.Credit > 0 &&
                          r.JournalType.Equals('A') && r.InventoryId != null
                    );

                if (InputJournals != null && InputJournals.Count() > 0)
                    cboToInventory.ReadOnly = true;
                else
                    cboToInventory.ReadOnly = false;
            }
            else
                cboToInventory.ReadOnly = false;
        }

        public void initCommonJavascript(string completeEventName)
        {
            SharedClientEvent = completeEventName;
            if (!MainControlClientName.Equals(string.Empty))
            {
                cpInventoryCommand.ClientSideEvents.BeginCallback = "function(s, e){ " +
                    string.Format("{0}.Show();", ldpnInventoryCommand.ClientInstanceName) +
                    " }";

                cpInventoryCommand.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnInventoryCommand.ClientInstanceName) +
                    " if (s.cpIsSave){ delete s.cpIsSave; alert('Đã cập nhật thông tin phiếu kho'); target.fire({ type: '" + "SharedClientEvent" + "' }); }" +
                    " }";

                cpOutputItemUnitBalanceDetail.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnInventoryCommand.ClientInstanceName) +
                " }";

                ButtonSaveCommand.ClientSideEvents.Click =
                    "function(s, e){ var validated1 = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtMovingInventoryCommandCode.ClientID +
                    ".GetMainElement(), null, true); " +
                    "var validated2 = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtOutputInventoryCommandCode.ClientID +
                    ".GetMainElement(), null, true); " +
                    "var validated3 = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtInputInventoryCommandCode.ClientID +
                    ".GetMainElement(), null, true); " +

                    "if (validated1 && validated2 && validated3) {" +
                    string.Format("{0}.PerformCallback('Save');", MainControlClientName)
                    + " } else alert('Vui lòng nhập đầy đủ các thông tin bắt buộc'); }";

                ButtonCloseCommandPopup.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", MainControlClientName)
                    + " }";

                ButtonCloseCommandPopup.CausesValidation = false;

                popupInventoryCommand.ClientSideEvents.Closing = "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", MainControlClientName)
                    + " }";

                grdOutputTransaction.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Refresh();", grdInputTransaction.ClientInstanceName)
                    + " }";

                TabbedLayoutGroup tabbedGrp = frmMovingInventoryCommand.Items[1] as TabbedLayoutGroup;
                tabbedGrp.ClientSideEvents.ActiveTabChanging = "function(s, e){ " +
                    string.Format("{0}.Refresh(); ", grdInputTransaction.ClientInstanceName) +
                    string.Format("{0}.Refresh(); ", grdOutputTransaction.ClientInstanceName) +
                    " }";

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "RegisterStartupScript", "<script>function grdOutputDetailJournal_RowClick(s, e){ " +
                "var params = new Array('RefreshClickExpandFormDetail', s.GetRowKey(e.visibleIndex)); " +
                " s.StartEditRow(e.visibleIndex); " +
                string.Format("{0}.Show();", ldpnInventoryCommand.ClientInstanceName) +
                cpOutputItemUnitBalanceDetail.ClientInstanceName + ".PerformCallback(params); " +
                " }</script>");
            }
        }

        public void SettingInit(ASPxGridView SourceGridView, string NewId, string EditId, string DeleteId, string completeEventName)
        {
            if (!MainControlClientName.Equals(string.Empty))
            {
                SourceGridView.ClientSideEvents.CustomButtonClick = "function(s, e){ " +
                    string.Format("if (e.buttonID == '{0}') ", NewId) + "{ " +
                    string.Format("{0}.PerformCallback('Create');", MainControlClientName) +
                    " } " +
                    string.Format("if (e.buttonID == '{0}') ", EditId) + "{ var key = s.GetRowKey(e.visibleIndex); var params = new Array('Edit', key); " +
                    string.Format("{0}.PerformCallback(params);", MainControlClientName) +
                    " } " +
                    string.Format("if (e.buttonID == '{0}') ", DeleteId) + "{ var r=confirm('Có chắc chắn muốn xóa phiếu kho này?'); if (r == false) return; " +
                    " var key = s.GetRowKey(e.visibleIndex); var params = new Array('Delete', key); " +
                    string.Format("{0}.PerformCallback(params);", MainControlClientName) +
                    " } }";
            }

            initCommonJavascript(completeEventName);
        }

        public void SettingInit(ASPxButton SourceButton, string completeEventName)
        {
            if (!MainControlClientName.Equals(string.Empty) && SourceButton != null)
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Create');", MainControlClientName) +
                    " }";
            }

            initCommonJavascript(completeEventName);
        }

        public void SettingInit(Guid InvoiceId, ASPxButton SourceButton, string completeEventName)
        {
            this.InvoiceId = InvoiceId;
            if (!MainControlClientName.Equals(string.Empty))
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('CreateByBill');", MainControlClientName) +
                    " }";
            }

            initCommonJavascript(completeEventName);
        }
        #endregion


        #region properties

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

        public Guid TransactionId
        {
            get
            {
                return (Guid)Session["TransactionId_" + ClientID + ViewStateControlId];
            }
            set
            {
                Session["TransactionId_" + ClientID + ViewStateControlId] = value;
            }
        }

        public string SharedClientEvent
        {
            get
            {
                if (Session["SharedClientEvent" + ClientID + ViewStateControlId] == null)
                    return null;

                return Session["SharedClientEvent" + ClientID + ViewStateControlId].ToString();
            }
            set
            {
                Session["SharedClientEvent" + ClientID + ViewStateControlId] = value;
            }
        }

        private Guid CurrentItemUnitId
        {
            get
            {
                if (Session["CurrentItemUnitId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["CurrentItemUnitId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["CurrentItemUnitId" + this.ClientID + ViewStateControlId] = value;
            }
        }


        private InventoryCommandBO InventoryCommandBO = new InventoryCommandBO();

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }

        private Guid InventoryCommandId
        {
            get
            {
                if (Session["InventoryCommandId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["InventoryCommandId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["InventoryCommandId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private Guid OutputInventoryCommandId
        {
            get
            {
                if (Session["OutputInventoryCommandId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["OutputInventoryCommandId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["OutputInventoryCommandId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private Guid InputInventoryCommandId
        {
            get
            {
                if (Session["InputInventoryCommandId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["InputInventoryCommandId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["InputInventoryCommandId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private Guid InvoiceId
        {
            get
            {
                if (Session["InvoiceId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["InvoiceId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["InvoiceId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        public string MainControlClientName
        {
            get
            {
                return cpInventoryCommand.ClientInstanceName;
            }
        }

        private ASPxButton ButtonSaveCommand
        {
            get
            {
                ASPxButton button = popupInventoryCommand.FindControl("btnSaveCommand") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonCloseCommandPopup
        {
            get
            {
                ASPxButton button = popupInventoryCommand.FindControl("btnCloseCommandPopup") as ASPxButton;
                return button;
            }
        }

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

        private Session session;

        #endregion


        #region State Pattern

        public void PreCRUD_CreatingMovingInventoryCommandByArtifact()
        {

        }

        public void PreCRUD_CreatingMovingInventoryCommand()
        {

        }

        public void PreCRUD_EdittingMovingInventoryCommand()
        {

        }

        public void CRUD_CreatingMovingInventoryCommand()
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            string errormsg = string.Empty;
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommand command =
                    InventoryCommandBO.CreateInventoryNewCommand(uow, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_MOVE);
                InventoryCommandId = command.InventoryCommandId;
                NAS.DAL.Inventory.Command.InventoryCommand OutputCommand = InventoryCommandBO.CreateInventoryCommandByMovingArtifact(uow, command.InventoryCommandId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                OutputInventoryCommandId = OutputCommand.InventoryCommandId;
                NAS.DAL.Inventory.Command.InventoryCommand InputputCommand = InventoryCommandBO.CreateInventoryCommandByMovingArtifact(uow, command.InventoryCommandId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN);
                InputInventoryCommandId = InputputCommand.InventoryCommandId;

                Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DefaultInventoryEnum.NOT_AVAILABLE);
                InventoryCommandBO.createInventoryCommandItemTransaction(
                    uow,
                    artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_OUTPUT),
                    DateTime.Now,
                    "Phiếu xuất kho nội bộ",
                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT, 
                    OutputInventoryCommandId,
                    cboFromInventory.Value != null ? Guid.Parse(cboFromInventory.Value.ToString()) : naInventory.InventoryId,
                    out errormsg);

                InventoryCommandBO.createInventoryCommandItemTransaction(
                    uow,
                    artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_INPUT),
                    DateTime.Now,
                    "Phiếu nhập kho nội bộ",
                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN, 
                    InputInventoryCommandId,
                    Guid.Empty,
                    out errormsg);

                uow.CommitChanges();

                MovingInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
                OutputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
                InputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();

                OutputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
                InputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();

                MovingInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
                OutputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
                InputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                uow.Dispose();
            }
        }

        public void CRUD_EdittingMovingInventoryCommand()
        {
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            MovingInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();

            XPCollection<NAS.DAL.Inventory.Command.InventoryCommand> childCommands = new
                    XPCollection<NAS.DAL.Inventory.Command.InventoryCommand>(session,
                        CriteriaOperator.And(
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                            new BinaryOperator("ParentInventoryCommandId", command)));

            if (childCommands != null && childCommands.Count > 0)
            {
                bool flgOutCmd = false;
                bool flgIntCmd = false;

                foreach (InventoryCommand childCommand in childCommands)
                {
                    if (childCommand.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                    {
                        OutputInventoryCommandId = childCommand.InventoryCommandId;
                        flgOutCmd = true;
                    }

                    else if (childCommand.CommandType == INVENTORY_COMMAND_TYPE.IN)
                    {
                        InputInventoryCommandId = childCommand.InventoryCommandId;
                        flgIntCmd = true;
                    }

                    UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
                    try
                    {
                        if (!flgOutCmd)
                        {
                            NAS.DAL.Inventory.Command.InventoryCommand OutputCommand = InventoryCommandBO.CreateInventoryCommandByMovingArtifact(uow, command.InventoryCommandId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                            OutputInventoryCommandId = OutputCommand.InventoryCommandId;
                        }

                        if (!flgIntCmd)
                        {
                            NAS.DAL.Inventory.Command.InventoryCommand InputputCommand = InventoryCommandBO.CreateInventoryCommandByMovingArtifact(uow, command.InventoryCommandId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN);
                            InputInventoryCommandId = InputputCommand.InventoryCommandId;
                        }
                        uow.CommitChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally {
                        uow.Dispose();
                    }
                }
            }

            OutputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();
            OutputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();

            MovingInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
            OutputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();
        }

        public void CRUD_ClosingMovingInventoryCommand()
        {
            try
            {
                if (GUIContext.State is CreatingMovingInventoryCommand)
                {
                    session.BeginTransaction();
                    InventoryCommandBO.DeleteLogicInventoryCommand(session, InventoryCommandId);
                    session.CommitTransaction();
                }
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public void CRUD_DeletingMovingInventoryCommand()
        {
            try
            {
                session.BeginTransaction();
                InventoryCommandBO.DeleteLogicInventoryCommand(session, InventoryCommandId);
                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public void CRUD_SavingMovingInventoryCommand()
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                NAS.DAL.Nomenclature.Inventory.Inventory defaultInventory =
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, NAS.DAL.Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);

                if (Guid.Parse(cboFromInventory.Value.ToString()).Equals(defaultInventory.InventoryId))
                    throw new Exception("Vui lòng chọn kho xuất!");

                if (Guid.Parse(cboToInventory.Value.ToString()).Equals(defaultInventory.InventoryId))
                    throw new Exception("Vui lòng chọn kho xuất!");

                MovingInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
    
                OutputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();

                InputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();

                InventoryCommandBO.SaveMovingInventoryCommand(uow,
                    InventoryCommandId,
                    txtMovingInventoryCommandCode.Text.Trim(),
                    txtOutputInventoryCommandIssueDate.Value != null ? DateTime.Parse(txtMovingInventoryCommandIssueDate.Value.ToString()) : DateTime.Now,
                    txtMovingInventoryCommandDescription.Text,
                    OutputInventoryCommandId,
                    txtOutputInventoryCommandCode.Text.Trim(),
                    txtOutputInventoryCommandIssueDate.Value != null ? DateTime.Parse(txtOutputInventoryCommandIssueDate.Value.ToString()) : DateTime.Now,
                    txtOutputInventoryCommandDescription.Text.Trim(),
                    Guid.Parse(cboFromInventory.Value.ToString()),
                    InputInventoryCommandId,
                    txtInputInventoryCommandCode.Text.Trim(),
                    txtInputInventoryCommandIssueDate.Value != null ? DateTime.Parse(txtInputInventoryCommandIssueDate.Value.ToString()) : DateTime.Now,
                    txtInputInventoryCommandDescription.Text.Trim(),
                    Guid.Parse(cboToInventory.Value.ToString()));

                if (cboMovingDirector.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.DIRECTOR,
                        Guid.Parse(cboMovingDirector.Value.ToString()));

                if (cboMovingStoreKeeper.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.STOREKEEPER,
                        Guid.Parse(cboMovingStoreKeeper.Value.ToString()));

                if (cboOutputDirector.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        OutputInventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.DIRECTOR,
                        Guid.Parse(cboOutputDirector.Value.ToString()));

                if (cboOutputStoreKeeper.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        OutputInventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.STOREKEEPER,
                        Guid.Parse(cboOutputStoreKeeper.Value.ToString()));

                if (cboInputDirector.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InputInventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.DIRECTOR,
                        Guid.Parse(cboInputDirector.Value.ToString()));

                if (cboInputStoreKeeper.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InputInventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.STOREKEEPER,
                        Guid.Parse(cboInputStoreKeeper.Value.ToString()));

                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateGUI_CreatingMovingInventoryCommand()
        {
            //grdTransaction.DetailRows.CollapseAllRows();
            txtMovingInventoryCommandCode.IsValid = true;
            txtMovingInventoryCommandCode.Focus();
            txtMovingInventoryCommandIssueDate.IsValid = true;
            txtMovingInventoryCommandDescription.IsValid = true;

            txtOutputInventoryCommandCode.IsValid = true;
            txtOutputInventoryCommandIssueDate.IsValid = true;
            txtOutputInventoryCommandDescription.IsValid = true;

            txtInputInventoryCommandCode.IsValid = true;
            txtInputInventoryCommandIssueDate.IsValid = true;
            txtInputInventoryCommandDescription.IsValid = true;

            TabbedLayoutGroup tabbedGrp = frmMovingInventoryCommand.Items[1] as TabbedLayoutGroup;
            tabbedGrp.ActiveTabIndex = 0;

            popupInventoryCommand.ShowOnPageLoad = true;
            popupInventoryCommand.HeaderText = string.Format("Thông tin phiếu chuyển kho - Thêm mới");

            cboMovingStoreKeeper.Value = null;
            cboMovingDirector.Value = null;

            cboOutputStoreKeeper.Value = null;
            cboOutputDirector.Value = null;

            cboInputStoreKeeper.Value = null;
            cboInputDirector.Value = null;
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_EdittingMovingInventoryCommand()
        {
            //grdTransaction.DetailRows.CollapseAllRows();
            txtMovingInventoryCommandCode.IsValid = true;
            txtMovingInventoryCommandCode.Focus();
            txtMovingInventoryCommandIssueDate.IsValid = true;
            txtMovingInventoryCommandIssueDate.ReadOnly = true;
            txtMovingInventoryCommandDescription.IsValid = true;

            txtMovingInventoryCommandCode.IsValid = true;
            txtMovingInventoryCommandIssueDate.IsValid = true;
            txtMovingInventoryCommandDescription.IsValid = true;

            txtInputInventoryCommandCode.IsValid = true;
            txtInputInventoryCommandIssueDate.IsValid = true;
            txtInputInventoryCommandDescription.IsValid = true;

            TabbedLayoutGroup tabbedGrp = frmMovingInventoryCommand.Items[1] as TabbedLayoutGroup;
            tabbedGrp.ActiveTabIndex = 0;

            popupInventoryCommand.ShowOnPageLoad = true;
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            popupInventoryCommand.HeaderText = string.Format("Thông tin phiếu chuyển kho - {0}", command.Code);

            Person pStoreKeeper = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
            Person pDirector = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.DIRECTOR);

            if (pStoreKeeper == null)
                cboMovingStoreKeeper.Value = null;
            else
                cboMovingStoreKeeper.Value = pStoreKeeper.PersonId;

            if (pDirector == null)
                cboMovingDirector.Value = null;
            else
                cboMovingDirector.Value = pDirector.PersonId;

            pStoreKeeper = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(OutputInventoryCommandId, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
            pDirector = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(OutputInventoryCommandId, DefaultInventoryCommandActorTypeEnum.DIRECTOR);

            if (pStoreKeeper == null)
                cboOutputStoreKeeper.Value = null;
            else
                cboOutputStoreKeeper.Value = pStoreKeeper.PersonId;

            if (pDirector == null)
                cboOutputDirector.Value = null;
            else
                cboOutputDirector.Value = pDirector.PersonId;

            pStoreKeeper = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InputInventoryCommandId, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
            pDirector = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InputInventoryCommandId, DefaultInventoryCommandActorTypeEnum.DIRECTOR);

            if (pStoreKeeper == null)
                cboInputStoreKeeper.Value = null;
            else
                cboInputStoreKeeper.Value = pStoreKeeper.PersonId;

            if (pDirector == null)
                cboInputDirector.Value = null;
            else
                cboInputDirector.Value = pDirector.PersonId;
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_ClosingMovingInventoryCommand()
        {
            popupInventoryCommand.ShowOnPageLoad = false;
        }

        public void UpdateGUI_DeletingMovingInventoryCommand()
        {
            popupInventoryCommand.ShowOnPageLoad = false;
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InputInventoryTransactionXDS.Session = session;
            OutputInventoryTransactionXDS.Session = session;

            InputInventoryJournalXDS.Session = session;
            OutputInventoryJournalXDS.Session = session;

            InputInventoryCommandXDS.Session = session;
            OutputInventoryCommandXDS.Session = session;
            MovingInventoryCommandXDS.Session = session;

            MovingInventoryCommandXDS.Session = session;
            OutputInventoryCommandXDS.Session = session;
            InputInventoryCommandXDS.Session = session;

            MovingInventoryCommandActorXDS.Session = session;
            OutputInventoryCommandActorXDS.Session = session;
            InputInventoryCommandActorXDS.Session = session;

            FromInventoryXDS.Session = session;
            ToInventoryXDS.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                IsRaiseDataUpdated = false;
            }

            if (!IsPostBack)
            {
                //grdTransaction.ClientInstanceName = string.Format("grdTransaction{0}", ViewStateControlId);
                grdInputTransaction.ClientInstanceName = string.Format("grdInputTransaction{0}", ViewStateControlId);
                grdOutputTransaction.ClientInstanceName = string.Format("grdOutputTransaction{0}", ViewStateControlId);
                popupInventoryCommand.ClientInstanceName = string.Format("popupInventoryCommand{0}", ViewStateControlId);
                cpInventoryCommand.ClientInstanceName = string.Format("cpInventoryCommand{0}", ViewStateControlId);
                cpOutputItemUnitBalanceDetail.ClientInstanceName = string.Format("cpOutputItemUnitBalanceDetail{0}", ViewStateControlId);
                ldpnInventoryCommand.ClientInstanceName = string.Format("ldpnInventoryCommand{0}", ViewStateControlId);
                GUIContext = new NAS.GUI.Pattern.Context();
            }
            TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
            OutputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();
            OutputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();

            MovingInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
            OutputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = OutputInventoryCommandId.ToString();
            InputInventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InputInventoryCommandId.ToString();
        }

        protected void cpOutputItemUnitBalanceDetail_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            InventoryLedgerBO LedgerBO = new InventoryLedgerBO();
            COGSBO CogsBO = new COGSBO();
            CurrencyBO currencyBO = new CurrencyBO();
            COGS Cogs = null;
            double newestBalance = 0;

            string[] para = e.Parameter.Split(',');
            string trs = para[0];

            switch (trs)
            {
                case "RefreshClickExpandFormDetail":
                    InventoryJournal journal = session.GetObjectByKey<InventoryJournal>(Guid.Parse(para[1]));
                    if (journal != null)
                    {
                        newestBalance = LedgerBO.GetItemUnitBalance(session,
                            journal.ItemUnitId.ItemUnitId,
                            Guid.Parse(cboFromInventory.Value.ToString()));

                        Cogs = CogsBO.GetLastCOGS(session,
                            journal.ItemUnitId.ItemUnitId,
                            currencyBO.GetDefaultCurrency(session).CurrencyId,
                            Guid.Parse(cboFromInventory.Value.ToString()));

                        lblOutputManufacturer.Text = journal.ItemUnitId.ItemId.ManufacturerName;;
                    }
                    break;
                case "RefreshInlineExpandFormDetail":
                    ItemUnit itemunit = session.GetObjectByKey<ItemUnit>(Guid.Parse(para[1]));
                    if (itemunit != null)
                    {
                        newestBalance = LedgerBO.GetItemUnitBalance(session,
                                itemunit.ItemUnitId,
                                Guid.Parse(cboFromInventory.Value.ToString()));

                        Cogs = CogsBO.GetLastCOGS(session,
                            itemunit.ItemUnitId,
                            currencyBO.GetDefaultCurrency(session).CurrencyId,
                            Guid.Parse(cboFromInventory.Value.ToString()));

                        lblOutputManufacturer.Text = itemunit.ItemId.ManufacturerName;
                    }
                    break;
            }

            if (Cogs == null)
                lblOutputPrice.Text = "0 VNĐ";
            else if (Cogs.COGSPrice == 0)
                lblOutputPrice.Text = "0 VNĐ";
            else
                lblOutputPrice.Text = string.Format("{0:#,###} VNĐ", Cogs.COGSPrice);

            if (newestBalance == 0)
                lblOutputBalance.Text = "0";
            else
                lblOutputBalance.Text = string.Format("{0:#,###}", newestBalance);
        }

        protected void cpInventoryCommand_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];

            switch (trs)
            {
                case "Create":
                    GUIContext.State = new CreatingMovingInventoryCommand(this);
                    break;
                case "Edit":
                    InventoryCommandId = Guid.Parse(para[1]);
                    GUIContext.State = new WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.State.EdittingMovingInventoryCommand(this);
                    break;
                case "Delete":
                    InventoryCommandId = Guid.Parse(para[1]);
                    GUIContext.State = new DeletingMovingInventoryCommand(this);
                    break;
                default:
                    GUIContext.Request(trs, this);
                    break;
            }

            if (trs.Equals("Save") || trs.Equals("Delete"))
                cpInventoryCommand.JSProperties.Add("cpIsSave", true);
        }

        protected void grdOutputTransaction_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            InventoryCommandBO.DeleteLogicInventoryTransaction(session, Guid.Parse(e.Keys[0].ToString()));
        }

        protected void grdOutputTransaction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //e.Cancel = true;
            //UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            //try
            //{
            //    InventoryCommandBO.createInventoryCommandItemTransaction(
            //        uow,
            //        e.NewValues["Code"].ToString(),
            //        DateTime.Parse(e.NewValues["IssueDate"].ToString()),
            //        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : string.Empty,
            //        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT, 
            //        OutputInventoryCommandId,);

            //    uow.CommitChanges();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    uow.Dispose();
            //    grdOutputTransaction.CancelEdit();
            //}
        }

        protected void grdOutputTransaction_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            InventoryCommand command = session.GetObjectByKey<InventoryCommand>(OutputInventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");
            if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");
            Guid key = Guid.Parse(e.Keys[0].ToString());
            NAS.DAL.Inventory.Command.InventoryCommandItemTransaction transaction =
                session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>(key);
            transaction.Code = e.NewValues["Code"].ToString();
            transaction.IssueDate = DateTime.Parse(e.NewValues["IssueDate"].ToString());
            if (e.NewValues["Description"] != null)
                transaction.Description = e.NewValues["Description"].ToString();
            transaction.Save();
            grdOutputTransaction.CancelEdit();
        }

        protected void grdOutputDetailJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdOutputDetailJournal = sender as ASPxGridView;
            object MasterKey = grdOutputDetailJournal.GetMasterRowKeyValue();
            if (MasterKey == null)
                return;
            OutputInventoryJournalXDS.CriteriaParameters["InventoryTransactionId"].DefaultValue = MasterKey.ToString();
        }

        protected void grdOutputDetailJournal_Init(object sender, EventArgs e)
        {
            ASPxGridView grdOutputDetailJournal = sender as ASPxGridView;
            grdOutputDetailJournal.DataSourceID = "OutputInventoryJournalXDS";
            grdOutputDetailJournal.ClientInstanceName = string.Format("grdOutputDetailJournal{0}", ViewStateControlId);
        }

        protected void grdOutputDetailJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdOutputDetailJournal = sender as ASPxGridView;
            try
            {
                session.BeginTransaction();
                InventoryCommandBO.deleteDoubleInventoryJournal(session, Guid.Parse(e.Keys[0].ToString()));
                InventoryJournal InputInventoryJournal = InventoryCommandBO.searchReleventMovingInventoryJournal(
                    Guid.Parse(e.Keys[0].ToString()), 
                    InputInventoryCommandId);
                InventoryCommandBO.deleteDoubleInventoryJournal(session, InputInventoryJournal.InventoryJournalId);
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
            finally
            {
                grdOutputDetailJournal.CancelEdit();
            }
        }

        protected void grdOutputDetailJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdOutputDetailJournal = sender as ASPxGridView;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                Guid _InventoryId = Guid.Empty;
                //Non-Persistent InventoryCommand
                //_InventoryId = Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString());
                _InventoryId = Guid.Parse(cboFromInventory.Value.ToString());
                InventoryJournal outputJournal = InventoryCommandBO.createDoubleInventoryJournalForInOutMovingTransaction(
                    uow,
                    Guid.Parse(grdOutputDetailJournal.GetMasterRowKeyValue().ToString()),
                    _InventoryId,
                    Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    double.Parse(e.NewValues["PlanCredit"].ToString()),
                    double.Parse(e.NewValues["Credit"].ToString()),
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty,
                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);

                InventoryCommandBO.AutoGenerateInventoryJournalForInputCommand_MovingCase(uow, outputJournal.InventoryJournalId, InputInventoryCommandId);
                
                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uow.Dispose();
                grdOutputDetailJournal.CancelEdit();
            }
        }

        protected void grdOutputDetailJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            ///////Combobox hàng hóa//////////////
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox comboItemUnit = e.Editor as ASPxComboBox;

                if (comboItemUnit != null)
                {
                    comboItemUnit.TabIndex = 1;
                    comboItemUnit.Focus();
                    comboItemUnit.ClientSideEvents.SelectedIndexChanged = "function(s,e){ " +
                        string.Format("if ({0}.GetEditor('LotId!Key').InCallback() )", gridview.ClientInstanceName) +
                       " lastItemUnitJournal = s.GetValue().toString(); " +
                       "else " +
                       string.Format("{0}.GetEditor('LotId!Key').PerformCallback(s.GetValue().toString());", gridview.ClientInstanceName) +
                       "}";

                    comboItemUnit.ClientSideEvents.ValueChanged = "function(s,e){ " +
                        gridview.ClientInstanceName + ".GetEditor('ExpireDate').SetDate(null); " +
                        gridview.ClientInstanceName + ".GetEditor('ItemUnitId.ItemId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.Name')); " +
                        gridview.ClientInstanceName + ".GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name')); " +
                        string.Format("{0}.Show();", ldpnInventoryCommand.ClientInstanceName) +
                        "var value = s.GetValue(); " +
                        "var params = new Array('RefreshInlineExpandFormDetail', value); " +
                        cpOutputItemUnitBalanceDetail.ClientInstanceName + ".PerformCallback(params); " +
                        "}";

                    comboItemUnit.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(comboItemUnit_ItemsRequestedByFilterCondition);
                    comboItemUnit.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(comboItemUnit_ItemRequestedByValue);
                    if (!gridview.IsNewRowEditing)
                    {
                        Guid itemUnitId = (Guid)e.Value;
                        comboItemUnit.Focus();
                    }

                    if (comboItemUnit.Value != null && !Guid.Parse(comboItemUnit.Value.ToString()).Equals(Guid.Empty))
                    {
                        CurrentItemUnitId = Guid.Parse(comboItemUnit.Value.ToString());
                        XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(session);
                        CriteriaOperator criteria = new BinaryOperator("ItemUnitId", CurrentItemUnitId, BinaryOperatorType.Equal);
                        collection.Criteria = criteria;
                        comboItemUnit.DataSource = collection;
                        comboItemUnit.DataBindItems();
                    }
                }
            }

            ///////spin kế hoạch chuyển//////////////
            if (e.Column.FieldName == "PlanCredit")
            {
                ASPxSpinEdit spinPlanCredit = e.Editor as ASPxSpinEdit;

                if (spinPlanCredit != null)
                {
                    spinPlanCredit.TabIndex = 2;
                    spinPlanCredit.MinValue = 0;
                    spinPlanCredit.MaxValue = decimal.MaxValue;
                }
            }

            ///////spin thực chuyển//////////////
            if (e.Column.FieldName == "Credit")
            {
                ASPxSpinEdit spinCredit = e.Editor as ASPxSpinEdit;

                if (spinCredit != null)
                {
                    spinCredit.TabIndex = 3;
                    spinCredit.MinValue = 0;
                    spinCredit.MaxValue = decimal.MaxValue;
                }
            }

            ///////Combobox lô//////////////
            if (e.Column.FieldName == "LotId!Key")
            {
                ASPxComboBox comboLots = e.Editor as ASPxComboBox;

                if (comboLots != null)
                {
                    comboLots.TabIndex = 4;
                    comboLots.ClientSideEvents.EndCallback = "function(s,e){ " +
                        "if(lastItemUnitJournal){ " +
                        string.Format("{0}.GetEditor('LotId!Key').PerformCallback(lastItemUnitJournal); lastItemUnitJournal = null; ",
                            gridview.ClientInstanceName)
                        + "}}";

                    comboLots.ClientSideEvents.ValueChanged = "function(s,e){ var date = Date.parseLocale(s.GetSelectedItem().GetColumnText('ExpireDate').split(' ')[0], 'dd/MM/yyyy'); " +
                        gridview.ClientInstanceName + ".GetEditor('ExpireDate').SetDate(date); }";

                    comboLots.Callback += new CallbackEventHandlerBase(comboLots_OnCallback);
                    comboLots.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(comboLots_ItemsRequestedByFilterCondition);
                    comboLots.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(comboLots_ItemRequestedByValue);

                    if (comboLots.Value != null && !Guid.Parse(comboLots.Value.ToString()).Equals(Guid.Empty))
                    {
                        loadCBOLot(CurrentItemUnitId, comboLots);
                    }
                }
            }

            ///////Combobox chọn kho đi-đến//////////////
            if (e.Column.FieldName == "FromInventoryId!Key")
            {
                ASPxComboBox comboInventory = e.Editor as ASPxComboBox;

                if (comboInventory != null)
                {
                    Inventory firstInventory = InventoryCommandBO.getFirstInventoryJournalForOutputCommand(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
                    if (firstInventory != null && gridview.IsNewRowEditing)
                    {
                        comboInventory.Value = firstInventory.InventoryId;
                        comboInventory.ReadOnly = true;
                    }

                    XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory> collection = new XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory>(
                        session,
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                    comboInventory.DataSource = collection;
                    comboInventory.DataBind();
                }
            }

            if (e.Column.FieldName == "Description")
            {
                ASPxTextBox txtDescription = e.Editor as ASPxTextBox;
                txtDescription.TabIndex = 5;
            }
        }

        void comboItemUnit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(uow);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = null;

            Bill billArtifact = InventoryCommandBO.GetSourceArtifactFromInventoryCommand(uow, InventoryCommandId);

            if (billArtifact != null && billArtifact is SalesInvoice)
            {
                IEnumerable<ItemUnit> billItem = billArtifact.BillItems.Select(b => b.ItemUnitId);

                criteria = CriteriaOperator.And(
                    new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
                        "PRODUCT")),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                    CriteriaOperator.Or(
                    new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("ItemId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)),
                    new InOperator("ItemUnitId", billItem));
            }
            else
            {
                criteria = CriteriaOperator.And(
                    new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
                        "PRODUCT")),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                    new ContainsOperator("ItemId.itemUnitTypeConfigs", new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)),
                    CriteriaOperator.Or(
                        new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("ItemId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)));
            }

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        void comboItemUnit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            ItemUnit obj = session.GetObjectByKey<ItemUnit>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new ItemUnit[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        void comboLots_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(CurrentItemUnitId);
            if (itemUnit == null)
                return;

            ASPxComboBox cbo = source as ASPxComboBox;
            CriteriaOperator criteria =
                CriteriaOperator.Or(
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemId!Key", itemUnit.ItemId.ItemId, BinaryOperatorType.Equal),
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal));

            XPCollection<NAS.DAL.Inventory.Lot.Lot> collection = new XPCollection<NAS.DAL.Inventory.Lot.Lot>(session);
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            cbo.DataSource = collection;
            cbo.DataBind();
        }

        void comboLots_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            NAS.DAL.Inventory.Lot.Lot obj = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new NAS.DAL.Inventory.Lot.Lot[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        void comboLots_OnCallback(object source, CallbackEventArgsBase e)
        {
            ASPxComboBox comboLots = source as ASPxComboBox;
            loadCBOLot(Guid.Parse(e.Parameter.ToString()), comboLots);
            CurrentItemUnitId = Guid.Parse(e.Parameter.ToString());
        }

        private void loadCBOLot(Guid ItemUnitId, ASPxComboBox cbo)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(ItemUnitId);
            CriteriaOperator criteria =
                CriteriaOperator.Or(
                    CriteriaOperator.And(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemId!Key", itemUnit.ItemId.ItemId, BinaryOperatorType.Equal)),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT, BinaryOperatorType.Equal));

            XPCollection<NAS.DAL.Inventory.Lot.Lot> collection = new XPCollection<NAS.DAL.Inventory.Lot.Lot>(session);
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            cbo.DataSource = collection;
            cbo.DataBind();
        }

        protected void grdOutputDetailJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName.Equals("ItemUnitId!Key"))
            {
                ItemUnit IU = session.GetObjectByKey<ItemUnit>(Guid.Parse(e.Value.ToString()));
                e.DisplayText = IU.ItemId.Code;
            }

            if (e.Column.FieldName.Equals("LotId!Key"))
            {
                NAS.DAL.Inventory.Lot.Lot LOT = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(Guid.Parse(e.Value.ToString()));
                e.DisplayText = LOT.Code;
            }

            if (e.Column.FieldName.Equals("FromInventoryId!Key"))
            {
                NAS.DAL.Nomenclature.Inventory.Inventory INV = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(Guid.Parse(e.Value.ToString()));
                if (INV != null)
                    e.DisplayText = INV.Name;
            }
        }

        protected void grdOutputDetailJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                InventoryCommand InputInventoryCommand = uow.GetObjectByKey<InventoryCommand>(InputInventoryCommandId);
                if (InputInventoryCommand == null)
                    throw new Exception("The InventoryCommand is not exist in system");
                InventoryJournal SourceOutputInventoryJournal = uow.GetObjectByKey<InventoryJournal>(Guid.Parse(e.Keys[0].ToString()));

                InventoryJournal InputInventoryJournal = InventoryCommandBO.searchReleventMovingInventoryJournal(Guid.Parse(e.Keys[0].ToString()), InputInventoryCommandId);
                InventoryJournal relevantInputInventoryJournal = InventoryCommandBO.searchRelevantInventoryJournal(uow, InputInventoryJournal.InventoryJournalId);
                
                if (InputInventoryJournal == null)
                    throw new Exception("The Input InventoryJournal is not exist in system");

                InventoryCommandBO.UpdateDoubleInventoryJournal(
                    uow,
                    InputInventoryJournal.InventoryJournalId,
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.TRANSITINVENTORY).InventoryId,
                    relevantInputInventoryJournal.InventoryId.InventoryId,
                    Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    double.Parse(e.NewValues["PlanCredit"].ToString()),
                    InputInventoryJournal.Credit,
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty);

                uow.FlushChanges();
                
                InventoryCommandBO.UpdateDoubleInventoryJournal(
                    uow,
                    Guid.Parse(e.Keys[0].ToString()),
                    //Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString()),
                    Guid.Parse(cboFromInventory.Value.ToString()),
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.TRANSITINVENTORY).InventoryId,
                    Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    double.Parse(e.NewValues["PlanCredit"].ToString()),
                    double.Parse(e.NewValues["Credit"].ToString()),
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty);

                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uow.Dispose();
                grdDetailJournal.CancelEdit();
            }
        }

        protected void grdOutputDetailJournal_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //if (e.NewValues["FromInventoryId!Key"].ToString().Equals(e.NewValues["FromInventoryId!Key"].ToString()))
            //    throw new Exception("'Từ kho' và 'Đến kho' không thể trùng nhau");

            //if (double.Parse(e.NewValues["PlanCredit"].ToString()) < (double.Parse(e.NewValues["Credit"].ToString())))
            //    throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn 'Số lượng thực tế'");

            if (double.Parse(e.NewValues["PlanCredit"].ToString()) <= 0)
                throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn hoặc bằng 0");

            if (double.Parse(e.NewValues["Credit"].ToString()) <= 0)
                throw new Exception("'Số lượng thực tế' không thể nhỏ hơn hoặc bằng 0");

            try
            {
                InventoryCommand inputCommand = session.GetObjectByKey<InventoryCommand>(InputInventoryCommandId);
                Inventory inputInventory = InventoryCommandBO.getFirstInventoryJournalForInputCommand(
                    session,
                    inputCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId);

                if (inputInventory == null)
                    return;

                Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.NOT_AVAILABLE);

                //Guid newInventoryId = Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString());
                Guid newInventoryId = Guid.Parse(cboFromInventory.Value.ToString());
                if (newInventoryId.Equals(inputInventory.InventoryId) && !newInventoryId.Equals(naInventory.InventoryId))
                {
                    throw new Exception(string.Format("Kho '{0}' đã được chọn làm kho nhập! Vui lòng chọn kho khác", inputInventory.Code));
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (cboFromInventory.Value != null)
            {
                InventoryLedgerBO LedgerBO = new InventoryLedgerBO();
                double newestBalance = LedgerBO.GetItemUnitBalance(session,
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    Guid.Parse(cboFromInventory.Value.ToString()));

                if (newestBalance < double.Parse(e.NewValues["Credit"].ToString()))
                    throw new Exception(string.Format("Số lượng tồn kho là {0}, nên không đủ để xuất hàng ",
                        newestBalance.ToString()));
            }
        }

        protected void cpnTransactionAllocationObjects_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];

            #region Transaction allocation
            if (command.Equals("InputAllocateTransaction") || command.Equals("OutputAllocateTransaction"))
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

                if (command.Equals("OutputAllocateTransaction"))
                {
                    TransactionId = (Guid)grdOutputTransaction.GetRowValues(visibleIndex, "InventoryTransactionId");

                    NASCustomFieldDataGridView.CMSObjectId =
                        InventoryCommandBO.GetCMSTransaction<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>
                        (TransactionId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT).ObjectId;
                }
                else if (command.Equals("InputAllocateTransaction")) {
                    TransactionId = (Guid)grdInputTransaction.GetRowValues(visibleIndex, "InventoryTransactionId");

                    NASCustomFieldDataGridView.CMSObjectId =
                        InventoryCommandBO.GetCMSTransaction<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>
                        (TransactionId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN).ObjectId;
                }
                NASCustomFieldDataGridView.DataBind();
                IsRaiseDataUpdated = true;
                TransactionCustomFieldDataGridView_BindDataUpdatedEvent();
            }
            #endregion

            #region GeneralJournal allocation
            else if (command.Equals("AllocateJournal"))
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
                IsRaiseDataUpdated = false;
                NASCustomFieldDataGridView.CMSObjectId
                    = InventoryCommandBO.GetCMSInventoryJournal(generalJournalId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT).ObjectId;
                NASCustomFieldDataGridView.DataBind();
            }
            #endregion
        }

        private void TransactionCustomFieldDataGridView_BindDataUpdatedEvent()
        {
            if (!IsRaiseDataUpdated)
            {
                NASCustomFieldDataGridView.DataUpdated -=
                    new CustomFieldControlDataUpdatedEventHandler(customFieldDataGridView_DataUpdated);
            }
            else
            {
                NASCustomFieldDataGridView.DataUpdated +=
                    new CustomFieldControlDataUpdatedEventHandler(customFieldDataGridView_DataUpdated);
            }
        }

        protected void customFieldDataGridView_DataUpdated(object sender,
            ERPSystem.CustomField.GUI.Control.CustomFieldControlEventArgs args)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                IEnumerable<NAS.DAL.CMS.ObjectDocument.Object> cmsObjects =
                     InventoryCommandBO.GetRelatedCMSObjectWithInventoryCommandItemTransaction(uow, TransactionId);

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

        protected void grdInputTransaction_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                session.BeginTransaction();
                InventoryCommandBO.DeleteLogicInventoryTransaction(session, Guid.Parse(e.Keys[0].ToString()));
                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void grdInputTransaction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //e.Cancel = true;
            //UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            //try
            //{
            //    InventoryCommandBO.createInventoryCommandItemTransaction(
            //        uow,
            //        e.NewValues["Code"].ToString(),
            //        DateTime.Parse(e.NewValues["IssueDate"].ToString()),
            //        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : string.Empty,
            //        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN, InputInventoryCommandId);

            //    uow.CommitChanges();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    uow.Dispose();
            //    grdInputTransaction.CancelEdit();
            //}
        }

        protected void grdInputTransaction_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;

            InventoryCommand command = session.GetObjectByKey<InventoryCommand>(InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");
            if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            Guid key = Guid.Parse(e.Keys[0].ToString());
            NAS.DAL.Inventory.Command.InventoryCommandItemTransaction transaction =
                session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>(key);

            transaction.Code = e.NewValues["Code"].ToString();
            transaction.IssueDate = DateTime.Parse(e.NewValues["IssueDate"].ToString());
            if (e.NewValues["Description"] != null)
                transaction.Description = e.NewValues["Description"].ToString();
            transaction.Save();
            grdInputTransaction.CancelEdit();
        }

        protected void grdInputDetailJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdInputDetailJournal = sender as ASPxGridView;
            object MasterKey = grdInputDetailJournal.GetMasterRowKeyValue();
            if (MasterKey == null)
                return;
            InputInventoryJournalXDS.CriteriaParameters["InventoryTransactionId"].DefaultValue = MasterKey.ToString();
        }

        protected void grdInputDetailJournal_Init(object sender, EventArgs e)
        {
            ASPxGridView grdInputDetailJournal = sender as ASPxGridView;
            grdInputDetailJournal.DataSourceID = "InputInventoryJournalXDS";
            grdInputDetailJournal.ClientInstanceName = string.Format("grdInputDetailJournal{0}", ViewStateControlId);
        }

        protected void grdInputDetailJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdInputDetailJournal = sender as ASPxGridView;
            try
            {
                session.BeginTransaction();
                InventoryCommandBO.deleteDoubleInventoryJournal(session, Guid.Parse(e.Keys[0].ToString()));
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
            finally
            {
                grdInputDetailJournal.CancelEdit();
            }
        }

        protected void grdInputDetailJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdInputDetailJournal = sender as ASPxGridView;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                Guid _InventoryId = Guid.Empty;
                //_InventoryId = Guid.Parse(e.NewValues["ToInventoryId!Key"].ToString());
                _InventoryId = Guid.Parse(cboToInventory.Value.ToString());
                InventoryCommandBO.createDoubleInventoryJournalForInOutMovingTransaction(
                    uow,
                    Guid.Parse(grdInputDetailJournal.GetMasterRowKeyValue().ToString()),
                    _InventoryId,
                    Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    double.Parse(e.NewValues["PlanCredit"].ToString()),
                    double.Parse(e.NewValues["Credit"].ToString()),
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty,
                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN);
                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uow.Dispose();
                grdInputDetailJournal.CancelEdit();
            }
        }

        protected void grdInputDetailJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            /////Combobox hàng hóa//////////////
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox comboItemUnit = e.Editor as ASPxComboBox;

                if (comboItemUnit != null)
                {
                    comboItemUnit.ClientSideEvents.SelectedIndexChanged = "function(s,e){ " +
                        string.Format("if ({0}.GetEditor('LotId!Key').InCallback() )", gridview.ClientInstanceName) +
                       " lastItemUnitJournal = s.GetValue().toString(); " +
                       "else " +
                       string.Format("{0}.GetEditor('LotId!Key').PerformCallback(s.GetValue().toString());", gridview.ClientInstanceName) +
                       "}";

                    comboItemUnit.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                               gridview.ClientInstanceName + ".GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name'));}";

                    comboItemUnit.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(comboItemUnit_ItemsRequestedByFilterCondition);
                    comboItemUnit.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(comboItemUnit_ItemRequestedByValue);
                    if (!gridview.IsNewRowEditing)
                    {
                        Guid itemUnitId = (Guid)e.Value;
                        comboItemUnit.Focus();
                    }

                    if (comboItemUnit.Value != null && !Guid.Parse(comboItemUnit.Value.ToString()).Equals(Guid.Empty))
                    {
                        CurrentItemUnitId = Guid.Parse(comboItemUnit.Value.ToString());
                        XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(session);
                        CriteriaOperator criteria = new BinaryOperator("ItemUnitId", CurrentItemUnitId, BinaryOperatorType.Equal);
                        collection.Criteria = criteria;
                        comboItemUnit.DataSource = collection;
                        comboItemUnit.DataBindItems();
                    }
                }
            }

            ///////Textbox thực nhận//////////////
            if (e.Column.FieldName == "Credit")
            {
                ASPxSpinEdit spinCredit = e.Editor as ASPxSpinEdit;

                if (spinCredit != null)
                {
                    spinCredit.TabIndex = 1;
                    spinCredit.Focus();
                    spinCredit.MinValue = 0;
                    spinCredit.MaxValue = decimal.MaxValue;
                }
            }

            ///////Combobox lô//////////////
            if (e.Column.FieldName == "LotId!Key")
            {
                ASPxComboBox comboLots = e.Editor as ASPxComboBox;

                if (comboLots != null)
                {
                    comboLots.ClientSideEvents.EndCallback = "function(s,e){ " +
                        "if(lastItemUnitJournal){ " +
                        string.Format("{0}.GetEditor('LotId!Key').PerformCallback(lastItemUnitJournal); lastItemUnitJournal = null; ",
                            gridview.ClientInstanceName)
                        + "}}";

                    comboLots.Callback += new CallbackEventHandlerBase(comboLots_OnCallback);
                    comboLots.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(comboLots_ItemsRequestedByFilterCondition);
                    comboLots.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(comboLots_ItemRequestedByValue);

                    if (comboLots.Value != null && !Guid.Parse(comboLots.Value.ToString()).Equals(Guid.Empty))
                    {
                        loadCBOLot(CurrentItemUnitId, comboLots);
                    }
                }
            }

            ///////Combobox chọn kho đi-đến//////////////
            if (e.Column.FieldName == "ToInventoryId!Key")
            {
                ASPxComboBox comboInventory = e.Editor as ASPxComboBox;

                if (comboInventory != null)
                {
                    XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory> collection = new XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory>(
                        session,
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                    comboInventory.DataSource = collection;
                    comboInventory.DataBind();
                }
            }

            if (e.Column.FieldName == "Description")
            {
                ASPxTextBox txtDescription = e.Editor as ASPxTextBox;
                txtDescription.TabIndex = 2;
            }
        }

        protected void grdInputDetailJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Value == null)
                return;
            if (e.Column.FieldName.Equals("ItemUnitId!Key"))
            {
                ItemUnit IU = session.GetObjectByKey<ItemUnit>(Guid.Parse(e.Value.ToString()));
                e.DisplayText = IU.ItemId.Code;
            }

            if (e.Column.FieldName.Equals("LotId!Key"))
            {
                NAS.DAL.Inventory.Lot.Lot LOT = session.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(Guid.Parse(e.Value.ToString()));
                e.DisplayText = LOT.Code;
            }

            if (e.Column.FieldName.Equals("ToInventoryId!Key"))
            {
                NAS.DAL.Nomenclature.Inventory.Inventory INV = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(Guid.Parse(e.Value.ToString()));
                if (INV != null)
                    e.DisplayText = INV.Name;
            }
        }

        protected void grdInputDetailJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                InventoryCommandBO.UpdateDoubleInventoryJournal(
                    uow,
                    Guid.Parse(e.Keys[0].ToString()),
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.TRANSITINVENTORY).InventoryId,
                    //Guid.Parse(e.NewValues["ToInventoryId!Key"].ToString()),
                    Guid.Parse(cboToInventory.Value.ToString()),
                    Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                    double.Parse(e.NewValues["PlanCredit"].ToString()),
                    double.Parse(e.NewValues["Credit"].ToString()),
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty);

                //InventoryJournal CreditJournal = uow.GetObjectByKey<InventoryJournal>(Guid.Parse(e.Keys[0].ToString()));

                //foreach (InventoryJournal journal in CreditJournal.InventoryTransactionId.InventoryJournals)
                //{
                //    if (journal != CreditJournal &&
                //        journal.JournalType == 'A' && journal.Debit > 0 && journal.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                //    {
                //        Inventory newInventory = uow.GetObjectByKey<Inventory>(Guid.Parse(e.NewValues["ToInventoryId!Key"].ToString()));
                //        journal.InventoryId = newInventory;
                //    }
                //}

                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                uow.Dispose();
                grdDetailJournal.CancelEdit();
            }
        }

        protected void grdInputDetailJournal_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (double.Parse(e.NewValues["PlanCredit"].ToString()) < (double.Parse(e.NewValues["Credit"].ToString())))
                throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn 'Số lượng thực tế'");

            if (double.Parse(e.NewValues["PlanCredit"].ToString()) < 0)
                throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn hoặc bằng 0");

            if (double.Parse(e.NewValues["Credit"].ToString()) < 0)
                throw new Exception("'Số lượng thực tế' không thể nhỏ hơn hoặc bằng 0");

            try{
                InventoryCommand outputCommand = session.GetObjectByKey<InventoryCommand>(OutputInventoryCommandId);
                Inventory OutInventory = InventoryCommandBO.getFirstInventoryJournalForOutputCommand(
                    session, 
                    outputCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId);

                Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.NOT_AVAILABLE);
                //Guid newInventoryId = Guid.Parse(e.NewValues["ToInventoryId!Key"].ToString());
                Guid  newInventoryId = Guid.Parse(cboToInventory.Value.ToString());
                if (newInventoryId.Equals(OutInventory.InventoryId) && !newInventoryId.Equals(naInventory.InventoryId))
                {
                    throw new Exception(string.Format("Kho '{0}' đã được chọn làm kho xuất! Vui lòng chọn kho khác", OutInventory.Code));
                }

            } catch(Exception)
            {
                throw;
            }

        }

        protected void colPersonOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = 0;
            collection.TopReturnedObjects = 10;

            CriteriaOperator criteria = new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);

            collection.Criteria = criteria;
            Person obj = session.GetObjectByKey<Person>(Guid.Parse(e.Value.ToString()));
            collection.Add(obj);
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void colPersonOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);


            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void grdOutputTransaction_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            if (e.Column.FieldName == "Code")
            {
                ASPxTextBox txtCode = e.Editor as ASPxTextBox;
                if (txtCode != null && txtCode.Text != null && txtCode.Text.Equals(string.Empty))
                {
                    txtCode.ReadOnly = true;
                    txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_OUTPUT);
                }
            }

            if (e.Column.FieldName == "IssueDate")
            {
                ASPxDateEdit date = e.Editor as ASPxDateEdit;

                if (date != null)
                {
                    date.Focus();
                }
            }
        }

        protected void grdInputTransaction_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            if (e.Column.FieldName == "Code")
            {
                ASPxTextBox txtCode = e.Editor as ASPxTextBox;

                if (txtCode != null && txtCode.Text != null && txtCode.Text.Equals(string.Empty))
                {
                    txtCode.ReadOnly = true;
                    txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_INPUT);
                }
            }

            if (e.Column.FieldName == "IssueDate")
            {
                ASPxDateEdit date = e.Editor as ASPxDateEdit;

                if (date != null)
                {
                    date.Focus();
                }
            }
        }

        protected void btnAddLot_Load(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            button.ClientVisible = false;
            //uAddNewLotsToItem1.SettingInit(button);
            //DevExpress.Web.ASPxGridView.ASPxGridView grd = (button.NamingContainer as
            //    DevExpress.Web.ASPxGridView.GridViewHeaderTemplateContainer).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as
            //    DevExpress.Web.ASPxGridView.ASPxGridView;

            //if (grd == null)
            //    return;
            //if (grd.EditingRowVisibleIndex >= 0)
            //{
            //    button.ClientVisible = true;
            //}
            //else
            //    button.ClientVisible = false;
        }



    }
}