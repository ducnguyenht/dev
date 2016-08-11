using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.BO.Accounting.Journal
{
    public class BalanceForwardTransactionBO
    {
        public class GeneralJournalEntity // GeneralJournal
        {
            [Size(36)]
            public string AccountCode;
            [Size(1024)]
            public string Description;            
            public double Credit;
            public double Debit;
            public double Balance;
        }

        //public byte Create(Session session
        //                    , string _AccountingPeriodCode
        //                    , DateTime _IssueDate
        //                    , string _Description
        //                    , GeneralJournalEntity[] _generaljournal
        //                    , double _Balance                
        //                    )                    
        //{
        //    try
        //    {
        //        General GeneralFunc = new General();
        //        XPQuery<AccountingPeriod> AccountingPeriodquery = session.Query<AccountingPeriod>();
        //        AccountingPeriod ActPer = AccountingPeriodquery.Where(r => r.Code == _AccountingPeriodCode).FirstOrDefault();

        //        XPQuery<Account> Accountquery = session.Query<Account>();
        //        BalanceForwardTransaction transaction = new BalanceForwardTransaction(session)
        //            {
        //                AccountingPeriodId = ActPer,
        //                IssueDate = _IssueDate,
        //                CreateDate = DateTime.Now,
        //                Description = _Description,
        //                Balance = _Balance
        //            };

        //        transaction.Save();

        //        foreach (GeneralJournalEntity gj in _generaljournal)
        //        {
        //            Account acount = Accountquery.Where(r => r.Code == gj.AccountCode).FirstOrDefault();
        //            GeneralJournalBalanceForward generalJournal = new GeneralJournalBalanceForward(session)
        //            {
        //                Description = gj.Description,
        //                AccountId = acount,
        //                TransactionId = transaction,
        //                Credit = gj.Credit,
        //                Debit = gj.Debit,
        //                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,                        
        //                Balance = _Balance
        //            };
        //            generalJournal.Save();
        //            GeneralFunc.BalanceUpdate(session, acount, generalJournal, true, _Balance);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return 1;
        //        throw;
        //    }
        //    finally
        //    {
        //        if (session != null) session.Dispose();
        //    }
        //    return 0;
        //}
    }
}
