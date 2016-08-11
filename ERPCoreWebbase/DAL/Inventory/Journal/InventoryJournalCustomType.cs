using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Journal
{
    public class InventoryJournalCustomType: XPCustomObject
    {
        public InventoryJournalCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fInventoryJournalCustomTypeId;
        [Key(true)]
        public Guid InventoryJournalCustomTypeId
        {
            get { return fInventoryJournalCustomTypeId; }
            set { SetPropertyValue<Guid>("InventoryJournalCustomTypeId", ref fInventoryJournalCustomTypeId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.ObjectType fObjectTypeId;
        [Association(@"InventoryJournalCustomTypeReferencesObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }

        private InventoryJournal fInventoryJournal;
        [Association(@"InventoryJournalCustomTypeReferencesInventoryJournal")]
        public InventoryJournal InventoryJournalId
        {
            get { return fInventoryJournal; }
            set { SetPropertyValue<InventoryJournal>("InventoryJournal", ref fInventoryJournal, value); }
        }
    }
}
