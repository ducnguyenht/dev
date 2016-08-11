using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.ETL.Accounting.FinancialOntheWay.TempData;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Currency;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;
using Utility;
using NAS.DAL.BI.Accounting;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Command.CommanDynamicField;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.System.Log;

namespace NAS.BO.ETL.Accounting.FinancialOntheWay
{
    public class FinancialOnTheWayBuyingGoodBO
    {
        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        #region other
        public FinancialOnTheWayBuyingGoodSummary GetFinancialOnTheWayBuyingGoodSummary(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            FinancialOnTheWayBuyingGoodSummary result = null;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", FinancialAccountCode, BinaryOperatorType.Equal);

                if (ownerOrgDim == null || monthDim == null || yearDim == null || financialAccountDim == null)
                {
                    return null;
                }
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    result = session.FindObject<FinancialOnTheWayBuyingGoodSummary>(criteria);

                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public OnTheWayBuyingGoodArtifact GetOnTheWayBuyingGoodArtifact(
                                                                    Session session,
                                                                    Guid FinancialOnTheWayBuyingGoodSummaryId,
                                                                    Guid PurchaseInvoiceId,
                                                                    Guid InputCommandId)
        {
            OnTheWayBuyingGoodArtifact result = null;
            try
            {
                Util util = new Util();
                FinancialOnTheWayBuyingGoodSummary summary = session.GetObjectByKey<FinancialOnTheWayBuyingGoodSummary>(FinancialOnTheWayBuyingGoodSummaryId);
                NAS.DAL.Invoice.PurchaseInvoice invoice = util.GetXpoObjectByFieldName<NAS.DAL.Invoice.PurchaseInvoice, Guid>(session, "BillId", PurchaseInvoiceId, BinaryOperatorType.Equal);
                InventoryCommand command = util.GetXpoObjectByFieldName<InventoryCommand, Guid>(session, "InventoryCommandId", InputCommandId, BinaryOperatorType.Equal);


                if (summary == null || invoice == null || command == null)
                {
                    return null;
                }
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_summary = new BinaryOperator("FinancialOnTheWayBuyingGoodSummaryId", summary, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_invoice = new BinaryOperator("InvoiceCode", invoice.Code, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_command = new BinaryOperator("LegalInvoiceCode", command.Code, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_summary, criteria_invoice, criteria_command);

                    result = session.FindObject<OnTheWayBuyingGoodArtifact>(criteria);

                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FinancialOnTheWayBuyingGoodSummary CreateFinancialOnTheWayBuyingGoodSummary(Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            FinancialOnTheWayBuyingGoodSummary result = new FinancialOnTheWayBuyingGoodSummary(session);
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialAccountDim accountDim = accountingBO.GetFinancialAccountDim(session, FinancialAccountCode);
                DimBO dimBO = new DimBO();
                MonthDim monthDim = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                YearDim yearDim = dimBO.GetYearDim(session, (short)IssueDate.Year);
                OwnerOrgDim ownOrgDim = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                result.BeginBalance = 0;
                result.FinancialAccountDimId = accountDim != null ? accountDim : accountingBO.CreateFinancialAccountDim(session, FinancialAccountCode);
                result.MonthDimId = monthDim != null ? monthDim : dimBO.CreateMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = yearDim != null ? yearDim : dimBO.CreateYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = ownOrgDim != null ? ownOrgDim : dimBO.CreateOwnerOrgDim(session, OwnerOrgId);
                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                if (result.FinancialAccountDimId == null || result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null)
                {
                    return null;
                }
                var date = new DateTime(IssueDate.Year, IssueDate.Month, 1);
                FinancialOnTheWayBuyingGoodSummary previousSummary = GetFinancialOnTheWayBuyingGoodSummary(session,
                    OwnerOrgId, date.AddMonths(-1), FinancialAccountCode);

                if (previousSummary != null)
                    result.BeginBalance = previousSummary.EndBalance;
                result.Save();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public OnTheWayBuyingGoodArtifact CreateOnTheWayBuyingGoodArtifact(Session session,
                                                                    Guid FinancialOnTheWayBuyingGoodSummaryId,
                                                                    Guid PurchaseInvoiceId,
                                                                    Guid InputCommandId)
        {
            OnTheWayBuyingGoodArtifact result = new OnTheWayBuyingGoodArtifact(session);
            try
            {
                Util util = new Util();               

                FinancialOnTheWayBuyingGoodSummary summary = session.GetObjectByKey<FinancialOnTheWayBuyingGoodSummary>(FinancialOnTheWayBuyingGoodSummaryId);

                NAS.DAL.Invoice.PurchaseInvoice invoice = util.GetXpoObjectByFieldName<NAS.DAL.Invoice.PurchaseInvoice, Guid>(session, "BillId", PurchaseInvoiceId, BinaryOperatorType.Equal);
                InventoryCommand command = util.GetXpoObjectByFieldName<InventoryCommand, Guid>(session, "InventoryCommandId", InputCommandId, BinaryOperatorType.Equal);


                if (summary == null || invoice == null || command == null)
                {
                    return null;
                }

                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                result.FinancialOnTheWayBuyingGoodSummaryId = summary;
                result.InvoiceCode = invoice.Code;
                result.InvoiceIssuedDate = invoice.IssuedDate;
                result.LegalInvoiceCode = command.Code;
                result.LegalInvoiceIssuedDate = command.IssueDate;
                result.Save();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CreateFinancialOnTheWayBuyingGoodDetail(Session session, ETL_FinancialOnTheWayBuyingGoodDetail Detail, string MainAccountCode)
        {
            try
            {
                if (Detail == null ||
                    MainAccountCode.Equals(string.Empty) ||
                    Detail.PurchaseInvoiceId.Equals(Guid.Empty) ||
                    Detail.InputInventoryCommandId.Equals(Guid.Empty) ||
                    Detail.OwnerOrgId.Equals(Guid.Empty) ||
                    Detail.IssueDate == null)
                    return;

                //bool flgNewSummary = false;
                Util util = new Util();
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialOnTheWayBuyingGoodSummary summary = GetFinancialOnTheWayBuyingGoodSummary(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                FinancialOnTheWayBuyingGoodDetail newDetail = new FinancialOnTheWayBuyingGoodDetail(session);
                if (summary == null)
                {
                    summary = CreateFinancialOnTheWayBuyingGoodSummary(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                    if (summary == null) return;
                }
                else
                {
                    var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                    FinancialOnTheWayBuyingGoodSummary previousSummary = GetFinancialOnTheWayBuyingGoodSummary(session,
                        Detail.OwnerOrgId, date.AddMonths(-1), MainAccountCode);

                    if (previousSummary != null)
                    {
                        summary.BeginBalance = previousSummary.EndBalance;
                    }
                }
                /*2014/02/22 Duc.Vo MOD START*/
                CorrespondFinancialAccountDim correspondFinancialAccountDim = null;
                FinancialAccountDim financialAccountDim = null;
                InventoryCommand InputCommand = null;
                NAS.DAL.Invoice.PurchaseInvoice PurchaseInvoice = null;

                if (!Detail.CorrespondAccountCode.Equals(string.Empty))
                    correspondFinancialAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(session, "Code", Detail.CorrespondAccountCode, BinaryOperatorType.Equal);

                if (!MainAccountCode.Equals(string.Empty))
                    financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", MainAccountCode, BinaryOperatorType.Equal);

                if (!Detail.InputInventoryCommandId.Equals(Guid.Empty))
                {
                    InputCommand = util.GetXpoObjectByFieldName<InventoryCommand, Guid>(session, "InventoryCommandId", Detail.InputInventoryCommandId, BinaryOperatorType.Equal);
                    if (InputCommand == null) return;
                }
                if (!Detail.PurchaseInvoiceId.Equals(Guid.Empty))
                {
                    PurchaseInvoice = util.GetXpoObjectByFieldName<NAS.DAL.Invoice.PurchaseInvoice, Guid>(session, "BillId", Detail.PurchaseInvoiceId, BinaryOperatorType.Equal);
                    if (PurchaseInvoice == null) return;
                }

                OnTheWayBuyingGoodArtifact artifact = GetOnTheWayBuyingGoodArtifact(
                    session, 
                    summary.FinancialOnTheWayBuyingGoodSummaryId, 
                    PurchaseInvoice.BillId, 
                    InputCommand.InventoryCommandId);

                if (artifact == null)
                {
                    artifact = CreateOnTheWayBuyingGoodArtifact(
                        session, 
                        summary.FinancialOnTheWayBuyingGoodSummaryId, 
                        PurchaseInvoice.BillId, 
                        InputCommand.InventoryCommandId);

                    if (artifact == null)
                    {
                        return;
                    }
                }

                /*2014/02/22 Duc.Vo INS START*/
                if (financialAccountDim == null && !MainAccountCode.Equals(string.Empty))
                {
                    financialAccountDim = accountingBO.CreateFinancialAccountDim(session, MainAccountCode);
                }

                if (correspondFinancialAccountDim == null && !Detail.CorrespondAccountCode.Equals(string.Empty))
                {
                    correspondFinancialAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, Detail.CorrespondAccountCode);
                }

                CurrencyDim currencyDim = util.GetXpoObjectByFieldName<CurrencyDim, string>(
                    session,
                    "Code",
                    Detail.CurrencyCode,
                    BinaryOperatorType.Equal);

                if (currencyDim == null && !Detail.CurrencyCode.Equals(string.Empty))
                {
                    currencyDim = accountingBO.CreateCurrencyDim(session, Detail.CurrencyCode);
                }

                FinancialTransactionDim financialTransactionDim = util.GetXpoObjectByFieldName<FinancialTransactionDim, Guid>(
                    session,
                    "RefId",
                    Detail.TransactionId,
                    BinaryOperatorType.Equal);

                if (financialTransactionDim == null)
                {
                    financialTransactionDim = accountingBO.CreateFinancialTransactionDim(session, Detail.TransactionId);
                    if (financialTransactionDim == null)
                    {
                        return;
                    }
                }

                newDetail.CorrespondFinancialAccountDimId = correspondFinancialAccountDim;
                newDetail.Credit = Detail.Credit;
                newDetail.Debit = Detail.Debit;
                newDetail.ActuaPrice = Detail.ActualPrice;
                newDetail.BookingPrice = Detail.BookedPrice;
                newDetail.FinancialAccountDimId = financialAccountDim;
                newDetail.OnTheWayBuyingGoodArtifactId = artifact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;
                newDetail.CurrencyDimId = currencyDim;
                /*2014-02-22 ERP-1417 Duc.Vo INS START*/

                if (newDetail.FinancialAccountDimId == null)
                    newDetail.FinancialAccountDimId = defaultFinancialAcc;
                if (newDetail.CorrespondFinancialAccountDimId == null)
                    newDetail.CorrespondFinancialAccountDimId = defaultCorrespondindAcc;
                /*2014-02-22 ERP-1417 Duc.Vo INS END*/
                newDetail.RowStatus = Constant.ROWSTATUS_ACTIVE;
                newDetail.Save();

                if (Detail.IsBalanceForward)
                {
                    summary.BeginBalance = summary.EndBalance = (decimal)Detail.Debit;
                    summary.CreditSum = summary.DebitSum = 0;
                }
                else
                {
                    CorrespondFinancialAccountDim defaultAccDim = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                    summary.CreditSum = (decimal)summary.OnTheWayBuyingGoodArtifacts.SelectMany(t=>t.FinancialOnTheWayBuyingGoodDetails).
                        Where(i => i.RowStatus == 1
                            && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(i=>i.Credit);

                    summary.DebitSum = (decimal)summary.OnTheWayBuyingGoodArtifacts.SelectMany(t => t.FinancialOnTheWayBuyingGoodDetails).
                        Where(i => i.RowStatus == 1
                            && i.Debit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(i => i.Debit);

                    summary.BeginBalance = summary.BeginBalance + summary.DebitSum - summary.CreditSum;
                }

                summary.Save();                
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region ETL
        public void FixInvokedBussinessObjects(Session session, XPCollection<BusinessObject> invokedBussinessObjects)
        {
            if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
                return;
            // chua link transaction
            CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
            CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
            CorrespondFinancialAccountDim defaultAccDim = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);

            XPCollection<FinancialOnTheWayBuyingGoodDetail> neededToBeFixList = new XPCollection<FinancialOnTheWayBuyingGoodDetail>(session, criteria);
            //FinancialOnTheWayBuyingGoodSummary fact = null;
            //List<OnTheWayBuyingGoodArtifact> relevantArtifacts = new List<OnTheWayBuyingGoodArtifact>();
            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
                foreach (FinancialOnTheWayBuyingGoodDetail detail in neededToBeFixList)
                {
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    //relevantArtifacts.Add(detail.OnTheWayBuyingGoodArtifactId);
                    //if (defaultAccDim != null && detail.CorrespondFinancialAccountDimId != null
                    //    && detail.CorrespondFinancialAccountDimId.Code.Equals(defaultAccDim.Code))
                    //{
                    //    fact.CreditSum -= (decimal)detail.Credit;
                    //    fact.DebitSum -= (decimal)detail.Debit;
                    //}

                    //fact.EndBalance
                    //        = fact.BeginBalance -
                    //        fact.CreditSum +
                    //        fact.DebitSum;

                    //fact.Save();
                }
        }

        public ETL_TransactionS04a6DN ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_TransactionS04a6DN resultTransaction = null;
            try
            {
                bool Acceptable = false;
                Util util = new Util();
                InventoryCommand command = null;
                NAS.DAL.Invoice.PurchaseInvoice invoice = null;
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account account = session.FindObject<Account>(criteria);
                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null)
                {
                    return resultTransaction;
                }
                
                try
                {
                    if (transaction is InventoryCommandFinancialTransaction)
                    {
                        command = (((transaction as InventoryCommandFinancialTransaction).
                            InventoryJournalFinancials.FirstOrDefault().InventoryJournalId.InventoryTransactionId)
                            as InventoryCommandItemTransaction).InventoryCommandId;

                        invoice = GetAllocatedPurchaseInvoiceByInventoryCommand(session, command.InventoryCommandId);
                    }
                    else
                    {
                        command = GetAllocatedInputInventoryCommandByManualTransaction(session, transaction.TransactionId);
                        invoice = GetAllocatedPurchaseInvoiceByManualTransaction(session, transaction.TransactionId);
                    }
                }
                catch (Exception)
                {
                    command = null;
                    invoice = null;
                }

                if (command == null)
                    return null;

                if (invoice == null)
                    return null;

                resultTransaction = new ETL_TransactionS04a6DN();

                resultTransaction.InputInventoryCommandId = command.InventoryCommandId;
                resultTransaction.PurchaseInvoiceId = invoice.BillId;

                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;                

                if (resultTransaction.SupplierOrgId == Guid.Empty)
                    resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                if (resultTransaction.CustomerOrgId == Guid.Empty)
                    resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;

                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;

                double numOfItem = 0;
                if (transaction != null)
                {
                    InventoryJournal inventoryJournal = null;
                    try
                    {
                        inventoryJournal = transaction.InventoryJournalFinancials.FirstOrDefault().InventoryJournalId;
                        numOfItem = inventoryJournal.Debit > 0 ? inventoryJournal.Debit : inventoryJournal.Credit;
                    }
                    catch (Exception)
                    {
                        numOfItem = 0;
                    }
                }
                resultTransaction.ActualPrice = resultTransaction.Amount / numOfItem;
                resultTransaction.BookedPrice = resultTransaction.ActualPrice;

                resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();
                foreach (GeneralJournal journal in 
                    transaction.GeneralJournals.Where(i=>i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY ||i.RowStatus == Constant.ROWSTATUS_ACTIVE) )
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
                    tempJournal.GeneralJournalId = journal.GeneralJournalId;                    

                    resultTransaction.GeneralJournalList.Add(tempJournal);
                    if (accountingBO.IsRelateAccount(session, account.AccountId, tempJournal.AccountId) && journal.Credit > 0 )
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

        public List<ETL_FinancialOnTheWayBuyingGoodDetail> TransformTransactionOnTheWayBuyingGoodDetail(Session session, ETL_Transaction transaction, string AccountCode)
        {
            Util util = new Util();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;
            List<ETL_FinancialOnTheWayBuyingGoodDetail> detailList = new List<ETL_FinancialOnTheWayBuyingGoodDetail>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();
                ETL_FinancialOnTheWayBuyingGoodDetail temp = null;
                List<ETL_GeneralJournal> JournalListJoined = liabilityBO.JoinJournal(session, etlTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList = liabilityBO.ClearJournalList(session, JournalListJoined, account.AccountId);
                foreach (ETL_GeneralJournal journal in JournalListJoined)
                {
                    temp = new ETL_FinancialOnTheWayBuyingGoodDetail();
                    temp.AccountCode = "";
                    temp.CorrespondAccountCode = "";
                    if (accountingBO.IsRelateAccount(session, account.AccountId, journal.AccountId))
                    {
                        temp.AccountCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    else
                    {
                        temp.CorrespondAccountCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    temp.ActualPrice = (transaction as ETL_TransactionS04a6DN).ActualPrice;
                    temp.BookedPrice = (transaction as ETL_TransactionS04a6DN).BookedPrice;
                    temp.IssueDate = etlTransaction.IssuedDate;
                    temp.OwnerOrgId = etlTransaction.OwnerOrgId;
                    temp.TransactionId = etlTransaction.TransactionId;
                    temp.Credit = journal.Credit;
                    temp.Debit = journal.Debit;
                    temp.Credit = journal.Credit;
                    temp.Debit = journal.Debit;
                    temp.CurrencyCode = session.GetObjectByKey<Currency>(journal.CurrencyId).Code;
                    temp.TransactionId = etlTransaction.TransactionId;
                    temp.IsBalanceForward = etlTransaction.IsBalanceForward;
                    temp.PurchaseInvoiceId = (transaction as ETL_TransactionS04a6DN).PurchaseInvoiceId;
                    temp.InputInventoryCommandId = (transaction as ETL_TransactionS04a6DN).InputInventoryCommandId;
                    detailList.Add(temp);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return detailList;
        }

        public void LoadFinancialOnTheWayBuyingGoodDetail(Session session, List<ETL_FinancialOnTheWayBuyingGoodDetail> DetailList, string MainAccountCode)
        {
            try
            {
                foreach (ETL_FinancialOnTheWayBuyingGoodDetail detail in DetailList)
                {
                    CreateFinancialOnTheWayBuyingGoodDetail(session, detail, MainAccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region GetCMSAllocated
        public NAS.DAL.Invoice.PurchaseInvoice GetAllocatedPurchaseInvoiceByInventoryCommand(Session session, Guid inventoryCommandId)
        {
            NAS.DAL.Invoice.PurchaseInvoice ret = null;
            try
            {
                InventoryCommand command =
                    session.GetObjectByKey<InventoryCommand>(inventoryCommandId);
                InventoryCommandObject commandObject = command.InventoryCommandObjects.First();
                if (commandObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);

                ObjectCustomField objectCustomField = commandObject.ObjectId.ObjectCustomFields.Where(
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

                ret = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NAS.DAL.Invoice.PurchaseInvoice GetAllocatedPurchaseInvoiceByManualTransaction(Session session, Guid manualBookingTransactionId)
        {
            NAS.DAL.Invoice.PurchaseInvoice ret = null;
            try
            {
                Transaction transaction =
                    session.GetObjectByKey<Transaction>(manualBookingTransactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.MANUAL_BOOKING_PURCHASE_INVOICE);

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

                ret = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public InventoryCommand GetAllocatedInputInventoryCommandByManualTransaction(Session session, Guid manualBookingTransactionId)
        {
            InventoryCommand ret = null;
            try
            {
                Transaction transaction =
                    session.GetObjectByKey<Transaction>(manualBookingTransactionId);
                TransactionObject transactionObject = transaction.TransactionObjects.First();
                if (transactionObject == null)
                    return null;
                ObjectTypeCustomField objectTypeCustomField =
                    ObjectTypeCustomField.GetDefault(session, DefaultObjectTypeCustomFieldEnum.MANUAL_BOOKING_INPUT_INVENTORY_COMMAND);

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

                ret = session.GetObjectByKey<InventoryCommand>(predefinitionData.RefId);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
