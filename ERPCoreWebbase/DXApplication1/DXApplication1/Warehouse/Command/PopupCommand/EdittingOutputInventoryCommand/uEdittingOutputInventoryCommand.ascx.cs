using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.Inventory.Command;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using NAS.DAL.Nomenclature.Item;
using WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.State;
using WebModule.ERPSystem.CustomField.GUI.Control;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.Inventory.Journal;
using NAS.ETLBO.System.Object;
using NAS.DAL.Inventory.Audit;
using NAS.DAL.Inventory.Lot;
using WebModule.Warehouse.Report;
using NAS.BO.Inventory.Command.Report;
using DevExpress.XtraPrintingLinks;
using NAS.BO.Inventory.Ledger;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Ledger;

namespace WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand
{
    public partial class uEdittingOutputInventoryCommand : System.Web.UI.UserControl
    {
        #region properties

        RPT_OutputInventoryCommand RPT_OutputInventoryCommandBO = new RPT_OutputInventoryCommand();

        List<RPT_GenernalJournal> RPT_GenernalJournals_Objects
        {
            get
            {
                if (Session["RPT_GenernalJournals_Objects" + ViewStateControlId] == null)
                    return null;
                return (List<RPT_GenernalJournal>)Session["RPT_GenernalJournals_Objects" + ViewStateControlId];
            }
            set
            {
                Session["RPT_GenernalJournals_Objects" + ViewStateControlId] = value;
            }
        }

        private string Transit
        {
            get
            {
                if (Session["Transit" + ClientID + ViewStateControlId] == null)
                    return string.Empty;
                return Session["Transit" + ClientID + ViewStateControlId].ToString();
            }
            set
            {
                Session["Transit" + ClientID + ViewStateControlId] = value;
            }
        }

        //private BusinessObjectBO BusinessObjectBO = new BusinessObjectBO();
        List<RPT_InventoryCommand_Row> RPT_InventoryCommand_Rows
        {
            get
            {
                if (Session["RPT_InventoryCommand_Rows" + ViewStateControlId] == null)
                    return null;
                return (List<RPT_InventoryCommand_Row>)Session["RPT_InventoryCommand_Rows" + ViewStateControlId];
            }
            set
            {
                Session["RPT_InventoryCommand_Rows" + ViewStateControlId] = value;
            }
        }

        private bool IsRaiseDataUpdated
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

        private Guid TransactionId
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
                return (string)Session["SharedClientEvent"];
            }
            set
            {
                Session["SharedClientEvent"] = value;
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

        private Guid SourceArtifactId
        {
            get
            {
                if (Session["SourceArtifactId" + this.ClientID + ViewStateControlId] == null)
                    return Guid.Empty;
                return Guid.Parse(Session["SourceArtifactId" + this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session["SourceArtifactId" + this.ClientID + ViewStateControlId] = value;
            }
        }

        private string MainControlClientName
        {
            get
            {
                return cpInventoryCommand.ClientInstanceName;
            }
        }

        private ASPxButton ButtonPrintCommandOnBookingEntriesPopup
        {
            get
            {
                ASPxButton button = popupBookingEntries.FindControl("btnPrint") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonPrintCommand
        {
            get
            {
                ASPxButton button = popupInventoryCommand.FindControl("btnPrint") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonBookedEntries {
            get
            {
                ASPxButton button = popupBookingEntries.FindControl("btnBookedEntries") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonBookingEntries {
            get
            {
                ASPxButton button = popupInventoryCommand.FindControl("btnBookingEntries") as ASPxButton;
                return button;
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

        #region method

        private void SettingCommonInfoGUI()
        {
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            IEnumerable<InventoryJournal> journals = null;
            if (command != null && command.InventoryCommandItemTransactions != null && command.InventoryCommandItemTransactions.Count > 0)
            {
                command.Reload();
                journals = command.InventoryCommandItemTransactions.SelectMany(r => r.InventoryJournals).
                    Where(r => (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                          r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) && 
                          r.Credit > 0 &&
                          r.JournalType.Equals('A') && r.InventoryId != null
                    );

                if (journals != null && journals.Count() > 0)
                    cboInventory.ReadOnly = true;
                else
                    cboInventory.ReadOnly = false;
            }
            else
                cboInventory.ReadOnly = false;

            if (command != null)
            {
                cboInventory.Value = command.RelevantInventoryId.InventoryId;
                if (command.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    ButtonBookingEntries.ClientVisible = ButtonPrintCommand.ClientVisible = true;
                else
                    ButtonBookingEntries.ClientVisible = ButtonPrintCommand.ClientVisible = false;

                if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    ButtonBookedEntries.ClientVisible = false;
                else
                    ButtonBookedEntries.ClientVisible = true;
            }

        }

        private void SettingReport(string transit)
        {
            _02_VT report1 = new _02_VT();
            _02_VT_Accounting report2 = new _02_VT_Accounting();
            if (transit.Equals("PrintCommand"))
            {
                report1.DataSource = RPT_InventoryCommand_Rows;
                report1.DataMember = "";
                rptOutputCommViewer.Report = report1;
                rptOutputCommViewer.DataBind();
            }
            else if (transit.Equals("PrintBooking"))
            {
                report2.DataSource = RPT_InventoryCommand_Rows;
                report2.DataMember = "";
                rptOutputCommViewer.Report = report2;
                rptOutputCommViewer.DataBind();
            }

            grdFinancialJournal.DataSource = RPT_GenernalJournals_Objects;
            grdFinancialJournal.DataBind();
            report2.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = gvDataExporter };
        }

        public void initCommonJavascript()
        {
            if (!MainControlClientName.Equals(string.Empty))
            {
                cpInventoryCommand.ClientSideEvents.BeginCallback = "function(s, e){ " +
                    string.Format("{0}.Show();", ldpnInventoryCommand.ClientInstanceName) +
                    " }";

                cpInventoryCommand.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnInventoryCommand.ClientInstanceName) +
                    " if (s.cpIsSave){ delete s.cpIsSave; alert('Đã cập nhật thông tin phiếu kho');  target.fire({ type: '" + SharedClientEvent + "' }); }" +
                    " }";

                cpItemUnitBalanceDetail.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnInventoryCommand.ClientInstanceName) +
                " }";

                ButtonPrintCommandOnBookingEntriesPopup.ClientSideEvents.Click =
                    "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtCode.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('PrintBooking');", MainControlClientName)
                    + " }}";

                ButtonPrintCommand.ClientSideEvents.Click =
                    "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtCode.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('PrintCommand');", MainControlClientName)
                    + " }}";

                ButtonSaveCommand.ClientSideEvents.Click =
                    "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtCode.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('Save');", MainControlClientName)
                    + " }}";

                ButtonCloseCommandPopup.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", MainControlClientName)
                    + " }";

                ButtonBookingEntries.ClientSideEvents.Click =
                    "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtCode.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('BookEntries');", MainControlClientName)
                    + " }}";

                ButtonBookedEntries.ClientSideEvents.Click =
                    "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    txtCode.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('BookedEntries');", MainControlClientName)
                    + " }}";

                ButtonCloseCommandPopup.CausesValidation = false;

                popupInventoryCommand.ClientSideEvents.Closing = "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", MainControlClientName)
                    + " }";

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "RegisterStartupScript", "<script>function grdDetailJournal_RowClick(s, e){ " +
                "var params = new Array('RefreshClickExpandFormDetail', s.GetRowKey(e.visibleIndex)); " + 
                " s.StartEditRow(e.visibleIndex); " +
                string.Format("{0}.Show();", ldpnInventoryCommand.ClientInstanceName) +
                cpItemUnitBalanceDetail.ClientInstanceName + ".PerformCallback(params); " +
                " }</script>");
            }
        }

        public void SettingInit(ASPxGridView SourceGridView, string NewId, string EditId, string DeleteId/*, string completeEventName*/)
        {
            //SharedClientEvent = completeEventName;
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

            initCommonJavascript();
        }

        public void SettingInit(ASPxButton SourceButton/*string completeEventName*/)
        {
            //SharedClientEvent = completeEventName;
            if (!MainControlClientName.Equals(string.Empty) && SourceButton != null)
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Create');", MainControlClientName) +
                    " }";
            }

            initCommonJavascript();
        }

        public void SettingInit<T>(Guid SourceArtifactId, ASPxButton SourceButton/*, string completeEventName*/)
        {
            initCommonJavascript();
            this.SourceArtifactId = SourceArtifactId;
            if (!MainControlClientName.Equals(string.Empty))
            {
                if (typeof(T).Equals(typeof(NAS.DAL.Invoice.SalesInvoice)))
                {
                    SourceButton.ClientSideEvents.Click =
                        "function(s, e){ " +
                        string.Format("{0}.PerformCallback('CreateByBill');", MainControlClientName) +
                        " }";
                } else if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommand)))
                {
                    SourceButton.ClientSideEvents.Click =
                        "function(s, e){ " +
                        string.Format("{0}.PerformCallback('CreateByAudit');", MainControlClientName) +
                        " }";
                }
            }            
        }
        #endregion

        #region State Pattern

        public void PreCRUD_CreatingOutputInventoryCommandByArtifact()
        {

        }

        public void PreCRUD_CreatingOutputInventoryCommand()
        {

        }

        public void PreCRUD_EdittingOutputInventoryCommand()
        {

        }

        public void CRUD_CreatingOutputInventoryCommandByAuditArtifact()
        {
            CriteriaOperator _filter;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            string errormsg = string.Empty;
            try
            {
                InventoryAuditArtifact audit = uow.GetObjectByKey<InventoryAuditArtifact>(SourceArtifactId);
                if (audit == null)
                    throw new Exception("The AuditInventorycommand is not existing in system");

                if (audit.ApprovalStatus == Utility.Constant.APRROVE_YES)
                    throw new Exception("Phiếu kiểm kê đã duyệt không thể tạo phiếu xuất kho");

                int _invalid = 0;

                if (audit.InventoryAuditItemUnits.Count > 0)
                {
                    foreach (InventoryAuditItemUnit item in audit.InventoryAuditItemUnits)
                    {
                        if (item.ProcessingAmount < 0)
                        {
                            _invalid++;
                        }
                     
                    }                    
                }

                if (_invalid == 0)
                {
                    throw new Exception("Phiếu kiểm kho không có số lượng chênh phiếu để xuất kho");
                }

                _filter = CriteriaOperator.And(
                    new BinaryOperator("ParentInventoryCommandId", SourceArtifactId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal));
                NAS.DAL.Inventory.Command.InventoryCommand command = session.FindObject<NAS.DAL.Inventory.Command.InventoryCommand>(_filter);
                if (command == null)
                {

                    command =
                            InventoryCommandBO.CreateInventoryCommandByInventoryAudit(uow,
                                                                                        audit,
                                                                                        ObjectTypeEnum.INVENTORY_OUT,
                                                                                        INVENTORY_COMMAND_TYPE.OUT);
                }


                 _filter = CriteriaOperator.And(
                     new BinaryOperator("InventoryCommandId.InventoryCommandId", command.InventoryCommandId, BinaryOperatorType.Equal),
                     new BinaryOperator("InventoryCommandId.RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal));
                InventoryCommandItemTransaction _inventoryCommandItemTransaction = session.FindObject<InventoryCommandItemTransaction>(_filter);
                if (_inventoryCommandItemTransaction == null)
                {
                    ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
                    _inventoryCommandItemTransaction =
                            InventoryCommandBO.createInventoryCommandItemTransaction(uow,
                                                                                    artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_OUTPUT),
                                                                                    command.IssueDate,
                                                                                    command.Description,
                                                                                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT,
                                                                                    command.InventoryCommandId, audit.InventoryId.InventoryId, out errormsg);
                }
                else
                {
                    //_inventoryCommandItemTransaction.Code = audit.Code;
                    _inventoryCommandItemTransaction.Save();

                    foreach (InventoryJournal item in _inventoryCommandItemTransaction.InventoryJournals)
                    {

                        //InventoryCommandBO.deleteDoubleInventoryJournal(session, item.InventoryJournalId);
                    }                
                }
       
                foreach(InventoryAuditItemUnit item in audit.InventoryAuditItemUnits)
                {
                    if (item.ProcessingAmount < 0)
                    {                      
                        InventoryCommandBO.createDoubleInventoryJournalForInOutTransaction(
                                uow,
                                _inventoryCommandItemTransaction.InventoryTransactionId,
                                audit.InventoryId.InventoryId,
                                session.FindObject<Lot>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE, BinaryOperatorType.Equal)).LotId,
                                item.ItemUnitId.ItemUnitId,
                                Math.Abs(item.ProcessingAmount),
                                Math.Abs(item.ProcessingAmount),
                                string.Format("Xuất kho Mã hàng hóa '{0}' theo phiếu kiểm kê '{1}'",
                                    item.ItemUnitId.ItemId.Code, audit.Code),
                                NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                    }
                }

                uow.CommitChanges();

                InventoryCommandId = command.InventoryCommandId;
                InventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
                InventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
                InventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
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

        public void CRUD_CreatingOutputInventoryCommandByArtifact()
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                NAS.DAL.Invoice.Bill bill = session.GetObjectByKey<NAS.DAL.Invoice.Bill>(SourceArtifactId);
                if (bill == null)
                    throw new Exception("The bill is not existing in system");

                if (InventoryCommandBO.CheckIsCompletedDeliveryForSaleInvoice(uow, SourceArtifactId))
                    throw new Exception(string.Format("Phiếu bán '{0}' đã được giao đủ nên không thể tạo phiếu xuất kho!", bill.Code));

                NAS.DAL.Inventory.Command.InventoryCommand command = InventoryCommandBO.CreateInventoryCommandByBill(
                    uow,
                    SourceArtifactId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                uow.CommitChanges();
                InventoryCommandId = command.InventoryCommandId;
                InventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
                InventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
                InventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                uow.Dispose();
            }
        }

        public void CRUD_CreatingOutputInventoryCommand()
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommand command =
                    InventoryCommandBO.CreateInventoryNewCommand(uow, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                uow.CommitChanges();
                InventoryCommandId = command.InventoryCommandId;
                InventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
                InventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
                InventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
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

        public void CRUD_EdittingOutputInventoryCommand()
        {
            NAS.DAL.Inventory.Command.InventoryCommand command 
                = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            InventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
            InventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
            InventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
        }

        public void CRUD_BookingEntriesOutputInventoryCommand()
        {
            GridViewBookingEntries1.SetDataSource(InventoryCommandBO.GetInventoryFinancialTransactionOfInventoryCommand(session, InventoryCommandId));
        }

        public void CRUD_BookedEntriesOutputInventoryCommand()
        {
            InventoryCommandBO.BookFinancialEntriesOfInventoryCommand(InventoryCommandId);
        }

        public void CRUD_ClosingOutputInventoryCommand()
        {
            try
            {
                if (GUIContext.State is CreatingOutputInventoryCommandByInvoiceArtifact ||
                    GUIContext.State is CreatingOutputInventoryCommand)
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

        public void CRUD_DeletingOutputInventoryCommand()
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

        public void CRUD_SavingOutputInventoryCommand()
        {
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            InventoryCommandXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = command.InventoryCommandId.ToString();
            NAS.DAL.Nomenclature.Inventory.Inventory defaultInventory =
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, NAS.DAL.Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);
            try
            {
                UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
                InventoryCommandBO.SaveInOutInventoryCommand(uow,
                    InventoryCommandId,
                    txtCode.Text.Trim(),
                    txtIssueDate.Value != null ? DateTime.Parse(txtIssueDate.Value.ToString()) : DateTime.Now,
                    txtDescription.Text,
                    cboInventory.Value != null ? Guid.Parse(cboInventory.Value.ToString()) : defaultInventory.InventoryId);

                if (cboReciever.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.RECEIVER,
                        Guid.Parse(cboReciever.Value.ToString()));

                if (cboCreator.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.CREATOR,
                        Guid.Parse(cboCreator.Value.ToString()));

                if (cboStoreKeeper.Value != null)
                    InventoryCommandBO.UpdateSelectedActorInventoryCommandCombobox(uow,
                        InventoryCommandId,
                        DefaultInventoryCommandActorTypeEnum.STOREKEEPER,
                        Guid.Parse(cboStoreKeeper.Value.ToString()));

                uow.CommitChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateGUI_SavingOutputInventoryCommandByArtifact()
        {
            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
            if (RelevantInventoryCommand != null)
                lblMovingInventoryCommand.Text = RelevantInventoryCommand.ParentInventoryCommandId.Code;
            else
            {
                lblMovingInventoryCommand.ClientVisible = false;
                (frmCosting.Items[0] as DevExpress.Web.ASPxFormLayout.LayoutGroup).FindItemOrGroupByName("ParentInventoryCommand").ClientVisible = false;
            }

            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            if (command.ParentInventoryCommandId != null && command.ParentInventoryCommandId.CommandType.Equals('A'))
            {
                (frmCosting.Items[0] as DevExpress.Web.ASPxFormLayout.LayoutGroup).FindItemOrGroupByName("ParentInventoryCommand").ClientVisible = true;
                (frmCosting.Items[0] as DevExpress.Web.ASPxFormLayout.LayoutGroup).FindItemOrGroupByName("ParentInventoryCommand").Caption = "Phiếu kiểm kho";
                lblMovingInventoryCommand.Text = command.ParentInventoryCommandId.Code;
                lblMovingInventoryCommand.ClientVisible = true;
            }
            //Testing
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_CreatingOutputInventoryCommandByArtifact()
        {
            grdTransaction.DetailRows.CollapseAllRows();
            txtCode.IsValid = true;
            txtCode.Focus();
            txtIssueDate.IsValid = true;
            txtDescription.IsValid = true;
            popupInventoryCommand.ShowOnPageLoad = true;
            popupInventoryCommand.HeaderText = string.Format("Thông tin phiếu xuất kho - Thêm mới");
            ButtonBookingEntries.ClientVisible = false;
            lblMovingInventoryCommand.ClientVisible = false;
            (frmCosting.Items[0] as DevExpress.Web.ASPxFormLayout.LayoutGroup).FindItemOrGroupByName("ParentInventoryCommand").ClientVisible = false;
            cboStoreKeeper.Value = null;
            cboReciever.Value = null;
            cboCreator.Value = null;
            UpdateGUI_SavingOutputInventoryCommandByArtifact();
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_CreatingOutputInventoryCommand()
        {
            grdTransaction.DetailRows.CollapseAllRows();
            txtCode.IsValid = true;
            txtCode.Focus();
            txtIssueDate.IsValid = true;
            txtDescription.IsValid = true;
            popupInventoryCommand.ShowOnPageLoad = true;
            popupInventoryCommand.HeaderText = string.Format("Thông tin phiếu xuất kho - Thêm mới");
            ButtonBookingEntries.ClientVisible = ButtonPrintCommand.ClientVisible = false;
            lblMovingInventoryCommand.ClientVisible = false;
            (frmCosting.Items[0] as DevExpress.Web.ASPxFormLayout.LayoutGroup).FindItemOrGroupByName("ParentInventoryCommand").ClientVisible = false;
            cboStoreKeeper.Value = null;
            cboReciever.Value = null;
            cboCreator.Value = null;
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_EdittingOutputInventoryCommand()
        {
            grdTransaction.DetailRows.CollapseAllRows();
            txtCode.IsValid = true;
            txtCode.Focus();
            txtIssueDate.IsValid = true;
            txtIssueDate.ReadOnly = true;
            txtDescription.IsValid = true;
            popupInventoryCommand.ShowOnPageLoad = true;
            NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
            popupInventoryCommand.HeaderText = string.Format("Thông tin phiếu xuất kho - {0}", command.Code);

            Person pStoreKeeper = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
            Person pReceiver = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.RECEIVER);
            Person pCreator = InventoryCommandBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.CREATOR);

            if (pStoreKeeper == null)
                cboStoreKeeper.Value = null;
            else
                cboStoreKeeper.Value = pStoreKeeper.PersonId;

            if (pReceiver == null)
                cboReciever.Value = null;
            else
                cboReciever.Value = pReceiver.PersonId;

            if (pCreator == null)
                cboCreator.Value = null;
            else
                cboCreator.Value = pCreator.PersonId;

            ButtonBookingEntries.ClientVisible = ButtonPrintCommand.ClientVisible = true;
            UpdateGUI_SavingOutputInventoryCommandByArtifact();
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_BookingEntriesOutputInventoryCommand()
        {
            popupBookingEntries.ShowOnPageLoad = true;
            //ButtonBookedEntries.ClientVisible = true;
            UpdateGUI_SavingOutputInventoryCommandByArtifact();
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_BookedEntriesOutputInventoryCommand()
        {
            popupBookingEntries.ShowOnPageLoad = true;
            //ButtonBookedEntries.ClientVisible = false;
            UpdateGUI_SavingOutputInventoryCommandByArtifact();
            SettingCommonInfoGUI();
        }

        public void UpdateGUI_ClosingOutputInventoryCommand()
        {
            popupInventoryCommand.ShowOnPageLoad = false;
        }

        public void UpdateGUI_DeletingOutputInventoryCommand()
        {
            popupInventoryCommand.ShowOnPageLoad = false;
        }

        #endregion

        #region handler

        protected void cpItemUnitBalanceDetail_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
                            Guid.Parse(cboInventory.Value.ToString()));

                        Cogs = CogsBO.GetLastCOGS(session, 
                            journal.ItemUnitId.ItemUnitId,
                            currencyBO.GetDefaultCurrency(session).CurrencyId,
                            Guid.Parse(cboInventory.Value.ToString()));

                        lblManufacturer.Text = journal.ItemUnitId.ItemId.ManufacturerName;
                    }
                    break;
                case "RefreshInlineExpandFormDetail":
                    ItemUnit itemunit = session.GetObjectByKey<ItemUnit>(Guid.Parse(para[1]));
                    if (itemunit != null)
                    {
                        newestBalance = LedgerBO.GetItemUnitBalance(session,
                                itemunit.ItemUnitId,
                                Guid.Parse(cboInventory.Value.ToString()));

                        Cogs = CogsBO.GetLastCOGS(session,
                            itemunit.ItemUnitId,
                            currencyBO.GetDefaultCurrency(session).CurrencyId,
                            Guid.Parse(cboInventory.Value.ToString()));

                        lblManufacturer.Text = itemunit.ItemId.ManufacturerName;
                    }
                    break;
            }

            if (Cogs == null)
                lblPrice.Text = "0 VNĐ";
            else if (Cogs.COGSPrice == 0)
                lblPrice.Text = "0 VNĐ";
            else
                lblPrice.Text = string.Format("{0:#,###} VNĐ", Cogs.COGSPrice);

            if (newestBalance == 0)
                lblBalance.Text = "0";
            else
                lblBalance.Text = string.Format("{0:#,###}", newestBalance);
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
                TransactionId = (Guid)grdTransaction.GetRowValues(visibleIndex, "InventoryTransactionId");

                NASCustomFieldDataGridView.CMSObjectId =
                    InventoryCommandBO.GetCMSTransaction<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>
                    (TransactionId, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT).ObjectId;
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

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InventoryCommandXDS.Session = session;
            InventoryTransactionXDS.Session = session;
            //InventoryJournalXDS.Session = session;
            InventoryXDS.Session = session;
            InventoryCommandActorXDS.Session = session;
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
                grdTransaction.ClientInstanceName = string.Format("grdTransaction{0}", ViewStateControlId);
                popupInventoryCommand.ClientInstanceName = string.Format("popupInventoryCommand{0}", ViewStateControlId);
                cpInventoryCommand.ClientInstanceName = string.Format("cpInventoryCommand{0}", ViewStateControlId);
                cpItemUnitBalanceDetail.ClientInstanceName = string.Format("cpItemUnitBalanceDetail{0}", ViewStateControlId);
                ldpnInventoryCommand.ClientInstanceName = string.Format("ldpnInventoryCommand{0}", ViewStateControlId);
                GUIContext = new NAS.GUI.Pattern.Context();
            }

            SettingReport(Transit);
            //SettingCommonInfoGUI();
            InventoryTransactionXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();
            InventoryCommandActorXDS.CriteriaParameters["InventoryCommandId"].DefaultValue = InventoryCommandId.ToString();


            initCommonJavascript();
            TransactionCustomFieldDataGridView_BindDataUpdatedEvent();

            if (GUIContext != null && GUIContext.State != null && GUIContext.State is BookingEntriesOutputInventoryCommand)
                GridViewBookingEntries1.SetDataSource(InventoryCommandBO.GetInventoryFinancialTransactionOfInventoryCommand(session, InventoryCommandId));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void cpInventoryCommand_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];

            switch (trs)
            {
                case "CreateByAudit":
                    GUIContext.State = new CreatingOutputInventoryCommandByAuditArtifact(this);
                    break;
                case "CreateByBill":
                    GUIContext.State = new CreatingOutputInventoryCommandByInvoiceArtifact(this);
                    break;
                case "Create":
                    GUIContext.State = new CreatingOutputInventoryCommand(this);
                    break;
                case "Edit":
                    InventoryCommandId = Guid.Parse(para[1]);
                    GUIContext.State = new EdittingOutputInventoryCommand.State.EdittingOutputInventoryCommand(this);
                    break;
                case "Delete":
                    InventoryCommandId = Guid.Parse(para[1]);
                    GUIContext.State = new DeletingOutputInventoryCommand(this);
                    break;
                case "PrintCommand":
                case "PrintBooking":
                    RPT_OutputInventoryCommandBO.LoadInventoryCommandReport(InventoryCommandId);
                    RPT_InventoryCommand_Rows = RPT_OutputInventoryCommandBO.RPT_InventoryCommand_Rows;
                    RPT_GenernalJournals_Objects = RPT_OutputInventoryCommandBO.RPT_GenernalJournals_Objects;
                    Transit = trs;
                    SettingReport(Transit);
                    UpdateGUI_SavingOutputInventoryCommandByArtifact();
                    popupOutputCommReport.ShowOnPageLoad = true;
                    break;
                default:
                    GUIContext.Request(trs, this);
                    break;
            }

            if (trs.Equals("Save") || trs.Equals("Delete"))
                cpInventoryCommand.JSProperties.Add("cpIsSave", true);

        }

        protected void grdDetailJournal_Init(object sender, EventArgs e)
        {
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            grdDetailJournal.DataSourceID = "InventoryJournalXDS";
            grdDetailJournal.ClientInstanceName = string.Format(
                "grdDetailJournal{0}{1}",
                ViewStateControlId,
                grdDetailJournal.GetMasterRowKeyValue().ToString().Replace("-", ""));
        }

        protected void grdDetailJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            object MasterKey = grdDetailJournal.GetMasterRowKeyValue();
            if (MasterKey == null)
                return;
            if (grdDetailJournal.NamingContainer.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent is ASPxGridView)
            {
                ASPxGridView grdTransaction = grdDetailJournal.NamingContainer.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as ASPxGridView;
                int idx = grdTransaction.FindVisibleIndexByKeyValue(MasterKey);
                if (idx >= 0)
                {
                    XpoDataSource InventoryJournalXDS = grdTransaction.FindDetailRowTemplateControl(idx, "InventoryJournalXDS") as XpoDataSource;
                    InventoryJournalXDS.CriteriaParameters["InventoryTransactionId"].DefaultValue = MasterKey.ToString();
                }
            }
        }

        protected void grdDetailJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();

            try
            {
                //////Trường hợp Phiếu xuất kho có Phiếu nhập kho đối ứng////
                if (RelevantInventoryCommand == null)
                {
                    Guid _InventoryId = Guid.Empty;
                    //Non-Persistent From InventoryCommand
                    //_InventoryId = Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString());
                    _InventoryId = Guid.Parse(cboInventory.Value.ToString());
                    InventoryCommandBO.createDoubleInventoryJournalForInOutTransaction(
                        uow,
                        Guid.Parse(grdDetailJournal.GetMasterRowKeyValue().ToString()),
                        _InventoryId,
                        Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                        Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                        double.Parse(e.NewValues["PlanCredit"].ToString()),
                        double.Parse(e.NewValues["Credit"].ToString()),
                        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty,
                        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
                }
                //////Trường hợp Phiếu xuất kho không có Phiếu nhập kho đối ứng////
                else {
                    Guid _InventoryId = Guid.Empty;
                    //Non-Persistent From InventoryCommand
                    //_InventoryId = Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString());
                    _InventoryId = Guid.Parse(cboInventory.Value.ToString());
                    InventoryJournal outputJournal = InventoryCommandBO.createDoubleInventoryJournalForInOutMovingTransaction(
                        uow,
                        Guid.Parse(grdDetailJournal.GetMasterRowKeyValue().ToString()),
                        _InventoryId,
                        Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                        Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                        double.Parse(e.NewValues["PlanCredit"].ToString()),
                        double.Parse(e.NewValues["Credit"].ToString()),
                        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty,
                        NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);

                    InventoryCommandBO.AutoGenerateInventoryJournalForInputCommand_MovingCase(uow, outputJournal.InventoryJournalId, RelevantInventoryCommand.InventoryCommandId);
                }
                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                uow.Dispose();
                grdDetailJournal.CancelEdit();
            }
        }

        protected void grdDetailJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                //////Trường hợp Phiếu xuất kho có Phiếu nhập kho đối ứng////
                if (RelevantInventoryCommand == null)
                {
                    InventoryCommandBO.UpdateDoubleInventoryJournal(
                        uow,
                        Guid.Parse(e.Keys[0].ToString()),
                        //Non-Persistent To InventoryCommand
                        //Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString()),
                        Guid.Parse(cboInventory.Value.ToString()),
                        NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.DEFAULTCST).InventoryId,
                        Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                        Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                        double.Parse(e.NewValues["PlanCredit"].ToString()),
                        double.Parse(e.NewValues["Credit"].ToString()),
                        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty);
                }
                //////Trường hợp Phiếu xuất kho không có Phiếu nhập kho đối ứng////
                else {
                    InventoryJournal SourceOutputInventoryJournal = uow.GetObjectByKey<InventoryJournal>(Guid.Parse(e.Keys[0].ToString()));

                    InventoryJournal InputInventoryJournal = InventoryCommandBO.searchReleventMovingInventoryJournal(Guid.Parse(e.Keys[0].ToString()), RelevantInventoryCommand.InventoryCommandId);
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
                        //Non-Persistent To InventoryCommand
                        //Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString()),
                         Guid.Parse(cboInventory.Value.ToString()),
                        NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.TRANSITINVENTORY).InventoryId,
                        Guid.Parse(e.NewValues["LotId!Key"].ToString()),
                        Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()),
                        double.Parse(e.NewValues["PlanCredit"].ToString()),
                        double.Parse(e.NewValues["Credit"].ToString()),
                        e.NewValues["Description"] != null ? e.NewValues["Description"].ToString().Trim() : string.Empty);

                    //foreach (InventoryJournal journal in SourceOutputInventoryJournal.InventoryTransactionId.InventoryJournals)
                    //{
                    //    if (journal != SourceOutputInventoryJournal &&
                    //        journal.JournalType == 'A' && journal.Credit > 0 && journal.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                    //    {
                    //        Inventory newInventory = uow.GetObjectByKey<Inventory>(Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString()));
                    //        journal.InventoryId = newInventory;
                    //    }
                    //}
                }

                uow.CommitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                uow.Dispose();
                grdDetailJournal.CancelEdit();
            }

        }

        protected void grdDetailJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
            ASPxGridView grdDetailJournal = sender as ASPxGridView;
            try
            {
                //////Trường hợp Phiếu xuất kho có Phiếu nhập kho đối ứng////
                session.BeginTransaction();
                if (RelevantInventoryCommand == null)
                {
                    InventoryCommandBO.deleteDoubleInventoryJournal(session, Guid.Parse(e.Keys[0].ToString()));
                }
                //////Trường hợp Phiếu xuất kho không có Phiếu nhập kho đối ứng////
                else
                {
                    InventoryCommandBO.deleteDoubleInventoryJournal(session, Guid.Parse(e.Keys[0].ToString()));
                    InventoryJournal InputInventoryJournal = InventoryCommandBO.searchReleventMovingInventoryJournal(
                        Guid.Parse(e.Keys[0].ToString()),
                        RelevantInventoryCommand.InventoryCommandId);
                    InventoryCommandBO.deleteDoubleInventoryJournal(session, InputInventoryJournal.InventoryJournalId);
                }
                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
            finally {
                grdDetailJournal.CancelEdit();
            }
        }

        protected void grdTransaction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            string errormsg = string.Empty;
            try
            {
                Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DefaultInventoryEnum.NOT_AVAILABLE);
                InventoryCommandBO.createInventoryCommandItemTransaction(
                    uow,
                    e.NewValues["Code"].ToString(),
                    DateTime.Parse(e.NewValues["IssueDate"].ToString()),
                    e.NewValues["Description"] != null ? e.NewValues["Description"].ToString() : string.Empty,
                    NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT, 
                    InventoryCommandId,
                    cboInventory.Value != null ? Guid.Parse(cboInventory.Value.ToString()) : naInventory.InventoryId,
                    out errormsg);
                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                uow.Dispose();
                grdTransaction.JSProperties["cpErrorMsg"] = errormsg;
                grdTransaction.CancelEdit();
            }
        }

        protected void grdTransaction_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                session.BeginTransaction();
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

                //BusinessObjectBO.UpdateBusinessObject(session,
                //    Utility.Constant.BusinessObjectType_OutputInventoryCommandItemTransaction,
                //    transaction.InventoryTransactionId, transaction.IssueDate);
                session.CommitTransaction();
            }
            catch (Exception)
            { 
                session.RollbackTransaction();
            }
            finally{
            grdTransaction.CancelEdit();
            }
        }

        protected void grdTransaction_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            InventoryCommandBO.DeleteLogicInventoryTransaction(session, Guid.Parse(e.Keys[0].ToString()));
        }

        protected void grdDetailJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            ///////Combobox hàng hóa//////////////
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox comboItemUnit = e.Editor as ASPxComboBox;

                if (comboItemUnit != null)
                {
                    comboItemUnit.Focus();
                    comboItemUnit.TabIndex = 1;
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
                        cpItemUnitBalanceDetail.ClientInstanceName + ".PerformCallback(params); " +
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

            ///////Textbox giá trị chuyển//////////////
            if (e.Column.FieldName == "PlanCredit")
            {
                ASPxSpinEdit spinCredit = e.Editor as ASPxSpinEdit;

                if (spinCredit != null)
                {
                    spinCredit.TabIndex = 2;
                    spinCredit.MinValue = 0;
                    spinCredit.MaxValue = decimal.MaxValue;
                }
            }

            ///////Textbox giá trị chuyển//////////////
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

                    comboLots.ClientSideEvents.ValueChanged = "function(s,e){ " +
                        " var date = Date.parseLocale(s.GetSelectedItem().GetColumnText('ExpireDate').split(' ')[0], 'dd/MM/yyyy'); " +
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
                    InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
                    if (RelevantInventoryCommand != null) {
                        Inventory firstInventory = InventoryCommandBO.getFirstInventoryJournalForOutputCommand(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
                        if (firstInventory != null && gridview.IsNewRowEditing)
                        {
                            comboInventory.Value = firstInventory.InventoryId;
                            comboInventory.ReadOnly = true;
                        }
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

        #endregion

        protected void grdDetailJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
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

        protected void grdDetailJournal_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            //if (double.Parse(e.NewValues["PlanCredit"].ToString()) < (double.Parse(e.NewValues["Credit"].ToString())))
            //    throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn 'Số lượng thực tế'");
            if (double.Parse(e.NewValues["PlanCredit"].ToString()) <= 0)
                throw new Exception("'Số lượng yêu cầu' không thể nhỏ hơn hoặc bằng 0");

            if (double.Parse(e.NewValues["Credit"].ToString()) <= 0)
                throw new Exception("'Số lượng thực tế' không thể nhỏ hơn hoặc bằng 0");

            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);

            if (RelevantInventoryCommand != null)
            {
                try
                {
                    InventoryCommand inputCommand = session.GetObjectByKey<InventoryCommand>(RelevantInventoryCommand.InventoryCommandId);
                    Inventory inputInventory = InventoryCommandBO.getFirstInventoryJournalForInputCommand(
                        session,
                        inputCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId);

                    if (inputInventory == null)
                        return;

                    Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(session, DefaultInventoryEnum.NOT_AVAILABLE);

                    //Non-Persistent From InventoryCommand
                    //Guid newInventoryId = Guid.Parse(e.NewValues["FromInventoryId!Key"].ToString());
                    Guid newInventoryId = Guid.Parse(cboInventory.Value.ToString());
                    if (newInventoryId.Equals(inputInventory.InventoryId) && !newInventoryId.Equals(naInventory.InventoryId))
                    {
                        throw new Exception(string.Format("Kho '{0}' đã được chọn làm kho nhập! Vui lòng chọn kho khác", inputInventory.Code));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (cboInventory.Value != null)
            {
                InventoryLedgerBO LedgerBO = new InventoryLedgerBO();
                double newestBalance = LedgerBO.GetItemUnitBalance(session, 
                    Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()), 
                    Guid.Parse(cboInventory.Value.ToString()));

                if (newestBalance < double.Parse(e.NewValues["Credit"].ToString()))
                    throw new Exception(string.Format("Số lượng tồn kho là {0}, nên không đủ để xuất hàng ",
                        newestBalance.ToString()));
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

        protected void grdTransaction_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "IssueDate")
            {
                ASPxDateEdit date = e.Editor as ASPxDateEdit;

                if (date != null)
                {
                    date.Value = DateTime.Now;
                    date.Focus();
                }
            }
        }

        protected void grdTransaction_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (InventoryCommandId.Equals(Guid.Empty))
                return;

            InventoryCommand RelevantInventoryCommand = InventoryCommandBO.searchRelevantInventoryCommand(InventoryCommandId);
            if (RelevantInventoryCommand != null){
                if (e.ButtonType == ColumnCommandButtonType.Delete || e.ButtonType == ColumnCommandButtonType.New)
                {
                    e.Visible = false;
                }
            }
        }

        protected void btnAddLot_Load(object sender, EventArgs e)
        {
            ASPxButton button = sender as ASPxButton;
            DevExpress.Web.ASPxGridView.ASPxGridView grd = (button.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewHeaderTemplateContainer).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as
                DevExpress.Web.ASPxGridView.ASPxGridView;

            button.ClientVisible = false;
        }

        protected void grdTransaction_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            e.NewValues["Code"] = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORYTRANSACTION_INPUT);
        }

        protected void gvDataExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
        {
            if (e.RowType == GridViewRowType.Header)
            {
                e.BrickStyle.BackColor = System.Drawing.Color.Transparent;
                e.BrickStyle.ForeColor = System.Drawing.Color.Black;
            }
            e.BrickStyle.BorderWidth = 0;
        }

        protected void InventoryJournalXDS_Init(object sender, EventArgs e)
        {
            XpoDataSource source = sender as XpoDataSource;
            source.Session = session;
        }

        protected void btnBookedEntries_Load(object sender, EventArgs e)
        {
            //if (InventoryCommandId.Equals(Guid.Empty))
            //    return;

            //if (!InventoryCommandBO.IsBookedEntriesForInventoryCommand(session, InventoryCommandId))
            //    ButtonBookedEntries.ClientVisible = false;
            //else
            //    ButtonBookedEntries.ClientVisible = true;
        }
    }
}