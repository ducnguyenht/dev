using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Accounting.Period;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;
using Utility;

namespace NAS.BO.Accounting.Journal
{
    public class AccountingPeriodTypeBO
    {
        #region AccountingPeriodType
        public static bool IsUsedAccoutingPeriodType(Session session, Guid AccoutingPeriodTypeId)
        {
            bool result = true;
            try
            {
                NAS.DAL.Accounting.Period.AccountingPeriodType accoutingPeriodType = session.GetObjectByKey<AccountingPeriodType>(AccoutingPeriodTypeId);
                if (accoutingPeriodType == null)
                {
                    result = false;
                    return result;
                }
                if (accoutingPeriodType.AccountingPeriods == null || accoutingPeriodType.AccountingPeriods.Count == 0)
                {
                    result = false;
                }
            }
            catch(Exception)
            {
                return result;
            }
            return result;
        }

        public AccountingPeriodTypeBO() { }

        public bool checkAccountingPeriodType_Name(UnitOfWork session, string name)
        {
            AccountingPeriodType AccountingPeriodType_Name = session.FindObject<AccountingPeriodType>(
                CriteriaOperator.And(
                    new BinaryOperator("Name", name, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                ));
            if (AccountingPeriodType_Name != null)
            {
                return true;
            }
            return false;
        }

        public bool changeIsDefaultAccountingPeriodType(UnitOfWork session)
        {
            try
            {
                XPCollection<AccountingPeriodType> accountingPeriodType = new XPCollection<AccountingPeriodType>(session);
                accountingPeriodType.Criteria = CriteriaOperator.And(
                    new BinaryOperator("IsDefault", true, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
                    );
                foreach (AccountingPeriodType apt in accountingPeriodType)
                {
                    apt.IsDefault = false;
                    apt.Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }               

        public bool checkIsAccountingPeriodTypeIdInCurrency(UnitOfWork session, string AccountingPeriodTypeId)
        {
            AccountingPeriod dbAccountingPeriodeId = session.FindObject<AccountingPeriod>(
                CriteriaOperator.And(
                new BinaryOperator("AccountingPeriodTypeId", AccountingPeriodTypeId, BinaryOperatorType.Equal),
                 new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                ));
            if (dbAccountingPeriodeId != null)
            {
                return true;
            }
            return false;
        }

        public AccountingPeriodType GetMinAccountingPeriodType(Session session)
        {
            AccountingPeriodType result = null;
            try
            {
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IsDefault = new BinaryOperator("IsDefault",true, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_IsDefault,criteria_RowStatus);
                result = session.FindObject<AccountingPeriodType>(criteria);
            }
            catch (Exception)
            {
            }
            return result;
        }
           
        #endregion

        #region AccountingPeriod
        public bool checkAccountingPeriod_Code(UnitOfWork uow, string code, string AccPeriodTypeId)
        {
            AccountingPeriod AccountingPeriod_Code = uow.FindObject<AccountingPeriod>(
                CriteriaOperator.And(
                    new BinaryOperator("Code", code, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                    new BinaryOperator("AccountingPeriodTypeId", Guid.Parse(AccPeriodTypeId.ToString()), BinaryOperatorType.Equal)
                    ));
            if (AccountingPeriod_Code != null)
            {
                return true;
            }
            return false;
        }

        public bool changeIsActiveAccountingPeriod(UnitOfWork uow)
        {
            try
            {
                AccountingPeriod apIsActive = uow.FindObject<AccountingPeriod>( new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal));
                XPCollection<AccountingPeriod> DBAccountingPeriod = new XPCollection<AccountingPeriod>(uow);
                DBAccountingPeriod.Criteria = CriteriaOperator.And(
                    new BinaryOperator("IsActive", true, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
                    );
                if (apIsActive == null)
                    throw new Exception("AccountingPeriod is not NAAN_DEFAULT");
                if (DBAccountingPeriod == null)
                    throw new Exception("AccountingPeriod is not in system");
                foreach (AccountingPeriod ap in DBAccountingPeriod)
                {
                    if (!ap.Code.Equals(apIsActive.Code))
                    {
                        ap.IsActive = false;
                        uow.FlushChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //public void changeIsActiveParentInParentAccP(UnitOfWork uow, Guid ParentID, bool isActive)
        //{
        //    try
        //    {
        //        NAS.DAL.Accounting.Journal.AccountingPeriod parentAccPeriod = uow.GetObjectByKey<NAS.DAL.Accounting.Journal.AccountingPeriod>(ParentID);
        //        if (parentAccPeriod != null)
        //        {
        //            if (isActive)
        //            {
        //                parentAccPeriod.IsActive = true;
        //            }
        //            else
        //            {
        //                parentAccPeriod.IsActive = false;
        //            }
        //            parentAccPeriod.Save();
        //            if (parentAccPeriod.ParentAccountingPeriodId != null)
        //            {
        //                changeIsActiveParentInParentAccP(uow, parentAccPeriod.ParentAccountingPeriodId.AccountingPeriodId, isActive);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
    }
}
