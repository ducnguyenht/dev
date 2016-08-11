using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.Report
{
    public class S10DN
    {
        string Code { get; set; }
        DateTime IssueDate { get; set; }
        string Description { get; set; }
        double COGSPrice { get; set; }
        string AccountCode { get; set; }
        double QuantityIn { get; set; }
        double QuantityOut { get; set; }
        double AmountIn { get; set; }
        double AmountOut { get; set; }
        double QuantityBalacne { get; set; }
        double AmountBalacne { get; set; }
    }
}