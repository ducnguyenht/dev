using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{
    public partial class OwnerOrg
    {
        public OwnerOrg(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        new public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                //if (!Util.isExistXpoObject<OwnerOrg>("Code", Utility.Constant.NAAN_DEFAULT_CODE))
                //{
                //    XPQuery<OrganizationType> organizationTypeQuery = session.Query<OrganizationType>();
                //    OrganizationType.Populate();
                //    OrganizationType organizationType = session.FindObject<OrganizationType>(
                //            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal)
                //        );
                //    OwnerOrg ownerOrg = new OwnerOrg(session)
                //    {
                //        Code = Utility.Constant.NAAN_DEFAULT_CODE,
                //        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                //        OrganizationTypeId = organizationType,
                //        Description = "",
                //        RowStatus = -1,
                //        RowCreationTimeStamp = DateTime.Now,
                //        TaxNumber = "",
                //        Address = ""
                //    };
                //    ownerOrg.Save();
                //}
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
    }

}
