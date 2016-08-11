using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;

namespace NAS.BO.ETL.Accounting.GoodsInInventory.TempData
{
    public class ETL_GoodsInInventoryDetail
    {
        public Guid OwnerOrgId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime IssueDate { get; set; }
        public string AccountCode { get; set; }
        public string CorrespondAccountCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ArtifactId { get; set; }
        public bool IsBalanceForward { get; set; }
    }

    public class ETL_GoodsInInventoryTransformData
    {
        public List<ETL_GoodsInInventoryDetail> ETL_DetailList
        {
            get;
            set;
        }

        public string MainAccountCode { get; set; }        
    }

    public class ETL_GoodsInInventoryTransaction : ETL_Transaction
    {
        public string AccountCode { get; set; }
        public double Quantity{ get; set; }
        public decimal Price { get; set; }
        public Guid ArtifactId { get; set; }
    }
}
