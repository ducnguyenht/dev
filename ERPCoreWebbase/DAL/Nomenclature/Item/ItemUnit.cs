using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemUnit
    {
        public ItemUnit(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        public bool checkIsExistHasManyChildren() 
        {
            if (ItemId != null && ParentItemUnitId != null && UnitId != null)
            {
                XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(Session);
                collection.Criteria = CriteriaOperator.And(
                        new BinaryOperator("ParentItemUnitId", ParentItemUnitId.ItemUnitId),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                        new BinaryOperator("ItemId", ItemId.ItemId, BinaryOperatorType.Equal)
                    );

                if (collection.Count > 0)
                    return true;

                return false;
            }
            return false;
        }

        public bool checkIsDupplicateUnitOnTreeAllBranch()
        {
            if (ItemId != null && UnitId != null)
            {
                ItemUnit it = Session.FindObject<ItemUnit>(
                  CriteriaOperator.And(
                    new BinaryOperator("UnitId", UnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("ItemId", ItemId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                  ));

                if (it != null)
                    return true;
                return false;
            }
            return false;
        }

        protected override void OnSaving()
        {
 
            base.OnSaving();

            if (ItemUnitId.Equals(Guid.Empty) && checkIsDupplicateUnitOnTreeAllBranch())
                throw new Exception(String.Format("Lỗi trùng đơn vị tính '{0}' trên cây", UnitId.Name));

            if (ItemUnitId.Equals(Guid.Empty) && checkIsExistHasManyChildren())
                throw new Exception(String.Format("Không được có nhiều nhánh trên cây hiện tại"));

            //if (!ItemUnitId.Equals(Guid.Empty) && checkIsDupplicateUnitOnTreeBranch())
            //    throw new Exception(String.Format("Lỗi trùng đơn vị tính '{0}' trên nhánh hiện tại của cây", UnitId.Name));

            //if (checkIsDupplicateUnitOnTheSameLevelBranch())
            //    throw new Exception(String.Format("Lỗi trùng đơn vị tính '{0}' trên cấp hiện tại", UnitId.Name));

            //ItemUnit parentitemunit;
            //if (checkIsExistCrosswiseRelationShip(out parentitemunit))
            //    throw new Exception(String.Format("Lỗi tồn tại các quan hệ chéo trên đơn vị tính '{0}'", UnitId.Name));
            
            
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<ItemUnit>("RowStatus",
                        Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL))
                {
                    ItemUnit defaultForSelectAll = new ItemUnit(session)
                    {
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    defaultForSelectAll.Save();
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

    }

}
