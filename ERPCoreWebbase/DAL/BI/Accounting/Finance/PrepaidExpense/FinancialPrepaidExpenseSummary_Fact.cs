using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.BI.Accounting.Finance.PrepaidExpense
{
    public class FinancialPrepaidExpenseSummary_Fact: XPCustomObject
    {
        public FinancialPrepaidExpenseSummary_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private int fFinancialPrepaidExpenseSummary_FactId;
        [Key(true)]
        public int FinancialPrepaidExpenseSummary_FactId
        {
            get { return fFinancialPrepaidExpenseSummary_FactId; }
            set { SetPropertyValue<int>("FinancialPrepaidExpenseSummary_FactId", ref fFinancialPrepaidExpenseSummary_FactId, value); }
        }

        private decimal fBeginCreditBalance;
        public decimal BeginCreditBalance
        {
            get { return fBeginCreditBalance; }
            set { SetPropertyValue<decimal>("BeginCreditBalance", ref fBeginCreditBalance, value); }
        }

        private decimal fBeginDebitBalance;
        public decimal BeginDebitBalance
        {
            get { return fBeginDebitBalance; }
            set { SetPropertyValue<decimal>("BeginDebitBalance", ref fBeginDebitBalance, value); }
        }

        private decimal fCreditSum;
        public decimal CreditSum
        {
            get { return fCreditSum; }
            set { SetPropertyValue<decimal>("CreditSum", ref fCreditSum, value); }
        }

        private decimal fDebitSum;
        public decimal DebitSum
        {
            get { return fDebitSum; }
            set { SetPropertyValue<decimal>("DebitSum", ref fDebitSum, value); }
        }

        private decimal fEndCreditBalance;
        public decimal EndCreditBalance
        {
            get { return fEndCreditBalance; }
            set { SetPropertyValue<decimal>("EndCreditBalance", ref fEndCreditBalance, value); }
        }

        private decimal fEndDebitBalance;
        public decimal EndDebitBalance
        {
            get { return fEndDebitBalance; }
            set { SetPropertyValue<decimal>("EndDebitBalance", ref fEndDebitBalance, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"FinancialPrepaidExpenseDetailReferencesFinancialPrepaidExpenseSummary_Fact", typeof(FinancialPrepaidExpenseDetail))]
        public XPCollection<FinancialPrepaidExpenseDetail> FinancialPrepaidExpenseDetails 
        { get { return GetCollection<FinancialPrepaidExpenseDetail>("FinancialPrepaidExpenseDetails"); } }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association("FinancialPrepaidExpenseSummary_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private OwnerOrgDim fOwnerOrgDimId;
        [Association("FinancialPrepaidExpenseSummary_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        private YearDim fYearDimId;
        [Association("FinancialPrepaidExpenseSummary_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }

        private MonthDim fMonthDimId;
        [Association("FinancialPrepaidExpenseSummary_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }
        #endregion
    }
}
