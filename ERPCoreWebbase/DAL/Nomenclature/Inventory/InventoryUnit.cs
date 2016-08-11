using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Nomenclature.Inventory
{

    public partial class InventoryUnit
    {
        public InventoryUnit(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static InventoryUnit InitNewRow(Session session)
        {
            try
            {
                InventoryUnit manufacturerOrg = new InventoryUnit(session)
                {
                    OrganizationId = Util.getDefaultXpoObject<NAS.DAL.Nomenclature.Organization.Organization>(session),
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };
                manufacturerOrg.Save();
                return manufacturerOrg;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {

            }
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into InventoryUnit table
                if (!Util.isExistXpoObject<InventoryUnit>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {

                    Nomenclature.Organization.Organization.Populate();

                    XPQuery<Nomenclature.Organization.Organization> organizationQuery =
                        session.Query<Nomenclature.Organization.Organization>();

                    Nomenclature.Organization.Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    InventoryUnit inventory = new InventoryUnit(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        OrganizationId = organization,
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    inventory.Save();
                }
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
