using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Nomenclature.Organization
{
    public class ManufacturerOrgBO
    {
        public static void DeleteLogical(Guid manufacturerOrgId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Finds ManufacturerOrg by Id
                ManufacturerOrg manufacturerOrg = session.GetObjectByKey<ManufacturerOrg>(manufacturerOrgId);
                //Check foreign key constraint with Item table
                var itemList = manufacturerOrg.Items.Where(r => r.RowStatus > 0);
                if (itemList.Count() > 0)
                {
                    string param0 = manufacturerOrg.Name;
                    string param1 = itemList.First().Name;
                    throw new Exception(String.Format("Nhà sản xuất '{0}' đang được cấu hình trong sản phẩm '{1}'", param0, param1));
                }
                manufacturerOrg.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                manufacturerOrg.Save();
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
