using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
namespace NAS.DAL.Inventory.Journal
{

    public partial class SalesInvoiceInventoryTransaction : InventoryTransaction
    {
        public SalesInvoiceInventoryTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private SalesInvoice _SalesInvoiceId;
        [Association("SalesInvoiceInventoryTransactionReferencesSalesInvoice")]
        public SalesInvoice SalesInvoiceId
        {
            get
            {
                return _SalesInvoiceId;
            }
            set
            {
                SetPropertyValue("SalesInvoiceId", ref _SalesInvoiceId, value);
            }
        }

        [Association(@"SalesInvoiceInventoryAccountingTransactionReferencesSalesInvoiceInventoryTransaction", typeof(SalesInvoiceInventoryAccountingTransaction)), Aggregated]
        public XPCollection<SalesInvoiceInventoryAccountingTransaction> SalesInvoiceInventoryAccountingTransactions { get { return GetCollection<SalesInvoiceInventoryAccountingTransaction>("SalesInvoiceInventoryAccountingTransactions"); } }

    }

}
