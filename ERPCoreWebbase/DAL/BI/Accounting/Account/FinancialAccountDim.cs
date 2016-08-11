using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.Report;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.Cash;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Accounting.Account
{
    public enum FinancialAccountDimEnum
    {
        NAAN_DEFAULT
    }

    public class FinancialAccountDim : XPCustomObject
    {
        public FinancialAccountDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static FinancialAccountDim GetDefault(Session session, FinancialAccountDimEnum code)
        {
            FinancialAccountDim ret = null;
            try
            {
                ret = session.FindObject<FinancialAccountDim>(
                    new BinaryOperator("Code", Enum.GetName(typeof(FinancialAccountDimEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<FinancialAccountDim>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    FinancialAccountDim account = new FinancialAccountDim(session);
                    account.Description = "Default Financial AccountDim";
                    account.Name = "Default Financial AccountDim";
                    account.RowStatus = 1;
                    account.Code = Utility.Constant.NAAN_DEFAULT_CODE;
                    account.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }
        #endregion

        #region Properties
        int fFinancialAccountDimId;
        [Key(true)]
        public int FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<int>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
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

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association("FinancialSupplierLiabilitySummaryFactReferencesFinancialAccountDim")]
        public XPCollection<FinancialSupplierLiabilitySummary_Fact> FinancialSupplierLiabilitySummary_Facts { get { return GetCollection<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_Facts"); } }

        [Association("FinancialSupplierLiabilityDetailReferencesFinancialAccountDim")]
        public XPCollection<FinancialSupplierLiabilityDetail> FinancialSupplierLiabilityDetails { get { return GetCollection<FinancialSupplierLiabilityDetail>("FinancialSupplierLiabilityDetails"); } }

        [Association("FinancialCustomerLiabilitySummary_FactReferencesFinancialAccountDim", typeof(FinancialCustomerLiabilitySummary_Fact))]
        public XPCollection<FinancialCustomerLiabilitySummary_Fact> FinancialCustomerLiabilitySummary_Facts { get { return GetCollection<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_Facts"); } }

        [Association("FinancialCustomerLiabilityDetailReferencesFinancialAccountDim", typeof(FinancialCustomerLiabilityDetail))]
        public XPCollection<FinancialCustomerLiabilityDetail> FinancialCustomerLiabilityDetails { get { return GetCollection<FinancialCustomerLiabilityDetail>("FinancialCustomerLiabilityDetails"); } }

        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesFinancialAccountDim", typeof(FinancialOnTheWayBuyingGoodDetail))]
        public XPCollection<FinancialOnTheWayBuyingGoodDetail> FinancialOnTheWayBuyingGoodDetails { get { return GetCollection<FinancialOnTheWayBuyingGoodDetail>("FinancialOnTheWayBuyingGoodDetails"); } }

        [Association(@"FinancialOnTheWayBuyingGoodSummaryReferencesFinancialAccountDim", typeof(FinancialOnTheWayBuyingGoodSummary))]
        public XPCollection<FinancialOnTheWayBuyingGoodSummary> FinancialOnTheWayBuyingGoodSummaries { get { return GetCollection<FinancialOnTheWayBuyingGoodSummary>("FinancialOnTheWayBuyingGoodSummaries"); } }

        [Association(@"DiaryJournal_FactReferencesFinancialAccountDim", typeof(DiaryJournal_Fact)), Aggregated]
        public XPCollection<DiaryJournal_Fact> DiaryJournal_Facts { get { return GetCollection<DiaryJournal_Fact>("DiaryJournal_Facts"); } }

        [Association("DiaryJournal_DetailReferencesFinancialAccountDim", typeof(DiaryJournal_Detail)), Aggregated]
        public XPCollection<DiaryJournal_Detail> DiaryJournal_Details { get { return GetCollection<DiaryJournal_Detail>("DiaryJournal_Details"); } }

        [Association(@"FinancialActualPriceSummary_FactReferencesFinancialAccountDim")]
        public XPCollection<FinancialActualPriceSummary_Fact> FinancialActualPriceSummary_Facts { get { return GetCollection<FinancialActualPriceSummary_Fact>("FinancialActualPriceSummary_Facts"); } }

        [Association(@"FinancialActualPriceDetailReferencesFinancialAccountDim", typeof(FinancialActualPriceDetail)), Aggregated]
        public XPCollection<FinancialActualPriceDetail> FinancialActualPriceDetails { get { return GetCollection<FinancialActualPriceDetail>("FinancialActualPriceDetails"); } }

        [Association(@"FinancialGeneralLedgerByYear_Fact-FinancialAccountDim", typeof(FinancialGeneralLedgerByYear_Fact))]
        public XPCollection<FinancialGeneralLedgerByYear_Fact> FinancialGeneralLedgerByYear_Facts { get { return GetCollection<FinancialGeneralLedgerByYear_Fact>("FinancialGeneralLedgerByYear_Facts"); } }

        [Association(@"SalesOrManufactureExpenseByGroupReferencesFinancialAccountDim", typeof(SalesOrManufactureExpenseByGroup))]
        public XPCollection<SalesOrManufactureExpenseByGroup> SalesOrManufactureExpenseByGroups { get { return GetCollection<SalesOrManufactureExpenseByGroup>("SalesOrManufactureExpenseByGroups"); } }

        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesFinancialAccountDim", typeof(FinancialSalesOrManufactureExpenseDetail))]
        public XPCollection<FinancialSalesOrManufactureExpenseDetail> FinancialSalesOrManufactureExpenseDetails { get { return GetCollection<FinancialSalesOrManufactureExpenseDetail>("FinancialSalesOrManufactureExpenseDetails"); } }

        [Association(@"FinancialBalanceAccount_FactReferencesFinancialAccountDim", typeof(FinancialBalanceAccount_Fact))]
        public XPCollection<FinancialBalanceAccount_Fact> FinancialBalanceAccount_Facts { get { return GetCollection<FinancialBalanceAccount_Fact>("FinancialBalanceAccount_Facts"); } }

        [Association(@"FinancialBusinessResult_FactReferencesFinancialAccountDim", typeof(FinancialBusinessResult_Fact))]
        public XPCollection<FinancialBusinessResult_Fact> FinancialBusinessResult_Facts { get { return GetCollection<FinancialBusinessResult_Fact>("FinancialBusinessResult_Facts"); } }

        [Association("FinancialPrepaidExpenseSummary_FactReferencesFinancialAccountDim", typeof(FinancialPrepaidExpenseSummary_Fact))]
        public XPCollection<FinancialPrepaidExpenseSummary_Fact> FinancialPrepaidExpenseSummary_Facts { get { return GetCollection<FinancialPrepaidExpenseSummary_Fact>("FinancialPrepaidExpenseSummary_Facts"); } }

        [Association("FinancialPrepaidExpenseDetailReferencesFinancialAccountDim", typeof(FinancialPrepaidExpenseDetail))]
        public XPCollection<FinancialPrepaidExpenseDetail> FinancialPrepaidExpenseDetails { get { return GetCollection<FinancialPrepaidExpenseDetail>("FinancialPrepaidExpenseDetails"); } }

        [Association("GoodsInTransitForSaleSummary_FactReferencesFinancialAccountDim", typeof(GoodsInTransitForSaleSummary_Fact))]
        public XPCollection<GoodsInTransitForSaleSummary_Fact> GoodsInTransitForSaleSummary_Facts { get { return GetCollection<GoodsInTransitForSaleSummary_Fact>("GoodsInTransitForSaleSummary_Facts"); } }

        [Association("GoodsInTransitForSaleDetailReferencesFinancialAccountDim", typeof(GoodsInTransitForSaleDetail))]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails { get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); } }

        [Association("GoodsInInventorySummary_FactReferencesFinancialAccountDim", typeof(GoodsInInventorySummary_Fact))]
        public XPCollection<GoodsInInventorySummary_Fact> GoodsInInventorySummary_Facts { get { return GetCollection<GoodsInInventorySummary_Fact>("GoodsInInventorySummary_Facts"); } }

        [Association("GoodsInInventoryDetailReferencesFinancialAccountDim", typeof(GoodsInInventoryDetail))]
        public XPCollection<GoodsInInventoryDetail> GoodsInInventoryDetails { get { return GetCollection<GoodsInInventoryDetail>("GoodsInInventoryDetails"); } }

        [Association(@"FinancialCash_FactReferencesFinancialAccountDim", typeof(FinancialCash_Fact)), Aggregated]
        public XPCollection<FinancialCash_Fact> FinancialCash_Facts
        {
            get { return GetCollection<FinancialCash_Fact>("FinancialCash_Facts"); }
        }

        [Association(@"FinancialItemInventorySummary_Fact-FinancialAccountDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }

        [Association("FinancialEntryDetail-FinancialAccountDim", typeof(FinancialEntryDetail))]
        public XPCollection<FinancialEntryDetail> FinancialEntryDetails { get { return GetCollection<FinancialEntryDetail>("FinancialEntryDetails"); } }

        [Association("FinancialDescriptionReportT2_FactReferencesFinancialAccountDim", typeof(FinancialDescriptionReportT2_Fact))]
        public XPCollection<FinancialDescriptionReportT2_Fact> FinancialDescriptionReportT2_Facts { get { return GetCollection<FinancialDescriptionReportT2_Fact>("FinancialDescriptionReportT2_Facts"); } }
        #endregion
    }
}
