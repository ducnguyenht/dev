using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood
{
    public partial class FinancialOnTheWayBuyingGoodSummary : XPCustomObject
    {
        public FinancialOnTheWayBuyingGoodSummary(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        #region properties
        private Guid fFinancialOnTheWayBuyingGoodSummaryId;
        [Key(true)]
        public Guid FinancialOnTheWayBuyingGoodSummaryId
        {
            get { return fFinancialOnTheWayBuyingGoodSummaryId; }
            set { SetPropertyValue<Guid>("FinancialOnTheWayBuyingGoodSummaryId", ref fFinancialOnTheWayBuyingGoodSummaryId, value); }
        }

        private decimal fBeginBalance;
        public decimal BeginBalance
        {
            get { return fBeginBalance; }
            set { SetPropertyValue<decimal>("BeginBalance", ref fBeginBalance, value); }
        }

        private decimal fDebitSum;
        private decimal fCreditSum;
        //4.CreditSum - money
        public decimal CreditSum
        {
            get { return fCreditSum; }
            set { SetPropertyValue("CreditSum", ref fCreditSum, value); }
        }
        //5.DebitSum - money
        public decimal DebitSum
        {
            get { return fDebitSum; }
            set { SetPropertyValue("DebitSum", ref fDebitSum, value); }
        }


        private decimal fEndBalance;
        public decimal EndBalance
        {
            get { return fEndBalance; }
            set { SetPropertyValue<decimal>("EndBalance", ref fEndBalance, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"OnTheWayBuyingGoodArtifactReferencesFianncialOnTheWayBuyingGoodSummary", typeof(OnTheWayBuyingGoodArtifact))]
        public XPCollection<OnTheWayBuyingGoodArtifact> OnTheWayBuyingGoodArtifacts { get { return GetCollection<OnTheWayBuyingGoodArtifact>("OnTheWayBuyingGoodArtifacts"); } }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association("FinancialOnTheWayBuyingGoodSummaryReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private OwnerOrgDim fOwnerOrgDimId;
        [Association("FinancialOnTheWayBuyingGoodSummaryReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        private MonthDim fMonthDimId;
        [Association("FinancialOnTheWayBuyingGoodSummaryReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }

        private YearDim fYearDimId;
        [Association("FinancialOnTHeWayBuyingGoodSummaryReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }
        #endregion
    }
}
