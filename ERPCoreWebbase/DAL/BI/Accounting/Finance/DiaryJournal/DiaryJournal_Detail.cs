using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;


namespace NAS.DAL.BI.Accounting.Finance.DiaryJournal
{
    public class DiaryJournal_Detail : XPCustomObject
    {
        public DiaryJournal_Detail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fDiaryJournal_DetailId;
        [Key(true)]
        public int DiaryJournal_DetailId
        {
            get { return fDiaryJournal_DetailId; }
            set { SetPropertyValue<int>("DiaryJournal_DetailId", ref fDiaryJournal_DetailId, value); }
        }

        double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }

        CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association(@"DiaryJournal_DetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref fCorrespondFinancialAccountDimId, value); }
        }

        CurrencyDim fCurrencyDimId;
        [Association(@"DiaryJournal_DetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }

        DiaryJournal_Fact fDiaryJournal_FactId;
        [Association(@"DiaryJournal_DetailReferencesDiaryJournal_Fact")]
        public DiaryJournal_Fact DiaryJournal_FactId
        {
            get { return fDiaryJournal_FactId; }
            set { SetPropertyValue<DiaryJournal_Fact>("DiaryJournal_FactId", ref fDiaryJournal_FactId, value); }
        }

        FinancialAccountDim fFinancialAccountDimId;
        [Association(@"DiaryJournal_DetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        FinancialTransactionDim fFinancialTransactionDimId;
        [Association(@"DiaryJournal_DetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }
    }
}
