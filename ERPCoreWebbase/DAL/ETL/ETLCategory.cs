using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;

namespace NAS.DAL.ETL
{
    public partial class ETLCategory : XPCustomObject
    {
        public ETLCategory(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static void Populate(Session dbSession)
        {
            Session session = null;
            try
            {
                session = dbSession;

                //insert default data into ETLCategory table
                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "Default",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Population"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Population",
                        Name = "Population",
                        Description = "Population jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session, "Code", "Recycle"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Recycle",
                        Name = "Recycle",
                        Description = "Recycle jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Accounting"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Accounting",
                        Name = "Accounting",
                        Description = "Accounting jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Scheduler"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Scheduler",
                        Name = "Scheduler",
                        Description = "Scheduler jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Restore"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Restore",
                        Name = "Restore",
                        Description = "Restore jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Backup"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Backup",
                        Name = "Backup",
                        Description = "Backup jobs",
                    };
                    _ETLCategory.Save();
                }

                if (!Util.IsExistXpoObject<ETLCategory>(session,"Code", "Migration"))
                {
                    ETLCategory _ETLCategory = new ETLCategory(session)
                    {
                        Code = "Migration",
                        Name = "Migration",
                        Description = "Migration jobs",
                    };
                    _ETLCategory.Save();
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
        Guid fETLCategoryId;
        [Key(true)]
        public Guid ETLCategoryId
        {
            get
            {
                return fETLCategoryId;
            }
            set
            {
                SetPropertyValue<Guid>("ETLCategoryId", ref fETLCategoryId, value);
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
        #endregion

        #region References

        [Association(@"ETLCategoryReferencesETLJob", typeof(ETLJob)), Aggregated]
        public XPCollection<ETLJob> ETLJobs { get { return GetCollection<ETLJob>("ETLJobs"); } }


        #endregion
    }
}
