using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Bank;
using NAS.DAL.Accounting.Currency;
namespace NAS.DAL.Vouches
{
    public partial class ExchangeRate : XPCustomObject
    {
        public ExchangeRate(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        Guid fExchangeRateId;
        [Key(true)]
        public Guid ExchangeRateId
        {
            get { return fExchangeRateId; }
            set { SetPropertyValue<Guid>("ExchangeRateId", ref fExchangeRateId, value); }
        }

        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        double fRate;
        public double Rate
        {
            get { return fRate; }
            set { SetPropertyValue<double>("Rate", ref fRate, value); }
        }

        DateTime fRowCreationTimeStamp;
        public DateTime RowCreationTimeStamp
        {
            get { return fRowCreationTimeStamp; }
            set { SetPropertyValue<DateTime>("RowCreationTimeStamp", ref fRowCreationTimeStamp, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        short fStatus;
        public short Status
        {
            get { return fStatus; }
            set { SetPropertyValue<short>("Status", ref fStatus, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private DateTime fAffectedDate;
        public DateTime AffectedDate
        {
            get { return fAffectedDate; }
            set { SetPropertyValue<DateTime>("AffectedDate", ref fAffectedDate, value); }
        }


       
        #endregion

        #region References
        private ForeignCurrency _ForeignCurrencyId;
        [Association("ExchangeRagteReferencesForeignCurrency")]
        public ForeignCurrency ForeignCurrencyId
        {
            get { return _ForeignCurrencyId; }
            set { SetPropertyValue("ForeignCurrencyId", ref _ForeignCurrencyId, value); }
        }

        private NAS.DAL.Nomenclature.Bank.Bank fBankId;
        [Association("NAS.DAL.Vouches.ExchangeRateReferencesBank")]
        public NAS.DAL.Nomenclature.Bank.Bank BankId
        {
            get { return fBankId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Bank.Bank>("BankId", ref fBankId, value); }
        }

        private NAS.DAL.Accounting.Currency.Currency fDenomiratorCurrencyId;
        [Association("NAS.DAL.Vouches.ExchangeRateReferencesCurrency")]
        public NAS.DAL.Accounting.Currency.Currency DenomiratorCurrencyId
        {
            get { return fDenomiratorCurrencyId; }
            set { SetPropertyValue("DenomiratorCurrencyId", ref fDenomiratorCurrencyId, value); }
        }

        private Currency fNumeratorCurrencyId;
        [Association(@"NAS.DAL.Vouches.ExchangeRateReferencesNumeratorCurrency")]
        public Currency NumeratorCurrencyId
        {
            get { return fNumeratorCurrencyId; }
            set { SetPropertyValue<Currency>("NumeratorCurrencyId", ref fNumeratorCurrencyId, value); }
        }
        #endregion

        #region Logic

        #endregion
    }

}
