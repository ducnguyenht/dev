using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.BI.Accounting.Finance.DiaryJournal
{
    public class FinancialCreditAccountDim : XPCustomObject
    {
        public FinancialCreditAccountDim(Session session)
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
        private string _Description;
        private int _FinancialCreditAccountDimId;

        //1.Field FinancialCreditAccountDimId
        [Key(true)]
        public int FinancialCreditAccountDimId
        {
            get
            {
                return _FinancialCreditAccountDimId;
            }
            set
            {
                SetPropertyValue("FinancialCreditAccountDimId", ref _FinancialCreditAccountDimId, value);
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

        //4.Field Rowstatus
        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue("RowStatus", ref _RowStatus, value);
            }
        }

        #endregion

        #region References

        //References
        [Association("FinancialDoubleEntry_FactsReferencesFinancialCreditAccountDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations
        {
            get
            {
                return GetCollection<FinancialDoubleEntry_Fact>("Relations");
            }
        }

        #endregion

        #region Method Check Object IsExist

        public static bool isExistXpoObject<T>(string fieldName, object value)
        {
            Session session = XpoHelper.GetNewSession();
            try
            {
                CriteriaOperator criteria = new BinaryOperator(fieldName, value, BinaryOperatorType.Equal);
                var result = session.GetObjects(session.GetClassInfo(typeof(T)), criteria, null, 0, false, true);
                if (result != null && result.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
            finally { if (session != null) { session.Dispose(); } }
        }
        #endregion

        #region Method Populate

        private static string _default = "DEFAULT";
        public static void Populate()
        {
            Session session = null;
            try
            {
                if (!isExistXpoObject<FinancialCreditAccountDim>("Name", _default))//check isExist
                {
                    //create new session
                    session = XpoHelper.GetNewSession();
                    //create new Value
                    FinancialCreditAccountDim newValue = new FinancialCreditAccountDim(session)
                    {
                        Name = _default,
                        Description = "NON",
                        RowStatus = 1
                    };
                    newValue.Save();//save
                }
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
