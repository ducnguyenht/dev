using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.BI.Accounting.GoodsInTransit
{
    public class GoodsInTransitForSaleSummary_Fact : XPCustomObject
    {
        public GoodsInTransitForSaleSummary_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private int fGoodsInTransitForSaleSummary_FactId;
        [Key(true)]
        public int GoodsInTransitForSaleSummary_FactId
        {
            get { return fGoodsInTransitForSaleSummary_FactId; }
            set { SetPropertyValue<int>("GoodsInTransitForSaleSummary_FactId", ref fGoodsInTransitForSaleSummary_FactId, value); }
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
        [Association(@"GoodsInTransitForSaleDetailReferencesGoodsInTransitForSaleSummary_Fact", typeof(GoodsInTransitForSaleDetail))]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails
        { get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); } }

        private FinancialAccountDim fFinancialAccountDimId;
        [Association(@"GoodsInTransitForSaleSummary_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private OwnerOrgDim fOwnerOrgDimId;
        [Association(@"GoodsInTransitForSaleSummary_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        private YearDim fYearDimId;
        [Association(@"GoodsInTransitForSaleSummary_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }

        private MonthDim fMonthDimId;
        [Association(@"GoodsInTransitForSaleSummary_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }
        #endregion
    }
}
