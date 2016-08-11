using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.Ledger
{

    public partial class InventoryLedger
    {
        public InventoryLedger(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        protected override void OnSaving()
        {
            base.OnSaving();
        }

        #endregion

    }

}
