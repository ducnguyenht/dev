using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Invoice
{

    public partial class PromotionType
    {
        public PromotionType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into PromotionType table
                if (!Util.isExistXpoObject<PromotionType>("PromotionTypeName", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    PromotionType promotionType = new PromotionType(session)
                    {
                        PromotionTypeName = Utility.Constant.NAAN_DEFAULT_NAME,
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };

                    promotionType.Save();
                }

                //insert product promotion type
                if (!Util.isExistXpoObject<PromotionType>("PromotionTypeName", "PROMOTION_TYPE_PRODUCT"))
                {
                    PromotionType promotionType = new PromotionType(session)
                    {
                        PromotionTypeName = "PROMOTION_TYPE_PRODUCT",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    promotionType.Save();
                }

                //insert product service type
                if (!Util.isExistXpoObject<PromotionType>("PromotionTypeName", "PROMOTION_TYPE_SERVICE"))
                {
                    PromotionType promotionType = new PromotionType(session)
                    {
                        PromotionTypeName = "PROMOTION_TYPE_SERVICE",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    promotionType.Save();
                }

            }
            catch (Exception)
            {
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
