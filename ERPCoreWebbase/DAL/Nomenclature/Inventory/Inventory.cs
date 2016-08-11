using System;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.Inventory
{
    public enum DefaultInventoryEnum
    {
        DEFAULTCST,
        TRANSITINVENTORY,
        NOT_AVAILABLE
    }

    public partial class Inventory
    {
        public Inventory(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static Inventory InitNewRow(Session session)
        {
            try
            {
                Inventory manufacturerOrg = new Inventory(session)
                {
                    OrganizationId = Util.getDefaultXpoObject<NAS.DAL.Nomenclature.Organization.Organization>(session),
                    //InventoryUnitId = Util.getDefaultXpoObject<InventoryUnit>(session),
                    RowStatus = -1,
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
                //insert default data into Inventory table
                if (!Util.isExistXpoObject<NAS.DAL.Nomenclature.Inventory.Inventory>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {

                    Nomenclature.Organization.Organization.Populate();

                    XPQuery<Nomenclature.Organization.Organization> organizationQuery =
                        session.Query<Nomenclature.Organization.Organization>();

                    Nomenclature.Organization.Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    InventoryUnit.Populate();

                    XPQuery<InventoryUnit> inventoryUnitQuery = session.Query<InventoryUnit>();

                    InventoryUnit inventoryUnit =
                        inventoryUnitQuery.Where(r => r.Name == Utility.Constant.NAAN_DEFAULT_NAME).FirstOrDefault();


                    NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                        new NAS.DAL.Nomenclature.Inventory.Inventory(session)
                        {
                            Name = Utility.Constant.NAAN_DEFAULT_NAME,
                            Description = "",
                            OrganizationId = organization,
                            InventoryUnitId = inventoryUnit,
                            RowStatus = -1,
                            RowCreationTimeStamp = DateTime.Now
                        };
                    inventory.Save();
                }

                if (!Util.isExistXpoObject<NAS.DAL.Nomenclature.Inventory.Inventory>("InventoryId", "fa31071d-6010-4788-83b9-9f0ce0c90c5f"))
                {
                    Nomenclature.Organization.Organization.Populate();

                    XPQuery<Nomenclature.Organization.Organization> organizationQuery =
                        session.Query<Nomenclature.Organization.Organization>();

                    Nomenclature.Organization.Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    InventoryUnit.Populate();

                    XPQuery<InventoryUnit> inventoryUnitQuery = session.Query<InventoryUnit>();

                    InventoryUnit inventoryUnit =
                        inventoryUnitQuery.Where(r => r.Name == Utility.Constant.NAAN_DEFAULT_NAME).FirstOrDefault();


                    NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                        new NAS.DAL.Nomenclature.Inventory.Inventory(session)
                        {
                            InventoryId = Guid.Parse("fa31071d-6010-4788-83b9-9f0ce0c90c5f"),
                            Name = "Kho mặc định",
                            Code = "KHOMACDINH",
                            Description = "",
                            OrganizationId = organization,
                            InventoryUnitId = inventoryUnit,
                            RowStatus = 1,
                            RowCreationTimeStamp = DateTime.Now
                        };
                    inventory.Save();
                }

                if (!Util.isExistXpoObject<NAS.DAL.Nomenclature.Inventory.Inventory>("Code", "DEFAULTCST"))
                {

                    Nomenclature.Organization.Organization.Populate();

                    XPQuery<Nomenclature.Organization.Organization> organizationQuery =
                        session.Query<Nomenclature.Organization.Organization>();

                    Nomenclature.Organization.Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    InventoryUnit.Populate();

                    XPQuery<InventoryUnit> inventoryUnitQuery = session.Query<InventoryUnit>();

                    InventoryUnit inventoryUnit =
                        inventoryUnitQuery.Where(r => r.Name == Utility.Constant.NAAN_DEFAULT_NAME).FirstOrDefault();


                    NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                        new NAS.DAL.Nomenclature.Inventory.Inventory(session)
                        {
                            Code = "DEFAULTCST",
                            Name = "Kho khách hàng",
                            Description = "",
                            OrganizationId = organization,
                            InventoryUnitId = inventoryUnit,
                            RowStatus = -1,
                            RowCreationTimeStamp = DateTime.Now
                        };
                    inventory.Save();
                }

                if (!Util.isExistXpoObject<NAS.DAL.Nomenclature.Inventory.Inventory>("Code", "TRANSITINVENTORY"))
                {

                    Nomenclature.Organization.Organization.Populate();

                    XPQuery<Nomenclature.Organization.Organization> organizationQuery =
                        session.Query<Nomenclature.Organization.Organization>();

                    Nomenclature.Organization.Organization organization =
                        organizationQuery.Where(r => r.Code == Utility.Constant.NAAN_DEFAULT_CODE).FirstOrDefault();

                    InventoryUnit.Populate();

                    XPQuery<InventoryUnit> inventoryUnitQuery = session.Query<InventoryUnit>();

                    InventoryUnit inventoryUnit =
                        inventoryUnitQuery.Where(r => r.Name == Utility.Constant.NAAN_DEFAULT_NAME).FirstOrDefault();

                    NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                        new NAS.DAL.Nomenclature.Inventory.Inventory(session)
                        {
                            Code = "TRANSITINVENTORY",
                            Name = "Kho trung chuyển",
                            Description = "",
                            OrganizationId = organization,
                            InventoryUnitId = inventoryUnit,
                            RowStatus = -1,
                            RowCreationTimeStamp = DateTime.Now
                        };
                    inventory.Save();
                }

                if (!Util.isExistXpoObject<NAS.DAL.Nomenclature.Inventory.Inventory>("Code", Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE))
                {
                    NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                        new NAS.DAL.Nomenclature.Inventory.Inventory(session)
                        {
                            Code = Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE,
                            Name = Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE,
                            Description = Utility.Constant.NAAN_DEFAULT_NOTAVAILABLE,
                            RowStatus = 1,
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
        public static Inventory GetDefault(Session session, DefaultInventoryEnum code)
        {
            try
            {
                if (Enum.GetName(typeof(DefaultInventoryEnum), code).Equals("NOT_AVAILABLE"))
                    return  session.FindObject<Inventory>(
                    new BinaryOperator("Code", "N/A"));

                return session.FindObject<Inventory>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultInventoryEnum), code)));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }

}
