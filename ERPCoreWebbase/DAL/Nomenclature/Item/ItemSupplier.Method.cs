using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.Item
{
    public partial class ItemSupplier
    {
        private bool checkIsDupplicateUnit() { 
            Session session = XpoHelper.GetNewSession();

            if (SupplierOrgId != null && ItemId != null)
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("SupplierOrgId", SupplierOrgId.OrganizationId),
                    new BinaryOperator("ItemId", ItemId.ItemId));
                ItemSupplier obj = session.FindObject<ItemSupplier>(criteria);
                if (obj != null)
                    return true;
            }

            return false;
        }

        protected override void OnSaving()
        {
            if (ItemSupplierId == Guid.Empty &&  checkIsDupplicateUnit())
                throw new Exception(String.Format("Nhà cung cấp {0} đã tồn tại, vui lòng chọn nhà cung cấp khác!", SupplierOrgId.Name));
            base.OnSaving();
        }
    }
}
