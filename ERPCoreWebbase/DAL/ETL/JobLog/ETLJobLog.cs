using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ShareDim;

namespace NAS.DAL.ETL.Log
{
    public partial class ETLJobLog : XPCustomObject    
    {
        public ETLJobLog(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        Guid fETLJobLogId;
        [Key(true)]
        public Guid ETLJobLogId
        {
            get
            {
                return fETLJobLogId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobLogId", ref fETLJobLogId, value);
            }
        }
        double fPercentageCal;
        public double PercentageCal
        {
            get
            {
                return fPercentageCal;
            }
            set
            {
                SetPropertyValue<double>("PercentageCal", ref fPercentageCal, value);
            }
        }
        double fProcessedTransaction;
        public double ProcessedTransaction
        {
            get
            {
                return fProcessedTransaction;
            }
            set
            {
                SetPropertyValue<double>("ProcessedTransaction", ref fProcessedTransaction, value);
            }
        }
        double fTotalTransaction;
        public double TotalTransaction
        {
            get
            {
                return fTotalTransaction;
            }
            set
            {
                SetPropertyValue<double>("TotalTransaction", ref fTotalTransaction, value);
            }
        }
        bool fIsNextRun;
        public bool IsNextRun
        {
            get
            {
                return fIsNextRun;
            }
            set
            {
                SetPropertyValue<bool>("IsNextRun", ref fIsNextRun, value);
            }
        }
        #endregion

        #region reference

        ETLJob fETLJobId;
        [Association(@"ETLJobReferencesETLJobLog")]
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
        DayDim fDayDimId;
        [Association(@"DayDimReferencesETLJobLog")]
        public DayDim DayDimId
        {
            get { return fDayDimId; }
            set { SetPropertyValue<DayDim>("DayDimId", ref fDayDimId, value); }
        }
        YearDim fYearDimId;
        [Association(@"YearDimReferencesETLJobLog")]
        public YearDim YearDimId
        {
            get { return fYearDimId; }
            set { SetPropertyValue<YearDim>("YearDimId", ref fYearDimId, value); }
        }
        MinuteDim fMinuteDimId;
        [Association(@"MinuteDimReferencesETLJobLog")]
        public MinuteDim MinuteDimId
        {
            get { return fMinuteDimId; }
            set { SetPropertyValue<MinuteDim>("MinuteDimId", ref fMinuteDimId, value); }
        }
        HourDim fHourDimId;
        [Association(@"HourDimReferencesETLJobLog")]
        public HourDim HourDimId
        {
            get { return fHourDimId; }
            set { SetPropertyValue<HourDim>("HourDimId", ref fHourDimId, value); }
        }
        MonthDim fMonthDimId;
        [Association(@"MonthDimReferencesETLJobLog")]
        public MonthDim MonthDimId
        {
            get { return fMonthDimId; }
            set { SetPropertyValue<MonthDim>("MonthDimId", ref fMonthDimId, value); }
        }
        ETLJobRunningStatus fETLJobRunningStatusId;
        [Association(@"ETLJobRunningStatusReferencesETLJobLog")]
        public ETLJobRunningStatus ETLJobRunningStatusId
        {
            get { return fETLJobRunningStatusId; }
            set { SetPropertyValue<ETLJobRunningStatus>("ETLJobRunningStatusId", ref fETLJobRunningStatusId, value); }
        }
        ETLJobRunningState fETLJobRunningStateId;
        [Association(@"ETLJobRunningStateReferencesETLJobLog")]
        public ETLJobRunningState ETLJobRunningStateId
        {
            get { return fETLJobRunningStateId; }
            set { SetPropertyValue<ETLJobRunningState>("ETLJobRunningStateId", ref fETLJobRunningStateId, value); }
        }

        #endregion
    }
}
