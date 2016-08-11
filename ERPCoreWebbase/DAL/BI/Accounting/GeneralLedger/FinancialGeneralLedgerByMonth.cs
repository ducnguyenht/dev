using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.GeneralLedger
{
    public class FinancialGeneralLedgerByMonth : XPCustomObject
    {
        public FinancialGeneralLedgerByMonth(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private int _FinancialGeneralLedgerByMonthId;
        [Key(true)]
        public int FinancialGeneralLedgerByMonthId
        {
            get { return _FinancialGeneralLedgerByMonthId; }
            set { SetPropertyValue<int>("FinancialGeneralLedgerByMonthId", ref _FinancialGeneralLedgerByMonthId, value); }
        }

        private double _CreditSum;
        public double CreditSum
        {
            get { return _CreditSum; }
            set { SetPropertyValue<double>("CreditSum", ref _CreditSum, value); }
        }

        private double _DebitSum;
        public double DebitSum
        {
            get { return _DebitSum; }
            set { SetPropertyValue<double>("DebitSum", ref _DebitSum, value); }
        }

        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        private FinancialGeneralLedgerByYear_Fact _FinancialGeneralLedgerByYear_FactId;
        [Association("FinancialGeneralLegerByMonth-FinancialGeneralLedgerByYear_Fact")]
        public FinancialGeneralLedgerByYear_Fact FinancialGeneralLedgerByYear_FactId
        {
            get { return _FinancialGeneralLedgerByYear_FactId; }
            set { SetPropertyValue<FinancialGeneralLedgerByYear_Fact>("FinancialGeneralLedgerByYear_FactId", ref _FinancialGeneralLedgerByYear_FactId, value); }
        }

        private MonthDim _MonthDimId;
        [Association("FinancialGeneralLedgerByMonth-MonthDim")]
        public MonthDim MonthDimId
        {
            get { return _MonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref _MonthDimId, value); }
        }

        private CurrencyDim _CurrencyDimId;
        [Association("FinancialGeneralLedgerByMonth-CurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return _CurrencyDimId; }
            set { SetPropertyValue<CurrencyDim>("CurrencyDimId", ref _CurrencyDimId, value); }
        }

        private CorrespondFinancialAccountDim _CorrespondFinancialAccountDimId;
        [Association("FinancialGeneralLedgerByMonth-CorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return _CorrespondFinancialAccountDimId; }
            set { SetPropertyValue<CorrespondFinancialAccountDim>("CorrespondFinancialAccountDimId", ref _CorrespondFinancialAccountDimId, value); }
        }
        #endregion
    }
}
