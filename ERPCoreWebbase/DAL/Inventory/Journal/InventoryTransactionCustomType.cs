using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Journal
{
    public class InventoryTransactionCustomType : XPCustomObject
    {
        public InventoryTransactionCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fInventoryTransactionCustomTypeId;
        [Key(true)]
        public Guid InventoryTransactionCustomTypeId
        {
            get { return fInventoryTransactionCustomTypeId; }
            set { SetPropertyValue<Guid>("InventoryTransactionCustomTypeId", ref fInventoryTransactionCustomTypeId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.ObjectType fObjectTypeId;
        [Association(@"InventoryTransactionCustomTypeReferencesObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }

        private InventoryTransaction fInventoryTransactionId;
        [Association(@"InventoryTransactionCustomTypeReferencesInventoryTransaction")]
        public InventoryTransaction InventoryTransactionId
        {
            get { return fInventoryTransactionId; }
            set { SetPropertyValue<InventoryTransaction>("InventoryTransaction", ref fInventoryTransactionId, value); }
        }
    }
}
