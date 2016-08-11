using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.CMS.ObjectDocument;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class ManufacturerOrg
    {
        public ManufacturerOrg(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static ManufacturerOrg InitNewRow(Session session)
        {
            try
            {
                ////Get MANUFACTURER object type
                //ObjectType objectType = Util.getXPCollection<ObjectType>(session, "Name", "MANUFACTURER").FirstOrDefault();               
                ////Create new CMS object
                //NAS.DAL.CMS.ObjectDocument.Object CMSObject = new CMS.ObjectDocument.Object(session)
                //{
                //    ObjectId = Guid.NewGuid(),
                //    ObjectTypeId = objectType
                //};
                //CMSObject.Save();

                //Create new ManufacturerOrg
                ManufacturerOrg manufacturerOrg = new ManufacturerOrg(session)
                {
                    OrganizationTypeId = Util.getDefaultXpoObject<OrganizationType>(session),
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                    //,
                    //ObjectTypeId = objectType,
                    //ObjectId = CMSObject
                };
                manufacturerOrg.Save();
                return manufacturerOrg;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        new public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                session.BeginTransaction();
                //if (!Util.isExistXpoObject<ManufacturerOrg>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                //{
                //    XPQuery<OrganizationType> organizationTypeQuery = session.Query<OrganizationType>();
                //    OrganizationType.Populate();
                //    OrganizationType organizationType =
                //        organizationTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                //    ManufacturerOrg manufacturerOrg = new ManufacturerOrg(session)
                //    {
                //        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                //        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                //        OrganizationTypeId = organizationType,
                //        Description = "",
                //        RowStatus = -1,
                //        RowCreationTimeStamp = DateTime.Now
                //    };

                //    manufacturerOrg.Save();
                //}

                if (!Util.isExistXpoObject<ManufacturerOrg>("Code", Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
                {
                    XPQuery<OrganizationType> organizationTypeQuery = session.Query<OrganizationType>();
                    OrganizationType.Populate();
                    OrganizationType organizationType =
                        organizationTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    ManufacturerOrg manufacturerOrg = new ManufacturerOrg(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL,
                        Name = Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL,
                        OrganizationTypeId = organizationType,
                        Description = "",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL,
                        RowCreationTimeStamp = DateTime.Now
                    };

                    manufacturerOrg.Save();
                }

                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        #endregion

    }

}
