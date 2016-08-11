using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL
{
    public partial class WeekDayByMonthRecurrencePattern : RecurrencePattern
    {
        public WeekDayByMonthRecurrencePattern(Session session) : base(session) { }
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
        ushort fWeekOrder;
        public ushort WeekOrder
        {
            get
            {
                return fWeekOrder;
            }
            set
            {
                SetPropertyValue<ushort>("WeekOrder", ref fWeekOrder, value);
            }
        }
        char fWeekDay;
        [Size(3)]
        public char WeekDay
        {
            get
            {
                return fWeekDay;
            }
            set
            {
                SetPropertyValue<char>("WeekDay", ref fWeekDay, value);
            }
        }
        #endregion
    }
}
