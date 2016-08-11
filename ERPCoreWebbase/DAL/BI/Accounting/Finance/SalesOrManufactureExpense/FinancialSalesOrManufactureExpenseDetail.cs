using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense
{
    public class FinancialSalesOrManufactureExpenseDetail : XPCustomObject
    {
        public FinancialSalesOrManufactureExpenseDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fFinancialSalesOrManufactureExpenseDetailId;
        [Key(true)]
        public Guid FinancialSalesOrManufactureExpenseDetailId
        {
            get { return fFinancialSalesOrManufactureExpenseDetailId; }
            set { SetPropertyValue<Guid>("FinancialSalesOrManufactureExpenseDetailId", ref fFinancialSalesOrManufactureExpenseDetailId, value); }
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

        #region References
        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private SalesOrManufactureExpenseByGroup fSalesOrManufactureExpenseByGroupId;
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesSalesOrManufactureExpenseByGroup")]
        public SalesOrManufactureExpenseByGroup SalesOrManufactureExpenseByGroupId
        {
            get { return fSalesOrManufactureExpenseByGroupId; }
            set { SetPropertyValue<SalesOrManufactureExpenseByGroup>("SalesOrManufactureExpenseByGroupId", ref fSalesOrManufactureExpenseByGroupId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        FinancialTransactionDim fFinancialTransactionDimId;
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        CurrencyDim fCurrencyDimId;
        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }
        #endregion
    }
}
