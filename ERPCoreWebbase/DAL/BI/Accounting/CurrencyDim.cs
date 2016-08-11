using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Accounting
{
    public class CurrencyDim : XPCustomObject
    {

        public CurrencyDim(Session session)
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

        #region Field [5]

        // Fields...
        private CurrencyDim _ParentCurrencyDimId;
        private short _RowStatus;
        private string _Description;
        private string _Name;
        private string _Code;
        private int _CurrencyDimId;

        [Key(true)]
        public int CurrencyDimId
        {
            get { return _CurrencyDimId; }
            set { SetPropertyValue("CurrencyDimId", ref _CurrencyDimId, value); }
        }
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue("Code", ref _Code, value); }
        }
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }

        #endregion

        #region References
        //References: (1)-(n) Get Collection
        [Association("FinancialSupplierLiabilityDetailReferencesCurrencyDim")]
        public XPCollection<FinancialSupplierLiabilityDetail> FinancialSupplierLiabilityDetails
        {
            get { return GetCollection<FinancialSupplierLiabilityDetail>("FinancialSupplierLiabilityDetails"); }
        }
        //References Parents
        //1.(n)-(1)
        [Association("ParentOf")]
        public CurrencyDim ParentCurrencyDimId
        {
            get { return _ParentCurrencyDimId; }
            set { SetPropertyValue("ParentCurrencyDimId", ref _ParentCurrencyDimId, value); }
        }
        //2.(1)-(n)
        [Association("ParentOf", typeof(CurrencyDim)), Aggregated]
        public XPCollection<CurrencyDim> Relations
        {
            get { return GetCollection<CurrencyDim>("Relations"); }
        }

        [Association(@"FinancialCustomerLiabilityDetailReferencesCurrencyDim", typeof(FinancialCustomerLiabilityDetail))]
        public XPCollection<FinancialCustomerLiabilityDetail> FinancialCustomerLiabilityDetails { get { return GetCollection<FinancialCustomerLiabilityDetail>("FinancialCustomerLiabilityDetails"); } }


        [Association(@"DiaryJournal_DetailReferencesCurrencyDim", typeof(DiaryJournal_Detail)), Aggregated]
        public XPCollection<DiaryJournal_Detail> DiaryJournal_Details
        {
            get { return GetCollection<DiaryJournal_Detail>("DiaryJournal_Details"); }
        }

        [Association(@"FinancialActualPriceDetailReferencesCurrencyDim", typeof(FinancialActualPriceDetail)), Aggregated]
        public XPCollection<FinancialActualPriceDetail> FinancialActualPriceDetails
        {
            get { return GetCollection<FinancialActualPriceDetail>("FinancialActualPriceDetails"); }
        }

        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesCurrencyDim", typeof(FinancialSalesOrManufactureExpenseDetail)), Aggregated]
        public XPCollection<FinancialSalesOrManufactureExpenseDetail> FinancialSalesOrManufactureExpenseDetails
        {
            get { return GetCollection<FinancialSalesOrManufactureExpenseDetail>("FinancialSalesOrManufactureExpenseDetails"); }
        }

        [Association(@"FinancialGeneralLedgerByMonth-CurrencyDim", typeof(FinancialGeneralLedgerByMonth))]
        public XPCollection<FinancialGeneralLedgerByMonth> FinancialGeneralLedgerByMonths { get { return GetCollection<FinancialGeneralLedgerByMonth>("FinancialGeneralLedgerByMonths"); } }

        [Association(@"FinancialPrepaidExpenseDetailReferencesCurrencyDim", typeof(FinancialPrepaidExpenseDetail)), Aggregated]
        public XPCollection<FinancialPrepaidExpenseDetail> FinancialPrepaidExpenseDetails
        { get { return GetCollection<FinancialPrepaidExpenseDetail>("FinancialPrepaidExpenseDetails"); } }

        [Association(@"GoodsInTransitForSaleDetailReferencesCurrencyDim", typeof(GoodsInTransitForSaleDetail)), Aggregated]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails
        { get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); } }

        [Association(@"GoodsInInventoryDetailReferencesCurrencyDim", typeof(GoodsInInventoryDetail)), Aggregated]
        public XPCollection<GoodsInInventoryDetail> GoodsInInventoryDetails
        { get { return GetCollection<GoodsInInventoryDetail>("GoodsInInventoryDetails"); } }

        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesCurrencyDim", typeof(FinancialOnTheWayBuyingGoodDetail)), Aggregated]
        public XPCollection<FinancialOnTheWayBuyingGoodDetail> FinancialOnTheWayBuyingGoodDetails
        { get { return GetCollection<FinancialOnTheWayBuyingGoodDetail>("FinancialOnTheWayBuyingGoodDetails"); } }


        [Association(@"FinancialEntryDetail-CurrencyDim", typeof(FinancialEntryDetail))]
        public XPCollection<FinancialEntryDetail> FinancialEntryDetails { get { return GetCollection<FinancialEntryDetail>("FinancialEntryDetails"); } }
        #endregion

    }
}
