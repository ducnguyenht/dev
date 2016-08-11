using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.UnitItem;

namespace NAS.DAL.Nomenclature.Item
{
    public partial class ItemUnitTypeConfig : XPCustomObject
    {
        public ItemUnitTypeConfig(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fItemUnitTypeConfigId;
        [Key(true)]
        public Guid ItemUnitTypeConfigId
        {
            get { return fItemUnitTypeConfigId; }
            set { SetPropertyValue<Guid>("ItemUnitTypeConfigId", ref fItemUnitTypeConfigId, value); }
        }

        private bool fIsMaster;
        public bool IsMaster
        {
            get { return fIsMaster; }
            set { SetPropertyValue<bool>("IsMaster", ref fIsMaster, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private bool isSelected;
        [NonPersistent]
        public bool IsSelected
        {
            get {
                if (RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                {
                    isSelected = true;
                    return true;
                }
                isSelected = false;
                return false;
            }

            set {
                isSelected = value;
            }

        }

        #region References
        private NAS.DAL.Nomenclature.Item.Item fItemId;
        [Association("ItemUnitTypeConfigReferencesItem")]
        public NAS.DAL.Nomenclature.Item.Item ItemId
        {
            get { return fItemId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.Item.Item>("ItemId", ref fItemId, value); }
        }

        private NAS.DAL.Nomenclature.UnitItem.UnitType fUnitTypeId;
        [Association("ItemUnitTypeConfigReferencesNAS.DAL.Nomenclature.UnitItem.UnitType")]
        public NAS.DAL.Nomenclature.UnitItem.UnitType UnitTypeId
        {
            get { return fUnitTypeId; }
            set { SetPropertyValue<NAS.DAL.Nomenclature.UnitItem.UnitType>("UnitTypeId", ref fUnitTypeId, value); }
        }
        #endregion

        #region logic

        #endregion
    }
}
