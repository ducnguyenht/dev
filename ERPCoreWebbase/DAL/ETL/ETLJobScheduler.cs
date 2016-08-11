using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL
{
    public partial class ETLJobScheduler : XPCustomObject
    {
        public ETLJobScheduler(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        Guid fETLJobSchedulerId;
        [Key(true)]
        public Guid ETLJobSchedulerId
        {
            get
            {
                return fETLJobSchedulerId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobSchedulerId", ref fETLJobSchedulerId, value);
            }
        }
        int fNumberOfRunning;
        public int NumberOfRunning
        {
            get
            {
                return fNumberOfRunning;
            }
            set
            {
                SetPropertyValue<int>("NumberOfRunning", ref fNumberOfRunning, value);
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
        DateTime fValidFrom;
        public DateTime ValidFrom
        {
            get
            {
                return fValidFrom;
            }
            set
            {
                SetPropertyValue<DateTime>("ValidFrom", ref fValidFrom, value);
            }
        }
        DateTime fValidUntil;
        public DateTime ValidUntil
        {
            get
            {
                return fValidUntil;
            }
            set
            {
                SetPropertyValue<DateTime>("ValidUntil", ref fValidUntil, value);
            }
        }
        #endregion

        #region References

        ETLJob fETLJobId;
        [Association(@"ETLJobReferencesETLJobScheduler")]
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
        [Association(@"ETLJobSchedulerReferencesETLJobSchedulerDetail", typeof(ETLJobSchedulerDetail)), Aggregated]
        public XPCollection<ETLJobSchedulerDetail> ETLJobSchedulerDetails { get { return GetCollection<ETLJobSchedulerDetail>("ETLJobSchedulerDetails"); } }
        [Association(@"ETLJobSchedulerReferencesRecurrencePattern", typeof(RecurrencePattern)), Aggregated]
        public XPCollection<RecurrencePattern> RecurrencePatterns { get { return GetCollection<RecurrencePattern>("RecurrencePatterns"); } }

        #endregion
    }
}
