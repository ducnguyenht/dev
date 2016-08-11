using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public sealed class OrganizationTypeConstant
    {
        private string value;

        public static readonly OrganizationTypeConstant NAAN_DEFAULT = new OrganizationTypeConstant("NAAN_DEFAULT");
        public static readonly OrganizationTypeConstant NAAN_CUSTOMER = new OrganizationTypeConstant("NAAN_CUSTOMER");
        public static readonly OrganizationTypeConstant OWNER = new OrganizationTypeConstant("OWNER");
        public static readonly OrganizationTypeConstant NAAN_CUSTOMER_SUB_ORGANIZATION = new OrganizationTypeConstant("NAAN_CUSTOMER_SUB_ORGANIZATION");

        private OrganizationTypeConstant(string v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public string Value { get { return value; } }

    }

    public partial class OrganizationType
    {
        public OrganizationType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        #region Logic
        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into OrganizationTypeBO table
                if (!Util.isExistXpoObject<OrganizationType>("Name", Utility.Constant.NAAN_DEFAULT_CODE))
                {
                    OrganizationType organizationType = new OrganizationType(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    organizationType.Save();
                }

                //insert NAAN_CUSTOMER organization type
                if (!Util.isExistXpoObject<OrganizationType>("Code", "NAAN_CUSTOMER"))
                {
                    OrganizationType organizationType = new OrganizationType(session)
                    {
                        Code = "NAAN_CUSTOMER",
                        Name = "NAAN_CUSTOMER",
                        Description = "Tổ chức khách hàng sử dụng phần mềm",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    organizationType.Save();
                }

                //insert OWNER organization type
                if (!Util.isExistXpoObject<OrganizationType>("Code", "OWNER"))
                {
                    OrganizationType organizationType = new OrganizationType(session)
                    {
                        Code = "OWNER",
                        Name = "OWNER",
                        Description = "Tổ chức có quyền trong hệ thống cao nhất",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    organizationType.Save();
                }

                //insert NAAN_CUSTOMER_SUB_ORGANIZATION organization type
                if (!Util.isExistXpoObject<OrganizationType>("Code", "NAAN_CUSTOMER_SUB_ORGANIZATION"))
                {
                    OrganizationType organizationType = new OrganizationType(session)
                    {
                        Code = "NAAN_CUSTOMER_SUB_ORGANIZATION",
                        Name = "NAAN_CUSTOMER_SUB_ORGANIZATION",
                        Description = "Tổ chức trực thuộc khách hàng sử dụng phần mềm",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    organizationType.Save();
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
