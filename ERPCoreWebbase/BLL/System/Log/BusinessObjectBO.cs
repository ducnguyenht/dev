using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using NAS.ETLBO.System.ETL;
using NAS.DAL;
using NAS.DAL.System.Log;
using NAS.DAL.ETL;
using NAS.DAL.ETL.ObjectLog;
using Utility;
using NAS.DAL.Accounting.Journal;

namespace NAS.ETLBO.System.Object
{
    public class BusinessObjectBO
    {
        public BusinessObject GetPreviousBusinessObject(Session session, Int64 businessObjectId,List<int> ObjectType)
        {
            BusinessObject result = null;
            try
            {
                BusinessObject currentBusinessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (currentBusinessObject == null) return null;
                DateTime IssueDate = currentBusinessObject.ObjectIssueDate;
                DateTime TimeStamp = currentBusinessObject.IssuedDateTimeStamp;
                CriteriaOperator criteria_ObjectType = new InOperator("ObjectType", ObjectType);
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator("ObjectIssueDate", IssueDate, BinaryOperatorType.Less);
                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("ObjectIssueDate", IssueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_IssuedDateTimeStamp = new BinaryOperator("IssuedDateTimeStamp", TimeStamp, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_IssuedDateTimeStamp);
                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_IssueDate_1, criteria_DateTime_1);

                CriteriaOperator criteria_Id = new BinaryOperator("BusinessObjectId", currentBusinessObject.BusinessObjectId, BinaryOperatorType.NotEqual);

                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_RowStatus, criteria_DateTime, criteria_Id, criteria_ObjectType);

                XPCollection<BusinessObject> BusinessObjectCol = new XPCollection<BusinessObject>(session, criteria);
                if(BusinessObjectCol == null || BusinessObjectCol.Count == 0) return null;
                BusinessObjectCol.Sorting.Add(new SortProperty("ObjectIssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                BusinessObjectCol.Sorting.Add(new SortProperty("IssuedDateTimeStamp", DevExpress.Xpo.DB.SortingDirection.Descending));
                //BusinessObjectCol.OrderByDescending(r => r.ObjectIssueDate);
                //BusinessObjectCol.OrderByDescending(r => r.IssuedDateTimeStamp);
                result = BusinessObjectCol.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public BusinessObject GetNextBusinessObject(Session session, Int64 businessObjectId, List<int> ObjectType)
        {
            BusinessObject result = null;
            try
            {
                BusinessObject currentBusinessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (currentBusinessObject == null) return null;
                DateTime IssueDate = currentBusinessObject.ObjectIssueDate;
                DateTime TimeStamp = currentBusinessObject.IssuedDateTimeStamp;

                CriteriaOperator criteria_ObjectType = new InOperator("ObjectType", ObjectType);
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator("ObjectIssueDate", IssueDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_IssueDate_2 = new BinaryOperator("ObjectIssueDate", IssueDate, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IssuedDateTimeStamp = new BinaryOperator("IssuedDateTimeStamp", TimeStamp, BinaryOperatorType.Greater);
                CriteriaOperator criteria_DateTime_1 = new GroupOperator(GroupOperatorType.And, criteria_IssueDate_2, criteria_IssuedDateTimeStamp);
                CriteriaOperator criteria_DateTime = new GroupOperator(GroupOperatorType.Or, criteria_IssueDate_1, criteria_DateTime_1);

                CriteriaOperator criteria_Id = new BinaryOperator("BusinessObjectId", currentBusinessObject.BusinessObjectId, BinaryOperatorType.NotEqual);

                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And,criteria_ObjectType, criteria_RowStatus, criteria_DateTime, criteria_Id);

                XPCollection<BusinessObject> BusinessObjectCol = new XPCollection<BusinessObject>(session, criteria);
                if (BusinessObjectCol == null || BusinessObjectCol.Count == 0) return null;
                BusinessObjectCol.Sorting.Add(new SortProperty("ObjectIssueDate", DevExpress.Xpo.DB.SortingDirection.Ascending));
                BusinessObjectCol.Sorting.Add(new SortProperty("IssuedDateTimeStamp", DevExpress.Xpo.DB.SortingDirection.Ascending));
                //BusinessObjectCol.OrderByDescending(r => r.ObjectIssueDate);
                //BusinessObjectCol.OrderByDescending(r => r.IssuedDateTimeStamp);
                result = BusinessObjectCol.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public void CreateBusinessObject(Session session,int objectType, Guid refId, DateTime issuedDate)
        {
            try
            {
                if (!Util.IsExistXpoObject<BusinessObject>(session,"RefId", refId))
                {
                    BusinessObject businessObject = new BusinessObject(session);
                    businessObject.IssuedDateTimeStamp = DateTime.Now;
                    businessObject.ObjectIssueDate = issuedDate;
                    businessObject.ObjectType = objectType;
                    businessObject.RefId = refId;
                    businessObject.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
                    bool check = objectEntryLogBO.CreateObjectEntryLog(session, businessObject);
                    if (!check) return;
                    businessObject.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateBusinessObject(Session session,int objectType, Guid refId, DateTime issuedDate)
        {
            try
            {
                if (!Util.IsExistXpoObject<BusinessObject>(session, "RefId", refId))
                {
                    CreateBusinessObject(session, objectType, refId, issuedDate);
                }
                else
                {
                    CriteriaOperator criteria_0 = new BinaryOperator("RefId", refId, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                    CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                    BusinessObject businessObject = session.FindObject<BusinessObject>(criteria);
                    if (businessObject.IssuedDateTimeStamp != issuedDate)
                    {
                        UpdateBusinessObjectIssuedDate(session, businessObject.BusinessObjectId, issuedDate);
                    }
                    else
                    {
                        ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
                        objectEntryLogBO.CreateObjectEntryLog(session, businessObject.BusinessObjectId);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Can't Update");
            }
        }
        //public void DeleteBusinessObject(Session session, int objectType, Guid refId)
        //{
        //    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus",Constant.ROWSTATUS_ACTIVE,BinaryOperatorType.GreaterOrEqual);
        //    CriteriaOperator criteria_ObjectType = new BinaryOperator("ObjectType",objectType,BinaryOperatorType.Equal);
        //    CriteriaOperator criteria_RefId = new BinaryOperator("RefId",refId,BinaryOperatorType.Equal);
        //    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus,criteria_ObjectType, criteria_RefId);

        //    BusinessObject currentBusinessObject = session.FindObject<BusinessObject>(criteria);
        //    if(currentBusinessObject == null) return;
        //    BusinessObject previousObject = GetPreviousBusinessObject(session, currentBusinessObject.BusinessObjectId);
        //    BusinessObject nextBusinessObject = GetNextBusinessObject(session, currentBusinessObject.BusinessObjectId);
        //    currentBusinessObject.RowStatus = Constant.ROWSTATUS_DELETED;
        //    currentBusinessObject.Save();
        //    if (previousObject != null)
        //    {
        //        UpdateBusinessObject(session, objectType, previousObject.RefId, previousObject.ObjectIssueDate);
        //    }
        //    if (nextBusinessObject != null)
        //    {
        //        UpdateBusinessObject(session, objectType, nextBusinessObject.RefId, nextBusinessObject.ObjectIssueDate);
        //    }
        //}
        public void UpdateBusinessObjectIssuedDate(Session session, Int64 businessObjectId, DateTime issuedDate)
        {
            try
            {
                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (businessObject == null) return;
                businessObject.IssuedDateTimeStamp = DateTime.Now;
                businessObject.ObjectIssueDate = issuedDate;
                businessObject.Save();
                ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
                objectEntryLogBO.CreateObjectEntryLog(session, businessObject.BusinessObjectId);
            }
            catch (Exception)
            {
            }
        }
        public bool IsExistETLEntryObjectHistory(Session session, Guid jobId, Int64 objectEntryLogId)
        {
            bool result = false;
            try
            {                
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                ObjectEntryLog objectEntryLog = session.GetObjectByKey<ObjectEntryLog>(objectEntryLogId);

                CriteriaOperator criteria_0 = new BinaryOperator(new OperandProperty("ETLBusinessObjectId.ETLJobId.ETLJobId"),job.ETLJobId, BinaryOperatorType.Equal);                
                CriteriaOperator criteria_1 = new BinaryOperator("ObjectEntryLogId", objectEntryLog, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1,criteria_2);

                ETLEntryObjectHistory etlEntryObjectHistory = session.FindObject<ETLEntryObjectHistory>(criteria);
                if (etlEntryObjectHistory != null)
                {
                    result = true;
                }

            }catch(Exception)
            {
                throw;
            }
            return result;
        }
        public bool NeedToBeProcessed(Session session, Int64 ObjectId, Guid jobId)
        {
            bool result = false;            
            BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(ObjectId);
            if (businessObject == null) return false;
            
            ETLJob etlJob = session.GetObjectByKey<ETLJob>(jobId);
            if (etlJob == null) return false;
            
            CriteriaOperator criteria_0 = new BinaryOperator("ETLJobId", etlJob, BinaryOperatorType.Equal);
            CriteriaOperator criteria_1 = new BinaryOperator("BusinessObjectId", businessObject, BinaryOperatorType.Equal);
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1,criteria_2);

            ETLBusinessObject etlBusinessObject = session.FindObject<ETLBusinessObject>(criteria);
            if (etlBusinessObject == null) return true;
            ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();            
            ObjectEntryLog objectEntryLog = objectEntryLogBO.GetNewestObjectEntryLog(session, businessObject.BusinessObjectId);
            if (objectEntryLog == null)
            {
                objectEntryLogBO.CreateObjectEntryLog(session, businessObject);
                objectEntryLog = objectEntryLogBO.GetNewestObjectEntryLog(session, businessObject.BusinessObjectId);
            }

            if (!IsExistETLEntryObjectHistory(session, jobId, objectEntryLog.ObjectEntryLogId))
            {
                return true;
            }

            return result;
        }

        public XPCollection<BusinessObject> GetAllBussinessObjectsAfterSpecificTransaction(
            Session session, 
            Guid jobId,
            List<int> ObjectType, 
            Guid RefId)
        {
            XPCollection<BusinessObject> businessObjectCollection = null;
            try
            {
                BusinessObject SpecificBussinessObject = GetBusinessObjectByRefId(session, RefId);
                if (SpecificBussinessObject == null)
                    return null;

                CriteriaOperator criteria_0 = new InOperator("ObjectType", ObjectType);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                CriteriaOperator criteria_2 = new BinaryOperator(
                        "ObjectIssueDate",
                        SpecificBussinessObject.ObjectIssueDate,
                        BinaryOperatorType.GreaterOrEqual);

                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
                businessObjectCollection = new XPCollection<BusinessObject>(session, criteria);

                if (businessObjectCollection == null || businessObjectCollection.Count == 0)
                    return null;

                businessObjectCollection.Sorting.Add(new SortProperty("ObjectIssueDate", SortingDirection.Ascending));
                businessObjectCollection.Sorting.Add(new SortProperty("IssuedDateTimeStamp", SortingDirection.Ascending));
            }

            catch (Exception)
            {
                return null;
            }
            return businessObjectCollection;
        }


        public BusinessObject GetOldestUnprocessedObject(Session session, Guid jobId, int ObjectType)
        {
            BusinessObject oldestObject = null;
            try
            {
                ETLJob etlJob = session.GetObjectByKey<ETLJob>(jobId);
                if (etlJob == null) return null;
                
                ETLLogBO etlLogBO = new ETLLogBO();
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = etlLogBO.GetChangedIssuedDateETLBusinessObject(session, jobId);
                if (etlBusinessObjectCollection != null && etlBusinessObjectCollection.Count() !=0)
                {
                    etlBusinessObjectCollection.Sorting.Add(new SortProperty("BusinessObjectIssuedDateTimeStamp", SortingDirection.Ascending));
                    ETLBusinessObject oldestETLBusinessObject = etlBusinessObjectCollection.FirstOrDefault();
                    etlBusinessObjectCollection = etlLogBO.GetNewerETLBusinessObject(session, jobId, oldestETLBusinessObject.BusinessObjectIssuedDateTimeStamp);
                    foreach (ETLBusinessObject EBO in etlBusinessObjectCollection)
                    {
                        EBO.RowStatus = -2;
                        EBO.Save();
                    }
                    oldestETLBusinessObject.RowStatus = -2;
                    oldestETLBusinessObject.Save();
                }

                CriteriaOperator criteria_0 = new BinaryOperator("ObjectType", ObjectType, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                XPCollection<BusinessObject> businessObjectCollection = new XPCollection<BusinessObject>(session,criteria);
                businessObjectCollection.Sorting.Add(new SortProperty("ObjectIssueDate", SortingDirection.Ascending));
                foreach (BusinessObject bo in businessObjectCollection)
                {
                    if (NeedToBeProcessed(session, bo.BusinessObjectId, jobId))
                    {                       
                        return bo;
                    } 
                }
            }
            catch (Exception)
            {
                throw;
            }
            return oldestObject;
        }
        public BusinessObject GetOldestUnprocessedObject(Session session, Guid jobId, List<int> ObjectType)
        {
            BusinessObject oldestObject = null;
            try
            {
                ETLJob etlJob = session.GetObjectByKey<ETLJob>(jobId);
                if (etlJob == null) return null;

                ETLLogBO etlLogBO = new ETLLogBO();
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = etlLogBO.GetChangedIssuedDateETLBusinessObject(session, jobId);
                if (etlBusinessObjectCollection != null && etlBusinessObjectCollection.Count() != 0)
                {
                    etlBusinessObjectCollection.Sorting.Add(new SortProperty("BusinessObject.IssuedDateTimeStamp", SortingDirection.Ascending));
                    ETLBusinessObject oldestETLBusinessObject = etlBusinessObjectCollection.FirstOrDefault();
                    etlBusinessObjectCollection = etlLogBO.GetNewerETLBusinessObject(session, jobId, oldestETLBusinessObject.BusinessObjectIssuedDateTimeStamp);
                    foreach (ETLBusinessObject EBO in etlBusinessObjectCollection)
                    {
                        EBO.RowStatus = -2;
                        EBO.Save();
                    }
                    oldestETLBusinessObject.RowStatus = -2;
                    oldestETLBusinessObject.Save();
                }

                CriteriaOperator criteria_0 = new InOperator("ObjectType", ObjectType);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                XPCollection<BusinessObject> businessObjectCollection = new XPCollection<BusinessObject>(session, criteria);
                businessObjectCollection.Sorting.Add(new SortProperty("ObjectIssueDate", SortingDirection.Ascending));
                businessObjectCollection.Sorting.Add(new SortProperty("IssuedDateTimeStamp", SortingDirection.Ascending));
                foreach (BusinessObject bo in businessObjectCollection)
                {
                    if (NeedToBeProcessed(session, bo.BusinessObjectId, jobId))
                    {
                        return bo;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return oldestObject;
        }        
        public BusinessObject GetBusinessObjectByRefId(Session session, Guid RefId)
        {
            BusinessObject businessObject = null;
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                                        new BinaryOperator("RefId", RefId, BinaryOperatorType.Equal),
                                        new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));
                businessObject = session.FindObject<BusinessObject>(criteria);
            }
            catch (Exception)
            {
                return businessObject;
            }
            return businessObject;
        }        
    }
}
