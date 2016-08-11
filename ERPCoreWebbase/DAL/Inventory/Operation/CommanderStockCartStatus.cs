using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.Operation
{

    public partial class CommanderStockCartStatus
    {
        public CommanderStockCartStatus(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<CommanderStockCartStatus>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    CommanderStockCartStatus cmdScT = new CommanderStockCartStatus(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartStatus>("Name", "SUBMITTED"))
                {
                    CommanderStockCartStatus cmdScT = new CommanderStockCartStatus(session)
                    {
                        Name = "SUBMITTED",
                        Description = "Khởi tạo",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartStatus>("Name", "IN_PROGRESS"))
                {
                    CommanderStockCartStatus cmdScT = new CommanderStockCartStatus(session)
                    {
                        Name = "IN_PROGRESS",
                        Description = "Đang tiến hành",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    }; 
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartStatus>("Name", "COMPLETE"))
                {
                    CommanderStockCartStatus cmdScT = new CommanderStockCartStatus(session)
                    {
                        Name = "COMPLETE",
                        Description = "Hoàn thành",
                        RowStatus = 1, 
                        RowCreationTimeStamp = DateTime.Now
                    };
                    cmdScT.Save();
                }

                if (!Util.isExistXpoObject<CommanderStockCartStatus>("Name", "BIAS"))
                {
                    CommanderStockCartStatus cmdScT = new CommanderStockCartStatus(session)
                    {
                        Name = "BIAS",
                        Description = "Số lượng thực tế bị chênh lệch",
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
