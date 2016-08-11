using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Command.CommanDynamicField
{
    public class InventoryCommandCustomType : XPCustomObject
    {
        public InventoryCommandCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fInventoryCommandCustomTypeId;
        [Key(true)]
        public Guid InventoryCommandCustomTypeId
        {
            get { return fInventoryCommandCustomTypeId; }
            set { SetPropertyValue<Guid>("InventoryCommandCustomTypeId", ref fInventoryCommandCustomTypeId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.ObjectType fObjectTypeId;
        [Association(@"InventoryCommandCustomTypeReferencesNAS.DAL.CMS.ObjectDocument.ObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }

        private InventoryCommand fInventoryCommandId;
        [Association(@"InventoryCommandCustomTypeReferencesInventoryCommand")]
        public InventoryCommand InventoryCommandId
        {
            get { return fInventoryCommandId; }
            set { SetPropertyValue<InventoryCommand>("InventoryCommandId", ref fInventoryCommandId, value); }
        }
    }
}
