using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL.Log
{
    public partial class MinuteDim : XPCustomObject
    {
        public MinuteDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion

        #region Data Field
        string fName;
        [Size(36)]
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

        Guid fMinuteDimId;
        [Key(true)]
        public Guid MinuteDimId
        {
            get
            {
                return fMinuteDimId;
            }
            set
            {
                SetPropertyValue<Guid>("MinuteDimId", ref fMinuteDimId, value);
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

        #region reference

        [Association(@"MinuteDimReferencesETLJobLog", typeof(ETLJobLog)), Aggregated]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }

        #endregion
    }
}
