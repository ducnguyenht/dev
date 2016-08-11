using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Actor;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;

namespace NAS.BO.ETL.Accounting.FinancialLiability
{
    public class DiaryJournalBO
    {
        public ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, string AccountCode)
        {
            ETLAccountingBO accountingBO = new ETLAccountingBO();
            ETL_GeneralJournal result = new ETL_GeneralJournal();
            try
            {
                if (journalList == null) return null;
                Util util = new Util();
                Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
                if (account == null) return null;
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (accountingBO.IsRelateAccount(session, journal.AccountId, account.AccountId))
                    {
                        return journal;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        public ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        {
            ETL_GeneralJournal result = new ETL_GeneralJournal();
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if (account == null) return null;
                result = GetJournal(session, journalList, account.Code);
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }
        public List<ETL_GeneralJournal> JoinJournal(Session session, List<ETL_GeneralJournal> journalList)
        {
            List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    ETL_GeneralJournal rsJournal = GetJournal(session, result, journal.AccountId);
                    if (rsJournal == null)
                    {
                        rsJournal = new ETL_GeneralJournal();
                        rsJournal.AccountId = accountingBO.GetHighestAccount(session, journal.AccountId).AccountId;
                        rsJournal.CreateDate = journal.CreateDate;
                        rsJournal.Credit = journal.Credit;
                        rsJournal.CurrencyId = journal.CurrencyId;
                        rsJournal.Debit = journal.Debit;
                        rsJournal.JournalType = journal.JournalType;
                        result.Add(rsJournal);
                    }
                    else
                    {
                        rsJournal.Credit += journal.Credit;
                        rsJournal.Debit += journal.Debit;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        public List<ETL_GeneralJournal> ClearJournalList(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        {
            List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();
            try
            {
                char AccountSide = 'N';
                char MainAccountSide = 'C';
                short DebitCount = 0;
                short CreditCount = 0;
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (journal.Credit > 0)
                    {
                        if (accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
                        {
                            AccountSide = 'C';
                        }
                        CreditCount++;
                    }
                    else
                    {
                        if (accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
                        {
                            AccountSide = 'D';
                        }
                        DebitCount++;
                    }
                }
                if (DebitCount == 1)
                {
                    MainAccountSide = 'D';
                }
                if (MainAccountSide == AccountSide) return journalList;

                ETL_GeneralJournal mainJournal = new ETL_GeneralJournal();

                if (MainAccountSide == 'C')
                {
                    mainJournal = journalList.Where(r => r.Credit > 0).FirstOrDefault();
                    result.Add(mainJournal);
                    foreach (ETL_GeneralJournal journal in journalList)
                    {
                        if (journal.Debit > 0)
                        {
                            if (!accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
                            {
                                mainJournal.Credit -= journal.Debit;
                            }
                            else
                            {
                                result.Add(journal);
                            }
                        }
                    }
                }
                if (MainAccountSide == 'D')
                {
                    mainJournal = journalList.Where(r => r.Debit > 0).FirstOrDefault();
                    result.Add(mainJournal);
                    foreach (ETL_GeneralJournal journal in journalList)
                    {
                        if (journal.Credit > 0)
                        {
                            if (!accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
                            {
                                mainJournal.Debit -= journal.Credit;
                            }
                            else
                            {
                                result.Add(journal);
                            }
                        }
                    }
                }
        
            }
            catch (Exception)
            {
                return null;
            }
        
            return result;
        }

        public List<DiaryJournalTemplate> TransformTransactionToTemplateArea(Session session, ETL_Transaction transaction, string AccountCode)
        {
            Util util = new Util();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;

            List<DiaryJournalTemplate> detail = new List<DiaryJournalTemplate>();

            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                List<ETL_GeneralJournal> JournalListJoined = JoinJournal(session, etlTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList = ClearJournalList(session, JournalListJoined, account.AccountId);
                foreach (ETL_GeneralJournal journal in FinishJournalList)
                {
                    DiaryJournalTemplate temp = new DiaryJournalTemplate();

                    temp.FinancialAccountDimCode = "";
                    temp.CorrespondFinancialAccountDimCode = "";
                    if (accountingBO.IsRelateAccount(session, account.AccountId, journal.AccountId))
                    {
                        temp.FinancialAccountDimCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    else
                    {
                        temp.CorrespondFinancialAccountDimCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    temp.CurrencyDimCode = session.GetObjectByKey<Currency>(journal.CurrencyId).Code;                    
                    temp.IssueDate = etlTransaction.IssuedDate;
                    temp.OwnerOrgId = etlTransaction.OwnerOrgId;
                    
                    temp.TransactionId = etlTransaction.TransactionId;
                    temp.Credit = (double)journal.Credit;
                    temp.Debit = (double)journal.Debit;

                    detail.Add(temp);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return detail;
        }

        public void LoadTemplateAreaToDiaryJournalDetail(Session session, List<DiaryJournalTemplate> TemplateArea, string AccountCode, char DebitOrCredit)
        {
            try
            {
                foreach (DiaryJournalTemplate detail in TemplateArea)
                {
                    CreateDiaryJournalDetail(session, detail, AccountCode, DebitOrCredit);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        // Get
        public DiaryJournal_Fact GetDiaryJournalFact(
                                                    Session session,
                                                    Guid OwnerOrgId,
                                                    DateTime IssueDate,
                                                    string FinancialAccountCode)
        {
            DiaryJournal_Fact result = null;
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
                    //CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_OwnerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    DiaryJournal_Fact fact = session.FindObject<DiaryJournal_Fact>(criteria);

                    if (fact == null) return null;
                    {
                        result = fact;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        // Create
        public DiaryJournal_Fact CreateDiaryJournalFact(Session session,
                                                        Guid OwnerOrgId,
                                                        DateTime IssueDate,
                                                        DiaryJournalTemplate diaryJournal,
                                                        string accountCode)
        {
            DiaryJournal_Fact result = new DiaryJournal_Fact(session);
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                DimBO dimBO = new DimBO();                
                result.FinancialAccountDimId = accountingBO.GetFinancialAccountDim(session, accountCode);
                result.MonthDimId = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = dimBO.GetYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                //result.CreditSum = 0;
                //result.DebitSum = 0;
                //result.BeginCreditBalance = 0;
                //result.BeginDebitBalance = 0;
                //result.EndCreditBalance = 0;
                //result.EndDebitBalance = 0;

                if (result.FinancialAccountDimId == null || result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null)
                {
                    return null;
                }
                result.Save();
                
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public void CreateDiaryJournalDetail(Session session, DiaryJournalTemplate diaryJournal, string accountCode, char debitOrCredit)
        {
            try
            {
                Util util = new Util();
                ETLAccountingBO accountingBO = new ETLAccountingBO();

                DiaryJournal_Fact Fact = GetDiaryJournalFact(session, diaryJournal.OwnerOrgId, diaryJournal.IssueDate, accountCode);
                DiaryJournal_Detail newDetail = new DiaryJournal_Detail(session);
                if (Fact == null)
                {
                    Fact = CreateDiaryJournalFact(session, diaryJournal.OwnerOrgId, diaryJournal.IssueDate, diaryJournal, accountCode);
                    if (Fact == null) return;
                }
                CorrespondFinancialAccountDim correspondFinancialAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(session, "Code", diaryJournal.CorrespondFinancialAccountDimCode, BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", accountCode, BinaryOperatorType.Equal);
                FinancialTransactionDim financialTransactionDim = util.GetXpoObjectByFieldName<FinancialTransactionDim, Guid>(session, "RefId", diaryJournal.TransactionId, BinaryOperatorType.Equal);
                CurrencyDim currencyDim = util.GetXpoObjectByFieldName<CurrencyDim, string>(session, "Code", diaryJournal.CurrencyDimCode, BinaryOperatorType.Equal);
                if (financialTransactionDim == null)
                {
                    financialTransactionDim = accountingBO.CreateFinancialTransactionDim(session, diaryJournal.TransactionId);
                    if (financialTransactionDim == null)
                    {
                        return;
                    }
                }
                if (financialAccountDim == null)
                {
                    financialAccountDim = accountingBO.CreateFinancialAccountDim(session, accountCode);
                }
                if (correspondFinancialAccountDim == null)
                {
                    correspondFinancialAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, diaryJournal.CorrespondFinancialAccountDimCode);
                }
                if (currencyDim == null)
                {
                    currencyDim = accountingBO.CreateCurrencyDim(session, diaryJournal.CurrencyDimCode);
                }
              
                Fact.Save();
                if (correspondFinancialAccountDim.Code == "131")
                {
                    newDetail.Credit = diaryJournal.Credit;    
                }

                if (correspondFinancialAccountDim != null)
                {
                    if (correspondFinancialAccountDim.Code == "")
                    {
                        if (debitOrCredit == 'C')
                        {
                            newDetail.Credit = diaryJournal.Credit;    
                        }
                        else
                        {
                            newDetail.Debit = diaryJournal.Debit;    
                        }
                        newDetail.FinancialAccountDimId = financialAccountDim;
                    }
                    else
                    {
                        if (debitOrCredit == 'C')
                        {
                            newDetail.Debit = diaryJournal.Debit;
                        }
                        else
                        {
                            newDetail.Credit = diaryJournal.Credit;
                        }
                        newDetail.CorrespondFinancialAccountDimId = correspondFinancialAccountDim;
                    }
                }
            
                newDetail.CurrencyDimId = currencyDim;
                newDetail.DiaryJournal_FactId = Fact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;                
                newDetail.Save();
            }
            catch (Exception)
            {
                return;
            }
        }
    }


    public class DiaryJournalTemplate
    {
        public string FinancialAccountDimCode { get; set; }
        public string CorrespondFinancialAccountDimCode { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string CurrencyDimCode { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime IssueDate { get; set; }
        public Guid OwnerOrgId { get; set; }
        public bool IsBalanceForward { get; set; }
    }
}
