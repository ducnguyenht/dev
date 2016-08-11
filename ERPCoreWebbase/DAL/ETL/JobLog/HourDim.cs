using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Security.Cryptography;

namespace NAS.DAL.ETL.Log
{
    public partial class HourDim : XPCustomObject
    {
        public HourDim(Session session) : base(session) { }
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

        Guid fHourDimId;
        [Key(true)]
        public Guid HourDimId
        {
            get
            {
                return fHourDimId;
            }
            set
            {
                SetPropertyValue<Guid>("HourDimId", ref fHourDimId, value);
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

        [Association(@"HourDimReferencesETLJobLog", typeof(ETLJobLog)), Aggregated]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }

        #endregion
    }
}
