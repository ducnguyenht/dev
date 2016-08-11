using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Inventory;
using System.Collections;
using NAS.DAL.Sales.PickingStockCart;
using NAS.DAL.Vouches;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Journal;
using Utility;
using NAS.DAL.System.ArtifactCode;
using NAS.DAL.Sales.Price;
using NAS.DAL.Accounting.Configure;
using NAS.DAL.Staging.Accounting.Journal;
using NAS.DAL.Nomenclature.UnitItem;
using NAS.DAL.ETL;
using NAS.DAL.System.Log;
using NAS.DAL.Accounting.Currency;

namespace NAS.DAL
{
    public class Util
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="rowStatuses">params of row status, default is all.</param>
        /// <returns></returns>
        public static bool isExistXpoObject<T>(string fieldName, object value, params short[] rowStatuses)
        {
            return isExistXpoObject<T>(fieldName, value, BinaryOperatorType.Equal, rowStatuses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="rowStatuses">params of row status, default is all.</param>
        /// <returns></returns>
        public static bool isExistXpoObject<T>(string fieldName, object value, BinaryOperatorType type, params short[] rowStatuses)
        {
            Session session = null;
            try
            {
                CriteriaOperator rowStatusCriteria = null;
                if(rowStatuses.Length > 0) {
                    rowStatusCriteria = new InOperator("RowStatus", rowStatuses.ToList());
                }
                CriteriaOperator fieldCriteria = new BinaryOperator(fieldName, value, type);
                CriteriaOperator criteria = CriteriaOperator.And(fieldCriteria, rowStatusCriteria);
                session = XpoHelper.GetNewSession();
                var result = session.GetObjects(session.GetClassInfo(typeof(T)), criteria, null, 0, false, true);

                if (result != null && result.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sesion"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XPCollection<T> getXPCollection<T>(Session sesion, string fieldName, object value, params short[] rowStatuses)
        {
            return getXPCollection<T>(sesion, fieldName, value, BinaryOperatorType.Equal, rowStatuses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sesion"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XPCollection<T> getXPCollection<T>(Session sesion, string fieldName, object value, BinaryOperatorType type, params short[] rowStatuses)
        {
            try
            {
                CriteriaOperator rowStatusCriteria = null;
                if (rowStatuses.Length > 0)
                {
                    rowStatusCriteria = new InOperator("RowStatus", rowStatuses.ToList());
                }
                CriteriaOperator fieldCriteria = new BinaryOperator(fieldName, value, type);
                CriteriaOperator criteria = CriteriaOperator.And(rowStatusCriteria, fieldCriteria);
                XPCollection<T> result = 
                    new XPCollection<T>(PersistentCriteriaEvaluationBehavior.InTransaction, sesion, criteria, false);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public static T getXpoObjectByName<T>(Session sesion, string value)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("Name", value);
                var collection = sesion.GetObjects(sesion.GetClassInfo<T>(), criteria, null, 0, false, true).GetEnumerator();
                while (collection.MoveNext())
                {
                    return (T)collection.Current;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public static T getDefaultXpoObject<T>(Session sesion)
        {
            try
            {
                CriteriaOperator criteria = new BinaryOperator("RowStatus", -1);
                var collection = sesion.GetObjects(sesion.GetClassInfo<T>(), criteria, null, 0, false, true).GetEnumerator();
                while (collection.MoveNext())
                {
                    return (T)collection.Current;
                }
                return default(T);
            }
            catch(Exception) {
                return default(T);
            }
            finally
            {

            }
        }

        public T GetXpoObjectByFieldName<T,V>(Session session, string FieldName, V Value,BinaryOperatorType CompareType)
        {
            T result = default(T);
            try
            {
                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_fieldName = new BinaryOperator(FieldName, Value, CompareType);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_fieldName,criteria_RowStatus);
                result = session.FindObject<T>(criteria);
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// Populate default data to database
        /// </summary>
        public static void Populate()
        {
            try
            {
                ////NAS.DAL.Accounting.AccountChart
                NAS.DAL.BI.Accounting.Account.CorrespondFinancialAccountDim.Populate();
                NAS.DAL.BI.Accounting.Account.FinancialAccountDim.Populate();
                Account.Populate();
                AccountType.Populate();
                AccountCategory.Populate();
                Currency.Populate();

                ////NAS.DAL.Accounting.Configure
                AllocationType.Populate();

                ////NAS.DAL.CMS.ObjectDocument
                ObjectType.Populate();
                CustomFieldType.Populate();
                ObjectTypeCustomField.Populate();

                ////NAS.DAL.Inventory.Item
                RecordedType.Populate();

                ////NAS.DAL.Inventory.Operation
                CommanderStockCartStatus.Populate();
                CommanderStockCartType.Populate();

                ////NAS.DAL.Inventory.StockCart
                StockCartActorType.Populate();

                ////NAS.DAL.Invoice
                TaxType.Populate();
                PromotionType.Populate();

                ////NAS.DAL.Nomenclature.Inventory
                Nomenclature.Inventory.Inventory.Populate();
                InventoryUnit.Populate();

                ////NAS.DAL.Nomenclature.Item
                ItemUnitRelationType.Populate();
                UnitType.Populate();
                Unit.Populate();
                ItemUnit.Populate();
                ItemTradingType.Populate();
                ItemCustomType.Populate();

                ////NAS.DAL.Nomenclature.Organization
                TradingCategory.Populate();
                AuthenticationProvider.Populate();
                DepartmentType.Populate();
                OrganizationType.Populate();
                Person.Populate();
                Organization.Populate();
                OwnerOrg.Populate();
                CustomerOrg.Populate();
                SupplierOrg.Populate();
                ManufacturerOrg.Populate();
                Department.Populate();
                DepartmentPerson.Populate();

                //NAS.DAL.Staging.Accounting.Journal
                AccountActorType.Populate();

                ////NAS.DAL.Vouches
                VouchesType.Populate();
                VouchesActorType.Populate();
                ReceiptVouchesType.Populate();
                PaymentVouchesType.Populate();
                ForeignCurrency.Populate();

                ////NAS.DAL.Accounting.Journal
                AccountingPeriod.Populate();
                //SalesInvoicePickingStockCart.Populate();

                ////NAS.DAL.System.ArtifactCode
                ArtifactType.Populate();
                CodeRuleDataType.Populate();
                CodeRuleDataFormat.Populate();
                RuleRepeaterType.Populate();

                //NAS.DAL.Sales.Price
                PricePolicyType.Populate();

                //NAS.DAL.Inventory.Lot.Lot
                NAS.DAL.Inventory.Lot.Lot.Populate();
                
                //NAS.DAL.Inventory.Command.InventoryCommandActorType
                NAS.DAL.Inventory.Command.InventoryCommandActorType.Populate();
                NAS.DAL.BI.Inventory.InventoryCommandDim.Populate();

                #region Other populate
                using (Session session = XpoHelper.GetNewSession())
                {
                    //Insert undefined supplier
                    if (!Util.isExistXpoObject<SupplierOrg>("OrganizationId",
                            Guid.Parse("3DEF2B62-2162-46CD-8418-DEE6F8E59E21")))
                    {
                        SupplierOrg undefinedSupplierOrg = new SupplierOrg(session)
                        {
                            OrganizationId = Guid.Parse("3DEF2B62-2162-46CD-8418-DEE6F8E59E21"),
                            Name = "Mặc định",
                            Description = "Mặc định",
                            Code = "MACDINH",
                            RowCreationTimeStamp = DateTime.Now,
                            RowStatus = Constant.ROWSTATUS_ACTIVE,
                            OrganizationTypeId =
                                NAS.DAL.Util.getDefaultXpoObject<OrganizationType>(session)
                        };
                        undefinedSupplierOrg.Save();
                    }
                }
                #endregion

            }
            catch (Exception)
            {
                throw new Exception("Populate failed");
            }
            finally
            {

            }
        }

        #region ETL

        public static void Populate(Session dbsession)
        {
            try
            {
                Session session = dbsession;
                ETLJobDetail.Populate(session);
                BusinessObject.Populate(session);
            }
            catch (Exception)
            {
                throw new Exception("Populate failed");
            }
            finally
            {

            }
        }

        public static bool IsExistXpoObject<T>(Session dbSession, string fieldName, object value, params short[] rowStatuses)
        {
            return IsExistXpoObject<T>(dbSession, fieldName, value, BinaryOperatorType.Equal, rowStatuses);
        }
        public static bool IsExistXpoObject<T>(Session dbSession, string fieldName, object value, BinaryOperatorType type, params short[] rowStatuses)
        {
            Session session = null;
            try
            {
                CriteriaOperator rowStatusCriteria = null;
                if (rowStatuses.Length > 0)
                {
                    rowStatusCriteria = new InOperator("RowStatus", rowStatuses.ToList());
                }
                CriteriaOperator fieldCriteria = new BinaryOperator(fieldName, value, type);
                CriteriaOperator criteria = CriteriaOperator.And(fieldCriteria, rowStatusCriteria);
                session = dbSession;
                var result = session.GetObjects(session.GetClassInfo(typeof(T)), criteria, null, 0, false, true);

                if (result != null && result.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        #endregion

    }
}
