using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Invoice;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class SaleInvoiceTransaction : Transaction
    {
        // Fields...
        SalesInvoice fSalesInvoiceId;                       
        [Association(@"SalesInvoiceTransactionReferencesSalesInvoice")]
        public SalesInvoice SalesInvoiceId
        {
            get
            {
                return fSalesInvoiceId;
            }
            set
            {
                SetPropertyValue<SalesInvoice>("SalesInvoiceId", ref fSalesInvoiceId, value);
            }
        }
    }
}
