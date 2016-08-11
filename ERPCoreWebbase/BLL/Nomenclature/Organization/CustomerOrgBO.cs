using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;

namespace NAS.BO.Nomenclature.Organization
{
    public class CustomerOrgBO
    {
        public bool CheckIsExistedCustomerInBill(Session session, Guid CustomerId) {
            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("SourceOrganizationId", CustomerId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                );

            Bill bill = session.FindObject<Bill>(criteria);
            if (bill == null)
                return false;
            return true;
        }

        public bool CheckIsExistedCustomerInVouche(Session session, Guid CustomerId)
        {
            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("SourceOrganizationId", CustomerId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                );

            NAS.DAL.Vouches.Vouches vouche = session.FindObject<NAS.DAL.Vouches.Vouches>(criteria);
            if (vouche == null)
                return false;
            return true;
        }
        
        public static void DeleteLogical(Guid customerOrgId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Finds ManufacturerOrg by Id
                NAS.DAL.Nomenclature.Organization.Organization customerOrg = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(customerOrgId);
                //Check foreign key constraint with Item table
                //var itemList = customerOrg.;
                //if (itemList.Count() > 0)
                //{
                //    string param0 = supplierOrg.Name;
                //    string param1 = itemList.First().ItemId.Name;
                //    throw new Exception(String.Format("Nhà cung cấp '{0}' đang được cấu hình trong hàng hóa '{1}'", param0, param1));
                //}
                customerOrg.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                customerOrg.Save();
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
