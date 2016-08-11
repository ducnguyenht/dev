using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;
using NAS.DAL;
using DevExpress.Xpo.Metadata;

namespace NAS.BO.CMS.ObjectDocument
{
    public class ObjectCustomFieldDataMultiSelectionListBO
    {
        public XPCollection<CustomFieldDataString> GetCustomFieldAllDataItems(Session session, Guid objectCustomFieldId)
        {
            try
            {
                ObjectCustomField ocf = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
                if (ocf == null)
                    throw new Exception("Input key is not exist in ObjectCustomField");

                if (ocf.ObjectTypeCustomFieldId == null || ocf.ObjectTypeCustomFieldId.CustomFieldId == null
                    || ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId == null)
                    throw new Exception("ObjectCustomField's configuration is wrong");

                if (!ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code.Equals("MULTI_CHOICE_LIST"))
                    throw new Exception("ObjectCustomField is not single selection list type");

                CriteriaOperator criteria = new BinaryOperator("CustomFieldId", ocf.ObjectTypeCustomFieldId.CustomFieldId);
                XPCollection<CustomFieldDataString> rs = new XPCollection<CustomFieldDataString>(session, criteria);
                return rs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public XPCollection<CustomFieldDataString> GetSelecteCustomFieldAllDataItems(Session session, Guid objectCustomFieldId)
        {
            try
            {
                ObjectCustomField ocf = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
                if (ocf == null)
                    throw new Exception("Input key is not exist in ObjectCustomField");

                if (ocf.ObjectTypeCustomFieldId == null || ocf.ObjectTypeCustomFieldId.CustomFieldId == null
                    || ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId == null)
                    throw new Exception("ObjectCustomField's configuration is wrong");

                if (!ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code.Equals("MULTI_CHOICE_LIST"))
                    throw new Exception("ObjectCustomField is not single selection list type");

                CriteriaOperator criteria = new ContainsOperator("ObjectCustomFieldDatas", new BinaryOperator("ObjectCustomFieldId", ocf));
                XPCollection<CustomFieldDataString> rs = new XPCollection<CustomFieldDataString>(session, criteria);
                return rs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateSelecteCustomFieldAllDataItems(Guid objectCustomFieldId, List<Guid> list)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    ObjectCustomField objectCustomField =
                                uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                    if (objectCustomField == null)
                        throw new Exception("The key is not exist in ObjectCustomField");

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

        public bool AddItemToSelectedCustomFieldDataItems(UnitOfWork uow, Guid objectCustomFieldId, Guid customFieldDataStringId)
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

                uow.CommitChanges();
                //else 
                //    throw new Exception("The item has been added before");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
