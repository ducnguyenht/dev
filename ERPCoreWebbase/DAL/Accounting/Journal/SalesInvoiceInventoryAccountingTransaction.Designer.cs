using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Journal;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class SalesInvoiceInventoryAccountingTransaction : Transaction
    {
        // Fields...
        SalesInvoiceInventoryTransaction fSalesInvoiceInventoryTransactionId;
        [Association(@"SalesInvoiceInventoryAccountingTransactionReferencesSalesInvoiceInventoryTransaction")]
        public SalesInvoiceInventoryTransaction SalesInvoiceInventoryTransactionId
        {
            get
            {
                return fSalesInvoiceInventoryTransactionId;
            }
            set
            {
                SetPropertyValue<SalesInvoiceInventoryTransaction>("SalesInvoiceInventoryTransactionId", ref fSalesInvoiceInventoryTransactionId, value);
            }
        }
    }
}
