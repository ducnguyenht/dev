using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Nomenclature.Organization
{
    public class SupplierOrgBO
    {
        public static void DeleteLogical(Guid supplierOrgId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Finds ManufacturerOrg by Id
                SupplierOrg supplierOrg = session.GetObjectByKey<SupplierOrg>(supplierOrgId);
                //Check foreign key constraint with Item table
                var itemList = supplierOrg.ItemSuppliers.Where(r => r.ItemId.RowStatus > 0);
                if (itemList.Count() > 0)
                {
                    string param0 = supplierOrg.Name;
                    string param1 = itemList.First().ItemId.Name;
                    throw new Exception(String.Format("Nhà cung cấp '{0}' đang được cấu hình trong hàng hóa '{1}'", param0, param1));
                }
                supplierOrg.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                supplierOrg.Save();
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
