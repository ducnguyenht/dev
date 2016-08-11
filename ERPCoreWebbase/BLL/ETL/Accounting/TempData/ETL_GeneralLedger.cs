using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.TempData
{
    public class ETL_GeneralLedger
    {
        public Guid TransactionId { get; set; }
        public Guid GeneralJournalId { get; set; }
        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public double Balance { get; set; }
        public string Description { get; set; }
        public char JournalType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
