using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.Log;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using NAS.DAL.ETL.Log;
using NAS.ETLBO.System.Object;
using NAS.DAL.ETL;
using NAS.DAL.System.ShareDim;
using Utility;

namespace NAS.ETLBO.System.ETL
{
    public class ETLLogBO
    {        
        public void CreateETLBusinessObject(Session session, Guid ETLJobId, Int64 ObjectId)
        {
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(ETLJobId);
                if (job == null) return;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(ObjectId);
                if (businessObject == null) return;

                CriteriaOperator criteria_0 = new BinaryOperator("ETLJobId", job, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("BusinessObjectId", businessObject, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1,criteria_2);

                ETLBusinessObject etlBusinessObject = session.FindObject<ETLBusinessObject>(criteria);

                if (etlBusinessObject != null)
                {
                    etlBusinessObject.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    etlBusinessObject.BusinessObjectIssuedDateTimeStamp = businessObject.ObjectIssueDate;
                    etlBusinessObject.Save();
                    return;
                }

                etlBusinessObject = new ETLBusinessObject(session);
                etlBusinessObject.BusinessObjectId = businessObject;
                etlBusinessObject.ETLJobId = job;
                etlBusinessObject.RowStatus = Constant.ROWSTATUS_ACTIVE;
                etlBusinessObject.BusinessObjectIssuedDateTimeStamp = businessObject.ObjectIssueDate;
                etlBusinessObject.Save();
                ETLBusinessObject LastETLBusinessObject = GetNearestETLBusinessObject(session, ETLJobId, ObjectId);
                etlBusinessObject.PreviousETLBusinessObjectId = LastETLBusinessObject;
                etlBusinessObject.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ETLBusinessObject GetETLBusinessObject(Session session, Guid jobId, Int64 ObjectId)
        {
            ETLBusinessObject etlBusinessObject = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(ObjectId);
                if (businessObject == null) return null;

                CriteriaOperator criteria_0 = new BinaryOperator("ETLJobId", job, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("BusinessObjectId", businessObject, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1,criteria_2);

                etlBusinessObject = session.FindObject<ETLBusinessObject>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObject;
        }
        public bool SetETLBusinessObjectStatus(Session session, Guid jobId, Int64 ObjectId, short rowstatus)
        {
            bool result = true;
            try
            {
                ETLBusinessObject etlBusinessObject = GetETLBusinessObject(session, jobId, ObjectId);
                if (etlBusinessObject == null)
                {
                    result = false;
                    return result;
                }
                etlBusinessObject.RowStatus = rowstatus;
                etlBusinessObject.Save();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public bool SetETLBusinessObjectStatus(Session session, Guid jobId, Guid RefId, short rowstatus)
        {
            bool result = true;
            try
            {
                BusinessObjectBO businessObjectBO = new BusinessObjectBO();
                BusinessObject businessObject = businessObjectBO.GetBusinessObjectByRefId(session, RefId);
                if (businessObjectBO == null) return false;
                ETLBusinessObject etlBusinessObject = GetETLBusinessObject(session, jobId, businessObject.BusinessObjectId);
                if (etlBusinessObject == null)
                {
                    result = false;
                    return result;
                }
                etlBusinessObject.RowStatus = rowstatus;
                etlBusinessObject.Save();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public XPCollection<ETLBusinessObject> GetNewerETLBusinessObject(Session session, Guid jobId, DateTime time)
        {
            XPCollection<ETLBusinessObject> etlBusinessObjectCollection = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;

                CriteriaOperator criteria_0 = new BinaryOperator("ETLJobId", job, BinaryOperatorType.Equal);                
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("BusinessObjectIssuedDateTimeStamp", time, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria = GroupOperator.Combine(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);

                etlBusinessObjectCollection = new XPCollection<ETLBusinessObject>(session,criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObjectCollection;
        }
        public XPCollection<ETLBusinessObject> GetNewerETLBusinessObject(Session session, Guid jobId, Int64 ObjectId)
        {
            XPCollection<ETLBusinessObject> etlBusinessObjectCollection = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(ObjectId);
                if (businessObject == null) return null;

                DateTime issueDate = businessObject.ObjectIssueDate;
                DateTime createDate = businessObject.IssuedDateTimeStamp;

                CriteriaOperator criteria_Job = new BinaryOperator("ETLJobId", job, BinaryOperatorType.Equal);
                //DateTime
                CriteriaOperator criteria_IssueDate = new BinaryOperator(new OperandProperty("BusinessObjectId.ObjectIssueDate"), issueDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator(new OperandProperty("BusinessObjectId.ObjectIssueDate"), issueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("BusinessObjectId.IssuedDateTimeStamp", createDate, BinaryOperatorType.Greater);
                CriteriaOperator criteria_Datetime = CriteriaOperator.Or(
                                                    criteria_IssueDate,
                                                    CriteriaOperator.And(criteria_IssueDate_1, criteria_CreateDate));

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_BusinessObjectId = new BinaryOperator(new OperandProperty("BusinessObjectId.BusinessObjectId"), businessObject.BusinessObjectId, BinaryOperatorType.NotEqual);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Job, criteria_Datetime, criteria_RowStatus, criteria_BusinessObjectId);

                etlBusinessObjectCollection = new XPCollection<ETLBusinessObject>(session,criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObjectCollection;
        }        
        public XPCollection<ETLBusinessObject> GetOlderETLBusinessObject(Session session, Guid jobId, Int64 ObjectId)
        {
            XPCollection<ETLBusinessObject> etlBusinessObjectCollection = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;

                BusinessObject businessObject = session.GetObjectByKey<BusinessObject>(ObjectId);
                if (businessObject == null) return null;

                DateTime issueDate = businessObject.ObjectIssueDate;
                DateTime createDate = businessObject.IssuedDateTimeStamp;

                CriteriaOperator criteria_Job = new BinaryOperator("ETLJobId", job, BinaryOperatorType.Equal);
                //DateTime
                CriteriaOperator criteria_IssueDate = new BinaryOperator(new OperandProperty("BusinessObjectId.ObjectIssueDate"), issueDate, BinaryOperatorType.Less);
                CriteriaOperator criteria_IssueDate_1 = new BinaryOperator(new OperandProperty("BusinessObjectId.ObjectIssueDate"), issueDate, BinaryOperatorType.Equal);
                CriteriaOperator criteria_CreateDate = new BinaryOperator("BusinessObjectId.IssuedDateTimeStamp", createDate, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_Datetime = CriteriaOperator.Or(
                                                    criteria_IssueDate,
                                                    CriteriaOperator.And(criteria_IssueDate_1, criteria_CreateDate));

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_BusinessObjectId = new BinaryOperator(new OperandProperty("BusinessObjectId.BusinessObjectId"), businessObject.BusinessObjectId, BinaryOperatorType.NotEqual);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Job, criteria_Datetime, criteria_RowStatus, criteria_BusinessObjectId);

                etlBusinessObjectCollection = new XPCollection<ETLBusinessObject>(session,criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObjectCollection;
        }
        public XPCollection<ETLBusinessObject> GetChangedIssuedDateETLBusinessObject(Session session, Guid jobId)
        {
            XPCollection<ETLBusinessObject> ChangedIssuedDateETLBusinessObjectCollection = null;
            try
            {
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return null;
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator(new OperandProperty("ETLJobId.ETLJobId"), job.ETLJobId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator(new OperandProperty("BusinessObjectId.ObjectIssueDate"), new OperandProperty("BusinessObjectIssuedDateTimeStamp"), BinaryOperatorType.NotEqual);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
                ChangedIssuedDateETLBusinessObjectCollection = new XPCollection<ETLBusinessObject>(session,criteria);
            }
            catch (Exception)
            {
                return ChangedIssuedDateETLBusinessObjectCollection;
                throw;
            }
            return ChangedIssuedDateETLBusinessObjectCollection;
        }
        public ETLBusinessObject GetNearestETLBusinessObject(Session session, Guid jobId, Int64 ObjectId)
        {
            ETLBusinessObject etlBusinessObject = null;
            try
            {
                XPCollection<ETLBusinessObject> etlBusinessObjectCollection = GetOlderETLBusinessObject(session, jobId, ObjectId);
                if (etlBusinessObjectCollection == null) return null;
                else
                {
                    etlBusinessObjectCollection.Sorting.Add(new SortProperty("BusinessObjectIssuedDateTimeStamp",SortingDirection.Descending));
                    etlBusinessObject = etlBusinessObjectCollection.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return etlBusinessObject;
        }

        #region Dimension
        public void CreatDayDim(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                DayDim day = session.FindObject<DayDim>(criteria);
                if (day == null)
                {
                    day = new DayDim(session);
                    day.Name = Name;
                    day.Description = Description;
                    day.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    day.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DayDim GetDayDim(Session session, string Name)
        {
            DayDim day = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                day = session.FindObject<DayDim>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return day;
        }
        public void CreatMonthDim(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                MonthDim month = session.FindObject<MonthDim>(criteria);
                if (month == null)
                {
                    month = new MonthDim(session);
                    month.Name = Name;
                    month.Description = Description;
                    month.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    month.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MonthDim GetMonthDim(Session session, string Name)
        {
            MonthDim Month = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                Month = session.FindObject<MonthDim>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return Month;
        }
        public void CreatYearDim(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                YearDim Year = session.FindObject<YearDim>(criteria);
                if (Year == null)
                {
                    Year = new YearDim(session);
                    Year.Name = Name;
                    Year.Description = Description;
                    Year.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    Year.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public YearDim GetYearDim(Session session, string Name)
        {
            YearDim Year = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                Year = session.FindObject<YearDim>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return Year;
        }
        public void CreatHourDim(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                HourDim Hour = session.FindObject<HourDim>(criteria);
                if (Hour == null)
                {
                    Hour = new HourDim(session);
                    Hour.Name = Name;
                    Hour.Description = Description;
                    Hour.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    Hour.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public HourDim GetHourDim(Session session, string Name)
        {
            HourDim Hour = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                Hour = session.FindObject<HourDim>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return Hour;
        }
        public void CreatMinuteDim(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                MinuteDim Minute = session.FindObject<MinuteDim>(criteria);
                if (Minute == null)
                {
                    Minute = new MinuteDim(session);
                    Minute.Name = Name;
                    Minute.Description = Description;
                    Minute.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    Minute.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MinuteDim GetMinuteDim(Session session, string Name)
        {
            MinuteDim Minute = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                Minute = session.FindObject<MinuteDim>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return Minute;
        }
        public void CreatETLJobRunningState(Session session, string Name, string Description)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                ETLJobRunningState _ETLJobRunningState = session.FindObject<ETLJobRunningState>(criteria);
                if (_ETLJobRunningState == null)
                {
                    _ETLJobRunningState = new ETLJobRunningState(session);
                    _ETLJobRunningState.Name = Name;
                    _ETLJobRunningState.Description = Description;
                    _ETLJobRunningState.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ETLJobRunningState GetETLJobRunningState(Session session, string Name)
        {
            ETLJobRunningState _ETLJobRunningState = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                _ETLJobRunningState = session.FindObject<ETLJobRunningState>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return _ETLJobRunningState;
        }
        public void CreatETLJobRunningStatus(Session session, string Name, string Description, int ErrorCode)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                ETLJobRunningStatus _ETLJobRunningStatus = session.FindObject<ETLJobRunningStatus>(criteria);
                if (_ETLJobRunningStatus == null)
                {
                    _ETLJobRunningStatus = new ETLJobRunningStatus(session);
                    _ETLJobRunningStatus.Name = Name;
                    _ETLJobRunningStatus.Description = Description;
                    _ETLJobRunningStatus.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ETLJobRunningStatus GetETLJobRunningStatus(Session session, string Name)
        {
            ETLJobRunningStatus _ETLJobRunningStatus = null;
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", Name, BinaryOperatorType.Equal);
                _ETLJobRunningStatus = session.FindObject<ETLJobRunningStatus>(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return _ETLJobRunningStatus;
        }
        #endregion

        #region ETLLog
        public bool CreatETLJobLog(Session session, Guid jobId, string StateName, string StatusName,DateTime date)
        {
            bool res = true;
            try
            {
                DateTime nowTime = date;
                string currentDay = nowTime.Day.ToString();
                string currentMonth = nowTime.Month.ToString();
                string currentYear = nowTime.Year.ToString();
                string currentHour = nowTime.Hour.ToString();
                string currentMinute = nowTime.Minute.ToString();
                DayDim day = GetDayDim(session, currentDay);
                if (day == null) return false;
                MonthDim month = GetMonthDim(session, currentMonth);
                if (month == null) return false;
                YearDim year = GetYearDim(session, currentYear);
                if (year == null) return false;
                HourDim hour = GetHourDim(session, currentHour);
                if (hour == null) return false;
                MinuteDim minute = GetMinuteDim(session, currentMinute);
                if (minute == null) return false;
                ETLJobRunningState state = GetETLJobRunningState(session, StateName);
                if (state == null) return false;
                ETLJobRunningStatus status = GetETLJobRunningStatus(session, StatusName);
                if (status == null) return false;
                ETLJob job = session.GetObjectByKey<ETLJob>(jobId);
                if (job == null) return false;

                ETLJobLog etlJobLog = new ETLJobLog(session);
                etlJobLog.ETLJobId = job;
                etlJobLog.DayDimId = day;
                etlJobLog.MinuteDimId = minute;
                etlJobLog.MonthDimId = month;
                etlJobLog.YearDimId = year;
                etlJobLog.HourDimId = hour;
                etlJobLog.ETLJobRunningStateId = state;
                etlJobLog.ETLJobRunningStatusId = status;
                etlJobLog.Save();
            }
            catch(Exception)
            {
                return false;
                throw;                
            }
            return res;
        }
        public void JobLog(Session session, Guid jobId, string StateName, string StatusName)
        {
            try
            {                
                DateTime nowTime = DateTime.Now;
                string currentDay = nowTime.Day.ToString();
                string currentMonth = nowTime.Month.ToString();
                string currentYear = nowTime.Year.ToString();
                string currentHour = nowTime.Hour.ToString();
                string currentMinute = nowTime.Minute.ToString();
                CreatDayDim(session, currentDay, currentDay);
                CreatMonthDim(session, currentMonth, currentMonth);
                CreatYearDim(session, currentYear, currentYear);
                CreatHourDim(session, currentHour, currentHour);
                CreatMinuteDim(session, currentMinute, currentMinute);
                CreatETLJobRunningState(session, StateName, StateName);
                CreatETLJobRunningStatus(session, StatusName, StatusName, 0);

                CreatETLJobLog(session, jobId, StateName, StatusName, nowTime);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
