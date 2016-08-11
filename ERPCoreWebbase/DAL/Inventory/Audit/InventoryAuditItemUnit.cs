using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;

namespace NAS.DAL.Inventory.Audit
{
    public class InventoryAuditItemUnit: XPCustomObject
    {
        public InventoryAuditItemUnit(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid _InventoryAuditItemUnitId;
        [Key(true)]
        public Guid InventoryAuditItemUnitId
        {
            get { return _InventoryAuditItemUnitId; }
            set { SetPropertyValue<Guid>("InventoryAuditItemUnitId", ref _InventoryAuditItemUnitId, value); }
        }

        double _BookingAmount;
        public double BookingAmount
        {
            get { return _BookingAmount; }
            set { SetPropertyValue<double>("BookingAmount", ref _BookingAmount, value); }
        }

        double _ProcessingAmount;
        public double ProcessingAmount
        {
            get { return _ProcessingAmount; }
            set { SetPropertyValue<double>("ProcessingAmount", ref _ProcessingAmount, value); }
        }

        double _RealAmount;
        public double RealAmount
        {
            get { return _RealAmount; }
            set { SetPropertyValue<double>("RealAmount", ref _RealAmount, value); }
        }


        double _ProcessingBiasAmount;
        public double ProcessingBiasAmount
        {
            get { return _ProcessingBiasAmount; }
            set { SetPropertyValue<double>("ProcessingBiasAmount", ref _ProcessingBiasAmount, value); }
        }



        byte[] _Suggestion;
        public byte[] Suggestion
        {
            get { return _Suggestion; }
            set { SetPropertyValue<byte[]>("Suggestion", ref _Suggestion, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        [Association(@"QualityItemReferencesInventoryAuditItemUnit", typeof(QualityItem))]
        public XPCollection<QualityItem> QualityItems { get { return GetCollection<QualityItem>("QualityItems"); } }

        private InventoryAuditArtifact _InventoryAuditArtifactId;
        [Association(@"InventoryAuditItemUnitReferencesInventoryAuditArtifact")]
        public InventoryAuditArtifact InventoryAuditArtifactId
        {
            get
            {
                return _InventoryAuditArtifactId;
            }
            set
            {
                SetPropertyValue("InventoryAuditArtifactId", ref _InventoryAuditArtifactId, value);
            }
        }

        private ItemUnit _ItemUnitId;
        [Association(@"InventoryAuditItemUnitReferencesItemUnit")]
        public ItemUnit ItemUnitId
        {
            get
            {
                return _ItemUnitId;
            }
            set
            {
                SetPropertyValue("ItemUnitId", ref _ItemUnitId, value);
            }
        }
    }
}
