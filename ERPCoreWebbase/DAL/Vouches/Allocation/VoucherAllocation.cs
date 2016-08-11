using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Vouches.Allocation
{
    public partial class VoucherAllocation : XPCustomObject
    {
        public VoucherAllocation(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fVoucherAllocationId;
        [Key(true)]
        public Guid VoucherAllocationId
        {
            get { return fVoucherAllocationId; }
            set { SetPropertyValue<Guid>("VoucherAllocationId", ref fVoucherAllocationId, value); }
        }

        private double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
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

        //References
        [Association(@"VoucherAllocationRoleReferencesVoucherAllocation", typeof(VoucherAllocationRole)), Aggregated]
        public XPCollection<VoucherAllocationRole> VoucherAllocationRoles { get { return GetCollection<VoucherAllocationRole>("VoucherAllocationRoles"); } }

        [Association(@"VoucherAllocationBookingAccountReferencesVoucherAllocation", typeof(VoucherAllocationBookingAccount)), Aggregated]
        public XPCollection<VoucherAllocationBookingAccount> VoucherAllocationBookingAccounts { get { return GetCollection<VoucherAllocationBookingAccount>("VoucherAllocationBookingAccounts"); } }

        private NAS.DAL.Accounting.Configure.Allocation fAllocationId;
        [Association("VoucherAllocationReferencesNAS.DAL.Accounting.Configure.Allocation")]
        public NAS.DAL.Accounting.Configure.Allocation AllocationId
        {
            get { return fAllocationId; }
            set { SetPropertyValue<NAS.DAL.Accounting.Configure.Allocation>("AllocationId", ref fAllocationId, value); }
        }

        //2013-12-12 ERP-951 Khoa.Truong DEL START
        //private NAS.DAL.Vouches.Vouches fVouchesId;
        //[Association("VoucherAllocationReferencesVouches")]
        //public NAS.DAL.Vouches.Vouches VouchesId
        //{
        //    get { return fVouchesId; }
        //    set { SetPropertyValue<NAS.DAL.Vouches.Vouches>("VouchesId", ref fVouchesId, value); }
        //}
        //2013-12-12 ERP-951 Khoa.Truong DEL END


        //END References
        [NonPersistent]
        public string MasterAccountActor { get; set; }
    }
}
