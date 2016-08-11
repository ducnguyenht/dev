using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Inventory.Command;
using NAS.BO.Accounting.Journal;
using NAS.BO.Inventory.Jouranl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL.Inventory.Lot;
using NAS.BO.Inventory.Journal;
using DevExpress.Web.ASPxGridView;
using NAS.DAL.Nomenclature.Item;

namespace WebModule.Purchasing.UserControl
{
    public partial class ucDeliverySchedule : System.Web.UI.UserControl
    {
        Session session;
        //Guid billId = Guid.Empty;

        public Guid BillId { get; set; }

        //Guid currencyId = Guid.Parse("23e455db-7409-419c-80e9-58830aa104db");
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsDetailBeforePerformDataSelect = false;
        }

        public void DataBind()
        {
            if (BillId != null)
            {
                if (IsDetailBeforePerformDataSelect) return;

                InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();
                InventoryTransactionBO purchaseInvoiceTransactionBO = new InventoryTransactionBO();
                GridDeliveryPlanning.DataSource = purchaseInvoiceTransactionBO.GetDeliveryPlanningForBill<PurchaseInvoice>(session, BillId);
                GridDeliveryPlanning.KeyFieldName = "InventoryTransactionId";
                GridDeliveryPlanning.DataBind();
                GridDeliveryActual.DataSource = inventoryCommandBO.GetActualInventoryJournalOfBill(session, BillId, NAS.DAL.CMS.ObjectDocument.DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);
                GridDeliveryActual.KeyFieldName = "InventoryJournalId";
                GridDeliveryActual.DataBind();
            }
        }

        protected void GridDeliveryPlanning_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            GridDeliveryPlanning.CancelEdit();
            InventoryTransactionBO purchaseInvoiceTransactionBO = new InventoryTransactionBO();
            purchaseInvoiceTransactionBO.CreateInventoryTransaction(session, BillId, (DateTime)e.NewValues["IssueDate"], (string)e.NewValues["Code"], (string)e.NewValues["Description"]);
            GridDeliveryPlanning.DataSource = purchaseInvoiceTransactionBO.GetDeliveryPlanningForBill<PurchaseInvoice>(session, BillId);
            GridDeliveryPlanning.DataBind();
        }

        protected void GridDeliveryPlanning_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            GridDeliveryPlanning.CancelEdit();
            InventoryTransactionBO purchaseInvoiceTransactionBO = new InventoryTransactionBO();
            purchaseInvoiceTransactionBO.UpdateInventoryTransaction(session, Guid.Parse(e.Keys["InventoryTransactionId"].ToString()), (DateTime)e.NewValues["IssueDate"], (string)e.NewValues["Code"], (string)e.NewValues["Description"]);
            GridDeliveryPlanning.DataSource = purchaseInvoiceTransactionBO.GetDeliveryPlanningForBill<PurchaseInvoice>(session, BillId);
            GridDeliveryPlanning.DataBind();
        }

        protected void GridDeliveryPlanning_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            PurchaseInvoice bill = session.GetObjectByKey<PurchaseInvoice>(BillId);
            try
            {
                InventoryTransactionBO purchaseInvoiceTransactionBO = new InventoryTransactionBO();
                if (bill.RowStatus == 4)
                {
                    throw (new Exception("Phiếu đã khóa, không thể thao tác"));
                }
                else
                {                    
                    purchaseInvoiceTransactionBO.DeleteInventoryTransaction(session, Guid.Parse(e.Keys["InventoryTransactionId"].ToString()));
                }
                e.Cancel = true;
                GridDeliveryPlanning.DataSource = purchaseInvoiceTransactionBO.GetDeliveryPlanningForBill<PurchaseInvoice>(session, BillId);
                GridDeliveryPlanning.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        protected void GridDeliveryPlanning_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            PurchaseInvoice bill = session.GetObjectByKey<PurchaseInvoice>(BillId);
            try
            {
                if (bill.RowStatus == 4)
                {                    
                    throw(new Exception("Phiếu đã khóa, không thể thao tác"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GridDeliveryPlanning_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            PurchaseInvoice bill = session.GetObjectByKey<PurchaseInvoice>(BillId);
            try
            {
                if (bill.RowStatus == 4)
                {
                    throw (new Exception("Phiếu đã khóa, không thể thao tác"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GridDeliveryPlanning_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        protected void GridDeliveryPlanning_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }
        public bool IsDetailBeforePerformDataSelect { get; set; }
        protected void GridPlanningJournal_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;
            InventoryJournalBO inventoryJournalBO = new InventoryJournalBO();
            gridview.JSProperties["cpInventoryTransactionId"] = gridview.GetMasterRowKeyValue().ToString();
            gridview.KeyFieldName = "InventoryJournalId";
            gridview.DataSource = inventoryJournalBO.GetDeliveryPlanningJournalForTransaction(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
            IsDetailBeforePerformDataSelect = true;
            //gridview.DataBind();
        }

        protected void GridPlanningJournal_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            ASPxGridView gridview = sender as ASPxGridView;
            //gridview.CancelEdit();
            InventoryJournalBO inventoryJournalBO = new InventoryJournalBO();
            inventoryJournalBO.DeleteInventoryPlanningJournal(session, Guid.Parse(e.Keys["InventoryJournalId"].ToString()));
            gridview.DataSource = inventoryJournalBO.GetDeliveryPlanningJournalForTransaction(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
            gridview.DataBind();
        }

        protected void GridPlanningJournal_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {            
            e.Cancel = true;
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.CancelEdit();
            Guid transactionId = Guid.Parse(gridview.JSProperties["cpInventoryTransactionId"].ToString());
            Guid itemUnitId = Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString());
            double amount = (double)e.NewValues["Credit"];
            Guid lotId = Guid.Parse(e.NewValues["LotId!Key"].ToString());
            Guid inventoryId = Guid.Parse(e.NewValues["InventoryId!Key"].ToString());

            InventoryJournalBO inventoryJournalBO = new InventoryJournalBO();
            inventoryJournalBO.CreateInventoryPlanningJournal(session, transactionId, itemUnitId,amount,lotId ,inventoryId, (string)e.NewValues["Description"]);
            gridview.DataSource = inventoryJournalBO.GetDeliveryPlanningJournalForTransaction(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
            gridview.DataBind();
        }

        protected void GridPlanningJournal_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            InventoryJournalBO inventoryJournalBO = new InventoryJournalBO();
            ASPxGridView gridview = sender as ASPxGridView;
            gridview.CancelEdit();
            inventoryJournalBO.UpdateInventoryPlanningJournal(session, (Guid)e.Keys[0], Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()), (double)(e.NewValues["Credit"]), Guid.Parse(e.NewValues["LotId!Key"].ToString()), Guid.Parse(e.NewValues["InventoryId!Key"].ToString()), (string)(e.NewValues["Description"]));
            gridview.DataSource = inventoryJournalBO.GetDeliveryPlanningJournalForTransaction(session, Guid.Parse(gridview.GetMasterRowKeyValue().ToString()));
            gridview.DataBind();
        }

        protected void GridPlanningJournal_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            PurchaseInvoice bill = session.GetObjectByKey<PurchaseInvoice>(BillId);
            try
            {
                if (bill.RowStatus == 4)
                {
                    throw (new Exception("Phiếu đã khóa, không thể thao tác"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GridPlanningJournal_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            PurchaseInvoice bill = session.GetObjectByKey<PurchaseInvoice>(BillId);
            try
            {
                if (bill.RowStatus == 4)
                {
                    throw (new Exception("Phiếu đã khóa, không thể thao tác"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GridPlanningJournal_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;                
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "GridPlanningJournal.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemUnitId.UnitId.Name'));" +
                                                       "}";
                combo.DataBindItems();
                combo.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(comboItem_Callback);                                
            }
        }
        protected void comboItem_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            FillLotCombo(sender as ASPxComboBox, Guid.Parse(e.Parameter));
        }
        private void FillLotCombo(ASPxComboBox combo, Guid itemId)
        {
            NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(itemId);
            if (itemUnit.ItemId != null)
            {
                combo.DataSource = itemUnit.ItemId.Lots;
            }
            else
            {
                combo.DataSource = null;                
            }
            combo.DataBindItems();
        }

        protected void GridPlanningJournal_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                NAS.DAL.Nomenclature.Item.ItemUnit itemUnit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(Guid.Parse(e.Value.ToString()));
                e.DisplayText = itemUnit.ItemId.Name;
            }
        }
        
        protected void comboItemUnit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;

            CriteriaOperator filter = new BinaryOperator("ItemUnitId", e.Value, BinaryOperatorType.Equal);
            BillItem obj = session.FindObject<BillItem>(filter);

            if (obj != null)
            {
                comboItemUnit.DataSource = new BillItem[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void comboItemUnit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<BillItem> collection = new XPCollection<BillItem>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = new BinaryOperator("BillId", BillId, BinaryOperatorType.Equal);

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemUnitId.ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void comboInventory_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Inventory obj = session.GetObjectByKey<Inventory>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Inventory[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void comboInventory_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Inventory> collection = new XPCollection<Inventory>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
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

        protected void comboLot_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Lot obj = session.GetObjectByKey<Lot>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Lot[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void comboLot_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Lot> collection = new XPCollection<Lot>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                   new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                   new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }
    }
}