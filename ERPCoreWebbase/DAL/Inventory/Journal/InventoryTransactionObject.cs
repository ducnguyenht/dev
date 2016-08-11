using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Journal
{
    public class InventoryTransactionObject : XPCustomObject
    {
        public InventoryTransactionObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Fields...
        private Guid _InventoryTransactionObjectId;
        private NAS.DAL.CMS.ObjectDocument.Object _ObjectId;
        private InventoryTransaction _InventoryTransactionId;

        [Key(true)]
        public Guid InventoryTransactionObjectId
        {
            get
            {
                return _InventoryTransactionObjectId;
            }
            set
            {
                SetPropertyValue("InventoryTransactionObjectId", ref _InventoryTransactionObjectId, value);
            }
        }

        [Association("InventoryTransactionObjectReferenceInventoryTransaction")]
        public InventoryTransaction InventoryTransactionId
        {
            get
            {
                return _InventoryTransactionId;
            }
            set
            {
                SetPropertyValue("InventoryTransactionId", ref _InventoryTransactionId, value);
            }
        }


        [Association("InventoryTransactionObjectReferenceObject")]
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
