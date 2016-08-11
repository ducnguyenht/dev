using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Inventory.TempData
{
    public class ETL_InventoryTransaction
    {
        public Guid AccountingPeriodId { get; set; }
        public Guid InventoryTransactionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime IssueDate { get; set; }
        public List<ETL_InventoryJournal> InventoryJournalList { get; set; }
        public List<ETL_COGS> COGSList { get; set; }
    }
}
