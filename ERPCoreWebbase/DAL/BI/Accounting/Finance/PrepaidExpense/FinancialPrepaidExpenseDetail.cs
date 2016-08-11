using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Finance.PrepaidExpense
{
    public class FinancialPrepaidExpenseDetail: XPCustomObject
    {
        public FinancialPrepaidExpenseDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fFinancialPrepaidExpenseDetailId;
        [Key(true)]
        public Guid FinancialPrepaidExpenseDetailId
        {
            get { return fFinancialPrepaidExpenseDetailId; }
            set { SetPropertyValue<Guid>("FinancialPrepaidExpenseDetailId", ref fFinancialPrepaidExpenseDetailId, value); }
        }

        private decimal fCredit;
        public decimal Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<decimal>("Credit", ref fCredit, value); }
        }

        private decimal fDebit;
        public decimal Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<decimal>("Debit", ref fDebit, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion        

        private FinancialPrepaidExpenseSummary_Fact fFinancialPrepaidExpenseSummary_FactId;
        [Association("FinancialPrepaidExpenseDetailReferencesFinancialPrepaidExpenseSummary_Fact")]
        public FinancialPrepaidExpenseSummary_Fact FinancialPrepaidExpenseSummary_FactId
        {
            get { return fFinancialPrepaidExpenseSummary_FactId; }
            set { SetPropertyValue<FinancialPrepaidExpenseSummary_Fact>("FinancialPrepaidExpenseSummary_FactId", ref fFinancialPrepaidExpenseSummary_FactId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association("FinancialPrepaidExpenseDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private FinancialTransactionDim fFinancialTransactionDimId;
        [Association("FinancialPrepaidExpenseDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association("FinancialPrepaidExpenseDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association("FinancialPrepaidExpenseDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }
    }
}
