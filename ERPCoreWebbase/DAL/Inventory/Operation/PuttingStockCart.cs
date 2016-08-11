using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.Operation
{

    public partial class PuttingStockCart
    {
        public PuttingStockCart(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
