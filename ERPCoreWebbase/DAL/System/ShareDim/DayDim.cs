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

namespace NAS.DAL.System.ShareDim
{
    #region ENUM
    public enum DayDimEnum
    {
        Day_01 = 1,
        Day_02 = 2,
        Day_03 = 3,
        Day_04 = 4,
        Day_05 = 5,
        Day_06 = 6,
        Day_07 = 7,
        Day_08 = 8,
        Day_09 = 9,
        Day_10 = 10,
        Day_11 = 11,
        Day_12 = 12,
        Day_13 = 13,
        Day_14 = 14,
        Day_15 = 15,
        Day_16 = 16,
        Day_17 = 17,
        Day_18 = 18,
        Day_19 = 19,
        Day_20 = 20,
        Day_21 = 21,
        Day_22 = 22,
        Day_23 = 23,
        Day_24 = 24,
        Day_25 = 25,
        Day_26 = 26,
        Day_27 = 27,
        Day_28 = 28,
        Day_29 = 29,
        Day_30 = 30,
        Day_31 = 31,
        last
    }
    #endregion
    public class DayDim : XPCustomObject, IDALValidate
    {
        public DayDim(Session session)
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


        #region Properties
        // Fields...
        private short _RowStatus;
        private string _Name;
        string _Code;
        private string _Description;
        private int _DayDimId;

        //1. Field DateId
        [Key(true)]
        public int DayDimId
        {
            get
            {
                return _DayDimId;
            }
            set
            {
                SetPropertyValue("DayDimId", ref _DayDimId, value);
            }
        }


        //2. Field Description
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue("Description", ref _Description, value);
            }
        }


        //3.Field Name
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        //4.Field RowStatus
        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue<short>("RowStatus", ref _RowStatus, value);
            }
        }
        #endregion

        #region References
        [Association("FinancialDoubleEntry_FactReferencesDayDim")]
        public XPCollection<FinancialDoubleEntry_Fact> FinancialDoubleEntrytions
        {
            get { return GetCollection<FinancialDoubleEntry_Fact>("FinancialDoubleEntrytions"); }
        }

        [Association("DayDimReferencesETLJobLog")]
        public XPCollection<ETLJobLog> ETLJobLogs
        {
            get { return GetCollection<ETLJobLog>("ETLJobLogs"); }
        }
        [Association("FinancialRevenueByItem_FactReferencesDayDim")]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }
        #endregion

        #region Validate
        #region ValidateParameter
        private bool ValidateName()
        {
            if (Int32.Parse(this.Name) < 1 || Int32.Parse(this.Name) > 31)
            {
                return false;
            }
            return true;
        }
        private bool ValidateDescription()
        {
            if (this.Description != null && this.Description.Length > 255)
            {
                return false;
            }
            return true;
        }
        private bool ValidateDayDimID()
        {
            if (this.DayDimId.ToString().Length <= 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateRowstus()
        {
            if (this.RowStatus.ToString().Length < 0 || this.RowStatus.ToString().Length > 1)
            {
                return false;
            }
            return true;
        }
        //Validate Parameters
        public bool ValidateParameter()
        {
            try
            {
                if (!ValidateName())
                {
                    return false;
                }
                if (!ValidateDescription())
                {
                    return false;
                }
                if (!ValidateDayDimID())
                {
                    return false;
                }
                if (!ValidateRowstus())
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Validate Unique
        private bool UniqueDayDimId()
        {
            return true;
        }
        //Validate Unique
        public bool ValidateUnique()
        {
            if (!UniqueDayDimId())
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Check Is Exist


        //private bool IsExistName()
        //{
        //    string a = _DayDimId.ToString();
        //    Session session = XpoHelper.GetNewSession();
        //    DayDim DayDim = session.FindObject<DayDim>(new BinaryOperator("Name", _Name.ToString(), BinaryOperatorType.Equal));
        //    if (DayDim != null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        private bool IsExistName()
        {
            if (isExistXpoObject1<DayDim>("Name", _Name))
            {
                return false;
            }
            return true;
        }
        private bool IsExistDayDimID()
        {
            if (isExistXpoObject1<DayDim>("DayDimId", _DayDimId))
            {
                return true;
            }
            return true;
        }
        //Validate IsExist
        public bool IsExist()
        {
            if (!IsExistName())
            {
                return false;
            }
            //if (!IsExistDayDimID())
            //{
            //    return false;
            //}
            return true;
        }
        #endregion

        #region Method OnSaving()
        //protected override void OnSaving()
        //{
        //    //if (!ValidateParameter())
        //    //{
        //    //    throw new Exception("Error Validate Parameter !!!");
        //    //}
        //    //if (!ValidateUnique())
        //    //{
        //    //    throw new Exception("Error Validate Unique !!!");
        //    //}
        //    ////if (!IsExist())
        //    ////{
        //    ////    throw new Exception("Error Validate Is Exist !!!");
        //    ////}
        //    //base.OnSaving();
        //}
        #endregion
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

        #region Method Populate
        //Method Populate() Using Session
        public static void Populate()
        {
            //Session session = null;
            //try
            //{
            //    for (Int32 dEnum = (Int32)DayDimEnum.Day_01; dEnum != (Int32)DayDimEnum.last; dEnum++)
            //    {
            //        if (!isExistXpoObject1<DayDim>("Name", dEnum))// (Name: FieldName[Column in database], dEnum Object[value])
            //        {
            //            session = XpoHelper.GetNewSession();
            //            DayDim DefaultValue = new DayDim(session)
            //            {
            //                Name = dEnum.ToString(),              //assign data in database
            //                Description = "DEFAUL",
            //                RowStatus = 1
            //            };
            //            DefaultValue.Save();                       //Save
            //        }
            //    }
            //}
            //catch (Exception) { throw; }
        }
        #endregion

        #region Method GetDefault
        //public static DayDim GetDefault(Session session, DayDimEnum day)
        //{

        //}
        #endregion
    }
}
