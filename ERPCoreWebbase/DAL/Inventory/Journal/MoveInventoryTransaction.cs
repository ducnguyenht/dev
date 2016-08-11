using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
namespace NAS.DAL.Inventory.Journal
{

    public partial class MoveInvoiceInventoryTransaction : InventoryTransaction
    {
        public MoveInvoiceInventoryTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }

}
