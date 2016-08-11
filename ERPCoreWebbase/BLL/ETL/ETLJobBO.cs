using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using NAS.ETLBO.System.Object;
using NAS.DAL.System.Log;
using NAS.DAL.ETL;

namespace NAS.ETLBO.System.ETL
{
    public class ETLJobBO
    {
        public ETLBusinessObject GetLastProcessedObject(Session session, Guid jobId)
        {
            ETLBusinessObject etlBusinessObject = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                CriteriaOperator criteria_0 = new BinaryOperator("ETLJobId",job,BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus",0,BinaryOperatorType.Greater);       
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And,criteria_0,criteria_1);
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = new XPCollection<ETLBusinessObject>(criteria);
                etlBusinessObjectCollection.Sorting.Add(new SortProperty("BusinessObjectIssuedDateTimeStamp",SortingDirection.Descending));
                etlBusinessObject = etlBusinessObjectCollection.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObject;
        }
        public Guid ETLGetNextProcessObject(Session session, Guid jobId, int objectType)
        {
            Guid result = Guid.Empty;
            ETLEntryObjectHistoryBO entryHistoryBO = new ETLEntryObjectHistoryBO();
            BusinessObjectBO businessObjectBO = new BusinessObjectBO();
            ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
            ETLLogBO etlLogBO = new ETLLogBO();            
            try
            {
                #region Logic
                BusinessObject businessObject = businessObjectBO.GetOldestUnprocessedObject(session, jobId, objectType);
                if (businessObject == null) return result;
                result = businessObject.RefId;
                etlLogBO.CreateETLBusinessObject(session, jobId, businessObject.BusinessObjectId);
                entryHistoryBO.CreatETLEntryObjectHistory(session, jobId, businessObject.BusinessObjectId, 0);
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = etlLogBO.GetNewerETLBusinessObject(session, jobId, businessObject.BusinessObjectId);
                foreach (ETLBusinessObject etlBO in etlBusinessObjectCollection)
                {
                    etlBO.RowStatus = -2;
                    etlBO.Save();
                }
                #endregion
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public Guid ETLGetNextProcessObject(Session session, Guid jobId, List<int> objectType)
        {
            Guid result = Guid.Empty;
            ETLEntryObjectHistoryBO entryHistoryBO = new ETLEntryObjectHistoryBO();
            BusinessObjectBO businessObjectBO = new BusinessObjectBO();
            ObjectEntryLogBO objectEntryLogBO = new ObjectEntryLogBO();
            ETLLogBO etlLogBO = new ETLLogBO();
            try
            {
                #region Logic
                BusinessObject businessObject = businessObjectBO.GetOldestUnprocessedObject(session, jobId, objectType);
                if (businessObject == null) return result;
                result = businessObject.RefId;
                etlLogBO.CreateETLBusinessObject(session, jobId, businessObject.BusinessObjectId);
                entryHistoryBO.CreatETLEntryObjectHistory(session, jobId, businessObject.BusinessObjectId, 0);
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = etlLogBO.GetNewerETLBusinessObject(session, jobId, businessObject.BusinessObjectId);
                foreach (ETLBusinessObject etlBO in etlBusinessObjectCollection)
                {
                    etlBO.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    etlBO.Save();
                }
                #endregion
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
    }
}
