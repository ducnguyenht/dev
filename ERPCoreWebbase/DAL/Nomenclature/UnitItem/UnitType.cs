using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.UnitItem
{
    public partial class UnitType : XPCustomObject
    {
        public UnitType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fUnitTypeId;
        [Key(true)]
        public Guid UnitTypeId
        {
            get { return fUnitTypeId; }
            set { SetPropertyValue<Guid>("UnitTypeId", ref fUnitTypeId, value); }
        }

        private string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        private bool fIsStandard;
        public bool IsStandard
        {
            get { return fIsStandard; }
            set { SetPropertyValue<bool>("IsStandard", ref fIsStandard, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        #region References
        [Association(@"ItemUnitTypeConfigReferencesNAS.DAL.Nomenclature.UnitItem.UnitType", typeof(ItemUnitTypeConfig))]
        public XPCollection<ItemUnitTypeConfig> _ItemUnitTypeConfig { get { return GetCollection<ItemUnitTypeConfig>("_ItemUnitTypeConfig"); } }

        [Association(@"UnitReferencesNAS.DAL.Nomenclature.UnitItem.UnitType", typeof(Unit))]
        public XPCollection<Unit> _Unit { get { return GetCollection<Unit>("_Unit"); } }
        #endregion

        #region populate default UnitType

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into UnitType table
                if (!Util.isExistXpoObject<UnitType>("Code", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_NAME,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        IsStandard = false,
                        Description = "",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }

                if (!Util.isExistXpoObject<UnitType>("Code", "SPECIFICATION", Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = "SPECIFICATION",
                        Name = @"Quy cách",
                        IsStandard = true,
                        Description = @"Loại đơn vị tính theo quy cách hàng hóa",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }

                if (!Util.isExistXpoObject<UnitType>("Code", "LENGTH", Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = "LENGTH",
                        Name = @"Độ dài",
                        IsStandard = true,
                        Description = @"Loại đơn vị đo độ dài",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }

                if (!Util.isExistXpoObject<UnitType>("Code", "WEIGHT", Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = "WEIGHT",
                        Name = @"Cân nặng",
                        IsStandard = true,
                        Description = @"Loại đơn vị đo cân nặng",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }

                if (!Util.isExistXpoObject<UnitType>("Code", "CAPACITY", Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = "CAPACITY",
                        Name = @"Thể tích",
                        IsStandard = true,
                        Description = @"Loại đơn vị đo thể tích",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }

                if (!Util.isExistXpoObject<UnitType>("Code", "AREA", Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    UnitType ut = new UnitType(session)
                    {
                        Code = "AREA",
                        Name = @"Diện tích",
                        IsStandard = true,
                        Description = @"Loại đơn vị đo diện tích",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT
                    };
                    ut.Save();
                }
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        #endregion
    }
}
