using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.BI.Accounting.Finance.DiaryJournal
{
    public class FinancialDoubleEntry_Fact : XPCustomObject
    {
        public FinancialDoubleEntry_Fact(Session session)
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
        private string _Owner;
        private string _Currency;
        private FinancialAssetDim _FinancialAssetDimId;
        private OwnerOrgDim _OwnerOrgDimId;
        private string _CheckingNumber;
        private string _DescriptionInfo;
        private Guid _OriginalArtifact;
        private double _Credit;
        private double _Debit;
        private DateTime _IssueDate;
        private string _AccountNo;
        private FinancialTransactionDim _FinancialTransactionDimId;
        private FinancialCreditAccountDim _FinancialCreditAccountDimId;
        private YearDim _YearDimId;
        private MonthDim _MonthDimId;
        private FinancialDebitAccountDim _FinancialDebitAccountDimId;
        private DayDim _DayDimId;
        private double _Amount;
        private int _FinancialDoubleEntry_FactId;


        //1.field FinancialDoubleEntry_FactId
        [Key(true)]
        public int FinancialDoubleEntry_FactId
        {
            get { return _FinancialDoubleEntry_FactId; }
            set { SetPropertyValue("FinancialDoubleEntry_FactId", ref _FinancialDoubleEntry_FactId, value); }
        }

        //2.field Amount
        public double Amount
        {
            get { return _Amount; }
            set { SetPropertyValue("Amount", ref _Amount, value); }
        }

        #endregion

        #region References
        //1.References: DayDim
        [Association("FinancialDoubleEntry_FactReferencesDayDim")]
        public DayDim DayDimId
        {
            get { return _DayDimId; }
            set { SetPropertyValue("DayDimId", ref _DayDimId, value); }
        }

        //2.References: DebitAccountDim
        [Association("FinancialDoubleEntry_FactReferencesFinancialDebitAccountDim")]
        public FinancialDebitAccountDim FinancialDebitAccountDimId
        {
            get { return _FinancialDebitAccountDimId; }
            set { SetPropertyValue("FinancialDebitAccountDimId", ref _FinancialDebitAccountDimId, value); }
        }

        //3.References: MonthDim
        [Association("FinancialDoubleEntry_FactReferencesMonthDim")]
        public MonthDim MonthDimId
        {
            get { return _MonthDimId; }
            set { SetPropertyValue("MonthDimId", ref _MonthDimId, value); }
        }

        //4.References: YearDim
        [Association("FinancialDoubleEntry_FactReferencesYearDim")]
        public YearDim YearDimId
        {
            get { return _YearDimId; }
            set { SetPropertyValue("YearDimId", ref _YearDimId, value); }
        }

        //5.References: FinancialCreditAccountDim
        [Association("FinancialDoubleEntry_FactsReferencesFinancialCreditAccountDim")]
        public FinancialCreditAccountDim FinancialCreditAccountDimId
        {
            get { return _FinancialCreditAccountDimId; }
            set { SetPropertyValue("FinancialCreditAccountDimId", ref _FinancialCreditAccountDimId, value); }
        }

        //6.References: FinancialTransactionDim
        [Association("FinancialDoubleEntry_FactsReferencesFinancialTransactionDim")]
        public FinancialTransactionDim FinancialTransactionDimId
        {
            get { return _FinancialTransactionDimId; }
            set { SetPropertyValue("FinancialTransactionDimId", ref _FinancialTransactionDimId, value); }
        }

        //7.References: OwnerOrgDim
        [Association("FinancialDoubleEntry_FactReferencesOwnerOrgDim")]
        public OwnerOrgDim OwnerOrgDimId
        {
            get { return _OwnerOrgDimId; }
            set { SetPropertyValue("OwnerOrgDimId", ref _OwnerOrgDimId, value); }
        }

        //8.References: FinancialAssetDim
        [Association("FinancialDoubleEntry_FactReferencesFinancialAssetDim")]
        public FinancialAssetDim FinancialAssetDimId
        {
            get { return _FinancialAssetDimId; }
            set { SetPropertyValue("FinancialAssetDimId", ref _FinancialAssetDimId, value); }
        }

        #endregion

        #region NonPersistent

        [NonPersistent]
        public string AccountNo
        {
            get
            {
                if (this.FinancialDebitAccountDimId != null)
                {
                    return this.FinancialDebitAccountDimId.Name;
                }
                else if (this.FinancialCreditAccountDimId != null)
                {
                    return this.FinancialCreditAccountDimId.Name;
                }
                return String.Empty;
            }
            set
            {
                _AccountNo = value;
            }
        }

        [NonPersistent]
        public DateTime IssueDate
        {
            get
            {
                if (this.DayDimId != null && this.MonthDimId != null && this.YearDimId != null)
                {
                    try
                    {
                        return new DateTime(int.Parse(this.YearDimId.Name),
                            int.Parse(this.MonthDimId.Name),
                            int.Parse(this.DayDimId.Name));
                    }
                    catch (Exception)
                    {
                        return DateTime.MinValue;
                    }

                }
                return DateTime.MinValue;
            }
            set
            {
                SetPropertyValue("IssueDate", ref _IssueDate, value);
            }
        }

        [NonPersistent]
        public double Debit
        {
            get
            {
                if (this.FinancialDebitAccountDimId != null)
                    return Amount;
                return 0;
            }
            set
            {
                SetPropertyValue("Debit", ref _Debit, value);
            }
        }

        [NonPersistent]
        public double Credit
        {
            get
            {
                if (this.FinancialCreditAccountDimId != null)
                    return Amount;
                return 0;
            }
            set
            {
                SetPropertyValue("Credit", ref _Credit, value);
            }
        }


        [NonPersistent]

        public Guid OriginalArtifact
        {
            get
            {
                if (this.FinancialTransactionDimId != null)
                    return this.FinancialTransactionDimId.RefId;
                return Guid.Empty;
            }
            set
            {
                SetPropertyValue("OriginalArtifact", ref _OriginalArtifact, value);
            }
        }

        [NonPersistent]

        public string CheckingNumber
        {
            get
            {
                if (this.FinancialTransactionDimId != null)
                    return this.FinancialTransactionDimId.Name;
                return String.Empty;
            }
            set
            {
                SetPropertyValue("CheckingNumber", ref _CheckingNumber, value);
            }
        }

        [NonPersistent]

        public string DescriptionInfo
        {
            get
            {
                if (this.FinancialTransactionDimId != null)
                    return this.FinancialTransactionDimId.Description;
                return String.Empty;
            }
            set
            {
                SetPropertyValue("DescriptionInfo", ref _DescriptionInfo, value);
            }
        }

        [NonPersistent]

        public string Currency
        {
            get
            {
                if (this.FinancialAssetDimId != null)
                    return this.FinancialAssetDimId.Name;
                return String.Empty;
            }
            set
            {
                SetPropertyValue("Currency", ref _Currency, value);
            }
        }

        [NonPersistent]

        public string Owner
        {
            get
            {
                if (this.OwnerOrgDimId != null)
                    return this.OwnerOrgDimId.Name;
                return String.Empty;
            }
            set
            {
                SetPropertyValue("Owner", ref _Owner, value);
            }
        }


        #endregion
    }
}
