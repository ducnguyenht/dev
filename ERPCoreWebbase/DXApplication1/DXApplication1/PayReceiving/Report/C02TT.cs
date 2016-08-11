using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.PayReceiving.Report
{
    public class C02TT
    {
        public string Code { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Description { get; set; }
        public string SupplierName { get; set; }
        public string PayerName { get; set; }
        public string Address { get; set; }
        public double Credit { get; set; }
        public double ExchangeRate { get; set; }
        public double CreditExchange { get; set; }
        public string CreditByString { get; set; }
        public string Currency { get; set; }
    }
}