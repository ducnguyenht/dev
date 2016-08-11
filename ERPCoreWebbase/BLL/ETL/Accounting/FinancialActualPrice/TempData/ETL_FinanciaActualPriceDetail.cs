using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;

namespace NAS.BO.ETL.Accounting.FinancialActualPrice.TempData
{
    public class ETL_FinanciaActualPriceDetail
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

    public class ETL_FinanciaActualPriceTransformData
    {
        public List<ETL_FinanciaActualPriceDetail> ETL_DetailList
        {
            get;
            set;
        }

        public string AccountCode { get; set; }
    }

    public class ETL_ActualPriceTransaction : ETL_Transaction
    {
        public string AccountCode { get; set; }
    }
}
