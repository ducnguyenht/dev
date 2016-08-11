using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemUnitRelationType
    {
        public ItemUnitRelationType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<ItemUnitRelationType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    ItemUnitRelationType unit = new ItemUnitRelationType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                } 
                //insert UNIT relation
                if (!Util.isExistXpoObject<ItemUnitRelationType>("Name", "UNIT"))
                {
                    ItemUnitRelationType unit = new ItemUnitRelationType(session)
                    {
                        Name = "UNIT",
                        Description = "Quan hệ cấu hình đơn vị tính",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                //insert BOM relation
                if (!Util.isExistXpoObject<ItemUnitRelationType>("Name", "BOM"))
                {
                    ItemUnitRelationType unit = new ItemUnitRelationType(session)
                    {
                        Name = "BOM",
                        Description = "Quan hệ cấu thành sản phẩm",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
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
