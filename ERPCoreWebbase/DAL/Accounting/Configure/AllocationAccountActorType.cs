using System;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Journal;

namespace NAS.DAL.Accounting.Configure
{

    public class AllocationAccountActorType : XPCustomObject
    {
        public AllocationAccountActorType(Session session)
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
        private AccountActorType _AccountActorTypeId;
        private bool _IsMaster;
        private Guid _AllocationAccountActorTypeId;
        private Allocation _AllocationId;

        [Key(true)]
        public Guid AllocationAccountActorTypeId
        {
            get
            {
                return _AllocationAccountActorTypeId;
            }
            set
            {
                SetPropertyValue("AllocationAccountActorTypeId", ref _AllocationAccountActorTypeId, value);
            }
        }

        public bool IsMaster
        {
            get
            {
                return _IsMaster;
            } 
            set
            {
                SetPropertyValue("IsMaster", ref _IsMaster, value);
            }
        }

        [Association("AllocationAccountActorTypeReferencesAllocation")]
        public Allocation AllocationId
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

        [Association("AllocationAccountActorTypeReferencesAccountActorType")]
        public AccountActorType AccountActorTypeId
        {
            get
            {
                return _AccountActorTypeId;
            }
            set
            {
                SetPropertyValue("AccountActorTypeId", ref _AccountActorTypeId, value);
            }
        }

    }

}