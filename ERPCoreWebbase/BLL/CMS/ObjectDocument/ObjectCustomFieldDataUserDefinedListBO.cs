using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;
using NAS.DAL;
using Utility;

namespace NAS.BO.CMS.ObjectDocument
{
    public class ObjectCustomFieldDataUserDefinedListBO
    {
        public bool UpdateCustomFieldData(Guid objectCustomFieldId,
            IEnumerable<Guid> itemIds,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            return UpdateSelecteCustomFieldAllDataItems(objectCustomFieldId, itemIds, customFieldTypeFlag);
        }


        /* 2013-12-14 Khoa.Truong INS START
         * Copy from NAS.BO.CMS.ObjectDocument.ObjectCustomFieldDataMultiSelectionListBO
         */
        private bool UpdateSelecteCustomFieldAllDataItems(Guid objectCustomFieldId,
            IEnumerable<Guid> list,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    ObjectCustomField objectCustomField =
                                uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                    if (objectCustomField == null)
                        throw new Exception("The key is not exist in ObjectCustomField");

                    objectCustomField.CustomFieldType = customFieldTypeFlag.Value;
                    uow.FlushChanges();

                    int cnt = objectCustomField.ObjectCustomFieldDatas.Count;
                    if (objectCustomField.ObjectCustomFieldDatas != null && cnt > 0)
                    {
                        uow.Delete(objectCustomField.ObjectCustomFieldDatas);
                        uow.FlushChanges();
                    }

                    foreach (Guid g in list)
                    {
                        AddItemToSelectedCustomFieldDataItems(uow, objectCustomFieldId, g);
                    }

                    uow.FlushChanges();

                    objectCustomField =
                                uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                    string displayText = String.Empty;
                    foreach (var item in objectCustomField.ObjectCustomFieldDatas)
                    {
                        if (objectCustomField.ObjectCustomFieldDatas.IndexOf(item) == 0)
                            displayText = ((CustomFieldDataString)item.CustomFieldDataId).StringValue;
                        else
                            displayText += String.Format(";{0}",
                                ((CustomFieldDataString)item.CustomFieldDataId).StringValue);
                    }

                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                    {
                        ObjectCustomFieldId = objectCustomFieldId,
                        CustomFieldName = objectCustomField.ObjectTypeCustomFieldId.CustomFieldId.Name,
                        CustomFieldData = displayText
                    };
                    objectBO.SetDynamicObjectListItem(uow, objectCustomField.ObjectId.ObjectId, dataitem);

                    uow.CommitChanges();
                    return true;
                }
                catch
                {
                    //uow.ExplicitRollbackTransaction();
                    throw;
                }
            }
        }

        private bool AddItemToSelectedCustomFieldDataItems(UnitOfWork uow, Guid objectCustomFieldId, Guid customFieldDataStringId)
        {
            try
            {
                //Get ObjectCustomField by Id
                ObjectCustomField objectCustomField =
                    uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField == null)
                    throw new Exception("The key is not exist in ObjectCustomField");

                CustomFieldDataString customFieldDataString =
                    uow.GetObjectByKey<CustomFieldDataString>(customFieldDataStringId);

                if (customFieldDataString == null)
                    throw new Exception("The key is not exist in CustomFieldDataString");

                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("ObjectCustomFieldId", objectCustomField),
                        new BinaryOperator("CustomFieldDataId", customFieldDataString)
                    );

                ObjectCustomFieldData ocfd = uow.FindObject<ObjectCustomFieldData>(criteria);

                if (ocfd == null)
                {
                    ocfd = new ObjectCustomFieldData(uow);
                    ocfd.ObjectCustomFieldId = objectCustomField;
                    ocfd.CustomFieldDataId = customFieldDataString;
                }
                //else 
                //    throw new Exception("The item has been added before");
                uow.CommitChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /* 2013-12-14 Khoa.Truong INS END */
    }
}
