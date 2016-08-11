using System;
using DevExpress.Xpo;
namespace NAS.DAL.Buy.StockCart
{

    public partial class PurchaseInvoicePuttingStockCart
    {
        public PurchaseInvoicePuttingStockCart(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
