using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.TempData
{
    public class ETL_GeneralJournal
    {
        public Guid GeneralJournalId { get; set; }
        public Guid AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }        
        public string Description { get; set; }
        public char JournalType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
