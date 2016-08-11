using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Journal;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class PurchaseInvoiceInventoryAccountingTransaction : Transaction
    {
        // Fields...
        PurchaseInvoiceInventoryTransaction fPurchaseInvoiceInventoryTransactionId;
        [Association(@"PurchaseInvoiceInventoryAccountingTransactionReferencesPurchaseInvoiceInventoryTransaction")]
        public PurchaseInvoiceInventoryTransaction PurchaseInvoiceInventoryTransactionId
        {
            get
            {
                return fPurchaseInvoiceInventoryTransactionId;
            }
            set
            {
                SetPropertyValue<PurchaseInvoiceInventoryTransaction>("PurchaseInvoiceInventoryTransactionId", ref fPurchaseInvoiceInventoryTransactionId, value);
            }
        }
    }
}
