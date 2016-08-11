using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL.Log
{
    public partial class ETLJobRunningStatus : XPCustomObject
    {
        public ETLJobRunningStatus(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field

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
        string fName;
        [Size(256)]
        public string Name
        {
            get
            {
                return fName;
            }
            set
            {
                SetPropertyValue<string>("Name", ref fName, value);
            }
        }
        int fErrorCode;
        public int ErrorCode
        {
            get
            {
                return fErrorCode;
            }
            set
            {
                SetPropertyValue<int>("ErrorCode", ref fErrorCode, value);
            }
        }
        Guid fETLJobRunningStatusId;
        [Key(true)]
        public Guid ETLJobRunningStatusId
        {
            get
            {
                return fETLJobRunningStatusId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobRunningStatusId", ref fETLJobRunningStatusId, value);
            }
        }
        #endregion

        #region reference

        [Association(@"ETLJobRunningStatusReferencesETLJobLog", typeof(ETLJobLog)), Aggregated]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }

        #endregion
    }
}
