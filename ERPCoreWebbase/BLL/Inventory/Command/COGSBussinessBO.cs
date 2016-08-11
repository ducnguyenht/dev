using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Accounting.Currency;

namespace NAS.BO.Inventory.Command
{
    public class COGSBussinessBO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="credit"></param>
        /// <param name="debit"></param>
        /// <param name="createDate"></param>
        /// <param name="price"></param>
        /// <param name="issueDate"></param>
        /// <param name="inventoryTransactionId"></param>
        /// <param name="inventoryId"></param>
        /// <param name="itemUnitId"></param>
        /// <param name="CurrencyId"></param>
        public void CreateCOGS(
            UnitOfWork uow, 
            double credit, 
            double debit, 
            DateTime createDate, 
            double price,
            DateTime issueDate,
            Guid inventoryTransactionId,
            Guid inventoryId,
            Guid itemUnitId,
            Guid currencyId)
        {
            InventoryTransaction transaction = uow.GetObjectByKey<InventoryTransaction>(inventoryTransactionId);
            if (transaction == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            NAS.DAL.Nomenclature.Inventory.Inventory inventory = 
                uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
            if (inventory == null)
                throw new Exception("The Inventory is not exist in system");

            ItemUnit itemUnit = uow.GetObjectByKey<ItemUnit>(itemUnitId);
            if (itemUnit == null)
                throw new Exception("The ItemUnit is not exist in system");

            Currency currency = uow.GetObjectByKey<Currency>(currencyId);
            if (currency == null)
                throw new Exception("The Currency is not exist in system");

            COGS cogs = new COGS(uow);
            cogs.Balance = 0;
            cogs.Total = 0;
            cogs.Credit = credit;
            cogs.Debit = debit;
            cogs.CreateDate = createDate;
            cogs.UpdateDate = createDate;
            cogs.Price = price;
            cogs.Assumption = 1;
            cogs.IssueDate = issueDate;
            cogs.InventoryTransactionId = transaction;
            cogs.InventoryId = inventory;
            cogs.ItemUnitId = itemUnit;
            cogs.CurrencyId = currency;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="credit"></param>
        /// <param name="debit"></param>
        /// <param name="createDate"></param>
        /// <param name="price"></param>
        /// <param name="issueDate"></param>
        /// <param name="inventoryTransactionId"></param>
        /// <param name="inventoryId"></param>
        /// <param name="itemUnitId"></param>
        /// <param name="CurrencyId"></param>
        public void CreateCOGS(
            Session session,
            double credit,
            double debit,
            DateTime createDate,
            double price,
            DateTime issueDate,
            Guid inventoryTransactionId,
            Guid inventoryId,
            Guid itemUnitId,
            Guid currencyId)
        {
            InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(inventoryTransactionId);
            if (transaction == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
            if (inventory == null)
                throw new Exception("The Inventory is not exist in system");

            ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);
            if (itemUnit == null)
                throw new Exception("The ItemUnit is not exist in system");

            Currency currency = session.GetObjectByKey<Currency>(currencyId);
            if (currency == null)
                throw new Exception("The Currency is not exist in system");

            COGS cogs = new COGS(session);
            cogs.Balance = 0;
            cogs.Total = 0;
            cogs.Credit = credit;
            cogs.Debit = debit;
            cogs.CreateDate = createDate;
            cogs.UpdateDate = createDate;
            cogs.Price = price;
            cogs.Assumption = 1;
            cogs.IssueDate = issueDate;
            cogs.InventoryTransactionId = transaction;
            cogs.InventoryId = inventory;
            cogs.ItemUnitId = itemUnit;
            cogs.CurrencyId = currency;
            cogs.Save();
        }
    }
}
