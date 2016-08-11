using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL
{
    public partial class WeeklyRecurrencePattern : RecurrencePattern
    {
        public WeeklyRecurrencePattern(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        bool fSunday;
        public bool Sunday
        {
            get
            {
                return fSunday;
            }
            set
            {
                SetPropertyValue<bool>("Sunday", ref fSunday, value);
            }
        }
        bool fMonday;
        public bool Monday
        {
            get
            {
                return fMonday;
            }
            set
            {
                SetPropertyValue<bool>("Monday", ref fMonday, value);
            }
        }
        bool fTuesday;
        public bool Tuesday
        {
            get
            {
                return fTuesday;
            }
            set
            {
                SetPropertyValue<bool>("Tuesday", ref fTuesday, value);
            }
        }
        bool fWednesday;
        public bool Wednesday
        {
            get
            {
                return fWednesday;
            }
            set
            {
                SetPropertyValue<bool>("Wednesday", ref fWednesday, value);
            }
        }
        bool fThursday;
        public bool Thursday
        {
            get
            {
                return fThursday;
            }
            set
            {
                SetPropertyValue<bool>("Thursday", ref fThursday, value);
            }
        }
        bool fFriday;
        public bool Friday
        {
            get
            {
                return fFriday;
            }
            set
            {
                SetPropertyValue<bool>("Friday", ref fFriday, value);
            }
        }
        bool fSaturday;
        public bool Saturday
        {
            get
            {
                return fSaturday;
            }
            set
            {
                SetPropertyValue<bool>("Saturday", ref fSaturday, value);
            }
        }
        int fRecurEveryNumberOfWeek;
        public int RecurEveryNumberOfWeek
        {
            get
            {
                return fRecurEveryNumberOfWeek;
            }
            set
            {
                SetPropertyValue<int>("RecurEveryNumberOfWeek", ref fRecurEveryNumberOfWeek, value);
            }
        }
        #endregion
    }
}
