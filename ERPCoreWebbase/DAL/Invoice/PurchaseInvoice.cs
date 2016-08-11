using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Invoice
{

    public partial class PurchaseInvoice
    {
        public PurchaseInvoice(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
