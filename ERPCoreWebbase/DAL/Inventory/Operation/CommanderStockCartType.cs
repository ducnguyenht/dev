using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.Operation
{

    public partial class CommanderStockCartType
    {
        public CommanderStockCartType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "PUTTING_COMMAND"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "PUTTING_COMMAND",
                        Description = "Phiếu nhập kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "GETTING_COMMAND"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "GETTING_COMMAND",
                        Description = "Phiếu xuất kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "INTERNAL_MOVING"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "INTERNAL_MOVING",
                        Description = "Phiếu chuyển kho nội bộ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "DEFAULT_PUTTING_COMMAND"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "DEFAULT_PUTTING_COMMAND",
                        Description = "Phiếu nhập kho mặc định",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "DEFAULT_GETTING_COMMAND"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "DEFAULT_GETTING_COMMAND",
                        Description = "Phiếu xuất kho mặc định",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save(); 
                }

                if (!Util.isExistXpoObject<CommanderStockCartType>("Name", "DEFAULT_INTERNAL_MOVING"))
                {
                    CommanderStockCartType cmdScT = new CommanderStockCartType(session)
                    {
                        Name = "DEFAULT_INTERNAL_MOVING",
                        Description = "Phiếu chuyển kho nội bộ mặc định",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
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
