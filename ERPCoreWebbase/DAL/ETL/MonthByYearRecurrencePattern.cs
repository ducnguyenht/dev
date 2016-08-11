using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL
{    
    public partial class MonthByYearRecurrencePattern : RecurrencePattern
    {
        public MonthByYearRecurrencePattern(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

        ushort fDayOfMonth;
        public ushort DayOfMonth
        {
            get
            {
                return fDayOfMonth;
            }
            set
            {
                SetPropertyValue<ushort>("DayOfMonth", ref fDayOfMonth, value);
            }
        }
        ushort fRecurEveryYears;
        public ushort RecurEveryYears
        {
            get
            {
                return fRecurEveryYears;
            }
            set
            {
                SetPropertyValue<ushort>("RecurEveryYears", ref fRecurEveryYears, value);
            }
        }
        char fOnMonth;
        [Size(3)]
        public char OnMonth
        {
            get
            {
                return fOnMonth;
            }
            set
            {
                SetPropertyValue<char>("OnMonth", ref fOnMonth, value);
            }
        }
        #endregion
    }
}
