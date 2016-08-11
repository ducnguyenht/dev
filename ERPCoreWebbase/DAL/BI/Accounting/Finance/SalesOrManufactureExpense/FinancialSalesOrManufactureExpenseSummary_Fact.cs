using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense
{
    public class FinancialSalesOrManufactureExpenseSummary_Fact : XPCustomObject
    {
        public FinancialSalesOrManufactureExpenseSummary_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        #region properties
        private int fFinancialSalesOrManufactureExpenseSummary_FactId;
        [Key(true)]
        public int FinancialSalesOrManufactureExpenseSummary_FactId
        {
            get { return fFinancialSalesOrManufactureExpenseSummary_FactId; }
            set { SetPropertyValue<int>("FinancialSalesOrManufactureExpenseSummary_FactId", ref fFinancialSalesOrManufactureExpenseSummary_FactId, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"SalesOrManufactureExpenseByGroupReferencesFinancialSalesOrManufactureExpenseSummary_Fact", typeof(SalesOrManufactureExpenseByGroup))]
        public XPCollection<SalesOrManufactureExpenseByGroup> SalesOrManufactureExpenseByGroups 
        { get { return GetCollection<SalesOrManufactureExpenseByGroup>("SalesOrManufactureExpenseByGroups"); } }

        private OwnerOrgDim fOwnerOrgDimId;
        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        private MonthDim fMonthDimId;
        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }

        private YearDim fYearDimId;
        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }
        #endregion
    }
}
