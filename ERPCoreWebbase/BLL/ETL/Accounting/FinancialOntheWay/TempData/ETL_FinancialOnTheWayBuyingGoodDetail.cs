using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;

namespace NAS.BO.ETL.Accounting.FinancialOntheWay.TempData
{
    public class ETL_FinancialOnTheWayBuyingGoodDetail
    {
        public Guid OwnerOrgId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime IssueDate { get; set; }
        public string AccountCode { get; set; }
        public string CurrencyCode { get; set; }
        public string CorrespondAccountCode { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public double ActualPrice { get; set; }
        public double BookedPrice { get; set; }
        public Guid InputInventoryCommandId { get; set; }
        public Guid PurchaseInvoiceId { get; set; }
        public bool IsBalanceForward { get; set; }
    }

    public class ETL_TransactionS04a6DN : ETL_Transaction
    {
        public Guid InputInventoryCommandId { get; set; }
        public Guid PurchaseInvoiceId { get; set; }
        public double ActualPrice { get; set; }
        public double BookedPrice { get; set; }
    }
}
