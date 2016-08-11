using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL.ObjectLog;

namespace NAS.DAL.System.Log
{
    public partial class ObjectEntryLog :XPCustomObject
    {
        public ObjectEntryLog(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

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

        Int64 fObjectEntryLogId;
        [Key(true)]
        public Int64 ObjectEntryLogId
        {
            get
            {
                return fObjectEntryLogId;
            }
            set
            {
                SetPropertyValue<Int64>("ObjectEntryLogId", ref fObjectEntryLogId, value);
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

        BusinessObject fBusinessObjectId;
        [Association(@"BusinessObjectReferencesObjectEntryLog")]
        public BusinessObject BusinessObjectId
        {
            get { return fBusinessObjectId; }
            set { SetPropertyValue<BusinessObject>("BusinessObjectId", ref fBusinessObjectId, value); }
        }

        [Association(@"ObjectEntryLogReferencesETLEntryObjectHistory", typeof(ETLEntryObjectHistory)), Aggregated]
        public XPCollection<ETLEntryObjectHistory> ETLEntryObjectHistorys { get { return GetCollection<ETLEntryObjectHistory>("ETLEntryObjectHistorys"); } }

        #endregion
    }
}
