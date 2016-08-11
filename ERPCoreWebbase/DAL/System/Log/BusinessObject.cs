using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.ETL;
using NAS.DAL;

namespace NAS.DAL.System.Log
{
    public partial class BusinessObject :XPCustomObject
    {
        public BusinessObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static void Populate(Session dbSession)
        {
            Session session = null;
            try
            {
                session = dbSession;
                //insert default data into BusinessObject table
                if (!Util.IsExistXpoObject<BusinessObject>(session,"RefId", Guid.Empty))
                {
                    BusinessObject _BusinessObject = new BusinessObject(session)
                    {
                        IssuedDateTimeStamp = DateTime.Now,
                        ObjectIssueDate = DateTime.Now,
                        ObjectType = -10,
                        RefId = Guid.Empty,
                        RowStatus = 1
                    };
                    _BusinessObject.Save();
                }
                //if (!Util.IsExistXpoObject<BusinessObject>(session,"RefId", Guid.Parse("00000000-0000-0000-0000-000000000003")))
                //{
                //    BusinessObject _BusinessObject = new BusinessObject(session)
                //    {
                //        IssuedDateTimeStamp = new DateTime(2013, 10, 23),
                //        ObjectIssueDate = new DateTime(2013, 10, 23),
                //        ObjectType = 1,
                //        RefId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                //        RowStatus = 1
                //    };
                //    _BusinessObject.Save();
                //}
                //if (!Util.IsExistXpoObject<BusinessObject>(session,"RefId", Guid.Parse("00000000-0000-0000-0000-000000000001")))
                //{
                //    BusinessObject _BusinessObject = new BusinessObject(session)
                //    {
                //        IssuedDateTimeStamp = new DateTime(2013, 10, 24),
                //        ObjectIssueDate = new DateTime(2013, 10, 24),
                //        ObjectType = 1,
                //        RefId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                //        RowStatus = 1
                //    };
                //    _BusinessObject.Save();
                //}
                //if (!Util.IsExistXpoObject<BusinessObject>(session,"RefId", Guid.Parse("00000000-0000-0000-0000-000000000002")))
                //{
                //    BusinessObject _BusinessObject = new BusinessObject(session)
                //    {
                //        IssuedDateTimeStamp = new DateTime(2013, 10, 25),
                //        ObjectIssueDate = new DateTime(2013, 10, 25),
                //        ObjectType = 1,
                //        RefId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                //        RowStatus = 1
                //    };
                //    _BusinessObject.Save();
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
        Int64 fBusinessObjectId;
        [Key(true)]
        public Int64 BusinessObjectId
        {
            get
            {
                return fBusinessObjectId;
            }
            set
            {
                SetPropertyValue<Int64>("BusinessObjectId", ref fBusinessObjectId, value);
            }
        }

        DateTime fIssuedDateTimeStamp;
        public DateTime IssuedDateTimeStamp
        {
            get
            {
                return fIssuedDateTimeStamp;
            }
            set
            {
                SetPropertyValue<DateTime>("IssuedDateTimeStamp", ref fIssuedDateTimeStamp, value);
            }
        }
        
        DateTime fObjectIssueDate;
        public DateTime ObjectIssueDate
        {
            get
            {
                return fObjectIssueDate;
            }
            set
            {
                SetPropertyValue<DateTime>("ObjectIssueDate", ref fObjectIssueDate, value);
            }
        }

        int fObjectType;
        public int ObjectType
        {
            get
            {
                return fObjectType;
            }
            set
            {
                SetPropertyValue<int>("ObjectType", ref fObjectType, value);
            }
        }
        
        Guid fRefId;
        public Guid RefId
        {
            get
            {
                return fRefId;
            }
            set
            {
                SetPropertyValue<Guid>("RefId", ref fRefId, value);
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

        [Association(@"BusinessObjectReferencesObjectEntryLog", typeof(ObjectEntryLog)), Aggregated]
        public XPCollection<ObjectEntryLog> ObjectEntryLogs { get { return GetCollection<ObjectEntryLog>("ObjectEntryLogs"); } }
        [Association(@"BusinessObjectReferencesETLBusinessObject", typeof(ETLBusinessObject)), Aggregated]
        public XPCollection<ETLBusinessObject> ETLBusinessObjects { get { return GetCollection<ETLBusinessObject>("ETLBusinessObjects"); } }

        #endregion
    }
}
