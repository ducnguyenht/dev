using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.State;
using NAS.BO.Inventory.Audit;
using NAS.DAL.Nomenclature.Inventory;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Inventory.Audit;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxGridView;
using Utility;
using NAS.DAL.Inventory.Ledger;
using System.Drawing;
using DevExpress.Web.ASPxHtmlEditor;

namespace WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand
{
    public partial class uAuditingInventoryCommand : System.Web.UI.UserControl
    {
        private Session session;
        private InventoryAuditArtifactBO m_InventoryAuditArtifactBO;

        #region State Area

        public void setState(NAS.GUI.Pattern.State state, string name)
        {
            GUIContext.State.PreTransitionCRUD(name);

            GUIContext.State = state;

            GUIContext.State.CRUD();
            GUIContext.State.UpdateGUI();


        }

        // Loading
        public void PreTransitionCRUD_LoadingAuditingInventoryCommand(string stateName)
        {



        }

        public void CRUD_LoadingAuditingInventoryCommand()
        {

        }

        public void UpdateGUI_LoadingAuditingInventoryCommand()
        {

        }
        //End Loading

        // Creating
        public void PreTransitionCRUD_CreatingAuditingInventoryCommand(string stateName)
        {
            // insert (update) data

            if (stateName == "LOADING")
            {
            }
            else if (stateName == "CANCELING")
            {
            }
            else
            {
                // no support
            }
        }

        public void CRUD_CreatingAuditingInventoryCommand()
        {

        }

        public void UpdateGUI_CreatingAuditingInventoryCommand()
        {
            popupInventoryCommandCheck.ShowOnPageLoad = true;
        }
        //End Creating

        // Editing
        public void PreTransitionCRUD_EditingAuditingInventoryCommand(string stateName)
        {
            // insert (update) data

            if (stateName == "LOADING")
            {
            }
            else if (stateName == "CANCELING")
            {
            }
            else
            {
                // no support
            }
        }
        public void CRUD_EditingAuditingInventoryCommand()
        {
        }
        public void UpdateGUI_EditingAuditingInventoryCommand()
        {
        }

        //End Editing

        #endregion
        /////

        #region User Define

        private void changeStatusProperties(bool allow)
        {
            txtLessProcess.ReadOnly = allow;
            txtLessQuanlity.ReadOnly = allow;
            txtLossProcess.ReadOnly = allow;
            txtLossQuanlity.ReadOnly = allow;
            txtBalanceProcess.ReadOnly = allow;
        }

        private void changeValueProperties(Guid inventoryAuditItemUnitId)
        {
            txtLessProcess.Value = 0;
            txtLessQuanlity.Value = 0;
            txtLossProcess.Value = 0;
            txtLossQuanlity.Value = 0;
            txtBalanceProcess.Value = 0;

            //lblPrice.Text = "0";
            lblRealAmount.Text = "0";
            lblBookAmount.Text = "0";
            lblBalanceAmount.Text = "0";
            lblManufacturer.Text = "0";
            lblBalance.Text = "0";

            CriteriaOperator _filter = new BinaryOperator("InventoryAuditItemUnitId", inventoryAuditItemUnitId, BinaryOperatorType.Equal);
            XPCollection<QualityItem> _listQualityItem = new XPCollection<QualityItem>(session, _filter);

            InventoryAuditItemUnit _inventoryAuditItemUnit = session.GetObjectByKey<InventoryAuditItemUnit>(inventoryAuditItemUnitId);
            if (_inventoryAuditItemUnit != null)
            {
                _filter = new BinaryOperator("InventoryAuditItemUnitId", inventoryAuditItemUnitId, BinaryOperatorType.Equal);
                InventoryAuditItemUnit _inventoryAuditItemUnitId = session.GetObjectByKey<InventoryAuditItemUnit>(inventoryAuditItemUnitId);

                if (_inventoryAuditItemUnitId == null)
                {
                    throw new Exception("InventoryAuditItemUnit not exists !");
                }


                lblManufacturer.Text = _inventoryAuditItemUnit.ItemUnitId.ItemId.ManufacturerOrgId.Name;
                lblBalance.Text = (_inventoryAuditItemUnit.RealAmount - _inventoryAuditItemUnit.BookingAmount).ToString();
                txtBalanceProcess.Value = _inventoryAuditItemUnitId.ProcessingBiasAmount;

                if (cboInventory.Value == null)
                {
                    throw new Exception("Chưa chọn kho kiểm kê !");
                }

                //Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));

                _filter = new BinaryOperator("Code", cboInventory.Text.Split('-')[0].ToString(), BinaryOperatorType.Equal);
                Inventory _inventory = session.FindObject<Inventory>(_filter);


                if (_inventory == null)
                {
                    throw new Exception("Chưa chọn kho kiểm kê !");
                }

                _filter = CriteriaOperator.And(
                                   new BinaryOperator("InventoryId.InventoryId", _inventory.InventoryId, BinaryOperatorType.Equal),
                                   new BinaryOperator("ItemUnitId.ItemUnitId", _inventoryAuditItemUnitId.ItemUnitId.ItemUnitId, BinaryOperatorType.Equal));
                COGS _cogs = session.FindObject<COGS>(_filter);

                if (_cogs == null)
                {
                    lblPrice.Text = "1";
                }
                else
                {
                    lblPrice.Text = _cogs.COGSPrice.ToString();
                }

            }

            if (_listQualityItem.Count > 0)
            {
                foreach (QualityItem q in _listQualityItem)
                {
                    if (q.QualityItemType.Name == "LESS_QUANLITY")
                    {
                        txtLessQuanlity.Value = q.AuditAmount;
                        txtLessProcess.Value = q.QualityProcessingAmount;
                    }
                    else
                    {
                        txtLossQuanlity.Value = q.AuditAmount;
                        txtLossProcess.Value = q.QualityProcessingAmount;
                    }
                }
            }

            changeValueDervived(_inventoryAuditItemUnit);
        }

        private void changeValueDervived(InventoryAuditItemUnit _inventoryAuditItemUnit)
        {
            if (_inventoryAuditItemUnit != null)
            {
                lblTotalModify.Text = (double.Parse(txtBalanceProcess.Value == null ? "0" : txtBalanceProcess.Value.ToString()) + double.Parse(txtLessProcess.Value == null ? "0" : txtLessProcess.Value.ToString()) + double.Parse(txtLossProcess.Value == null ? "0" : txtLossProcess.Value.ToString())).ToString();
                lblRealAmount.Text = (_inventoryAuditItemUnit.RealAmount * double.Parse(lblPrice.Text)).ToString();
                lblBookAmount.Text = (_inventoryAuditItemUnit.BookingAmount * double.Parse(lblPrice.Text)).ToString();
                lblBalanceAmount.Text = ((_inventoryAuditItemUnit.RealAmount - _inventoryAuditItemUnit.BookingAmount) * double.Parse(lblPrice.Text)).ToString();
            }
        }

        #endregion User Define


        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }


        private string PopupControlClientName
        {
            get
            {
                return popupInventoryCommandCheck.ClientInstanceName;
            }
        }

        private string MainControlClientName
        {
            get
            {
                return cpInventoryCommandCheck.ClientInstanceName;
            }
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

        private Guid SessionId
        {
            get
            {
                return Guid.Parse(Session[this.ClientID + ViewStateControlId].ToString());
            }
            set
            {
                Session[this.ClientID + ViewStateControlId] = value;
            }
        }

        private ASPxButton ButtonSaveCommand
        {
            get
            {
                ASPxButton button = popupInventoryCommandCheck.FindControl("btnSaveAuditCommand") as ASPxButton;
                return button;
            }
        }

        private ASPxButton ButtonCloseCommand
        {
            get
            {
                ASPxButton button = popupInventoryCommandCheck.FindControl("btnCloseAuditCommandPopup") as ASPxButton;
                return button;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            InventoryAuditItemUnitXDS.Session = session;
            InventoryCommandActorXDS.Session = session;
        }


        protected override void OnPreRender(EventArgs e)
        {
            //ASPxButton sBtn = popupInventoryCommandCheck.FindControl("btnModifyOutCommand") as ASPxButton;
            //uEdittingOutputInventoryCommand1.SettingInit(SessionId, sBtn);

            //sBtn = popupInventoryCommandCheck.FindControl("btnModifyInCommand") as ASPxButton;
            //uEdittingInputInventoryCommand1.SettingInit(SessionId, sBtn);

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
            }

            if (!IsPostBack)
            {
                //grdDetailJournal.ClientInstanceName = string.Format("grdTransaction{0}", ViewStateControlId);
                popupInventoryCommandCheck.ClientInstanceName = string.Format("popupInventoryCommandCheck{0}", ViewStateControlId);
                cpInventoryCommandCheck.ClientInstanceName = string.Format("cpInventoryCommandCheck{0}", ViewStateControlId);
                ldpnInventoryCommandCheck.ClientInstanceName = string.Format("ldpnInventoryCommandCheck{0}", ViewStateControlId);

                GUIContext = new NAS.GUI.Pattern.Context(new LoadingAuditingInventoryCommand(this));
                SessionId = Guid.Empty;
            }
            else
            {
                InventoryCommandActorXDS.CriteriaParameters.Add("InventoryCommandId", SessionId.ToString());
                InventoryAuditItemUnitXDS.CriteriaParameters.Add("InventoryCommandId", SessionId.ToString());

                grdInventoryCommandActor.DataBind();
                grdDetailJournal.DataBind();
            }


            try
            {
                InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(Guid.Parse(SessionId.ToString()));
                if (_inventoryAuditArtifact != null)
                {
                    if (_inventoryAuditArtifact.ApprovalStatus == Constant.APRROVE_YES)
                    {
                        txtSuggestion.Enabled = false;
                    }
                    else
                    {
                        txtSuggestion.Enabled = true;
                    }
                }
            }
            catch
            {
                txtSuggestion.Enabled = true;
            }

            //ASPxButton button = (ASPxButton)popupInventoryCommandCheck.FindControl("btnModifyInCommand");
            //InventoryAuditArtifact _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(SessionId);
            //if (_inventoryAuditArtifact == null)
            //{
            //    button.Enabled = false;
            //}
            //else
            //{
            //    button.Enabled = true;
            //}

            initCommonJavascript();

        }

        protected void cpAuditInventoryCommand_OnCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            string trs = para[0];
            InventoryAuditArtifact _inventoryAuditArtifact = null;
            InventoryCommandActor _inventoryCommandActor = null;

            ASPxButton sBtn;

            switch (para[0])
            {
                case "Create":

                    txtCode.Text = "";
                    txtIssuedDate.Date = DateTime.Now;
                    txtDescription.Text = "";
                    txtName.Text = "";

                    txtBalanceProcess.Value = 0;
                    txtLessProcess.Value = 0;
                    txtLessQuanlity.Value = 0;
                    txtLossProcess.Value = 0;
                    txtLossQuanlity.Value = 0;

                    cboInventory.Value = null;

                    setState(new CreatingAuditingInventoryCommand(this), "CREATING");

                    _inventoryAuditArtifact = new InventoryAuditArtifact(session);

                    _inventoryAuditArtifact.InventoryCommandId = SessionId = Guid.NewGuid();
                    _inventoryAuditArtifact.RowStatus = 0;
                    _inventoryAuditArtifact.CommandType = 'A';
                    _inventoryAuditArtifact.Save();

                    //
                    CriteriaOperator _filter = new BinaryOperator("Name", "CHIEFCHECKING", BinaryOperatorType.Equal);
                    InventoryCommandActorType _inventoryCommandActorType = session.FindObject<InventoryCommandActorType>(_filter);

                    if (_inventoryCommandActorType == null)
                    {
                        _inventoryCommandActorType = new InventoryCommandActorType(session);
                        _inventoryCommandActorType.Name = "CHIEFCHECKING";
                        _inventoryCommandActorType.Description = "Trưởng ban";
                        _inventoryCommandActorType.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        _inventoryCommandActorType.Save();
                    }
                    _inventoryCommandActor = new InventoryCommandActor(session);
                    _inventoryCommandActor.InventoryCommandId = _inventoryAuditArtifact;
                    _inventoryCommandActor.InventoryCommandActorTypeId = _inventoryCommandActorType;
                    _inventoryCommandActorType.Save();

                    _filter = new BinaryOperator("Name", "CHECKER1", BinaryOperatorType.Equal);
                    _inventoryCommandActorType = session.FindObject<InventoryCommandActorType>(_filter);
                    if (_inventoryCommandActorType == null)
                    {
                        _inventoryCommandActorType = new InventoryCommandActorType(session);
                        _inventoryCommandActorType.Name = "CHECKER1";
                        _inventoryCommandActorType.Description = "Ủy viên 1";
                        _inventoryCommandActorType.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        _inventoryCommandActorType.Save();
                    }

                    _inventoryCommandActor = new InventoryCommandActor(session);
                    _inventoryCommandActor.InventoryCommandId = _inventoryAuditArtifact;
                    _inventoryCommandActor.InventoryCommandActorTypeId = _inventoryCommandActorType;
                    _inventoryCommandActorType.Save();

                    _filter = new BinaryOperator("Name", "CHECKER2", BinaryOperatorType.Equal);
                    _inventoryCommandActorType = session.FindObject<InventoryCommandActorType>(_filter);
                    if (_inventoryCommandActorType == null)
                    {
                        _inventoryCommandActorType = new InventoryCommandActorType(session);
                        _inventoryCommandActorType.Name = "CHECKER1";
                        _inventoryCommandActorType.Description = "Ủy viên 1";
                        _inventoryCommandActorType.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        _inventoryCommandActorType.Save();
                    }

                    _inventoryCommandActor = new InventoryCommandActor(session);
                    _inventoryCommandActor.InventoryCommandId = _inventoryAuditArtifact;
                    _inventoryCommandActor.InventoryCommandActorTypeId = _inventoryCommandActorType;
                    _inventoryCommandActorType.Save();

                    //
                    _filter = new BinaryOperator("Name", "LESS_QUANLITY", BinaryOperatorType.Equal);
                    QualityItemType qualityItemType = session.FindObject<QualityItemType>(_filter);
                    if (qualityItemType == null)
                    {
                        qualityItemType = new QualityItemType(session);
                        qualityItemType.Name = "LESS_QUANLITY";
                        qualityItemType.RowStatus = 1;
                        qualityItemType.Description = "Kém phẩm chất";
                        qualityItemType.Save();
                    }

                    _filter = new BinaryOperator("Name", "LOSS_QUANLITY", BinaryOperatorType.Equal);
                    qualityItemType = session.FindObject<QualityItemType>(_filter);
                    if (qualityItemType == null)
                    {
                        qualityItemType = new QualityItemType(session);
                        qualityItemType.Name = "LOSS_QUANLITY";
                        qualityItemType.RowStatus = 1;
                        qualityItemType.Description = "Mất phẩm chất";
                        qualityItemType.Save();
                    }

                    cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");

                    break;

                case "Edit":

                    // reset

                    txtCode.Text = "";
                    txtIssuedDate.Date = DateTime.Now;
                    txtDescription.Text = "";
                    txtName.Text = "";
                    cboInventory.Text = "";

                    InventoryCommandId = Guid.Parse(para[1]);

                    _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(Guid.Parse(para[1]));
                    if (_inventoryAuditArtifact != null)
                    {
                        SessionId = _inventoryAuditArtifact.InventoryCommandId;

                        txtCode.Text = _inventoryAuditArtifact.Code;
                        txtIssuedDate.Date = _inventoryAuditArtifact.IssueDate;
                        txtDescription.Text = _inventoryAuditArtifact.Description;
                        txtName.Text = _inventoryAuditArtifact.Name;

                        if (_inventoryAuditArtifact.InventoryId != null)
                        {
                            cboInventory.Text = String.Format("{0}-{1}", _inventoryAuditArtifact.InventoryId.Code, _inventoryAuditArtifact.InventoryId.Name);
                        }

                        if (_inventoryAuditArtifact.InventoryAuditItemUnits.Count > 0)
                        {
                            grdDetailJournal.Selection.SetSelection(0, true);
                        }

                        if (_inventoryAuditArtifact.ApprovalStatus == Constant.APRROVE_YES)
                        {
                            ASPxButton button = (ASPxButton)popupInventoryCommandCheck.FindControl("btnSaveCommand");
                            if (button != null)
                            {
                                button.Enabled = false;
                            }

                            button = (ASPxButton)popupInventoryCommandCheck.FindControl("btnModifyCommand");
                            if (button != null)
                            {
                                button.Enabled = false;
                            }
                        }


                        cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");
                    }

                    break;

                case "Delete":
                    InventoryCommandId = Guid.Parse(para[1]);
                    break;

                case "Save":

                    _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(SessionId);
                    if (_inventoryAuditArtifact != null)
                    {
                        _inventoryAuditArtifact.Code = txtCode.Text;
                        _inventoryAuditArtifact.Name = txtName.Text;
                        _inventoryAuditArtifact.Description = txtDescription.Text;
                        _inventoryAuditArtifact.IssueDate = DateTime.Parse(txtIssuedDate.Value.ToString());

                        Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));
                        if (_inventory != null)
                        {
                            _inventoryAuditArtifact.InventoryId = _inventory;
                        }

                        _inventoryAuditArtifact.RowStatus = 1;
                        _inventoryAuditArtifact.CommandType = 'A';
                        _inventoryAuditArtifact.Save();
                    }

                    break;

                case "SaveAndClose":

                    _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(SessionId);
                    if (_inventoryAuditArtifact != null)
                    {
                        _inventoryAuditArtifact.Code = txtCode.Text;
                        _inventoryAuditArtifact.Name = txtName.Text;
                        _inventoryAuditArtifact.Description = txtDescription.Text;

                        _inventoryAuditArtifact.IssueDate = DateTime.Parse(txtIssuedDate.Value.ToString());

                        Inventory _inventory = session.GetObjectByKey<Inventory>(cboInventory.Value);
                        if (_inventory != null)
                        {
                            _inventoryAuditArtifact.InventoryId = _inventory;
                        }

                        _inventoryAuditArtifact.RowStatus = 1;
                        _inventoryAuditArtifact.CommandType = 'A';
                        _inventoryAuditArtifact.Save();


                        popupInventoryCommandCheck.ShowOnPageLoad = false;
                        cpInventoryCommandCheck.JSProperties.Clear();
                        cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");
                    }
                    break;

                case "Close":
                    _inventoryAuditArtifact = session.GetObjectByKey<InventoryAuditArtifact>(SessionId);
                    if (_inventoryAuditArtifact != null)
                    {
                        if (_inventoryAuditArtifact.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
                        {
                            cpInventoryCommandCheck.JSProperties.Clear();
                            cpInventoryCommandCheck.JSProperties.Add("cpAsking", "asking");
                        }
                        else
                        {
                            popupInventoryCommandCheck.ShowOnPageLoad = false;
                            cpInventoryCommandCheck.JSProperties.Clear();
                            cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");
                        }
                    }
                    else
                    {
                        popupInventoryCommandCheck.ShowOnPageLoad = false;
                        cpInventoryCommandCheck.JSProperties.Clear();
                        cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");
                    }

                    grdDetailJournal.CancelEdit();

                    break;

                case "CloseAlway":
                    grdDetailJournal.CancelEdit();
                    popupInventoryCommandCheck.ShowOnPageLoad = false;
                    cpInventoryCommandCheck.JSProperties.Clear();
                    cpInventoryCommandCheck.JSProperties.Add("cpRefreshGrid", "refresh");
                    break;
                case "Print":
                    break;
            }


            sBtn = popupInventoryCommandCheck.FindControl("btnModifyOutCommand") as ASPxButton;
            uEdittingOutputInventoryCommand1.SettingInit<NAS.DAL.Inventory.Command.InventoryCommand>(SessionId, sBtn);

            sBtn = popupInventoryCommandCheck.FindControl("btnModifyInCommand") as ASPxButton;
            uEdittingInputInventoryCommand1.SettingInit<NAS.DAL.Inventory.Command.InventoryCommand>(SessionId, sBtn);

            if (trs.Equals("Save") || trs.Equals("Delete"))
                cpInventoryCommandCheck.JSProperties.Add("cpIsSave", true);
        }

        public void initCommonJavascript()
        {
            if (!MainControlClientName.Equals(string.Empty))
            {
                cpInventoryCommandCheck.ClientSideEvents.BeginCallback = "function(s, e){ " +
                    string.Format("{0}.Show();", ldpnInventoryCommandCheck.ClientInstanceName) +
                    " }";

                cpInventoryCommandCheck.ClientSideEvents.EndCallback = "function(s, e){ " +
                    string.Format("{0}.Hide(); ", ldpnInventoryCommandCheck.ClientInstanceName) +
                    " if (s.cpRefreshGrid) {" +
                        "grdDetailJournal.Refresh();" +
                        "grdInventoryCommand.Refresh();" +
                        "delete (s.cpRefreshGrid);" +
                    "}" +
                    " if (s.cpIsSave){ delete s.cpIsSave;" +
                        string.Format("{0}.SetEnabled(false); ", ButtonSaveCommand.ClientInstanceName) +
                    "}" +
                    " if (s.cpAsking) {" +
                        " if (confirm('Bạn có muốn lưu phiếu này không ?')) {" +
                            string.Format("{0}.PerformCallback('SaveAndClose');", MainControlClientName) +
                        " }" +
                        " else {" +
                             string.Format("{0}.PerformCallback('CloseAlway');", MainControlClientName) +
                        " }" +
                        " delete (s.cpAsking);" +
                    " }" +
                " }";

                popupInventoryCommandCheck.ClientSideEvents.Closing = "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", MainControlClientName)
                    + " }";

                //ButtonSaveCommand.ClientSideEvents.Click =
                //   "function(s, e){ var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                //   txtCode.ClientID +
                //   ".GetMainElement(), null, true); if (validated) {" +
                //   string.Format("{0}.PerformCallback('Save');", MainControlClientName)
                //   + " }}";

                ButtonSaveCommand.ClientSideEvents.Click =
                 "function(s, e){" +
                    string.Format("{0}.PerformCallback('Save');", MainControlClientName)
                 + "}";


                ButtonCloseCommand.ClientSideEvents.Click =
                   "function(s, e){" +
                        string.Format("{0}.Hide();", popupInventoryCommandCheck.ClientInstanceName)
                   + "}";

                ButtonCloseCommand.CausesValidation = false;
            }
        }

        public void SettingInit(ASPxButton SourceButton)
        {
            if (!MainControlClientName.Equals(string.Empty) && SourceButton != null)
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Create');", MainControlClientName) +
                    " }";
            }
            initCommonJavascript();
        }

        public void SettingInit(ASPxGridView SourceGrid, string method)
        {
            if (!MainControlClientName.Equals(string.Empty) && SourceGrid != null)
            {
                SourceGrid.ClientSideEvents.EndCallback =
                    "function(s, e){ " +
                        "if (s.cpNew) {" +
                            string.Format("{0}.Show();", PopupControlClientName) +
                             string.Format("{0}.PerformCallback('Create');", MainControlClientName) +
                            "delete (s.cpNew);" +
                        "}" +
                        "if (s.cpUpdate) {" +
                            string.Format("{0}.Show();", PopupControlClientName) +
                            string.Format("{0}.PerformCallback('Edit|' + grdInventoryCommand.GetRowKey(grdInventoryCommand.GetFocusedRowIndex()));", MainControlClientName) +
                            "hfReportAudit.Set('id', grdInventoryCommand.GetRowKey(grdInventoryCommand.GetFocusedRowIndex()));" +
                            "delete (s.cpUpdate);" +
                        "}" +
                        "if (s.cpRefreshGrid) {" +
                            "grdInventoryCommand.Refresh();" +
                            "delete (s.cpRefreshGrid);" +
                        "}" +
                    "}";
            }

            initCommonJavascript();
        }

        public void buttonInitClientEventClick(ASPxButton SourceButton, string action)
        {
            if (!MainControlClientName.Equals(string.Empty) && SourceButton != null)
            {
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                        string.Format("{0}.PerformCallback('" + action + "');", MainControlClientName) +
                    "}";
            }
        }

        protected void cboInventory_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<Inventory> collection = new XPCollection<Inventory>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator("Code", Constant.NAAN_DEFAULT_NOTAVAILABLE, BinaryOperatorType.NotEqual),
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboInventory.DataSource = collection;
            cboInventory.DataBindItems();
        }

        protected void cboInventory_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                ASPxComboBox combo = source as ASPxComboBox;
                Inventory obj = session.GetObjectByKey<Inventory>(e.Value);

                if (obj != null)
                {
                    cboInventory.DataSource = new Inventory[] { obj };
                    cboInventory.DataBindItems();
                }
            }
            catch
            {
            }
        }


        protected void comboItemUnit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            ItemUnit obj = session.GetObjectByKey<ItemUnit>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new ItemUnit[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void comboItemUnit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                    new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
                        "PRODUCT")),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                    new ContainsOperator("ItemId.itemUnitTypeConfigs", new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)),
                    new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void colPersonOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Person obj = session.GetObjectByKey<Person>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Person[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colPersonOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria =
                CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));


            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void colInventoryCommandOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            InventoryCommandActorType obj = session.GetObjectByKey<InventoryCommandActorType>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                combo.DataSource = new InventoryCommandActorType[] { obj };
                combo.DataBindItems();
            }
        }

        protected void colInventoryCommandOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox combo = source as ASPxComboBox;
            XPCollection<InventoryCommandActorType> collection = new XPCollection<InventoryCommandActorType>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));


            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            combo.DataSource = collection;
            combo.DataBindItems();

        }

        protected void formInventoryCommandActor_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {

        }


        #region grdInventoryCommandActor
        protected void grdInventoryCommandActor_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["InventoryCommandId"] = SessionId;
        }

        protected void grdInventoryCommandActor_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
        }

        protected void grdInventoryCommandActor_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
        }
        #endregion

        double lossQuantity;
        double lossProcess;
        double lessQuantity;
        double lessProcess;
        double balanceProcess;
        protected void grdDetailJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (cboInventory.Value == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));
            if (_inventory == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            e.NewValues["InventoryAuditItemUnitId"] = Guid.NewGuid();
            e.NewValues["InventoryAuditArtifactId!Key"] = SessionId.ToString();
            e.NewValues["RowStatus"] = "1";

            ASPxSpinEdit c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["BookingAmount"], "colBookingAmount");
            e.NewValues["BookingAmount"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["RealAmount"], "colRealAmount");
            e.NewValues["RealAmount"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LessProcess"], "colLessProcess");
            lessProcess = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LossProcess"], "colLossProcess");
            lossProcess = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LessQuantity"], "colLessQuantity");
            lessQuantity = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LossQuantity"], "colLossQuantity");
            lossQuantity = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["ProcessingBiasAmount"], "colBalanceProcess");
            balanceProcess = double.Parse(c.Value.ToString());

            e.NewValues["ProcessingBiasAmount"] = balanceProcess.ToString();
            e.NewValues["ProcessingAmount"] = (
                                                     balanceProcess
                                                     + lessProcess
                                                     + lossProcess
                                              ).ToString();


        }

        protected void grdDetailJournal_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            CriteriaOperator _filter;

            _filter = new BinaryOperator("Name", "LESS_QUANLITY", BinaryOperatorType.Equal);
            QualityItemType _qualityItemType = session.FindObject<QualityItemType>(_filter);

            if (_qualityItemType != null)
            {
                QualityItem qualityItem = new QualityItem(session);
                qualityItem.QualityItemType = _qualityItemType;
                qualityItem.InventoryAuditItemUnitId = session.GetObjectByKey<InventoryAuditItemUnit>(Guid.Parse(e.NewValues["InventoryAuditItemUnitId"].ToString()));
                qualityItem.AuditAmount = lessQuantity;
                qualityItem.QualityProcessingAmount = lessProcess;

                qualityItem.Save();
            }

            _filter = new BinaryOperator("Name", "LOSS_QUANLITY", BinaryOperatorType.Equal);
            _qualityItemType = session.FindObject<QualityItemType>(_filter);

            if (_qualityItemType != null)
            {
                QualityItem qualityItem = new QualityItem(session);
                qualityItem.QualityItemType = _qualityItemType;
                qualityItem.InventoryAuditItemUnitId = session.GetObjectByKey<InventoryAuditItemUnit>(Guid.Parse(e.NewValues["InventoryAuditItemUnitId"].ToString()));
                qualityItem.AuditAmount = lossQuantity;
                qualityItem.QualityProcessingAmount = lossProcess;

                qualityItem.Save();
            }

            grdDetailJournal.Selection.SetSelectionByKey(Guid.Parse(e.NewValues["InventoryAuditItemUnitId"].ToString()), true);
            grdDetailJournal.JSProperties.Add("cpDisableProperties", "true");
        }

        protected void grdDetailJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cboInventory.Value == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));
            if (_inventory == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }


            CriteriaOperator _filter = new BinaryOperator("InventoryAuditItemUnitId", Guid.Parse(e.OldValues["InventoryAuditItemUnitId"].ToString()), BinaryOperatorType.Equal);
            XPCollection<QualityItem> _listQualityItem = new XPCollection<QualityItem>(session, _filter);

            ASPxSpinEdit c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LessProcess"], "colLessProcess");
            lessProcess = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LossProcess"], "colLossProcess");
            lossProcess = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LessQuantity"], "colLessQuantity");
            lessQuantity = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["LossQuantity"], "colLossQuantity");
            lossQuantity = double.Parse(c.Value.ToString());

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["ProcessingBiasAmount"], "colBalanceProcess");
            balanceProcess = double.Parse(c.Value.ToString());

            if (_listQualityItem.Count > 0)
            {
                foreach (QualityItem q in _listQualityItem)
                {
                    if (q.QualityItemType.Name == "LESS_QUANLITY")
                    {
                        q.AuditAmount = lessQuantity;
                        q.QualityProcessingAmount = lessProcess;
                        q.Save();
                    }
                    else
                    {
                        q.AuditAmount = lossQuantity;
                        q.QualityProcessingAmount = lossProcess;
                        q.Save();
                    }
                }
            }

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["BookingAmount"], "colBookingAmount");
            e.NewValues["BookingAmount"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdDetailJournal.FindEditRowCellTemplateControl((GridViewDataColumn)grdDetailJournal.Columns["RealAmount"], "colRealAmount");
            e.NewValues["RealAmount"] = c.Value.ToString();

            e.NewValues["ProcessingBiasAmount"] = balanceProcess.ToString();
            e.NewValues["ProcessingAmount"] = (
                                                     balanceProcess
                                                     + lessProcess
                                                     + lossProcess
                                              ).ToString();



            grdDetailJournal.JSProperties.Add("cpDisableProperties", "true");
        }

        protected void grdDetailJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            InventoryAuditItemUnit inventoryAuditItemUnitId = session.GetObjectByKey<InventoryAuditItemUnit>(Guid.Parse(e.Values["InventoryAuditItemUnitId"].ToString()));
            if (inventoryAuditItemUnitId != null)
            {
                inventoryAuditItemUnitId.RowStatus = -1;
                inventoryAuditItemUnitId.Save();
            }

            e.Cancel = true;
        }

        protected void colBookingAmount_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "BookingAmount").ToString();
                }
            }



            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +
                                                            "colBalance.SetValue(colRealAmount.GetValue()-colBookingAmount.GetValue());" +
                                                            "lblBalance.SetValue(colRealAmount.GetValue()-s.GetValue());" +
                                                            "lblBalanceAmount.SetValue((s.GetValue()-colBookingAmount.GetValue())*parseFloat(lblPrice.GetText()));" +
                                                            "lblBookAmount.SetValue(s.GetValue()*parseFloat(lblPrice.GetText()));" +
                                                            "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +

                                                  "}";
        }

        protected void colRealAmount_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "RealAmount").ToString();
                }
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +
                                                            "colBalance.SetValue(colRealAmount.GetValue()-colBookingAmount.GetValue());" +
                                                            "lblBalance.SetValue(s.GetValue()-colBookingAmount.GetValue());" +
                                                            "lblBalanceAmount.SetValue((s.GetValue()-colBookingAmount.GetValue())*parseFloat(lblPrice.GetText()));" +
                                                            "lblRealAmount.SetValue(s.GetValue()*parseFloat(lblPrice.GetText()));" +
                                                            "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +
                                                  "}";
        }

        protected void colBalance_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = container.Text;
            }

            spin.BackColor = Color.White;
        }

        protected void colBalanceProcess_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "ProcessingBiasAmount").ToString();
                }
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +
                //"txtBalanceProcess.SetText(s.GetValue());" +                                                        
                                                            "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +

                                                  "}";
        }

        protected void colLessQuantity_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "LessQuantity").ToString();
                }
            }

            //spin.ClientSideEvents.ValueChanged = "function (s,e) { " +                                                     
            //                                                "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +
            //                                      "}";
        }


        protected void colLessProcess_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "LessProcess").ToString();
                }
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +
                //"txtLessProcess.SetText(s.GetValue());" +
                                                            "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +

                                                  "}";
        }


        protected void colLossQuantity_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "LossQuantity").ToString();
                }
            }

            //spin.ClientSideEvents.ValueChanged = "function (s,e) { " +                                                     
            //                                                "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +
            //                                      "}";
        }

        protected void colLossProcess_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                if (container.DataItem != null)
                {
                    spin.Text = DataBinder.Eval(container.DataItem, "LossProcess").ToString();
                }
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +
                //"txtLossProcess.SetText(s.GetValue());" +
                                                            "lblTotalModify.SetText(parseFloat(colBalanceProcess.GetValue())+colLessProcess.GetValue()+colLossProcess.GetValue());" +
                                                  "}";
        }


        protected void txtSuggestion_Init(object sender, EventArgs e)
        {
        }

        protected void grdDetailJournal_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }

        protected void grdDetailJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;

                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "if (s.GetSelectedIndex() < 0) {return;}" +
                    //"grdDetailJournal.GetEditor('ItemUnitId.ItemId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.Name'));" +
                                                           "grdDetailJournal.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name'));" +
                                                           "lblManufacturer.SetText(s.GetSelectedItem().GetColumnText('ItemId.ManufacturerOrgId.Name'));" +
                                                           "cpProperty.PerformCallback('recogs|' + s.GetSelectedItem().GetColumnText('ItemUnitId'));" +
                                                       "}";

                if (combo != null)
                {
                    combo.Focus();
                }
            }
            if (e.Column.Name == "dexuat")
            {
                e.Editor.Enabled = false;
                e.Editor.BackColor = Color.WhiteSmoke;
            }
        }

        protected void cpProperty_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            switch (para[0])
            {
                case "properties":
                    changeValueProperties(Guid.Parse(para[1].ToString()));
                    break;

                case "status":
                    changeStatusProperties(bool.Parse(para[1]));
                    break;
                case "modify":
                    changeValueProperties(Guid.Parse(para[1]));
                    changeStatusProperties(false);
                    break;

                case "recogs":

                    if (cboInventory.Value == null)
                    {
                        throw new Exception("Chưa chọn kho kiểm kê !");
                    }

                    Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));
                    if (_inventory == null)
                    {
                        throw new Exception("Chưa chọn kho kiểm kê !");
                    }

                    CriteriaOperator _filter = CriteriaOperator.And(
                                    new BinaryOperator("InventoryId.InventoryId", _inventory.InventoryId, BinaryOperatorType.Equal),
                                    new BinaryOperator("ItemUnitId.ItemUnitId", Guid.Parse(para[1]), BinaryOperatorType.Equal));
                    COGS _cogs = session.FindObject<COGS>(_filter);

                    if (_cogs == null)
                    {
                        lblPrice.Text = "1";
                    }
                    else
                    {
                        lblPrice.Text = _cogs.COGSPrice.ToString();
                    }

                    changeValueProperties(Guid.Empty);
                    break;
            }
        }

        protected void grdDetailJournal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (cboInventory.Value == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            CriteriaOperator _filter = new BinaryOperator("Code", cboInventory.Text.Split('-')[0].ToString(), BinaryOperatorType.Equal);
            Inventory _inventory = session.FindObject<Inventory>(_filter);
            if (_inventory == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            grdDetailJournal.JSProperties.Add("cpRefreshProperties", e.EditingKeyValue.ToString());
        }

        protected void grdDetailJournal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (cboInventory.Value == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            Inventory _inventory = session.GetObjectByKey<Inventory>(Guid.Parse(cboInventory.Value.ToString()));
            if (_inventory == null)
            {
                throw new Exception("Chưa chọn kho kiểm kê !");
            }

            grdDetailJournal.JSProperties.Add("cpRefreshProperties", Guid.Empty.ToString());
        }

        protected void frmDetailOfLine_Init(object sender, EventArgs e)
        {
            if (!Page.IsCallback)
            {
                changeStatusProperties(true);
            }
        }



        protected void txtLessProcess_Init(object sender, EventArgs e)
        {
            txtLessProcess.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                                 "lblTotalModify.SetText(parseFloat(txtBalanceProcess.GetText())+txtLessProcess.GetNumber()+txtLossProcess.GetNumber());" +
                                                            "}";
        }

        protected void txtLossProcess_Init(object sender, EventArgs e)
        {
            txtLossProcess.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                               "lblTotalModify.SetText(parseFloat(txtBalanceProcess.GetText())+txtLessProcess.GetNumber()+txtLossProcess.GetNumber());" +
                                                          "}";
        }


        protected void txtBalanceProcess_Init(object sender, EventArgs e)
        {

            txtBalanceProcess.ClientSideEvents.ValueChanged = "function (s,e) { " +
                                                            "lblTotalModify.SetText(s.GetValue()+txtLessProcess.GetNumber()+txtLossProcess.GetNumber());" +
                                                  "}";
        }

        protected void txtLossQuanlity_Init(object sender, EventArgs e)
        { }

        protected void txtLessQuanlity_Init(object sender, EventArgs e)
        { }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            if (bytes != null)
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
            else
            {
                return "";
            }
        }

        protected string GetImageName(object dataValue)
        {
            if (dataValue == null)
            {
                return "";
            }
            InventoryAuditItemUnit _inventoryAuditItemUnit;
            _inventoryAuditItemUnit = session.GetObjectByKey<InventoryAuditItemUnit>((Guid)dataValue);

            string url = "";

            if (_inventoryAuditItemUnit != null)
            {
                if (GetString(_inventoryAuditItemUnit.Suggestion) == "")
                {
                    url = "~/images/icon/Edit/textred_16x16.png";
                }
                else
                {
                    url = "~/images/icon/Edit/textblu_16x16.png";
                }
            }

            return url;
        }

        protected void callbackSuggestion_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            InventoryAuditItemUnit _inventoryAuditItemUnit;
            _inventoryAuditItemUnit = session.GetObjectByKey<InventoryAuditItemUnit>(Guid.Parse(param[1]));

            switch (param[0])
            {
                case "open":
                    if (_inventoryAuditItemUnit != null)
                    {
                        txtSuggestion.Html = GetString(_inventoryAuditItemUnit.Suggestion);
                    }
                    else
                    {
                        txtSuggestion.Html = "";
                    }
                    break;
                case "close":
                    if (_inventoryAuditItemUnit != null && _inventoryAuditItemUnit.InventoryAuditArtifactId.ApprovalStatus != Utility.Constant.APRROVE_YES)
                    {
                        _inventoryAuditItemUnit.Suggestion = GetBytes(txtSuggestion.Html);
                        _inventoryAuditItemUnit.Save();

                        callbackSuggestion.JSProperties.Add("cpRefreshGrid", "refresh");
                    }
                    break;
            }
        }

        protected void grdDetailJournal_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {

        }

        protected void grdDetailJournal_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.VisibleIndex <= -1) return;

            InventoryAuditItemUnit row = (InventoryAuditItemUnit)grdDetailJournal.GetRow(e.VisibleIndex);
            if (row != null)
            {
                if (row.InventoryAuditArtifactId.ApprovalStatus.ToString().Contains(Constant.APRROVE_YES))
                {
                    e.Visible = false;
                }
            }

        }

        protected void cboInventory_Init(object sender, EventArgs e)
        {
        }


        protected void cpInventoryAction_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            ASPxButton sBtn = null;

            switch (para[0])
            {
                case "in":
                    //sBtn = popupInventoryCommandCheck.FindControl("btnModifyCommand") as ASPxButton;
                    //uEdittingOutputInventoryCommand1.SettingInit(SessionId, sBtn);

                    break;

                case "out":


                    break;

                case "print":

                    break;
            }
        }

        protected void btnModifyOutCommand_PreRender(object sender, EventArgs e)
        {
            base.OnPreRender(e);

        }

        protected void btnModifyOutCommand_Load(object sender, EventArgs e)
        {

        }

        protected void btnModifyInCommand_Init(object sender, EventArgs e)
        {

        }

        protected void btnModifyOutCommand_Init(object sender, EventArgs e)
        {

        }

        protected void btnPrintCommand_Init(object sender, EventArgs e)
        {

        }

        protected void grdDetailJournal_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
        {

        }

        protected void grdDetailJournal_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            CriteriaOperator _filter;
            QualityItemType _qualityItemType;
            QualityItem _qualityItem;

            if (e.Column.FieldName == "Balance")
            {
                e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("RealAmount")) - Convert.ToDecimal(e.GetListSourceFieldValue("BookingAmount"));
            }



            if (e.Column.FieldName == "LossQuantity")
            {
                _filter = CriteriaOperator.And(new BinaryOperator("QualityItemType.Name", "LOSS_QUANLITY", BinaryOperatorType.Equal),
                                                new BinaryOperator("InventoryAuditItemUnitId.InventoryAuditItemUnitId", Guid.Parse(e.GetListSourceFieldValue("InventoryAuditItemUnitId").ToString()), BinaryOperatorType.Equal));

                _qualityItem = session.FindObject<QualityItem>(_filter);

                if (_qualityItem != null)
                {
                    e.Value = _qualityItem.AuditAmount;
                }
                else
                {
                    e.Value = 0;
                }
            }

            if (e.Column.FieldName == "LossProcess")
            {
                _filter = CriteriaOperator.And(new BinaryOperator("QualityItemType.Name", "LOSS_QUANLITY", BinaryOperatorType.Equal),
                                                new BinaryOperator("InventoryAuditItemUnitId.InventoryAuditItemUnitId", Guid.Parse(e.GetListSourceFieldValue("InventoryAuditItemUnitId").ToString()), BinaryOperatorType.Equal));


                _qualityItem = session.FindObject<QualityItem>(_filter);

                if (_qualityItem != null)
                {
                    e.Value = _qualityItem.QualityProcessingAmount;
                }
                else
                {
                    e.Value = 0;
                }
            }

            if (e.Column.FieldName == "LessQuantity")
            {
                _filter = CriteriaOperator.And(new BinaryOperator("QualityItemType.Name", "LESS_QUANLITY", BinaryOperatorType.Equal),
                                               new BinaryOperator("InventoryAuditItemUnitId.InventoryAuditItemUnitId", Guid.Parse(e.GetListSourceFieldValue("InventoryAuditItemUnitId").ToString()), BinaryOperatorType.Equal));

                _qualityItem = session.FindObject<QualityItem>(_filter);

                if (_qualityItem != null)
                {
                    e.Value = _qualityItem.AuditAmount;
                }
                else
                {
                    e.Value = 0;
                }
            }

            if (e.Column.FieldName == "LessProcess")
            {
                _filter = CriteriaOperator.And(new BinaryOperator("QualityItemType.Name", "LESS_QUANLITY", BinaryOperatorType.Equal),
                                               new BinaryOperator("InventoryAuditItemUnitId.InventoryAuditItemUnitId", Guid.Parse(e.GetListSourceFieldValue("InventoryAuditItemUnitId").ToString()), BinaryOperatorType.Equal));

                _qualityItem = session.FindObject<QualityItem>(_filter);

                if (_qualityItem != null)
                {
                    e.Value = _qualityItem.QualityProcessingAmount;
                }
                else
                {
                    e.Value = 0;
                }
            }
        }

        protected void report_panel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            report_panel.JSProperties.Add("cpShowReport", "showreport");
        }

    }
}