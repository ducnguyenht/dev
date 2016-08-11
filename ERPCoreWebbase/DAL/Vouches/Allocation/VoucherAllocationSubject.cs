using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Vouches.Allocation
{
    public partial class VoucherAllocationSubject : XPCustomObject
    {
        public VoucherAllocationSubject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fVoucherAllocationSubjectId;
        [Key(true)]
        public Guid VoucherAllocationSubjectId
        {
            get { return fVoucherAllocationSubjectId; }
            set { SetPropertyValue<Guid>("VoucherAllocationSubjectId", ref fVoucherAllocationSubjectId, value); }
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
        private NAS.DAL.Staging.Accounting.Journal.AccountActorType fAccountActorTypeId;
        [Association("VoucherAllocationSubjectReferencesAccountActorType")]
        public NAS.DAL.Staging.Accounting.Journal.AccountActorType AccountActorTypeId
        {
            get { return fAccountActorTypeId; }
            set { SetPropertyValue<NAS.DAL.Staging.Accounting.Journal.AccountActorType>("AccountActorTypeId", ref fAccountActorTypeId, value); }
        }

        [Association(@"VoucherAllocationRoleReferencesVoucherAllocationSubject", typeof(VoucherAllocationRole))]
        public XPCollection<VoucherAllocationRole> VourcherAllocationRoles { get { return GetCollection<VoucherAllocationRole>("VoucherAllocationRoles"); } }
        //END References
    }
}
