using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL
{
    public partial class ETLJobSchedulerDetail : XPCustomObject
    {
        public ETLJobSchedulerDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        Guid fETLJobSchedulerDetailId;
        [Key(true)]
        public Guid ETLJobSchedulerDetailId
        {
            get
            {
                return fETLJobSchedulerDetailId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobSchedulerDetailId", ref fETLJobSchedulerDetailId, value);
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
        byte fStartHour;
        public byte StartHour
        {
            get
            {
                return fStartHour;
            }
            set
            {
                SetPropertyValue<byte>("StartHour", ref fStartHour, value);
            }
        }
        byte fStartMinute;
        public byte StartMinute
        {
            get
            {
                return fStartMinute;
            }
            set
            {
                SetPropertyValue<byte>("StartMinute", ref fStartMinute, value);
            }
        }
        #endregion

        #region References

        ETLJobScheduler fETLJobSchedulerId;
        [Association(@"ETLJobSchedulerReferencesETLJobSchedulerDetail")]
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

        #endregion
    }
}
