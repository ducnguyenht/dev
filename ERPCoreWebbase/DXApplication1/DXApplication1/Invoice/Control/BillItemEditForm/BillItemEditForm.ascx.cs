using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.Invoice.Control.BillItemEditForm.Strategy;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Nomenclature.Items;
using NAS.DAL.Invoice;
using DevExpress.Web.ASPxCallbackPanel;
using NAS.BO.Invoice;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

namespace WebModule.Invoice.Control.BillItemEditForm
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BillItemEditFormClientSideEvents : ClientSideEvents
    {
        public string SelectedTradingItemIndexChanged { get; set; }
        public string SelectedTradingUnitIndexChanged { get; set; }
        public string GridViewDataChanged { get; set; }
        public string StartRowEditing { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [AutoFormatDisable]
        [Themeable(false)]
        [MergableProperty(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewClientSideEvents GridViewClientSideEvents { get; set; }
    }

    public partial class BillItemEditForm : System.Web.UI.UserControl
    {
        public string ClientInstanceName { get; set; }

        public string _ClientInstanceName
        {
            get
            {
                if (ClientInstanceName == null || ClientInstanceName.Trim().Length == 0)
                    return ClientID;
                return ClientInstanceName;
            }
        }

        private BillItemEditFormClientSideEvents _ClientSideEvents;

        [Category("Client-Side")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [AutoFormatDisable]
        [Themeable(false)]
        [MergableProperty(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public BillItemEditFormClientSideEvents ClientSideEvents
        {
            get
            {
                if (_ClientSideEvents == null)
                {
                    _ClientSideEvents = new BillItemEditFormClientSideEvents();
                    _ClientSideEvents.GridViewClientSideEvents = gridviewBillItem.ClientSideEvents;
                }
                return _ClientSideEvents;
            }
        }

        private BillItemEditFormStrategy _BillItemEditFormStrategy;
        private BillItemEditFormStrategy BillItemEditFormStrategy
        {
            get
            {
                return _BillItemEditFormStrategy;
            }
        }

        public void SetBillItemEditFormStrategy(BillItemEditFormStrategy strategy)
        {
            _BillItemEditFormStrategy = strategy;
        }

        private TradingItemsComboBoxDataLoadingStrategy _TradingItemsComboBoxDataLoadingStrategy;
        private TradingItemsComboBoxDataLoadingStrategy TradingItemsComboBoxDataLoadingStrategy
        {
            get
            {
                return _TradingItemsComboBoxDataLoadingStrategy;
            }
        }

        public void SetTradingItemsComboBoxDataLoadingStrategy(
            TradingItemsComboBoxDataLoadingStrategy strategy)
        {
            _TradingItemsComboBoxDataLoadingStrategy = strategy;
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

        public Guid BillId
        {
            get;
            set;
            //get
            //{
            //    if (ViewStateControlId == null)
            //        GenerateViewStateControlId();
            //    return (Guid)Session["BillId_" + ViewStateControlId];
            //}
            //set
            //{
            //    if (ViewStateControlId == null)
            //        GenerateViewStateControlId();
            //    Session["BillId_" + ViewStateControlId] = value;
            //}
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        public void DataBind()
        {
            gridviewBillItem_LoadData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.Contains("Invoice/SalesInvoice/GUI/FixedAssetsSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/FixedAssetsPurchaseInvoiceListingForm.aspx"))
            {
                gridviewBillItem.Columns[0].Caption = "Tài sản cố định";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/ToolSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/ToolPurchaseInvoiceListingForm.aspx"))
            {
                gridviewBillItem.Columns[0].Caption = "Công cụ, dụng cụ";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/MaterialSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/MaterialPurchaseInvoiceListingForm.aspx"))
            {
                gridviewBillItem.Columns[0].Caption = "Nguyên vật liệu";
            }
        }

        private void gridviewBillItem_LoadData()
        {
            if (BillItemEditFormStrategy != null)
            {
                gridviewBillItem.DataSource = BillItemEditFormStrategy.GetBillItems(session, BillId);
                gridviewBillItem.DataBind();
            }
        }

        protected void comboItem_ItemsRequestedByFilterCondition(object source,
            ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (TradingItemsComboBoxDataLoadingStrategy == null)
                return;
            TradingItemsComboBoxDataLoadingStrategy.ComboBoxItem_ItemsRequestedByFilterCondition(session, source, e);
        }

        protected void comboItem_ItemRequestedByValue(object source,
            ListEditItemRequestedByValueEventArgs e)
        {
            if (TradingItemsComboBoxDataLoadingStrategy == null)
                return;
            TradingItemsComboBoxDataLoadingStrategy.ComboBoxItem_ItemRequestedByValue(session, source, e);
        }

        protected void gridviewBillItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                Guid itemId = (Guid)e.NewValues["ItemId!Key"];
                Guid unitId = (Guid)e.NewValues["UnitId!Key"];
                double quantity = (double)e.NewValues["Quantity"];
                double price = (double)e.NewValues["Price"];
                //double promotionInPercentage = double.MinValue;
                double promotionInPercentage = -1;
                string comment = (string)e.NewValues["Comment"];
                if (e.NewValues["PromotionInPercentage"] != null)
                {
                    promotionInPercentage = (double)e.NewValues["PromotionInPercentage"];
                }
                BillItemEditFormStrategy.CreateBillItem(uow, BillId, itemId, unitId, quantity, price, promotionInPercentage, comment);
                uow.CommitChanges();
                gridviewBillItem.JSProperties["cpEvent"] = "DataChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
                e.Cancel = true;
                gridviewBillItem.CancelEdit();
            }
        }

        protected void gridviewBillItem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                Guid billItemId = (Guid)e.Keys["BillItemId"];
                Guid itemId = (Guid)e.NewValues["ItemId!Key"];
                Guid unitId = (Guid)e.NewValues["UnitId!Key"];
                double quantity = (double)e.NewValues["Quantity"];
                double price = (double)e.NewValues["Price"];
                //double promotionInPercentage = double.MinValue;
                double promotionInPercentage = -1;
                string comment = (string)e.NewValues["Comment"];
                if (e.NewValues["PromotionInPercentage"] != null)
                {
                    promotionInPercentage = (double)e.NewValues["PromotionInPercentage"];
                }
                BillItemEditFormStrategy.UpdateBillItem(uow, billItemId, itemId, unitId, quantity, price, promotionInPercentage, comment);
                uow.CommitChanges();
                gridviewBillItem.JSProperties["cpEvent"] = "DataChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
                e.Cancel = true;
                gridviewBillItem.CancelEdit();
            }
        }

        protected void gridviewBillItem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (BillId == null || BillId.Equals(Guid.Empty))
                return;

            Bill bill = session.GetObjectByKey<Bill>(BillId);
            if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            {
                throw new Exception(String.Format("Không thể thực hiện vì hóa đơn '{0}' đã bị khóa",
                    bill.Code));
            }

            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                Guid billItemId = (Guid)e.Keys["BillItemId"];
                BillItemEditFormStrategy.DeleteBillItem(uow, billItemId);
                uow.CommitChanges();
                gridviewBillItem.JSProperties["cpEvent"] = "DataChanged";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
                e.Cancel = true;
            }
        }

        protected void gridviewBillItem_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "UnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                if (gridviewBillItem.IsEditing && !gridviewBillItem.IsNewRowEditing)
                {
                    object itemId = gridviewBillItem.GetRowValuesByKeyValue(e.KeyValue, "ItemId!Key");
                    if (itemId == DBNull.Value) return;
                    FillUnitCombo(combo, (Guid)itemId);
                }
                combo.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(comboUnit_Callback);
            }
            else if (e.Column.FieldName == "PromotionInPercentage")
            {
                Bill bill = session.GetObjectByKey<Bill>(BillId);
                if (bill == null) return;
                if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    if (e.Value == null || e.Value == DBNull.Value) return;
                    ASPxSpinEdit spinEdit = e.Editor as ASPxSpinEdit;
                    if ((double)e.Value == -1 && spinEdit.IsValid)
                    {
                        spinEdit.Text = null;
                    }
                }
                else if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_AMOUNT)
                    || bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                {
                    ASPxSpinEdit spinEdit = e.Editor as ASPxSpinEdit;
                    spinEdit.Text = null;
                    spinEdit.ReadOnly = true;
                    spinEdit.ReadOnlyStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                }
            }
        }

        protected void comboUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            FillUnitCombo(sender as ASPxComboBox, Guid.Parse(e.Parameter));
        }

        private void FillUnitCombo(ASPxComboBox combo, Guid itemId)
        {
            //Get Item
            Item item = session.GetObjectByKey<Item>(itemId);
            if (item.ItemUnits != null && item.ItemUnits.Count > 0)
            {
                combo.DataSource = item.ItemUnits
                    .Where(r => (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                            || r.RowStatus == Utility.Constant.ROWSTATUS_DEFAULT)
                        && (r.UnitId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                            || r.UnitId.RowStatus == Utility.Constant.ROWSTATUS_DEFAULT))
                    .Select(r => r.UnitId);
                combo.DataBindItems();
            }
            else
            {
                combo.DataSource = new Item[0];
                combo.DataBindItems();
            }
        }

        protected void gridviewBillItem_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("UnitId!Key"))
            {
                if (e.Value == null || e.Value == DBNull.Value) return;
                NAS.DAL.Nomenclature.Item.Unit unit =
                    session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Unit>(e.Value);
                e.DisplayText = String.Format("{0} - {1}", unit.Code, unit.Name);
            }
            else if (e.Column.FieldName.Equals("PromotionInPercentage"))
            {
                var billId = gridviewBillItem.GetRowValues(e.VisibleRowIndex, "BillId!Key");
                if (billId == null)
                    return;
                Bill bill = session.GetObjectByKey<Bill>(billId);
                if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    if (e.Value == null || e.Value == DBNull.Value) return;
                    double promotionInPercentage = (double)e.Value;
                    if (promotionInPercentage == -1)
                        e.DisplayText = "N/A";
                    else
                        e.DisplayText = promotionInPercentage.ToString();
                }
                else if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_AMOUNT)
                    || bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                {
                    e.DisplayText = "N/A";
                    e.Column.CellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                }
            }
            else if (e.Column.Name.Equals("VATInPercentage"))
            {
                var val = gridviewBillItem.GetRowValues(e.VisibleRowIndex, "BillItemId");
                if (val == null)
                    return;
                BillItem billItem = session.GetObjectByKey<BillItem>(val);
                BillItemTax billItemTax = billItem.BillItemTaxs.FirstOrDefault();
                if (billItemTax != null)
                {
                    e.DisplayText = billItemTax.TaxInPercentage.ToString();
                }
                else
                {
                    e.DisplayText = "N/A";
                }
            }
        }

        protected void gridviewBillItem_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            double quantity = 0;
            double price = 0;
            double promotion = 0;
            if (e.NewValues["Quantity"] != null)
            {
                quantity = (double)e.NewValues["Quantity"];
                if (quantity <= 0)
                    Utility.Helpers.AddErrorToGridViewColumn(
                        e.Errors,
                        gridviewBillItem.Columns["Quantity"],
                        "Số lượng phải lớn hơn 0");
            }
            if (e.NewValues["Price"] != null)
            {
                price = (double)e.NewValues["Price"];
                if (price <= 0)
                    Utility.Helpers.AddErrorToGridViewColumn(
                        e.Errors,
                        gridviewBillItem.Columns["Price"],
                        "Đơn giá phải lớn hơn 0");
            }
            if (e.NewValues["PromotionInPercentage"] != null)
            {
                promotion = (double)e.NewValues["PromotionInPercentage"];
                if (promotion <= 0)
                    Utility.Helpers.AddErrorToGridViewColumn(
                        e.Errors,
                        gridviewBillItem.Columns["PromotionInPercentage"],
                        "Phần trăm chiết khấu phải lớn hơn 0");
            }
        }

        protected void panelItemVAT_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            ASPxSpinEdit spin = panel.FindControl("spinItemTax") as ASPxSpinEdit;
            ItemBO itemBO = new ItemBO();
            ItemTax itemTax = itemBO.GetCurrentVATOfItem(session, Guid.Parse(e.Parameter));
            if (itemTax != null)
                spin.Number = (decimal)itemTax.TaxId.Percentage;
            else
                spin.Number = 0;
        }

        protected void spinItemTax_Load(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            ASPxCallbackPanel panel = spin.NamingContainer as ASPxCallbackPanel;
            //Set data when in edit mode
            if (gridviewBillItem.IsEditing && !gridviewBillItem.IsNewRowEditing && !panel.IsCallback)
            {
                var billItemId = (panel.NamingContainer
                    as DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer).KeyValue;
                object itemId = gridviewBillItem.GetRowValuesByKeyValue(billItemId, "ItemId!Key");
                if (itemId == DBNull.Value) return;
                ItemBO itemBO = new ItemBO();
                ItemTax itemTax = itemBO.GetCurrentVATOfItem(session, (Guid)itemId);
                if (itemTax != null)
                    spin.Number = (decimal)itemTax.TaxId.Percentage;
                else
                    spin.Number = 0;
            }
        }

        protected void gridviewBillItem_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name.Equals("VATInPercentage"))
            {
                var val = gridviewBillItem.GetRowValues(e.VisibleIndex, "BillItemId");
                if (val == null)
                    return;
                if (!BillBOBase.IsMostAppearTaxPercentage((Guid)val))
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(230, 185, 184);
                }
            }
        }

        protected void gridviewBillItem_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (BillId == null || BillId.Equals(Guid.Empty))
                return;

            Bill bill = session.GetObjectByKey<Bill>(BillId);
            if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            {
                throw new Exception(String.Format("Không thể thực hiện vì hóa đơn '{0}' đã bị khóa",
                    bill.Code));
            }

            gridviewBillItem.JSProperties["cpEvent"] = "StartRowEditing";
        }

        protected void gridviewBillItem_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (BillId == null || BillId.Equals(Guid.Empty))
                return;

            Bill bill = session.GetObjectByKey<Bill>(BillId);
            if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
            {
                throw new Exception(String.Format("Không thể thực hiện vì hóa đơn '{0}' đã bị khóa",
                    bill.Code));
            }
        }
    }
}