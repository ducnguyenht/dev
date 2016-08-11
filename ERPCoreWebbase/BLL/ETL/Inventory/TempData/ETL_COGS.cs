using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Inventory.TempData
{
    public class ETL_COGS
    {
        public double Amount { get; set; }
        public short Assumption { get; set; }
        public double Balance { get; set; }
        public double COGSPrice { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Description { get; set; }
        public Guid InventoryId { get; set; }
        public Guid InventoryTransactionId { get; set; }
        public Guid ItemUnitId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid COGSId { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
