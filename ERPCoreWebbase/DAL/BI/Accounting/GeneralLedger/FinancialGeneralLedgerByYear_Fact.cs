using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.GeneralLedger
{
    public class FinancialGeneralLedgerByYear_Fact : XPCustomObject
    {
        public FinancialGeneralLedgerByYear_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties

        private int _FinancialGeneralLedgerByYear_FactId;
        [Key(true)]
        public int FinancialGeneralLedgerByYear_FactId
        {
            get { return _FinancialGeneralLedgerByYear_FactId; }
            set { SetPropertyValue<int>("FinancialGeneralLedgerByYear_FactId", ref _FinancialGeneralLedgerByYear_FactId, value); }
        }

        private double _BeginCreditBalance;
        public double BeginCreditBalance
        {
            get { return _BeginCreditBalance; }
            set { SetPropertyValue<double>("BeginCreditBalance", ref _BeginCreditBalance, value); }
        }

        private double _BeginDebitBalance;
        public double BeginDebitBalance
        {
            get { return _BeginDebitBalance; }
            set { SetPropertyValue<double>("BeginDebitBalance", ref _BeginDebitBalance, value); }
        }

        private double _EndCreditBalance;
        public double EndCreditBalance
        {
            get { return _EndCreditBalance; }
            set { SetPropertyValue<double>("EndCreditBalance", ref _EndCreditBalance, value); }
        }

        private double _EndDebitBalance;
        public double EndDebitBalance
        {
            get { return _EndDebitBalance; }
            set { SetPropertyValue<double>("EndDebitBalance", ref _EndDebitBalance, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        private YearDim _YearDimId;
        [Association("FinancailGeneralLedgerByYear_Fact-YearDim")]
        public YearDim YearDimId
        {
            get { return _YearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref _YearDimId, value); }
        }

        private FinancialAccountDim _FinancialAccountDimId;
        [Association("FinancialGeneralLedgerByYear_Fact-FinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return _FinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref _FinancialAccountDimId, value); }
        }

        [Association("FinancialGeneralLegerByMonth-FinancialGeneralLedgerByYear_Fact", typeof(FinancialGeneralLedgerByMonth))]
        public XPCollection<FinancialGeneralLedgerByMonth> FinancialGeneralLedgerByMonths { get { return GetCollection<FinancialGeneralLedgerByMonth>("FinancialGeneralLedgerByMonths"); } }
        #endregion
    }
}
