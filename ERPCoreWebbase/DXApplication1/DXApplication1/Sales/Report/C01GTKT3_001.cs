using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPCore.Sales.Report
{

    public class C01GTKT3_001
    {
        public C01GTKT3_001()
        {

        }

        public Guid BillId { get; set; }
        public DateTime CreateDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerTax { get; set; }
        public string CustomerAddress { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double PriceA { get; set; }
        public double AmountA { get; set; }
        public double PromotionInPercentage { get; set; }
        public double PromotionInNumber { get; set; }
        public double TaxInPercentage { get; set; }
        public double TaxInNumber { get; set; }
        public double Amount { get; set; }
        public double Total { get; set; }
        public string TotalByString { get; set; }
    }
}