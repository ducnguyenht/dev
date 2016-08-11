using System;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Organization;
namespace NAS.DAL.Vouches
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class VouchesActor: XPCustomObject
    {
        public VouchesActor(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Person _PersonId;
        private Department _DepartmentId;
        private Organization _OrganizationId;
        private Vouches _VouchesId;
        private VouchesActorType _VouchesActorTypeId;
        Guid fVouchesActorId;
        [Key(true)]
        public Guid VouchesActorId
        {
            get { return fVouchesActorId; }
            set { SetPropertyValue<Guid>("VouchesActorId", ref fVouchesActorId, value); }
        }

        [Association("VouchesActorReferencesPerson")]
        public Person PersonId
        {
            get
            {
                return _PersonId;
            }
            set
            {
                SetPropertyValue("PersonId", ref _PersonId, value);
            }
        }
        [Association("VouchesActorReferencesDepartment")]
        public Department DepartmentId
        {
            get
            {
                return _DepartmentId;
            }
            set
            {
                SetPropertyValue("DepartmentId", ref _DepartmentId, value);
            }
        }
        [Association("VouchesActorReferencesOrganization")]
        public Organization OrganizationId
        {
            get
            {
                return _OrganizationId;
            }
            set
            {
                SetPropertyValue("OrganizationId", ref _OrganizationId, value);
            }
        }
        [Association("VouchesActorReferencesVouches")]
        public Vouches VouchesId
        {
            get
            {
                return _VouchesId;
            }
            set
            {
                SetPropertyValue("VouchesId", ref _VouchesId, value);
            }
        }

        [Association("VouchesActorReferencesVouchesActorType")]
        public VouchesActorType VouchesActorTypeId
        {
            get
            {
                return _VouchesActorTypeId;
            }
            set
            {
                SetPropertyValue("VouchesActorTypeId", ref _VouchesActorTypeId, value);
            }
        }
    }

}
