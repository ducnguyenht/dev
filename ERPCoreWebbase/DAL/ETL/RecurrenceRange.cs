using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL
{
    public partial class RecurrenceRange : XPCustomObject
    {
        public RecurrenceRange(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field        
        Guid fRecurrenceRangeId;
        [Key(true)]
        public Guid RecurrenceRangeId
        {
            get
            {
                return fRecurrenceRangeId;
            }
            set
            {
                SetPropertyValue<Guid>("RecurrenceRangeId", ref fRecurrenceRangeId, value);
            }
        }
        ushort fEndAfter;
        public ushort EndAfter
        {
            get
            {
                return fEndAfter;
            }
            set
            {
                SetPropertyValue<ushort>("EndAfter", ref fEndAfter, value);
            }
        }
        DateTime fEndByDate;
        public DateTime EndByDate
        {
            get
            {
                return fEndByDate;
            }
            set
            {
                SetPropertyValue<DateTime>("EndByDate", ref fEndByDate, value);
            }
        }
        bool fIsEndABy;
        public bool IsEndABy
        {
            get
            {
                return fIsEndABy;
            }
            set
            {
                SetPropertyValue<bool>("IsEndABy", ref fIsEndABy, value);
            }
        }
        bool fIsEndAfter;
        public bool IsEndAfter
        {
            get
            {
                return fIsEndAfter;
            }
            set
            {
                SetPropertyValue<bool>("IsEndAfter", ref fIsEndAfter, value);
            }
        }
        bool fIsNoEndDay;
        public bool IsNoEndDay
        {
            get
            {
                return fIsNoEndDay;
            }
            set
            {
                SetPropertyValue<bool>("IsNoEndDay", ref fIsNoEndDay, value);
            }
        }
        DateTime fStartDate;
        public DateTime StartDate
        {
            get
            {
                return fStartDate;
            }
            set
            {
                SetPropertyValue<DateTime>("StartDate", ref fStartDate, value);
            }
        }
        #endregion

        #region Reference

        RecurrencePattern fRecurrencePatternId;
        [Association(@"RecurrencePatternReferencesRecurrenceRange")]
        public RecurrencePattern RecurrencePatternId
        {
            get { return RecurrencePatternId; }
            set { SetPropertyValue<RecurrencePattern>("RecurrencePatternId", ref fRecurrencePatternId, value); }
        }

        #endregion
    }
}
