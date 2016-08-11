using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Purchasing.Report
{
    public class CReportPurchaseInvoice
    {
        public DateTime IssueDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string ManufacturerName { get; set; }
        public double Quantity { get; set; }        
    }
}