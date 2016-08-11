using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Report
{
    public class FinancialDescriptionReportT1_Fact : XPCustomObject
    {
        int fFinancialDescriptionReportT1_FactId;
        [Key(true)]
        public int FinancialDescriptionReportT1_FactId
        {
            get { return fFinancialDescriptionReportT1_FactId; }
            set { SetPropertyValue<int>("FinancialDescriptionReportT1_Fact", ref fFinancialDescriptionReportT1_FactId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }


        public FinancialDescriptionReportT1_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
