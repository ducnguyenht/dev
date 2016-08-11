using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO
{
    public class General
    {
        short[] StateList = new short[]{};
        short[] TransitionList = new short[] { };
        short[,] MachineState = new short[,]{
                                       };

        //public double coef(Session session, Guid a, Guid b)
        //{
        //    return 0;
        //}

        //public double FullCoef(Session session, Guid ItemUnitId, Guid RelatedItemUnitId,Guid ItemId)
        //{
        //    double result = 1;
        //    ItemUnit currentUnit = session.GetObjectByKey<ItemUnit>(ItemUnitId);
        //    ItemUnit RelatedItemUnit = session.GetObjectByKey<ItemUnit>(RelatedItemUnitId);
        //    XPQuery<ItemUnit> ItemUnitQuery = session.Query<ItemUnit>();
        //    var ItemUnitList = (from c in ItemUnitQuery
        //                        where c.ItemId.ItemId == ItemId
        //                        && c.UnitId == currentUnit.UnitId
        //                        select c);
        //    var RelatedItemUnitList = (from c in ItemUnitQuery
        //                               where c.ItemId.ItemId == ItemId
        //                               && c.UnitId == RelatedItemUnit.UnitId
        //                               select c);
        //    foreach (ItemUnit itemunit in ItemUnitList)
        //    {
        //        foreach (ItemUnit Relateditemunit in ItemUnitList)
        //        {
        //            result = coef(session, itemunit.ItemUnitId, Relateditemunit.ItemUnitId);
        //            if (result != 0) return result;
        //        }
        //    }
        //    return 0;
        //}

        public void AddStateForMachine(short CurrentState, short Transition, short NewState)
        {

            return;
        }
        public short StateMachine(short CurrentState,short Transition)
        {
            int stateindex = Array.IndexOf(StateList,CurrentState);
            int Transitionindex = Array.IndexOf(TransitionList, Transition);
            if (stateindex == -1) return -1;
            return MachineState[stateindex,Transitionindex];
            // return fMachineState[CurrentState, Transition];
        }
        public static double AccountBalance(Session session,Account _Account)
        {
            XPQuery<GeneralLedger> LedgerQuery = session.Query<GeneralLedger>();
            GeneralLedger LastLedger = (from c in LedgerQuery
                                        where c.AccountId == _Account
                                        && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                                        orderby c.IssuedDate descending
                                        , c.UpdateDate descending
                                        select c).FirstOrDefault();
            if (LastLedger == null) return 0;
            return LastLedger.Balance;
        }
        public double TotalBalance(Session session, AccountType AccType)
        {
            double total = 0;            
            
            XPQuery<Account> accQuery = session.Query<Account>();            

            foreach (Account acc in accQuery.Where(r => r.AccountTypeId == AccType && r.ParentAccountId == null))
            {
                total +=AccountBalance(session,acc);                
            }
            return total;
        }
        public double TotalBalance(Session session, AccountCategory AccCategory)
        {
            double total = 0;

            XPQuery<AccountType> accQuery = session.Query<AccountType>();

            foreach (AccountType acc in accQuery.Where(r => r.AccountCategoryId == AccCategory))
            {
                total += TotalBalance(session,acc);
            }
            return total;
        }

        //public void BalanceUpdate(Session session,
        //                                    Account _Account,
        //                                    GeneralJournal _GeneralJournal,
        //                                    bool _IsBalanceForward,
        //                                    double _Debit,
        //                                    double _Credit)
        //{
        //    BalanceUpdate(session,_Account,_GeneralJournal,_IsBalanceForward,_Debit-_Credit);
        //}

        //public void BalanceUpdate(   Session session,
        //                                    Account _Account,
        //                                    GeneralJournal _GeneralJournal,
        //                                    bool _IsBalanceForward,
        //                                    double _deltaBalance)
        //{
        //    if ((_Account == null) || (_GeneralJournal == null)) return;
        //    XPQuery<GeneralLedger> generalLedgerQuery = session.Query<GeneralLedger>();
        //    GeneralLedger NewestGeneralLedger = generalLedgerQuery.Where(r => r.AccountId == _Account).OrderByDescending(c => c.CreateDate).FirstOrDefault();
        //    double CurrentBalance = 0;
        //    double nextBalance = 0;
        //    string des = "";
        //    if (NewestGeneralLedger != null)
        //    {
        //       CurrentBalance = NewestGeneralLedger.Balance;
        //    }
        //    if (_IsBalanceForward)
        //    {
        //        des = "Nhập đầu kì";                
        //    }
            
        //    nextBalance = CurrentBalance + _deltaBalance * ((_Account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT) ? -1 : 1);
            
        //    GeneralLedger GL = new GeneralLedger(session)
        //    {
        //        //IsBalanceForward =_IsBalanceForward,
        //        Balance = nextBalance, 
        //        AccountId = _Account,
        //        //GeneralJournalId = _GeneralJournal,
        //        Description = des,
        //        CreateDate = DateTime.Now
        //    };
        //    GL.Save();
        //    if (_Account.ParentAccountId != null)
        //    {
        //        XPQuery<Account> AccountQuery = session.Query<Account>();
        //        Account parentAccount = AccountQuery.Where(r => r.AccountId == _Account.ParentAccountId.AccountId).FirstOrDefault();

        //        if (parentAccount != null)
        //        {
        //            BalanceUpdate(session, parentAccount, _GeneralJournal, _IsBalanceForward, _deltaBalance);
        //        }
        //    }
        //}
    }
}
