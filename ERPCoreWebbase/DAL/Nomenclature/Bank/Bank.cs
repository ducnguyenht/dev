using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Vouches;

namespace NAS.DAL.Nomenclature.Bank
{
    public partial class Bank : XPCustomObject
    {
        public Bank(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties

        private Guid fBankId;
        [Key(true)]
        public Guid BankId
        {
            get { return fBankId; }
            set { SetPropertyValue<Guid>("BankId", ref fBankId, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private string fSwiffCode;
        public string SwiffCode
        {
            get { return fSwiffCode; }
            set { SetPropertyValue<string>("SwiffCode", ref fSwiffCode, value); }
        }
        #endregion

        #region References
        [Association(@"BankBranchReferencesBank", typeof(BankBranch)), Aggregated]
        public XPCollection<BankBranch> BankBranchs { get { return GetCollection<BankBranch>("BankBranchs"); } }

        [Association(@"NAS.DAL.Vouches.ExchangeRateReferencesBank", typeof(ExchangeRate))]
        public XPCollection<ExchangeRate> ExchangeRates { get { return GetCollection<ExchangeRate>("ExchangeRates"); } }
        #endregion

    }
}
