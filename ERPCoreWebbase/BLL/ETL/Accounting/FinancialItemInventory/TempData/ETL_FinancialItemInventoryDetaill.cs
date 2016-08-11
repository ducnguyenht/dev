using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Inventory.TempData;

namespace NAS.BO.ETL.Accounting.FinancialItemInventory.TempData
{

    public class ETL_FinancialItemInventoryDetail
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

    public class ETL_FinancialItemInventoryTransformData
    {
        public List<ETL_FinancialItemInventoryDetail> ETL_FinancialDetailList
        {
            get;
            set;
        }

        public List<ETL_InventoryLedger> ETL_InventoryDetailList
        {
            get;
            set;
        }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ArtifactId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UnitId { get; set; }
        public Guid InventoryId { get; set; }
        public Guid CorrespondInventoryId { get; set; }   
        public string AccountCode { get; set; }   
    }
    
    public class ETL_FinancialItemInventoryExtracting
    {
        public ETL_Transaction FinancialTransaction { get; set; }
        public ETL_InventoryTransaction InventoryTransaction { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ArtifactId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UnitId { get; set; }
        public Guid InventoryId { get; set; }
        public Guid CorrespondInventoryId { get; set; }        
        public string AccountCode { get; set; } 
    }
}
