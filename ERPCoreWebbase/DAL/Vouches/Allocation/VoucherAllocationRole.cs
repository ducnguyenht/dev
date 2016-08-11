using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Vouches.Allocation
{
    public partial class VoucherAllocationRole : XPCustomObject
    {
        public VoucherAllocationRole(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fVoucherAllocationRoleId;
        [Key(true)]
        public Guid VoucherAllocationRoleId
        {
            get { return fVoucherAllocationRoleId; }
            set { SetPropertyValue<Guid>("VoucherAllocationRoleId", ref fVoucherAllocationRoleId, value); }
        }

        private bool fIsMaster;
        public bool IsMaster
        {
            get { return fIsMaster; }
            set { SetPropertyValue<bool>("IsMaster", ref fIsMaster, value); }
        }

        private VoucherAllocationSubject fVoucherAllocationSubjectId;
        [Association("VoucherAllocationRoleReferencesVoucherAllocationSubject")]
        public VoucherAllocationSubject VoucherAllocationSubjectId
        {
            get { return fVoucherAllocationSubjectId; }
            set { SetPropertyValue<VoucherAllocationSubject>("VoucherAllocationSubjectId", ref fVoucherAllocationSubjectId, value); }
        }

        private VoucherAllocation fVoucherAllocationId;
        [Association("VoucherAllocationRoleReferencesVoucherAllocation")]
        public VoucherAllocation VoucherAllocationId
        {
            get { return fVoucherAllocationId; }
            set { SetPropertyValue<VoucherAllocation>("VoucherAllocationId", ref fVoucherAllocationId, value); }
        }
    }
}
