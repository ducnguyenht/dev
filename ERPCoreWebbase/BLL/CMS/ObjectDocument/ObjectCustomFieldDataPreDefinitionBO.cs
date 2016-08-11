using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using Utility;
using NAS.BO.CMS.ObjectDocument.ObjectCustomFieldDataPreDefinitionStrategy;

namespace NAS.BO.CMS.ObjectDocument
{

    public class ObjectCustomFieldDataPreDefinitionBO
    {
        private ICustomFieldDataPreDefinitionDataGetter GetDataGetter(PredefinitionCustomFieldTypeEnum preDefinitionType)
        {
            PreDefinitionDataGetterType type = PreDefinitionDataGetterType.ORGANIZATION;
            switch (preDefinitionType)
            {
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_MANUFACTURER:
                    type = PreDefinitionDataGetterType.MANUFACTURER;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_ORGANIZATION:
                    type = PreDefinitionDataGetterType.ORGANIZATION;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_DEPARTMENT:
                    type = PreDefinitionDataGetterType.DEPARTMENT;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_PERSON:
                    type = PreDefinitionDataGetterType.PERSON;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER:
                    type = PreDefinitionDataGetterType.CUSTOMER;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_SUPPLIER:
                    type = PreDefinitionDataGetterType.SUPPLIER;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVENTORY:
                    type = PreDefinitionDataGetterType.INVENTORY;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_LOT:
                    type = PreDefinitionDataGetterType.LOT;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE:
                    type = PreDefinitionDataGetterType.INVOICE_SALE;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE:
                    type = PreDefinitionDataGetterType.INVOICE_PURCHASE;
                    break;
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_ITEM:
                    type = PreDefinitionDataGetterType.ITEM;
                    break;
                /*2014/03/03 Duc.Vo INS*/
                case PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND:
                    type = PreDefinitionDataGetterType.INPUT_INVENTORY_COMMAND;
                    break;
                /*2014/03/03 Duc.Vo INS*/
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_MANUFACTURER:
                    type = PreDefinitionDataGetterType.MANUFACTURER;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_ORGANIZATION:
                    type = PreDefinitionDataGetterType.ORGANIZATION;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_DEPARTMENT:
                    type = PreDefinitionDataGetterType.DEPARTMENT;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_PERSON:
                    type = PreDefinitionDataGetterType.PERSON;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_CUSTOMER:
                    type = PreDefinitionDataGetterType.CUSTOMER;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_SUPPLIER:
                    type = PreDefinitionDataGetterType.SUPPLIER;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_INVENTORY:
                    type = PreDefinitionDataGetterType.INVENTORY;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_LOT:
                    type = PreDefinitionDataGetterType.LOT;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_INVOICE_SALE:
                    type = PreDefinitionDataGetterType.INVOICE_SALE;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_INVOICE_PURCHASE:
                    type = PreDefinitionDataGetterType.INVOICE_PURCHASE;
                    break;
                case PredefinitionCustomFieldTypeEnum.MULTI_CHOICE_LIST_ITEM:
                    type = PreDefinitionDataGetterType.ITEM;
                    break;
                default:
                    throw new Exception("Unsupported type");
            }
            return PreDefinitionDataGetterSimpleFactory.Create(type);
        }

        public bool UpdateCustomFieldData(Guid objectCustomFieldId, 
            IEnumerable<Guid> refIds, 
            PredefinitionCustomFieldTypeEnum preDefinitionType,
            CustomFieldTypeFlag flag)
        {
            bool ret = false;
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                ObjectCustomField objectCustomField = uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                objectCustomField.CustomFieldType = flag.Value;

                uow.FlushChanges();

                NAS.DAL.CMS.ObjectDocument.CustomField customField =
                    objectCustomField.ObjectTypeCustomFieldId.CustomFieldId;

                //Delete all old data
                uow.Delete(objectCustomField.ObjectCustomFieldDatas);
                uow.FlushChanges();
                //Insert new data for object
                if (refIds != null)
                {
                    foreach (var refId in refIds)
                    {
                        PredefinitionData predefinitionData = new PredefinitionData(uow)
                        {
                            CustomFieldId = customField,
                            PredefinitionType =
                                Enum.GetName(typeof(PredefinitionCustomFieldTypeEnum), preDefinitionType),
                            RefId = refId
                        };
                        //predefinitionData.Save();

                        ObjectCustomFieldData objectCustomFieldData = new ObjectCustomFieldData(uow)
                        {
                            CustomFieldDataId = predefinitionData,
                            ObjectCustomFieldId = objectCustomField
                        };
                        //objectCustomFieldData.Save();

                        uow.FlushChanges();
                    }
                }

                ICustomFieldDataPreDefinitionDataGetter getter = GetDataGetter(preDefinitionType);
                ObjectBO objectBO = new ObjectBO();
                DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                {
                    ObjectCustomFieldId = objectCustomFieldId,
                    CustomFieldName = customField.Name,
                    CustomFieldData = getter.GetData(objectCustomFieldId).ToString()
                };
                objectBO.SetDynamicObjectListItem(uow, objectCustomField.ObjectId.ObjectId, dataitem);

                uow.CommitChanges();
                ret = true;
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }
    }
}
