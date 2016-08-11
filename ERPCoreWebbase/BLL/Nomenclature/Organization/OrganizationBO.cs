using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Vouches;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Journal;

namespace NAS.BO.Nomenclature.Organization
{
    public class OrganizationBO
    {
        public bool checkExistDepartmentInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("OrganizationId", organId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                //Department depart = session.FindObject<Department>(criteria);
                XPCollection<Department> departCL = new XPCollection<Department>(session, criteria);
                if (departCL.Count > 0)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistBillActorInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("OrganizationId", organId);
                BillActor billActor = session.FindObject<BillActor>(criteria);
                if (billActor == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }        
        public bool checkExistInventoryInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("OrganizationId", organId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));                
                XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory> inventCL = new XPCollection<DAL.Nomenclature.Inventory.Inventory>(session, criteria);
                if (inventCL.Count > 0)
                    return true;
                return false;
                //NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.FindObject<NAS.DAL.Nomenclature.Inventory.Inventory>(criteria);
                //if (inventory == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistInventoryUnitInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("OrganizationId", organId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<NAS.DAL.Nomenclature.Inventory.InventoryUnit> inventUnitCL = new XPCollection<DAL.Nomenclature.Inventory.InventoryUnit>(session, criteria);
                if (inventUnitCL.Count > 0)
                    return true;
                return false;
                //NAS.DAL.Nomenclature.Inventory.InventoryUnit inventoryUnit = session.FindObject<NAS.DAL.Nomenclature.Inventory.InventoryUnit>(criteria);
                //if (inventoryUnit == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistStockCartActorInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("OrganizationId", organId);
                StockCartActor stockCartActor = session.FindObject<StockCartActor>(criteria);
                if (stockCartActor == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistVouchesActorInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("OrganizationId", organId);
                VouchesActor vouchesActor = session.FindObject<VouchesActor>(criteria);
                if (vouchesActor == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistBillSourceOrgainzationInOrgan(Session session, Guid sourceOrganId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("SourceOrganizationId", sourceOrganId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0 , BinaryOperatorType.Greater));
                XPCollection<Bill> billCL = new XPCollection<Bill>(session, criteria);
                if (billCL.Count > 0)
                    return true;
                return false;
                //Bill bill = session.FindObject<Bill>(criteria);
                //if (bill == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistVouchesSourceOrgainzationInOrgan(Session session, Guid sourceOrganId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("SourceOrganizationId", sourceOrganId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<NAS.DAL.Vouches.Vouches> vouchesCL = new XPCollection<DAL.Vouches.Vouches>(session, criteria);
                if (vouchesCL.Count > 0)
                    return true;
                return false;
                //NAS.DAL.Vouches.Vouches vouches = session.FindObject<NAS.DAL.Vouches.Vouches>(criteria);
                //if (vouches == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistVouchesTargetOrgainzationInOrgan(Session session, Guid targetOrganId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(new BinaryOperator("TargetOrganizationId", targetOrganId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<NAS.DAL.Vouches.Vouches> vouchesCL = new XPCollection<DAL.Vouches.Vouches>(session, criteria);
                if (vouchesCL.Count > 0)
                    return true;
                return false;
                //CriteriaOperator criteria = new BinaryOperator("TargetOrganizationId", targetOrganId);
                //NAS.DAL.Vouches.Vouches vouches = session.FindObject<NAS.DAL.Vouches.Vouches>(criteria);
                //if (vouches == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistAccountInOrgan(Session session, Guid organId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("OrganizationId", organId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<Account> accountCL = new XPCollection<Account>(session, criteria);
                if (accountCL.Count > 0)
                    return true;
                return false;
                //Account account = session.FindObject<Account>(criteria);
                //if (account == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool checkExistAccountingInOrgan(Session session, Guid operaId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And( new BinaryOperator("OrganizationId", operaId, BinaryOperatorType.Equal),
                                                                  new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                XPCollection<AccountingPeriod> accPeriodCL = new XPCollection<AccountingPeriod>(session, criteria);
                if (accPeriodCL.Count > 0)
                    return true;
                return false;
                //AccountingPeriod accountingPeriod = session.FindObject<AccountingPeriod>(criteria);
                //if (accountingPeriod == null)
                //{
                //    return false;
                //}
                //return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
