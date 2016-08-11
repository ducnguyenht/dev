using System;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Vouches.Allocation;

namespace NAS.DAL.Accounting.Configure
{

    public class Allocation : XPCustomObject
    {
        public Allocation(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        // Fields...

        private OwnerOrg _OwnerOrgId;
        private AllocationType _fAllocationTypeId;
        private short _RowStatus;
        private string _Name;
        private string _Description;
        private string _Code;
        private Guid _AllocationId;

        [Key(true)]
        public Guid AllocationId
        {
            get
            {
                return _AllocationId;
            }
            set
            {
                SetPropertyValue("AllocationId", ref _AllocationId, value);
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }


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


        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }



        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue("RowStatus", ref _RowStatus, value);
            }
        }


        [Association("AllocationRefencesAllcationType")]
        public AllocationType AllocationTypeId
        {
            get { return _fAllocationTypeId; }
            set { SetPropertyValue<AllocationType>("AllocationTypeId", ref _fAllocationTypeId, value); }
        }


        [Association(@"AllocationRefencesOwerOrg", typeof(OwnerOrg))]
        public OwnerOrg OwnerOrgId
        {
            get
            {
                return _OwnerOrgId;
            }
            set
            {
                SetPropertyValue("OwnerOrgId", ref _OwnerOrgId, value);
            }
        }

        [Association("AllocationTemplatesRefencesAllocation",typeof(AllocationAccountTemplate)), Aggregated]
        public XPCollection<AllocationAccountTemplate> AllocationTemplates
        {
            get
            {
                return GetCollection<AllocationAccountTemplate>("AllocationTemplates");
            }
        }

        [Association(@"VoucherAllocationReferencesNAS.DAL.Accounting.Configure.Allocation", typeof(VoucherAllocation))]
        public XPCollection<VoucherAllocation> VoucherAllocations { get { return GetCollection<VoucherAllocation>("VoucherAllocations"); } }

        [Association("AllocationAccountActorTypeReferencesAllocation"), Aggregated]
        public XPCollection<AllocationAccountActorType> AllocationAccountActorTypes
        {
            get
            {
                return GetCollection<AllocationAccountActorType>("AllocationAccountActorTypes");
            }
        }
    
    }

}