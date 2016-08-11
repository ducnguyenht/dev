using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.TempData
{
    public class ETL_FinnancialCustomerLiabilityDetail
    {
        public Guid OwnerOrgId { get; set; }
        public Guid CustomerOrgId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime IssueDate { get; set; }
        public string AccountCode { get; set; }
        public string CorrespondAccountCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public bool IsBalanceForward { get; set; }
    }
}
