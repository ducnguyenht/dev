using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public enum SingleSelectionBuiltInTypeEnum
    {
        SINGLE_CHOICE_LIST_MANUFACTURER,
        SINGLE_CHOICE_LIST_ORGANIZATION,
        SINGLE_CHOICE_LIST_DEPARTMENT,
        SINGLE_CHOICE_LIST_PERSON,
        SINGLE_CHOICE_LIST_CUSTOMER,
        SINGLE_CHOICE_LIST_SUPPLIER,
        SINGLE_CHOICE_LIST_INVENTORY,
        SINGLE_CHOICE_LIST_LOT,
        SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
        SINGLE_CHOICE_LIST_INVOICE_SALE,
        SINGLE_CHOICE_LIST_ITEM,
        /*2014/03/01 Duc.Vo INS*/
        SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND
        /*2014/03/01 Duc.Vo INS*/
    }

    public static class NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreators
    {
        public static NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreator GetCreator(SingleSelectionBuiltInTypeEnum type)
        {
            NASCustomFieldTypeBuiltInSingleSelectionListStrategyCreator ret = null;
            switch (type)
            {
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_MANUFACTURER:
                    ret = new NASCustomFieldTypeManufacturerBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_ORGANIZATION:
                    ret = new NASCustomFieldTypeOrganizationBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_DEPARTMENT:
                    ret = new NASCustomFieldTypeDepartmentBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_PERSON:
                    ret = new NASCustomFieldTypePersonBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER:
                    ret = new NASCustomFieldTypeCustomerBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_SUPPLIER:
                    ret = new NASCustomFieldTypeSupplierBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVENTORY:
                    ret = new NASCustomFieldTypeInventoryBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_LOT:
                    ret = new NASCustomFieldTypeLotBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE:
                    ret = new NASCustomFieldTypePurchaseInvoiceBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE:
                    ret = new NASCustomFieldTypeSaleInvoiceBuiltInSingleSelectionListStrategyCreator();
                    break;
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_ITEM:
                    ret = new NASCustomFieldTypeItemBuiltInSingleSelectionListStrategyCreator();
                    break;
                /*2014/03/01 Duc.Vo INS*/
                case SingleSelectionBuiltInTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND:
                    ret = new NASCustomFieldTypeInputInventoryCommandBuiltInSingleSelectionListStrategyCreator();
                    break;
                /*2014/03/01 Duc.Vo INS*/
                default:
                    break;
            }
            return ret;
        }

    }
}