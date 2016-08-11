using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Inventory.Audit
{
    public class QualityItem : XPCustomObject
    {
        public QualityItem(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid _QualityItemId;
        [Key(true)]
        public Guid QualityItemId
        {
            get { return _QualityItemId; }
            set { SetPropertyValue<Guid>("QualityItemId", ref _QualityItemId, value); }
        }

        double _AuditAmount;
        public double AuditAmount
        {
            get { return _AuditAmount; }
            set { SetPropertyValue<double>("AuditAmount", ref _AuditAmount, value); }
        }

        double _QualityProcessingAmount;
        public double QualityProcessingAmount
        {
            get { return _QualityProcessingAmount; }
            set { SetPropertyValue<double>("QualityProcessingAmount", ref _QualityProcessingAmount, value); }
        }

        private QualityItemType _QualityItemTypeId;
        [Association(@"QualityItemReferencesQualityItemType")]
        public QualityItemType QualityItemType
        {
            get
            {
                return _QualityItemTypeId;
            }
            set
            {
                SetPropertyValue("QualityItemTypeId", ref _QualityItemTypeId, value);
            }
        }

        private InventoryAuditItemUnit _InventoryAuditItemUnitId;
        [Association(@"QualityItemReferencesInventoryAuditItemUnit")]
        public InventoryAuditItemUnit InventoryAuditItemUnitId
        {
            get
            {
                return _InventoryAuditItemUnitId;
            }
            set
            {
                SetPropertyValue("InventoryAuditItemUnitId", ref _InventoryAuditItemUnitId, value);
            }
        }
    }
}
