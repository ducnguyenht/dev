using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Report
{
    public class C01VT
    {
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemUnit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string AmountByString { get; set; }
        public string PurchaseInvoiceCode { get; set; }
        public DateTime PurchaseInvoiceDate { get; set; }
        public string InventoryName { get; set; }
        public string InventoryAddress { get; set; }
        public string ReceiverName { get; set; }
    }
}