using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.ETL.Log
{
    public partial class ETLJobRunningState : XPCustomObject    
    {
        public ETLJobRunningState(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoDefault.Session;

                //insert default data into ETLCategory table
                //if (!Util.isExistXpoObject<ETLCategory>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                //{
                //    ETLCategory _ETLCategory = new ETLCategory(session)
                //    {
                //        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                //        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                //        Description = "Default",
                //    };
                //    _ETLCategory.Save();
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
        Guid fETLJobRunningStateId;
        [Key(true)]
        public Guid ETLJobRunningStateId
        {
            get
            {
                return fETLJobRunningStateId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobRunningStateId", ref fETLJobRunningStateId, value);
            }
        }
        #endregion

        #region reference

        [Association(@"ETLJobRunningStateReferencesETLJobLog", typeof(ETLJobLog)), Aggregated]
        public XPCollection<ETLJobLog> ETLJobLogs { get { return GetCollection<ETLJobLog>("ETLJobLogs"); } }

        #endregion
    }
}
