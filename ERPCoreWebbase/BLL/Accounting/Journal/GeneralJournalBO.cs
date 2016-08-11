using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Accounting.Journal;
using DevExpress.Xpo;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL;

namespace NAS.BO.Accounting.Journal
{
    public enum Side
    {
        DEBIT,
        CREDIT
    }

    public sealed class JounalTypeFlag
    {
        private char value;

        public static readonly JounalTypeFlag ACTUAL =
            new JounalTypeFlag(JounalTypeConstant.ACTUAL);
        public static readonly JounalTypeFlag PLANNING =
            new JounalTypeFlag(JounalTypeConstant.PLANNING);

        private JounalTypeFlag(char v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public char Value { get { return value; } }
    }

    public class GeneralJournalBO
    {
        public GeneralJournal CreateGeneralJournal(
                Session session,
                Guid transactionId,
                Guid accountId,
                Side side,
                double amount,
                string description,
                JounalTypeFlag journalType
            )
        {
            try
            {
                Transaction transaction = session.GetObjectByKey<Transaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found transaction");
                }

                Account account = session.GetObjectByKey<Account>(accountId);

                double debit = 0;
                double credit = 0;
                switch (side)
                {
                    case Side.DEBIT:
                        debit = amount;
                        break;
                    case Side.CREDIT:
                        credit = amount;
                        break;
                    default:
                        break;
                }

                GeneralJournal generalJournal = new GeneralJournal(session)
                {
                    AccountId = account,
                    CreateDate = DateTime.Now,
                    Credit = credit,
                    Debit = debit,
                    Description = description,
                    JournalType = journalType.Value,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    TransactionId = transaction
                };
                generalJournal.Save();
                return generalJournal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GeneralJournal CreateGeneralJournal(
                Session session,
                Transaction transaction,
                Guid accountId,
                Side side,
                double amount,
                string description,
                JounalTypeFlag journalType
            )
        {
            try
            {

                if (transaction == null)
                {
                    throw new Exception("Could not found transaction");
                }

                Account account = session.GetObjectByKey<Account>(accountId);

                double debit = 0;
                double credit = 0;
                switch (side)
                {
                    case Side.DEBIT:
                        debit = amount;
                        break;
                    case Side.CREDIT:
                        credit = amount;
                        break;
                    default:
                        break;
                }

                GeneralJournal generalJournal = new GeneralJournal(session)
                {
                    AccountId = account,
                    CreateDate = DateTime.Now,
                    Credit = credit,
                    Debit = debit,
                    Description = description,
                    JournalType = journalType.Value,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    TransactionId = transaction
                };
                generalJournal.Save();
                return generalJournal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GeneralJournal UpdateGeneralJournal(
                Session session,
                Guid generalJournalId,
                Guid accountId,
                Side side,
                double amount,
                string description
            )
        {
            try
            {
                Account account = session.GetObjectByKey<Account>(accountId);

                double debit = 0;
                double credit = 0;
                switch (side)
                {
                    case Side.DEBIT:
                        debit = amount;
                        break;
                    case Side.CREDIT:
                        credit = amount;
                        break;
                    default:
                        break;
                }

                GeneralJournal generalJournal =
                    session.GetObjectByKey<GeneralJournal>(generalJournalId);

                generalJournal.AccountId = account;
                generalJournal.Credit = credit;
                generalJournal.Debit = debit;
                generalJournal.Description = description;

                generalJournal.Save();

                return generalJournal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteGeneralJounal(Guid generalJournalId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                GeneralJournal generalJournal =
                   session.GetObjectByKey<GeneralJournal>(generalJournalId);
                if (generalJournal == null)
                {
                    return false;
                }
                generalJournal.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                generalJournal.Save();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }                
    }
}
