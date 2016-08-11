using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.DB;
using NAS.DAL.System.Resource;
using NAS.DAL.System.Privileage;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Invoice;
using NAS.DAL.Sales.PickingStockCart;
using NAS.DAL.Buy.StockCart;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Vouches;
using NAS.DAL.Sales.Price;
using NAS.DAL;
using System.Collections;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Inventory;
using DevExpress.Data.Filtering;
using NAS.DAL.System.ArtifactCode;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Staging.Accounting.Journal;

namespace WebModule
{
    public partial class PopulateDB : System.Web.UI.Page
    {

        public class CloneIXPSimpleObjectHelper
        {
            /// <summary>
            /// A dictionary containing objects from the source session as key and objects from the 
            /// target session as values
            /// </summary>
            /// <returns></returns>
            Dictionary<object, object> clonedObjects;
            Session sourceSession;
            Session targetSession;

            /// <summary>
            /// Initializes a new instance of the CloneIXPSimpleObjectHelper class.
            /// </summary>
            public CloneIXPSimpleObjectHelper(Session source, Session target)
            {
                this.clonedObjects = new Dictionary<object, object>();
                this.sourceSession = source;
                this.targetSession = target;
            }

            public T Clone<T>(T source) where T : IXPSimpleObject
            {
                return Clone<T>(source, targetSession, false);
            }
            public T Clone<T>(T source, bool synchronize) where T : IXPSimpleObject
            {
                return (T)Clone(source as IXPSimpleObject, targetSession, synchronize);
            }

            public object Clone(IXPSimpleObject source)
            {
                return Clone(source, targetSession, false);
            }
            public object Clone(IXPSimpleObject source, bool synchronize)
            {
                return Clone(source, targetSession, synchronize);
            }

            public T Clone<T>(T source, Session targetSession, bool synchronize) where T : IXPSimpleObject
            {
                return (T)Clone(source as IXPSimpleObject, targetSession, synchronize);
            }

            /// <summary>
            /// Clones and / or synchronizes the given IXPSimpleObject.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="targetSession"></param>
            /// <param name="synchronize">If set to true, reference properties are only cloned in case
            /// the reference object does not exist in the targetsession. Otherwise the exising object will be
            /// reused and synchronized with the source. Set this property to false when knowing at forehand 
            /// that the targetSession will not contain any of the objects of the source.</param>
            /// <returns></returns>
            public object Clone(IXPSimpleObject source, Session targetSession, bool synchronize)
            {
                if (source == null)
                    return null;
                if (clonedObjects.ContainsKey(source))
                    return clonedObjects[source];
                XPClassInfo targetClassInfo = targetSession.GetClassInfo(source.GetType());
                object clone = null;
                if (synchronize)
                    clone = targetSession.GetObjectByKey(targetClassInfo, source.Session.GetKeyValue(source));
                if (clone == null)
                    clone = targetClassInfo.CreateNewObject(targetSession);
                clonedObjects.Add(source, clone);

                foreach (XPMemberInfo m in targetClassInfo.PersistentProperties)
                {
                    if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey)
                        continue;
                    object val;
                    if (m.ReferenceType != null)
                    {
                        object createdByClone = m.GetValue(clone);
                        if ((createdByClone != null) && synchronize == false)
                            val = createdByClone;
                        else
                        {
                            val = Clone((IXPSimpleObject)m.GetValue(source), targetSession, synchronize);
                        }

                    }
                    else
                    {
                        val = m.GetValue(source);
                    }
                    m.SetValue(clone, val);
                }
                foreach (XPMemberInfo m in targetClassInfo.CollectionProperties)
                {
                    if (m.HasAttribute(typeof(AggregatedAttribute)))
                    {
                        XPBaseCollection col = (XPBaseCollection)m.GetValue(clone);
                        XPBaseCollection colSource = (XPBaseCollection)m.GetValue(source);
                        foreach (IXPSimpleObject obj in new ArrayList(colSource))
                            col.BaseAdd(Clone(obj, targetSession, synchronize));
                    }
                }
                return clone;
            }

            public object Clone(IXPSimpleObject source, Session targetSession, bool synchronize, bool isCopyKey, bool isGetAggregated)
            {
                if (source == null)
                    return null;
                if (clonedObjects.ContainsKey(source))
                    return clonedObjects[source];
                XPClassInfo targetClassInfo = targetSession.GetClassInfo(source.GetType());
                object clone = null;
                if (synchronize)
                    clone = targetSession.GetObjectByKey(targetClassInfo, source.Session.GetKeyValue(source));
                if (clone == null)
                    clone = targetClassInfo.CreateNewObject(targetSession);
                clonedObjects.Add(source, clone);

                foreach (XPMemberInfo m in targetClassInfo.PersistentProperties)
                {
                    if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || (m.IsKey && !isCopyKey))
                        continue;
                    object val;
                    if (m.ReferenceType != null)
                    {
                        object createdByClone = m.GetValue(clone);
                        if ((createdByClone != null) && synchronize == false)
                            val = createdByClone;
                        else
                        {
                            val = Clone((IXPSimpleObject)m.GetValue(source), targetSession, synchronize);
                        }

                    }
                    else
                    {
                        val = m.GetValue(source);
                    }
                    m.SetValue(clone, val);
                }
                if (isGetAggregated)
                {
                    foreach (XPMemberInfo m in targetClassInfo.CollectionProperties)
                    {
                        if (m.HasAttribute(typeof(AggregatedAttribute)))
                        {
                            XPBaseCollection col = (XPBaseCollection)m.GetValue(clone);
                            XPBaseCollection colSource = (XPBaseCollection)m.GetValue(source);
                            foreach (IXPSimpleObject obj in new ArrayList(colSource))
                                col.BaseAdd(Clone(obj, targetSession, synchronize));
                        }
                    }
                }
                return clone;
            }

        }

        private readonly static object lockObject = new object();
        static volatile IDataLayer fSourceDataLayer;
        static volatile IDataLayer fTargetDataLayer;
        private static Session GetSourceSession(string connectionString, AutoCreateOption option)
        {
            if (fSourceDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fSourceDataLayer == null)
                    {
                        fSourceDataLayer = GetDataLayer(connectionString, option);
                    }
                }
            }
            return new Session(fSourceDataLayer);
        }
        private static Session GetTargetSession(string connectionString, AutoCreateOption option)
        {
            if (fTargetDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fTargetDataLayer == null)
                    {
                        fTargetDataLayer = GetDataLayer(connectionString, option);
                    }
                }
            }
            return new Session(fTargetDataLayer);
        }
        private static UnitOfWork GetSourceUnitOfWork(string connectionString, AutoCreateOption option)
        {
            if (fSourceDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fSourceDataLayer == null)
                    {
                        fSourceDataLayer = GetDataLayer(connectionString, option);
                    }
                }
            }
            return new UnitOfWork(fSourceDataLayer);
        }
        private static UnitOfWork GetTargetUnitOfWork(string connectionString, AutoCreateOption option)
        {
            if (fTargetDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fTargetDataLayer == null)
                    {
                        fTargetDataLayer = GetDataLayer(connectionString, option);
                    }
                }
            }
            return new UnitOfWork(fTargetDataLayer);
        }

        private static IDataLayer GetDataLayer(string connectionString, AutoCreateOption option)
        {
            //Utility.LogWriter.Instance.WriteToLog("---START: DAL.Purchasing.GetDataLayer()---");
            try
            {
                XpoDefault.Session = null;
                string conn = connectionString;
                XPDictionary dict = new ReflectionDictionary();
                //IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
                IDataStore store = XpoDefault.GetConnectionProvider(conn, option);

                dict.GetDataStoreSchema(

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
                    typeof(NAS.DAL.Nomenclature.Item.Unit).Assembly,

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

        private Session sourceSession;
        private Session targetSession;
        protected void Page_Init(object sender, EventArgs e)
        {
            string sourceConnStr = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.120", "ERPCORE");
            string targetConnStr = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("192.168.1.120", "ERPCORE_Cloned");
            sourceSession = GetSourceSession(sourceConnStr, AutoCreateOption.DatabaseAndSchema);
            targetSession = GetTargetSession(targetConnStr, AutoCreateOption.DatabaseAndSchema); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clone(sourceSession, targetSession);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            sourceSession.Dispose();
            targetSession.Dispose();
        }
            
        private void CloneOrganizations(Session source, Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            CriteriaOperator criteriaGetRoot = new UnaryOperator(UnaryOperatorType.IsNull, "ParentOrganizationId");
            XPCollection<Organization> rootCollection = new XPCollection<Organization>(source, criteriaGetRoot);
            foreach (var root in rootCollection)
            {
                try
                {
                    Organization clone = (Organization)cloneHelper.Clone(root, target, true, true, false);
                    clone.Save();
                    //Clone descendant of the root element
                    CloneDescendantOrganizations(cloneHelper, target, root);
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDescendantOrganizations(CloneIXPSimpleObjectHelper cloneHelper, Session target, Organization organization)
        {
            if (organization.Organizations == null || organization.Organizations.Count == 0) return;
            foreach (var item in organization.Organizations)
            {
                Organization clone = (Organization)cloneHelper.Clone(item, target, true, true, false);
                clone.Save();
                CloneDescendantOrganizations(cloneHelper, target, item);
            }
        }
        private void CloneAccountCategories(Session source, Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<AccountCategory> accountCategories = new XPCollection<AccountCategory>(source);
            foreach (var item in accountCategories)
            {
                try
                {
                    AccountCategory clone = (AccountCategory)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>
        /// Clone data from source to target database
        /// </summary>
        public void Clone(Session source, Session target)
        {
            try
            {
                #region Default Data
                //Clone AccountCategory
                CloneAccountCategories(source, target);
                //Clone AccountType
                CloneAccountTypes(source, target);               

                //v.1.0.18+
                ////NAS.DAL.Accounting.Configure
                //AllocationType.Populate();

                //Clone ObjectType
                CloneObjectTypes(source, target);
                //Clone CustomFieldType
                CloneCustomFieldTypes(source, target);

                //Clone CustomFieldType
                CloneRecordedTypes(source, target);

                CloneCommanderStockCartStatuses(source, target);
                CloneCommanderStockCartTypes(source, target);

                CloneStockCartActorTypes(source, target);

                CloneTaxTypes(source, target);
                ClonePromotionTypes(source, target);

                CloneItemUnitRelationTypes(source, target);
                CloneItemTradingTypes(source, target);

                CloneTradingCategories(source, target);
                CloneDepartmentTypes(source, target);
                CloneOrganizationType(source, target);

                //recursive
                CloneOrganizations(source, target);

                CloneAccountActorTypes(source, target);

                CloneVouchesTypes(source, target);
                CloneVouchesActorTypes(source, target);
                CloneReceiptVouchesTypes(source, target);
                ClonePaymentVouchesTypes(source, target);
                CloneForeignCurrency(source, target);

                ////NAS.DAL.System.ArtifactCode
                CloneArtifactTypes(source, target);
                CloneCodeRuleDataTypes(source, target);
                CloneCodeRuleDataFormats(source, target);
                CloneRuleRepeaterTypes(source, target);

                //NAS.DAL.Sales.Price
                ClonePricePolicyType(source, target);
                #endregion

                #region Basic Data
                //Clone Department
                //recursive
                CloneDepartments(source, target);
                //Clone Person
                ClonePersons(source, target);
                //Clone LoginAccount
                CloneLoginAccount(source, target);
                //Clone DepartmentPerson
                CloneDepartmentPerson(source, target);
                //Clone OrganizationCategory
                CloneOrganizationCategory(source, target);
                //Clone Accounting Period
                CloneAccountingPeriods(source, target);
                //Clone Inventory
                //recursive
                CloneInventories(source, target);
                //Clone InventoryUnit
                CloneInventoryUnit(source, target);
                //Clone Account
                CloneAccounts(source, target);
                //Clone Item
                CloneItems(source, target);
                //Clone Unit
                CloneUnits(source, target);
                //Clone ItemUnit
                //recursive
                CloneItemUnit(source, target);

                //NAS.DAL.CMS.ObjectDocument
                CloneObjects(source, target);
                CloneCustomFields(source, target);
                CloneObjectTypeCustomField(source, target);
                CloneCustomFieldData(source, target);
                CloneObjectCustomField(source, target);
                CloneObjectCustomFieldData(source, target);

                CloneItemCustomType(source, target);
                CloneItemObject(source, target);

                CloneArtifactCodeRules(source, target);
                //recursive
                CloneCodeRuleDefinitions(source, target);
                CloneCodeRuleData(source, target);
                #endregion

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        private void CloneCodeRuleData(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CodeRuleData> collection = new XPCollection<CodeRuleData>(source);
            foreach (var item in collection)
            {
                try
                {
                    CodeRuleData clone = (CodeRuleData)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCodeRuleDefinitions(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            CriteriaOperator criteriaGetRoot = new UnaryOperator(UnaryOperatorType.IsNull, "ParentCodeRuleDefinitionId");
            XPCollection<CodeRuleDefinition> rootCollection = new XPCollection<CodeRuleDefinition>(source, criteriaGetRoot);
            foreach (var root in rootCollection)
            {
                try
                {
                    CodeRuleDefinition clone = (CodeRuleDefinition)cloneHelper.Clone(root, target, true, true, false);
                    clone.Save();
                    //Clone descendant of the root element
                    CloneDescendantCodeRuleDefinitions(cloneHelper, target, root);
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDescendantCodeRuleDefinitions(CloneIXPSimpleObjectHelper cloneHelper, DevExpress.Xpo.Session target, CodeRuleDefinition codeRuleDefinition)
        {
            if (codeRuleDefinition.CodeRuleDefinitions == null || 
                codeRuleDefinition.CodeRuleDefinitions.Count == 0) return;
            foreach (var item in codeRuleDefinition.CodeRuleDefinitions)
            {
                CodeRuleDefinition clone = (CodeRuleDefinition)cloneHelper.Clone(item, target, true, true, false);
                clone.Save();
                CloneDescendantCodeRuleDefinitions(cloneHelper, target, item);
            }
        }

        private void CloneArtifactCodeRules(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ArtifactCodeRule> collection = new XPCollection<ArtifactCodeRule>(source);
            foreach (var item in collection)
            {
                try
                {
                    ArtifactCodeRule clone = (ArtifactCodeRule)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItemObject(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ItemObject> collection = new XPCollection<ItemObject>(source);
            foreach (var item in collection)
            {
                try
                {
                    ItemObject clone = (ItemObject)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItemCustomType(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ItemCustomType> collection = new XPCollection<ItemCustomType>(source);
            foreach (var item in collection)
            {
                try
                {
                    ItemCustomType clone = (ItemCustomType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneObjectCustomFieldData(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ObjectCustomFieldData> collection = new XPCollection<ObjectCustomFieldData>(source);
            foreach (var item in collection)
            {
                try
                {
                    ObjectCustomFieldData clone = (ObjectCustomFieldData)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneObjectCustomField(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ObjectCustomField> collection = new XPCollection<ObjectCustomField>(source);
            foreach (var item in collection)
            {
                try
                {
                    ObjectCustomField clone = (ObjectCustomField)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCustomFieldData(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CustomFieldData> collection = new XPCollection<CustomFieldData>(source);
            foreach (var item in collection)
            {
                try
                {
                    CustomFieldData clone = (CustomFieldData)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneObjectTypeCustomField(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ObjectTypeCustomField> collection = new XPCollection<ObjectTypeCustomField>(source);
            foreach (var item in collection)
            {
                try
                {
                    ObjectTypeCustomField clone = (ObjectTypeCustomField)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCustomFields(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CustomField> collection = new XPCollection<CustomField>(source);
            foreach (var item in collection)
            {
                try
                {
                    CustomField clone = (CustomField)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneObjects(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<NAS.DAL.CMS.ObjectDocument.Object> collection = new XPCollection<NAS.DAL.CMS.ObjectDocument.Object>(source);
            foreach (var item in collection)
            {
                try
                {
                    NAS.DAL.CMS.ObjectDocument.Object clone = 
                        (NAS.DAL.CMS.ObjectDocument.Object)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItemUnit(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            CriteriaOperator criteriaGetRoot = new UnaryOperator(UnaryOperatorType.IsNull, "ParentItemUnitId");
            XPCollection<ItemUnit> rootCollection = new XPCollection<ItemUnit>(source, criteriaGetRoot);
            foreach (var root in rootCollection)
            {
                try
                {
                    ItemUnit clone = (ItemUnit)cloneHelper.Clone(root, target, true, true, false);
                    clone.Save();
                    //Clone descendant of the root element
                    CloneDescendantItemUnit(cloneHelper, target, root);
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDescendantItemUnit(CloneIXPSimpleObjectHelper cloneHelper, DevExpress.Xpo.Session target, ItemUnit itemUnit)
        {
            if (itemUnit.ItemUnits == null ||
                itemUnit.ItemUnits.Count == 0) return;
            foreach (var item in itemUnit.ItemUnits)
            {
                ItemUnit clone = (ItemUnit)cloneHelper.Clone(item, target, true, true, false);
                clone.Save();
                CloneDescendantItemUnit(cloneHelper, target, item);
            }
        }

        private void CloneUnits(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<NAS.DAL.Nomenclature.Item.Unit> collection = new XPCollection<NAS.DAL.Nomenclature.Item.Unit>(source);
            foreach (var item in collection)
            {
                try
                {
                    NAS.DAL.Nomenclature.Item.Unit clone = 
                        (NAS.DAL.Nomenclature.Item.Unit)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItems(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<NAS.DAL.Nomenclature.Item.Item> collection = 
                new XPCollection<NAS.DAL.Nomenclature.Item.Item>(source);
            foreach (var item in collection)
            {
                try
                {
                    NAS.DAL.Nomenclature.Item.Item clone = 
                        (NAS.DAL.Nomenclature.Item.Item)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneAccounts(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<Account> collection = new XPCollection<Account>(source);
            foreach (var item in collection)
            {
                try
                {
                    Account clone = (Account)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneInventoryUnit(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<InventoryUnit> collection = new XPCollection<InventoryUnit>(source);
            foreach (var item in collection)
            {
                try
                {
                    InventoryUnit clone = (InventoryUnit)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneInventories(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            CriteriaOperator criteriaGetRoot = new UnaryOperator(UnaryOperatorType.IsNull, "ParentInventoryId");
            XPCollection<Inventory> rootCollection = new XPCollection<Inventory>(source, criteriaGetRoot);
            foreach (var root in rootCollection)
            {
                try
                {
                    Inventory clone = (Inventory)cloneHelper.Clone(root, target, true, true, false);
                    clone.Save();
                    //Clone descendant of the root element
                    CloneDescendantInventories(cloneHelper, target, root);
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDescendantInventories(CloneIXPSimpleObjectHelper cloneHelper, DevExpress.Xpo.Session target, Inventory inventory)
        {
            if (inventory.Inventorys == null ||
                inventory.Inventorys.Count == 0) return;
            foreach (var item in inventory.Inventorys)
            {
                Inventory clone = (Inventory)cloneHelper.Clone(item, target, true, true, false);
                clone.Save();
                CloneDescendantInventories(cloneHelper, target, item);
            }
        }

        private void CloneAccountingPeriods(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<AccountingPeriod> collection = new XPCollection<AccountingPeriod>(source);
            foreach (var item in collection)
            {
                try
                {
                    AccountingPeriod clone = (AccountingPeriod)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneOrganizationCategory(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<OrganizationCategory> collection = new XPCollection<OrganizationCategory>(source);
            foreach (var item in collection)
            {
                try
                {
                    OrganizationCategory clone = (OrganizationCategory)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDepartmentPerson(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<DepartmentPerson> collection = new XPCollection<DepartmentPerson>(source);
            foreach (var item in collection)
            {
                try
                {
                    TaxType clone = (TaxType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneLoginAccount(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<LoginAccount> collection = new XPCollection<LoginAccount>(source);
            foreach (var item in collection)
            {
                try
                {
                    LoginAccount clone = (LoginAccount)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void ClonePersons(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<Person> collection = new XPCollection<Person>(source);
            foreach (var item in collection)
            {
                try
                {
                    Person clone = (Person)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDepartments(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            CriteriaOperator criteriaGetRoot = new UnaryOperator(UnaryOperatorType.IsNull, "ParentDepartmentId");
            XPCollection<Department> rootCollection = new XPCollection<Department>(source, criteriaGetRoot);
            foreach (var root in rootCollection)
            {
                try
                {
                    Department clone = (Department)cloneHelper.Clone(root, target, true, true, false);
                    clone.Save();
                    //Clone descendant of the root element
                    CloneDescendantDepartments(cloneHelper, target, root);
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDescendantDepartments(CloneIXPSimpleObjectHelper cloneHelper, DevExpress.Xpo.Session target, Department department)
        {
            if (department.Departments == null ||
                department.Departments.Count == 0) return;
            foreach (var item in department.Departments)
            {
                Department clone = (Department)cloneHelper.Clone(item, target, true, true, false);
                clone.Save();
                CloneDescendantDepartments(cloneHelper, target, item);
            }
        }

        private void ClonePricePolicyType(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<PricePolicyType> collection = new XPCollection<PricePolicyType>(source);
            foreach (var item in collection)
            {
                try
                {
                    PricePolicyType clone = (PricePolicyType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneRuleRepeaterTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<RuleRepeaterType> collection = new XPCollection<RuleRepeaterType>(source);
            foreach (var item in collection)
            {
                try
                {
                    RuleRepeaterType clone = (RuleRepeaterType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCodeRuleDataFormats(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CodeRuleDataFormat> collection = new XPCollection<CodeRuleDataFormat>(source);
            foreach (var item in collection)
            {
                try
                {
                    CodeRuleDataFormat clone = (CodeRuleDataFormat)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCodeRuleDataTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CodeRuleDataType> collection = new XPCollection<CodeRuleDataType>(source);
            foreach (var item in collection)
            {
                try
                {
                    CodeRuleDataType clone = (CodeRuleDataType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneArtifactTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ArtifactType> collection = new XPCollection<ArtifactType>(source);
            foreach (var item in collection)
            {
                try
                {
                    ArtifactType clone = (ArtifactType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneForeignCurrency(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ForeignCurrency> collection = new XPCollection<ForeignCurrency>(source);
            foreach (var item in collection)
            {
                try
                {
                    ForeignCurrency clone = (ForeignCurrency)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void ClonePaymentVouchesTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<PaymentVouchesType> collection = new XPCollection<PaymentVouchesType>(source);
            foreach (var item in collection)
            {
                try
                {
                    PaymentVouchesType clone = (PaymentVouchesType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneReceiptVouchesTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ReceiptVouchesType> collection = new XPCollection<ReceiptVouchesType>(source);
            foreach (var item in collection)
            {
                try
                {
                    ReceiptVouchesType clone = (ReceiptVouchesType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneVouchesActorTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<VouchesActorType> collection = new XPCollection<VouchesActorType>(source);
            foreach (var item in collection)
            {
                try
                {
                    VouchesActorType clone = (VouchesActorType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneVouchesTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<VouchesType> collection = new XPCollection<VouchesType>(source);
            foreach (var item in collection)
            {
                try
                {
                    VouchesType clone = (VouchesType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneAccountActorTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<AccountActorType> collection = new XPCollection<AccountActorType>(source);
            foreach (var item in collection)
            {
                try
                {
                    AccountActorType clone = (AccountActorType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneOrganizationType(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<OrganizationType> collection = new XPCollection<OrganizationType>(source);
            foreach (var item in collection)
            {
                try
                {
                    OrganizationType clone = (OrganizationType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneDepartmentTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<DepartmentType> collection = new XPCollection<DepartmentType>(source);
            foreach (var item in collection)
            {
                try
                {
                    DepartmentType clone = (DepartmentType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneTradingCategories(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<TradingCategory> collection = new XPCollection<TradingCategory>(source);
            foreach (var item in collection)
            {
                try
                {
                    TradingCategory clone = (TradingCategory)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItemTradingTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ItemTradingType> collection = new XPCollection<ItemTradingType>(source);
            foreach (var item in collection)
            {
                try
                {
                    ItemTradingType clone = (ItemTradingType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneItemUnitRelationTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ItemUnitRelationType> collection = new XPCollection<ItemUnitRelationType>(source);
            foreach (var item in collection)
            {
                try
                {
                    ItemUnitRelationType clone = (ItemUnitRelationType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void ClonePromotionTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<PromotionType> collection = new XPCollection<PromotionType>(source);
            foreach (var item in collection)
            {
                try
                {
                    PromotionType clone = (PromotionType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }
        
        private void CloneTaxTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<TaxType> collection = new XPCollection<TaxType>(source);
            foreach (var item in collection)
            {
                try
                {
                    TaxType clone = (TaxType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneStockCartActorTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<StockCartActorType> collection = new XPCollection<StockCartActorType>(source);
            foreach (var item in collection)
            {
                try
                {
                    StockCartActorType clone = (StockCartActorType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCommanderStockCartTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CommanderStockCartType> collection = new XPCollection<CommanderStockCartType>(source);
            foreach (var item in collection)
            {
                try
                {
                    CommanderStockCartType clone = (CommanderStockCartType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCommanderStockCartStatuses(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CommanderStockCartStatus> collection = new XPCollection<CommanderStockCartStatus>(source);
            foreach (var item in collection)
            {
                try
                {
                    CommanderStockCartStatus clone = (CommanderStockCartStatus)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneRecordedTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<RecordedType> recordedTypes = new XPCollection<RecordedType>(source);
            foreach (var item in recordedTypes)
            {
                try
                {
                    RecordedType clone = (RecordedType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneCustomFieldTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<CustomFieldType> customFieldTypes = new XPCollection<CustomFieldType>(source);
            foreach (var item in customFieldTypes)
            {
                try
                {
                    CustomFieldType clone = (CustomFieldType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneObjectTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<ObjectType> objectTypes = new XPCollection<ObjectType>(source);
            foreach (var item in objectTypes)
            {
                try
                {
                    ObjectType clone = (ObjectType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

        private void CloneAccountTypes(DevExpress.Xpo.Session source, DevExpress.Xpo.Session target)
        {
            CloneIXPSimpleObjectHelper cloneHelper = new CloneIXPSimpleObjectHelper(source, target);
            XPCollection<AccountType> accountTypes = new XPCollection<AccountType>(source);
            foreach (var item in accountTypes)
            {
                try
                {
                    AccountType clone = (AccountType)cloneHelper.Clone(item, target, true, true, false);
                    clone.Save();
                }
                catch (Exception)
                {

                }
            }
        }

    }
}