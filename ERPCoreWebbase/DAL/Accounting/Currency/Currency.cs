using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Vouches;

namespace NAS.DAL.Accounting.Currency
{
    public partial class Currency : XPCustomObject
    {

        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into CustomFieldType table
                if (!Util.isExistXpoObject<Currency>("Code", "NAAN_DEFAULT"))
                {
                    Currency objectTypeBO = new Currency(session)
                    {
                        Code = "NAAN_DEFAULT",
                        Name = "NAAN_DEFAULT",
                        Description = "NAAN_DEFAULT",
                        RowStatus = -1
                    };
                    objectTypeBO.Save();
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

        public Currency(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fCurrencyId;
        [Key(true)]
        public Guid CurrencyId
        {
            get { return fCurrencyId; }
            set { SetPropertyValue<Guid>("CurrencyId", ref fCurrencyId, value); }
        }

        private Currency fParentCurrencyId;
        [Association(@"CurrencyParentCurrency")]
        public Currency ParentCurrencyId
        {
            get { return fParentCurrencyId; }
            set { SetPropertyValue<Currency>("ParentCurrencyId", ref fParentCurrencyId, value); }
        }

        private string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private double fCoefficient;
        public double Coefficient
        {
            get { return fCoefficient; }
            set { SetPropertyValue<double>("Coefficient", ref fCoefficient, value); }
        }

        private double fNumRequired;
        public double NumRequired
        {
            get { return fNumRequired; }
            set { SetPropertyValue<double>("NumRequired", ref fNumRequired, value); }
        }

        private bool fIsDefault;
        public bool IsDefault
        {
            get { return fIsDefault; }
            set { SetPropertyValue<bool>("IsDefault", ref fIsDefault, value); }
        }

       
        #endregion

        #region References
        private CurrencyType fCurrencyTypeId;
        [Association("CurrencyReferencesCurrencyType")]
        public CurrencyType CurrencyTypeId
        {
            get { return fCurrencyTypeId; }
            set { SetPropertyValue<CurrencyType>("CurrencyTypeId", ref fCurrencyTypeId, value); }
        }

        [Association(@"CurrencyParentCurrency")]
        public XPCollection<Currency> Currencies { get { return GetCollection<Currency>("Currencies"); } }

        [Association(@"GeneralJournalReferencesCurrency")]
        public XPCollection<GeneralJournal> GeneralJournals { get { return GetCollection<GeneralJournal>("GeneralJournals"); } }
                
        [Association(@"COGSReferencesCurrency")]
        public XPCollection<COGS> COGSs { get { return GetCollection<COGS>("COGSs"); } }

        [Association(@"GeneralLedgerReferencesCurrency")]
        public XPCollection<GeneralLedger> GeneralLedgers { get { return GetCollection<GeneralLedger>("GeneralLedgers"); } }

        [Association(@"NAS.DAL.Vouches.ExchangeRateReferencesCurrency", typeof(ExchangeRate))]
        public XPCollection<ExchangeRate> ExchangeRates { get { return GetCollection<ExchangeRate>("ExchangeRates"); } }

        [Association(@"NAS.DAL.Vouches.ExchangeRateReferencesNumeratorCurrency", typeof(ExchangeRate))]
        public XPCollection<ExchangeRate> exchangeRates { get { return GetCollection<ExchangeRate>("exchangeRates"); } }

        /*2014-01-17 ERP-1411 Khoa.Truong INS START*/
        [Association(@"VouchesAmountReferencesCurrency")]
        public XPCollection<VouchesAmount> VouchesAmounts { get { return GetCollection<VouchesAmount>("VouchesAmounts"); } }
        /*2014-01-17 ERP-1411 Khoa.Truong INS END*/
        #endregion


    }
}
