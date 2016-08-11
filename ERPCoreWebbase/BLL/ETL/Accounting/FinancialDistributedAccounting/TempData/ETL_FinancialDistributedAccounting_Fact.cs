using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;

namespace NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData
{    
    public class ETL_FinancialDistributedAccounting_Fact
    {
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public Guid AccountPeriodId { get; set; }
        public Guid FinancialAssetDimId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
    }
    public class Transaction:ETL_Transaction
    {
        public Guid ArtifactRefId { get; set; }
        public Guid AccountingPeriodId { get; set; }
        public string ArtifactDescription { get; set; }
        public Guid FinancialAssetDimId { get; set; }
        public List<ETL_FactTable> FactTableList { get; set; }
    }
    public class ETL_FactTable
    {
        public string CreditAccountCode { get; set; }
        public string DebitAccountCode { get; set; }
        public string Amount { get; set; }
    }
}
