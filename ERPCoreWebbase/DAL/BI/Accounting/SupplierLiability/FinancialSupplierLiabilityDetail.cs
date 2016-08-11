using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.BI.Accounting.SupplierLiability
{
    public class FinancialSupplierLiabilityDetail:XPCustomObject
    {
        public FinancialSupplierLiabilityDetail(Session session)
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

        #region Field [4]       
        // Fields...
        private FinancialTransactionDim _FinancialTransactionDimId;
        private CorrespondFinancialAccountDim _CorrespondFinancialAccountDimId;
        private FinancialAccountDim _FinancialAccountDimId;
        private CurrencyDim _CurrencyDimId;
        private FinancialSupplierLiabilitySummary_Fact _FinancialSupplierLiabilitySummary_FactId;
        private decimal _ActualPrice;
        private decimal _BookingPrice;                
        private decimal _Debit;
        private decimal _Credit;
        private short _RowStatus;
        private int _FinancialCustomerLiabilityDetailId;

        //1.FinancialCustomerLiabilityDetailId - int - [Key]
        [Key(true)]
        public int FinancialCustomerLiabilityDetailId
        {
            get { return _FinancialCustomerLiabilityDetailId; }
            set { SetPropertyValue("FinancialCustomerLiabilityDetailId", ref _FinancialCustomerLiabilityDetailId, value); }
        }
        //2.Credit - money
        public decimal Credit
        {
            get { return _Credit; }
            set { SetPropertyValue("Credit", ref _Credit, value); }
        }
        //3.Debit - money
        public decimal Debit
        {
            get { return _Debit; }
            set { SetPropertyValue("Debit", ref _Debit, value); }
        }
        //4.RowStatus - short
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }
        //5.ActualPrice - money
        public decimal ActualPrice
        {
            get { return _ActualPrice; }
            set { SetPropertyValue("ActualPrice", ref _ActualPrice, value); }
        }
        //6.BookingPrice - money
        public decimal BookingPrice
        {
            get { return _BookingPrice; }
            set { SetPropertyValue("BookingPrice", ref _BookingPrice, value); }
        }
        #endregion

        #region References
        //NAS.DAL.BI.Accounting.Finance.CustomerLiability : 5 References (n)-(1): by Id
        //1.
        [Association("FinancialSupplierLiabilitySummaryFactReferencesFinancialSupplierLiabilityDetail")]
        public FinancialSupplierLiabilitySummary_Fact FinancialSupplierLiabilitySummary_FactId
        {
            get { return _FinancialSupplierLiabilitySummary_FactId; }
            set { SetPropertyValue<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_FactId", ref _FinancialSupplierLiabilitySummary_FactId, value); }
        }
        //2.
        [Association("FinancialSupplierLiabilityDetailReferencesCurrencyDim")]
        public CurrencyDim CurrencyDimId
        {
            get { return _CurrencyDimId; }
            set { SetPropertyValue("CurrencyDimId", ref _CurrencyDimId, value); }
        }
        //3.
        [Association("FinancialSupplierLiabilityDetailReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return _FinancialAccountDimId; }
            set { SetPropertyValue("FinancialAccountDimId", ref _FinancialAccountDimId, value); }
        }
        //4.
        [Association("FinancialSupplierLiabilityDetailReferencesCorrespondFinancialAccountDim")]
        public CorrespondFinancialAccountDim CorrespondFinancialAccountDimId
        {
            get { return _CorrespondFinancialAccountDimId; }
            set { SetPropertyValue("CorrespondFinancialAccountDimId", ref _CorrespondFinancialAccountDimId, value); }
        }
        //5.
        [Association("FinancialSupplierLiabilityDetailReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return _FinancialTransactionDimId; }
            set { SetPropertyValue("FinancialTransactionDimId", ref _FinancialTransactionDimId, value); }
        }
      
        #endregion
    }
}
