using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Inventory.Lot;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.CMS.ObjectDocument.ObjectCustomFieldDataPreDefinitionStrategy
{
    public class CustomFieldDataPreDefinitionData : List<CustomFieldDataPreDefinitionDataItem>
    {
        public override string ToString()
        {
            string ret = String.Empty;
            for (int i = 0; i < this.Count(); i++)
            {
                if (i == 0)
                {
                    ret = this.ElementAt(i).DisplayText;
                }
                else
                {
                    ret += String.Format(";{0}", this.ElementAt(i).DisplayText);
                }
            }
            return ret;
        }
    }

    public class CustomFieldDataPreDefinitionDataItem
    {
        public Guid CustomFieldDataId { get; set; }
        public Guid RefId { get; set; }
        public string PredefinitionType { get; set; }
        public string DisplayText { get; set; }
    }

    public interface ICustomFieldDataPreDefinitionDataGetter
    {
        CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId);
    }

    public class PreDefinitionManufacturerDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        ManufacturerOrg manufacturerOrg =
                                    session.GetObjectByKey<ManufacturerOrg>(predefinitionData.RefId);

                        if (manufacturerOrg != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = manufacturerOrg.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionOrganizationDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Organization organization =
                                    session.GetObjectByKey<Organization>(predefinitionData.RefId);

                        if (organization != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = organization.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionDepartmentDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Department department =
                                    session.GetObjectByKey<Department>(predefinitionData.RefId);

                        if (department != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = department.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionPersonDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Person person =
                                    session.GetObjectByKey<Person>(predefinitionData.RefId);

                        if (person != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = person.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionCustomerDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Organization customer =
                                    session.GetObjectByKey<Organization>(predefinitionData.RefId);

                        if (customer != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = customer.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionSupplierDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Organization supplier =
                                    session.GetObjectByKey<Organization>(predefinitionData.RefId);

                        if (supplier != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = supplier.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionInventoryDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        NAS.DAL.Nomenclature.Inventory.Inventory inventory =
                                    session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(predefinitionData.RefId);

                        if (inventory != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = inventory.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionLotDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Lot lot =
                                    session.GetObjectByKey<Lot>(predefinitionData.RefId);

                        if (lot != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = lot.Code
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionSaleInvoiceDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        SalesInvoice salesInvoice =
                                    session.GetObjectByKey<SalesInvoice>(predefinitionData.RefId);

                        if (salesInvoice != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = salesInvoice.Code
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionPurchaseInvoiceDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice =
                                    session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(predefinitionData.RefId);

                        if (purchaseInvoice != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = purchaseInvoice.Code
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }

    public class PreDefinitionItemDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        Item itemData = session.GetObjectByKey<Item>(predefinitionData.RefId);

                        if (itemData != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = String.Format("{0} - {1}", itemData.Code, itemData.Name) 
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }
    /*2014/03/03 Duc.Vo INS*/
    public class PreDefinitionInputInventoryCommandDataGetter : ICustomFieldDataPreDefinitionDataGetter
    {
        public CustomFieldDataPreDefinitionData GetData(Guid objectCustomFieldId)
        {
            CustomFieldDataPreDefinitionData ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                ObjectCustomField objectCustomField = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField.ObjectCustomFieldDatas != null)
                {
                    ret = new CustomFieldDataPreDefinitionData();

                    foreach (var data in objectCustomField.ObjectCustomFieldDatas)
                    {
                        PredefinitionData predefinitionData = (PredefinitionData)data.CustomFieldDataId;

                        NAS.DAL.Inventory.Command.InventoryCommand Command =
                                    session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(predefinitionData.RefId);

                        if (Command != null)
                        {
                            CustomFieldDataPreDefinitionDataItem item = new CustomFieldDataPreDefinitionDataItem()
                            {
                                CustomFieldDataId = predefinitionData.CustomFieldDataId,
                                PredefinitionType = predefinitionData.PredefinitionType,
                                RefId = predefinitionData.RefId,
                                DisplayText = Command.Name
                            };
                            ret.Add(item);
                        }
                    }
                }
                return ret;
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
    }
    /*2014/03/03 Duc.Vo INS*/

    public enum PreDefinitionDataGetterType
    {
        MANUFACTURER,
        ORGANIZATION,
        DEPARTMENT,
        PERSON,
        CUSTOMER,
        SUPPLIER,
        INVENTORY,
        /*2014/03/03 Duc.Vo INS*/
        INPUT_INVENTORY_COMMAND,
        /*2014/03/03 Duc.Vo INS*/
        LOT,
        INVOICE_SALE,
        INVOICE_PURCHASE,
        ITEM
    }

    public static class PreDefinitionDataGetterSimpleFactory
    {
        public static ICustomFieldDataPreDefinitionDataGetter Create(PreDefinitionDataGetterType type)
        {
            ICustomFieldDataPreDefinitionDataGetter ret = null;
            switch (type)
            {
                case PreDefinitionDataGetterType.MANUFACTURER:
                    ret = new PreDefinitionManufacturerDataGetter();
                    break;
                case PreDefinitionDataGetterType.ORGANIZATION:
                    ret = new PreDefinitionOrganizationDataGetter();
                    break;
                case PreDefinitionDataGetterType.DEPARTMENT:
                    ret = new PreDefinitionDepartmentDataGetter();
                    break;
                case PreDefinitionDataGetterType.PERSON:
                    ret = new PreDefinitionPersonDataGetter();
                    break;
                case PreDefinitionDataGetterType.CUSTOMER:
                    ret = new PreDefinitionCustomerDataGetter();
                    break;
                case PreDefinitionDataGetterType.SUPPLIER:
                    ret = new PreDefinitionSupplierDataGetter();
                    break;
                case PreDefinitionDataGetterType.INVENTORY:
                    ret = new PreDefinitionInventoryDataGetter();
                    break;
                case PreDefinitionDataGetterType.LOT:
                    ret = new PreDefinitionLotDataGetter();
                    break;
                case PreDefinitionDataGetterType.INVOICE_SALE:
                    ret = new PreDefinitionSaleInvoiceDataGetter();
                    break;
                case PreDefinitionDataGetterType.INVOICE_PURCHASE:
                    ret = new PreDefinitionPurchaseInvoiceDataGetter();
                    break;
                case PreDefinitionDataGetterType.ITEM:
                    ret = new PreDefinitionItemDataGetter();
                    break;
                /*2014/03/03 Duc.Vo INS*/
                case PreDefinitionDataGetterType.INPUT_INVENTORY_COMMAND:
                    ret = new PreDefinitionInputInventoryCommandDataGetter();
                    break;
                /*2014/03/03 Duc.Vo INS*/
                default:
                    break;
            }
            return ret;
        }
    }
}
