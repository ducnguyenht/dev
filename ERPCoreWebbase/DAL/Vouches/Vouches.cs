using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.Journal;
using System.Linq;
using System.Collections.Generic;
using NAS.DAL.Vouches.Allocation;
using System.ComponentModel;
namespace NAS.DAL.Vouches
{

    public partial class Vouches : XPCustomObject
    {
        #region Logic
        protected override void OnSaving()
        {
            base.OnSaving();
            if (this.IssuedDate.Year > 1753)
            {
                this.IssuedDate = this.IssuedDate.AddHours(DateTime.Now.Hour - this.IssuedDate.Hour);
                this.IssuedDate = this.IssuedDate.AddMinutes(DateTime.Now.Minute - this.IssuedDate.Minute);
                this.IssuedDate = this.IssuedDate.AddSeconds(DateTime.Now.Second - this.IssuedDate.Second);
                this.IssuedDate = this.IssuedDate.AddMilliseconds(DateTime.Now.Millisecond - this.IssuedDate.Millisecond);
            }
        }
        #endregion

        public Vouches(Session session) : base(session) {
            
        }

        public override void AfterConstruction() { base.AfterConstruction(); }
        private string _Address;
        private string _Description;
        Guid fVouchesId;
        [Key(true)]
        public Guid VouchesId
        {
            get { return fVouchesId; }
            set { SetPropertyValue<Guid>("VouchesId", ref fVouchesId, value); }
        }
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        [Size(255)]
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

        [Size(128)]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                SetPropertyValue("Address", ref _Address, value);
            }
        }

        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }
        DateTime fIssuedDate;
        public DateTime IssuedDate
        {
            get { return fIssuedDate; }
            set { SetPropertyValue<DateTime>("IssuedDate", ref fIssuedDate, value); }
        }
        DateTime fLastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set { SetPropertyValue<DateTime>("LastUpdateDate", ref fLastUpdateDate, value); }
        }
        double fSumOfCredit;
        public double SumOfCredit
        {
            get { return fSumOfCredit; }
            set { SetPropertyValue<double>("SumOfCredit", ref fSumOfCredit, value); }
        }
        double fSumOfDebit;
        public double SumOfDebit
        {
            get { return fSumOfDebit; }
            set { SetPropertyValue<double>("SumOfDebit", ref fSumOfDebit, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        
        Organization fSourceOrganizationId;
        [Association(@"VouchesSourceOrgainzationReferencesOrganization")]
        public Organization SourceOrganizationId
        {
            get { return fSourceOrganizationId; }
            set { SetPropertyValue<Organization>("SourceOrganizationId", ref fSourceOrganizationId, value); }
        }
        Organization fTargetOrganizationId;
        [Association(@"VouchesTargetOrgainzationReferencesOrganization")]
        public Organization TargetOrganizationId
        {
            get { return fTargetOrganizationId; }
            set { SetPropertyValue<Organization>("TargetOrganizationId", ref fTargetOrganizationId, value); }
        }
        VouchesType fVouchesTypeId;
        [Association(@"VouchesReferencesVouchesType")]
        public VouchesType VouchesTypeId
        {
            get { return fVouchesTypeId; }
            set { SetPropertyValue<VouchesType>("VouchesTypeId", ref fVouchesTypeId, value); }
        }
        [Association(@"VouchesActorReferencesVouches", typeof(VouchesActor)), Aggregated]
        public XPCollection<VouchesActor> VouchesActors { get { return GetCollection<VouchesActor>("VouchesActors"); } }
        [Association(@"VouchesAmountReferencesVouches", typeof(VouchesAmount)), Aggregated]
        public XPCollection<VouchesAmount> VouchesAmounts { get { return GetCollection<VouchesAmount>("VouchesAmounts"); } }

        //2013-12-12 ERP-951 Khoa.Truong DEL START
        //[Association("VoucherAllocationReferencesVouches", typeof(VoucherAllocation)), Aggregated]
        //public XPCollection<VoucherAllocation> VoucherAllocations { get { return GetCollection<VoucherAllocation>("VoucherAllocations"); } }
        //2013-12-12 ERP-951 Khoa.Truong DEL END

        [Association("VoucherObjectReferenceVouches"),Aggregated]
        public XPCollection<VoucherObject> VoucherObjects
        {
            get
            {
                return GetCollection<VoucherObject>("VoucherObjects");
            }
        }

        [Association("VoucherCustomTypeReferenceVouches"), Aggregated]
        public XPCollection<VoucherCustomType> VoucherCustomTypes
        {
            get
            {
                return GetCollection<VoucherCustomType>("VoucherCustomTypes");
            }
        }

        [NonPersistent]
        public string EntryBookingStatus
        {
            get;
            set;
        }
    
    }

}
