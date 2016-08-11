using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ShareDim;
using Utility;
using NAS.DAL.BI.Actor;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Inventory;
using NAS.DAL.BI.Item;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.Accounting.Journal;

namespace NAS.BO.ETL
{
    public class DimBO
    {
        #region MonthDim
        public MonthDim GetMonthDim(Session session, short Month)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Name = new BinaryOperator("Name", Month.ToString(), BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Name,criteria_RowStaus);
                MonthDim MonthDim = session.FindObject<MonthDim>(criteria);
                if (MonthDim == null)
                {
                    MonthDim = new MonthDim(session);
                    MonthDim.Description = Month.ToString();
                    MonthDim.Name = Month.ToString();
                    MonthDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    MonthDim.Save();
                }
                return MonthDim;
            }
            catch
            {
                return null;
            }
        }
        public MonthDim CreateMonthDim(Session session, short Month)
        {
            try
            {
                if (Month > 12 || Month < 1)
                {
                    return null;
                }
                else
                {
                    MonthDim monthDim = new MonthDim(session);
                    monthDim.Description = "Tháng " + Month.ToString();
                    monthDim.Name = Month.ToString();
                    monthDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    monthDim.Save();
                    return monthDim;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region YearDim
        public YearDim GetYearDim(Session session, short Year)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Name = new BinaryOperator("Name", Year.ToString(), BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Name, criteria_RowStaus);
                YearDim YearDim = session.FindObject<YearDim>(criteria);
                if (YearDim == null)
                {
                    YearDim = new YearDim(session);
                    YearDim.Description = Year.ToString();
                    YearDim.Name = Year.ToString();
                    YearDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    YearDim.Save();
                }
                return YearDim;
            }
            catch
            {
                return null;
            }
        }
        public YearDim CreateYearDim(Session session, short Year)
        {
            try
            {
                YearDim yearDim = new YearDim(session);
                yearDim.Description = "Năm " + Year.ToString();
                yearDim.Name = Year.ToString();
                yearDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                yearDim.Save();
                return yearDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region OwnerOrgDim
        public OwnerOrgDim GetOwnerOrgDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                OwnerOrgDim ownerOrgDim = session.FindObject<OwnerOrgDim>(criteria);
                if (ownerOrgDim == null)
                {
                    return CreateOwnerOrgDim(session, RefId);
                }
                return ownerOrgDim;
            }
            catch
            {
                return null;
            }
        }
        public OwnerOrgDim CreateOwnerOrgDim(Session session, Guid RefId)
        {
            try
            {
                Organization ownerOrg = session.GetObjectByKey<Organization>(RefId);
                if (ownerOrg == null) return null;
                OwnerOrgDim ownerOrgDim = new OwnerOrgDim(session);
                ownerOrgDim.Code = ownerOrg.Code;
                ownerOrgDim.Description = ownerOrg.Description;
                ownerOrgDim.RefId = RefId;
                ownerOrgDim.Name = ownerOrg.Name;
                ownerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                ownerOrgDim.Save();
                return ownerOrgDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region SupplierOrgDim
        public SupplierOrgDim GetSupplierOrgDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                SupplierOrgDim supplierOrgDim = session.FindObject<SupplierOrgDim>(criteria);
                if (supplierOrgDim == null)
                {
                    return CreateSupplierOrgDim(session, RefId);
                }
                return supplierOrgDim;
            }
            catch
            {
                return null;
            }
        }
        public SupplierOrgDim CreateSupplierOrgDim(Session session, Guid RefId)
        {
            try
            {
                Organization supplierOrg = session.GetObjectByKey<Organization>(RefId);
                if (supplierOrg == null) return null;
                SupplierOrgDim supplierOrgDim = new SupplierOrgDim(session);
                supplierOrgDim.Code = supplierOrg.Code;
                supplierOrgDim.Description = supplierOrg.Description;
                supplierOrgDim.RefId = RefId;
                supplierOrgDim.Name = supplierOrg.Name;
                supplierOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                supplierOrgDim.Save();
                return supplierOrgDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region CustomerOrgDim
        public CustomerOrgDim GetCustomerOrgDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                CustomerOrgDim CustomerOrgDim = session.FindObject<CustomerOrgDim>(criteria);
                if (CustomerOrgDim == null)
                {
                    return CreateCustomerOrgDim(session, RefId);
                }
                return CustomerOrgDim;
            }
            catch
            {
                return null;
            }
        }
        public CustomerOrgDim CreateCustomerOrgDim(Session session, Guid RefId)
        {
            try
            {
                Organization supplierOrg = session.GetObjectByKey<Organization>(RefId);
                if (supplierOrg == null) return null;
                CustomerOrgDim customerOrgDim = new CustomerOrgDim(session);
                customerOrgDim.Code = supplierOrg.Code;
                customerOrgDim.Description = supplierOrg.Description;
                customerOrgDim.RefId = RefId;
                customerOrgDim.Name = supplierOrg.Name;
                customerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                customerOrgDim.Save();
                return customerOrgDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region InventoryDim
        public InventoryDim GetInventoryDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                InventoryDim inventoryDim = session.FindObject<InventoryDim>(criteria);
                if (inventoryDim == null)
                {
                    return CreateInventoryDim(session, RefId);
                }
                return inventoryDim;
            }
            catch
            {
                return null;
            }
        }
        public InventoryDim CreateInventoryDim(Session session, Guid RefId)
        {
            try
            {
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(RefId);
                if (inventory == null) return null;
                InventoryDim inventoryDim = new InventoryDim(session);
                inventoryDim.Code = inventory.Code;
                inventoryDim.Description = inventory.Description;
                inventoryDim.RefId = RefId;
                inventoryDim.Name = inventory.Name;
                inventoryDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                inventoryDim.Save();
                return inventoryDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region ItemDim
        public ItemDim GetItemDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                ItemDim itemDim = session.FindObject<ItemDim>(criteria);
                if (itemDim == null)
                {
                    return CreateItemDim(session, RefId);
                }
                return itemDim;
            }
            catch
            {
                return null;
            }
        }
        public ItemDim CreateItemDim(Session session, Guid RefId)
        {
            try
            {
                NAS.DAL.Nomenclature.Item.Item item = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Item>(RefId);
                if (item == null) return null;
                ItemDim itemDim = new ItemDim(session);
                itemDim.Code = item.Code;
                itemDim.Description = item.Description;
                itemDim.RefId = RefId;
                itemDim.Name = item.Name;
                itemDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                itemDim.Save();
                return itemDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region UnitDim
        public UnitDim GetUnitDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                UnitDim unitDim = session.FindObject<UnitDim>(criteria);
                if (unitDim == null)
                {
                    return CreateUnitDim(session, RefId);
                }
                return unitDim;
            }
            catch
            {
                return null;
            }
        }
        public UnitDim CreateUnitDim(Session session, Guid RefId)
        {
            try
            {
                NAS.DAL.Nomenclature.Item.Unit unit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Unit>(RefId);
                if (unit == null) return null;
                UnitDim unitDim = new UnitDim(session);
                unitDim.Code = unit.Code;
                unitDim.Description = unit.Description;
                unitDim.RefId = RefId;
                unitDim.Name = unit.Name;
                unitDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                unitDim.Save();
                return unitDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region InventoryCommandDim
        public InventoryCommandDim GetInventoryCommandDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                InventoryCommandDim inventoryCommand = session.FindObject<InventoryCommandDim>(criteria);
                if (inventoryCommand == null)
                {
                    return CreateInventoryCommandDim(session, RefId);
                }
                return inventoryCommand;
            }
            catch
            {
                return null;
            }
        }
        public InventoryCommandDim CreateInventoryCommandDim(Session session, Guid RefId)
        {
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(RefId);
                if (command == null) return null;
                InventoryCommandDim commandDim = new InventoryCommandDim(session);
                commandDim.Code = command.Code;
                commandDim.Description = command.Description;
                commandDim.RefId = RefId;
                commandDim.Name = command.Name;
                commandDim.IssueDate = command.IssueDate;
                commandDim.CommandType = command.CommandType;
                commandDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                commandDim.Save();
                return commandDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region CorrespondInventoryDim
        public CorrespondInventoryDim GetCorrespondInventoryDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                CorrespondInventoryDim inventoryDim = session.FindObject<CorrespondInventoryDim>(criteria);
                if (inventoryDim == null)
                {
                    return CreateCorrespondInventoryDim(session, RefId);
                }
                return inventoryDim;
            }
            catch
            {
                return null;
            }
        }
        public CorrespondInventoryDim CreateCorrespondInventoryDim(Session session, Guid RefId)
        {
            try
            {
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(RefId);
                if (inventory == null) return null;
                CorrespondInventoryDim inventoryDim = new CorrespondInventoryDim(session);
                inventoryDim.Code = inventory.Code;
                inventoryDim.Description = inventory.Description;
                inventoryDim.RefId = RefId;
                inventoryDim.Name = inventory.Name;
                inventoryDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                inventoryDim.Save();
                return inventoryDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region InventoryTransactionDim
        public InventoryTransactionDim GetInventoryTransactionDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                InventoryTransactionDim transactionDim = session.FindObject<InventoryTransactionDim>(criteria);
                if (transactionDim == null)
                {
                    return CreateInventoryTransactionDim(session, RefId);
                }
                return transactionDim;
            }
            catch
            {
                return null;
            }
        }
        public InventoryTransactionDim CreateInventoryTransactionDim(Session session, Guid RefId)
        {
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(RefId);
                if (transaction == null) return null;
                InventoryTransactionDim transactionDim = new InventoryTransactionDim(session);
                transactionDim.Code = transaction.Code;
                transactionDim.Description = transaction.Description;
                transactionDim.RefId = RefId;
                transactionDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                transactionDim.Save();
                return transactionDim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region AccountingPeriodDim
        public AccountingPeriodDim GetAccountingPeriodDim(Session session, Guid RefId)
        {
            try
            {
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_RefId = new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStaus);
                AccountingPeriodDim accountingPeriodDim = session.FindObject<AccountingPeriodDim>(criteria);
                if (accountingPeriodDim == null)
                {
                    return CreateAccountingPeriodDim(session, RefId);
                }
                return accountingPeriodDim;
            }
            catch
            {
                return null;
            }
        }
        public AccountingPeriodDim CreateAccountingPeriodDim(Session session, Guid RefId)
        {
            try
            {
                AccountingPeriod period = session.GetObjectByKey<AccountingPeriod>(RefId);
                if (period == null) return null;
                AccountingPeriodDim Dim = new AccountingPeriodDim(session);
                Dim.Code = period.Code;
                Dim.Description = period.Description;
                Dim.FromDateTime = period.FromDateTime;
                Dim.ToDateTime = period.ToDateTime;
                Dim.RefId = RefId;
                Dim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                Dim.Save();
                return Dim;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
