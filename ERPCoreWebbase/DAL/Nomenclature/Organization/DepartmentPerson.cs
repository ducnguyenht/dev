using System;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class DepartmentPerson
    {
        public DepartmentPerson(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region

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
                XPQuery<Person> personQuery = session.Query<Person>();
                Person.Populate();
                Person person =
                    personQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                if (!Util.isExistXpoObject<DepartmentPerson>("PersonId.PersonId", person.PersonId))
                {
                    XPQuery<Department> departmentQuery = session.Query<Department>();
                    Department.Populate();

                    Department department =
                        departmentQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    DepartmentPerson departmentPerson = new DepartmentPerson(session)
                    {
                        DepartmentId = department,
                        PersonId = person,
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };

                    departmentPerson.Save();
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
