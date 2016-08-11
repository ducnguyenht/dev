using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DAL.Staging.Accounting.Journal;

namespace WebModule.Accounting.AllocationConfigure.Controls
{
    public static class AccountActorComboBoxStrategyCreators
    {
        public static AccountActorComboBoxStrategyCreator GetCreator(AccountActorTypeEnum type)
        {
            AccountActorComboBoxStrategyCreator ret = null;
            switch (type)
            {
                case AccountActorTypeEnum.ORGANIZATION:
                    ret = new AccountActorOrganizationComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.DEPARTMENT:
                    ret = new AccountActorDepartmentComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.PERSON:
                    ret = new AccountActorPersonComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.CUSTOMER:
                    ret = new AccountActorCustomerComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.MANUFACTURER:
                    ret = new AccountActorManufacturerComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.SUPPLIER:
                    ret = new AccountActorSupplierComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.INVENTORY:
                    break;
                case AccountActorTypeEnum.LOT:
                    ret = new AccountActorLotComboBoxStrategyCreator();
                    break;
                case AccountActorTypeEnum.BILL:
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}