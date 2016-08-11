using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.BI.Accounting.Finance.DiaryJournal
{
    public class FinancialDebitAccountDim : XPCustomObject
    {
        public FinancialDebitAccountDim(Session session)
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
        private string _Descriptiom;
        private int _FinancialDebitAccountDimId;

        //1.Field FinancialDebitAccountDimId
        [Key(true)]
        public int FinancialDebitAccountDimId
        {
            get
            {
                return _FinancialDebitAccountDimId;
            }
            set
            {
                SetPropertyValue("FinancialDebitAccountDimId", ref _FinancialDebitAccountDimId, value);
            }
        }

        //2. Field: Descriptiom
        public string Descriptiom
        {
            get
            {
                return _Descriptiom;
            }
            set
            {
                SetPropertyValue("Descriptiom", ref _Descriptiom, value);
            }
        }

        //3. Field: Name
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
                SetPropertyValue("RowStatus", ref _RowStatus, value);
            }
        }

        #endregion

        #region References

        //References
        [Association("FinancialDoubleEntry_FactReferencesFinancialDebitAccountDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations
        {
            get
            {
                return GetCollection<FinancialDoubleEntry_Fact>("Relations");
            }
        }

        #endregion

        #region Method Populate()
        static string _default = "DEFAULT";
        public static void Populate()
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                //check isExist (using findObject<T>(new BinaryOperator))
                FinancialDebitAccountDim defaultDebitName = uow.FindObject<FinancialDebitAccountDim>(new BinaryOperator("Name", _default));
                if (defaultDebitName == null)
                {
                    defaultDebitName = new FinancialDebitAccountDim(uow) // Create new value
                    {
                        Name = _default,
                        Descriptiom = "DEFAULT DESCRIPTION",
                        RowStatus = 1
                    };
                    uow.CommitChanges();//save
                }
            }
        }

        #endregion
    }
}
