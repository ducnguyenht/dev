using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.PayReceiving.Report
{
    public class C01TT
    {
        public string Code { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string ReceiverName { get; set; }
        public string Address { get; set; }
        public double Debit { get; set; }
        public double ExchangeRate { get; set; }
        public double DebitExchange { get; set; }
        public string DebitByString { get; set; }
        public string Currency { get; set; }
    }
}