using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Command.CommanDynamicField
{
    public class InventoryCommandObject : XPCustomObject
    {
        public InventoryCommandObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fInventoryCommandObjectId;
        [Key(true)]
        public Guid InventoryCommandObjectId
        {
            get { return fInventoryCommandObjectId; }
            set { SetPropertyValue<Guid>("InventoryCommandObjectId", ref fInventoryCommandObjectId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.Object fObjectId;
        [Association(@"InventoryCommandObjectReferencesNAS.DAL.CMS.ObjectDocument.Object")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get { return fObjectId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.Object>("ObjectId", ref fObjectId, value); }
        }

        private InventoryCommand fInventoryCommandId;
        [Association(@"InventoryCommandObjectReferencesInventoryCommand")]
        public InventoryCommand InventoryCommandId
        {
            get { return fInventoryCommandId; }
            set { SetPropertyValue<InventoryCommand>("InventoryCommandId", ref fInventoryCommandId, value); }
        }
    }
}
