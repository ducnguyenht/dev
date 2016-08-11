using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;

namespace NAS.BO.ETL.Accounting.FinancialSalesOrManufactureExpense.TempData
{
    public class ETL_SalesOrManufacturerExpenseDetail
    {
        public Guid OwnerOrgId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime IssueDate { get; set; }
        public string AccountCode { get; set; }
        public string CorrespondAccountCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public bool IsBalanceForward { get; set; }
    }

    public class ETL_SalesOrManufacturerExpenseTransformData
    {
        public List<ETL_SalesOrManufacturerExpenseDetail> ETL_DetailList
        {
            get;
            set;
        }

        public string HighestAccountCode { get; set; }
        public string MainAccountCode { get; set; }
    }

    public class ETL_SalesOrManufacturerExpenseTransaction : ETL_Transaction
    {
        public string AccountCode { get; set; }
    }
}
