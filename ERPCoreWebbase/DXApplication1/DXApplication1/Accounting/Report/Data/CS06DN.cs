using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;

namespace WebModule.Accounting.Report.Data
{
    [NonPersistent]
    public class CS06DN : XPLiteObject
    {
        public string AccountCode;
        public string AccountName;
        public double BeginDebit;
        public double BeginCredit;
        public double Debit;
        public double Credit;
        public double EndDebit;
        public double EndCredit;

        public CS06DN(Session session) : base(session) { }
    }
}