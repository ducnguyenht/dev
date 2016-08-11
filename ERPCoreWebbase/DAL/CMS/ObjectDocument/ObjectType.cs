using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public enum ObjectTypeCategoryEnum
    {
        NAAN_DEFAULT,
        ITEM,
        ORGANIZATION
    }

    public enum ObjectTypeEnum
    {
        NAAN_DEFAULT,
        PRODUCT,
        SELF_PRODUCTION,
        SERVICE,
        MATERIAL,
        TOOL,
        FIXED_ASSETS,
        MANUFACTURER,
        CUSTOMER,
        SUPPLIER,
        VOUCHER_RECEIPT,
        VOUCHER_PAYMENT,
        INVOICE_PURCHASE,
        INVOICE_SALE,
        INVENTORY_IN,
        INVENTORY_OUT,
        INVENTORY_MOVE,
        INVENTORY_AUDIT,
        MANUAL_BOOKING,
        OPENBALANCE_ACCOUTING
    }

    public partial class ObjectType
    {
        public ObjectType(Session session) : base(session) {  }
        public override void AfterConstruction() { base.AfterConstruction(); }


        #region Logic
        public static ObjectType GetDefault(Session session, ObjectTypeEnum code)
        {
            ObjectType ret = null;
            try
            {
                ret = session.FindObject<ObjectType>(
                    new BinaryOperator("Name", Enum.GetName(typeof(ObjectTypeEnum), code)));
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
                //insert default data into ObjectType table
                if (!Util.isExistXpoObject<ObjectType>("Name", 
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.NAAN_DEFAULT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.NAAN_DEFAULT),
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                //insert PRODUCT object type
                if (!Util.isExistXpoObject<ObjectType>("Name", 
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.PRODUCT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        ObjectTypeId = Guid.Parse("5817B239-E150-4C8E-A313-EAA8BD6944C4"),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.PRODUCT),
                        Description = "Hàng hóa",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                //insert SELF_PRODUCTION object type
                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SELF_PRODUCTION)))
                {
                    ObjectType objectType = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SELF_PRODUCTION),
                        Description = "Sản phẩm",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectType.Save();
                }

                //insert FIXED_ASSETS object type
                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.FIXED_ASSETS)))
                {
                    ObjectType objectType = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.FIXED_ASSETS),
                        Description = "Tài sản cố định",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectType.Save();
                }

                //insert SERVICE object type
                if (!Util.isExistXpoObject<ObjectType>("Name", 
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SERVICE)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        ObjectTypeId = Guid.Parse("BEBAB7E7-8294-4EB0-81DF-B5E33ACBFE76"),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SERVICE),
                        Description = "Dịch vụ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                //insert MATERIAL object type
                if (!Util.isExistXpoObject<ObjectType>("Name", 
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MATERIAL)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        ObjectTypeId = Guid.Parse("7C32F816-23D1-4B67-97F5-940461E06305"),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MATERIAL),
                        Description = "Nguyên vật liệu",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                //insert TOOL object type
                if (!Util.isExistXpoObject<ObjectType>("Name", 
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.TOOL)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ITEM),
                        ObjectTypeId = Guid.Parse("F34C4E28-04C5-492E-AEE2-09416914F950"),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.TOOL),
                        Description = "Công cụ dụng cụ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.CUSTOMER)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ORGANIZATION),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.CUSTOMER),
                        Description = "Khách hàng",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SUPPLIER)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.ORGANIZATION),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.SUPPLIER),
                        Description = "Nhà cung cấp",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MANUFACTURER)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MANUFACTURER),
                        Description = "Nhà sản xuất",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_AUDIT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_AUDIT),
                        Description = "Phiếu kiểm kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }
                ////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_IN)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_IN),
                        Description = "Phiếu nhập kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }
                else
                {
                    ObjectType objectType =
                        Util.getXPCollection<ObjectType>(session, "Name", "INVENTORY_IN")[0];
                    objectType.Description = "Phiếu nhập kho";
                    objectType.Save();
                }
                ///////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_OUT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_OUT),
                        Description = "Phiếu xuất kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }
                else
                {
                    ObjectType objectType =
                        Util.getXPCollection<ObjectType>(session, "Name", "INVENTORY_OUT")[0];
                    objectType.Description = "Phiếu xuất kho";
                    objectType.Save();
                }
                //////////////////////////////////////////////////////////

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_MOVE)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVENTORY_MOVE),
                        Description = "Phiếu chuyển kho",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }
                else
                {
                    ObjectType objectType =
                        Util.getXPCollection<ObjectType>(session, "Name", "INVENTORY_MOVE")[0];
                    objectType.Description = "Phiếu chuyển kho";
                    objectType.Save();
                }
                ////////////////////////////////////////////////////////////
                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVOICE_PURCHASE)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVOICE_PURCHASE),
                        Description = "Phiếu mua hàng",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVOICE_SALE)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.INVOICE_SALE),
                        Description = "Phiếu bán hàng",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.VOUCHER_RECEIPT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.VOUCHER_RECEIPT),
                        Description = "Phiếu thu",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.VOUCHER_PAYMENT)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.VOUCHER_PAYMENT),
                        Description = "Phiếu chi",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                    Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MANUAL_BOOKING)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.MANUAL_BOOKING),
                        Description = "Bút toán thủ công",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<ObjectType>("Name",
                   Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.OPENBALANCE_ACCOUTING)))
                {
                    ObjectType objectTypeBO = new ObjectType(session)
                    {
                        Category = Enum.GetName(typeof(ObjectTypeCategoryEnum), ObjectTypeCategoryEnum.NAAN_DEFAULT),
                        Name = Enum.GetName(typeof(ObjectTypeEnum), ObjectTypeEnum.OPENBALANCE_ACCOUTING),
                        Description = "Số dư đầu kỳ",
                        RowStatus = 1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    objectTypeBO.Save();
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
