using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.FinancialActualPrice
{
    public class FinancialActualPriceDetail : XPCustomObject
    {
        public FinancialActualPriceDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fFinancialActualPriceDetailId;
        [Key(true)]
        public Guid FinancialActualPriceDetailId
        {
            get { return fFinancialActualPriceDetailId; }
            set { SetPropertyValue<Guid>("FinancialActualPriceDetailId", ref fFinancialActualPriceDetailId, value); }
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
        private FinancialTransactionDim fFinancialTransactionDimId;
        [Association(@"FinancialActualPriceDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association(@"FinancialActualPriceDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"FinancialActualPriceDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"FinancialActualPriceDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private FinancialActualPriceSummary_Fact fFinancialActualPriceSummary_FactId;
        [Association(@"FinancialActualPriceDetailReferencesFinancialActualPriceSummary_Fact")]
        public FinancialActualPriceSummary_Fact FinancialActualPriceSummary_FactId
        {
            get { return fFinancialActualPriceSummary_FactId; }
            set { SetPropertyValue<FinancialActualPriceSummary_Fact>("FinancialActualPriceSummary_FactId", ref fFinancialActualPriceSummary_FactId, value); }
        }
        #endregion
    }
}
