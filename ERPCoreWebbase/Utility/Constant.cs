using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{

    public sealed class DbRowStatusCode
    {
        public const string Process_Running_Mutex_Name_ETLService = "Global\\ETLServiceRunning";
        public const string Process_Running_Mutex_Name_Accounting = "Global\\AccountingProcessRunning";
        public const string Process_Running_Mutex_Name_System = "Global\\SystemProcessRunning";
        public const string Process_Running_Mutex_Name_WareHouse = "Global\\WareHouseProcessRunning";
        public const string Process_Running_Mutex_Name_Sale = "Global\\SaleProcessRunning";
        public const string Process_Running_Mutex_Name_Purchase = "Global\\PurchaseProcessRunning";
        public const string Process_Running_Mutex_Name_ETLMaster = "Global\\ETLMasterProcessRunning";

        public const string Process_Stop_Mutex_Name_Accounting = "Global\\AccountingProcessStop";
        public const string Process_Stop_Mutex_Name_System = "Global\\SystemProcessStop";
        public const string Process_Stop_Mutex_Name_WareHouse = "Global\\WareHouseProcessStop";
        public const string Process_Stop_Mutex_Name_Sale = "Global\\SaleProcessStop";
        public const string Process_Stop_Mutex_Name_Purchase = "Global\\PurchaseProcessStop";
        public const string Process_Stop_Mutex_Name_ETLMaster = "Global\\ETLMasterProcessStop";
        private char value;

        //public static readonly DbRowStatusCode Active = new DbRowStatusCode(Constant.ROWSTATUS_ACTIVE);
        //public static readonly DbRowStatusCode Inactive = new DbRowStatusCode(Constant.ROWSTATUS_INACTIVE);
        //public static readonly DbRowStatusCode Obsolete = new DbRowStatusCode(Constant.ROWSTATUS_OBSOLETE);
        //public static readonly DbRowStatusCode Deleted = new DbRowStatusCode(Constant.ROWSTATUS_DELETED);

        private DbRowStatusCode(char v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public char Value { get { return value; } }

    }


    public sealed class OrganizationRetalionType
    {
        private char value;

        public static readonly OrganizationRetalionType Child =
            new OrganizationRetalionType(Constant.RELATION_TYPE_CHILD);
        public static readonly OrganizationRetalionType Parent =
            new OrganizationRetalionType(Constant.RELATION_TYPE_PARENT);

        private OrganizationRetalionType(char v)
        {
            value = v;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public char Value { get { return value; } }

    }

    public class Constant
    {
        //Calculation type
        public const char CALCULATION_TYPE_ON_ITEMS = 'I';
        public const char CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE = 'P';
        public const char CALCULATION_TYPE_ON_BILL_BY_AMOUNT = 'T';

        //Row status constants
        public const short ROWSTATUS_INITIAL = -10;
        public const short ROWSTATUS_FINAL = -11;
        public const short ROWSTATUS_ACTIVE = 1;
        public const short ROWSTATUS_INACTIVE = 2;
        public const short ROWSTATUS_OBSOLETE = -3;
        public const short ROWSTATUS_DELETED = -2;
        public const short ROWSTATUS_DEFAULT = -1;
        public const short ROWSTATUS_TEMP = 0;
        public const short ROWSTATUS_DISABLE = 3;
        public const short ROWSTATUS_DEFAULT_SELECTEDALL = -4;
        public const short ROWSTATUS_BOOKED_ENTRY = 4;
        public const short ROWSTATUS_CLOSED_PERIOD = 5;
        public const short ROWSTATUS_ETL_UP_UPDATING = 16;
        public const short ROWSTATUS_DRILLING_UP_UPDATING = 8;
        //End row status constants
        
        //Transition Constants
        public const short TRANSITION_CREATING = 0;
        public const short TRANSITION_POPULATE = 1;
        public const short TRANSITION_SYSTEM_DELETE = 2;
        public const short TRANSITION_EDITING = 3;
        public const short TRANSITION_COMPLETE = 4;
        public const short TRANSITION_ETL_DELETE = 5;
        public const short TRANSITION_ETL_BEGIN = 6;
        public const short TRANSITION_ETL_COMPLETE = 7;
        public const short TRANSITION_USER_DELETE = 8;
        public const short TRANSITION_USER_ENABLE = 9;
        public const short TRANSITION_USER_DISABLE = 10;
        public const short TRANSITION_DRILLING_UP_UPDATING = 11;
        public const short TRANSITION_DRILLING_UP_COMPLETE = 12;
        //End Transition Constants

        //
        public const short STATUS_EXCHANGE_RATE_ACTIVE = 1;
        public const short STATUS_EXCHANGE_RATE_INACTIVE = 0;
        //
        
        //Balance Type
        public const short BALANCE_TYPE_DEBIT = 1;
        public const short BALANCE_TYPE_CREDIT = -1;
        public const short BALANCE_TYPE_ABOUT = 0;
        //
        
        //Constant Id for Operation
        public const string DEFAULT_MANUFACTURER = "C378ACC0-4961-4E53-99A5-00004358A752";
        public const string ROOTUNITNODE = "29407EF6-08FE-48BD-B5B8-53E1222F61B6";
        public const string ROOTUNITNODECODE = "ROOT";
        public const string ROOTUNITNODENAME = "Thông tin đơn vị tính";
        public const string NOT_IDENTIFIED = "BE663B99-AC76-4D27-AC6E-DBEC9E3DB62A";
        public const string CATEGORY_FINISHPRODUCT = "768BE24D-536D-4612-8F45-05F5A3CDD5CD";
        //End Constant Id for Operation

        public const char RELATION_TYPE_PARENT = 'P';
        public const char RELATION_TYPE_CHILD = 'C';

        //Constant value for operation
        public const int OPERATION_DISABLE_VALUE = 0;
        public const int OPERATION_READ_VALUE = 1;
        public const int OPERATION_EXECUTE_VALUE = 2;
        public const int OPERATION_CREATE_VALUE = 4;
        public const int OPERATION_UPDATE_VALUE = 8;
        public const int OPERATION_DELETE_VALUE = 16;
        public const int OPERATION_DENY_VALUE = 32;
        //End Constant value for operation

        //Gender user
        public const string GENDER_MALE = "M"; 
        public const string GENDER_FEMALE = "F";
        public const string GENDER_OTHER = "O";
        //End gender user

        public const string LANG_DEFAULT = "VN";

        public static readonly Guid ORGANIZATION_ROOT = Guid.Parse("fb74e0a8-5c18-49f3-8fc0-c223d40ea733");

        public const char ADMIN = 'A';
        public const char NON_ADMIN = 'N';

        public const bool AUTHORIZATION_GRANT = true;
        public const bool AUTHORIZATION_DENY = false;

        //Start AccessObject Group Id
        public const string ACCESSOBJECT_DEFAULT_GROUPID = "default";
        public const string ACCESSOBJECT_SYSTEM_GROUPID = "system";
        public const string ACCESSOBJECT_PURCHASE_GROUPID = "purchase";
        public const string ACCESSOBJECT_SALES_GROUPID = "sales";
        public const string ACCESSOBJECT_WAREHOUSE_GROUPID = "warehouse";
        public const string ACCESSOBJECT_PAYRECEIVE_GROUPID = "payreceive";
        public const string ACCESSOBJECT_ACCOUNT_GROUPID = "account";
        public const string ACCESSOBJECT_IMEXPORT_GROUPID = "imexport";
        public const string ACCESSOBJECT_PRODUCE_GROUPID = "produce";
        public const string ACCESSOBJECT_AUTHORIZATION_GROUPID = "authorization";
        public const string ACCESSOBJECT_MANAGECONTENT_GROUPID = "managecontent";
        public const string ACCESSOBJECT_BRANCH_GROUPID = "branch";
        public const string ACCESSOBJECT_MEMBER_GROUPID = "member";
        public const string ACCESSOBJECT_BANK_GROUPID = "bank";
        //End AccessObject Group Id

        //AccessObjectId System
        public const string ACCESSOBJECT_CUSTOMERTYPE_ID = "customertype";
        public const string ACCESSOBJECT_CUSTOMER_ID = "customer";
        public const string ACCESSOBJECT_CUSTOMERSERVICE_ID = "customerservice";

        public const string ACCESSOBJECT_SYSTEM_DBCONFIG_ID = "system-dbconfig";
        public const string ACCESSOBJECT_SYSTEM_MAILSERVERCONFIG_ID = "system-mailserverconfig";
        //End AccessObjectId System

        //AccessObjectId Application
        public const string ACCESSOBJECT_APPLICATIONTYPE_ID = "applicationtype";
        public const string ACCESSOBJECT_APPLICATION_ID = "Application";
        //End AccessObjectId Application

        //AccessObjectId Deploy
        public const string ACCESSOBJECT_DEPLOY_ID = "deploy";
        //End AccessObjectId Application

        //AccessObjectId Authorization
        public const string ACCESSOBJECT_AUTHORIZATION_ID = "authorization";
        public const string ACCESSOBJECT_AUTHORIZATION_ORGANIZATIONID = "authorization-organization";
        public const string ACCESSOBJECT_AUTHORIZATION_USERID = "authorization-user";
        public const string ACCESSOBJECT_AUTHORIZATION_INVITEUSERID = "authorization-invite-user";
        
        //End AccessObjectId Authorization

        //AccessObjectId ManageContent
        public const string ACCESSOBJECT_MANAGECONTENT_ID = "management";
        //End AccessObjectId ManageContent

        //AccessObjectId Branch
        public const string ACCESSOBJECT_BRANCH_ID = "branch";
        //End AccessObjectId ManageContent

        //AccessObjectId Default
        public const string ACCESSOBJECT_DEFAULT_ID = "default";
        //AccessObjectId Default

        //AccessObjectId Member
        public const string ACCESSOBJECT_MEMBER_BUSINESS_ID = "member-business";
        public const string ACCESSOBJECT_MEMBER_BUSINESSPERSON_ID = "member-businessperson";
        //End AccessObjectId Member

        //AccessObjectId Produce
        public const string ACCESSOBJECT_PRODUCE_PRODUCT_ID = "produce-product";
        public const string ACCESSOBJECT_PRODUCE_FINISHEDPRODUCT_ID = "produce-finishedproduct";
        public const string ACCESSOBJECT_PRODUCE_PRODUCEREQUIREMENTID = "produce-producerequirement";
        public const string ACCESSOBJECT_PRODUCE_MATERIALREQUIREMENTID = "produce-materialrequirement";
        public const string ACCESSOBJECT_PRODUCE_HUMANREQUIREMENTID = "produce-humanrequirement";
        public const string ACCESSOBJECT_PRODUCE_EQUIPMENTREQUIREMENTID = "produce-equipmentrequirement";
        public const string ACCESSOBJECT_PRODUCE_PRICEESTIMATESID = "produce-priceestimates";
        public const string ACCESSOBJECT_PRODUCE_PLANNINGDETAILID = "produce-planningdetail";
        public const string ACCESSOBJECT_PRODUCE_GANTTCHARTID = "produce-ganttchart";
        public const string ACCESSOBJECT_PRODUCE_PRODUCTIONCOMMANDEXECUTION_ID = "produce-productioncommandexecution";
        public const string ACCESSOBJECT_PRODUCE_PRODUCTIONCOMMAND_ID = "produce-productioncommand";
        public const string ACCESSOBJECT_PRODUCE_UNFINISHEDPRODUCT_ID = "produce-unfinishedproduct";
        public const string ACCESSOBJECT_PRODUCE_MATERIAL_ID = "produce-material";
        public const string ACCESSOBJECT_PRODUCE_PHASE_ID = "produce-phase";
        public const string ACCESSOBJECT_PRODUCE_REPORT = "produce-report";
        //End Produce

        //AccessObjectId Warehouse
        public const string ACCESSOBJECT_WAREHOUSE_WAREHOUSE_ID = "warehouse-warehouse";
        public const string ACCESSOBJECT_WAREHOUSE_INITINVENTORY_ID = "warehouse-init-inventory";
        public const string ACCESSOBJECT_WAREHOUSE_STOCKCART_ID = "warehouse-stockcart";
        public const string ACCESSOBJECT_WAREHOUSE_UNIT_ID = "warehouse-unit";
        public const string ACCESSOBJECT_WAREHOUSE_STORAGE_ID = "warehouse-storage";
        public const string ACCESSOBJECT_WAREHOUSE_CATEGORY_ID = "warehouse-category";
        public const string ACCESSOBJECT_WAREHOUSE_INPUTCOMM_ID = "warehouse-inputcomm";
        public const string ACCESSOBJECT_WAREHOUSE_OUTPUTCOMM_ID = "warehouse-outputcomm";
        public const string ACCESSOBJECT_WAREHOUSE_INPUTWAREHOUSE_ID = "warehouse-inputwarehouse";
        public const string ACCESSOBJECT_WAREHOUSE_OUTPUTWAREHOUSE_ID = "warehouse-outputwarehouse";
        public const string ACCESSOBJECT_WAREHOUSE_WAREHOUSEINVENTORY_ID = "warehouse-inventory";
        public const string ACCESSOBJECT_WAREHOUSE_HISTORYENTRY_ID = "warehouse-historyentry";
        public const string ACCESSOBJECT_WAREHOUSE_INTELLIGENTWAREHOUSE_ID = "warehouse-intelligent";
        public const string ACCESSOBJECT_WAREHOUSE_SOURCEEXIST_ID = "warehouse-sourceexist";
        public const string ACCESSOBJECT_WAREHOUSE_SOURCEEXISTMATERIAL_ID = "warehouse-sourceexistmaterial";
        public const string ACCESSOBJECT_WAREHOUSE_SOURCEEXISTPRODUCT_ID = "warehouse-sourceexistproduct";
        public const string ACCESSOBJECT_WAREHOUSE_SOURCEEXISTEQUIPMENT_ID = "warehouse-sourceexistequipment";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTPRODUCT_ID = "warehouse-reportproduct";
        public const string ACCESSOBJECT_WAREHOUSE_REPORT_ID = "warehouse-report";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTRP_ID = "warehouse-report-rp";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTMATERIAL_ID = "warehouse-reportmaterial";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTDEVICE_ID = "warehouse-reportdevice";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTFINISHEDPRODUCT_ID = "warehouse-reportfinishedproduct";
        public const string ACCESSOBJECT_WAREHOUSE_REPORTINVENTORY_ID = "warehouse-reportinventory";
        public const string ACCESSOBJECT_WAREHOUSE_INPUTINVENTORYCOMMAND_ID = "warehouse-inputinventorycommand";
        public const string ACCESSOBJECT_WAREHOUSE_OUTPUTINVENTORYCOMMAND_ID = "warehouse-outputinventorycommand";
        public const string ACCESSOBJECT_WAREHOUSE_MOVEINVENTORYCOMMAND_ID = "warehouse-moveinventorycommand";
        //End AccessObjectId Warehouse
        //Start AccessObjectId
        public const string ACCESSOBJECT_SYSTEM_ID = "system";
        public const string ACCESSOBJECT_SYSTEM_USER_ID = "system-user";
        public const string ACCESSOBJECT_SYSTEM_DEPARTMENT_ID = "system-department";
        public const string ACCESSOBJECT_SYSTEM_ORGANIZATION_ID = "system-organization";
        public const string ACCESSOBJECT_SYSTEM_AUTHORIZATION_ID = "system-authorization";
        public const string ACCESSOBJECT_PURCHASE_PRODUCT_ID = "purchase-product";
        public const string ACCESSOBJECT_PURCHASE_MATERIAL_ID = "purchase-material";
        public const string ACCESSOBJECT_PURCHASE_SERVICE_ID = "purchase-service";
        public const string ACCESSOBJECT_PURCHASE_DEVICE_ID = "purchase-device";
        public const string ACCESSOBJECT_PURCHASE_MANUFACTURER_ID = "purchase-manufacturer";
        public const string ACCESSOBJECT_PURCHASE_COOPERATIVEPRINCIPLES_ID = "purchase-cooperativeprincilples";
        public const string ACCESSOBJECT_PURCHASE_SUPPLIER_ID = "purchase-supplier";
        public const string ACCESSOBJECT_PURCHASE_PURCHASE_DEVICE_ID = "purchase-purchasedevice";
        public const string ACCESSOBJECT_PURCHASE_PURCHASE_FIXEDASSESTS_ID = "purchase-purchasefixedassests";
        public const string ACCESSOBJECT_PURCHASE_PURCHASE_MATERIALS_ID = "purchase-purchasematerials";
        public const string ACCESSOBJECT_PURCHASE_PROCESSOFSALESTOTAL_ID = "purchase-processofsalestotal";
        public const string ACCESSOBJECT_PURCHASE_BRINGBACKPRODUCT_ID = "purchase-bringbackproduct";
        public const string ACCESSOBJECT_PURCHASE_ACTIVEELEMENT = "purchase-activeelement";
        public const string ACCESSOBJECT_PURCHASE_ITEMUNIT = "purchase-itemunit";
        public const string ACCESSOBJECT_SALES_CUSTOMER_ID = "sales-customer";
        public const string ACCESSOBJECT_SALES_PARTNER_ID = "sales-partner";
        public const string ACCESSOBJECT_SALES_MATERIAL_ID = "sales-material";
        public const string ACCESSOBJECT_SALES_SERVICE_ID = "sales-service";
        public const string ACCESSOBJECT_SALES_PRODUCT_ID = "sales-product";
        public const string ACCESSOBJECT_SALES_PROMOTION_ID = "sales-promotion";
        public const string ACCESSOBJECT_SALES_POLICY_ID = "sales-policy";
        public const string ACCESSOBJECT_SALES_MODIFYPOLICY_ID = "sales-modifypolicy";
        public const string ACCESSOBJECT_SALES_DEBTCUST_ID = "sales-debtcust";
        public const string ACCESSOBJECT_SALES_DEBTPARTNER_ID = "sales-debtpartner";
        public const string ACCESSOBJECT_SALES_ORDERS_ID = "sales-orders";
        public const string ACCESSOBJECT_SALES_ODERSPROCESS_ID = "sales-ordersprocess";
        public const string ACCESSOBJECT_SALES_PROCESSOFSALESTOTAL_ID = "sales-processofsalestotal";
        public const string ACCESSOBJECT_SALES_QUOTATION_ID = "sales-quotation";
        public const string ACCESSOBJECT_SALES_BRINGBACKPRODUCT_ID = "sales-bringbackproduct";
        public const string ACCESSOBJECT_SALES_CONDITIONTORECEIVE_ID = "sales-conditiontoreceive";
        public const string ACCESSOBJECT_SALES_COOPERATIVEPRINCIPLES_ID = "sales-cooperativeprinciples";
        public const string ACCESSOBJECT_SALES_TEST_ID = "sales-test";
        //End AccessObjectId

        //AccessObjectId Warehouse ACCESSOBJECT_ACCOUNTING_INTVOU ACCESSOBJECT_ACCOUNT_CURRENCY
        public const string ACCESSOBJECT_ACCOUNT_INTVOU = "account-hachtoanxuatkho";
        public const string ACCESSOBJECT_ACCOUNT_SDDINHKHOAN_ID = "account-sododinhkhoan";
        public const string ACCESSOBJECT_ACCOUNT_GROSSSPLITPURCHASE_ID = "account-grosssplitpurchase";
        public const string ACCESSOBJECT_ACCOUNT_GROSSSPLITSALE_ID = "account-grosssplitsale";
        public const string ACCESSOBJECT_ACCOUNT_VATRECEIPTFORPRINTING_ID = "account-vatreceiptforprinting";
        public const string ACCESSOBJECT_ACCOUNT_DEFFERED_EXP_PROCESS_ID = "account-deffered_exp_process";
        public const string ACCESSOBJECT_ACCOUNT_RESTORE_DEFFERED_EXP_PROCESS_ID = "account-restore_deffered_exp_process";
        public const string ACCESSOBJECT_ACCOUNT_PHIEUMUA_ID ="account-phieumua";
        public const string ACCESSOBJECT_ACCOUNT_PRODUCTFORINVENTORIES_CACULATION_ID = "account-prodct_inventory_caculation";
        public const string ACCESSOBJECT_ACCOUNT_GENERALACCOUNT_ID = "account-general-account";
        public const string ACCESSOBJECT_ACCOUNT_GENERALLEDGER_ID = "account-general-ledger";
        public const string ACCESSOBJECT_ACCOUNT_TRANSACTIONHISTORY_ID = "account-transaction-history";        
        public const string ACCESSOBJECT_ACCOUNT_CURRENCY_ID = "account-currency";
        public const string ACCESSOBJECT_ACCOUNT_VATRECEIPT_ID = "account-VAT-receipt";
        public const string ACCESSOBJECT_ACCOUNT_RECEIPTVOUCHERSPLIT_ID = "account-currency";
        public const string ACCESSOBJECT_ACCOUNT_GERNERALJOURNAL_ID = "account-general-journal";
        public const string ACCESSOBJECT_ACCOUNT_BALANCESHEET_ID = "account-balance-sheet";
        public const string ACCESSOBJECT_ACCOUNT_ACCOUNTINGENTRY_ID = "account-accounting-entry";
        public const string ACCESSOBJECT_ACCOUNT_REPORTPRODUCTCOGS_ID = "account-reportproductcogs";
        public const string ACCESSOBJECT_ACCOUNT_TAXTYPESETTING_ID = "account-taxtypesetting";
        public const string ACCESSOBJECT_ACCOUNT_CONFIG_ALLOCATION = "account-config-allocation";
        public const string ACCESSOBJECT_ACCOUNT_CURRENCY_UNIT_ID = "account-currency-unit";
        
        //End AccessObjectId
        
        // AccessObjectId report
        public const string ACCESSOBJECT_ACCOUNT_DIARYGENERAL_ID = "report-diarygeneral";
        public const string ACCESSOBJECT_ACCOUNT_DIARYLEDGER_ID = "report-diaryledger";
        public const string ACCESSOBJECT_ACCOUNT_VOUCHERLEDGER_ID = "report-voucherledger";
        public const string ACCESSOBJECT_ACCOUNT_DIARYVOUCHER_ID = "report-diaryvoucher";
        public const string ACCESSOBJECT_PAYRECEIVE_DIARYCURRENCY_ID = "report-diarycurrency";
        //End AccessObjectId report  

        public const string ACCESSOBJECT_PAYRECEIVE_PAYMENTSCHEDULE = "payreceiving-schedule";
        public const string ACCESSOBJECT_PAYRECEIVE_PAYRECEIVINGREPORT_ID = "payreceiving-report";

        //  AccessObjectId Bank
        public const string ACCESSOBJECT_BANK_LIST_ID = "bank-list";
        public const string ACCESSOBJECT_ACCOUNTBANK_LIST_ID = "accountbank-list";
        public const string ACCESSOBJECT_RECEIPT_VOUCHERBANK_ID = "receiptvoucherbank-list";
        public const string ACCESSOBJECT_PAYMENT_VOUCHERBANK_ID = "paymentvoucherbank-list";
        public const string ACCESSOBJECT_BORROW_BANK_ID = "borrowbank-list";

        //

        //Hash algorithm constants
        public const string HASH_ALORITHM_MD5 = "MD5";

        //default input string
        public const string DEFAULT_DB_STRING = "NA-NA-NA-NA";

        public const string NAAN_DEFAULT_NOTAVAILABLE = "N/A";
        public const string NAAN_DEFAULT_NAME = "NAAN_DEFAULT";
        public const string NAAN_DEFAULT_CODE = "NAAN_DEFAULT";
        public const string NAAN_DEFAULT_CODE_SELECTEDALL = "NAAN_DEFAULT_SELECTEDALL";

        public const string DEFAULT_CODE = "default";

        public const string RECEIPT_PURCHASE = "PURCHASE";
        public const string RECEIPT_SALE = "SALE";

        public const char PLANNING_JOURNAL = 'P';
        public const char ACTUAL_JOURNAL = 'A';

        public const char VAT_NO_DECLARE = 'N';
        public const char VAT_YES_DECLARE = 'Y';

        public const char APRROVE_NO = 'N';
        public const char APRROVE_YES = 'Y';

        public const char VAT_IN = 'I';
        public const char VAT_OUT = 'O';

        #region ETL
        public enum MarkError : short
        {
            IsNotExistMark = -4,
            JobError = -3,
            BusinessObjectError = -2,
            Error = -1,
            Successed = 0,
            IsMarked = 1
        };
        public const string Process_Running_Mutex_Name_ETLProcess = "Global\\ETLProcessRunning";
        public const string Process_Running_Mutex_Name_ETLService = "Global\\ETLServiceRunning";
        public const string Process_Running_Mutex_Name_Accounting = "Global\\AccountingProcessRunning";
        public const string Process_Running_Mutex_Name_System = "Global\\SystemProcessRunning";
        public const string Process_Running_Mutex_Name_WareHouse = "Global\\WareHouseProcessRunning";
        public const string Process_Running_Mutex_Name_Sale = "Global\\SaleProcessRunning";
        public const string Process_Running_Mutex_Name_Purchase = "Global\\PurchaseProcessRunning";
        public const string Process_Running_Mutex_Name_ETLMaster = "Global\\ETLMasterProcessRunning";

        public const string Process_Stop_Mutex_Name_ETLProcess = "Global\\ETLProcessStop";
        public const string Process_Stop_Mutex_Name_Accounting = "Global\\AccountingProcessStop";
        public const string Process_Stop_Mutex_Name_System = "Global\\SystemProcessStop";
        public const string Process_Stop_Mutex_Name_WareHouse = "Global\\WareHouseProcessStop";
        public const string Process_Stop_Mutex_Name_Sale = "Global\\SaleProcessStop";
        public const string Process_Stop_Mutex_Name_Purchase = "Global\\PurchaseProcessStop";
        public const string Process_Stop_Mutex_Name_ETLMaster = "Global\\ETLMasterProcessStop";

        public const int BusinessObjectType_Transaction = 0;
        public const int BusinessObjectType_InputInventoryCommandItemTransaction = 1;
        public const int BusinessObjectType_OutputInventoryCommandItemTransaction = 2;
        public const int BusinessObjectType_InputInventoryCommandFinancialTransaction = 3;
        public const int BusinessObjectType_OutputInventoryCommandFinancialTransaction = 4;
        public const int BusinessObjectType_PurcharseFinancialTransaction = 5;
        public const int BusinessObjectType_SalesFinancialTransaction = 6;
        public const int BusinessObjectType_PaymentVoucherTransaction = 7;
        public const int BusinessObjectType_ReceiptVoucherTransaction = 8;
        public const int BusinessObjectType_COGS = 9;
        public const int BusinessObjectType_FinancialTransaction = 10;
        public const int BusinessObjectType_InventoryTransaction = 11;
        public const int BusinessObjectType_SalesInvoice = 12;
        public const int BusinessObjectType_PurchaseInvoice = 13;
        public const int BusinessObjectType_ReceiptVoucher = 14;
        public const int BusinessObjectType_PaymentVoucher = 15;

        #endregion
    }
}
