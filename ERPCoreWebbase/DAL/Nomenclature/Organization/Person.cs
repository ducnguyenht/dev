using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class Person
    {
        public Person(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static Person addDefaultPerson(){ 
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Person table

                Person person = new Person(session)
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = Utility.Constant.NAAN_DEFAULT_NAME,
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };
                person.Save();

                session.Dispose();
                return person;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public static void Populate()
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //insert default data into Person table
                if (!Util.isExistXpoObject<Person>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    Person person = new Person(uow)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    person.Save();
                }

                if (!Util.isExistXpoObject<Person>("PersonId", Guid.Parse("96947D1F-3FE3-4FF7-A759-DEBCC664FEBC")))
                {
                    
                    Person person = new Person(uow)
                    {
                        PersonId = Guid.Parse("96947D1F-3FE3-4FF7-A759-DEBCC664FEBC"),
                        Code = "MACDINH",
                        Name = "Nhân viên mặc định",
                        Description = "Nhân viên mặc định",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    DepartmentPerson departmentPerson = new DepartmentPerson(uow)
                    {
                        PersonId = person,
                        DepartmentId = Util.getDefaultXpoObject<Department>(uow),
                        RowStatus = Constant.ROWSTATUS_ACTIVE
                    };
                    person.Save();
                }

                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        #endregion

    }

}
