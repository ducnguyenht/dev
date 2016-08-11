using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Accounting
{
    public class FinancialTransactionDim : XPCustomObject
    {
        public FinancialTransactionDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fFinancialTransactionDimId;
        [Key(true)]
        public int FinancialTransactionDimId
        {
            get { return fFinancialTransactionDimId; }
            set { SetPropertyValue<int>("FinancialTransactionDimId", ref fFinancialTransactionDimId, value); }
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

        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"FinancialDoubleEntry_FactsReferencesFinancialTransactionDim", typeof(FinancialDoubleEntry_Fact)), Aggregated]
        public XPCollection<FinancialDoubleEntry_Fact> FinancialDoubleEntry_Facts
        {
            get { return GetCollection<FinancialDoubleEntry_Fact>("FinancialDoubleEntry_Facts"); }
        }

        [Association("FinancialSupplierLiabilityDetailReferencesFinancialTransactionDim")]
        public XPCollection<FinancialSupplierLiabilityDetail> FinancialSupplierLiabilityDetails
        {
            get { return GetCollection<FinancialSupplierLiabilityDetail>("FinancialSupplierLiabilityDetails"); }
        }

        [Association("FinancialCustomerLiabilityDetailReferencesFinancialTransactionDim", typeof(FinancialCustomerLiabilityDetail))]
        public XPCollection<FinancialCustomerLiabilityDetail> FinancialCustomerLiabilityDetails { get { return GetCollection<FinancialCustomerLiabilityDetail>("FinancialCustomerLiabilityDetails"); } }

        [Association("DiaryJournal_DetailReferencesFinancialTransactionDim", typeof(DiaryJournal_Detail)), Aggregated]
        public XPCollection<DiaryJournal_Detail> DiaryJournal_Details 
        {
            get { return GetCollection<DiaryJournal_Detail>("DiaryJournal_Details"); } 
        }

        [Association(@"FinancialActualPriceDetailReferencesFinancialTransactionDim", typeof(FinancialActualPriceDetail)), Aggregated]
        public XPCollection<FinancialActualPriceDetail> FinancialActualPriceDetails
        {
            get { return GetCollection<FinancialActualPriceDetail>("FinancialActualPriceDetails"); }
        }

        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesFinancialTransactionDim", typeof(FinancialSalesOrManufactureExpenseDetail)), Aggregated]
        public XPCollection<FinancialSalesOrManufactureExpenseDetail> FinancialSalesOrManufactureExpenseDetails { get { return GetCollection<FinancialSalesOrManufactureExpenseDetail>("FinancialSalesOrManufactureExpenseDetails"); } }

        [Association(@"FinancialPrepaidExpenseDetailReferencesFinancialTransactionDim", typeof(FinancialPrepaidExpenseDetail)), Aggregated]
        public XPCollection<FinancialPrepaidExpenseDetail> FinancialPrepaidExpenseDetails
        { get { return GetCollection<FinancialPrepaidExpenseDetail>("FinancialPrepaidExpenseDetails"); } }

        [Association(@"GoodsInTransitForSaleDetailReferencesFinancialTransactionDim", typeof(GoodsInTransitForSaleDetail)), Aggregated]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails
        { get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); } }

        [Association(@"GoodsInInventoryDetailReferencesFinancialTransactionDim", typeof(GoodsInInventoryDetail)), Aggregated]
        public XPCollection<GoodsInInventoryDetail> GoodsInInventoryDetails
        { get { return GetCollection<GoodsInInventoryDetail>("GoodsInInventoryDetails"); } }

        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesFinancialTransactionDim", typeof(FinancialOnTheWayBuyingGoodDetail)), Aggregated]
        public XPCollection<FinancialOnTheWayBuyingGoodDetail> FinancialOnTheWayBuyingGoodDetails
        { get { return GetCollection<FinancialOnTheWayBuyingGoodDetail>("FinancialOnTheWayBuyingGoodDetails"); } }

        [Association(@"FinancialEntryDetail-FinancialTransactionDim", typeof(FinancialEntryDetail))]
        public XPCollection<FinancialEntryDetail> FinancialEntryDetails { get { return GetCollection<FinancialEntryDetail>("FinancialEntryDetails"); } }

       
        #endregion
    }
}
