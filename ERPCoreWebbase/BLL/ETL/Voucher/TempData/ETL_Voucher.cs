using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Inventory.TempData;

namespace NAS.BO.ETL.Voucher.TempData
{
    public class ETL_Voucher
    {
        public Guid VoucherId { get; set; }
        public DateTime IssueDate { get; set; }
        public Guid OwnerOrgId { get; set; }
        public Guid SupplierOrg { get; set; }
        public Guid CustomerOrgId { get; set; }
        public decimal Amount { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public List<ETL_Transaction> FinancialTransactionList{ get; set; }        
    }
}
