using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;
namespace NAS.DAL.Inventory.Item
{

    public partial class RecordedType  
    {
        public RecordedType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into RecordedType table
                if (!Util.isExistXpoObject<RecordedType>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    RecordedType recordedType = new RecordedType(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "", 
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    recordedType.Save();
                }
                //insert INITIAL_INVENTORY record type
                if (!Util.isExistXpoObject<RecordedType>("Name", "INITIAL_INVENTORY"))
                {
                    RecordedType recordedType = new RecordedType(session)
                    {
                        Name = "INITIAL_INVENTORY",
                        Description = "Nhập số dư tồn kho đầu kỳ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    recordedType.Save();
                }

                //insert PUTTING_INVENTORY record type
                if (!Util.isExistXpoObject<RecordedType>("Name", "PUTTING_INVENTORY"))
                {
                    RecordedType recordedType = new RecordedType(session)
                    {
                        Name = "PUTTING_INVENTORY",
                        Description = "Nhập kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    recordedType.Save();
                }

                //insert GETTING_INVENTORY record type
                if (!Util.isExistXpoObject<RecordedType>("Name", "GETTING_INVENTORY"))
                {
                    RecordedType recordedType = new RecordedType(session)
                    {
                        Name = "GETTING_INVENTORY",
                        Description = "Xuất kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    recordedType.Save();
                }

                //insert ENTRY_INVENTORY record type
                if (!Util.isExistXpoObject<RecordedType>("Name", "ENTRY_INVENTORY"))
                {
                    RecordedType recordedType = new RecordedType(session)
                    {
                        Name = "ENTRY_INVENTORY",
                        Description = "Bút toán",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    recordedType.Save();
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
