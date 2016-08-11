using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Buy.StockCart;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Sales.PickingStockCart;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Inventory.Jouranl;

namespace NAS.BO.Buy
{
    public class PurchaseInvoiceInventoryBO
    {
        public void CreatePurchaseInvoiceInventoryTransaction(Session session, Guid purchaseInvoiceId, Guid accountingPeriodId, DateTime issueDate, Guid inventoryId, XPCollection<BillItem> billItems, string code, string description)
        {
            InventoryTransactionBO invTransactionBO = new InventoryTransactionBO();
            invTransactionBO.ReceiptType = Utility.Constant.RECEIPT_PURCHASE;
            //invTransactionBO.CreatePurchaseInvoiceInventoryTransaction(session, purchaseInvoiceId, accountingPeriodId, issueDate, inventoryId, billItems, code, description);
            
            invTransactionBO.CreateInventoryTransaction(session, inventoryId, code, description, issueDate, billItems);
        }
    }
}
