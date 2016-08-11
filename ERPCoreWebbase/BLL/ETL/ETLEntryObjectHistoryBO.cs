using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.Log;
using NAS.ETLBO.System.Object;
using NAS.DAL.ETL.ObjectLog;
using DevExpress.Data.Filtering;
using NAS.DAL.ETL;

namespace NAS.ETLBO.System.ETL
{
    public class ETLEntryObjectHistoryBO
    {        
        public void CreatETLEntryObjectHistory(Session session, Guid jobId, Int64 businessObjectId, int ErrorCode)
        {
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (businessObject == null) return;

                ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
                ETLLogBO etlLogBO = new ETLLogBO();
                ObjectEntryLog newestObjectEntryLog = objectEntryLogBO.GetNewestObjectEntryLog(session, businessObjectId);
                if (newestObjectEntryLog == null) return;

                ETLBusinessObject etlBusinessObject = etlLogBO.GetETLBusinessObject(session, jobId, businessObjectId);
                if (etlBusinessObject == null) return;

                ETLEntryObjectHistory etlEntryObjectHistory = new ETLEntryObjectHistory(session);
                etlEntryObjectHistory.ErrorCode = ErrorCode;
                etlEntryObjectHistory.ETLBusinessObjectId = etlBusinessObject;
                etlEntryObjectHistory.ObjectEntryLogId = newestObjectEntryLog;
                etlEntryObjectHistory.RowTimeStamp = DateTime.Now;
                etlEntryObjectHistory.RowStatus = 0;
                etlEntryObjectHistory.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ETLEntryObjectHistory GetETLEntryObjectHistory(Session session, Guid jobId, Int64 businessObjectId)
        {
            ETLEntryObjectHistory etlEntryObjectHistory = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (businessObject == null) return null;

                ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
                ETLLogBO etlLogBO = new ETLLogBO();
                ObjectEntryLog newestObjectEntryLog = objectEntryLogBO.GetNewestObjectEntryLog(session, businessObjectId);

                ETLBusinessObject etlBusinessObject = etlLogBO.GetETLBusinessObject(session, jobId, businessObjectId);
                if (etlBusinessObject == null) return null;

                CriteriaOperator criteria_0 = new BinaryOperator("ETLBusinessObjectId", etlBusinessObject, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator(new OperandProperty("ObjectEntryLogId.BusinessObjectId.BusinessObjectId"), businessObject.BusinessObjectId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1);

                etlEntryObjectHistory = session.FindObject<ETLEntryObjectHistory>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return etlEntryObjectHistory;

        }
        public bool SetETLEntryObjectHistoryStatus(Session session, Guid jobId, Int64 businessObjectId, short rowstatus)
        {
            bool result = true;
            try
            {
                ETLEntryObjectHistory etlEntryObjectHistory = GetETLEntryObjectHistory(session, jobId, businessObjectId);
                if (etlEntryObjectHistory == null)
                {
                    result = false;
                    return result;
                }
                etlEntryObjectHistory.RowStatus = rowstatus;
                etlEntryObjectHistory.RowTimeStamp = DateTime.Now;
                etlEntryObjectHistory.Save();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public bool SetETLEntryObjectHistoryStatus(Session session, Guid jobId, Guid RefId, short rowstatus)
        {
            bool result = true;
            try
            {
                BusinessObjectBO businessObjectBO = new BusinessObjectBO();
                BusinessObject businessObject = businessObjectBO.GetBusinessObjectByRefId(session, RefId);
                if (businessObjectBO == null) return false;
                ETLEntryObjectHistory etlEntryObjectHistory = GetETLEntryObjectHistory(session, jobId, businessObject.BusinessObjectId);
                if (etlEntryObjectHistory == null)
                {
                    result = false;
                    return result;
                }
                etlEntryObjectHistory.RowStatus = rowstatus;
                etlEntryObjectHistory.RowTimeStamp = DateTime.Now;
                etlEntryObjectHistory.Save();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public bool SetETLEntryObjectHistoryErrorCode(Session session, Guid jobId, Int64 businessObjectId, int errorCode)
        {
            bool result = true;
            try
            {
                ETLEntryObjectHistory etlEntryObjectHistory = GetETLEntryObjectHistory(session, jobId, businessObjectId);
                if (etlEntryObjectHistory == null)
                {
                    result = false;
                    return result;
                }
                etlEntryObjectHistory.ErrorCode = errorCode;
                etlEntryObjectHistory.RowTimeStamp = DateTime.Now;
                etlEntryObjectHistory.Save();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
