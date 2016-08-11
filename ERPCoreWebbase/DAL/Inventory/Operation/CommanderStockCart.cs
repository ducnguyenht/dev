using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.Operation
{

    public partial class CommanderStockCart
    {
        public CommanderStockCart(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
