using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL
{
    public partial class DayByMonthRecurrencePattern : RecurrencePattern
    {
        public DayByMonthRecurrencePattern(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

        ushort fOfEveryMonths;
        public ushort OfEveryMonths
        {
            get
            {
                return fOfEveryMonths;
            }
            set
            {
                SetPropertyValue<ushort>("OfEveryMonths", ref fOfEveryMonths, value);
            }
        }
        ushort fTheDay;
        public ushort TheDay
        {
            get
            {
                return fTheDay;
            }
            set
            {
                SetPropertyValue<ushort>("TheDay", ref fTheDay, value);
            }
        }
        #endregion
    }
}
