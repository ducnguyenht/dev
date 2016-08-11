using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Organization
{

    public partial class SupplierOrg
    {
        public SupplierOrg(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static SupplierOrg InitNewRow(Session session)
        {
            try
            {
                SupplierOrg supplierOrg = new SupplierOrg(session)
                {
                    OrganizationTypeId = Util.getDefaultXpoObject<OrganizationType>(session),
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };
                supplierOrg.Save();
                return supplierOrg;
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
                OrganizationType organizationType = session.FindObject<OrganizationType>(new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE));
                //Insert 
                if (!Util.isExistXpoObject<Organization>("Code",
                        Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL))
                {
                    SupplierOrg defaultSupplierForSelectAll = new SupplierOrg(session)
                    {
                        Code = Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL,
                        Name = Utility.Constant.NAAN_DEFAULT_CODE_SELECTEDALL,
                        OrganizationTypeId = organizationType,
                        Description = "",
                        RowStatus = Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL,
                        RowCreationTimeStamp = DateTime.Now,
                        TaxNumber = "",
                        Address = ""
                    };
                    defaultSupplierForSelectAll.Save();
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
