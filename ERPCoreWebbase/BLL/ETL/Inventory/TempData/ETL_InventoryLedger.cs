using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Inventory.TempData
{
    public class ETL_InventoryLedger
    {
        public Guid AccountId { get; set; }
        public double Balance { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Description { get; set; }
        public Guid InventoryId { get; set; }        
        public Guid InventoryTransactionId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid LotId { get; set; }
        public bool IsOriginal { get; set; }
        public Guid ItemUnitId { get; set; }
        public char LedgerType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
