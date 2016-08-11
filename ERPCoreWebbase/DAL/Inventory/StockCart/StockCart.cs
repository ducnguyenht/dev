using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.StockCart
{

    public partial class StockCart
    {
        public StockCart(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
