using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL.Log;

namespace NAS.DAL.ETL.ObjectLog
{
    public partial class ETLObjectLog : XPCustomObject
    {
        public ETLObjectLog(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        
        int fErrorCode;
        public int ErrorCode
        {
            get
            {
                return fErrorCode;
            }
            set
            {
                SetPropertyValue<int>("ErrorCode", ref fErrorCode, value);
            }
        }        
        int fETLObjectLogId;
        [Key(true)]
        public int ETLObjectLogId
        {
            get
            {
                return fETLObjectLogId;
            }
            set
            {
                SetPropertyValue<int>("ETLObjectLogId", ref fETLObjectLogId, value);
            }
        }
        Guid fLastUpdatedObjectId;
        public Guid LastUpdatedObjectId
        {
            get
            {
                return fLastUpdatedObjectId;
            }
            set
            {
                SetPropertyValue<Guid>("LastUpdatedObjectId", ref fLastUpdatedObjectId, value);
            }
        }
        DateTime fLastUpdatedObjectTimeStamp;
        public DateTime LastUpdatedObjectTimeStamp
        {
            get
            {
                return fLastUpdatedObjectTimeStamp;
            }
            set
            {
                SetPropertyValue<DateTime>("LastUpdatedObjectTimeStamp", ref fLastUpdatedObjectTimeStamp, value);
            }
        }
        DateTime fObjectIssueDate;
        public DateTime ObjectIssueDate
        {
            get
            {
                return fObjectIssueDate;
            }
            set
            {
                SetPropertyValue<DateTime>("ObjectIssueDate", ref fObjectIssueDate, value);
            }
        }
        int fObjectType;
        public int ObjectType
        {
            get
            {
                return fObjectType;
            }
            set
            {
                SetPropertyValue<int>("ObjectType", ref fObjectType, value);
            }
        }
        short fRowStatus;
        public short RowStatus
        {
            get
            {
                return fRowStatus;
            }
            set
            {
                SetPropertyValue<short>("RowStatus", ref fRowStatus, value);
            }
        }

        #endregion

        #region References
        ETLJob fETLJobId;
        [Association(@"ETLJobReferencesETLObjectLog")]
        public ETLJob ETLJobId
        {
            get
            {
                return fETLJobId;
            }
            set
            {
                SetPropertyValue<ETLJob>("ETLJobId", ref fETLJobId, value);
            }
        }

        #endregion
    }
}
