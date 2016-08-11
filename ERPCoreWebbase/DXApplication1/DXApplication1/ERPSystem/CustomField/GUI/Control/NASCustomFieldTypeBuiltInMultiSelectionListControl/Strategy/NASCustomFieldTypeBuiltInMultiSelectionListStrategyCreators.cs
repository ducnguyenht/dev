using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy
{

    public enum MultiSelectionBuiltInTypeEnum
    {
        MULTI_CHOICE_LIST_MANUFACTURER,
        MULTI_CHOICE_LIST_ORGANIZATION,
        MULTI_CHOICE_LIST_DEPARTMENT,
        MULTI_CHOICE_LIST_PERSON,
        MULTI_CHOICE_LIST_CUSTOMER,
        MULTI_CHOICE_LIST_SUPPLIER,
        MULTI_CHOICE_LIST_INVENTORY,
        MULTI_CHOICE_LIST_LOT,
        MULTI_CHOICE_LIST_INVOICE_PURCHASE,
        MULTI_CHOICE_LIST_INVOICE_SALE,
        MULTI_CHOICE_LIST_ITEM
    }

    public static class NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreators
    {
        public static NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreator 
            GetCreator(MultiSelectionBuiltInTypeEnum type)
        {
            NASCustomFieldTypeBuiltInMultiSelectionListStrategyCreator ret = null;
            switch (type)
            {
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_MANUFACTURER:
                    ret = new NASCustomFieldTypeManufacturerBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_ORGANIZATION:
                    ret = new NASCustomFieldTypeOrganizationBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_DEPARTMENT:
                    ret = new NASCustomFieldTypeDepartmentBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_PERSON:
                    ret = new NASCustomFieldTypePersonBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_CUSTOMER:
                    ret = new NASCustomFieldTypeCustomerBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_SUPPLIER:
                    ret = new NASCustomFieldTypeSupplierBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVENTORY:
                    ret = new NASCustomFieldTypeInventoryBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_LOT:
                    ret = new NASCustomFieldTypeLotBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_PURCHASE:
                    ret = new NASCustomFieldTypePurchaseInvoiceBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_INVOICE_SALE:
                    ret = new NASCustomFieldTypeSaleInvoiceBuiltInMultiSelectionListStrategyCreator();
                    break;
                case MultiSelectionBuiltInTypeEnum.MULTI_CHOICE_LIST_ITEM:
                    ret = new NASCustomFieldTypeItemBuiltInMultiSelectionListStrategyCreator();
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}