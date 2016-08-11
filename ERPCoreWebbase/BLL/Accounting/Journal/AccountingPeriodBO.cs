using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Period;
using NAS.DAL;
using System.Globalization;
using Utility;

namespace NAS.BO.Accounting.Journal
{
    public class AccountingPeriodBO
    {
        public AccountingPeriodBO() { }
        
        public static AccountingPeriod getCurrentAccountingPeriod(Session session)
        {
            //XPQuery<AccountingPeriod> acPeriodQuery = session.Query<AccountingPeriod>();
            //var list = from c in acPeriodQuery where c.IsActive == true && c.RowStatus > 0 select c;
            //return list.FirstOrDefault();
            return GetAccountingPeriod(session, DateTime.Now);
        }
        public static AccountingPeriod GetAccountingPeriod(Session session, DateTime Date)
        {
            //CriteriaOperator criteria_0 = new BinaryOperator("Rowstatus", 0, BinaryOperatorType.Greater);
            //CriteriaOperator criteria_1 = new BinaryOperator("IsDefault", true, BinaryOperatorType.Greater);
            //CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
            //AccountingPeriodType periodType = session.FindObject<AccountingPeriodType>(criteria);            
            
            //AccountingPeriod accountingPeriod = null;
            //criteria_1 = new BinaryOperator("IsActive", true, BinaryOperatorType.Equal);
            //CriteriaOperator criteria_2 = new BinaryOperator("FromDateTime", issuedDate, BinaryOperatorType.LessOrEqual);
            //CriteriaOperator criteria_3 = new BinaryOperator("ToDateTime", issuedDate, BinaryOperatorType.GreaterOrEqual);
            //CriteriaOperator criteria_4 = new BinaryOperator("AccountingPeriodTypeId", periodType, BinaryOperatorType.Equal);
            //criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3, criteria_4);
            //XPCollection<AccountingPeriod> accountingPeriodCollection = new XPCollection<AccountingPeriod>(criteria);
            //accountingPeriod = accountingPeriodCollection.FirstOrDefault();
            //return accountingPeriod;
            
            AccountingPeriod accountingPeriod = null;
            try
            {
                //CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                //CriteriaOperator criteria_1 = new BinaryOperator("IsActive", true, BinaryOperatorType.Equal);
                //CriteriaOperator criteria_2 = new BinaryOperator("FromDateTime", issuedDate, BinaryOperatorType.LessOrEqual);
                //CriteriaOperator criteria_3 = new BinaryOperator("ToDateTime", issuedDate, BinaryOperatorType.GreaterOrEqual);
                //CriteriaOperator criteria_4 = new BinaryOperator(new OperandProperty("AccountingPeriodTypeId.IsDefault"), new OperandValue(true), BinaryOperatorType.Equal);
                //CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3, criteria_4);                
                //XPCollection<AccountingPeriod> accountingPeriodCollection = new XPCollection<AccountingPeriod>(session, criteria);
                //accountingPeriod = accountingPeriodCollection.FirstOrDefault();

                //issuedDate = DateTime.ParseExact(issuedDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                AccountingPeriodTypeBO typeBO = new AccountingPeriodTypeBO();
                AccountingPeriodType minAccountingPeriodType = typeBO.GetMinAccountingPeriodType(session);
                if (minAccountingPeriodType == null) return null;
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IsActive = new BinaryOperator("IsActive", true, BinaryOperatorType.Equal);
                CriteriaOperator criteria_FromDate = new BinaryOperator("FromDateTime", Date, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_ToDate = new BinaryOperator("ToDateTime", Date, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Type = new BinaryOperator("AccountingPeriodTypeId", minAccountingPeriodType, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_FromDate, criteria_IsActive, criteria_RowStatus, criteria_ToDate, criteria_Type);
                XPCollection<AccountingPeriod> AccountingPeriodCol = new XPCollection<AccountingPeriod>(session, criteria);

                if (AccountingPeriodCol == null) return null;
                if (AccountingPeriodCol.Count == 0) return null;
                accountingPeriod = AccountingPeriodCol.FirstOrDefault();
                //XPQuery<AccountingPeriod> qr1 = new XPQuery<AccountingPeriod>(session);

                //var col = from r in qr1
                //          where r.RowStatus >= 1
                //          && r.FromDateTime <= Date
                //          && r.ToDateTime >= Date
                //          && r.AccountingPeriodTypeId == minAccountingPeriodType
                //          select r;
                //if (col == null) return null;
                //if (col.Count() == 0) return null;
                //accountingPeriod = col.FirstOrDefault() as AccountingPeriod;
            }
            catch (Exception)
            {
                throw;
            }
            return accountingPeriod;
        }
        public static void CreatAccountingPeriodComposite(Session session, Guid accountingPeriodId, Guid childAccountingPeriodId)
        {
            AccountingPeriod parentAccountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
            AccountingPeriod childAccountingPeriod = session.GetObjectByKey<AccountingPeriod>(childAccountingPeriodId);
            if ((parentAccountingPeriod != null) && (childAccountingPeriod != null))
            {
                AccountingPeriodComposite composite = new AccountingPeriodComposite(session);
                composite.AccountingPeriodId = parentAccountingPeriod;
                composite.ChildrenAccountingPeriodId = childAccountingPeriod;
                composite.RowStatus = Constant.ROWSTATUS_ACTIVE;
                composite.Save();
            }
        }
        public static bool IsChildOfAnother(Session session, Guid accountingPeriodId)
        {
            bool result = false;
            try
            {
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(accountingPeriodId);
                if (accountingPeriodId == null) return false;
                if (accountingPeriod.AccountingPeriodComposites == null)
                {
                    result = false;
                }
                else
                {
                    XPCollection<AccountingPeriodComposite> AccountingPeriodCompositeCol = new XPCollection<AccountingPeriodComposite>(accountingPeriod.AccountingPeriodComposites, criteria_RowStatus);
                    if (AccountingPeriodCompositeCol == null)
                    {
                        result = false;
                    }
                    else
                    {
                        if (AccountingPeriodCompositeCol.Count == 0)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
    }
}
