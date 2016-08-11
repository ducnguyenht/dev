using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.DB;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Sales.PickingStockCart;
using NAS.DAL.Buy.StockCart;
using NAS.DAL.System.Resource;
using NAS.DAL.System.Privileage;
using NAS.DAL.Vouches;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Sales.Price;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.Accounting.Period;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;
using NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.BI.Inventory;

namespace NAS.DAL
{
    public static class XpoHelper
    {
        public static Session GetNewSession()
        {
            return new Session(DataLayer);
        }

        public static ExplicitUnitOfWork GetNewExplicitUnitOfWork()
        {
            return new ExplicitUnitOfWork(DataLayer);
        }

        public static UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }
        
        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;

        static IDataLayer DataLayer
        {
            get
            {
                if (fDataLayer == null)
                {
                    lock (lockObject)
                    {
                        if (fDataLayer == null)
                        {
                            fDataLayer = GetDataLayer();
                        }
                    }
                }
                return fDataLayer;
            }
        }

        public static void ClearDataLayer()
        {
            fDataLayer = null;
        }

        private static IDataLayer GetDataLayer()
        {
            //Utility.LogWriter.Instance.WriteToLog("---START: DAL.Purchasing.GetDataLayer()---");
            try
            { 
                XpoDefault.Session = null;
                //string conn = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.150", "ERPCORE_LocalSite");
                //string conn = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.150", "ERPCORETai");
                //string conn = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.150", "ERPCORE_TESTGiaodien");
                //string conn = ConnectionHelper.ConnectionString;
                //string conn = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.22", "ngoclinhadmin", "Na12345678", "ngoclinhdb");
                //Utility.LogWriter.Instance.WriteToLog("Get active database congfiguration...");
                DbConfig dbConfig = new DbConfig();
                string conn = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.133", "sa", "123456", "ERPCore");//"XpoProvider=MSSqlServer;data source=192.168.1.133;integrated security=SSPI;initial catalog=ERPCore"; //dbConfig.getActiveDbConfig().ConnectionString;//"XpoProvider=MSSqlServer;data source=192.168.1.133;integrated security=SSPI;initial catalog=ERPCORE";//
                //Utility.LogWriter.Instance.WriteToLog("Connection string: " + conn);
                  
                XPDictionary dict = new ReflectionDictionary();
                //IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
                IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);

                dict.GetDataStoreSchema(
                    typeof(InventoryCommandDim).Assembly,
                    typeof(CorrespondFinancialAccountDim).Assembly,
                    typeof(FinancialSupplierLiabilityDetail).Assembly,
                    typeof(FinancialCustomerLiabilityDetail).Assembly,
                    typeof(FinancialOnTheWayBuyingGoodDetail).Assembly,
                    typeof(DiaryJournal_Detail).Assembly,
                    typeof(FinancialPrepaidExpenseSummary_Fact).Assembly,
                    typeof(FinancialPrepaidExpenseDetail).Assembly,

                    //NAS.DAL.Accounting.Currency
                    typeof(Currency).Assembly,
                    typeof(CurrencyType).Assembly,

                    //NAS.DAL.AccountingPeriod
                    typeof(AccountingPeriodType).Assembly,

                    ////NAS.DAL.System.Resource
                    typeof(App).Assembly,
                    typeof(AppComponent).Assembly,
                    typeof(AppComponentContent).Assembly,
                    typeof(AppComponentContentType).Assembly,
                    typeof(AppComponentData).Assembly,
                    typeof(AppComponentOperation).Assembly,
                    typeof(AppOperation).Assembly,

                    ////NAS.DAL.System.Privilege
                    typeof(PrivilegeDepartment).Assembly,
                    typeof(SpecialPrivilege).Assembly,

                    ////NAS.DAL.CMS.ObjectDocument
                    typeof(CustomField).Assembly,
                    typeof(CustomFieldData).Assembly,
                    typeof(CustomFieldDataDateTime).Assembly,
                    typeof(CustomFieldDataFloat).Assembly,
                    typeof(CustomFieldDataImage).Assembly,
                    typeof(CustomFieldDataInt).Assembly,
                    typeof(CustomFieldDataPeriod).Assembly,
                    typeof(CustomFieldDataRichText).Assembly,
                    typeof(CustomFieldDataString).Assembly,
                    typeof(CustomFieldType).Assembly,
                    typeof(NAS.DAL.CMS.ObjectDocument.Object).Assembly,
                    typeof(ObjectCustomField).Assembly,
                    typeof(ObjectCustomFieldData).Assembly,
                    typeof(ObjectType).Assembly,
                    typeof(ObjectTypeCustomField).Assembly,

                    ////NAS.DAL.Inventory.Item
                    typeof(InventoryItem).Assembly,
                    typeof(RecordedType).Assembly,

                    ////NAS.DAL.Inventory.Operation
                    typeof(CommanderStockCart).Assembly,
                    typeof(CommanderStockCartItem).Assembly,
                    typeof(CommanderStockCartStatus).Assembly,
                    typeof(CommanderStockCartType).Assembly,
                    typeof(MovingStockCart).Assembly,
                    typeof(PickingStockCart).Assembly,
                    typeof(PuttingStockCart).Assembly,

                    ////NAS.DAL.Inventory.StockCart
                    typeof(StockCartActor).Assembly,
                    typeof(StockCartActorType).Assembly,
                    typeof(StockCart).Assembly,
                    typeof(StockCartItem).Assembly,
                    ////NAS.DAL.Inventory.Ledger
                    typeof(InventoryLedger).Assembly,
                    typeof(InventoryJournalBalanceForward).Assembly,
                    typeof(InventoryTransactionBalanceForward).Assembly,
                    ////NAS.DAL.Invoice
                    typeof(BillActor).Assembly,
                    typeof(Bill).Assembly,
                    typeof(BillItem).Assembly,
                    typeof(BillPromotion).Assembly,
                    typeof(BillTax).Assembly,
                    typeof(BillType).Assembly,
                    typeof(PromotionType).Assembly,
                    typeof(SaleInvoiceArtiface).Assembly,
                    typeof(PurchaseInvoice).Assembly,
                    typeof(SalesInvoice).Assembly,
                    typeof(TaxType).Assembly,

                    ////NAS.DAL.Sales.PickingStockCart
                    typeof(SalesInvoicePickingStockCart).Assembly,

                    ////NAS.DAL.Buy.StockCart
                    typeof(PurchaseInvoicePuttingStockCart).Assembly,

                    ////NAS.DAL.Nomenclature.Inventory
                    typeof(NAS.DAL.Nomenclature.Inventory.Inventory).Assembly,
                    typeof(NAS.DAL.Nomenclature.Inventory.InventoryUnit).Assembly,

                    ////NAS.DAL.Nomenclature.Item
                    typeof(Item).Assembly,
                    typeof(ItemCustomType).Assembly,
                    typeof(ItemObject).Assembly,
                    typeof(ItemSupplier).Assembly,
                    typeof(ItemTradingType).Assembly,
                    typeof(ItemUnit).Assembly,
                    typeof(ItemUnitRelationType).Assembly,
                    typeof(Unit).Assembly,

                    ////NAS.DAL.Nomenclature.Organization
                    typeof(AuthenticationProvider).Assembly,
                    typeof(Department).Assembly,
                    typeof(DepartmentPerson).Assembly,
                    typeof(DepartmentType).Assembly,
                    typeof(LoginAccount).Assembly,
                    typeof(ManufacturerOrg).Assembly,
                    typeof(Organization).Assembly,
                    typeof(OrganizationType).Assembly,
                    typeof(OwnerOrg).Assembly,
                    typeof(Person).Assembly,
                    typeof(ServiceOrg).Assembly,
                    typeof(SupplierOrg).Assembly,
                    typeof(TradingOrg).Assembly,


                    ////NAS.DAL.Nomenclature.Bank
                    typeof(NAS.DAL.Nomenclature.Bank.Bank).Assembly,
                    typeof(NAS.DAL.Nomenclature.Bank.BankBranch).Assembly,

                    ////NAS.DAL.Vouches
                    typeof(NAS.DAL.Vouches.Vouches).Assembly,
                    typeof(VouchesType).Assembly,
                    typeof(VouchesActor).Assembly,
                    typeof(VouchesActorType).Assembly,
                    typeof(VouchesAmount).Assembly,
                    typeof(PaymentVouches).Assembly,
                    typeof(PaymentVouchesType).Assembly,
                    typeof(ReceiptVouches).Assembly,
                    typeof(ReceiptVouchesType).Assembly,
                    typeof(ForeignCurrency).Assembly,
                    typeof(ExchangeRate).Assembly,

                    //Price
                    typeof(PricePolicyType).Assembly,
                    typeof(PricePolicy).Assembly,
                    typeof(PricePolicyCondition).Assembly,
                    typeof(PricePolicyItemUnitCondition).Assembly,
                    typeof(PricePolicyManufacturerCondition).Assembly,
                    typeof(PricePolicySupplierCondition).Assembly
                );

                IDataLayer dl = new ThreadSafeDataLayer(dict, store);
                return dl;
            }
            catch (Exception)
            {
                //Utility.LogWriter.Instance.WriteToLog("Exception: " + ex.Message);
                throw;
            }
            finally
            {
                //Utility.LogWriter.Instance.WriteToLog("---END: DAL.Purchasing.GetDataLayer()---");
            }

        }

    }
}
