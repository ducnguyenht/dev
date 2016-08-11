using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.Report
{
    public class C011GTGT
    {
        public double Amount { get; set; }
        public string ClaimItem { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public double TaxInNumber { get; set; }
        public double TaxInPercentage { get; set; }
        public double TotalAmount { get; set; }
        public string BillCode { get; set; }
        public string SeriesNumber { get; set; }
        public string ObjectTax { get; set; }
        public string ObjectName { get; set; }
        public string LegalInvoiceArtifactTypeName { get; set; }
        public string LegalInvoiceArtifactTypeCode { get; set; }
    }
}