using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood
{
    public partial class FinancialOnTheWayBuyingGoodDetail : XPCustomObject
    {
        public FinancialOnTheWayBuyingGoodDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fFinancialOnTheWayBuyingGoodDetailId;
        [Key(true)]
        public Guid FinancialOnTheWayBuyingGoodDetailId
        {
            get { return fFinancialOnTheWayBuyingGoodDetailId; }
            set { SetPropertyValue<Guid>("FinancialOnTheWayBuyingGoodDetailId", ref fFinancialOnTheWayBuyingGoodDetailId, value); }
        }

        private double fActuaPrice;
        public double ActuaPrice
        {
            get { return fActuaPrice; }
            set { SetPropertyValue<double>("ActuaPrice", ref fActuaPrice, value); }
        }

        private double fBookingPrice;
        public double BookingPrice
        {
            get { return fBookingPrice; }
            set { SetPropertyValue<double>("BookingPrice", ref fBookingPrice, value); }
        }

        private double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        private double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
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
        [Association("FinancialOnTheWayBuyingGoodDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        private OnTheWayBuyingGoodArtifact fOnTheWayBuyingGoodArtifactId;
        [Association("FinancialOnTheWayBuyingGoodDetailReferencesOnTheWayBuyingGoodArtifact")]
        public OnTheWayBuyingGoodArtifact OnTheWayBuyingGoodArtifactId
        {
            get { return fOnTheWayBuyingGoodArtifactId; }
            set { SetPropertyValue<OnTheWayBuyingGoodArtifact>("OnTheWayBuyingGoodArtifactId", ref fOnTheWayBuyingGoodArtifactId, value); }
        }

        private CorrespondFinancialAccountDim fCorrespondFinancialAccountDimId;
        [Association("FinancialOnTheWayBuyingGoodDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return fCorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountArtifactId", ref fCorrespondFinancialAccountDimId, value); }
        }

        private FinancialTransactionDim fFinancialTransactionDimId;
        [Association("FinancialOnTheWayBuyingGoodDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<FinancialTransactionDim>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
        }

        private CurrencyDim fCurrencyDimId;
        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return fCurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref fCurrencyDimId, value); }
        }
        #endregion
    }
}
