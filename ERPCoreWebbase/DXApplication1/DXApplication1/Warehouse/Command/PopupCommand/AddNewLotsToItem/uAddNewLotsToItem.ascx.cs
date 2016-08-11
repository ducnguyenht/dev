using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Inventory.Lot;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Warehouse.Command.PopupCommand.AddNewLotsToItem
{
    public partial class uAddNewLotsToItem : System.Web.UI.UserControl
    {
        private Session session;
        private string _ObjectHandler;
        private string _EventHandler;
        public string ObjectHandler
        {
            get
            {
                return _ObjectHandler;
            }
            set
            {
                _ObjectHandler = value;
            }
            //get
            //{
            //    if (Session["TargetResponeJS_uAddNewLotsToItem"] == null)
            //        return null;

            //    return Session["TargetResponeJS_uAddNewLotsToItem"].ToString();
            //}
            //set
            //{
            //    Session["TargetResponeJS_uAddNewLotsToItem"] = value;
            //}
        }

        private Guid LotId
        {
            get
            {
                return (Guid)Session["LotId" + ViewStateControlId];
            }
            set
            {
                Session["LotId" + ViewStateControlId] = value;
            }
        }

        public string EventHandler
        {
            get
            {
                return _EventHandler;
            }
            set
            {
                _EventHandler = value;
            }
            //get
            //{
            //    if (Session["SharedClientEvent_uAddNewLotsToItem"] == null)
            //        return null;

            //    return Session["SharedClientEvent_uAddNewLotsToItem"].ToString();
            //}
            //set
            //{
            //    Session["SharedClientEvent_uAddNewLotsToItem"] = value;
            //}
        }

        private ASPxComboBox ComboBoxItemUnit
        {
            get
            {
                return this.Session["ComboBoxItemUnit" + ViewStateControlId] as ASPxComboBox;
            }
            set
            {
                this.Session["ComboBoxItemUnit" + ViewStateControlId] = value;
            }
        }

        private ASPxGridView GridViewItemUnit
        {
            get
            {
                return this.Session["GridViewItemUnit" + ViewStateControlId] as ASPxGridView;
            }
            set
            {
                this.Session["GridViewItemUnit" + ViewStateControlId] = value;
            }
        }

        private void initCommonJavascript()
        {
            if (!MainControlClientName.Equals(string.Empty))
            {
                cpItemByLoad.ClientSideEvents.BeginCallback = "function(s, e){  " +
                    string.Format("{0}.Show();", ldpnItemByLot.ClientInstanceName) +
                    " }";

                cpItemByLoad.ClientSideEvents.EndCallback = "function(s, e){ " +
                    " if (s.cpIsSave){ delete s.cpIsSave; alert('Đã cập nhật thông tin'); " 
                    + ObjectHandler + ".fire({ type: '" 
                    + EventHandler
                    + "' , OutParam: s.cpOutputLotId }); delete s.cpOutputLotId; } " +
                    string.Format("{0}.Hide(); ", ldpnItemByLot.ClientInstanceName) +
                    "}";

                ButtonCloseCommandPopup.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Close');", cpItemByLoad.ClientInstanceName)
                    + " }";

                ButtonSaveCommandPopup.ClientSideEvents.Click =
                    "function(s, e){ " +
                    string.Format("{0}.PerformCallback('Save');", cpItemByLoad.ClientInstanceName)
                    + " }";

                popupItemByLot.ClientSideEvents.Closing = "function(s, e){ " +
                   string.Format("{0}.PerformCallback('Close');", cpItemByLoad.ClientInstanceName)
                   + " }";

                //CboLotCode.ClientSideEvents.ValueChanged = "function(s,e){ var date = Date.parseLocale(s.GetSelectedItem().GetColumnText('ExpireDate').split(' ')[0], 'dd/mmmm/yyyy'); " +
                //        DateEditExpiredDate.ClientInstanceName + ".SetDate(date); }";

                //chkLotSelectionType.ClientSideEvents.CheckedChanged =
                //    "function(s, e){ " +
                //    string.Format("{0}.PerformCallback('Refresh');", MainControlClientName)
                //    + " }";
            }
        }

        public void SettingInit(ASPxButton SourceButton, ASPxComboBox SourceComboBox)
        {
            initCommonJavascript();

            if (SourceComboBox == null)
                SourceButton.ClientSideEvents.Click =
                        "function(s, e){ " +
                        string.Format("{0}.PerformCallback('Show');", MainControlClientName) +
                        " }";
            else
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    "var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    SourceComboBox.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('Show');", MainControlClientName) +
                    " }}";

            DevExpress.Web.ASPxGridView.ASPxGridView grd = (SourceButton.NamingContainer as
                DevExpress.Web.ASPxGridView.GridViewHeaderTemplateContainer).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as
                DevExpress.Web.ASPxGridView.ASPxGridView;
            ImportedItemUnitId = Guid.Empty;
            if (grd == null)
                return;

            this.ComboBoxItemUnit = SourceComboBox;

            ItemUnit itemUnit = null;

            if (grd.EditingRowVisibleIndex >= 0)
            {
                ImportedItemUnitId = Guid.Parse(grd.GetRowValues(grd.EditingRowVisibleIndex, "ItemUnitId!Key").ToString());
                itemUnit = session.GetObjectByKey<ItemUnit>(ImportedItemUnitId);
            }
            else if (grd.IsNewRowEditing && this.ComboBoxItemUnit != null)
            {
                itemUnit = session.GetObjectByKey<ItemUnit>(this.ComboBoxItemUnit.Value);
                if (itemUnit == null)
                {
                    itemUnit = session.GetObjectByKey<ItemUnit>(this.ComboBoxItemUnit.Value);
                }
            }

            if (itemUnit != null)
            {
                lblItem.Text = itemUnit.ItemId.Code;
            }
        }

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private Guid ImportedItemUnitId
        {
            get
            {
                return (Guid)Session["ImportedItemUnitId" + ClientID + ViewStateControlId];
            }
            set
            {
                Session["ImportedItemUnitId" + ClientID + ViewStateControlId] = value;
            }
        }

        private ASPxButton ButtonSaveCommandPopup
        {
            get
            {
                ASPxButton button = popupItemByLot.FindControl("btnSavePopup") as ASPxButton;
                return button;
            }
        }
        
        private ASPxButton ButtonCloseCommandPopup
        {
            get
            {
                ASPxButton button = popupItemByLot.FindControl("btnClosePopup") as ASPxButton;
                return button;
            }
        }

        public string MainControlClientName
        {
            get
            {
                return cpItemByLoad.ClientInstanceName;
            }
        }

        private string ViewStateControlId
        {
            get { return (string)ViewState["ViewStateControlId"]; }
            set { ViewState["ViewStateControlId"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            LotXDS.Session = session;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
            }

            if (!IsPostBack)
            {
                ldpnItemByLot.ClientInstanceName = string.Format("ldpnItemByLot{0}", ViewStateControlId);
                cpItemByLoad.ClientInstanceName = string.Format("cpItemByLoad{0}", ViewStateControlId);
                DateEditExpiredDate.ClientInstanceName = string.Format("DateEditExpiredDate{0}", ViewStateControlId);
            }

            //try
            //{
            //    //if (cboItem.Value != null && !Guid.Parse(cboItem.Value.ToString()).Equals(Guid.Empty))
            //    //{
            //    //    XPCollection<Item> collection = new XPCollection<Item>(session);
            //    //    CriteriaOperator criteria = new BinaryOperator("ItemId", Guid.Parse(cboItem.Value.ToString()), BinaryOperatorType.Equal);
            //    //    collection.Criteria = criteria;
            //    //    cboItem.DataSource = collection;
            //    //    cboItem.DataBindItems();
            //    //}
            //}
            //catch
            //{ }

            initCommonJavascript();
        }

        protected void cpItemByLoad_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            string trs = para[0];

            switch (trs)
            {
                case "Show":
                    popupItemByLot.ShowOnPageLoad = true;
                    //cboItem.Focus();
                    txtLotCode.Text = string.Empty;
                    txtLotCode.Focus();
                    DateEditExpiredDate.Value = null;
                    break;
                case "Save":
                    //if (cboItem.Value == null)
                    //    throw new Exception("Chưa nhập mã hàng hóa");
                    if (lblItem.Text.Trim().Equals(string.Empty))
                        throw new Exception("Chưa nhập mã hàng hóa");
                    if (txtLotCode.Text.Trim().Equals(string.Empty))
                        throw new Exception("Chưa nhập mã lô");
                    if (DateEditExpiredDate.Value == null)
                        throw new Exception("Chưa nhập hạn dùng");
                    SaveLotItem();
                    break;
                case "Refresh":
                    //BuildGUI();
                    break;
                case "Close":
                    popupItemByLot.ShowOnPageLoad = false;
                    break;
                default:
                    break;
            }
        }

        void comboItem_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            ASPxComboBox comboItem = source as ASPxComboBox;
            XPCollection<Item> collection = new XPCollection<Item>(uow);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = null;

            criteria = CriteriaOperator.And(
                new ContainsOperator("ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
                    "PRODUCT")),
                new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                CriteriaOperator.Or(
                    new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItem.DataSource = collection;
            comboItem.DataBindItems();
        }

        void comboItem_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            try
            {
                Item obj = session.GetObjectByKey<Item>(e.Value.ToString());
                if (obj != null)
                {
                    comboItemUnit.DataSource = new Item[] { obj };
                    comboItemUnit.DataBindItems();
                    comboItemUnit.Value = Guid.Parse(e.Value.ToString());
                }
            }
            catch
            { 
            
            }
        }

        public void SaveLotItem()
        {
            Lot lot = null;
            Item item = session.FindObject<Item>(
                CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                    new BinaryOperator("Code", lblItem.Text.Trim(), BinaryOperatorType.Equal)
                ));
            lot = session.FindObject<NAS.DAL.Inventory.Lot.Lot>(CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                    new BinaryOperator("Code", txtLotCode.Text.Trim(), BinaryOperatorType.Equal),
                    new BinaryOperator("ItemId!Key", item.ItemId, BinaryOperatorType.Equal)
                ));

            if (lot == null)
            {
                lot = new Lot(session)
                {
                    ItemId = item,
                    Code = txtLotCode.Text.Trim(),
                    ExpireDate = DateTime.Parse(DateEditExpiredDate.Value.ToString()),
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                };
                lot.Save();
            }
            else {
                throw new Exception(string.Format("Số lô '{0}' ứng với hàng hóa '{1}' đã có sẵn trên hệ thống! Vui lòng nhập số lô khác",
                                    txtLotCode.Text.Trim(), item.Code));
            }
            
            cpItemByLoad.JSProperties.Add("cpIsSave", true);
            cpItemByLoad.JSProperties.Add("cpOutputLotId", lot.LotId);
            popupItemByLot.ShowOnPageLoad = false;
        }

        public void SettingInit(ASPxButton SourceButton, ASPxGridView SourceGridView)
        {
            if (MainControlClientName.Equals(string.Empty))
                return;
            initCommonJavascript();

            if (SourceGridView == null)
                SourceButton.ClientSideEvents.Click =
                        "function(s, e){ " +
                        string.Format("{0}.PerformCallback('Show');", MainControlClientName) +
                        " }";
            else
                SourceButton.ClientSideEvents.Click =
                    "function(s, e){ " +
                    "var validated = ASPxClientEdit.ValidateEditorsInContainer(" +
                    SourceGridView.ClientID +
                    ".GetMainElement(), null, true); if (validated) {" +
                    string.Format("{0}.PerformCallback('Show');", MainControlClientName) +
                    " }}";

            ImportedItemUnitId = Guid.Empty;

            this.GridViewItemUnit = SourceGridView;

            ItemUnit itemUnit = null;

            if (this.GridViewItemUnit.FocusedRowIndex >= 0)
            {
                Guid key = Guid.Parse(GridViewItemUnit.GetRowValues(this.GridViewItemUnit.FocusedRowIndex, "ItemUnitId").ToString());
                itemUnit = session.GetObjectByKey<ItemUnit>(key);

                if (itemUnit != null)
                {
                    lblItem.Text = itemUnit.ItemId.Code;
                    //cboItem.Value = itemUnit.ItemId.ItemId;
                    //cboItem.Text = itemUnit.ItemId.Code;
                    //cboItem.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(comboItem_ItemsRequestedByFilterCondition);
                    //cboItem.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(comboItem_ItemRequestedByValue);
                }
            }
        }
    }
}