using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using NAS.DAL.System.Log;
using Utility;

namespace NAS.ETLBO.System.Object
{
    public class ObjectEntryLogBO
    {
        public bool CreateObjectEntryLog(Session session, Int64 businessObjectId)
        {
            try
            {
                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                if (businessObject == null) return false;
                ObjectEntryLog objectEntryLog = new ObjectEntryLog(session);
                objectEntryLog.LastUpdatedObjectTimeStamp = DateTime.Now;
                objectEntryLog.RowStatus = Constant.ROWSTATUS_ACTIVE;
                objectEntryLog.BusinessObjectId = businessObject;
                objectEntryLog.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CreateObjectEntryLog(Session session, BusinessObject businessObject)
        {
            try
            {
                ObjectEntryLog objectEntryLog = new ObjectEntryLog(session);
                objectEntryLog.LastUpdatedObjectTimeStamp = DateTime.Now;
                objectEntryLog.RowStatus = Constant.ROWSTATUS_ACTIVE;
                objectEntryLog.BusinessObjectId = businessObject;
                objectEntryLog.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }                
        public ObjectEntryLog GetNewestObjectEntryLog(Session session, Int64 businessObjectId)
        {
            ObjectEntryLog objectEntryLog = null;
            try
            {
                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(businessObjectId);
                
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
                CriteriaOperator criteria_1 = new BinaryOperator("BusinessObjectId", businessObject, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);

                XPCollection<ObjectEntryLog> objectEntryLogCollection = new XPCollection<ObjectEntryLog>(session,criteria);

                objectEntryLogCollection.Sorting.Add(new SortProperty("LastUpdatedObjectTimeStamp", SortingDirection.Descending));
                objectEntryLog = objectEntryLogCollection.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }            
            return objectEntryLog;
        }
    }
}
