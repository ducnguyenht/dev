using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.TempData
{
    public class ETL_Transaction
    {
        public Guid TransactionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }        
        public bool IsBalanceForward { get; set; }
        public double Amount { get; set; }
        public Guid OwnerOrgId { get; set; }
        public Guid SupplierOrgId { get; set; }
        public Guid CustomerOrgId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<ETL_GeneralJournal> GeneralJournalList { get; set; }
    }
}
