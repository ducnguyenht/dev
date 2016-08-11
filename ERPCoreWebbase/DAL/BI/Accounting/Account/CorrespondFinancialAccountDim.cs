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
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.Cash;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Accounting.Account
{
    public enum CorrespondFinancialAccountDimEnum
    {
        NAAN_DEFAULT
    }

    public class CorrespondFinancialAccountDim : XPCustomObject
    {

        public CorrespondFinancialAccountDim(Session session)
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
        #region Logic
        public static CorrespondFinancialAccountDim GetDefault(Session session, CorrespondFinancialAccountDimEnum code)
        {
            CorrespondFinancialAccountDim ret = null;
            try
            {
                ret = session.FindObject<CorrespondFinancialAccountDim>(
                    new BinaryOperator("Code", Enum.GetName(typeof(CorrespondFinancialAccountDimEnum), code)));
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
                if (!Util.isExistXpoObject<CorrespondFinancialAccountDim>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    CorrespondFinancialAccountDim account = new CorrespondFinancialAccountDim(session)
                    {
                        Description = "Default Corresponding Financial AccountDim",
                        Name = "Default Corresponding Financial AccountDim",
                        RowStatus = 1,
                        Code = Utility.Constant.NAAN_DEFAULT_CODE
                    };

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

        #region [4 ]Field

        // Fields...
        private string _Code;
        private string _Name;
        private string _Description;
        private short _RowStatus;
        private int _CorrespondFinancialAccountDimId;
        //1.CorrespondFinancialAccountDimId - Guid - [Key]
        [Key(true)]
        public int CorrespondFinancialAccountDimId
        {
            get { return _CorrespondFinancialAccountDimId; }
            set { SetPropertyValue("CorrespondFinancialAccountDimId", ref _CorrespondFinancialAccountDimId, value); }
        }
        //2.Description - string
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        //3.Name - string
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        //4.RowStatus - short
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue("Code", ref _Code, value); }
        }

        #endregion

        #region  References
        //[FinancialSupplierLiabilityDetail]
        [Association("FinancialSupplierLiabilityDetailReferencesCorrespondFinancialAccountDim")]
        public XPCollection<FinancialSupplierLiabilityDetail> FinancialSupplierLiabilityDetails
        {
            get { return GetCollection<FinancialSupplierLiabilityDetail>("FinancialSupplierLiabilityDetails"); }
        }

        [Association("FinancialCustomerLiabilityDetailReferencesCorrespondFinancialAccountDim", typeof(FinancialCustomerLiabilityDetail))]
        public XPCollection<FinancialCustomerLiabilityDetail> FinancialCustomerLiabilityDetails { get { return GetCollection<FinancialCustomerLiabilityDetail>("FinancialCustomerLiabilityDetails"); } }

        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesCorrespondFinancialAccountDim", typeof(FinancialOnTheWayBuyingGoodDetail))]
        public XPCollection<FinancialOnTheWayBuyingGoodDetail> FinancialOnTheWayBuyingGoodDetails { get { return GetCollection<FinancialOnTheWayBuyingGoodDetail>("FinancialOnTheWayBuyingGoodDetails"); } }

        [Association(@"DiaryJournal_DetailReferencesCorrespondFinancialAccountDim", typeof(DiaryJournal_Detail)), Aggregated]
        public XPCollection<DiaryJournal_Detail> DiaryJournal_Details
        {
            get { return GetCollection<DiaryJournal_Detail>("DiaryJournal_Details"); }
        }

        [Association(@"FinancialActualPriceDetailReferencesCorrespondFinancialAccountDim", typeof(FinancialActualPriceDetail)), Aggregated]
        public XPCollection<FinancialActualPriceDetail> FinancialActualPriceDetails
        {
            get { return GetCollection<FinancialActualPriceDetail>("FinancialActualPriceDetails"); }
        }

        [Association(@"FinancialSalesOrManufactureExpenseDetailReferencesCorrespondFinancialAccountDim", typeof(FinancialSalesOrManufactureExpenseDetail))]
        public XPCollection<FinancialSalesOrManufactureExpenseDetail> FinancialSalesOrManufactureExpenseDetails { get { return GetCollection<FinancialSalesOrManufactureExpenseDetail>("FinancialSalesOrManufactureExpenseDetails"); } }

        [Association(@"FinancialGeneralLedgerByMonth-CorrespondFinancialAccountDim", typeof(FinancialGeneralLedgerByMonth))]
        public XPCollection<FinancialGeneralLedgerByMonth> FinancialGeneralLedgerByMonths { get { return GetCollection<FinancialGeneralLedgerByMonth>("FinancialGeneralLedgerByMonths"); } }
        
        [Association(@"FinancialPrepaidExpenseDetailReferencesCorrespondFinancialAccountDim", typeof(FinancialPrepaidExpenseDetail)), Aggregated]
        public XPCollection<FinancialPrepaidExpenseDetail> FinancialPrepaidExpenseDetails
        { get { return GetCollection<FinancialPrepaidExpenseDetail>("FinancialPrepaidExpenseDetails"); } }

        [Association(@"GoodsInTransitForSaleDetailReferencesCorrespondFinancialAccountDim", typeof(GoodsInTransitForSaleDetail)), Aggregated]
        public XPCollection<GoodsInTransitForSaleDetail> GoodsInTransitForSaleDetails
        { get { return GetCollection<GoodsInTransitForSaleDetail>("GoodsInTransitForSaleDetails"); } }

        [Association(@"GoodsInInventoryDetailReferencesCorrespondFinancialAccountDim", typeof(GoodsInInventoryDetail)), Aggregated]
        public XPCollection<GoodsInInventoryDetail> GoodsInInventoryDetails
        { get { return GetCollection<GoodsInInventoryDetail>("GoodsInInventoryDetails"); } }

        [Association(@"FinancialCash_FactReferencesCorrespondFinancialAccountDim", typeof(FinancialCash_Fact)), Aggregated]
        public XPCollection<FinancialCash_Fact> FinancialCash_Facts
        {
            get { return GetCollection<FinancialCash_Fact>("FinancialCash_Facts"); }
        }

        [Association(@"ItemInventoryByArtifact-CorrespondFinancialAccountDim", typeof(ItemInventoryByArtifact))]
        public XPCollection<ItemInventoryByArtifact> ItemInventoryByArtifacts { get { return GetCollection<ItemInventoryByArtifact>("ItemInventoryByArtifacts"); } }

        [Association(@"FinancialEntryDetail-CorrespondFinancialAccountDim", typeof(FinancialEntryDetail))]
        public XPCollection<FinancialEntryDetail> FinancialEntryDetails { get { return GetCollection<FinancialEntryDetail>("FinancialEntryDetails"); } }
        #endregion

       
    }
}
