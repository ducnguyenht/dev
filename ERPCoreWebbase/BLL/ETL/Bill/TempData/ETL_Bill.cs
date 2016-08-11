using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Inventory.TempData;

namespace NAS.BO.ETL.Bill.TempData
{
    public class ETL_Bill
    {        
        public Guid BillId { get; set; }
        public DateTime IssueDate { get; set; }        
        public Guid OwnerOrgId { get; set; }
        public Guid CustomerOrgId { get; set; }
        public Guid SupplierOrgId { get; set; }
        public List<ETL_BillItem> BillItemList { get; set; }
        public List<ETL_Transaction> FinancialTranSactionList { get; set; }
        public List<ETL_InventoryTransaction> InventoryTranSactionList { get; set; }
    }
}
