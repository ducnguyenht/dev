using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Nomenclature.Supplier
{
    public class SupplierBO
    {
        public void updateTradingCategoriesForObject<T>(Session session, Guid supplierId, List<Guid> typeList) {

            try
            {
                T s = session.GetObjectByKey<T>(supplierId);
                if (s == null)
                    throw new Exception("Key is not Exist in Org");

                CriteriaOperator criteria =
                        new BinaryOperator("OrganizationId!Key", supplierId, BinaryOperatorType.Equal);
                XPCollection<OrganizationCategory> ocl = new XPCollection<OrganizationCategory>(session, criteria);

                foreach (OrganizationCategory oc in ocl)
                {
                    oc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    oc.Save();
                }

                foreach (Guid k in typeList)
                {
                    TradingCategory tc = session.GetObjectByKey<TradingCategory>(k);
                    if (tc == null)
                        throw new Exception("Key is not Exist in TradingCategory");

                    criteria = CriteriaOperator.And(
                        new BinaryOperator("OrganizationId", s, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                        new BinaryOperator("TradingCategoryId", tc, BinaryOperatorType.Greater)
                    );

                    OrganizationCategory oc = session.FindObject<OrganizationCategory>(criteria);

                    if (oc == null)
                    {
                        oc = new OrganizationCategory(session);
                        oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        oc.OrganizationId = s as DAL.Nomenclature.Organization.Organization;
                        oc.TradingCategoryId = tc;
                        oc.Save();
                    }
                    else
                    {
                        oc.TradingCategoryId = tc;
                        oc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        oc.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
