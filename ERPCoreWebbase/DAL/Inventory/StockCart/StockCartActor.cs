using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.StockCart
{

    public partial class StockCartActor
    {
        public StockCartActor(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
