using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public enum OrganizationEnum
    {
        NAAN_DEFAULT,
        QUASAPHARCO
    }

    public partial class Organization
    {
        public Organization(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        public static Organization GetDefault(Session session, OrganizationEnum code)
        {
            Organization ret = null;
            try
            {
                ret = session.FindObject<Organization>(
                    new BinaryOperator("Code", Enum.GetName(typeof(OrganizationEnum), code)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<Organization>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    XPQuery<OrganizationType> organizationTypeQuery = session.Query<OrganizationType>();
                    OrganizationType.Populate();
                    OrganizationType organizationType =
                        organizationTypeQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();
                    Organization organization = new Organization(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        OrganizationTypeId = organizationType,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now,
                        TaxNumber = "",
                        Address = ""
                    };

                    organization.Save();
                }

                //Insert QUASAPHARCO organization
                if (!Util.isExistXpoObject<Organization>("OrganizationId",
                        Guid.Parse("D52962C2-A75D-4F6E-BE0A-FF0C07D2B80B")))
                {
                    Organization quasaparcoOrg = new Organization(session)
                    {
                        OrganizationId = Guid.Parse("D52962C2-A75D-4F6E-BE0A-FF0C07D2B80B"),
                        Name = "Công ty CP TM Dược Sâm Ngọc Linh Quảng Nam",
                        Description = "Công ty CP TM Dược Sâm Ngọc Linh Quảng Nam",
                        Code = "QUASAPHARCO",
                        RowCreationTimeStamp = DateTime.Now,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        OrganizationTypeId =
                            NAS.DAL.Util.getXPCollection<OrganizationType>(session, "Name",
                                OrganizationTypeConstant.OWNER.Value).FirstOrDefault()
                    };
                    quasaparcoOrg.Save();
                }

            }
            catch (Exception)
            {
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
