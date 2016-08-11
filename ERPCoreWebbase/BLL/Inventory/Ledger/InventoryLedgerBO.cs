using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Xpo.DB;

namespace NAS.BO.Inventory.Ledger
{
    public class InventoryLedgerBO
    {
        #region NewestInventoryLedger
        public InventoryLedger GetNewestInventoryLedger(Session session, Guid _ItemUnitId, Guid _InventoryId)
        {
            try
            {
                ItemUnit _ItemUnit = session.GetObjectByKey<ItemUnit>(_ItemUnitId);
                NAS.DAL.Nomenclature.Inventory.Inventory _Inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(_InventoryId);

                if (_ItemUnit == null || _Inventory == null) return null;

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", _ItemUnit, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Inventory = new BinaryOperator("InventoryId", _Inventory, BinaryOperatorType.Equal);

                CriteriaOperator criteria = CriteriaOperator.And(criteria_Inventory, criteria_ItemUnit, criteria_RowStatus);
                XPCollection<InventoryLedger> InventoryLedgerCol = new XPCollection<InventoryLedger>(session, criteria);
                InventoryLedgerCol.Sorting.Add(new SortProperty("IssueDate", SortingDirection.Descending));
                InventoryLedgerCol.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                InventoryLedger newestLedger = InventoryLedgerCol.FirstOrDefault();                
                return newestLedger;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public InventoryLedger GetNewestInventoryLedgerByDate(Session session, Guid _ItemUnitId, Guid _InventoryId, DateTime _IssueDate)
        {
            try
            {
                ItemUnit _ItemUnit = session.GetObjectByKey<ItemUnit>(_ItemUnitId);
                NAS.DAL.Nomenclature.Inventory.Inventory _Inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(_InventoryId);

                if (_ItemUnit == null || _Inventory == null) return null;

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", _ItemUnit, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Inventory = new BinaryOperator("InventoryId", _Inventory, BinaryOperatorType.Equal);
                CriteriaOperator criteria_IssueDate = new BinaryOperator("IssueDate", _IssueDate, BinaryOperatorType.LessOrEqual);

                CriteriaOperator criteria = CriteriaOperator.And(criteria_Inventory, criteria_ItemUnit, criteria_RowStatus);
                XPCollection<InventoryLedger> InventoryLedgerCol = new XPCollection<InventoryLedger>(session, criteria);
                InventoryLedgerCol.Sorting.Add(new SortProperty("IssueDate", SortingDirection.Descending));
                InventoryLedgerCol.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                InventoryLedger newestLedger = InventoryLedgerCol.FirstOrDefault();
                return newestLedger;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public InventoryLedger GetNewestInventoryLedger(Session session, ItemUnit _ItemUnit, Guid _InventoryId)
        {
            return GetNewestInventoryLedger(session, _ItemUnit.ItemUnitId, _InventoryId);
        }

        public InventoryLedger GetNewestInventoryLedger(Session session, Guid _ItemUnitId, NAS.DAL.Nomenclature.Inventory.Inventory _Inventory)
        {
            return GetNewestInventoryLedger(session, _ItemUnitId, _Inventory.InventoryId);
        }

        public InventoryLedger GetNewestInventoryLedger(Session session, ItemUnit _ItemUnit, NAS.DAL.Nomenclature.Inventory.Inventory _Inventory)
        {
            return GetNewestInventoryLedger(session, _ItemUnit.ItemUnitId, _Inventory.InventoryId);
        }
        #endregion

        #region ItemUnitBalance
        public double GetItemUnitBalance(Session session, Guid _ItemUnitId)
        {
            double result = 0;
            try
            {
                XPQuery<InventoryLedger> inventoryLedgerQuery = new XPQuery<InventoryLedger>(session);
                IQueryable<NAS.DAL.Nomenclature.Inventory.Inventory> inventoryList = (from r in inventoryLedgerQuery
                                     where r.ItemUnitId.ItemUnitId == _ItemUnitId
                                     select r.InventoryId).Distinct();
                foreach (NAS.DAL.Nomenclature.Inventory.Inventory inventory in inventoryList)
                {
                    result += GetItemUnitBalance(session, _ItemUnitId, inventory);
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double GetItemUnitBalance(Session session, ItemUnit _ItemUnit)
        {
            return GetItemUnitBalance(session, _ItemUnit.ItemUnitId);
        }

        public double GetItemUnitBalance(Session session, Guid _ItemUnitId,Guid _InventoryId)
        {
            double result= 0;
            try
            {
                InventoryLedger newestLedger = GetNewestInventoryLedger(session, _ItemUnitId, _InventoryId);
                if (newestLedger == null) return 0;
                result = newestLedger.Balance;
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double GetItemUnitBalance(Session session, ItemUnit _ItemUnit, Guid _InventoryId)
        {
            return GetItemUnitBalance(session, _ItemUnit.ItemUnitId, _InventoryId);
        }

        public double GetItemUnitBalance(Session session, Guid _ItemUnitId, NAS.DAL.Nomenclature.Inventory.Inventory _Inventory)
        {
            return GetItemUnitBalance(session, _ItemUnitId, _Inventory.InventoryId);
        }

        public double GetItemUnitBalance(Session session, ItemUnit _ItemUnit, NAS.DAL.Nomenclature.Inventory.Inventory _Inventory)
        {
            return GetItemUnitBalance(session, _ItemUnit.ItemUnitId, _Inventory.InventoryId);
        }
        #endregion
    }
}
