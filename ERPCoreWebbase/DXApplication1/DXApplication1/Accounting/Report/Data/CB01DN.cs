using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Accounting.Report.Data
{
    public class CB01DN
    {
        public int OrderNumber {get;set;}   
        public string AccountName {get;set;}
        public string OrderCode { get; set; }      
        public string Description { get; set; }
        public double EndBalance { get; set; }
        public double BeginBalance { get; set; }
        public string AccountCode { get; set; }
        public string Expression {get;set;}
        public bool Detail { get; set; }
        public CB01DN()
        {          
        }
    }
}