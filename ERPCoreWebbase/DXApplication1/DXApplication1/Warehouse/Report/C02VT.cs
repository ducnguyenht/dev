using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Warehouse.Report
{
    public class C02VT
    {
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string itemUnit { get; set; }
        public double quantity { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public string code { get; set; }
        public DateTime createDate { get; set; }
        public string amountByString { get; set; }
        public string SaleInvoiceCode { get; set; }
        public DateTime SaleInvoiceDate { get; set; }
        public string InventoryName { get; set; }
        public string InventoryAddress { get; set; }
        public string SenderName { get; set; }
    }
}