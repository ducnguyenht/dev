using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Item;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.BI.Actor;
using NAS.DAL.ETL.Log;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.BillDim;

namespace NAS.DAL.BI.Accounting
{
    public class FinancialRevenueByItem_Fact : XPCustomObject
    {
        public FinancialRevenueByItem_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fFinancialRevenueByItem_FactId;
        [Key(true)]
        public int FinancialRevenueByItem_FactId
        {
            get { return fFinancialRevenueByItem_FactId; }
            set { SetPropertyValue<int>("FinancialRevenueByItem_FactId", ref fFinancialRevenueByItem_FactId, value); }
        }

        double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        UnitDim fUnitDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesUnitDim")]
        public UnitDim UnitDimId
        {
            get { return fUnitDimId; }
            set { SetPropertyValue<UnitDim>("UnitDimId", ref fUnitDimId, value); }
        }

        ItemDim fItemDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesItemDim")]
        public ItemDim ItemDimId
        {
            get { return fItemDimId; }
            set { SetPropertyValue<ItemDim>("ItemDimId", ref fItemDimId, value); }
        }

        OwnerOrgDim fOwnerOrgDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<OwnerOrgDim>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        FinancialAssetDim fFinancialAssetDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesFinancialAssetDim")]
        public FinancialAssetDim FinancialAssetDimId
        {
            get { return fFinancialAssetDimId; }
            set { SetPropertyValue<FinancialAssetDim>("FinancialAssetDimId", ref fFinancialAssetDimId, value); }
        }

        InvoiceDim fInvoiceDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesInvoiceDim")]
        public InvoiceDim InvoiceDimId
        {
            get { return fInvoiceDimId; }
            set { SetPropertyValue<InvoiceDim>("InvoiceDimId", ref fInvoiceDimId, value); }
        }

        DayDim fDayDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesDayDim")]
        public DayDim DayDimId
        {
            get { return fDayDimId; }
            set { SetPropertyValue<DayDim>("DayDimId", ref fDayDimId, value); }
        }

        YearDim fYearDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }

        MonthDim fMonthDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }

        CustomerOrgDim fCustomerOrgDimId;
        [Association(@"FinancialRevenueByItem_FactReferencesCustomerOrgDim")]
        public CustomerOrgDim CustomerOrgDimId
        {
            get { return fCustomerOrgDimId; }
            set { SetPropertyValue<CustomerOrgDim>("CustomerOrgDimId", ref fCustomerOrgDimId, value); }
        }
        #endregion
    }
}
