using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Currency
{
    public partial class CurrencyType : XPCustomObject
    {
        public CurrencyType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fCurrencyTypeId;
        [Key(true)]
        public Guid CurrencyTypeId
        {
            get { return fCurrencyTypeId; }
            set { SetPropertyValue<Guid>("CurrencyType", ref fCurrencyTypeId, value); }
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
            set { SetPropertyValue<string>("Descripiton", ref fDescription, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private bool fIsMaster;
        public bool IsMaster
        {
            get { return fIsMaster; }
            set { SetPropertyValue<bool>("IsMaster", ref fIsMaster, value); }
        }
        #endregion

        #region References
        [Association(@"CurrencyReferencesCurrencyType", typeof(Currency))]
        public XPCollection<Currency> Currencies { get { return GetCollection<Currency>("Currencies"); } }
        #endregion
    }
}


