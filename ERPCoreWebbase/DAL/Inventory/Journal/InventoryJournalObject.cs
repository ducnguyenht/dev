using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Journal
{
    public class InventoryJournalObject : XPCustomObject
    {
        public InventoryJournalObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Fields...
        private Guid _InventoryJournalObjectId;
        private NAS.DAL.CMS.ObjectDocument.Object _ObjectId;
        private InventoryJournal _InventoryJournalId;

        [Key(true)]
        public Guid InventoryJournalObjectId
        {
            get
            {
                return _InventoryJournalObjectId;
            }
            set
            {
                SetPropertyValue("InventoryJournalObjectId", ref _InventoryJournalObjectId, value);
            }
        }

        [Association("InventoryJournalObjectReferenceInventoryJournal")]
        public InventoryJournal InventoryJournalId
        {
            get
            {
                return _InventoryJournalId;
            }
            set
            {
                SetPropertyValue("InventoryJournalId", ref _InventoryJournalId, value);
            }
        }


        [Association("InventoryJournalObjectReferenceObject")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get
            {
                return _ObjectId;
            }
            set
            {
                SetPropertyValue("ObjectId", ref _ObjectId, value);
            }
        }
    }
}
