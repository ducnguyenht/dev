using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.Organization
{

    public enum CustomerOrgEnum
    {
        MACDINH
    }

    [MapInheritance(MapInheritanceType.OwnTable)]
    public class CustomerOrg : BuyerOrg
    {
        public CustomerOrg(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        #region Logic

        public static CustomerOrg InitNewRow(Session session)
        {
            try
            {
                CustomerOrg customerOrg = new CustomerOrg(session)
                {
                    OrganizationTypeId = Util.getDefaultXpoObject<OrganizationType>(session),
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };
                customerOrg.Save();
                return customerOrg;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        public static CustomerOrg GetDefault(Session session, CustomerOrgEnum code)
        {
            CustomerOrg ret = null;
            try
            {
                ret = session.FindObject<CustomerOrg>(
                    new BinaryOperator("Code", Enum.GetName(typeof(CustomerOrgEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        new public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<CustomerOrg>("Code", "MACDINH"))
                {
                    XPQuery<OrganizationType> organizationTypeQuery = session.Query<OrganizationType>();
                    OrganizationType.Populate();
                    OrganizationType organizationType =
                        organizationTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    CustomerOrg customerOrg = new CustomerOrg(session)
                    {
                        Code = "MACDINH",
                        Name = "Khách hàng Mặc định",
                        OrganizationTypeId = organizationType,
                        Description = "",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };

                    customerOrg.Save();
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
