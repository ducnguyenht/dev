using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
namespace NAS.DAL.Inventory.Journal
{

    public partial class PurchaseInvoiceInventoryTransaction : InventoryTransaction
    {
        public PurchaseInvoiceInventoryTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private PurchaseInvoice _PurchaseInvoiceId;
        [Association("PurchaseInvoiceInventoryTransactionReferencesSalesInvoice")]
        public PurchaseInvoice PurchaseInvoiceId
        {
            get
            {
                return _PurchaseInvoiceId;
            }
            set
            {
                SetPropertyValue("PurchaseInvoiceId", ref _PurchaseInvoiceId, value);
            }
        }

        [Association(@"PurchaseInvoiceInventoryAccountingTransactionReferencesPurchaseInvoiceInventoryTransaction", typeof(PurchaseInvoiceInventoryAccountingTransaction)), Aggregated]
        public XPCollection<PurchaseInvoiceInventoryAccountingTransaction> PurchaseInvoiceInventoryAccountingTransactions { get { return GetCollection<PurchaseInvoiceInventoryAccountingTransaction>("PurchaseInvoiceInventoryAccountingTransactions"); } }

    }
}
