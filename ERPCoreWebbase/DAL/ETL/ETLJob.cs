using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL.Log;
using NAS.DAL.ETL.ObjectLog;
using DevExpress.Data.Filtering;
using NAS.DAL;

namespace NAS.DAL.ETL
{    
    public partial class ETLJob : XPCustomObject    
    {
        public ETLJob(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static void Populate(Session dbSession)
        {
            Session session = null;
            try
            {
                session = dbSession;
                ETLCategory.Populate(session);
                CriteriaOperator criteria = new BinaryOperator("Code",Utility.Constant.NAAN_DEFAULT_CODE,BinaryOperatorType.Equal);
                ETLCategory category;
                //insert default data into ETLJob table
                if (!Util.IsExistXpoObject<ETLJob>(session,"Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    category = session.FindObject<ETLCategory>(criteria);
                    ETLJob _ETLJob = new ETLJob(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,                        
                        Description = "Default",
                        ETLCategory = category,
                        Is24x7 = false,
                        Priority = 0,
                        RowStatus = -1
                    };
                    _ETLJob.Save();
                }

                //if (!Util.IsExistXpoObject<ETLJob>(session,"Code", "GeneralJournalToGeneralLedger"))
                //{
                //    criteria = new BinaryOperator("Code", "Accounting", BinaryOperatorType.Equal);
                //    category = session.FindObject<ETLCategory>(criteria);
                //    ETLJob _ETLJob = new ETLJob(session)
                //    {
                //        Code = "GeneralJournalToGeneralLedger",
                //        Description = "GeneralJournal To GeneralLedger Job",
                //        ETLCategory = category,
                //        Is24x7 = false,
                //        Priority = 0,
                //        RowStatus = 1
                //    };
                //    _ETLJob.Save();
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //if (session != null) session.Dispose();
            }
        }        
        #endregion

        #region Data Field
        string fCode;
        [Size(36)]
        public string Code
        {
            get
            {
                return fCode;
            }
            set
            {
                SetPropertyValue<string>("Code", ref fCode, value);
            }
        }
        string fDescription;
        [Size(1024)]
        public string Description
        {
            get
            {
                return fDescription;
            }
            set
            {
                SetPropertyValue<string>("Description", ref fDescription, value);
            }
        }
        
        Guid fETLJobId;
        [Key(true)]
        public Guid ETLJobId
        {
            get
            {
                return fETLJobId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobId", ref fETLJobId, value);
            }
        }
        bool fIs24x7;
        public bool Is24x7
        {
            get
            {
                return fIs24x7;
            }
            set
            {
                SetPropertyValue<bool>("Is24x7", ref fIs24x7, value);
            }
        }
        byte fPriority;
        public byte Priority
        {
            get
            {
                return fPriority;
            }
            set
            {
                SetPropertyValue<byte>("Priority", ref fPriority, value);
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
        #endregion

        #region References

        //ETLJob fNextETLJobId;
        //[Association(@"ParentOf")]
        //public ETLJob NextETLJobId
        //{
        //    get { return fNextETLJobId; }
        //    set { SetPropertyValue<ETLJob>("NextETLJobId", ref fNextETLJobId, value); }
        //}

        ETLCategory fETLCategory;
        [Association(@"ETLCategoryReferencesETLJob")]
        public ETLCategory ETLCategory
        {
            get
            {
                return fETLCategory;
            }
            set
            {
                SetPropertyValue<ETLCategory>("ETLCategory", ref fETLCategory, value);
            }
        }
        [Association(@"ETLJobReferencesETLJobDetail", typeof(ETLJobDetail)), Aggregated]
        public XPCollection<ETLJobDetail> ETLJobDetails { get { return GetCollection<ETLJobDetail>("ETLJobDetails"); } }
        [Association(@"ETLJobReferencesETLJobScheduler", typeof(ETLJobScheduler)), Aggregated]
        public XPCollection<ETLJobScheduler> ETLJobSchedulers { get { return GetCollection<ETLJobScheduler>("ETLJobSchedulers"); } }
        [Association(@"ETLJobReferencesETLJobLog", typeof(ETLJobLog)), Aggregated]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }
        [Association(@"ETLJobReferencesETLBusinessObject", typeof(ETLBusinessObject)), Aggregated]
        public XPCollection<ETLBusinessObject> ETLBusinessObjects { get { return GetCollection<ETLBusinessObject>("ETLBusinessObjects"); } }

        #endregion
    }
}
