using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Actor;

namespace NAS.DAL.BI.Accounting.Finance.DiaryJournal
{
    public class DiaryJournal_Fact : XPCustomObject
    {
        public DiaryJournal_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fDiaryJournal_FactId;
        [Key(true)]
        public int DiaryJournal_FactId
        {
            get { return fDiaryJournal_FactId; }
            set { SetPropertyValue<int>("DiaryJournal_FactId", ref fDiaryJournal_FactId, value); }
        }

        double fBeginCreditBalance;
        public double BeginCreditBalance
        {
            get { return fBeginCreditBalance; }
            set { SetPropertyValue<double>("BeginCreditBalance", ref fBeginCreditBalance, value); }
        }

        double fBeginDebitBalance;
        public double BeginDebitBalance
        {
            get { return fBeginDebitBalance; }
            set { SetPropertyValue<double>("BeginDebitBalance", ref fBeginDebitBalance, value); }
        }

        double fCreditSum;
        public double CreditSum
        {
            get { return fCreditSum; }
            set { SetPropertyValue<double>("CreditSum", ref fCreditSum, value); }
        }

        double fDebitSum;
        public double DebitSum
        {
            get { return fDebitSum; }
            set { SetPropertyValue<double>("DebitSum", ref fDebitSum, value); }
        }

        double fEndCreditBalance;
        public double EndCreditBalance
        {
            get { return fEndCreditBalance; }
            set { SetPropertyValue<double>("EndCreditBalance", ref fEndCreditBalance, value); }
        }

        double fEndDebitBalance;
        public double EndDebitBalance
        {
            get { return fEndDebitBalance; }
            set { SetPropertyValue<double>("EndDebitBalance", ref fEndDebitBalance, value); }
        }


        FinancialAccountDim fFinancialAccountDimId;
        [Association(@"DiaryJournal_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        MonthDim fMonthDimId;
        [Association(@"DiaryJournal_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }

        YearDim fYearDimId;
        [Association(@"DiaryJournal_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }

        OwnerOrgDim fOwnerOrgDimId;
        [Association(@"DiaryJournal_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        [Association("DiaryJournal_DetailReferencesDiaryJournal_Fact", typeof(DiaryJournal_Detail)), Aggregated]
        public XPCollection<DiaryJournal_Detail> DiaryJournal_Details
        {
            get { return GetCollection<DiaryJournal_Detail>("DiaryJournal_Details"); }
        }

    }
}
