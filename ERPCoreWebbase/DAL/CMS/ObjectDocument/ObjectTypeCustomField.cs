using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using Utility;
namespace NAS.DAL.CMS.ObjectDocument
{

    public enum DefaultObjectTypeCustomFieldEnum
    {
        /// <summary>
        /// Phiếu thu - Phiếu bán
        /// </summary>
        RECEIPT_VOUCHER_SALE_INVOICE,
        /// <summary>
        /// Phiếu chi - Phiếu mua
        /// </summary>
        PAYMENT_VOUCHER_PURCHASE_INVOICE,
        /// <summary>
        /// Phiếu xuất kho - Khách hàng
        /// </summary>
        INVENTORY_OUT_CLIENT,
        /// <summary>
        /// Phiếu xuất kho - Phiếu bán
        /// </summary>
        INVENTORY_OUT_SALE_INVOICE,
        /// <summary>
        /// Phiếu nhập kho - Khách hàng
        /// </summary>
        INVENTORY_IN_CLIENT,
        /// <summary>
        /// Phiếu nhập kho - Phiếu mua
        /// </summary>
        INVENTORY_IN_PURCHASE_INVOICE,
        /// <summary>
        /// Phiếu kiểm kê
        /// </summary>
        INVENTORY_COMMAND_AUDIT,
        /// <summary>
        /// 
        /// </summary>
        INVENTORY_OPEN_BALANCE,
        /// <summary>
        /// Số dư đầu kỳ - Hàng hóa
        /// </summary>
        BALANCE_FORWARD_TRANSACTION_ITEM,
        /// <summary>
        /// Phiếu thu - Khách hàng
        /// </summary>
        RECEIPT_VOUCHER_CUSTOMER,
        /// <summary>
        /// Phiếu thu - Nhà cung cấp
        /// </summary>
        RECEIPT_VOUCHER_SUPPLIER,
        /// <summary>
        /// Phiếu chi - Nhà cung cấp
        /// </summary>
        PAYMENT_VOUCHER_SUPPLIER,
        /// <summary>
        /// Phiếu chi - Khách hàng
        /// </summary>
        PAYMENT_VOUCHER_CUSTOMER,
        /// <summary>
        /// Bút toán thủ công - Phiếu mua hàng
        /// </summary>
        MANUAL_BOOKING_PURCHASE_INVOICE,
        /// <summary>
        /// Bút toán thủ công - Phiếu nhập kho
        /// </summary>
        MANUAL_BOOKING_INPUT_INVENTORY_COMMAND,
        /// <summary>
        /// Bút toán thủ công - Khách hàng
        /// </summary>
        MANUAL_BOOKING_CUSTOMER,
        /// <summary>
        /// Bút toán thủ công - Nhà cung cấp
        /// </summary>
        MANUAL_BOOKING_SUPPLIER
    }

    public partial class ObjectTypeCustomField
    {
        public ObjectTypeCustomField(Session session) : base(session) { }
        public override void AfterConstruction() { 
            base.AfterConstruction();
            CustomFieldType = CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT;
        }

        #region Logic
        public static void AttachCustomFieldsToObjectType(List<CustomFieldOption> customFields, ObjectTypeEnum objectType)
        {
            if (customFields == null)
                throw new Exception("Input Parameter is null");
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                CustomFieldOption o = new CustomFieldOption();
                try
                {
                    ObjectType OT = ObjectType.GetDefault(uow, objectType);

                    if (OT == null)
                        throw new Exception("The ObjectType is not exist in system");

                    for (int i = 0; i < customFields.Count; i++)
                    {
                        NAS.DAL.CMS.ObjectDocument.CustomFieldType CFT =
                            NAS.DAL.CMS.ObjectDocument.CustomFieldType.GetDefault(uow, customFields[i].CustomFieldType);
                        if (CFT == null)
                            throw new Exception("The CustomFieldType is not exist in system");

                        NAS.DAL.CMS.ObjectDocument.CustomField CF =
                        uow.FindObject<NAS.DAL.CMS.ObjectDocument.CustomField>(
                            CriteriaOperator.And(
                                new BinaryOperator("Code", customFields[i].FieldCode, BinaryOperatorType.Equal),
                                new NotOperator(new NullOperator("CustomFieldTypeId")),
                                new BinaryOperator("CustomFieldTypeId", CFT, BinaryOperatorType.Equal)
                            ));

                        if (CF == null)
                        {
                            CF = new DAL.CMS.ObjectDocument.CustomField(uow);
                            CF.Code = customFields[i].FieldCode;
                            CF.Name = customFields[i].FieldName;
                            CF.CustomFieldTypeId = CFT;
                            CF.CustomFieldType = customFields[i].CustomFieldFlag.Value;
                            uow.FlushChanges();

                            NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField OTCF = 
                                new NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField(uow);
                            OTCF.Code = customFields[i].ObjectTypeFieldCode;
                            OTCF.CustomFieldId = CF;
                            OTCF.ObjectTypeId = OT;
                            OTCF.CustomFieldType = customFields[i].ObjectTypeCustomFieldFlag.Value;
                            uow.FlushChanges();
                        }
                        else
                        {
                            int countExist = OT.ObjectTypeCustomFields.Where(r => r.Code == customFields[i].ObjectTypeFieldCode).Count();
                            if (countExist == 0)
                            {
                                NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField OTCF = 
                                    new NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField(uow);
                                OTCF.Code = customFields[i].ObjectTypeFieldCode;
                                OTCF.CustomFieldId = CF;
                                OTCF.ObjectTypeId = OT;
                                OTCF.CustomFieldType = customFields[i].ObjectTypeCustomFieldFlag.Value;
                                uow.FlushChanges();
                            }
                        }
                    }
                    uow.CommitChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    uow.Dispose();
                }
            }
        }

        public static void Populate()
        {
            #region Receipt Voucher
            //Populate custom field to receipt voucher
            List<CustomFieldOption> options = new List<CustomFieldOption>();
            CustomFieldOption option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "RECEIPT_VOUCHER_SALE_INVOICE",
                FieldCode = "SALE_INVOICE",
                FieldName = "Phiếu bán",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER
            };
            options.Add(option);
            /*2014-01-20 ERP-1458 Khoa.Truong INS START*/
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "RECEIPT_VOUCHER_CUSTOMER",
                FieldCode = "CLIENT",
                FieldName = "Khách hàng",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            /*2014-01-20 ERP-1458 Khoa.Truong INS END*/

            /*2014-01-22 ERP-1458 Khoa.Truong INS START*/
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "RECEIPT_VOUCHER_SUPPLIER",
                FieldCode = "SUPPLIER",
                FieldName = "Nhà cung cấp",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            /*2014-01-22 ERP-1458 Khoa.Truong INS END*/
            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.VOUCHER_RECEIPT);
            #endregion

            #region Payment Voucher
            //Populate custom field to payment voucher
            options = new List<CustomFieldOption>();
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "PAYMENT_VOUCHER_PURCHASE_INVOICE",
                FieldCode = "PURCHASE_INVOICE",
                FieldName = "Phiếu mua",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER
            };
            options.Add(option);
            /*2014-01-20 ERP-1458 Khoa.Truong INS START*/
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "PAYMENT_VOUCHER_SUPPLIER",
                FieldCode = "SUPPLIER",
                FieldName = "Nhà cung cấp",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            /*2014-01-20 ERP-1458 Khoa.Truong INS END*/

            /*2014-01-22 ERP-1458 Khoa.Truong INS START*/
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "PAYMENT_VOUCHER_CUSTOMER",
                FieldCode = "CLIENT",
                FieldName = "Khách hàng",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            /*2014-01-22 ERP-1458 Khoa.Truong INS END*/

            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.VOUCHER_PAYMENT);
            #endregion

            #region Inventory command
            options = new List<CustomFieldOption>();
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "INVENTORY_OUT_CLIENT",
                FieldCode = "CLIENT",
                FieldName = "Khách hàng",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "INVENTORY_OUT_SALE_INVOICE",
                FieldCode = "SALE_INVOICE",
                FieldName = "Phiếu bán",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER
            };
            options.Add(option);
            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.INVENTORY_OUT);

            //Populate custom field to in inventory command
            options = new List<CustomFieldOption>();
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "INVENTORY_IN_CLIENT",
                FieldCode = "CLIENT",
                FieldName = "Khách hàng",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "INVENTORY_IN_PURCHASE_INVOICE",
                FieldCode = "PURCHASE_INVOICE",
                FieldName = "Phiếu mua",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER
            };
            options.Add(option);
            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.INVENTORY_IN);
            #endregion

            #region BalanceForward Inventory
            options = new List<CustomFieldOption>();
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "INVENTORY_OPEN_BALANCE",
                FieldCode = "INVENTORY",
                FieldName = "Kho đầu kỳ",                
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVENTORY,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER
            };

            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "BALANCE_FORWARD_TRANSACTION_ITEM",
                FieldCode = "ITEM",
                FieldName = "Hàng hóa",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_ITEM,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);

            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.OPENBALANCE_ACCOUTING);
            #endregion

            #region Manual Booking
            options = new List<CustomFieldOption>();
            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "MANUAL_BOOKING_PURCHASE_INVOICE",
                FieldCode = "PURCHASE_INVOICE",
                FieldName = "Phiếu mua",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);

            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "MANUAL_BOOKING_INPUT_INVENTORY_COMMAND",
                FieldCode = "INPUT_INVENTORY_COMMAND",
                FieldName = "Phiếu nhập kho",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);

            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "MANUAL_BOOKING_CUSTOMER",
                FieldCode = "CUSTOMER",
                FieldName = "Khách hàng",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);

            option = new CustomFieldOption()
            {
                ObjectTypeFieldCode = "MANUAL_BOOKING_SUPPLIER",
                FieldCode = "SUPPLIER",
                FieldName = "Nhà cung cấp",
                CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY,
                CustomFieldType = CustomFieldTypeEnum.SINGLE_CHOICE_LIST_SUPPLIER,
                ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY
            };
            options.Add(option);
            
            ObjectTypeCustomField.AttachCustomFieldsToObjectType(options, ObjectTypeEnum.MANUAL_BOOKING);

            #endregion
        }

        public static ObjectTypeCustomField GetDefault(Session session,
            DefaultObjectTypeCustomFieldEnum value)
        {
            ObjectTypeCustomField ret = null;
            try
            {
                ret = session.FindObject<ObjectTypeCustomField>(
                    new BinaryOperator("Code", Enum.GetName(typeof(DefaultObjectTypeCustomFieldEnum), value)));
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }

    public class CustomFieldOption
    {
        public CustomFieldTypeEnum CustomFieldType
        {
            get;
            set;
        }

        public string ObjectTypeFieldCode
        {
            get;
            set;
        }

        public string FieldName
        {
            get;
            set;
        }

        public string FieldCode
        {
            get;
            set;
        }

        public CustomFieldTypeFlag CustomFieldFlag
        {
            get;
            set;
        }

        public CustomFieldTypeFlag ObjectTypeCustomFieldFlag
        {
            get;
            set;
        }
    }

}
