using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL
{
    public partial class RecurrencePattern : XPCustomObject
    {
        public RecurrencePattern(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

        Guid fRecurrencePatternId;
        [Key(true)]
        public Guid RecurrencePatternId
        {
            get
            {
                return fRecurrencePatternId;
            }
            set
            {
                SetPropertyValue<Guid>("RecurrencePatternId", ref fRecurrencePatternId, value);
            }
        }

        #endregion

        #region References

        ETLJobScheduler fETLJobSchedulerId;
        [Association(@"ETLJobSchedulerReferencesRecurrencePattern")]
        public ETLJobScheduler ETLJobSchedulerId
        {
            get
            {
                return fETLJobSchedulerId;
            }
            set
            {
                SetPropertyValue<ETLJobScheduler>("ETLJobSchedulerId", ref fETLJobSchedulerId, value);
            }
        }
        [Association(@"RecurrencePatternReferencesRecurrenceRange", typeof(RecurrenceRange)), Aggregated]
        public XPCollection<RecurrenceRange> RecurrenceRanges { get { return GetCollection<RecurrenceRange>("RecurrenceRanges"); } }

        #endregion
    }
}
