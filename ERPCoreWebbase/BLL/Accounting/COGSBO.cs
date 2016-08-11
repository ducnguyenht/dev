using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using Utility;

namespace NAS.BO.Accounting
{
    public class COGSBO
    {
        public XPCollection<COGS> GetOlderCOGS(Session session, Guid COGSId)
        {
            XPCollection<COGS> result = null;
            try
            {
                COGS currentCOGS = session.GetObjectByKey<COGS>(COGSId);
                DateTime IssueDate = currentCOGS.IssueDate;
                DateTime CreateDate = currentCOGS.CreateDate;
                //RowStatus >=0
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                //Datetime (ia<ib||(ia=ib & ca<=cb))
                CriteriaOperator criteria_DateTime_0 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Less);

                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);

                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_DateTime_0, criteria_DateTime_1);

                //<>ID
                CriteriaOperator criteria_Id = new BinaryOperator("COGSId", currentCOGS.COGSId, BinaryOperatorType.NotEqual);

                //#ItemUnit
                CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", currentCOGS.ItemUnitId, BinaryOperatorType.Equal);
                //#Currency
                CriteriaOperator criteria_Currency = new BinaryOperator("CurrencyId", currentCOGS.CurrencyId, BinaryOperatorType.Equal);

                //Full Criteria
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id, criteria_ItemUnit, criteria_Currency);

                result = new XPCollection<COGS>(session, criteria);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public XPCollection<COGS> GetNewerCOGS(Session session, Guid COGSId)
        {
            XPCollection<COGS> result = null;
            try
            {
                COGS currentCOGS = session.GetObjectByKey<COGS>(COGSId);
                DateTime IssueDate = currentCOGS.IssueDate;
                DateTime CreateDate = currentCOGS.CreateDate;
                //RowStatus >=0
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);

                //Datetime (ia>ib||(ia=ib & ca>cb))
                CriteriaOperator criteria_DateTime_0 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Greater);

                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("IssuedDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", CreateDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_CreateDate);

                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_DateTime_0, criteria_DateTime_1);

                //<>ID
                CriteriaOperator criteria_Id = new BinaryOperator("COGSId", currentCOGS.COGSId, BinaryOperatorType.NotEqual);

                //#ItemUnit
                CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", currentCOGS.ItemUnitId, BinaryOperatorType.Equal);
                
                //#Currency
                CriteriaOperator criteria_Currency = new BinaryOperator("CurrencyId", currentCOGS.CurrencyId, BinaryOperatorType.Equal);

                //Full Criteria
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id, criteria_ItemUnit, criteria_Currency);

                result = new XPCollection<COGS>(session, criteria);
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
        /// <param name="COGSId"></param>
        /// <returns></returns>        
        public COGS GetPreviousCOGS(Session session, Guid COGSId)
        {
            COGS result = null;
            try
            {
                XPCollection<COGS> OlderCOGSList = GetOlderCOGS(session, COGSId);
                OlderCOGSList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Descending));
                OlderCOGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                result = OlderCOGSList.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public COGS GetNextCOGS(Session session, Guid COGSId)
        {
            COGS result = null;
            try
            {
                XPCollection<COGS> NewerCOGSList = GetNewerCOGS(session, COGSId);
                NewerCOGSList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Ascending));
                NewerCOGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Ascending));
                result = NewerCOGSList.FirstOrDefault();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public COGS GetLastCOGS(Session session, Guid ItemUnitId, Guid CurrencyId, Guid InventoryId)
        {
            COGS result = null;
            try
            {
                CriteriaOperator filter = CriteriaOperator.And(
                        new BinaryOperator("ItemUnitId!Key", ItemUnitId),
                        new BinaryOperator("CurrencyId!Key", CurrencyId),
                        new BinaryOperator("InventoryId!Key", InventoryId));
                XPCollection<COGS> COGSList = new XPCollection<COGS>(session, filter);
                COGSList.Sorting.Add(new SortProperty("IssueDate", SortingDirection.Descending));
                COGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                result = COGSList.FirstOrDefault();
                /////////////////////////////////////////
                if (result == null)
                {
                    filter = CriteriaOperator.And(
                        new BinaryOperator("ItemUnitId!Key", ItemUnitId),
                        new BinaryOperator("InventoryId!Key", InventoryId));
                    COGSList = new XPCollection<COGS>(session, filter);
                    COGSList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Descending));
                    COGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                    result = COGSList.FirstOrDefault();
                }
                /////////////////////////////////////////
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public COGS GetLastCOGSByIssueDate(Session session, Guid ItemUnitId, Guid CurrencyId, Guid InventoryId, DateTime IssueDate)
        {
            COGS result = null;
            try
            {
                CriteriaOperator filter = CriteriaOperator.And(
                        new BinaryOperator("ItemUnitId!Key", ItemUnitId),
                        new BinaryOperator("CurrencyId!Key", CurrencyId),
                        new BinaryOperator("InventoryId!Key", InventoryId),
                        new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.LessOrEqual));
                XPCollection<COGS> COGSList = new XPCollection<COGS>(session, filter);
                COGSList.Sorting.Add(new SortProperty("IssueDate", SortingDirection.Descending));
                COGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                result = COGSList.FirstOrDefault();
                /////////////////////////////////////////
                if (result == null)
                {
                    filter = CriteriaOperator.And(
                        new BinaryOperator("ItemUnitId!Key", ItemUnitId),
                        new BinaryOperator("InventoryId!Key", InventoryId));
                    COGSList = new XPCollection<COGS>(session, filter);
                    COGSList.Sorting.Add(new SortProperty("IssuedDate", SortingDirection.Descending));
                    COGSList.Sorting.Add(new SortProperty("CreateDate", SortingDirection.Descending));
                    result = COGSList.FirstOrDefault();
                }
                /////////////////////////////////////////
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
