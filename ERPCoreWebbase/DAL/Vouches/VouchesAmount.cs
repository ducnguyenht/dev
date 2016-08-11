using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.Currency;
namespace NAS.DAL.Vouches
{

    public partial class VouchesAmount : XPCustomObject
    {
        public VouchesAmount(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        Guid fVouchesAmountId;
        [Key(true)]
        public Guid VouchesAmountId
        {
            get { return fVouchesAmountId; }
            set { SetPropertyValue<Guid>("VouchesAmountId", ref fVouchesAmountId, value); }
        }
        double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }
        double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }
        double fExchangeRate;
        public double ExchangeRate
        {
            get { return fExchangeRate; }
            set { SetPropertyValue<double>("ExchangeRate", ref fExchangeRate, value); }
        }

        /*2014-01-17 ERP-1411 Khoa.Truong DEL START*/
        //ForeignCurrency fForeignCurrencyId;
        //[Association(@"VouchesAmountReferencesForeignCurrency")]
        //public ForeignCurrency ForeignCurrencyId
        //{
        //    get { return fForeignCurrencyId; }
        //    set { SetPropertyValue<ForeignCurrency>("ForeignCurrencyId", ref fForeignCurrencyId, value); }
        //}
        /*2014-01-17 ERP-1411 Khoa.Truong DEL END*/

        /*2014-01-17 ERP-1411 Khoa.Truong INS START*/
        Currency fCurrencyId;
        [Association(@"VouchesAmountReferencesCurrency")]
        public Currency CurrencyId
        {
            get { return fCurrencyId; }
            set { SetPropertyValue<Currency>("CurrencyId", ref fCurrencyId, value); }
        }
        /*2014-01-17 ERP-1411 Khoa.Truong INS END*/

        Vouches fVouchesId;
        [Association(@"VouchesAmountReferencesVouches")]
        public Vouches VouchesId
        {
            get { return fVouchesId; }
            set { SetPropertyValue<Vouches>("VouchesId", ref fVouchesId, value); }
        }

    }

}
