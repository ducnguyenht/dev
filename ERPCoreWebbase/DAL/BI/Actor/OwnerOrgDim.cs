using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Actor
{
    public class OwnerOrgDim : XPCustomObject
    {
        public OwnerOrgDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fOwnerOrgDimId;
        [Key(true)]
        public int OwnerOrgDimId
        {
            get { return fOwnerOrgDimId; }
            set { SetPropertyValue<int>("OwnerOrgDimId", ref fOwnerOrgDimId, value); }
        }

        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association("FinancialDoubleEntry_FactReferencesOwnerOrgDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations
        {
            get { return GetCollection<FinancialDoubleEntry_Fact>("Relations"); }
        }


        [Association(@"FinancialRevenueByItem_FactReferencesOwnerOrgDim", typeof(FinancialRevenueByItem_Fact)), Aggregated]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }

        [Association(@"FinancialSupplierLiabilitySummaryFactReferencesOwnerOrgDim", typeof(FinancialSupplierLiabilitySummary_Fact)), Aggregated]
        public XPCollection<FinancialSupplierLiabilitySummary_Fact> FinancialSupplierLiabilitySummary_Facts
        {
            get { return GetCollection<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_Facts"); }
        }

        [Association(@"FinancialCustomerLiabilitySummary_FactReferencesOwnerOrgDim", typeof(FinancialCustomerLiabilitySummary_Fact))]
        public XPCollection<FinancialCustomerLiabilitySummary_Fact> FinancialCustomerLiabilitySummary_Facts { get { return GetCollection<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_Facts"); } }

        [Association(@"FinancialOnTheWayBuyingGoodSummaryReferencesOwnerOrgDim", typeof(FinancialOnTheWayBuyingGoodSummary))]
        public XPCollection<FinancialOnTheWayBuyingGoodSummary> FinancialOnTheWayBuyingGoodSummaries { get { return GetCollection<FinancialOnTheWayBuyingGoodSummary>("FinancialOnTheWayBuyingGoodSummaries"); } }

        [Association(@"DiaryJournal_FactReferencesOwnerOrgDim", typeof(DiaryJournal_Fact)), Aggregated]
        public XPCollection<DiaryJournal_Fact> DiaryJournal_Facts
        {
            get { return GetCollection<DiaryJournal_Fact>("DiaryJournal_Facts"); }
        }

        [Association(@"FinancialActualPriceSummary_FactReferencesOwnerOrgDim")]
        public XPCollection<FinancialActualPriceSummary_Fact> FinancialActualPriceSummary_Facts
        {
            get { return GetCollection<FinancialActualPriceSummary_Fact>("FinancialActualPriceSummary_Facts"); }
        }

        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesOwnerOrgDim", typeof(FinancialSalesOrManufactureExpenseSummary_Fact))]
        public XPCollection<FinancialSalesOrManufactureExpenseSummary_Fact> FinancialSalesOrManufactureExpenseSummary_Facts { get { return GetCollection<FinancialSalesOrManufactureExpenseSummary_Fact>("FinancialSalesOrManufactureExpenseSummary_Facts"); } }

        [Association(@"FinancialPrepaidExpenseSummary_FactReferencesOwnerOrgDim", typeof(FinancialPrepaidExpenseSummary_Fact))]
        public XPCollection<FinancialPrepaidExpenseSummary_Fact> FinancialPrepaidExpenseSummary_Facts
        { get { return GetCollection<FinancialPrepaidExpenseSummary_Fact>("FinancialPrepaidExpenseSummary_Facts"); } }

        [Association("GoodsInTransitForSaleSummary_FactReferencesOwnerOrgDim", typeof(GoodsInTransitForSaleSummary_Fact))]
        public XPCollection<GoodsInTransitForSaleSummary_Fact> GoodsInTransitForSaleSummary_Facts { get { return GetCollection<GoodsInTransitForSaleSummary_Fact>("GoodsInTransitForSaleSummary_Facts"); } }

        [Association("GoodsInInventorySummary_FactReferencesOwnerOrgDim", typeof(GoodsInInventorySummary_Fact))]
        public XPCollection<GoodsInInventorySummary_Fact> GoodsInInventorySummary_Facts { get { return GetCollection<GoodsInInventorySummary_Fact>("GoodsInInventorySummary_Facts"); } }

        [Association(@"FinancialItemInventorySummary_Fact-OwnerOrgDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }
        #endregion
    }
}
