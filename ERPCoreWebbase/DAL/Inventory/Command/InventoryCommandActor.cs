using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.DAL.Inventory.Command
{
    public class InventoryCommandActor : XPCustomObject
    {
        public InventoryCommandActor(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid _InventoryCommandActorId;
        [Key(true)]
        public Guid InventoryCommandActorId
        {
            get
            {
                return _InventoryCommandActorId;
            }
            set
            {
                SetPropertyValue("InventoryCommandActorId", ref _InventoryCommandActorId, value);
            }
        }

        private Guid _DepartmentId;
        public Guid DepartmentId
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

        Organization _OrganizationId;
        [Association(@"InventoryCommandActorReferencesOrganization")]
        public Organization OrganizationId
        {
            get { return _OrganizationId; }
            set { SetPropertyValue<Organization>("OrganizationId", ref _OrganizationId, value); }
        }

        Person _PersonId;
        [Association(@"InventoryCommandActorReferencesPerson")]
        public Person PersonId
        {
            get { return _PersonId; }
            set { SetPropertyValue<Person>("PersonId", ref _PersonId, value); }
        }

        private InventoryCommandActorType _InventoryCommandActorTypeId;
        [Association(@"InventoryCommandActorReferencesInventoryCommandActorType")]
        public InventoryCommandActorType InventoryCommandActorTypeId
        {
            get
            {
                return _InventoryCommandActorTypeId;
            }
            set
            {
                SetPropertyValue("InventoryCommandActorTypeId", ref _InventoryCommandActorTypeId, value);
            }
        }

        private InventoryCommand _InventoryCommandId;
        [Association(@"InventoryCommandReferencesInventoryCommandActor")]
        public InventoryCommand InventoryCommandId
        {
            get
            {
                return _InventoryCommandId;
            }
            set
            {
                SetPropertyValue("InventoryCommandId", ref _InventoryCommandId, value);
            }
        }


    }
}
