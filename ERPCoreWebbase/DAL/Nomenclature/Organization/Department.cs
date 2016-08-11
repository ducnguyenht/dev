using System;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class Department
    {
        public Department(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        public int CheckUniqueName()
        {
            return 1;
        }

        public int CheckConstraint()
        {
            return 1;
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<Department>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    XPQuery<Organization> organizationQuery = session.Query<Organization>();
                    XPQuery<DepartmentType> departmentTypeQuery = session.Query<DepartmentType>();

                    DepartmentType.Populate();
                    Organization.Populate();

                    Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    DepartmentType departmentType =
                        departmentTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    Department department = new Department(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        DepartmentTypeId = departmentType,
                        OrganizationId = organization,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };

                    department.Save();
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
