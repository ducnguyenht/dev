using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using Utility;
using DevExpress.Xpo.DB;

namespace NAS.BO.Accounting.Journal
{
    public class GeneralLedgerBO
    {
        public XPCollection<GeneralLedger> GetOlderGeneralLedger(Session session, Guid GeneralLedgerId)
        {
            XPCollection<GeneralLedger> result = null;
            try
            {
                GeneralLedger currentGeneralLedger = session.GetObjectByKey<GeneralLedger>(GeneralLedgerId);
                DateTime IssueDate = currentGeneralLedger.IssuedDate;
                DateTime CreateDate = currentGeneralLedger.CreateDate;
                //RowStatus >=0
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                //Datetime (ia<ib||(ia=ib & ca<=cb))
                CriteriaOperator criteria_DateTime_0 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Less);
                
                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);
                
                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_DateTime_0, criteria_DateTime_1);
                
                //<>ID
                CriteriaOperator criteria_Id = new BinaryOperator("GeneralLedgerId", currentGeneralLedger.GeneralLedgerId, BinaryOperatorType.NotEqual);

                //#Account
                CriteriaOperator criteria_Account = new BinaryOperator("AccountId", currentGeneralLedger.AccountId, BinaryOperatorType.Equal);
                
                //#Currence
                CriteriaOperator criteria_Currency = new BinaryOperator("CurrencyId", currentGeneralLedger.CurrencyId, BinaryOperatorType.Equal);
                
                //Full Criteria
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id, criteria_Account, criteria_Currency);

                result = new XPCollection<GeneralLedger>(session, criteria);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public XPCollection<GeneralLedger> GetNewerGeneralLedger(Session session, Guid GeneralLedgerId)
        {
            XPCollection<GeneralLedger> result = null;
            try
            {
                GeneralLedger currentGeneralLedger = session.GetObjectByKey<GeneralLedger>(GeneralLedgerId);
                DateTime IssueDate = currentGeneralLedger.IssuedDate;
                DateTime CreateDate = currentGeneralLedger.CreateDate;
                //RowStatus >=0
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                //Datetime (ia>ib||(ia=ib & ca>cb))
                CriteriaOperator criteria_DateTime_0 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Greater);

                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);

                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_DateTime_0, criteria_DateTime_1);

                //<>ID
                CriteriaOperator criteria_Id = new BinaryOperator("GeneralLedgerId", currentGeneralLedger.GeneralLedgerId, BinaryOperatorType.NotEqual);

                //#Account
                CriteriaOperator criteria_Account = new BinaryOperator("AccountId", currentGeneralLedger.AccountId, BinaryOperatorType.Equal);
                
                //#Currency
                CriteriaOperator criteria_Currency = new BinaryOperator("CurrencyId", currentGeneralLedger.CurrencyId, BinaryOperatorType.Equal);

                //Full Criteria
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id, criteria_Account,criteria_Currency);

                result = new XPCollection<GeneralLedger>(session, criteria);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// A is the previousLedger of B when:
        /// + ((A.IssueDate<B.IssueDate) ||(A.IssueDate=B.IssueDate && A.CreateDate<=B.CreateDate))
        /// + <>ID
        /// + RowStatus>=0
        /// + #Account
        /// </summary>
        /// <param name="session"></param>
        /// <param name="GeneralLedgerId"></param>
        /// <returns></returns>        
        public GeneralLedger GetPreviousGeneralLedger(Session session, Guid GeneralLedgerId)
        {
            GeneralLedger result = null;
            try
            {
                XPCollection<GeneralLedger> OlderGeneralLedgerList = GetOlderGeneralLedger(session, GeneralLedgerId);
                OlderGeneralLedgerList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Descending));
                OlderGeneralLedgerList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));                
                result = OlderGeneralLedgerList.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public GeneralLedger GetNextGeneralLedger(Session session, Guid GeneralLedgerId)
        {
            GeneralLedger result = null;
            try
            {
                XPCollection<GeneralLedger> NewerGeneralLedgerList = GetNewerGeneralLedger(session, GeneralLedgerId);
                NewerGeneralLedgerList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Ascending));
                NewerGeneralLedgerList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Ascending));
                result = NewerGeneralLedgerList.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
