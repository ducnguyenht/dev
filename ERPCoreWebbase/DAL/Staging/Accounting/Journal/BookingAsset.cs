using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Ledger;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class BookingAsset : XPCustomObject, IDALValidate
    {
        public BookingAsset(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fBookingAssetId;
        [Key(true)]
        public Guid BookingAssetId
        {
            get { return fBookingAssetId; }
            set { SetPropertyValue<Guid>("BookingAssetId", ref fBookingAssetId, value); }
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

        private Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables

        private BookingAssetType fBookingAssetTypeId;
        [Association("BookingAssetReferencesBookingAssetType")]
        public BookingAssetType BookingAssetTypeId
        {
            get { return fBookingAssetTypeId; }
            set { SetPropertyValue<BookingAssetType>("BookingAssetTypeId", ref fBookingAssetTypeId, value); }
        }

        [Association(@"DoubleEntryJournalReferencesBookingAsset", typeof(DoubleEntryJournal))]
        public XPCollection<DoubleEntryJournal> DoubleEntryJournals { get { return GetCollection<DoubleEntryJournal>("DoubleEntryJournals"); } }

        [Association(@"DoubleEntryLedgerReferencesBookingAsset", typeof(DoubleEntryLedger))]
        public XPCollection<DoubleEntryLedger> DoubleEntryLedgers { get { return GetCollection<DoubleEntryLedger>("DoubleEntryLedgers"); } }

        //END link between 2 tables

        //validate database

        public bool ValidateParameter()
        {
            if (this.Name.Equals(string.Empty))
                return false;
            return true;
        }

        public bool ValidateUnique()
        {
            return true;
        }

        public bool IsExist()
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override void OnSaving()
        {
            if (ValidateParameter())
            {
                if (ValidateUnique())
                    base.OnSaving();
            }
        }

        //END validate database
    }
}
