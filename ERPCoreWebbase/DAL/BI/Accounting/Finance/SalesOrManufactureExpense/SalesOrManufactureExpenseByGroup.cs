using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense
{
    public class SalesOrManufactureExpenseByGroup: XPCustomObject
    {
        public SalesOrManufactureExpenseByGroup(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region properties
        private Guid fSalesOrManufactureExpenseByGroupId;
        [Key(true)]
        public Guid SalesOrManufactureExpenseByGroupId
        {
            get { return fSalesOrManufactureExpenseByGroupId; }
            set { SetPropertyValue<Guid>("SalesOrManufactureExpenseByGroupId", ref fSalesOrManufactureExpenseByGroupId, value); }
        }

        private decimal fSumExpense;
        public decimal SumExpense
        {
            get { return fSumExpense; }
            set { SetPropertyValue<decimal>("SumExpense", ref fSumExpense, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region references
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesSalesOrManufactureExpenseByGroup", typeof(FinancialSalesOrManufactureExpenseDetail))]
        public XPCollection<FinancialSalesOrManufactureExpenseDetail> FinancialSalesOrManufactureExpenseDetails { get { return GetCollection<FinancialSalesOrManufactureExpenseDetail>("FinancialSalesOrManufactureExpenseDetails"); } }

        private FinancialSalesOrManufactureExpenseSummary_Fact fFinancialSalesOrManufactureExpenseSummary_FactId;
        [Association(@"SalesOrManufactureExpenseByGroupReferencesFinancialSalesOrManufactureExpenseSummary_Fact")]
        public FinancialSalesOrManufactureExpenseSummary_Fact FinancialSalesOrManufactureExpenseSummary_FactId
        {
            get { return fFinancialSalesOrManufactureExpenseSummary_FactId; }
            set { SetPropertyValue<FinancialSalesOrManufactureExpenseSummary_Fact>("FinancialSalesOrManufactureExpenseSummary_FactId", ref fFinancialSalesOrManufactureExpenseSummary_FactId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"SalesOrManufactureExpenseByGroupReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }
        #endregion
    }
}
