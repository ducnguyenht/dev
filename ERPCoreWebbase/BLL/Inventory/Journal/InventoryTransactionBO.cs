using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Buy.StockCart;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Nomenclature.Inventory;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Sales.PickingStockCart;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Nomenclature.Item;
using System.ComponentModel;
using NAS.BO.Accounting.Journal;
using Utility;
using NAS.BO.Inventory.Journal;
using NAS.DAL.Inventory.Lot;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.BO.Inventory.Jouranl
{
    public class InventoryTransactionBO
    {


        #region NewLogic

        private const string INVENTORY_TRANSACTION_TYPE_INPUT = "INPUT";
        private const string INVENTORY_TRANSACTION_TYPE_OUTPUT = "OUTPUT";

        public List<COGS> getNextCOGSList(Session session, Guid currentCOGSId, DateTime issueDate, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                return (from c in COGSQuery
                        where c.ItemUnitId.ItemUnitId == itemUnitId
                        && c.InventoryId.InventoryId == inventoryId
                        && c.IssueDate >= issueDate
                        && c.COGSId != currentCOGSId
                        orderby
                            c.IssueDate,
                            c.UpdateDate,
                            c.CreateDate
                        select c).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }


        public List<InventoryLedger> getNextInventoryLedgerList(Session session, Guid currentInventoryLedgerId, DateTime issueDate, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<InventoryLedger> inventoryLedgerQuery = session.Query<InventoryLedger>();
                return (from c in inventoryLedgerQuery
                        where c.ItemUnitId.ItemUnitId == itemUnitId
                        && c.InventoryId.InventoryId == inventoryId
                        && c.IssueDate >= issueDate
                        && c.InventoryLedgerId != currentInventoryLedgerId
                        orderby
                            c.IssueDate,
                            c.UpdateDate,
                            c.CreateDate
                        select c).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }


        public InventoryLedger getPreviousInventoryLedger(Session session, DateTime issueDate, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                //XPQuery<InventoryLedger> inventoryLedgerQuery = session.Query<InventoryLedger>();
                //return (from c in inventoryLedgerQuery
                //        where c.ItemUnitId.ItemUnitId == itemUnitId
                //        && c.InventoryId.InventoryId == inventoryId
                //        && c.IssueDate < issueDate
                //        orderby
                //            c.IssueDate descending
                //        //c.UpdateDate descending,
                //        //c.CreateDate descending
                //        select c).FirstOrDefault();
                CriteriaOperator criteria = CriteriaOperator.And
                (
                    new BinaryOperator("ItemUnitId.ItemUnitId", itemUnitId),
                    new BinaryOperator("InventoryId.InventoryId", inventoryId),
                    new BinaryOperator("IssueDate", issueDate, BinaryOperatorType.Less)
                );
                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                var collection = session.GetObjects(
                    session.GetClassInfo<InventoryLedger>(), criteria, sortCollection, 1, false, true).GetEnumerator();
                if (collection == null) return null;
                while (collection.MoveNext())
                {
                    return (InventoryLedger)collection.Current;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="inventoryId"></param>
        /// <param name="inventoryIssuedDate"></param>
        /// <param name="invetoryTransactionType">"INTPUT" or "OUTPUT"</param>
        /// <param name="itemUnit"></param>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="priceAfterPromotion"></param>
        /// <param name="description"></param>
        private void CreateAndRefreshCOGS(Session session,
                                InventoryTransaction inventoryTransaction,
                                Guid inventoryId,
                                DateTime inventoryIssuedDate,
                                string invetoryTransactionType,
                                ItemUnit itemUnit,
                                double debit,
                                double credit,
                                double priceAfterPromotion,
                                string description)
        {
            //Validation
            //Validate session
            if (session == null)
            {
                throw new ArgumentNullException("Session can not be null");
            }
            //Validate inventoryTransaction
            if (inventoryTransaction == null)
            {
                throw new ArgumentNullException("inventoryTransaction can not be null");
            }
            //Validate inventory
            NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
            if (inventory == null)
            {
                throw new ArgumentNullException("Specified inventory does not exist");
            }
            //Validate itemUnit
            if (itemUnit == null)
            {
                throw new ArgumentNullException("itemUnit can not be null");
            }
            //Validate invetoryTransactionType
            if (!invetoryTransactionType.Equals("INPUT") && !invetoryTransactionType.Equals("OUTPUT"))
            {
                throw new Exception("Inventory transaction type must be \"INPUT\" or \"OUTPUT\"");
            }
            //Validate when INPUT
            if (invetoryTransactionType.Equals("INPUT"))
            {
                if (debit <= 0)
                {
                    throw new Exception("Debit must be greater than 0 if invetoryTransactionType is \"INPUT\"");
                }
                //Validate priceAfterPromotion
                if (priceAfterPromotion <= 0)
                {
                    throw new Exception("priceAfterPromotion must be greater than 0 if invetoryTransactionType is \"INPUT\"");
                }
            }
            //Validate OUTPUT credit
            if (invetoryTransactionType.Equals("OUTPUT"))
            {
                if (credit <= 0)
                {
                    throw new Exception("Credit must be greater than 0 if invetoryTransactionType is \"OUTPUT\"");
                }
            }

            //Bussiness logic
            double balance = 0;
            double amount = 0;
            double total = 0;
            double price = 0;
            /*2013-11-24 ERP-1120 Khoa.Truong INS START*/
            double cogsPrice = 0;
            /*2013-11-24 ERP-1120 Khoa.Truong INS END*/
            //Get previous COGS of itemUnit
            COGS previousCOGS =
                this.getPreviousCOGS(session, inventoryIssuedDate, inventoryId, itemUnit.ItemUnitId);
            //Sets data when type is INPUT
            if (invetoryTransactionType.Equals("INPUT"))
            {
                credit = 0;
                price = priceAfterPromotion;
                amount = (debit - credit) * price;

                //Set balance and total
                if (previousCOGS != null)
                {
                    balance = previousCOGS.Balance + (debit - credit);
                    total = previousCOGS.Total + amount;
                    /*2013-11-24 ERP-1120 Khoa.Truong INS START*/
                    cogsPrice = total / balance;
                    /*2013-11-24 ERP-1120 Khoa.Truong INS END*/
                }
                else
                {
                    balance = debit - credit;
                    total = amount;
                    /*2013-11-24 ERP-1120 Khoa.Truong INS START*/
                    cogsPrice = total / balance;
                    /*2013-11-24 ERP-1120 Khoa.Truong INS END*/
                }
                
            }
            //Sets data when type is OUTPUT
            else
            {
                debit = 0;

                if (previousCOGS != null)
                {
                    if (previousCOGS.Balance < credit)
                    {
                        throw new Exception(
                        String.Format("Số lượng hàng hóa '{0}' với đơn vị tính '{1}' trong kho '{2}' không đủ để xuất kho",
                                            itemUnit.ItemId.Code, itemUnit.UnitId.Code, inventory.Code));
                    }
                    price = previousCOGS.COGSPrice;
                    balance = previousCOGS.Balance + (debit - credit);
                    amount = (debit - credit) * price;
                    total = previousCOGS.Total + amount;
                    /*2013-11-24 ERP-1120 Khoa.Truong INS START*/
                    if (balance != 0)
                    {
                        cogsPrice = total / balance; 
                    }
                    else
                    {
                        cogsPrice = previousCOGS.COGSPrice;
                    }
                    /*2013-11-24 ERP-1120 Khoa.Truong INS END*/
                }
                else
                {
                    throw new Exception(
                        String.Format("Không có hàng hóa '{0}' với đơn vị tính '{1}' trong kho '{2}'",
                                            itemUnit.ItemId.Code, itemUnit.UnitId.Code, inventory.Code));
                }
            }
            //Create new COGS
            COGS cOGS = new COGS(session)
            {
                COGSId = Guid.NewGuid(),
                InventoryTransactionId = inventoryTransaction,
                ItemUnitId = itemUnit,
                InventoryId = inventory,
                Debit = debit,
                Credit = credit,
                Price = price,
                Amount = amount,
                Assumption = 1,
                Balance = balance,
                Total = total,
                /*2013-11-24 ERP-1120 Khoa.Truong MOD START*/
                //COGSPrice = total / balance,
                COGSPrice = cogsPrice,
                /*2013-11-24 ERP-1120 Khoa.Truong MOD START*/
                Description = description,
                CreateDate = DateTime.Now,
                IssueDate = inventoryIssuedDate,
                UpdateDate = DateTime.Now
            };
            cOGS.Save();

            //Get next COGS list
            COGS tempPreviousCOGS = null;
            List<COGS> nextCOGSList =
                this.getNextCOGSList(session, cOGS.COGSId, inventoryIssuedDate, inventoryId, itemUnit.ItemUnitId);
            //Update COGS
            if (nextCOGSList != null)
            {
                tempPreviousCOGS = cOGS;
                foreach (var currentCOGS in nextCOGSList)
                {
                    //INPUT
                    if (currentCOGS.Credit == 0)
                    {
                        currentCOGS.Balance = tempPreviousCOGS.Balance + (currentCOGS.Debit - currentCOGS.Credit);
                        currentCOGS.Total = tempPreviousCOGS.Total + currentCOGS.Amount;
                        /*2013-11-24 ERP-1120 Khoa.Truong MOD START*/
                        //currentCOGS.COGSPrice = currentCOGS.Total / currentCOGS.Balance;
                        if (currentCOGS.Balance != 0)
                        {
                            currentCOGS.COGSPrice = Math.Abs(currentCOGS.Total / currentCOGS.Balance);
                        }
                        else
                        {
                            currentCOGS.COGSPrice = currentCOGS.Price;
                        }
                        /*2013-11-24 ERP-1120 Khoa.Truong MOD END*/
                    }
                    //OUTPUT
                    else if (currentCOGS.Debit == 0)
                    {
                        currentCOGS.Price = tempPreviousCOGS.COGSPrice;
                        currentCOGS.Amount = currentCOGS.Price * (currentCOGS.Debit - currentCOGS.Credit);
                        currentCOGS.Balance = tempPreviousCOGS.Balance + (currentCOGS.Debit - currentCOGS.Credit);
                        currentCOGS.Total = tempPreviousCOGS.Total + currentCOGS.Amount;
                        /*2013-11-24 ERP-1120 Khoa.Truong MOD START*/
                        //currentCOGS.COGSPrice = currentCOGS.Total / currentCOGS.Balance;
                        if (currentCOGS.Balance != 0)
                        {
                            currentCOGS.COGSPrice = Math.Abs(currentCOGS.Total / currentCOGS.Balance);
                        }
                        else
                        {
                            currentCOGS.COGSPrice = previousCOGS.COGSPrice;
                        }
                        /*2013-11-24 ERP-1120 Khoa.Truong MOD END*/
                    }
                    //Save
                    currentCOGS.Save();

                    tempPreviousCOGS = currentCOGS;
                }
            }

        }


        public double GetUnitCoefficient(Session session, Guid itemUnitId, Guid relateItemUnitId)
        {
            double result = 1;
            if (itemUnitId.Equals(relateItemUnitId))
            {
                return 1;
            }
            ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);
            ItemUnit relateItemUnit = session.GetObjectByKey<ItemUnit>(relateItemUnitId);
            try
            {
                //Up
                ItemUnit parentUnit = itemUnit.ParentItemUnitId;
                ItemUnit currentUnit = itemUnit;
                do
                {
                    if (parentUnit != null)
                    {
                        result *= currentUnit.NumRequired;
                    }
                    else
                    {
                        break;
                    }
                    if (parentUnit == relateItemUnit) return 1 / result;
                    currentUnit = parentUnit;
                    parentUnit = currentUnit.ParentItemUnitId;
                }
                while (parentUnit != null);

                //Down
                result = 1;
                parentUnit = relateItemUnit.ParentItemUnitId;
                currentUnit = relateItemUnit;
                do
                {
                    if (parentUnit != null)
                    {
                        result *= currentUnit.NumRequired;
                    }
                    else
                    {
                        break;
                    }
                    if (parentUnit == itemUnit) return result;
                    currentUnit = parentUnit;
                    parentUnit = currentUnit.ParentItemUnitId;
                }
                while (parentUnit != null);

            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="inventoryId"></param>
        /// <param name="inventoryIssuedDate"></param>
        /// <param name="invetoryTransactionType">"INTPUT" or "OUTPUT"</param>
        /// <param name="itemUnit"></param>
        /// <param name="debit"></param>
        /// <param name="credit"></param>
        /// <param name="description"></param>
        private void CreateAndRefreshInventoryLedger(Session session,
                                InventoryTransaction inventoryTransaction,
                                Guid inventoryId,
                                DateTime inventoryIssuedDate,
                                string invetoryTransactionType,
                                ItemUnit itemUnit,
                                double debit,
                                double credit,
                                string description)
        {
            //Validation
            //Validate session
            if (session == null)
            {
                throw new ArgumentNullException("Session can not be null");
            }
            //Validate inventoryTransaction
            if (inventoryTransaction == null)
            {
                throw new ArgumentNullException("inventoryTransaction can not be null");
            }
            //Validate inventory
            NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
            if (inventory == null)
            {
                throw new ArgumentNullException("Specified inventory does not exist");
            }
            //Validate itemUnit
            if (itemUnit == null)
            {
                throw new ArgumentNullException("itemUnit can not be null");
            }
            //Validate invetoryTransactionType
            if (!invetoryTransactionType.Equals("INPUT") && !invetoryTransactionType.Equals("OUTPUT"))
            {
                throw new Exception("Inventory transaction type must be \"INPUT\" or \"OUTPUT\"");
            }
            //Validate when INPUT
            if (invetoryTransactionType.Equals("INPUT"))
            {
                if (debit <= 0)
                {
                    throw new Exception("Debit must be greater than 0 if invetoryTransactionType is \"INPUT\"");
                }
            }
            //Validate OUTPUT credit
            if (invetoryTransactionType.Equals("OUTPUT"))
            {
                if (credit <= 0)
                {
                    throw new Exception("Credit must be greater than 0 if invetoryTransactionType is \"OUTPUT\"");
                }
            }

            XPQuery<ItemUnit> itemUnitQuery = session.Query<ItemUnit>();
            var relateItemUnitList = (from c in itemUnitQuery
                                      where c.ItemId == itemUnit.ItemId
                                      select c);
            //double inventoryLedgerOrder = 1;
            foreach (var relateItemUnit in relateItemUnitList)
            {
                //Get unit coefficient between the itemUnit and relateItemUnit
                bool isOriginal = false;
                double coefficient = this.GetUnitCoefficient(session, itemUnit.ItemUnitId, relateItemUnit.ItemUnitId);
                if (coefficient == 0)
                {
                    continue;
                }
                if (itemUnit.Equals(relateItemUnit))
                {
                    isOriginal = true;
                }
                //Bussiness logic
                double balance = 0;
                double convertedDebit = 0;
                double convertedCredit = 0;
                //Get previous InventoryLedger of itemUnit
                InventoryLedger previousInventoryLedger =
                    this.getPreviousInventoryLedger(session, inventoryIssuedDate, inventoryId, relateItemUnit.ItemUnitId);
                //Sets data when type is INPUT
                if (invetoryTransactionType.Equals("INPUT"))
                {
                    convertedDebit = debit * double.Parse(coefficient.ToString());
                    //Set balance
                    if (previousInventoryLedger != null)
                    {
                        balance = previousInventoryLedger.Balance + (convertedDebit - convertedCredit);
                    }
                    else
                    {
                        balance = convertedDebit - credit;
                    }
                }
                //Sets data when type is OUTPUT
                else
                {
                    convertedCredit = credit * double.Parse(coefficient.ToString());
                    if (previousInventoryLedger != null)
                    {
                        if (previousInventoryLedger.Balance < convertedCredit)
                        {
                            throw new Exception(
                            String.Format("Số lượng hàng hóa '{0}' với đơn vị tính '{1}' trong kho '{2}' không đủ để xuất kho",
                                                itemUnit.ItemId.Code, itemUnit.UnitId.Code, inventory.Code));
                        }
                        balance = previousInventoryLedger.Balance + (convertedDebit - convertedCredit);
                    }
                    else
                    {
                        throw new Exception(
                            String.Format("The item '{0}' with unit '{1}' does not exist in the inventory '{2}'",
                                                relateItemUnit.ItemId.Code, relateItemUnit.UnitId.Code, inventory.Code));
                    }
                }
                DateTime inventoryLedgerIssuedDate = new DateTime(inventoryIssuedDate.Ticks);
                //inventoryLedgerIssuedDate =
                //    inventoryLedgerIssuedDate.AddMilliseconds(inventoryLedgerOrder);
                //Create new InventoryLedger for the itemUnit
                InventoryLedger inventoryLedger = new InventoryLedger(session)
                {
                    InventoryLedgerId = Guid.NewGuid(),
                    InventoryTransactionId = inventoryTransaction,
                    ItemUnitId = relateItemUnit,
                    InventoryId = inventory,
                    Debit = convertedDebit,
                    Credit = convertedCredit,
                    Balance = balance,
                    Description = description,
                    CreateDate = DateTime.Now,
                    IssueDate = inventoryLedgerIssuedDate,
                    UpdateDate = DateTime.Now,
                    IsOriginal = isOriginal
                };
                inventoryLedger.Save();
                //Increase order of inventory ledger
                //inventoryLedgerOrder += 10;

                //Get next InventoryLedger list
                InventoryLedger tempPreviousInventoryLedger = null;
                List<InventoryLedger> nextInventoryLedgerList = null;

                nextInventoryLedgerList = this.getNextInventoryLedgerList(session,
                                                    inventoryLedger.InventoryLedgerId,
                                                    inventoryIssuedDate,
                                                    inventoryId,
                                                    relateItemUnit.ItemUnitId);
                //Update InventoryLedger
                if (nextInventoryLedgerList != null)
                {
                    tempPreviousInventoryLedger = inventoryLedger;
                    foreach (var currentInventoryLedger in nextInventoryLedgerList)
                    {
                        //Update balance
                        currentInventoryLedger.Balance = tempPreviousInventoryLedger.Balance +
                                    (currentInventoryLedger.Debit - currentInventoryLedger.Credit);
                        //Save
                        currentInventoryLedger.Save();

                        tempPreviousInventoryLedger = currentInventoryLedger;
                    }
                }

            }

        }


        public void insertInventoryJournalBalanceForward(Session session, InventoryTransactionBalanceForward inventoryTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory, ItemUnit itemUnit, double balance)
        {
            try
            {
                InventoryJournalBalanceForward invJournal = new InventoryJournalBalanceForward(session);
                invJournal.InventoryTransactionId = inventoryTransaction;
                invJournal.InventoryId = inventory;
                invJournal.ItemUnitId = itemUnit;
                invJournal.Credit = 0;
                invJournal.Debit = balance;
                invJournal.Balance = invJournal.Debit - invJournal.Credit;
                //invJournal.CreateDate = inventoryTransaction.IssueDate;
                invJournal.Description = inventoryTransaction.Description;
                invJournal.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        public void CreateInventoryTransactionBalanceForward(Session session, Guid accountingPeriodId, DateTime transactionTime, Guid inventoryId, Guid itemUnitId,
                                                            string code, string description, double balance, double price)
        {
            //Get accounting period
            AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);

            //Get previous InventoryTransaction
            InventoryTransactionBalanceForward previousInventoryTransaction =
                (InventoryTransactionBalanceForward)this.getPreviousInventoryTransaction(session,
                                                    accountingPeriodId,
                                                    transactionTime);

            ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);

            Guid previousInventoryTransactionId = Guid.Empty;
            if (previousInventoryTransaction != null)
            {
                previousInventoryTransactionId = previousInventoryTransaction.InventoryTransactionId;
            }
            //Get target inventory
            NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

            InventoryTransactionBalanceForward inventoryTransaction = null;

            string inventoryTransactionType = null;

            inventoryTransactionType = InventoryTransactionBO.INVENTORY_TRANSACTION_TYPE_INPUT;

            //Create new purchasing inventory transaction
            inventoryTransaction = new InventoryTransactionBalanceForward(session)
            {
                InventoryTransactionId = Guid.NewGuid(),
                Code = code,
                AccountingPeriodId = accountingPeriod,
                CreateDate = DateTime.Now,
                Description = description,
                IssueDate = transactionTime,
                //PreviousInventoryTransactionId = previousInventoryTransactionId,
                RowStatus = Constant.ROWSTATUS_ACTIVE,
                UpdateDate = DateTime.Now,
                Balance = balance
            };
            inventoryTransaction.Save();

            //Update next link
            //InventoryTransaction nextInventoryTransaction =
            //    getNextInventoryTransactionByPreviousId(session,
            //                                            accountingPeriod.AccountingPeriodId,
            //                                            previousInventoryTransactionId,
            //                                            inventoryTransaction.InventoryTransactionId);
            //if (nextInventoryTransaction != null)
            //{
            //    nextInventoryTransaction.PreviousInventoryTransactionId =
            //                                        inventoryTransaction.InventoryTransactionId;
            //    nextInventoryTransaction.Save();
            //}
            //double inventoryLedgerOrder = 0;

            double debit = 0;
            double credit = 0;

            debit = balance;

            this.insertInventoryJournalBalanceForward(
                session,
                inventoryTransaction, 
                inventory,
                itemUnit,
                balance);

            //Update COGS 
            double priceAfterPromotion = price;
            this.CreateAndRefreshCOGS
            (
                session,
                inventoryTransaction,       //Inventory transaction
                inventoryId,                //Create for inventory
                transactionTime,            //Inventory issued date
                inventoryTransactionType,   //Inventoty transaction type
                itemUnit,                   //For item unit
                debit,                      //Dedit
                credit,                     //Credit
                priceAfterPromotion,        //Price after promote
                description                 //Description
            );

            DateTime inventoryLedgerIssuedDate = new DateTime(transactionTime.Ticks);
            //inventoryLedgerIssuedDate =
            //    inventoryLedgerIssuedDate.AddMilliseconds(inventoryLedgerOrder);
            //Update InventoryLedger
            this.CreateAndRefreshInventoryLedger
            (
                session,
                inventoryTransaction,       //Inventory transaction
                inventoryId,                //Create for inventory
                inventoryLedgerIssuedDate,  //Inventory issued date
                inventoryTransactionType,   //Inventoty transaction type
                itemUnit,                   //For item unit
                debit,                      //Dedit
                credit,                     //Credit
                description                 //Description
            );
            //inventoryLedgerOrder += 10;

        }


        /// <summary>
        /// Creates new inventory transaction for billItems and updates COGS, updates balance of unit chart
        /// </summary>
        /// <param name="session">DevExpress.XPO.Session</param>
        /// <param name="forInventoryId">Create inventory transaction for InventoryId</param>
        /// <param name="inventoryTransactionCode">Inventory transaction code</param>
        /// <param name="inventoryTransactionDescription">Inventory transaction description</param>
        /// <param name="inventoryIssuedDate">Inventory issued date</param>
        /// <param name="billItems">List of bill item</param>
        public void CreateInventoryTransaction(Session session,
            Guid forInventoryId,
            string inventoryTransactionCode,
            string inventoryTransactionDescription,
            DateTime inventoryIssuedDate,
            XPCollection<BillItem> billItems)
        {
            //Validate billItems
            if (billItems.FirstOrDefault() == null)
            {
                throw new Exception("Must have at least one BillItem");
            }

            //Get current AccountingPeriod
            AccountingPeriod currentAccountingPeriod = AccountingPeriodBO.getCurrentAccountingPeriod(session);

            //Get previous InventoryTransaction
            InventoryTransaction previousInventoryTransaction =
                this.getPreviousInventoryTransaction(session,
                                                    currentAccountingPeriod.AccountingPeriodId,
                                                    inventoryIssuedDate);
            Guid previousInventoryTransactionId = Guid.Empty;
            if (previousInventoryTransaction != null)
            {
                previousInventoryTransactionId = previousInventoryTransaction.InventoryTransactionId;
            }
            //Get target inventory
            NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(forInventoryId);

            InventoryTransaction inventoryTransaction = null;

            string inventoryTransactionType = null;

            if (billItems.FirstOrDefault().BillId is NAS.DAL.Invoice.PurchaseInvoice)
            {
                inventoryTransactionType = InventoryTransactionBO.INVENTORY_TRANSACTION_TYPE_INPUT;

                //Create new purchasing inventory transaction
                inventoryTransaction = new PurchaseInvoiceInventoryTransaction(session)
                {
                    InventoryTransactionId = Guid.NewGuid(),
                    Code = inventoryTransactionCode,
                    AccountingPeriodId = currentAccountingPeriod,
                    CreateDate = DateTime.Now,
                    Description = inventoryTransactionDescription,
                    IssueDate = inventoryIssuedDate,
                    //PreviousInventoryTransactionId = previousInventoryTransactionId,
                    RowStatus = Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now,
                    PurchaseInvoiceId =
                        (NAS.DAL.Invoice.PurchaseInvoice)billItems.FirstOrDefault().BillId
                };
                inventoryTransaction.Save();
            }
            else if (billItems.FirstOrDefault().BillId is NAS.DAL.Invoice.SalesInvoice)
            {
                inventoryTransactionType = InventoryTransactionBO.INVENTORY_TRANSACTION_TYPE_OUTPUT;

                //Create new sale inventory transaction
                inventoryTransaction = new SalesInvoiceInventoryTransaction(session)
                {
                    InventoryTransactionId = Guid.NewGuid(),
                    Code = inventoryTransactionCode,
                    AccountingPeriodId = currentAccountingPeriod,
                    CreateDate = DateTime.Now,
                    Description = inventoryTransactionDescription,
                    IssueDate = inventoryIssuedDate,
                    //PreviousInventoryTransactionId = previousInventoryTransactionId,
                    RowStatus = Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now,
                    SalesInvoiceId =
                        (NAS.DAL.Invoice.SalesInvoice)billItems.FirstOrDefault().BillId
                };
                inventoryTransaction.Save();
            }

            //Update next link
            //InventoryTransaction nextInventoryTransaction =
            //    getNextInventoryTransactionByPreviousId(session,
            //                                            currentAccountingPeriod.AccountingPeriodId,
            //                                            previousInventoryTransactionId,
            //                                            inventoryTransaction.InventoryTransactionId);
            //if (nextInventoryTransaction != null)
            //{
            //    nextInventoryTransaction.PreviousInventoryTransactionId =
            //                                        inventoryTransaction.InventoryTransactionId;
            //    nextInventoryTransaction.Save();
            //}
            double inventoryLedgerOrder = 0;
            foreach (var billItem in billItems)
            {
                double debit = 0;
                double credit = 0;
                //Create GeneralJournal for each billItem
                this.insertInventoryJournal(session,
                                            inventoryTransaction,
                                            inventory, billItem);

                if (inventoryTransactionType.Equals(InventoryTransactionBO.INVENTORY_TRANSACTION_TYPE_INPUT))
                {
                    debit = billItem.Quantity;
                }
                else if (inventoryTransactionType.Equals(InventoryTransactionBO.INVENTORY_TRANSACTION_TYPE_OUTPUT))
                {
                    credit = billItem.Quantity;
                }

                //Update COGS 
                double priceAfterPromotion =
                    billItem.Price * (100 - billItem.PromotionInPercentage) / 100;
                this.CreateAndRefreshCOGS
                (
                    session,
                    inventoryTransaction,       //Inventory transaction
                    forInventoryId,             //Create for inventory
                    inventoryIssuedDate,        //Inventory issued date
                    inventoryTransactionType,   //Inventoty transaction type
                    billItem.ItemUnitId,        //For item unit
                    debit,                      //Dedit
                    credit,                     //Credit
                    priceAfterPromotion,        //Price after promote
                    billItem.Comment            //Description
                );

                DateTime inventoryLedgerIssuedDate = new DateTime(inventoryIssuedDate.Ticks);
                inventoryLedgerIssuedDate =
                    inventoryLedgerIssuedDate.AddMilliseconds(inventoryLedgerOrder);
                //Update InventoryLedger
                this.CreateAndRefreshInventoryLedger
                (
                    session,
                    inventoryTransaction,       //Inventory transaction
                    forInventoryId,             //Create for inventory
                    inventoryLedgerIssuedDate,  //Inventory issued date
                    inventoryTransactionType,   //Inventoty transaction type
                    billItem.ItemUnitId,        //For item unit
                    debit,                      //Dedit
                    credit,                     //Credit
                    billItem.Comment            //Description
                );
                inventoryLedgerOrder += 10;

            }

        }



        #endregion
        public string ReceiptType
        {
            get;
            set;
        }
        public DateTime CurrentIssueDate
        {
            get;
            set;
        }
        public Guid CurrentItemUnit
        {
            get;
            set;
        }
        public DateTime setTimeSpanIssueDate(DateTime issueDate)
        {
            DateTime currentTime = new DateTime(issueDate.Year, issueDate.Month, issueDate.Day);
            currentTime = currentTime + DateTime.Now.TimeOfDay;
            return currentTime;
        }
        public void CreateInventoryTransaction(Session session, Guid accountingPeriodId, Guid inventoryId,
            XPCollection<BillItem> billItems, string code, string description)
        {
            //using (UnitOfWork uNOW = XpoHelper.GetNewUnitOfWork())
            //{
            InventoryTransaction newInvTransaction = new InventoryTransaction(session);
            ////Insert StockCart
            newInvTransaction.Code = code;
            newInvTransaction.Description = description;
            newInvTransaction.CreateDate = DateTime.Now;
            TimeSpan currentTimeSpan = newInvTransaction.CreateDate.TimeOfDay;
            newInvTransaction.IssueDate = this.CurrentIssueDate + currentTimeSpan;
            if (newInvTransaction.IssueDate.Year > 1900)
            {
                newInvTransaction.IssueDate -= this.CurrentIssueDate.TimeOfDay;
            }
            //newInvTransaction.IssueDate = setTimeSpanIssueDate(transactionTime);
            newInvTransaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            newInvTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            //Get previous InventoryTransaction
            InventoryTransaction previousInvTrs = this.getPreviousInventoryTransaction(session, accountingPeriodId, newInvTransaction.IssueDate);
            if (previousInvTrs != null)
            {
                //newInvTransaction.PreviousInventoryTransactionId = previousInvTrs.InventoryTransactionId;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);
                    ////Insert InventoryLedger
                    this.insertInventoryLedger(session, newInvTransaction, inventory, idxBillItem, true);
                    //Update next InventoryTransaction
                    //InventoryTransaction nextInvTrs = session.GetObjectByKey<InventoryTransaction>(previousInvTrs.InventoryTransactionId);
                    InventoryTransaction nextInvTrs = getNextInventoryTransactionByPreviousId(session, accountingPeriodId, previousInvTrs.InventoryTransactionId, newInvTransaction.InventoryTransactionId);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }
            else
            {
                //newInvTransaction.PreviousInventoryTransactionId = Guid.Empty;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);
                    ////Insert InventoryLedger
                    this.insertInventoryLedger(session, newInvTransaction, inventory, idxBillItem, true);
                    InventoryTransaction nextInvTrs = this.getNextInventoryTransactionByIssueDate(session, accountingPeriodId, newInvTransaction);
                    //this.getNextInventoryTransaction(session, accountingPeriodId, previousInvTrs.InventoryTransactionId);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }
            //}
        }
        //public void CreateInventoryTransactionBalanceForward(Session session, Guid accountingPeriodId, DateTime transactionTime, Guid inventoryId, Guid itemUnitId,
        //                                                    string code, string description, double balance, double price)
        //{
        //    //if (this.checkExitBalanceForward(session, inventoryId, itemUnitId))
        //    //{
        //    //    return;
        //    //}
        //    InventoryTransactionBalanceForward newInvTransaction = new InventoryTransactionBalanceForward(session);
        //    AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
        //    ////Insert StockCart
        //    newInvTransaction.Code = code;
        //    newInvTransaction.Description = description;

        //    newInvTransaction.CreateDate = DateTime.Now;
        //    newInvTransaction.UpdateDate = DateTime.Now;
        //    newInvTransaction.IssueDate = transactionTime;
        //    newInvTransaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
        //    newInvTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
        //    newInvTransaction.Balance = balance;

        //    BillItem idxBillItem = new BillItem(session);
        //    idxBillItem.Quantity = balance;
        //    idxBillItem.ItemUnitId = session.GetObjectByKey<ItemUnit>(itemUnitId);
        //    //Get previous InventoryTransaction
        //    InventoryTransaction previousInvTrs = this.getPreviousInventoryTransaction(session, accountingPeriodId, newInvTransaction.IssueDate);
        //    if (previousInvTrs != null)
        //    {
        //        newInvTransaction.PreviousInventoryTransactionId = previousInvTrs.InventoryTransactionId;
        //        newInvTransaction.Save();
        //        NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

        //        ////Insert InventoryJournal
        //        this.insertInventoryJournalBalanceForward(session, newInvTransaction, inventory, idxBillItem);
        //        ////Insert InventoryLedger
        //        this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, price, true);
        //        //Update next InventoryTransaction
        //        //InventoryTransaction nextInvTrs = session.GetObjectByKey<InventoryTransaction>(previousInvTrs.InventoryTransactionId);
        //        InventoryTransaction nextInvTrs = getNextInventoryTransactionByPreviousId(session, accountingPeriodId, previousInvTrs.InventoryTransactionId, newInvTransaction.InventoryTransactionId);
        //        if (nextInvTrs != null)
        //        {
        //            nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
        //            nextInvTrs.Save();
        //            UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
        //            UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
        //        }
        //    }
        //    else
        //    {
        //        newInvTransaction.PreviousInventoryTransactionId = Guid.Empty;
        //        newInvTransaction.Save();
        //        NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

        //        ////Insert InventoryJournal
        //        this.insertInventoryJournalBalanceForward(session, newInvTransaction, inventory, idxBillItem);
        //        ////Insert InventoryLedger
        //        this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, price, true);
        //        InventoryTransaction nextInvTrs = this.getNextInventoryTransactionByIssueDate(session, accountingPeriodId, newInvTransaction);
        //        //this.getNextInventoryTransaction(session, accountingPeriodId, previousInvTrs.InventoryTransactionId);
        //        if (nextInvTrs != null)
        //        {
        //            nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
        //            nextInvTrs.Save();
        //            UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
        //            UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
        //        }

        //    }
        //}
        public void CreatePurchaseInvoiceInventoryTransaction(Session session, Guid purchaseInvoiceId, Guid accountingPeriodId, DateTime transactionTime, Guid inventoryId,
            XPCollection<BillItem> billItems, string code, string description)
        {
            PurchaseInvoiceInventoryTransaction newInvTransaction = new PurchaseInvoiceInventoryTransaction(session);
            AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            ////Insert StockCart
            newInvTransaction.PurchaseInvoiceId = session.GetObjectByKey<DAL.Invoice.PurchaseInvoice>(purchaseInvoiceId);
            newInvTransaction.Code = code;
            newInvTransaction.Description = description;
            newInvTransaction.CreateDate = DateTime.Now;
            newInvTransaction.UpdateDate = newInvTransaction.CreateDate;
            newInvTransaction.IssueDate = setTimeSpanIssueDate(transactionTime);
            newInvTransaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            newInvTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            //Get previous InventoryTransaction
            /////2013-10-17 ERP-772 Khoa.Truong MOD START
            /////Edit getPreviousInventoryTransaction metnod
            InventoryTransaction previousInvTrs = this.getPreviousInventoryTransaction(session, accountingPeriodId, newInvTransaction.IssueDate);
            /////2013-10-17 ERP-772 Khoa.Truong MOD END
            if (previousInvTrs != null)
            {
                //newInvTransaction.PreviousInventoryTransactionId = previousInvTrs.InventoryTransactionId;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);

                    ////Insert InventoryLedger
                    /////2013-10-17 ERP-772 Khoa.Truong MOD START
                    /////Edit insertInventoryLedgerCOGS method
                    this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, idxBillItem.Price, true);
                    /////2013-10-17 ERP-772 Khoa.Truong MOD END

                    //Update next InventoryTransaction
                    //InventoryTransaction nextInvTrs = session.GetObjectByKey<InventoryTransaction>(previousInvTrs.InventoryTransactionId);
                    /////2013-10-17 ERP-772 Khoa.Truong WARN START
                    /////getNextInventoryTransactionByPreviousId is called in foreach
                    InventoryTransaction nextInvTrs = getNextInventoryTransactionByPreviousId(session, accountingPeriodId, previousInvTrs.InventoryTransactionId, newInvTransaction.InventoryTransactionId);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        /////2013-10-17 ERP-772 Khoa.Truong WARN END
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }
            else
            {
                //newInvTransaction.PreviousInventoryTransactionId = Guid.Empty;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);
                    ////Insert InventoryLedger
                    /////2013-10-17 ERP-772 Khoa.Truong MOD START
                    /////Edit insertInventoryLedgerCOGS method
                    this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, idxBillItem.Price, true);
                    /////2013-10-17 ERP-772 Khoa.Truong MOD END

                    //Update next InventoryTransaction
                    /////2013-10-17 ERP-772 Khoa.Truong WARN START
                    /////getNextInventoryTransactionByPreviousId is called in foreach
                    InventoryTransaction nextInvTrs = this.getNextInventoryTransactionByIssueDate(session, accountingPeriodId, newInvTransaction);
                    //this.getNextInventoryTransaction(session, accountingPeriodId, previousInvTrs.InventoryTransactionId);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        /////2013-10-17 ERP-772 Khoa.Truong WARN END
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }
        }
        public void CreateSalesInvoiceInventoryTransaction(Session session, Guid salesInvoiceId, Guid accountingPeriodId, DateTime transactionTime, Guid inventoryId,
            XPCollection<BillItem> billItems, string code, string description)
        {
            SalesInvoiceInventoryTransaction newInvTransaction = new SalesInvoiceInventoryTransaction(session);
            AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            ////Insert StockCart
            newInvTransaction.SalesInvoiceId = session.GetObjectByKey<SalesInvoice>(salesInvoiceId);
            newInvTransaction.Code = code;
            newInvTransaction.Description = description;

            newInvTransaction.CreateDate = DateTime.Now;
            newInvTransaction.IssueDate = setTimeSpanIssueDate(transactionTime);
            newInvTransaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            newInvTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;

            //Get previous InventoryTransaction
            InventoryTransaction previousInvTrs = this.getPreviousInventoryTransaction(session, accountingPeriodId, newInvTransaction.IssueDate);
            if (previousInvTrs != null)
            {
                //newInvTransaction.PreviousInventoryTransactionId = previousInvTrs.InventoryTransactionId;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);
                    ////Insert InventoryLedger
                    this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, idxBillItem.Price, true);
                    //Update next InventoryTransaction
                    //InventoryTransaction nextInvTrs = session.GetObjectByKey<InventoryTransaction>(previousInvTrs.InventoryTransactionId);

                    InventoryTransaction nextInvTrs = getNextInventoryTransactionByPreviousId(session, accountingPeriodId, previousInvTrs.InventoryTransactionId, newInvTransaction.InventoryTransactionId);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }
            else
            {
                //newInvTransaction.PreviousInventoryTransactionId = Guid.Empty;
                newInvTransaction.Save();
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);

                foreach (BillItem idxBillItem in billItems)
                {
                    ////Insert InventoryJournal
                    this.insertInventoryJournal(session, newInvTransaction, inventory, idxBillItem);
                    ////Insert InventoryLedger
                    this.insertInventoryLedgerCOGS(session, newInvTransaction, inventory, idxBillItem, idxBillItem.Price, true);
                    //Update next InventoryTransaction
                    InventoryTransaction nextInvTrs = this.getNextInventoryTransactionByIssueDate(session, accountingPeriodId, newInvTransaction);
                    if (nextInvTrs != null)
                    {
                        //nextInvTrs.PreviousInventoryTransactionId = newInvTransaction.InventoryTransactionId;
                        nextInvTrs.Save();
                        UpdateNextInventoryLedger(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                        UpdateNextCOGS(session, newInvTransaction.IssueDate, inventoryId, idxBillItem.ItemUnitId.ItemUnitId);
                    }
                }
            }

        }
        public void CreateBalanceItemChildren(Session session, InventoryTransaction newInvTransaction, NAS.DAL.Nomenclature.Inventory.Inventory inventory,
                                                Guid parentItemUnitId, double deltaBalance)
        {
            XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
            List<ItemUnit> listChildrenItemUnit = ItemUnitQuery.Where(r => r.ParentItemUnitId.ItemUnitId == parentItemUnitId).ToList();
            foreach (ItemUnit childrenItemUnit in listChildrenItemUnit)
            {
                if (childrenItemUnit.ItemUnitId.Equals(this.CurrentItemUnit))
                {
                    continue;
                }
                InventoryLedger previousInventoryLedger = this.getPreviousInventoryLedger(session, newInvTransaction.IssueDate, inventory.InventoryId, childrenItemUnit.ItemUnitId);
                InventoryLedger newInventoryLedger = new InventoryLedger(session);
                double deltaBalanceChildren = deltaBalance * childrenItemUnit.NumRequired;
                if (previousInventoryLedger == null)
                {
                    newInventoryLedger.Balance = deltaBalanceChildren;
                }
                else
                {
                    newInventoryLedger.Balance = deltaBalanceChildren + previousInventoryLedger.Balance;
                }
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    newInventoryLedger.Credit = 0;
                    newInventoryLedger.Debit = deltaBalanceChildren;
                }
                else
                {
                    newInventoryLedger.Credit = deltaBalanceChildren;
                    newInventoryLedger.Debit = 0;
                }
                newInventoryLedger.CreateDate = newInvTransaction.IssueDate;
                newInventoryLedger.InventoryTransactionId = newInvTransaction;
                newInventoryLedger.ItemUnitId = childrenItemUnit;
                newInventoryLedger.InventoryId = inventory;
                newInventoryLedger.Description = newInvTransaction.Description;
                newInventoryLedger.IsOriginal = false;
                newInventoryLedger.Save();
                CreateBalanceItemChildren(session, newInvTransaction, inventory, childrenItemUnit.ItemUnitId, deltaBalanceChildren);
            }
        }
        public void CreateBalanceItemChildrenCOGS(Session session, InventoryTransaction newInvTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory, Guid parentItemUnitId, double deltaBalance, double price)
        {
            XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
            List<ItemUnit> listChildrenItemUnit = ItemUnitQuery.Where(r => r.ParentItemUnitId.ItemUnitId == parentItemUnitId).ToList();
            foreach (ItemUnit childrenItemUnit in listChildrenItemUnit)
            {
                if (childrenItemUnit.ItemUnitId.Equals(this.CurrentItemUnit))
                {
                    continue;
                }
                COGS lastInventoryCOGS = this.getPreviousCOGS(session, newInvTransaction.IssueDate, inventory.InventoryId, childrenItemUnit.ItemUnitId);

                //COGS lastInventoryCOGS = this.getLastestCOGS(session, childrenItemUnit.ItemUnitId);
                COGS newInventoryCOGS = new COGS(session);
                double deltaBalanceChildren = deltaBalance * childrenItemUnit.NumRequired;
                if (lastInventoryCOGS == null)
                {
                    newInventoryCOGS.Balance = deltaBalanceChildren;
                }
                else
                {
                    newInventoryCOGS.Balance = deltaBalanceChildren + lastInventoryCOGS.Balance;
                }
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    newInventoryCOGS.Credit = 0;
                    newInventoryCOGS.Debit = deltaBalanceChildren;
                }
                else
                {
                    newInventoryCOGS.Credit = deltaBalanceChildren < 0 ? -deltaBalanceChildren : deltaBalanceChildren;
                    newInventoryCOGS.Debit = 0;
                }

                /////2013-10-17 ERP-772 Khoa.Truong DEL START
                //if (lastInventoryCOGS == null)
                //{
                //    newInventoryCOGS.Balance = deltaBalanceChildren;
                //    //newInventoryCOGS.Price = price / deltaBalanceChildren;
                //    //newInventoryCOGS.Amount = 0;
                //    //newInventoryCOGS.Total = newInventoryCOGS.Price * newInventoryCOGS.Balance;
                //    //newInventoryCOGS.COGSPrice = newInventoryCOGS.Price;
                //}
                //else
                //{
                //    newInventoryCOGS.Balance = lastInventoryCOGS.Balance + deltaBalanceChildren;
                //    //if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                //    //{
                //    //    newInventoryCOGS.Price = lastInventoryCOGS.Price;
                //    //}
                //    //else
                //    //{
                //    //    newInventoryCOGS.Price = lastInventoryCOGS.COGSPrice;
                //    //}
                //    //newInventoryCOGS.Amount = deltaBalanceChildren * newInventoryCOGS.Price;
                //    //newInventoryCOGS.Total = newInventoryCOGS.Amount + lastInventoryCOGS.Total;
                //    //newInventoryCOGS.COGSPrice = newInventoryCOGS.Total / newInventoryCOGS.Balance;
                //}
                /////2013-10-17 ERP-772 Khoa.Truong DEL END

                newInventoryCOGS.IssueDate = newInvTransaction.IssueDate;
                newInventoryCOGS.CreateDate = DateTime.Now;
                newInventoryCOGS.UpdateDate = newInventoryCOGS.CreateDate;
                newInventoryCOGS.InventoryTransactionId = newInvTransaction;
                newInventoryCOGS.ItemUnitId = childrenItemUnit;
                newInventoryCOGS.InventoryId = inventory;
                newInventoryCOGS.Description = newInvTransaction.Description;
                newInventoryCOGS.IsOriginal = false;
                newInventoryCOGS.Save();
                CreateBalanceItemChildrenCOGS(session, newInvTransaction, inventory, childrenItemUnit.ItemUnitId, deltaBalanceChildren, newInventoryCOGS.Price);
            }
        }
        public void CreateBalanceItemParent(Session session, InventoryTransaction newInvTransaction, NAS.DAL.Nomenclature.Inventory.Inventory inventory,
                                            Guid itemUnitId, double deltaBalance)
        {
            XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
            ItemUnit currentItemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);
            ItemUnit parentItemUnit = currentItemUnit.ParentItemUnitId;
            //List<ItemUnit> listParentItemUnit = ItemUnitQuery.Where(r => r.ItemUnitId == currentItemUnit.ParentItemUnitId.ItemUnitId).ToList();
            if (parentItemUnit != null)
            {
                InventoryLedger previousInventoryLedger = this.getPreviousInventoryLedger(session, newInvTransaction.IssueDate, inventory.InventoryId, parentItemUnit.ItemUnitId);
                InventoryLedger newInventoryLedger = new InventoryLedger(session);
                double deltaBalanceParent = deltaBalance / parentItemUnit.NumRequired;
                if (previousInventoryLedger == null)
                {
                    newInventoryLedger.Balance = deltaBalanceParent;
                }
                else
                {
                    newInventoryLedger.Balance = deltaBalanceParent + previousInventoryLedger.Balance;
                }
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    newInventoryLedger.Credit = 0;
                    newInventoryLedger.Debit = deltaBalanceParent;
                }
                else
                {
                    newInventoryLedger.Credit = deltaBalanceParent;
                    newInventoryLedger.Debit = 0;
                }
                newInventoryLedger.CreateDate = newInvTransaction.IssueDate;
                newInventoryLedger.InventoryTransactionId = newInvTransaction;
                newInventoryLedger.InventoryId = inventory;
                newInventoryLedger.ItemUnitId = parentItemUnit;
                newInventoryLedger.Description = newInvTransaction.Description;
                newInventoryLedger.IsOriginal = false;
                newInventoryLedger.Save();
                //UpdateBalanceItemChildren(session, inventoryJournal, parentItemUnit.ItemUnitId, deltaBalanceParent, isBalance);
                CreateBalanceItemParent(session, newInvTransaction, inventory, parentItemUnit.ItemUnitId, deltaBalanceParent);
            }
        }
        public void CreateBalanceItemParentCOGS(Session session, InventoryTransaction newInvTransaction, NAS.DAL.Nomenclature.Inventory.Inventory inventory,
            Guid itemUnitId, double deltaBalance, double price)
        {
            XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
            ItemUnit currentItemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);
            ItemUnit parentItemUnit = currentItemUnit.ParentItemUnitId;
            //List<ItemUnit> listParentItemUnit = ItemUnitQuery.Where(r => r.ItemUnitId == currentItemUnit.ParentItemUnitId.ItemUnitId).ToList();
            if (parentItemUnit != null)
            {
                COGS lastInventoryCOGS = this.getPreviousCOGS(session, newInvTransaction.IssueDate, inventory.InventoryId, parentItemUnit.ItemUnitId);
                //COGS lastInventoryCOGS = this.getLastestCOGS(session, parentItemUnit.ItemUnitId);
                COGS newInventoryCOGS = new COGS(session);
                double deltaBalanceParent = deltaBalance / currentItemUnit.NumRequired;
                if (lastInventoryCOGS == null)
                {
                    newInventoryCOGS.Balance = deltaBalanceParent;
                }
                else
                {
                    newInventoryCOGS.Balance = deltaBalanceParent + lastInventoryCOGS.Balance;
                }
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    newInventoryCOGS.Credit = 0;
                    newInventoryCOGS.Debit = deltaBalanceParent;
                }
                else
                {
                    newInventoryCOGS.Credit = deltaBalanceParent < 0 ? -deltaBalanceParent : deltaBalanceParent;
                    newInventoryCOGS.Debit = 0;
                }

                /////2013-10-17 ERP-772 Khoa.Truong DEL START
                //if (lastInventoryCOGS == null)
                //{
                //    newInventoryCOGS.Balance = deltaBalanceParent;
                //    //newInventoryCOGS.Price = price / deltaBalanceParent;
                //    //newInventoryCOGS.Amount = 0;
                //    //newInventoryCOGS.Total = newInventoryCOGS.Price * newInventoryCOGS.Balance;
                //    //newInventoryCOGS.COGSPrice = newInventoryCOGS.Price;
                //}
                //else
                //{
                //    newInventoryCOGS.Balance = lastInventoryCOGS.Balance + deltaBalanceParent;
                //    //if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                //    //{
                //    //    newInventoryCOGS.Price = lastInventoryCOGS.Price;
                //    //}
                //    //else
                //    //{
                //    //    newInventoryCOGS.Price = lastInventoryCOGS.COGSPrice;
                //    //}
                //    //newInventoryCOGS.Amount = deltaBalanceParent * newInventoryCOGS.Price;
                //    //newInventoryCOGS.Total = newInventoryCOGS.Amount + lastInventoryCOGS.Total;
                //    //newInventoryCOGS.COGSPrice = newInventoryCOGS.Total / newInventoryCOGS.Balance;
                //}
                /////2013-10-17 ERP-772 Khoa.Truong DEL END

                newInventoryCOGS.IssueDate = newInvTransaction.IssueDate;
                newInventoryCOGS.CreateDate = DateTime.Now;
                newInventoryCOGS.UpdateDate = newInventoryCOGS.CreateDate;
                newInventoryCOGS.InventoryTransactionId = newInvTransaction;
                newInventoryCOGS.InventoryId = inventory;
                newInventoryCOGS.ItemUnitId = parentItemUnit;
                newInventoryCOGS.Description = newInvTransaction.Description;
                newInventoryCOGS.IsOriginal = false;
                newInventoryCOGS.Save();
                //UpdateBalanceItemChildren(session, inventoryJournal, parentItemUnit.ItemUnitId, deltaBalanceParent, isBalance);
                CreateBalanceItemParentCOGS(session, newInvTransaction, inventory, parentItemUnit.ItemUnitId, deltaBalanceParent, newInventoryCOGS.Price);
            }
        }
        public void insertInventoryLedgerCOGS(Session session, InventoryTransaction newInvTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory, BillItem billItem, double price, bool isOriginal)
        {
            try
            {
                //InventoryTransaction preTrS = session.GetObjectByKey<InventoryTransaction>(newInvTransaction.PreviousInventoryTransactionId);
                /////2013-10-17 ERP-772 Khoa.Truong MOD START
                /////Edit method getPreviousCOGS
                COGS previousInvLedger = this.getPreviousCOGS(session, newInvTransaction.IssueDate, inventory.InventoryId, billItem.ItemUnitId.ItemUnitId);
                /////2013-10-17 ERP-772 Khoa.Truong MOD END
                COGS cogs = new COGS(session);
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    cogs.Credit = 0;
                    cogs.Debit = billItem.Quantity;
                }
                else
                {
                    cogs.Credit = billItem.Quantity;
                    cogs.Debit = 0;
                }
                double deltaBalance = cogs.Debit - cogs.Credit;
                if (previousInvLedger == null)
                {
                    cogs.Balance = cogs.Debit - cogs.Credit;
                    cogs.Price = price;
                    cogs.Amount = price * (cogs.Debit - cogs.Credit);
                    cogs.Total = cogs.Amount;
                    if (cogs.Balance == 0)
                    {
                        cogs.COGSPrice = cogs.Price;
                    }
                    else
                    {
                        cogs.COGSPrice = cogs.Total / cogs.Balance;
                    }
                }
                else
                {
                    cogs.Balance += (cogs.Debit - cogs.Credit) + previousInvLedger.Balance;
                    if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                    {
                        cogs.Price = price;
                    }
                    else
                    {
                        cogs.Price = previousInvLedger.COGSPrice;

                    }
                    cogs.Amount = cogs.Price * (cogs.Debit - cogs.Credit);

                    /////2013-10-17 ERP-772 Khoa.Truong MOD START
                    /////Edit method getFirstPreviousCOGSTotalOriginal
                    double previousTotal = getFirstPreviousCOGSTotalOriginal(session, newInvTransaction.IssueDate, inventory.InventoryId, billItem.ItemUnitId.ItemUnitId);
                    /////2013-10-17 ERP-772 Khoa.Truong MOD START

                    //cogs.Total = cogs.Amount + previousInvLedger.Total;
                    cogs.Total = cogs.Amount + previousTotal;
                    if (cogs.Balance == 0)
                    {
                        cogs.COGSPrice = cogs.Price;
                    }
                    else
                    {
                        /////2013-10-17 ERP-772 Khoa.Truong MOD START
                        /////Edit getPreviousCOGSBalanceOriginal method
                        double previousBalance = getPreviousCOGSBalanceOriginal(session, newInvTransaction.IssueDate, inventory.InventoryId, billItem.ItemUnitId.ItemUnitId);
                        /////2013-10-17 ERP-772 Khoa.Truong MOD END

                        double amount = previousBalance + (cogs.Debit - cogs.Credit);
                        amount = amount == 0 ? 1 : amount;
                        //cogs.COGSPrice = cogs.Total / cogs.Balance;
                        cogs.COGSPrice = cogs.Total / amount;
                    }

                }

                cogs.InventoryTransactionId = newInvTransaction;
                cogs.InventoryId = inventory;
                cogs.ItemUnitId = billItem.ItemUnitId;
                cogs.Description = newInvTransaction.Description;
                cogs.CreateDate = DateTime.Now;
                cogs.UpdateDate = cogs.CreateDate;
                cogs.IssueDate = newInvTransaction.IssueDate;
                cogs.IsOriginal = isOriginal;
                cogs.Save();
                this.CurrentItemUnit = billItem.ItemUnitId.ItemUnitId;
                //viewing
                CreateBalanceItemChildrenCOGS(session, newInvTransaction, inventory, billItem.ItemUnitId.ItemUnitId, deltaBalance, cogs.Price);
                CreateBalanceItemParentCOGS(session, newInvTransaction, inventory, billItem.ItemUnitId.ItemUnitId, deltaBalance, cogs.Price);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void insertInventoryJournalBalanceForward(Session session, InventoryTransactionBalanceForward inventoryTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory, BillItem billItem)
        {
            try
            {
                InventoryJournalBalanceForward invJournal = new InventoryJournalBalanceForward(session);
                invJournal.InventoryTransactionId = inventoryTransaction;
                invJournal.InventoryId = inventory;
                invJournal.ItemUnitId = billItem.ItemUnitId;
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    invJournal.Credit = 0;
                    invJournal.Debit = billItem.Quantity;
                }
                else
                {
                    invJournal.Credit = billItem.Quantity;
                    invJournal.Debit = 0;
                }
                invJournal.Balance = invJournal.Debit - invJournal.Credit;
                //invJournal.CreateDate = inventoryTransaction.IssueDate;
                invJournal.Description = inventoryTransaction.Description;
                invJournal.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void insertInventoryLedger(Session session, InventoryTransaction newInvTransaction, NAS.DAL.Nomenclature.Inventory.Inventory inventory,
                                                                               BillItem billItem, bool isOriginal)
        {
            try
            {
                //InventoryTransaction preTrS = session.GetObjectByKey<InventoryTransaction>(newInvTransaction.PreviousInventoryTransactionId);
                //InventoryLedger previousInvLedger = this.getPreviousInventoryLedger(session, preTrS.IssueDate, inventory.InventoryId, billItem.ItemUnitId.ItemUnitId);
                InventoryLedger newInvLedger = new InventoryLedger(session);
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    newInvLedger.Credit = 0;
                    newInvLedger.Debit = billItem.Quantity;
                }
                else
                {
                    newInvLedger.Credit = billItem.Quantity;
                    newInvLedger.Debit = 0;
                }
                double deltaBalance = newInvLedger.Debit - newInvLedger.Credit;
                //if (previousInvLedger == null)
                //{
                //    newInvLedger.Balance = newInvLedger.Debit - newInvLedger.Credit;
                //}
                //else
                //{
                //    newInvLedger.Balance += (newInvLedger.Debit - newInvLedger.Credit) + previousInvLedger.Balance;
                //}

                newInvLedger.InventoryTransactionId = newInvTransaction;
                newInvLedger.InventoryId = inventory;
                newInvLedger.ItemUnitId = billItem.ItemUnitId;
                newInvLedger.Description = newInvTransaction.Description;
                newInvLedger.CreateDate = newInvTransaction.IssueDate;
                newInvLedger.IssueDate = newInvTransaction.IssueDate;
                newInvLedger.IsOriginal = isOriginal;
                newInvLedger.Save();
                this.CurrentItemUnit = billItem.ItemUnitId.ItemUnitId;
                CreateBalanceItemChildren(session, newInvTransaction, inventory, billItem.ItemUnitId.ItemUnitId, deltaBalance);
                CreateBalanceItemParent(session, newInvTransaction, inventory, billItem.ItemUnitId.ItemUnitId, deltaBalance);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void insertInventoryJournal(Session session, InventoryTransaction inventoryTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory, BillItem billItem)
        {
            try
            {
                InventoryJournal invJournal = new InventoryJournal(session);
                invJournal.InventoryTransactionId = inventoryTransaction;
                invJournal.InventoryId = inventory;
                invJournal.ItemUnitId = billItem.ItemUnitId;
                if (this.ReceiptType.Equals(Utility.Constant.RECEIPT_PURCHASE))
                {
                    invJournal.Credit = 0;
                    invJournal.Debit = billItem.Quantity;
                }
                else
                {
                    invJournal.Credit = billItem.Quantity;
                    invJournal.Debit = 0;
                }
                //invJournal.CreateDate = inventoryTransaction.IssueDate;
                invJournal.Description = inventoryTransaction.Description;
                invJournal.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void insertInventoryJournal(Session session, InventoryTransaction inventoryTransaction,
            NAS.DAL.Nomenclature.Inventory.Inventory inventory,
            string inventoryTransactionType, ItemUnit itemUnit, double debit, double credit)
        {
            try
            {
                InventoryJournal invJournal = new InventoryJournal(session);
                invJournal.InventoryTransactionId = inventoryTransaction;
                invJournal.InventoryId = inventory;
                invJournal.ItemUnitId = itemUnit;
                if (inventoryTransactionType.Equals("INPUT"))
                {
                    invJournal.Credit = 0;
                    invJournal.Debit = debit;
                }
                else if (inventoryTransactionType.Equals("OUTPUT"))
                {
                    invJournal.Credit = credit;
                    invJournal.Debit = 0;
                }
                //invJournal.CreateDate = inventoryTransaction.IssueDate;
                invJournal.Description = inventoryTransaction.Description;
                invJournal.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public void UpdateBalanceItemChildren(Session session, DateTime currentDate, Guid inventoryId,
                                                Guid parentItemUnitId, double deltaBalance)
        {
            XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
            List<ItemUnit> listChildrenItemUnit = ItemUnitQuery.Where(r => r.ParentItemUnitId.ItemUnitId == parentItemUnitId).ToList();
            foreach (ItemUnit childrenItemUnit in listChildrenItemUnit)
            {
                if (childrenItemUnit.ItemUnitId.Equals(this.CurrentItemUnit))
                {
                    continue;
                }
                InventoryLedger nextInventoryLedger = this.getNextInventoryLedger(session, currentDate, inventoryId, childrenItemUnit.ItemUnitId);
                if (nextInventoryLedger == null)
                {
                    return;
                }
                InventoryLedger previousInventoryLedger = this.getPreviousInventoryLedger(session, nextInventoryLedger.CreateDate, inventoryId, childrenItemUnit.ItemUnitId);
                double deltaBalanceChildren = deltaBalance * childrenItemUnit.NumRequired;
                nextInventoryLedger.Balance = (nextInventoryLedger.Debit - nextInventoryLedger.Credit) + previousInventoryLedger.Balance;
                nextInventoryLedger.Save();
                UpdateBalanceItemChildren(session, currentDate, inventoryId, childrenItemUnit.ItemUnitId, deltaBalanceChildren);
            }
        }
        public void UpdateBalanceItemParent(Session session, DateTime currentDate, Guid inventoryId,
                                                Guid parentItemUnitId, double deltaBalance)
        {
            ItemUnit currentItemUnit = session.GetObjectByKey<ItemUnit>(parentItemUnitId);
            ItemUnit parentItemUnit = currentItemUnit.ParentItemUnitId;
            //foreach (ItemUnit childrenItemUnit in listChildrenItemUnit)
            if (parentItemUnit != null)
            {
                InventoryLedger nextInventoryLedger = this.getNextInventoryLedger(session, currentDate, inventoryId, parentItemUnit.ItemUnitId);
                if (nextInventoryLedger == null)
                {
                    return;
                }
                InventoryLedger previousInventoryLedger = this.getPreviousInventoryLedger(session, nextInventoryLedger.CreateDate, inventoryId, parentItemUnit.ItemUnitId);
                double deltaBalanceChildren = deltaBalance / currentItemUnit.NumRequired;
                nextInventoryLedger.Balance = (nextInventoryLedger.Debit - nextInventoryLedger.Credit) + previousInventoryLedger.Balance;
                nextInventoryLedger.Save();
                UpdateBalanceItemParent(session, currentDate, inventoryId, parentItemUnit.ItemUnitId, deltaBalanceChildren);
            }
        }
        public void UpdateNextInventoryLedger(Session session, DateTime currentDateTime, Guid inventoryId, Guid itemUnitId)
        {
            /////2013-10-17 ERP-772 Khoa.Truong DEL START
            InventoryLedger currentLedger = this.getInventoryLedgerByIssueDate(session, currentDateTime, inventoryId, itemUnitId);
            InventoryLedger nextInvLedger = this.getNextInventoryLedger(session, currentDateTime, inventoryId, itemUnitId);
            if (nextInvLedger == null)
            {
                return;
            }
            double delPreviousBalance = currentLedger.Debit - currentLedger.Credit;
            nextInvLedger.Balance = currentLedger.Balance + (nextInvLedger.Debit - nextInvLedger.Credit);
            nextInvLedger.Save();
            this.UpdateBalanceItemChildren(session, currentLedger.CreateDate, inventoryId, itemUnitId, delPreviousBalance);
            this.UpdateBalanceItemParent(session, currentLedger.CreateDate, inventoryId, itemUnitId, delPreviousBalance);
            UpdateNextInventoryLedger(session, nextInvLedger.CreateDate, inventoryId, itemUnitId);
            /////2013-10-17 ERP-772 Khoa.Truong DEL END

            /////2013-10-17 ERP-772 Khoa.Truong INS START

            /////2013-10-17 ERP-772 Khoa.Truong INS END

        }
        public void UpdateNextCOGS(Session session, DateTime currentDateTime, Guid inventoryId, Guid itemUnitId)
        {
            COGS currentLedger = this.getCOGSByIssueDate(session, currentDateTime, inventoryId, itemUnitId);
            COGS nextInvLedger = this.getNextCOGS(session, currentDateTime, inventoryId, itemUnitId);
            if (nextInvLedger == null || nextInvLedger.IsOriginal == false)
            {
                return;
            }
            if ((nextInvLedger.Debit - nextInvLedger.Credit) < 0)
            {
                nextInvLedger.Price = currentLedger.COGSPrice;
                nextInvLedger.Amount = (nextInvLedger.Debit - nextInvLedger.Credit) * nextInvLedger.Price;
                nextInvLedger.Total = nextInvLedger.Amount + currentLedger.Total;
            }
            else
            {
                double previousTotal = getFirstPreviousCOGSTotalOriginal(session, nextInvLedger.CreateDate, inventoryId, itemUnitId);
                nextInvLedger.Total = nextInvLedger.Amount + previousTotal;
            }
            if (nextInvLedger.Balance == 0)
            {
                nextInvLedger.COGSPrice = nextInvLedger.Price;
            }
            else
            {
                nextInvLedger.COGSPrice = nextInvLedger.Total / nextInvLedger.Balance;
            }
            nextInvLedger.Save();
            UpdateNextCOGS(session, nextInvLedger.CreateDate, inventoryId, itemUnitId);
        }
        public double getSaleInvoiceInventoryTransactionTotal(Session session, Guid transactionId)
        {            
            double result = 0;
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                var cogsTotalList = (from c in COGSQuery
                                     where c.InventoryTransactionId.InventoryTransactionId == transactionId
                                     select new { total = c.COGSPrice * c.Credit });

                if (cogsTotalList.Count() == 0) return result;

                foreach (var o in cogsTotalList)
                {
                    result += o.total;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public double getPurchseInvoiceInventoryTransactionTotal(Session session, Guid transactionId)
        {
            double result = 0;
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                var cogsTotalList = (from c in COGSQuery
                                     where c.InventoryTransactionId.InventoryTransactionId == transactionId
                                     select new { total = c.Price * c.Debit });

                if (cogsTotalList.Count() == 0) return result;

                foreach (var o in cogsTotalList)
                {
                    result += o.total;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public InventoryTransaction getNextInventoryTransactionByPreviousId(Session session, Guid currentAccPeriodId, Guid previousId, Guid newInvTsId)
        {
            try
            {
                XPQuery<InventoryTransaction> xpqInvTransaction = session.Query<InventoryTransaction>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInvTransaction
                           where c.AccountingPeriodId.AccountingPeriodId == currentAccPeriodId
                            //&& c.PreviousInventoryTransactionId == previousId
                            && c.InventoryTransactionId != newInvTsId
                            //&& c.PreviousInventoryTransactionId != Guid.Empty
                            && c.RowStatus > 0
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return null;
            }
            finally
            {
            }
        }
        public InventoryTransaction getNextInventoryTransactionByIssueDate(Session session, Guid currentAccPeriodId, InventoryTransaction invTransaction)
        {
            try
            {
                XPQuery<InventoryTransaction> xpqInvTransaction = session.Query<InventoryTransaction>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInvTransaction
                           where c.AccountingPeriodId.AccountingPeriodId == currentAccPeriodId
                           && c.IssueDate >= invTransaction.IssueDate
                           //&& c.PreviousInventoryTransactionId == Guid.Empty
                           && c.InventoryTransactionId != invTransaction.InventoryTransactionId
                           && c.RowStatus > 0
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return null;
            }
            finally
            {
            }
        }
        public InventoryTransaction getNextInventoryTransaction(Session session, Guid currentAccPeriodId, Guid previousId)
        {
            try
            {
                XPQuery<InventoryTransaction> xpqInvTransaction = session.Query<InventoryTransaction>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInvTransaction
                           where c.AccountingPeriodId.AccountingPeriodId == currentAccPeriodId
                                //&& c.PreviousInventoryTransactionId == previousId
                                && c.RowStatus > 0
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return null;
            }
            finally
            {
            }
        }
        public InventoryTransaction getPreviousInventoryTransaction(Session session, Guid currentAccPeriodId, DateTime issueDate)
        {
            try
            {
                XPQuery<InventoryTransaction> xpqInvTransaction = session.Query<InventoryTransaction>();
                //get InventoryLedger top to sum balance
                /////2013-10-17 ERP-772 Khoa.Truong MOD START
                //var list = from c in xpqInvTransaction
                //           where c.IssueDate <= issueDate
                //           && c.AccountingPeriodId.AccountingPeriodId == currentAccPeriodId
                //           && c.RowStatus > 0
                //           orderby c.IssueDate descending
                //           select c;
                /////2013-10-17 ERP-772 Khoa.Truong MOD END

                /////2013-10-17 ERP-772 Khoa.Truong INS START
                return (from c in xpqInvTransaction
                        where c.IssueDate < issueDate
                        && c.AccountingPeriodId.AccountingPeriodId == currentAccPeriodId
                        && c.RowStatus > 0
                        orderby c.IssueDate descending
                        select c).FirstOrDefault();
                /////2013-10-17 ERP-772 Khoa.Truong INS END
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return null;
            }
            finally
            {
            }
        }
        public InventoryLedger getInventoryLedgerByIssueDate(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<InventoryLedger> xpqInventoryLedger = session.Query<InventoryLedger>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInventoryLedger
                           where c.ItemUnitId.ItemUnitId == itemUnitId
                           && c.InventoryId.InventoryId == inventoryId
                           && c.CreateDate == currentCreateTime
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public COGS getCOGSByIssueDate(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<COGS> xpqInventoryLedger = session.Query<COGS>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInventoryLedger
                           where c.ItemUnitId.ItemUnitId == itemUnitId
                           && c.InventoryId.InventoryId == inventoryId
                           && c.CreateDate == currentCreateTime
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public InventoryLedger getNextInventoryLedger(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<InventoryLedger> xpqInventoryLedger = session.Query<InventoryLedger>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInventoryLedger
                           where c.ItemUnitId.ItemUnitId == itemUnitId
                           && c.InventoryId.InventoryId == inventoryId
                           && c.CreateDate > currentCreateTime
                           orderby c.CreateDate ascending
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public COGS getNextCOGS(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<COGS> xpqInventoryLedger = session.Query<COGS>();
                //get InventoryLedger top to sum balance
                var list = from c in xpqInventoryLedger
                           where c.ItemUnitId.ItemUnitId == itemUnitId
                           && c.InventoryId.InventoryId == inventoryId
                           && c.CreateDate > currentCreateTime
                           orderby c.CreateDate ascending
                           select c;
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        //public InventoryLedger getPreviousInventoryLedger(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        //{
        //    try
        //    {
        //        XPQuery<InventoryLedger> COGSQuery = session.Query<InventoryLedger>();
        //        //get InventoryLedger top to sum balance
        //        var list = from c in COGSQuery
        //                   where c.ItemUnitId.ItemUnitId == itemUnitId
        //                   && c.InventoryId.InventoryId == inventoryId
        //                   && c.CreateDate < currentCreateTime
        //                   orderby c.CreateDate descending
        //                   select c;
        //        return list.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //    }
        //}
        public COGS getPreviousCOGS(Session session, DateTime issueDate, Guid inventoryId, Guid itemUnitId)
        {
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                //get InventoryLedger top to sum balance

                /////2013-10-17 ERP-772 Khoa.Truong DEL START
                //var list = from c in COGSQuery
                //           where c.ItemUnitId.ItemUnitId == itemUnitId 
                //           && c.InventoryId.InventoryId == inventoryId 
                //           && c.CreateDate < currentCreateTime
                //           orderby c.CreateDate descending 
                //           select c;
                /////2013-10-17 ERP-772 Khoa.Truong DEL END

                /////2013-10-17 ERP-772 Khoa.Truong INS START
                /////Get previous COGS base Issued date
                return (from c in COGSQuery
                        where c.ItemUnitId.ItemUnitId == itemUnitId
                        && c.InventoryId.InventoryId == inventoryId
                        && c.IssueDate < issueDate
                        orderby
                            c.IssueDate descending,
                            c.UpdateDate descending,
                            c.CreateDate descending
                        select c).FirstOrDefault();
                /////2013-10-17 ERP-772 Khoa.Truong INS END
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public double getFirstPreviousCOGSTotalOriginal(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            double result = 0;
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                //get InventoryLedger top to sum balance
                /////2013-10-17 ERP-772 Khoa.Truong DEL START
                //var list = from c in COGSQuery
                //           where c.ItemUnitId.ItemUnitId == itemUnitId
                //           && c.InventoryId.InventoryId == inventoryId
                //           && c.CreateDate < currentCreateTime
                //           && c.IsOriginal == true
                //           orderby c.CreateDate descending
                //           select c;
                /////2013-10-17 ERP-772 Khoa.Truong DEL END

                /////2013-10-17 ERP-772 Khoa.Truong INS START
                var list = (from c in COGSQuery
                            where c.ItemUnitId.ItemUnitId == itemUnitId
                            && c.InventoryId.InventoryId == inventoryId
                            && c.IssueDate < currentCreateTime
                            && c.IsOriginal == true
                            orderby c.IssueDate descending
                            select c).Take(1);
                /////2013-10-17 ERP-772 Khoa.Truong INS END

                if (list.FirstOrDefault() != null)
                {
                    result += list.FirstOrDefault().Total;
                }

            }
            catch (Exception ex)
            {
                result = 0;
            }
            finally
            {

            }
            return result;
        }
        public double getPreviousCOGSTotalOriginal(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            double result = 0;
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                //get InventoryLedger top to sum balance
                var list = from c in COGSQuery
                           where c.ItemUnitId.ItemUnitId == itemUnitId
                           && c.InventoryId.InventoryId == inventoryId
                           && c.CreateDate < currentCreateTime
                           && c.IsOriginal == true
                           orderby c.CreateDate descending
                           select c;
                foreach (COGS cogsidx in list)
                {
                    result += cogsidx.Total;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            finally
            {

            }
            return result;
        }
        public double getPreviousCOGSBalanceOriginal(Session session, DateTime currentCreateTime, Guid inventoryId, Guid itemUnitId)
        {
            double result = 0;
            try
            {
                XPQuery<COGS> COGSQuery = session.Query<COGS>();
                //get InventoryLedger top to sum balance
                /////2013-10-17 ERP-772 Khoa.Truong DEL START
                //var list = from c in COGSQuery
                //           where c.ItemUnitId.ItemUnitId == itemUnitId
                //           && c.InventoryId.InventoryId == inventoryId
                //           && c.CreateDate < currentCreateTime
                //           && c.IsOriginal == true
                //           orderby c.CreateDate descending
                //           select c;
                /////2013-10-17 ERP-772 Khoa.Truong DEL END

                /////2013-10-17 ERP-772 Khoa.Truong INS START
                var list = (from c in COGSQuery
                            where c.ItemUnitId.ItemUnitId == itemUnitId
                            && c.InventoryId.InventoryId == inventoryId
                            && c.IssueDate < currentCreateTime
                            && c.IsOriginal == true
                            orderby c.IssueDate descending
                            select c).Take(1);
                /////2013-10-17 ERP-772 Khoa.Truong INS END

                if (list.FirstOrDefault() != null)
                {
                    result += list.FirstOrDefault().Balance;
                }
            }
            catch (Exception ex)
            {
                result = 0;
            }
            finally
            {

            }
            return result;
        }
        public List<InventoryBalancePriceBO> getInventoryBanlanceForward(Session session, Guid inventoryId, Guid itemUnitId)
        {
            List<InventoryBalancePriceBO> list = new List<InventoryBalancePriceBO>();
            XPQuery<InventoryJournalBalanceForward> invBFJournalQuery = session.Query<InventoryJournalBalanceForward>();
            XPQuery<InventoryTransactionBalanceForward> invTransactionBalanceForwardQuery = session.Query<InventoryTransactionBalanceForward>();
            XPQuery<COGS> invjQuery = session.Query<COGS>();
            var listObj = from invtbf in invTransactionBalanceForwardQuery
                          join invj in invBFJournalQuery on invtbf equals invj.InventoryTransactionId
                          join cogs in invjQuery on invtbf equals cogs.InventoryTransactionId
                          where (invj.InventoryId.InventoryId == inventoryId && invj.ItemUnitId.ItemUnitId == itemUnitId &&
                                 cogs.InventoryId.InventoryId == inventoryId && cogs.ItemUnitId.ItemUnitId == itemUnitId)
                          select new InventoryBalancePriceBO
                          {
                                InventoryJournalId = invj.InventoryJournalId, 
                                Code = invtbf.Code, 
                                IssueDate = invtbf.IssueDate, 
                                Balance = invtbf.Balance, 
                                Price = cogs.Price 
                          };
            return list = new List<InventoryBalancePriceBO>(listObj.ToList()); 
        }
        public bool checkExitBalanceForward(Session session, Guid inventoryId, Guid itemUnitId, Guid accountPeriod)
        {
            try
            {
                XPQuery<InventoryJournalBalanceForward> invJournalQuery = session.Query<InventoryJournalBalanceForward>();
                XPQuery<InventoryTransactionBalanceForward> invTransactionBalanceForwardQuery = session.Query<InventoryTransactionBalanceForward>();
                var listObj = from invtbf in invTransactionBalanceForwardQuery.Where(i => i.RowStatus >= 1 && i.AccountingPeriodId.AccountingPeriodId == accountPeriod)
                              join invj in invJournalQuery.Where(i => i.RowStatus >= 1) on invtbf equals invj.InventoryTransactionId
                              where (invj.InventoryId.InventoryId == inventoryId && invj.ItemUnitId.ItemUnitId == itemUnitId)
                              select invtbf;

                if (listObj.FirstOrDefault() == null)
                    return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return false;
            }
            finally
            {
            }
            return false;
        }
        public void getInventoryLedgers(Session session,
                                        XPCollection<InventoryJournal> InventoryJournals,
                                        Guid ItemUnitId,
                                        out XPCollection<InventoryLedger> InventoryLedgersInventory)
        {
            try
            {
                //CriteriaOperator criteria1 = new InOperator("InventoryJournalId", InventoryJournals);
                CriteriaOperator criteria2 = new BinaryOperator("ItemUnitId", ItemUnitId);
                //CriteriaOperator criteria = CriteriaOperator.And(criteria1, criteria2);
                InventoryLedgersInventory = new XPCollection<InventoryLedger>(session, criteria2);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static List<Item> getItemsInInventoryJournal(Session session, Guid inventoryId)
        {
            List<Item> rs = null;
            try
            {
                XPQuery<Item> itemQuery = session.Query<Item>();
                XPQuery<ItemUnitRelationType> itemUnitRelationTypeQ = session.Query<ItemUnitRelationType>();
                ItemUnitRelationType unitRelationType = itemUnitRelationTypeQ.Where(r => r.Name == "UNIT").FirstOrDefault();
                XPQuery<InventoryJournal> inventoryJournalQuery = session.Query<InventoryJournal>();
                rs = (from ivj in inventoryJournalQuery
                      where ivj.InventoryId.InventoryId == inventoryId
                      && ivj.ItemUnitId.ItemUnitRelationTypeId == unitRelationType
                      group ivj by ivj.ItemUnitId.ItemId into it
                      select it.Key).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public static XPCollection<ItemUnit> getItemUnitsInInventory(Session session, Guid inventoryId)
        {
            List<Item> rs = null;
            try
            {
                XPQuery<ItemUnit> itemUnitQuery = session.Query<ItemUnit>();
                XPQuery<Item> itemQuery = session.Query<Item>();
                XPQuery<ItemUnitRelationType> itemUnitRelationTypeQ = session.Query<ItemUnitRelationType>();
                ItemUnitRelationType unitRelationType = itemUnitRelationTypeQ.Where(r => r.Name == "UNIT").FirstOrDefault();
                XPQuery<InventoryJournal> inventoryJournalQuery = session.Query<InventoryJournal>();
                rs = (from ivj in inventoryJournalQuery
                      where ivj.InventoryId.InventoryId == inventoryId
                      && ivj.ItemUnitId.ItemUnitRelationTypeId == unitRelationType
                      group ivj by ivj.ItemUnitId.ItemId into it
                      select it.Key).ToList();

                XPCollection<ItemUnit> itemUnits = new XPCollection<ItemUnit>(session);
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                        new BinaryOperator("ItemId.RowStatus", 0, BinaryOperatorType.Greater),
                        new InOperator("ItemId", rs)
                    );

                itemUnits.Criteria = criteria;
                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
                itemUnits.Sorting = sortCollection;
                return itemUnits;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        public List<InitInventoryItemUnitObject> getAllItemUnits(Session session, Guid InventoryId, Guid accountPeriod)
        {
            List<InitInventoryItemUnitObject> list = null;
            try
            {
                AccountingPeriod accPeriod = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                InventoryTransactionBO itbo = new InventoryTransactionBO();
                XPQuery<ItemUnit> itemUnitQuery = session.Query<ItemUnit>();
                XPQuery<Item> itemQuery = session.Query<Item>();
                var rsQ = from iu in itemUnitQuery
                          join i in itemQuery on iu.ItemId equals i
                          where iu.RowStatus > 0 & i.RowStatus > 0
                          orderby iu.ItemId.Code ascending
                          select new InitInventoryItemUnitObject
                    {
                        ItemUnitId = iu.ItemUnitId,
                        ItemCode = i.Code,
                        ItemName = i.Name,
                        UnitName = iu.UnitId.Name,
                        IsComplete = checkExitBalanceForward(session, InventoryId, iu.ItemUnitId, accountPeriod) ? short.Parse("0") : short.Parse("1"),
                        Manufacturer = i.ManufacturerOrgId.Name
                    };
                return list = new List<InitInventoryItemUnitObject>(rsQ.ToList()); 
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        public XPCollection<InventoryTransaction> GetDeliveryPlanningForBill<T>(Session session, Guid billId) where T : Bill
        {
            XPCollection<InventoryTransaction> resultCollection = null;// = new XPCollection<InventoryTransaction>(session);
            try
            {
                if (typeof(T).Equals(typeof(NAS.DAL.Invoice.PurchaseInvoice)))
                {
                    NAS.DAL.Invoice.PurchaseInvoice bill =
                        session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);
                    if (bill == null)
                    {
                        return resultCollection;
                    }
                    CriteriaOperator criteria = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    //CriteriaOperator criteria_1 = new ContainsOperator("InventoryJournals", new BinaryOperator("JournalType", JounalTypeConstant.PLANNING));
                    //CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0);
                    resultCollection = new XPCollection<InventoryTransaction>(session,
                        bill.PurchaseInvoiceInventoryTransactions, criteria);
                }
                else if (typeof(T).Equals(typeof(NAS.DAL.Invoice.SalesInvoice)))
                {
                    NAS.DAL.Invoice.SalesInvoice bill =
                        session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);
                    if (bill == null)
                    {
                        return resultCollection;
                    }
                    CriteriaOperator criteria = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    //CriteriaOperator criteria_1 = new ContainsOperator("InventoryJournals", new BinaryOperator("JournalType", JounalTypeConstant.PLANNING));
                    //CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0);
                    resultCollection = new XPCollection<InventoryTransaction>(session,
                        bill.SalesInvoiceInventoryTransactions, criteria);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultCollection;
        }
        public Guid CreateInventoryTransaction(Session session, Guid billId, DateTime _IssueDate, string _Name, string _Description)
        {            
            Bill _bill = session.GetObjectByKey<Bill>(billId);            
            Guid result = Guid.Empty;
            if (_bill == null) return result;
            try
            {
                if (_bill is NAS.DAL.Invoice.PurchaseInvoice)
                {
                    PurchaseInvoiceInventoryTransaction transaction = new PurchaseInvoiceInventoryTransaction(session);
                    transaction.PurchaseInvoiceId = _bill as NAS.DAL.Invoice.PurchaseInvoice;
                    transaction.IssueDate = _IssueDate;
                    transaction.CreateDate = DateTime.Now;
                    transaction.UpdateDate = DateTime.Now;
                    transaction.Code = _Name;
                    transaction.Description = _Description;
                    transaction.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    transaction.Save();
                    result = transaction.InventoryTransactionId;
                }
                if (_bill is SalesInvoice)
                {
                    SalesInvoiceInventoryTransaction transaction = new SalesInvoiceInventoryTransaction(session);
                    transaction.SalesInvoiceId = _bill as SalesInvoice;
                    transaction.IssueDate = _IssueDate;
                    transaction.CreateDate = DateTime.Now;
                    transaction.UpdateDate = DateTime.Now;
                    transaction.Code = _Name;
                    transaction.Description = _Description;
                    transaction.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    transaction.Save();
                    result = transaction.InventoryTransactionId;
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public void UpdateInventoryTransaction(Session session, Guid transactionId, DateTime _IssueDate, string _Name, string _Description)
        {
            try
            {
                PurchaseInvoiceInventoryTransaction transaction = session.GetObjectByKey<PurchaseInvoiceInventoryTransaction>(transactionId);             
                transaction.IssueDate = _IssueDate;
                transaction.CreateDate = DateTime.Now;
                transaction.UpdateDate = DateTime.Now;
                transaction.Code = _Name;
                transaction.Description = _Description;
                transaction.RowStatus = Constant.ROWSTATUS_ACTIVE;
                transaction.Save();
            }
            catch (Exception)
            {
                throw;
            }   
        }
        public void DeleteInventoryTransaction(Session session, Guid transactionId)
        {
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(transactionId);
                transaction.RowStatus = Constant.ROWSTATUS_DELETED;
                transaction.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
