using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class BookingAssetType : XPCustomObject, IDALValidate
    {
        public BookingAssetType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fBookingAssetTypeId;
        [Key(true)]
        public Guid BookingAssetTypeId
        {
            get { return fBookingAssetTypeId; }
            set { SetPropertyValue<Guid>("BookingAssetTypeId", ref fBookingAssetTypeId, value); }
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

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }

        //link between 2 tables

        [Association(@"BookingAssetReferencesBookingAssetType", typeof(BookingAsset))]
        public XPCollection<BookingAsset> BookingAssets { get { return GetCollection<BookingAsset>("BookingAssets"); } }

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
