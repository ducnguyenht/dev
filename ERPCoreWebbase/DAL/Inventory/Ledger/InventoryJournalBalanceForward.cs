using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Journal;
namespace NAS.DAL.Inventory.Ledger
{

    public partial class InventoryJournalBalanceForward : InventoryJournal
    {
        public InventoryJournalBalanceForward(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        double _Balance;
        public double Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                SetPropertyValue("Balance", ref _Balance, value);
            }
        }

    }

}
