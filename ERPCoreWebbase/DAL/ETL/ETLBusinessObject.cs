using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL.ObjectLog;
using NAS.DAL;
using NAS.DAL.System.Log;
using DevExpress.Data.Filtering;

namespace NAS.DAL.ETL
{    
    public partial class ETLBusinessObject : XPCustomObject
    {
        public ETLBusinessObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
                
        #endregion
        public static void Populate(Session dbSession)
        {
            Session session = null;
            try
            {
                session = dbSession;
                //insert default data into ETLBusinessObject table
                if (!Util.IsExistXpoObject<ETLBusinessObject>(session,"RowStatus", 0))
                {
                    CriteriaOperator criteria = new BinaryOperator("ObjectType",-10,BinaryOperatorType.Equal);
                    BusinessObject businessObject = session.FindObject<BusinessObject>(criteria);
                    
                    criteria = new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal);
                    ETLJob job = session.FindObject<ETLJob>(criteria);

                    ETLBusinessObject _ETLBusinessObject = new ETLBusinessObject(session)
                    {
                        BusinessObjectIssuedDateTimeStamp = DateTime.Now,
                        BusinessObjectId = businessObject,
                        ETLJobId = job,
                        RowStatus = 0
                    };
                    _ETLBusinessObject.Save();
                }
                //if (!Util.IsExistXpoObject<ETLBusinessObject>(session,"RowStatus",2))
                //{
                //    ETLBusinessObject _ETLBusinessObject = new ETLBusinessObject(session)
                //    {
                //        BusinessObjectIssuedDateTimeStamp = new DateTime(2013, 10, 23),
                //        BusinessObjectId = session.GetObjectByKey<BusinessObject>((Int64)2),
                //        ETLJobId = session.GetObjectByKey<ETLJob>(Guid.Parse("1d6260c7-797a-41c3-b419-709b4270a0ee")),
                //        RowStatus = 2
                //    };
                //    _ETLBusinessObject.Save();
                //}
                //if (!Util.IsExistXpoObject<ETLBusinessObject>(session,"RowStatus", 3))
                //{
                //    ETLBusinessObject _ETLBusinessObject = new ETLBusinessObject(session)
                //    {
                //        BusinessObjectIssuedDateTimeStamp = new DateTime(2013, 10, 24),
                //        BusinessObjectId = session.GetObjectByKey<BusinessObject>((Int64)3),
                //        ETLJobId = session.GetObjectByKey<ETLJob>(Guid.Parse("1d6260c7-797a-41c3-b419-709b4270a0ee")),
                //        RowStatus = 3
                //    };
                //    _ETLBusinessObject.Save();
                //}
                //if (!Util.IsExistXpoObject<ETLBusinessObject>(session,"RowStatus", 4))
                //{
                //    ETLBusinessObject _ETLBusinessObject = new ETLBusinessObject(session)
                //    {
                //        BusinessObjectIssuedDateTimeStamp = new DateTime(2013, 10, 25),
                //        BusinessObjectId = session.GetObjectByKey<BusinessObject>((Int64)4),
                //        ETLJobId = session.GetObjectByKey<ETLJob>(Guid.Parse("1d6260c7-797a-41c3-b419-709b4270a0ee")),
                //        RowStatus = 4
                //    };
                //    _ETLBusinessObject.Save();
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
        #region Data Field
        
        Int64 fETLBusinessObjectId;
        [Key(true)]
        public Int64 ETLBusinessObjectId
        {
            get { return fETLBusinessObjectId; }
            set { SetPropertyValue<Int64>("ETLBusinessObjectId", ref fETLBusinessObjectId, value); }
        }
        DateTime fBusinessObjectIssuedDateTimeStamp;
        public DateTime BusinessObjectIssuedDateTimeStamp
        {
            get
            {
                return fBusinessObjectIssuedDateTimeStamp;
            }
            set
            {
                SetPropertyValue<DateTime>("BusinessObjectIssuedDateTimeStamp", ref fBusinessObjectIssuedDateTimeStamp, value);
            }
        }        
        ETLBusinessObject fPreviousETLBusinessObjectId;
        public ETLBusinessObject PreviousETLBusinessObjectId
        {
            get { return fPreviousETLBusinessObjectId; }
            set { SetPropertyValue<ETLBusinessObject>("PreviousETLBusinessObjectId", ref fPreviousETLBusinessObjectId, value); }
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

        BusinessObject fBusinessObjectId;
        [Association(@"BusinessObjectReferencesETLBusinessObject")]
        public BusinessObject BusinessObjectId
        {
            get { return fBusinessObjectId; }
            set { SetPropertyValue<BusinessObject>("BusinessObjectId", ref fBusinessObjectId, value); }
        }
        ETLJob fETLJobId;
        [Association(@"ETLJobReferencesETLBusinessObject")]
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

        [Association(@"ETLBusinessObjectReferencesETLEntryObjectHistory", typeof(ETLEntryObjectHistory)), Aggregated]
        public XPCollection<ETLEntryObjectHistory> ETLEntryObjectHistorys { get { return GetCollection<ETLEntryObjectHistory>("ETLEntryObjectHistorys"); } }

        #endregion
    }
}
