using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument
{

    public enum CustomFieldTypeEnum
    {
        STRING,
        DATETIME,
        FLOAT,
        INTEGER,
        SINGLE_CHOICE_LIST,
        MULTI_CHOICE_LIST,
        SINGLE_CHOICE_LIST_MANUFACTURER,
        SINGLE_CHOICE_LIST_ORGANIZATION,
        SINGLE_CHOICE_LIST_DEPARTMENT,
        SINGLE_CHOICE_LIST_PERSON,
        SINGLE_CHOICE_LIST_CUSTOMER,
        SINGLE_CHOICE_LIST_SUPPLIER,
        SINGLE_CHOICE_LIST_INVENTORY,
        SINGLE_CHOICE_LIST_LOT,
        SINGLE_CHOICE_LIST_INVOICE_SALE,
        SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
        SINGLE_CHOICE_LIST_ITEM,
        SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND,
        MULTI_CHOICE_LIST_MANUFACTURER,
        MULTI_CHOICE_LIST_ORGANIZATION,
        MULTI_CHOICE_LIST_DEPARTMENT,
        MULTI_CHOICE_LIST_PERSON,
        MULTI_CHOICE_LIST_CUSTOMER,
        MULTI_CHOICE_LIST_SUPPLIER,
        MULTI_CHOICE_LIST_INVENTORY,
        MULTI_CHOICE_LIST_LOT,
        MULTI_CHOICE_LIST_INVOICE_SALE,
        MULTI_CHOICE_LIST_INVOICE_PURCHASE,
        MULTI_CHOICE_LIST_ITEM
    }

    public partial class CustomFieldType
    {
        public CustomFieldType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        public static CustomFieldType GetDefault(Session session, CustomFieldTypeEnum code)
        {
            CustomFieldType ret = null;
            try
            {
                ret = session.FindObject<CustomFieldType>(
                    new BinaryOperator("Code", Enum.GetName(typeof(CustomFieldTypeEnum), code)));
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

                #region Basic type
                //insert default data into CustomFieldType table
                if (!Util.isExistXpoObject<CustomFieldType>("Code", "STRING"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "STRING",
                        Name = "Chuỗi",
                        Description = "Chuỗi kí tự"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "DATETIME"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "DATETIME",
                        Name = "Ngày tháng",
                        Description = "Ngày tháng"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "FLOAT"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "FLOAT",
                        Name = "Số thực",
                        Description = "Số thực"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "INTEGER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "INTEGER",
                        Name = "Số nguyên",
                        Description = "Số nguyên"
                    };
                    objectTypeBO.Save();
                }
                #endregion

                #region User-defined list type
                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST",
                        Name = "Danh sách (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST",
                        Name = "Danh sách (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }
                #endregion

                #region Predefinition type
                #region Single choice type
                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_MANUFACTURER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_MANUFACTURER",
                        Name = "Nhà sản xuất (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_ORGANIZATION"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_ORGANIZATION",
                        Name = "Tổ chức (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_DEPARTMENT"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_DEPARTMENT",
                        Name = "Phòng ban (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_PERSON"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_PERSON",
                        Name = "Nhân viên (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_CUSTOMER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_CUSTOMER",
                        Name = "Khách hàng (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_SUPPLIER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_SUPPLIER",
                        Name = "Nhà cung cấp (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_INVENTORY"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_INVENTORY",
                        Name = "Kho (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_LOT"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_LOT",
                        Name = "Lô (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_INVOICE_SALE"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_INVOICE_SALE",
                        Name = "Phiếu bán hàng (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_INVOICE_PURCHASE"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_INVOICE_PURCHASE",
                        Name = "Phiếu mua hàng (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_ITEM"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_ITEM",
                        Name = "Hàng hóa (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                /*2014/03/01 Duc.Vo INS*/
                if (!Util.isExistXpoObject<CustomFieldType>("Code", "SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND",
                        Name = "Phiếu nhập kho (chọn 1)",
                        Description = "Chỉ chọn được 1 giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }
                /*2014/03/01 Duc.Vo INS*/


                #endregion
                #region Multi choice type
                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_MANUFACTURER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_MANUFACTURER",
                        Name = "Nhà sản xuất (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_ORGANIZATION"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_ORGANIZATION",
                        Name = "Tổ chức (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_DEPARTMENT"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_DEPARTMENT",
                        Name = "Phòng ban (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_PERSON"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_PERSON",
                        Name = "Nhân viên (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_CUSTOMER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_CUSTOMER",
                        Name = "Khách hàng (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_SUPPLIER"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_SUPPLIER",
                        Name = "Nhà cung cấp (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_INVENTORY"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_INVENTORY",
                        Name = "Kho (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_LOT"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_LOT",
                        Name = "Lô (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_INVOICE_PURCHASE"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_INVOICE_PURCHASE",
                        Name = "Phiếu mua hàng (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_INVOICE_SALE"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_INVOICE_SALE",
                        Name = "Phiếu bán hàng (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }

                if (!Util.isExistXpoObject<CustomFieldType>("Code", "MULTI_CHOICE_LIST_ITEM"))
                {
                    CustomFieldType objectTypeBO = new CustomFieldType(session)
                    {
                        Code = "MULTI_CHOICE_LIST_ITEM",
                        Name = "Hàng hóa (chọn nhiều)",
                        Description = "Có thể chọn nhiều giá trị từ danh sách"
                    };
                    objectTypeBO.Save();
                }
                #endregion
                #endregion

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
