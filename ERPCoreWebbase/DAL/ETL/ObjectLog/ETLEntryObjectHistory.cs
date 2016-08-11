using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL.Log;
using NAS.DAL.System.Log;

namespace NAS.DAL.ETL.ObjectLog
{
    public partial class ETLEntryObjectHistory : XPCustomObject
    {
        public ETLEntryObjectHistory(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        Int64 fETLEntryObjectHistoryId;
        [Key(true)]
        public Int64 ETLEntryObjectHistoryId
        {
            get { return fETLEntryObjectHistoryId; }
            set { SetPropertyValue<Int64>("ETLEntryObjectHistoryId", ref fETLEntryObjectHistoryId, value); }
        }
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
        DateTime fRowTimeStamp;
        public DateTime RowTimeStamp
        {
            get
            {
                return fRowTimeStamp;
            }
            set
            {
                SetPropertyValue<DateTime>("RowTimeStamp", ref fRowTimeStamp, value);
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

        ETLBusinessObject fETLBusinessObjectId;
        [Association(@"ETLBusinessObjectReferencesETLEntryObjectHistory")]
        public ETLBusinessObject ETLBusinessObjectId
        {
            get
            {
                return fETLBusinessObjectId;
            }
            set
            {
                SetPropertyValue<ETLBusinessObject>("ETLBusinessObjectId", ref fETLBusinessObjectId, value);
            }
        }

        ObjectEntryLog fObjectEntryLogId;
        [Association(@"ObjectEntryLogReferencesETLEntryObjectHistory")]
        public ObjectEntryLog ObjectEntryLogId
        {
            get
            {
                return fObjectEntryLogId;
            }
            set
            {
                SetPropertyValue<ObjectEntryLog>("ObjectEntryLogId", ref fObjectEntryLogId, value);
            }
        }

        #endregion
    }
}
