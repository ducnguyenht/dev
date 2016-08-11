using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.ETL.Log;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.System.ShareDim
{
    public class YearDim : XPCustomObject, IDALValidate
    {
        public YearDim(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        #region properties

        // Fields...
        private short _RowStatus;
        private string _Name;
        private string _Description;
        private int _YearDimId;

        //1.Field YearDimId
        [Key(true)]
        public int YearDimId
        {
            get { return _YearDimId; }
            set { SetPropertyValue("YearDimId", ref _YearDimId, value); }
        }
        //2.Field Description
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        //3.Field Name
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        //4.Field Rowstatus
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        //1.
        [Association("FinancialDoubleEntry_FactReferencesYearDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations { get { return GetCollection<FinancialDoubleEntry_Fact>("Relations"); } }

        [Association("YearDimReferencesETLJobLog")]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }

        [Association("FinancialRevenueByItem_FactReferencesYearDim")]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts { get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); } }

        [Association(@"FinancialSupplierLiabilitySummaryFactReferencesYearDim", typeof(FinancialSupplierLiabilitySummary_Fact)), Aggregated]
        public XPCollection<FinancialSupplierLiabilitySummary_Fact> FinancialSupplierLiabilitySummary_Facts { get { return GetCollection<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_Facts"); } }

        [Association(@"FinancialCustomerLiabilitySummary_FactReferencesYearDim", typeof(FinancialCustomerLiabilitySummary_Fact))]
        public XPCollection<FinancialCustomerLiabilitySummary_Fact> FinancialCustomerLiabilitySummary_Facts { get { return GetCollection<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_Facts"); } }

        [Association(@"FinancialOnTHeWayBuyingGoodSummaryReferencesYearDim", typeof(FinancialOnTheWayBuyingGoodSummary))]
        public XPCollection<FinancialOnTheWayBuyingGoodSummary> FinancialOnTheWayBuyingGoodSummaries { get { return GetCollection<FinancialOnTheWayBuyingGoodSummary>("FinancialOnTheWayBuyingGoodSummaries"); } }

        [Association(@"DiaryJournal_FactReferencesYearDim", typeof(DiaryJournal_Fact)), Aggregated]
        public XPCollection<DiaryJournal_Fact> DiaryJournal_Facts { get { return GetCollection<DiaryJournal_Fact>("DiaryJournal_Facts"); } }

        [Association(@"FinancialActualPriceSummary_FactReferencesYearDim")]
        public XPCollection<FinancialActualPriceSummary_Fact> FinancialActualPriceSummary_Facts { get { return GetCollection<FinancialActualPriceSummary_Fact>("FinancialActualPriceSummary_Facts"); } }

        [Association(@"FinancailGeneralLedgerByYear_Fact-YearDim", typeof(FinancialGeneralLedgerByYear_Fact))]
        public XPCollection<FinancialGeneralLedgerByYear_Fact> FinancialGeneralLedgerByYear_Facts { get { return GetCollection<FinancialGeneralLedgerByYear_Fact>("FinancialGeneralLedgerByYear_Facts"); } }

        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesYearDim", typeof(FinancialSalesOrManufactureExpenseSummary_Fact))]
        public XPCollection<FinancialSalesOrManufactureExpenseSummary_Fact> FinancialSalesOrManufactureExpenseSummary_Facts { get { return GetCollection<FinancialSalesOrManufactureExpenseSummary_Fact>("FinancialSalesOrManufactureExpenseSummary_Facts"); } }

        [Association(@"FinancialPrepaidExpenseSummary_FactReferencesYearDim", typeof(FinancialPrepaidExpenseSummary_Fact))]
        public XPCollection<FinancialPrepaidExpenseSummary_Fact> FinancialPrepaidExpenseSummary_Facts
        { get { return GetCollection<FinancialPrepaidExpenseSummary_Fact>("FinancialPrepaidExpenseSummary_Facts"); } }

        [Association("GoodsInTransitForSaleSummary_FactReferencesYearDim", typeof(GoodsInTransitForSaleSummary_Fact))]
        public XPCollection<GoodsInTransitForSaleSummary_Fact> GoodsInTransitForSaleSummary_Facts { get { return GetCollection<GoodsInTransitForSaleSummary_Fact>("GoodsInTransitForSaleSummary_Facts"); } }

        [Association("GoodsInInventorySummary_FactReferencesYearDim", typeof(GoodsInInventorySummary_Fact))]
        public XPCollection<GoodsInInventorySummary_Fact> GoodsInInventorySummary_Facts { get { return GetCollection<GoodsInInventorySummary_Fact>("GoodsInInventorySummary_Facts"); } }

        [Association(@"FinancialItemInventorySummary_Fact-YearDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }
        #endregion

        #region Method Populate

        private const string DEFAULT = "2014";
        public static void Populate()
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                YearDim defaultYearDim = uow.FindObject<YearDim>(new BinaryOperator("Name", DEFAULT));//check is Exist
                if (defaultYearDim == null)
                {
                    defaultYearDim = new YearDim(uow)//Create new record
                    {
                        Name = DEFAULT,//DEFAUL="2014"
                        Description = "DEFAUL",
                        RowStatus = 1
                    };
                    uow.CommitChanges();//Save
                }
            }
        }

        #endregion

        #region Method GetDefault

        private static YearDim _Default;
        public static YearDim GetDefault(Session session)
        {
            if (_Default == null)
            {
                _Default = session.FindObject<YearDim>(new BinaryOperator("Name", DEFAULT));
            }
            return _Default;
        }
        #endregion

        #region Method Check Exist Object

        //Method IsExistObject
        public static bool isExistXpoObject<T>(string fieldName, object value)
        {
            Session session = XpoHelper.GetNewSession();
            try
            {
                CriteriaOperator cri = new BinaryOperator(fieldName, value, BinaryOperatorType.Equal);
                var result = session.GetObjects(session.GetClassInfo(typeof(T)), cri, null, 0, false, true);
                if (result != null && result.Count > 0) { return true; }
                else { return false; }
            }
            catch (Exception) { throw; }
            finally { if (session != null) { session.Dispose(); } }
        }
        #endregion

        #region Validate
        #region Validate Parameter
        //1.Validate Name
        private bool ValidateName()
        {
            if (this.Name != null && this.Name.Length != 4)
            {
                return false;
            }
            return true;
        }
        //2.Validate Description
        private bool ValidateDescription()
        {
            if (this.Description != null && this.Description.Length > 200)
            {
                return false;
            }
            return true;
        }
        //3.Validate RowStatus
        private bool ValidateRowstatus()
        {
            return true;
        }
        //4.Validate YearDimId
        private bool ValidateYearDimId()
        {
            return true;
        }
        //Validate Parameter
        public bool ValidateParameter()
        {
            if (!ValidateName())
            {
                return false;
            }
            if (!ValidateDescription())
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Validate Unique
        //Validate Unique
        public bool ValidateUnique()
        {
            return true;
        }
        #endregion

        #region Validate IsExist
        //Validate IsExist
        public bool IsExist()
        {
            return true;
        }
        #endregion

        #region Method Onsaving
        //Override Method OnSaving
        //protected override void OnSaving()
        //{
        //    if (!ValidateParameter())
        //    {
        //        throw new Exception("Error Validate Parameter!!!");
        //    }
        //    if (!ValidateUnique())
        //    {
        //        throw new Exception("Error Validate Unique!!!");
        //    }
        //    if (!IsExist())
        //    {
        //        throw new Exception("Error Validate IsExist!!!");
        //    }
        //    base.OnSaving();
        //}
        #endregion
        #endregion
    }
}
