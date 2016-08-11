using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.TempData;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Currency;
using NAS.BO.Accounting;
using Utility;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL;
using NAS.DAL.BI.Accounting;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting.Journal;
using NAS.DAL.System.Log;
using NAS.ETLBO.System.Object;
using NAS.DAL.ETL;
using System.Diagnostics;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.BI.Inventory;
using NAS.DAL.Inventory.Command;

namespace NAS.BO.ETL.Accounting
{
    public class ETLAccountingBO
    {        
        public FinancialAccountDim GetFinancialAccountDim(Session session, string Code)
        {
            try
            {
                Util util = new Util();
                Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", Code, BinaryOperatorType.Equal);
                if (account == null) return null;
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", Code, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStaus);

                FinancialAccountDim result = session.FindObject<FinancialAccountDim>(criteria);
                if (result == null)
                {
                    result = CreateFinancialAccountDim(session, Code);
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public FinancialAccountDim CreateFinancialAccountDim(Session session, string Code)
        {
            try
            {
                Util util = new Util();
                Account account = null;
                
                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", Code, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStaus);

                account = session.FindObject<Account>(criteria);

                if (account == null) return null;

                if (!Util.IsExistXpoObject<FinancialAccountDim>(session, "Code", Code))
                {
                    FinancialAccountDim accountDim = new FinancialAccountDim(session);
                    accountDim.Code = Code;
                    accountDim.Description = account.Name;
                    accountDim.Name = Code;
                    accountDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    accountDim.Save();
                    return accountDim;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public CorrespondFinancialAccountDim CreateCorrespondFinancialAccountDim(Session session, string Code)
        {
            try
            {
                Util util = new Util();

                Account account = null;

                CriteriaOperator criteria_RowStaus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", Code, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStaus);

                account = session.FindObject<Account>(criteria);

                if (account == null) return null;

                if (!Util.IsExistXpoObject<CorrespondFinancialAccountDim>(session, "Code", Code))
                {
                    CorrespondFinancialAccountDim accountDim = new CorrespondFinancialAccountDim(session);
                    accountDim.Code = Code;
                    accountDim.Description = account.Name;
                    accountDim.Name = Code;
                    accountDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    accountDim.Save();
                    return accountDim;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public CurrencyDim CreateCurrencyDim(Session session, string Code)
        {
            try
            {
                Util util = new Util();
                if (!Util.IsExistXpoObject<CurrencyDim>(session, "Code", Code))
                {
                    CurrencyDim currencyDim = new CurrencyDim(session);
                    currencyDim.Code = Code;
                    currencyDim.Description = "";
                    currencyDim.Name = Code;
                    currencyDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    currencyDim.Save();
                    return currencyDim;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public FinancialTransactionDim CreateFinancialTransactionDim(Session session, Guid RefId)
        {
            try
            {
                Transaction transaction = session.GetObjectByKey<Transaction>(RefId);
                if (transaction == null) return null;
                FinancialTransactionDim transactionDim = new FinancialTransactionDim(session);
                transactionDim.Description = transaction.Description;
                transactionDim.RefId = RefId;
                transactionDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                transactionDim.Name = transaction.Code;
                transactionDim.IssueDate = transaction.IssueDate;
                transactionDim.Save();
                return transactionDim;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public InventoryCommandDim CreateInventoryCommandDim(Session session, Guid RefId)
        //{
        //    try
        //    {
        //        InventoryCommand command = session.GetObjectByKey<InventoryCommand>(RefId);
        //        if (command == null) return null;
        //        InventoryCommandDim commandDim = new InventoryCommandDim(session);
        //        commandDim.Description = command.Description;
        //        commandDim.RefId = RefId;
        //        commandDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
        //        commandDim.Code = command.Code;
        //        commandDim.Name = command.Name;
        //        commandDim.IssueDate = command.IssueDate;
        //        commandDim.Save();
        //        return commandDim;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public bool IsRelateAccount(Session session, Guid ParentAccountID, Guid ChildAccountID)
        {
            bool result = false;
            try
            {
                if (ParentAccountID == ChildAccountID) return true;
                Account parentAccount = session.GetObjectByKey<Account>(ParentAccountID);
                Account childAccount = session.GetObjectByKey<Account>(ChildAccountID);
                
                if (parentAccount == null || childAccount == null) return false;
                while (childAccount.ParentAccountId != null)
                {
                    if (parentAccount == childAccount.ParentAccountId) return true;
                    childAccount = childAccount.ParentAccountId;
                }
                return false;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public bool IsRelateAccount(Session session, string parentCode, Guid ChildAccountID)
        {
            bool result = false;
            try
            {
                if (parentCode.Equals(string.Empty)) 
                    return false;

                CriteriaOperator criteria_RowStatus
                    = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", parentCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account parentAccount = session.FindObject<Account>(criteria);
                Account childAccount = session.GetObjectByKey<Account>(ChildAccountID);

                if (parentAccount == null || childAccount == null) return false;
                while (childAccount.ParentAccountId != null)
                {
                    if (parentAccount == childAccount.ParentAccountId) return true;
                    childAccount = childAccount.ParentAccountId;
                }
                return false;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public bool IsHighestAccount(Session session, Guid AccountId)
        {
            bool result = false;
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if (account == null) return false;
                if (account.ParentAccountId == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public Account GetHighestAccount(Session session, Guid AccountId)
        {
            Account result = null;
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if(account == null) return null;
                while (account.ParentAccountId != null)
                {
                    account = account.ParentAccountId;
                }
                result = account;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        public XPCollection<GeneralLedger> GetNewerGeneralLedger(Session session, GeneralLedger generalLedger)
        {
            XPCollection<GeneralLedger> result = null;
            try
            {
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IssueDateEqual = new BinaryOperator("IssuedDate", generalLedger.IssuedDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_IssueDateGreater = new BinaryOperator("IssuedDate", generalLedger.IssuedDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", generalLedger.CreateDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_DateTime1 = CriteriaOperator.And(criteria_IssueDateEqual, criteria_CreateDate);
                CriteriaOperator criteria_DateTime = CriteriaOperator.Or(criteria_IssueDateGreater, criteria_DateTime1);
                
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime);
                result = new XPCollection<GeneralLedger>(session, criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public double GetAccountingBalanceAtTime(Session session, Guid AccountingId, Guid CurrencyId, DateTime time)
        {
            double result = 0;
            try
            {
                Currency currentCurrency = session.GetObjectByKey<Currency>(CurrencyId);
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1;
                criteria_1 = new BinaryOperator("TransactionId.IssueDate", time, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("AccountId!Key", AccountingId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_3 = new BinaryOperator("CurrencyId.CurrencyTypeId", currentCurrency.CurrencyTypeId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3);
                XPCollection<GeneralLedger> ledgerList = new XPCollection<GeneralLedger>(session, criteria);
                if (ledgerList == null || ledgerList.Count == 0)
                {
                    return 0;
                }
                ledgerList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Descending));
                ledgerList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                result = ledgerList.FirstOrDefault().Balance;
                foreach (GeneralLedger ledger in ledgerList)
                {
                    Util util = new Util();
                    BusinessObjectBO businessObjectBO = new BusinessObjectBO();
                    BusinessObject businessObject = util.GetXpoObjectByFieldName<BusinessObject, Guid>(session, "RefId", ledger.TransactionId.TransactionId, BinaryOperatorType.Equal);
                    ETLJob etlJob = util.GetXpoObjectByFieldName<ETLJob, string>(session, "Code", "GeneralJournalToGeneralLedgerJob", BinaryOperatorType.Equal);
                    //Console.WriteLine("Test:" + businessObject.BusinessObjectId + " " + ledger.TransactionId.TransactionId);
                    if (!businessObjectBO.NeedToBeProcessed(session, businessObject.BusinessObjectId, etlJob.ETLJobId))
                    {
                        result = ledger.Balance;
                        return result;
                    }
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }        
        public ETL_Transaction ExtractTransaction(Session session,Guid TransactionId)
        {
            ETL_Transaction resultTransaction = null;
            try
            {
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null)
                {
                    return resultTransaction;
                }
                resultTransaction = new ETL_Transaction();
                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;
                resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();
                foreach (GeneralJournal journal in transaction.GeneralJournals)
                {
                    ETL_GeneralJournal tempJournal = new ETL_GeneralJournal();
                    tempJournal.AccountId = journal.AccountId.AccountId;
                    tempJournal.CreateDate = journal.CreateDate;
                    tempJournal.Credit = journal.Credit;
                    if (journal.CurrencyId == null)
                    {
                        tempJournal.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                    }
                    else
                    {
                        tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                    }
                    tempJournal.Debit = journal.Debit;
                    tempJournal.Description = journal.Description;
                    tempJournal.GeneralJournalId = journal.GeneralJournalId;
                    tempJournal.JournalType = journal.JournalType;
                    resultTransaction.GeneralJournalList.Add(tempJournal);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultTransaction;
        }
        public ETL_Transaction ExtractTransaction(Session session, Guid TransactionId,string AccountCode)
        {
            ETL_Transaction resultTransaction = null;
            try
            {
                bool Acceptable = false;
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account account = session.FindObject<Account>(criteria);
                /*2014/02/20 Duc.Vo INS START*/
                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
                /*2014/02/20 Duc.Vo INS END*/
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null)
                {
                    return resultTransaction;
                }

                Util util = new Util();

                /*2014/02/20 Duc.Vo MOD START*/
                resultTransaction = new ETL_Transaction();
                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;

                if (transaction is SaleInvoiceTransaction)
                {
                    if ((transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId != null)
                        resultTransaction.CustomerOrgId = (transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                } else
                if (transaction is PurchaseInvoiceTransaction)
                {
                    if ((transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId != null)
                        resultTransaction.SupplierOrgId = (transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;
                } else
                if (transaction is PaymentVouchesTransaction)
                {
                    PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();

                    Organization SuppOrg = paymentVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = paymentVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                } else
                if (transaction is ReceiptVouchesTransaction)
                {
                    ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                    Organization SuppOrg = receiptVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = receiptVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                } else
                {
                    Organization SuppOrg = GetAllocatedSupplierByManualTransaction(session, transaction.TransactionId);
                    Organization CustOrg = GetAllocatedCustomerByManualTransaction(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }

                if (resultTransaction.SupplierOrgId == Guid.Empty)
                    resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                if (resultTransaction.CustomerOrgId == Guid.Empty)
                    resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;

                /*2014/02/20 Duc.Vo MOD END*/
                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;
                resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();
                foreach (GeneralJournal journal in transaction.GeneralJournals.Where(i => i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
                {
                    ETL_GeneralJournal tempJournal = new ETL_GeneralJournal();
                    /*2014/02/20 Duc.Vo MOD START*/
                    if (journal.AccountId != null)
                        tempJournal.AccountId = journal.AccountId.AccountId;
                    else
                        tempJournal.AccountId = defaultAccount.AccountId;
                    /*2014/02/20 Duc.Vo MOD END*/
                    tempJournal.CreateDate = journal.CreateDate;
                    tempJournal.Credit = journal.Credit;
                    if (journal.CurrencyId == null)
                    {
                        tempJournal.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                    }
                    else
                    {
                        tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                    }
                    tempJournal.Debit = journal.Debit;
                    tempJournal.Description = journal.Description;
                    tempJournal.GeneralJournalId = journal.GeneralJournalId;
                    tempJournal.JournalType = journal.JournalType;
                    resultTransaction.GeneralJournalList.Add(tempJournal);
                    if (IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    {
                        Acceptable = true;
                    }
                }
                if (!Acceptable) return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultTransaction;
        }
        public List<ETL_GeneralLedger> TransformGeneralJournalToGeneralLedger(ETL_Transaction transaction)
        {
            List<ETL_GeneralLedger> result = new List<ETL_GeneralLedger>();
            try
            {                
                foreach (ETL_GeneralJournal etlJournal in transaction.GeneralJournalList)
                {
                    ETL_GeneralLedger tempLedger = new ETL_GeneralLedger();
                    tempLedger.TransactionId = transaction.TransactionId;
                    tempLedger.JournalType = etlJournal.JournalType;
                    tempLedger.AccountId = etlJournal.AccountId;
                    tempLedger.CurrencyId = etlJournal.CurrencyId;
                    tempLedger.Credit = etlJournal.Credit;
                    tempLedger.Debit = etlJournal.Debit;
                    tempLedger.Description = etlJournal.Description;
                    tempLedger.CreateDate = etlJournal.CreateDate;
                    tempLedger.IssueDate = transaction.IssuedDate;
                    tempLedger.UpdateDate = DateTime.Now;
                    tempLedger.Balance = 0;
                    result.Add(tempLedger);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }        
        public void LoadGeneralLedger(Session session, List<ETL_GeneralLedger> GeneralledgerList)
        {
            try
            {
                AccountingBO accountingBO = new AccountingBO();
                if(GeneralledgerList ==null) return;
                if(GeneralledgerList.Count ==0) return;
                Transaction transaction = session.GetObjectByKey<Transaction>(GeneralledgerList[0].TransactionId);
                if(transaction == null) return;
                foreach(GeneralLedger ledger in transaction.GeneralLedgers){                    
                    ledger.RowStatus = Constant.ROWSTATUS_DELETED;
                    ledger.Save();
                }                

                foreach (ETL_GeneralLedger ledger in GeneralledgerList)
                {
                    Account account = session.GetObjectByKey<Account>(ledger.AccountId);                    
                    Currency currency = session.GetObjectByKey<Currency>(ledger.CurrencyId);
                    if (account != null && currency!= null)
                    {
                        double sign = 1;
                        if (account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT)
                        {
                            sign = -1;
                        }

                        double _coefficient = 0;
                        _coefficient = accountingBO.getCoefficientCompareWithDefaultByCurrencyCode(session, currency.Code);

                        GeneralLedger generalLedger = new GeneralLedger(session);
                        generalLedger.AccountId = account;                        
                        generalLedger.Credit = ledger.Credit;
                        generalLedger.Debit = ledger.Debit;
                        generalLedger.Description = ledger.Description;
                        generalLedger.CurrencyId = currency;
                        generalLedger.TransactionId = transaction;
                        generalLedger.IssuedDate = ledger.IssueDate;
                        generalLedger.IsOriginal = true;                        
                        generalLedger.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        generalLedger.CreateDate = DateTime.Now;
                        generalLedger.UpdateDate = DateTime.Now;
                        generalLedger.Balance = GetAccountingBalanceAtTime(session, account.AccountId, currency.CurrencyId, transaction.IssueDate) + (ledger.Debit - ledger.Credit) * sign * _coefficient;
                        generalLedger.Save();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NAS.DAL.Nomenclature.Organization.Organization GetAllocatedSupplierByManualTransaction(Session session, Guid manualBookingTransactionId)
        {
            NAS.DAL.Nomenclature.Organization.Organization ret = null;
            try
            {
                Transaction transaction =
                    session.GetObjectByKey<Transaction>(manualBookingTransactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.MANUAL_BOOKING_SUPPLIER);

                ObjectCustomField objectCustomField = transactionObject.ObjectId.ObjectCustomFields.Where(
                        r => r.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId ==
                            objectTypeCustomField.ObjectTypeCustomFieldId).FirstOrDefault();
                if (objectCustomField == null)
                    return null;

                ObjectCustomFieldData objectCustomFieldData =
                    objectCustomField.ObjectCustomFieldDatas.FirstOrDefault();
                if (objectCustomFieldData == null)
                    return null;

                PredefinitionData predefinitionData =
                    (PredefinitionData)objectCustomFieldData.CustomFieldDataId;

                ret = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public NAS.DAL.Nomenclature.Organization.Organization GetAllocatedCustomerByManualTransaction(Session session, Guid manualBookingTransactionId)
        {
            NAS.DAL.Nomenclature.Organization.Organization ret = null;
            try
            {
                Transaction transaction =
                    session.GetObjectByKey<Transaction>(manualBookingTransactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.MANUAL_BOOKING_CUSTOMER);

                ObjectCustomField objectCustomField = transactionObject.ObjectId.ObjectCustomFields.Where(
                        r => r.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId ==
                            objectTypeCustomField.ObjectTypeCustomFieldId).FirstOrDefault();
                if (objectCustomField == null)
                    return null;

                ObjectCustomFieldData objectCustomFieldData =
                    objectCustomField.ObjectCustomFieldDatas.FirstOrDefault();
                if (objectCustomFieldData == null)
                    return null;

                PredefinitionData predefinitionData =
                    (PredefinitionData)objectCustomFieldData.CustomFieldDataId;

                ret = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
