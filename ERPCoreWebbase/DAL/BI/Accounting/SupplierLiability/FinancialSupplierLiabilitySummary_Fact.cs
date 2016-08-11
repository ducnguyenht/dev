using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.BI.Accounting.SupplierLiability
{
    public class FinancialSupplierLiabilitySummary_Fact: XPCustomObject
    {
        public FinancialSupplierLiabilitySummary_Fact(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        #region Field [8]
        // Fields...
        private FinancialAccountDim _FinancialAccountDimId;
        private OwnerOrgDim _OwnerOrgDimId;
        private YearDim _YearDimId;
        private MonthDim _MonthDimId;
        private SupplierOrgDim _SupplierOrgDimId;

        private short _RowStatus;
        private decimal _EndDebitBalance;
        private decimal _EndCreditBalance;
        private decimal _DebitSum;
        private decimal _CreditSum;
        private decimal _BeginDebitBalance;
        private decimal _BeginCreditBalance;
        private int _FinancialSupplierLiabilitySummary_FactId;

        //1.ID - int - [Key]
        [Key(true)]
        public int FinancialSupplierLiabilitySummary_FactId
        {
            get { return _FinancialSupplierLiabilitySummary_FactId; }
            set { SetPropertyValue("FinancialSupplierLiabilitySummary_FactId", ref _FinancialSupplierLiabilitySummary_FactId, value); }
        }
        //2.BeginCreditBalance - money
        public decimal BeginCreditBalance
        {
            get { return _BeginCreditBalance; }
            set { SetPropertyValue("BeginCreditBalance", ref _BeginCreditBalance, value); }
        }
        //3.BeginDebitBalance - money
        public decimal BeginDebitBalance
        {
            get { return _BeginDebitBalance; }
            set { SetPropertyValue("BeginDebitBalance", ref _BeginDebitBalance, value); }
        }
        //4.CreditSum - money
        public decimal CreditSum
        {
            get { return _CreditSum; }
            set { SetPropertyValue("CreditSum", ref _CreditSum, value); }
        }
        //5.DebitSum - money
        public decimal DebitSum
        {
            get { return _DebitSum; }
            set { SetPropertyValue("DebitSum", ref _DebitSum, value); }
        }
        //6.EndCreditBalance - money
        public decimal EndCreditBalance
        {
            get { return _EndCreditBalance; }
            set { SetPropertyValue("EndCreditBalance", ref _EndCreditBalance, value); }
        }
        //7.EndDebitBalance - money
        public decimal EndDebitBalance
        {
            get { return _EndDebitBalance; }
            set { SetPropertyValue("EndDebitBalance", ref _EndDebitBalance, value); }
        }
        //8.RowStatus - short
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }

        #endregion

        #region References
        //NAS.DAL.BI.Accounting.Finance.SupplierLiability : 6 References
        //1.References (1)-(n): Get Colection
        [Association("FinancialSupplierLiabilitySummaryFactReferencesFinancialSupplierLiabilityDetail")]
        public XPCollection<FinancialSupplierLiabilityDetail> FinancialSupplierLiabilityDetails
        {
            get { return GetCollection<FinancialSupplierLiabilityDetail>("FinancialSupplierLiabilityDetails"); }
        }
        //2.References(n)-(1):[1] By Id
        [Association("FinancialSupplierLiabilitySummaryFactReferencesSupplierOrgDim")]
        public SupplierOrgDim SupplierOrgDimId
        {
            get { return _SupplierOrgDimId; }
            set { SetPropertyValue("SupplierOrgDimId", ref _SupplierOrgDimId, value); }
        }        
        //3.(n)-(1): By Id
        [Association("FinancialSupplierLiabilitySummaryFactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return _MonthDimId; }
            set { SetPropertyValue("MonthDimId", ref _MonthDimId, value); }
        }
        //4.(n)-(1): By Id
        [Association("FinancialSupplierLiabilitySummaryFactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return _YearDimId; }
            set { SetPropertyValue("YearDimId", ref _YearDimId, value); }
        }
        //5.(n)-(1): By Id
        [Association("FinancialSupplierLiabilitySummaryFactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return _OwnerOrgDimId; }
            set { SetPropertyValue("OwnerOrgDimId", ref _OwnerOrgDimId, value); }
        }
        //6.(n)-(1): By Id
        [Association("FinancialSupplierLiabilitySummaryFactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return _FinancialAccountDimId; }
            set { SetPropertyValue("FinancialAccountDimId", ref _FinancialAccountDimId, value); }
        }
        

        #endregion
    }
}
