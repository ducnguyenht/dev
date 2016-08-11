using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections;
using NAS.DAL;
using NAS.DAL.Invoice;
using NAS.DAL.System.Privileage;
using NAS.DAL.Vouches;
using NAS.DAL.Inventory.StockCart;

namespace NAS.BO.Nomenclature.Organization
{
    public class DepartmentBO
    {
        public List<Person> getAllPeopleInDepartments(Session session, Department department)
        {
            try
            {
                CriteriaOperator criteria1 = new BinaryOperator("DepartmentId", department);
                CriteriaOperator criteria2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                XPCollection<DepartmentPerson> dplist = new XPCollection<DepartmentPerson>(session);
                dplist.Criteria = CriteriaOperator.And(
                        criteria1,
                        criteria2
                    );

                List<Person> pslist = new List<Person>();
                foreach (DepartmentPerson dp in dplist) {
                    if (!pslist.Contains<Person>(dp.PersonId) && dp.RowStatus > 0 && dp.PersonId.RowStatus > 0)
                        pslist.Add(dp.PersonId);
                }
                
                return pslist;
            }
            catch (Exception e) {
                throw;
            }
        }
        public List<Person> getAllPeopleInOrganization(Session session, Guid OrganizationId)
        {
            List<Person> rs = new List<Person>();
            NAS.DAL.Nomenclature.Organization.Organization o = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(OrganizationId);
            if (o == null)
                throw new Exception("The key is not exist in Organization");
            if (o.Deparments == null || o.Deparments.Count == 0)
                return null;

            foreach(Department d in o.Deparments)
            {
                List<Person> lst = getAllPeopleInDepartments(session, d);
                foreach (Person p in lst)
                {
                    if (!rs.Contains<Person>(p))
                        rs.Add(p);
                }
            }
            return rs;
        }
        public void updatePerson(Session session, List<Department> departments, Guid personId, string code, string name, short rowStatus)
        {
            try
            {
                session.BeginTransaction();
                Person p = session.GetObjectByKey<Person>(personId);

                if (p == null)
                    throw new Exception(string.Format("Không tồn tại personId {0 trong hệ thống}", personId));

                p.Code = code;
                p.Name = name;
                p.RowStatus = rowStatus;
                p.Save();

                XPCollection<DepartmentPerson> dplist = new XPCollection<DepartmentPerson>(session);
                dplist.Criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                    new BinaryOperator("PersonId", p, BinaryOperatorType.Equal));

                foreach (DepartmentPerson dp in dplist)
                {
                    dp.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    dp.Save();
                }

                foreach (Department d in departments)
                {
                    DepartmentPerson dp = session.FindObject<DepartmentPerson>(CriteriaOperator.And(
                        new BinaryOperator("PersonId!Key", p.PersonId, BinaryOperatorType.Equal),
                        new BinaryOperator("DepartmentId!Key", d.DepartmentId, BinaryOperatorType.Equal)
                        ));

                    if (dp != null)
                        dp.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    else
                    {
                        dp = new DepartmentPerson(session);
                        dp.DepartmentId = d;
                        dp.PersonId = p;
                        dp.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        dp.RowCreationTimeStamp = DateTime.Now;
                    }
                    dp.Save();
                }
                session.CommitTransaction();
            }
            catch (Exception) {
                session.RollbackTransaction();
                throw;
            }
        }
        public bool checkExistPersonInBill(Session session, Guid targetOrganId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("TargetOrganizationId", targetOrganId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<Bill> billCL = new XPCollection<Bill>(session, criteria);
                if (billCL.Count > 0)
                    return true;
                return false;
                //Bill bill = session.FindObject<Bill>(criteria);
                //if (bill == null)
                //    return false;

                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInVouchesActor(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("PersonId", personId);
                VouchesActor vouchesActor = session.FindObject<VouchesActor>(criteria);
                if (vouchesActor == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInStockCartActor(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("PersonId", personId);
                StockCartActor stockCartActor = session.FindObject<StockCartActor>(criteria);
                if (stockCartActor == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInSpecialPrivilege(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("PersonId", personId);
                SpecialPrivilege specialPrivilege = session.FindObject<SpecialPrivilege>(criteria);
                if (specialPrivilege == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInBillActor(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("PersonId", personId);
                BillActor billActor = session.FindObject<BillActor>(criteria);
                if (billActor == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInDepartmentPerson(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("PersonId", personId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<DepartmentPerson> dpPersonCL = new XPCollection<DepartmentPerson>(session, criteria);
                if (dpPersonCL.Count > 0)
                    return true;
                return false;
                //DepartmentPerson departmentPerson = session.FindObject<DepartmentPerson>(criteria);
                //if (departmentPerson == null)
                //    return false;

                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPersonInLoginAccount(Session session, Guid personId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("PersonId", personId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<LoginAccount> lgaccCL = new XPCollection<LoginAccount>(session, criteria);
                if (lgaccCL.Count > 0)
                    return true;
                return false;
                //LoginAccount loginAccount = session.FindObject<LoginAccount>(criteria);
                //if (loginAccount == null)
                //    return false;

                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistDepartmentPersonInDepartment(Session session, Guid departId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("DepartmentId", departId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                //DepartmentPerson departPerson = session.FindObject<DepartmentPerson>(criteria);
                XPCollection<DepartmentPerson> dpPersonCL = new XPCollection<DepartmentPerson>(session, criteria);
                if (dpPersonCL.Count > 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistPrivilegeDepartmentInDepartment(Session session, Guid departId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("DepartmentId", departId);
                PrivilegeDepartment privilegeDepart = session.FindObject<PrivilegeDepartment>(criteria);
                if (privilegeDepart == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistVouchesActorInDepartment(Session session, Guid departId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("DepartmentId", departId);
                VouchesActor vouchesActor = session.FindObject<VouchesActor>(criteria);
                if (vouchesActor == null)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
