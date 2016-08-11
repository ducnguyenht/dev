using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.BO.ETL.Accounting.FinancialLiability
{
    public class FinancialLiabilityBO
    {
        public bool IsExistInJournalList(Session session, List<ETL_GeneralJournal> journalList, string AccountCode)
        {
            ETLAccountingBO accountingBO = new ETLAccountingBO();
            if (journalList == null) return false;
            if (journalList.Count == 0) return false;
            bool result = false;
            try
            {
                Util util = new Util();
                Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
                if (account == null) return false;
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (accountingBO.IsRelateAccount(session, journal.AccountId, account.AccountId))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
        public bool IsExistInJournalList(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        {
            bool result = false;
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if (account == null) return false;
                return IsExistInJournalList(session, journalList, account.Code);
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }

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
                return GetJournal(session, journalList, account.Code);
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
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
    }
}
