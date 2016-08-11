using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility.ETL;
using NAS.DAL;

namespace NAS.DAL.ETL
{
    public partial class ETLJobDetail : XPCustomObject
    {
        public ETLJobDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static void Populate(Session dbSession)
        {
            Session session = null;
            try
            {
                ETLUtils util = new ETLUtils();
                ETLJob.Populate(dbSession);
                session = dbSession;
                ETLJob etlJob;                
                CriteriaOperator criteria = new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal);
                etlJob = session.FindObject<ETLJob>(criteria);
                //insert default data into ETLCategory table
                if (!Util.IsExistXpoObject<ETLJobDetail>(session,"ETLJobId", etlJob ))
                {
                    etlJob = session.FindObject<ETLJob>(criteria);
                    ETLJobDetail _ETLJobDetail = new ETLJobDetail(session)
                    {
                        Data = null,
                        ETLJobId = etlJob
                    };
                    _ETLJobDetail.Save();
                }

                criteria = new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal);
                etlJob = session.FindObject<ETLJob>(criteria);
                if (!Util.IsExistXpoObject<ETLJobDetail>(session,"ETLJobId", etlJob))
                {
                    etlJob = session.FindObject<ETLJob>(criteria);
                    ETLJobDetail _ETLJobDetail = new ETLJobDetail(session)
                    {
                        Data = ETLUtils.ConvertStringToByte(Utility.Constant.NAAN_DEFAULT_CODE),
                        ETLJobId = etlJob
                    };
                    _ETLJobDetail.Save();
                }
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

        byte[] fData;
        public byte[] Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<byte[]>("Data", ref fData, value);
            }
        }
        Guid fETLJobDetailId;
        [Key(true)]
        public Guid ETLJobDetailId
        {
            get
            {
                return fETLJobDetailId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLJobDetailId", ref fETLJobDetailId, value);
            }
        }

        #endregion

        #region Reference
        ETLJob fETLJobId;
        [Association(@"ETLJobReferencesETLJobDetail")]
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
        #endregion
    }
}
