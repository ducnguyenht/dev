using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL;
using NAS.DAL.System.ShareDim;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.BO.ETL.Accounting.DiaryJournal
{
    public class DJ_Journal
    {
        public Guid GeneralJournalId { get; set; }
        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Description { get; set; }
        public char JournalType { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class DJ_Transaction
    {
        public bool IsBalanceForward { get; set; }
        public double Amount { get; set; }
        public Guid OwnerOrgId { get; set; }
        public Guid SupplierOrgId { get; set; }
        public Guid CustomerOrgId { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<DJ_Journal> GeneralJournalList { get; set; }
    }  
    
    public class DJ_Fact
    {
        public string Month{get;set;}
        public string Year{get;set;}
        public Decimal CreditSum{get;set;}
        public Decimal DebitSum{get;set;}
        public Decimal BeginCredit{get;set;}
        public Decimal BeginDebit{get;set;}
        public Decimal EndCredit{get;set;}
        public Decimal EndDebit{get;set;}
        public Guid OwnerOrg{get;set;}
        public Guid MainAccountDim{get;set;}
        public bool IsBalanceForward { get; set; }
        public List<DJ_Detail> DetailList { get; set; }
    }

    public class DJ_Detail
    {        
        public Decimal Credit{get;set;}
        public Decimal Debit{get;set;}
        public Guid AccountDim{get;set;}
        public Guid TransactionDim{get;set;}
        public Guid CurrencyDim{get;set;}
    }

    public class ETL_DiaryJournal
    {
        #region Support method
        public bool IsJoinableJournals(Session session, ETL_GeneralJournal journal1, ETL_GeneralJournal journal2)
        {
            bool result = false;
            ETLAccountingBO accountingBO = new ETLAccountingBO();
            try
            {
                result = (accountingBO.IsRelateAccount(session, journal1.AccountId, journal2.AccountId) || accountingBO.IsRelateAccount(session, journal1.AccountId, journal2.AccountId))
                        && ((journal1.Credit * journal2.Credit > 0) || (journal1.Debit * journal2.Debit > 0))
                        && (journal1.CurrencyId == journal2.CurrencyId)
                        && (journal1.JournalType == journal2.JournalType);
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
        public bool IsOffBalanceAccount(Session session, Guid AccountId)
        {
            bool result = false;
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if (account == null)
                {
                    return true;
                }
                if (account.AccountTypeId.AccountCategoryId.Code == "OFFBALANCE")
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
        public ETL_GeneralJournal JoinJournal(Session session, ETL_GeneralJournal journal1, ETL_GeneralJournal journal2)
        {
            ETL_GeneralJournal result = null;
            ETLAccountingBO accountingBO = new ETLAccountingBO();
            try
            {
                if (!IsJoinableJournals(session, journal1, journal2))
                {
                    return null;
                }
                result = journal1;
                result.Debit += journal2.Debit;
                result.Credit += journal2.Credit;
                result.AccountId = accountingBO.GetHighestAccount(session, journal1.AccountId).AccountId;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        #endregion

        public ETL_Transaction ExtractDiaryJournalTransaction(Session session, Guid TransactionId)
        {
            ETL_Transaction result = null;
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                result = accountingBO.ExtractTransaction(session, TransactionId);
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public List<ETL_GeneralJournal> JoinJournalList(Session session, List<ETL_GeneralJournal> journalList)
        {
            List<ETL_GeneralJournal> result = null;
            try
            {
                result = new List<ETL_GeneralJournal>();
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (!IsOffBalanceAccount(session, journal.AccountId))
                    {
                        bool flag = false;
                        foreach (ETL_GeneralJournal rs_journal in result)
                        {
                            if (IsJoinableJournals(session, rs_journal, journal))
                            {
                                flag = true;
                                rs_journal.Debit += journal.Debit;
                                rs_journal.Credit += journal.Credit;
                                rs_journal.AccountId = accountingBO.GetHighestAccount(session, rs_journal.AccountId).AccountId;
                            }
                        }
                        if (!flag)
                        {
                            ETL_GeneralJournal temp_journal = journal;
                            temp_journal.AccountId = accountingBO.GetHighestAccount(session, temp_journal.AccountId).AccountId;
                            result.Add(temp_journal);
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
        public List<DJ_Fact> TransformToDiaryJournal(Session session, ETL_Transaction transaction)
        {
            List<DJ_Fact> result = null;
            try
            {
                List<ETL_GeneralJournal> CreditJournalList = new List<ETL_GeneralJournal>();
                List<ETL_GeneralJournal> DebitJournalList = new List<ETL_GeneralJournal>();
                int Credit_Count = 0;
                int Debit_Count = 0;
                string month = transaction.IssuedDate.Month.ToString();
                string year = transaction.IssuedDate.Year.ToString();
                Util util = new Util();
                //if (!Util.IsExistXpoObject<MonthDim>(session, "Name", month, BinaryOperatorType.Equal))
                //{
                //    MonthDim newMonthDim = new MonthDim(session);
                //    newMonthDim.Description = month;
                //    newMonthDim.Name = month;
                //    newMonthDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                //    newMonthDim.Save();
                //}

                //if (!Util.IsExistXpoObject<YearDim>(session, "Name", year, BinaryOperatorType.Equal))
                //{
                //    YearDim newYearDim = new YearDim(session);
                //    newYearDim.Description = year;
                //    newYearDim.Name = year;
                //    newYearDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                //    newYearDim.Save();
                //}

                //if (!Util.IsExistXpoObject<OwnerOrgDim>(session, "Name", "QUASAPHARCO", BinaryOperatorType.Equal))
                //{
                //    OwnerOrgDim newOwnerOrgDim = new OwnerOrgDim(session);
                //    newOwnerOrgDim.Description = "QUASAPHARCO";
                //    newOwnerOrgDim.Name = "QUASAPHARCO";
                //    newOwnerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                //    //Vuong miss GUID
                //    newOwnerOrgDim.Save();
                //}

                string mode="";
                foreach (ETL_GeneralJournal journal in transaction.GeneralJournalList)
                {
                    Account account = session.GetObjectByKey<Account>(journal.AccountId);
                    if (account != null)
                    {
                        //if (!Util.IsExistXpoObject<FinancialAccountDim>(session, "Code", account.Code, BinaryOperatorType.Equal))
                        //{
                        //    FinancialAccountDim _FinancialAccountDim = new FinancialAccountDim(session);
                        //    _FinancialAccountDim.Code = account.Code;
                        //    _FinancialAccountDim.Description = account.Description;
                        //    _FinancialAccountDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                        //    _FinancialAccountDim.Save();
                        //}

                        //if (!Util.IsExistXpoObject<CorrespondFinancialAccountDim>(session, "Code", account.Code, BinaryOperatorType.Equal))
                        //{
                        //    CorrespondFinancialAccountDim _CorrespondFinancialAccountDim = new CorrespondFinancialAccountDim(session);
                        //    _CorrespondFinancialAccountDim.Code = account.Code;
                        //    _CorrespondFinancialAccountDim.Description = account.Description;
                        //    _CorrespondFinancialAccountDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                        //    _CorrespondFinancialAccountDim.Save();
                        //}

                        if (journal.Credit == 0)
                        {
                            DebitJournalList.Add(journal);
                            Debit_Count++;
                        }
                        else
                        {
                            CreditJournalList.Add(journal);
                            Credit_Count++;
                        }
                    }
                }
                Debit_Count = Debit_Count > 2 ? 2 : Debit_Count;
                Credit_Count = Credit_Count > 2 ? 2 : Credit_Count;
                mode = Credit_Count.ToString() + Debit_Count.ToString();
                result = new List<DJ_Fact>();
                switch (mode)//[Credit][Debit]
                {
                    case "00"://Error
                        {
                            return null;
                        }
                    case "01":
                    case "10"://BalanceForwardTransaction
                        {
                            foreach (ETL_GeneralJournal journal in transaction.GeneralJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.BeginCredit = (Decimal)journal.Credit;
                                DiaryJournalFact.BeginDebit = (Decimal)journal.Debit;
                                DiaryJournalFact.CreditSum = 0;
                                DiaryJournalFact.DebitSum = 0;
                                DiaryJournalFact.EndCredit = 0;
                                DiaryJournalFact.EndDebit = 0;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = true;
                                result.Add(DiaryJournalFact);
                            }
                            break;
                        }
                    case "11":
                        {
                            foreach (ETL_GeneralJournal journal in DebitJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();                                                                
                                DiaryJournalFact.CreditSum = 0;
                                DiaryJournalFact.DebitSum = (Decimal)journal.Debit;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;                                
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in CreditJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = (Decimal)journal_dt.Credit;
                                    detail.Debit = 0;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }

                            foreach (ETL_GeneralJournal journal in CreditJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.CreditSum = (Decimal)journal.Credit;
                                DiaryJournalFact.DebitSum = 0;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in DebitJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = 0;
                                    detail.Debit = (Decimal)journal_dt.Debit;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }
                            break;
                        }
                    case "12"://1 Credit - n Debit
                        {
                            foreach (ETL_GeneralJournal journal in CreditJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.CreditSum = (Decimal)journal.Credit;
                                DiaryJournalFact.DebitSum = 0;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in DebitJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = 0;
                                    detail.Debit = (Decimal)journal_dt.Debit;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }

                            foreach (ETL_GeneralJournal journal in DebitJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.CreditSum = 0;
                                DiaryJournalFact.DebitSum = (Decimal)journal.Debit;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in CreditJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = (Decimal)journal.Debit;
                                    detail.Debit = 0;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }
                            break;
                        }
                    case "21"://n Credit - 1 Debit
                        {
                            foreach (ETL_GeneralJournal journal in CreditJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.CreditSum = (Decimal)journal.Credit;
                                DiaryJournalFact.DebitSum = 0;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in DebitJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = 0;
                                    detail.Debit = (Decimal)journal.Credit;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }

                            foreach (ETL_GeneralJournal journal in DebitJournalList)
                            {
                                DJ_Fact DiaryJournalFact = new DJ_Fact();
                                DiaryJournalFact.CreditSum = 0;
                                DiaryJournalFact.DebitSum = (Decimal)journal.Debit;
                                DiaryJournalFact.MainAccountDim = journal.AccountId;
                                DiaryJournalFact.Month = month;
                                DiaryJournalFact.Year = year;
                                DiaryJournalFact.OwnerOrg = transaction.OwnerOrgId;
                                DiaryJournalFact.IsBalanceForward = false;
                                DiaryJournalFact.DetailList = new List<DJ_Detail>();
                                foreach (ETL_GeneralJournal journal_dt in CreditJournalList)
                                {
                                    DJ_Detail detail = new DJ_Detail();
                                    detail.AccountDim = journal_dt.AccountId;
                                    detail.Credit = (Decimal)journal_dt.Credit;
                                    detail.Debit = 0;
                                    detail.TransactionDim = transaction.TransactionId;
                                    DiaryJournalFact.DetailList.Add(detail);
                                }
                                result.Add(DiaryJournalFact);
                            }
                            break;
                        }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public void LoadDiaryJournal(Session session, DJ_Fact DiaryJournalFact)
        { 
            try
            {
                //Get OwnerOrgDim => ownerOrgDim
                #region OwnerOrgDim
                Organization ownerOrg = session.GetObjectByKey<Organization>(DiaryJournalFact.OwnerOrg);
                if (DiaryJournalFact.OwnerOrg == Guid.Empty || ownerOrg == null)
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_Code = new BinaryOperator("Code", "QUASAPHARCO", BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                    OwnerOrgDim ownerOrgDim = session.FindObject<OwnerOrgDim>(criteria);
                }
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_RefId = new BinaryOperator("RefId", ownerOrg.OrganizationId, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RefId, criteria_RowStatus);
                    OwnerOrgDim ownerOrgDim = session.FindObject<OwnerOrgDim>(criteria);
                    if (ownerOrgDim == null)
                    {
                        ownerOrgDim = new OwnerOrgDim(session);
                        ownerOrgDim.Code = ownerOrg.Code;
                        ownerOrgDim.Description = ownerOrg.Description;
                        ownerOrgDim.Name = ownerOrg.Name;
                        ownerOrgDim.RefId = ownerOrg.OrganizationId;
                        ownerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                        ownerOrgDim.Save();
                    }
                }
                #endregion
                //Get MonthDim
                #region MonthDim
                #endregion
                //Get YearDim
                #region YearDim
                #endregion
                //Get FinancialAccountDim                
                #region FinancialAccountDim
                #endregion
                //Find Exist Fact                
                #region Find Fact
                #endregion
                //If Exist
                #region Exist Fact
                #endregion
                //If Not Exist
                #region Not Exist Fact
                #endregion
            }
            catch (Exception)
            {
                return;
            }
        }
        public void LoadDiaryJournal(Session session, List<DJ_Fact> DiaryJournalFactList)
        {
            try
            {
                foreach (DJ_Fact fact in DiaryJournalFactList)
                {
                    LoadDiaryJournal(session, fact);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
