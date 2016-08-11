using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Invoice;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class PurchaseInvoiceTransaction : Transaction
    {
        // Fields...
        PurchaseInvoice fPurchaseInvoiceId;
        [Association(@"PurchaseInvoiceTransactionReferencesPurchaseInvoice")]
        public PurchaseInvoice PurchaseInvoiceId
        {
            get
            {
                return fPurchaseInvoiceId;
            }
            set
            {
                SetPropertyValue<PurchaseInvoice>("PurchaseInvoiceId", ref fPurchaseInvoiceId, value);
            }
        }
    }
}
