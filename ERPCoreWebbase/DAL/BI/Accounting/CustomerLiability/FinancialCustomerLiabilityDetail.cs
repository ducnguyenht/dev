using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.CustomerLiability
{
    public class FinancialCustomerLiabilityDetail : XPCustomObject
    {
        public FinancialCustomerLiabilityDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fFinancialCustomerLiabilityDetailId;
        [Key(true)]
        public Guid FinancialCustomerLiabilityDetailId
        {
            get { return fFinancialCustomerLiabilityDetailId; }
            set { SetPropertyValue<Guid>("FinancialCustomerLiabilityDetailId", ref fFinancialCustomerLiabilityDetailId, value); }
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
        [Association("FinancialCustomerLiabilityDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association("FinancialCustomerLiabilityDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association("FinancialCustomerLiabilityDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association("FinancialCustomerLiabilityDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private FinancialCustomerLiabilitySummary_Fact fFinancialCustomerLiabilitySummary_FactId;
        [Association("FinancialCustomerLiabilityDetailReferencesFinancialSupplierLiabilitySummary_Fact")]
        public FinancialCustomerLiabilitySummary_Fact FinancialCustomerLiabilitySummary_FactId
        {
            get { return fFinancialCustomerLiabilitySummary_FactId; }
            set { SetPropertyValue<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_FactId", ref fFinancialCustomerLiabilitySummary_FactId, value); }
        }
        #endregion
    }
}
