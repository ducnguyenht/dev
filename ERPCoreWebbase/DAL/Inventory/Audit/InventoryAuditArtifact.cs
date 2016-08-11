using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Inventory.Command;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Audit
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public class InventoryAuditArtifact: InventoryCommand
    {
        public InventoryAuditArtifact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        char fApprovalStatus;
        public char ApprovalStatus
        {
            get { return fApprovalStatus; }
            set { SetPropertyValue<char>("ApprovalStatus", ref fApprovalStatus, value); }
        }

        private NAS.DAL.Nomenclature.Inventory.Inventory _InventoryId;
        [Association(@"InventoryAuditArtifactReferencesInventory")]
        public NAS.DAL.Nomenclature.Inventory.Inventory InventoryId
        {
            get
            {
                return _InventoryId;
            }
            set
            {
                SetPropertyValue("InventoryId", ref _InventoryId, value);
            }
        }

        [Association(@"InventoryAuditItemUnitReferencesInventoryAuditArtifact", typeof(InventoryAuditItemUnit))]
        public XPCollection<InventoryAuditItemUnit> InventoryAuditItemUnits { get { return GetCollection<InventoryAuditItemUnit>("InventoryAuditItemUnits"); } }
    }
}
