using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.Report
{
    public class FinancialDescriptionReportT2_Fact : XPCustomObject
    {
        int fFinancialDescriptionReportT2_FactId;
        [Key(true)]
        public int FinancialDescriptionReportT2_FactId
        {
            get { return fFinancialDescriptionReportT2_FactId; }
            set { SetPropertyValue<int>("FinancialDescriptionReportT2_FactId", ref fFinancialDescriptionReportT2_FactId, value); }
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

        string fExpression;
        public string Expression
        {
            get { return fExpression; }
            set { SetPropertyValue<string>("Expression", ref fExpression, value); }
        }

        FinancialAccountDim fFinancialAccountDimId;
        [Association(@"FinancialDescriptionReportT2_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        public FinancialDescriptionReportT2_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
