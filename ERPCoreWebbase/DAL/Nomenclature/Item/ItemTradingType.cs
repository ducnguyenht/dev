using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemTradingType
    {
        public ItemTradingType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<ItemTradingType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    ItemTradingType unit = new ItemTradingType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                //insert BUYING_AND_SALES unit
                if (!Util.isExistXpoObject<ItemTradingType>("Name", "BUYING_AND_SALES"))
                {
                    ItemTradingType unit = new ItemTradingType(session)
                    {
                        Name = "BUYING_AND_SALES",
                        Description = "Có thể mua và bán",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                //insert BUYING_ONLY unit
                if (!Util.isExistXpoObject<ItemTradingType>("Name", "BUYING_ONLY"))
                {
                    ItemTradingType unit = new ItemTradingType(session)
                    {
                        Name = "BUYING_ONLY",
                        Description = "Chỉ có thể mua",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                //insert SALES_ONLY unit
                if (!Util.isExistXpoObject<ItemTradingType>("Name", "SALES_ONLY"))
                {
                    ItemTradingType unit = new ItemTradingType(session)
                    {
                        Name = "SALES_ONLY",
                        Description = "Chỉ có thể bán",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save(); 
                }
                //insert BUYING_FOR_INTERNAL_USING unit
                if (!Util.isExistXpoObject<ItemTradingType>("Name", "BUYING_FOR_INTERNAL_USING"))
                {
                    ItemTradingType unit = new ItemTradingType(session)
                    {
                        Name = "BUYING_FOR_INTERNAL_USING",
                        Description = "Mua để dùng nội bộ",
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
