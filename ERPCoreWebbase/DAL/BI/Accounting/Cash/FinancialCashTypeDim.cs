using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Accounting.Cash
{
    public class FinancialCashTypeDim : XPCustomObject
    {
        int fFinancialCashTypeDimId;
        [Key(true)]
        public int FinancialCashTypeDimId
        {
            get { return fFinancialCashTypeDimId; }
            set { SetPropertyValue<int>("FinancialCashTypeDimId", ref fFinancialCashTypeDimId, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        [Association("FinancialCash_FactReferencesFinancialCashTypeDim", typeof(FinancialCash_Fact)), Aggregated]
        public XPCollection<FinancialCash_Fact> FinancialCash_Facts
        {
            get { return GetCollection<FinancialCash_Fact>("FinancialCash_Facts"); }
        }

        public FinancialCashTypeDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
