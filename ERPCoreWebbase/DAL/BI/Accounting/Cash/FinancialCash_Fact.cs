using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Cash
{
    public class FinancialCash_Fact : XPCustomObject
    {
        int fFinancialCash_FactId;
        [Key(true)]
        public int FinancialCash_FactId
        {
            get { return fFinancialCash_FactId; }
            set { SetPropertyValue<int>("FinancialCash_FactId", ref fFinancialCash_FactId, value); }
        }

        double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }

        double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        private FinancialCashTypeDim fFinancialCashTypeDimId;
        [Association("FinancialCash_FactReferencesFinancialCashTypeDim")]
        public FinancialCashTypeDim FinancialCashTypeDimId
        {
            get { return fFinancialCashTypeDimId; }
            set { SetPropertyValue<FinancialCashTypeDim>("FinancialCashTypeDimId", ref fFinancialCashTypeDimId, value); }
        }

        private FinancialVoucherDim fFinancialVoucherDimId;
        [Association("FinancialCash_FactReferencesFinancialVoucherDim")]
        public FinancialVoucherDim FinancialVoucherDimId
        {
            get { return fFinancialVoucherDimId; }
            set { SetPropertyValue<FinancialVoucherDim>("FinancialVoucherDimId", ref fFinancialVoucherDimId, value); }
        }

        FinancialAccountDim fFinancialAccountDimId;
        [Association(@"FinancialCash_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"FinancialCash_FactReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        public FinancialCash_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
