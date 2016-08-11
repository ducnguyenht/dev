using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Command
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class InventoryCommandFinancialTransaction: NAS.DAL.Accounting.Journal.Transaction
    {
        public InventoryCommandFinancialTransaction(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private InventoryCommand _InventoryCommandId;
        [Association(@"InventoryCommandFinancialTransactionReferencesInventoryCommand")]
        public NAS.DAL.Inventory.Command.InventoryCommand InventoryCommandId
        {
            get
            {
                return _InventoryCommandId;
            }
            set
            {
                SetPropertyValue("InventoryCommandId", ref _InventoryCommandId, value);
            }
        }
    }
}
