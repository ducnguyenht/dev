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
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.BI.Accounting.GeneralLedger;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.System.ShareDim
{
    #region MonthDimEnum
    public enum MonthDimEnum
    {
        Month_01 = 1,
        Month_02 = 2,
        Month_03 = 3,
        Month_04 = 4,
        Month_05 = 5,
        Month_06 = 6,
        Month_07 = 7,
        Month_08 = 8,
        Month_09 = 9,
        Month_10 = 10,
        Month_11 = 11,
        Month_12 = 12,
        Last
    }
    #endregion

    public class MonthDim : XPCustomObject, IDALValidate
    {

        private static string _Default;

        public MonthDim(Session session): base(session){}

        public override void AfterConstruction() { base.AfterConstruction(); }


        #region properties
        // Fields...
        private short _RowStatus;
        private string _Name;
        private string _Description;
        private int _MonthDimId;
        //1.Field: MonthDimId
        [Key(true)]
        public int MonthDimId
        {
            get { return _MonthDimId; }
            set { SetPropertyValue("MonthDimId", ref _MonthDimId, value); }
        }
        //2.Field Description
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        //3.Field: Name
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        //4.Field: RowStatus
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref _RowStatus, value); }
        }
        #endregion

        #region References
        //1.
        [Association("FinancialDoubleEntry_FactReferencesMonthDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations
        {
            get { return GetCollection<FinancialDoubleEntry_Fact>("Relations"); }
        }

        [Association("MonthDimReferencesETLJobLog")]
        public XPCollection<ETLJobLog> ETLJobLogs
        {
            get { return GetCollection<ETLJobLog>("ETLJobLogs"); }
        }

        [Association("FinancialRevenueByItem_FactReferencesMonthDim")]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }

        [Association(@"FinancialSupplierLiabilitySummaryFactReferencesMonthDim", typeof(FinancialSupplierLiabilitySummary_Fact)), Aggregated]
        public XPCollection<FinancialSupplierLiabilitySummary_Fact> FinancialSupplierLiabilitySummary_Facts
        {
            get { return GetCollection<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_Facts"); }
        }

        [Association(@"FinancialCustomerLiabilitySummary_FactReferencesMonthDim", typeof(FinancialCustomerLiabilitySummary_Fact))]
        public XPCollection<FinancialCustomerLiabilitySummary_Fact> FinancialCustomerLiabilitySummary_Facts { get { return GetCollection<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_Facts"); } }

        [Association(@"FinancialOnTheWayBuyingGoodSummaryReferencesMonthDim", typeof(FinancialOnTheWayBuyingGoodSummary))]
        public XPCollection<FinancialOnTheWayBuyingGoodSummary> FinancialOnTheWayBuyingGoodSummaries { get { return GetCollection<FinancialOnTheWayBuyingGoodSummary>("FinancialOnTheWayBuyingGoodSummaries"); } }


        [Association(@"DiaryJournal_FactReferencesMonthDim", typeof(DiaryJournal_Fact)), Aggregated]
        public XPCollection<DiaryJournal_Fact> DiaryJournal_Facts
        {
            get { return GetCollection<DiaryJournal_Fact>("DiaryJournal_Facts"); }
        }

        [Association(@"FinancialActualPriceSummary_FactReferencesMonthDim")]
        public XPCollection<FinancialActualPriceSummary_Fact> FinancialActualPriceSummary_Facts
        {
            get { return GetCollection<FinancialActualPriceSummary_Fact>("FinancialActualPriceSummary_Facts"); }
        }

        [Association(@"FinancialSalesOrManufactureExpenseSummary_FactReferencesMonthDim", typeof(FinancialSalesOrManufactureExpenseSummary_Fact))]
        public XPCollection<FinancialSalesOrManufactureExpenseSummary_Fact> FinancialSalesOrManufactureExpenseSummary_Facts { get { return GetCollection<FinancialSalesOrManufactureExpenseSummary_Fact>("FinancialSalesOrManufactureExpenseSummary_Facts"); } }

        [Association(@"FinancialGeneralLedgerByMonth-MonthDim", typeof(FinancialGeneralLedgerByMonth))]
        public XPCollection<FinancialGeneralLedgerByMonth> FinancialGeneralLedgerByMonths { get { return GetCollection<FinancialGeneralLedgerByMonth>("FinancialGeneralLedgerByMonths"); } }


        [Association(@"FinancialPrepaidExpenseSummary_FactReferencesMonthDim", typeof(FinancialPrepaidExpenseSummary_Fact))]
        public XPCollection<FinancialPrepaidExpenseSummary_Fact> FinancialPrepaidExpenseSummary_Facts
        { get { return GetCollection<FinancialPrepaidExpenseSummary_Fact>("FinancialPrepaidExpenseSummary_Facts"); } }

        [Association("GoodsInTransitForSaleSummary_FactReferencesMonthDim", typeof(GoodsInTransitForSaleSummary_Fact))]
        public XPCollection<GoodsInTransitForSaleSummary_Fact> GoodsInTransitForSaleSummary_Facts { get { return GetCollection<GoodsInTransitForSaleSummary_Fact>("GoodsInTransitForSaleSummary_Facts"); } }

        [Association("GoodsInInventorySummary_FactReferencesMonthDim", typeof(GoodsInInventorySummary_Fact))]
        public XPCollection<GoodsInInventorySummary_Fact> GoodsInInventorySummary_Facts { get { return GetCollection<GoodsInInventorySummary_Fact>("GoodsInInventorySummary_Facts"); } }

        [Association(@"FinancialItemInventorySummary_Fact-MonthDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }

        #endregion

        #region Validate
        private bool ValidateName()
        {
            if (int.Parse(this.Name) > 12 || int.Parse(this.Name) < 1)
            {
                return false;
            }
            return true;
        }
        private bool ValidateDescription()
        {
            if (this.Description != null && this.Description.Length > 250)
            {
                return false;
            }
            return true;
        }
        private bool ValidateMonthDimId()
        {
            if (this.MonthDimId.ToString().Length < 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateRowStatus()
        {
            if (this.RowStatus.ToString().Length < 0)
            {
                return false;
            }
            return true;
        }
        //ValidateParameter
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
            if (!ValidateMonthDimId())
            {
                return false;
            }
            if (!ValidateRowStatus())
            {
                return false;
            }

            return true;
        }
        private bool ValidateUniqueName()
        {
            Session session = null;
            try
            {
                if (!isExistXpoObject1<MonthDim>("Name", this._Name))
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
            finally { if (session != null) { session.Dispose(); } }
        }
        //Validate Unique        
        public bool ValidateUnique()
        {
            if (!ValidateUniqueName())
            {
                return false;
            }
            return true;
        }
        private bool IsExistName()
        {
            if (isExistXpoObject1<MonthDim>("Name", _Name))
            {
                return false;
            }
            return true;
        }
        private bool IsExistMonthDimId()
        {
            if (isExistXpoObject1<MonthDim>("MonthDimId", this._MonthDimId))
            {
                return false;
            }
            return true;
        }
        //Validate Is Exist
        public bool IsExist()
        {
            if (!IsExistName())
            {
                return false;
            }
            //if (!IsExistMonthDimId())
            //{
            //    return false;
            //}

            return true;
        }


        #endregion


        #region Method Onsaving

        //protected override void OnSaving()
        //{
        //    if (!ValidateParameter())
        //    {
        //        throw new Exception("Error Validate Parameter !!!");
        //    }
        //    if (!ValidateUnique())
        //    {
        //        throw new Exception("Error Validate Unique !!!");
        //    }
        //    if (!IsExist())
        //    {
        //        throw new Exception("Error Validate Is Exist !!!");
        //    }
        //    base.OnSaving();
        //}

        #endregion

        #region Method Check Exist Object
        public static bool isExistXpoObject1<T>(string fieldName, object value)
        {
            Session session = null;//define session 
            try
            {
                session = XpoHelper.GetNewSession();
                //CriteriaOperator: Provides the abstract class (must inherit in VB) base class for criteria operators
                CriteriaOperator fieldCriteria = new BinaryOperator(fieldName, value, BinaryOperatorType.Equal);
                var result = session.GetObjects(session.GetClassInfo(typeof(T)), fieldCriteria, null, 0, false, true);//GetInfo Object, assign to var result
                if (result != null && result.Count > 0)//if object is not exist return true 
                {
                    return true;
                }
                else
                {
                    return false;                      //if object is exist return false
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();//Dispose session
            }
        }
        #endregion

        #region Populate

        public static void Populate()
        {
            Session session = null;
            try
            {
                for (Int32 DefaultMonth = (Int32)MonthDimEnum.Month_01; DefaultMonth != (Int32)MonthDimEnum.Last; DefaultMonth++)
                {
                    if (!isExistXpoObject1<MonthDim>("Name", DefaultMonth))//if Data is not Exist, assign Data Default
                    {
                        session = XpoHelper.GetNewSession();//create new session
                        MonthDim DefaultValue = new MonthDim(session)
                        {
                            Name = DefaultMonth.ToString(),
                            Description = "DEFAULT",
                            RowStatus = 1,
                        };
                        DefaultValue.Save();//save
                    }
                }
            }
            catch (Exception) { throw; }

        }
        #endregion

        #region Method GetDefault
        public static string GetDefault(Session session, string para)
        {
            if (para != null)
            {
                //MonthDim a = session.FindObject<MonthDim>(new BinaryOperator("Name", para, BinaryOperatorType.Equal));               
                //    _Default = a.ToString();                 
                for (int DefaultMonth = (int)MonthDimEnum.Month_01; DefaultMonth != (int)MonthDimEnum.Last; DefaultMonth++)
                {
                    if (para.Equals(DefaultMonth))
                    {
                        _Default = para;
                        return _Default;
                    }
                }
            }
            return null;
        }

        //public static MonthDim GetDefault1(Session session, MonthDimEnum dEnum)
        //{

        //}

        #endregion
    }
}
