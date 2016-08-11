using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.Inventory.StockCart
{

    public partial class StockCartActorType
    {
        public StockCartActorType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                //insert ISSUED_BY record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "ISSUED_BY"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "ISSUED_BY",
                        Description = "Người lập thẻ kho",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert APPROVED_BY record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "APPROVED_BY"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "APPROVED_BY",
                        Description = "Người duyệt",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert DELIVER record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "DELIVER"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "DELIVER",
                        Description = "Người giao",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert RECEIVER record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "RECEIVER"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "RECEIVER",
                        Description = "Người nhận",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert DIRECTOR record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "DIRECTOR"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "DIRECTOR",
                        Description = "Giám đốc",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert MANAGER record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "MANAGER"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "MANAGER",
                        Description = "Trưởng kho",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                } 

                //insert STORE_KEEPER record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "STORE_KEEPER"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "STORE_KEEPER",
                        Description = "Thủ kho",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
                }

                //insert AUTDITOR record type
                if (!Util.isExistXpoObject<StockCartActorType>("Name", "AUTDITOR"))
                {
                    StockCartActorType stockCartActorType = new StockCartActorType(session)
                    {
                        Name = "AUTDITOR",
                        Description = "Kiểm toán kho",
                        RowStatus = 1
                    };
                    stockCartActorType.Save();
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
