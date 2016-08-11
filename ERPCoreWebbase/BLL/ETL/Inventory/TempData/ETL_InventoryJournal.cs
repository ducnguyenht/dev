using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Inventory.TempData
{
    public class ETL_InventoryJournal
    {
        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public Guid InventoryId { get; set; }
        public Guid InventoryJournalId { get; set; }
        public Guid InventoryTransactionId { get; set; }
        public Guid ItemUnitId { get; set; }
        public char JournalType { get; set; }
        public Guid LotId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
