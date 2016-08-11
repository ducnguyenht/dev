using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL
{
    public partial class DailyRecurrencePattern : RecurrencePattern
    {
        public DailyRecurrencePattern(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

        int fEveryNumberOfDay;
        public int EveryNumberOfDay
        {
            get
            {
                return fEveryNumberOfDay;
            }
            set
            {
                SetPropertyValue<int>("EveryNumberOfDay", ref fEveryNumberOfDay, value);
            }
        }
        bool fEveryWeekDay;
        public bool EveryWeekDay
        {
            get
            {
                return fEveryWeekDay;
            }
            set
            {
                SetPropertyValue<bool>("EveryWeekDay", ref fEveryWeekDay, value);
            }
        }

        #endregion
    }
}
