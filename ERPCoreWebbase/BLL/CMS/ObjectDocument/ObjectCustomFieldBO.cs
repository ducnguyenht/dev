using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.CMS.ObjectDocument;
using Utility;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.BO.CMS.ObjectDocument
{
    public enum BasicCustomFieldTypeEnum
    {
        NONE,
        STRING,
        DATETIME,
        FLOAT,
        INTEGER
    }

    public enum PredefinitionCustomFieldTypeEnum
    {
        SINGLE_CHOICE_LIST_MANUFACTURER,  
        SINGLE_CHOICE_LIST_ORGANIZATION,
        SINGLE_CHOICE_LIST_DEPARTMENT,
        SINGLE_CHOICE_LIST_PERSON,
        SINGLE_CHOICE_LIST_CUSTOMER,
        SINGLE_CHOICE_LIST_SUPPLIER,
        SINGLE_CHOICE_LIST_INVENTORY,
        SINGLE_CHOICE_LIST_LOT,
        SINGLE_CHOICE_LIST_INVOICE_SALE,
        SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
        SINGLE_CHOICE_LIST_ITEM,
        /*2014/03/03 Duc.Vo INS*/
        SINGLE_CHOICE_LIST_INPUT_INVENTORY_COMMAND,
        /*2014/03/03 Duc.Vo INS*/
        MULTI_CHOICE_LIST_MANUFACTURER,
        MULTI_CHOICE_LIST_ORGANIZATION,
        MULTI_CHOICE_LIST_DEPARTMENT,
        MULTI_CHOICE_LIST_PERSON,
        MULTI_CHOICE_LIST_CUSTOMER,
        MULTI_CHOICE_LIST_SUPPLIER,
        MULTI_CHOICE_LIST_INVENTORY,
        MULTI_CHOICE_LIST_LOT,
        MULTI_CHOICE_LIST_INVOICE_SALE,
        MULTI_CHOICE_LIST_INVOICE_PURCHASE,
        MULTI_CHOICE_LIST_ITEM
    }

    public class ObjectCustomFieldBO
    {
        public bool UpdateBasicData(Guid objectCustomFieldId,
            object value,
            BasicCustomFieldTypeEnum basicCustomFieldType)
        {
            return UpdateBasicData(objectCustomFieldId, 
                value, 
                basicCustomFieldType, 
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdateBasicData(Guid objectCustomFieldId, 
            object value, 
            BasicCustomFieldTypeEnum basicCustomFieldType,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            BasicCustomFieldTypeBOBase basicCustomFieldTypeBO = null;
            switch (basicCustomFieldType)
            {
                case BasicCustomFieldTypeEnum.STRING:
                    basicCustomFieldTypeBO = new CustomFieldDataStringBO();
                    break;
                case BasicCustomFieldTypeEnum.DATETIME:
                    basicCustomFieldTypeBO = new CustomFieldDataDateTimeBO();
                    break;
                case BasicCustomFieldTypeEnum.FLOAT:
                    basicCustomFieldTypeBO = new ObjectCustomFieldDataFloatBO();
                    break;
                case BasicCustomFieldTypeEnum.INTEGER:
                    basicCustomFieldTypeBO = new ObjectCustomFieldDataIntegerBO();
                    break;
                default:
                    break;
            }
            return basicCustomFieldTypeBO.UpdateCustomFieldData(objectCustomFieldId, 
                value, 
                customFieldTypeFlag);
        }

        public bool UpdatePredefinitionData(Guid objectCustomFieldId,
            IEnumerable<Guid> refIds,
            PredefinitionCustomFieldTypeEnum predefinitionCustomFieldType)
        {
            return UpdatePredefinitionData(objectCustomFieldId, 
                refIds, 
                predefinitionCustomFieldType,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdatePredefinitionData(Guid objectCustomFieldId, 
            IEnumerable<Guid> refIds, 
            PredefinitionCustomFieldTypeEnum predefinitionCustomFieldType,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            ObjectCustomFieldDataPreDefinitionBO objectCustomFieldDataPreDefinitionBO =
                new ObjectCustomFieldDataPreDefinitionBO();
            return objectCustomFieldDataPreDefinitionBO.UpdateCustomFieldData(
                objectCustomFieldId, 
                refIds, 
                predefinitionCustomFieldType,
                customFieldTypeFlag);
        }

        public bool UpdateUserDefinedListData(Guid objectCustomFieldId,
            IEnumerable<Guid> itemIds)
        {
            return UpdateUserDefinedListData(objectCustomFieldId,
                itemIds,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdateUserDefinedListData(Guid objectCustomFieldId,  
            IEnumerable<Guid> itemIds,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            ObjectCustomFieldDataUserDefinedListBO objectCustomFieldDataUserDefinedListBO =
                new ObjectCustomFieldDataUserDefinedListBO();
            return objectCustomFieldDataUserDefinedListBO
                .UpdateCustomFieldData(objectCustomFieldId, itemIds, customFieldTypeFlag);
        }

        public ObjectCustomField GetObjectCustomField(Session session, Guid objectCustomFieldId)
        {
            return session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
        }

        public ObjectCustomField GetObjectCustomField(Session session, Guid objectId, Guid objectTypeCustomFieldId)
        {
            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("ObjectId.ObjectId", objectId),
                new BinaryOperator("ObjectTypeCustomFieldId.ObjectTypeCustomFieldId", objectTypeCustomFieldId)
            );
            return session.FindObject<ObjectCustomField>(criteria);
        }
    }
}
