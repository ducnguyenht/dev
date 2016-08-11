using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;

namespace NAS.BO.CMS.ObjectDocument
{
    public class ObjectCustomFieldDataSingleSelectionListBO
    {
        public XPCollection<CustomFieldDataString> GetCustomFieldAllData(Session session, Guid objectCustomFieldId)
        {
            try
            {
                ObjectCustomField ocf = session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
                if (ocf == null)
                    throw new Exception("Input key is not exist in ObjectCustomField");

                if (ocf.ObjectTypeCustomFieldId == null || ocf.ObjectTypeCustomFieldId.CustomFieldId == null
                    || ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId == null)
                    throw new Exception("ObjectCustomField's configuration is wrong");

                if (!ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code.Equals("SINGLE_CHOICE_LIST"))
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

        public CustomFieldDataString GetCustomFielData(Session session, Guid objectCustomFieldId)
        {
            try
            {
                CustomFieldDataStringBO bo = new CustomFieldDataStringBO();
                return bo.GetCustomFieldData(session, objectCustomFieldId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateCustomFieldData(Guid objectCustomFieldId, Guid customFieldDataStringId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get ObjectCustomField by Id
                session.BeginTransaction();

                ObjectCustomField objectCustomField =
                    session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);

                if (objectCustomField == null)
                    throw new Exception("The key is not exist in ObjectCustomField");

                CustomFieldDataString customFieldDataString =
                    session.GetObjectByKey<CustomFieldDataString>(customFieldDataStringId);

                if (customFieldDataString == null)
                {
                    session.Delete(objectCustomField.ObjectCustomFieldDatas);
                    DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                    {
                        ObjectCustomFieldId = objectCustomFieldId,
                        CustomFieldName = null,
                        CustomFieldData = null
                    };
                    ObjectBO objectBO = new ObjectBO();
                    objectBO.SetDynamicObjectListItem(session, objectCustomField.ObjectId.ObjectId, dataitem);
                    //throw new Exception("The key is not exist in CustomFieldDataString");
                }
                else
                {
                    if (objectCustomField.ObjectCustomFieldDatas != null && objectCustomField.ObjectCustomFieldDatas.Count > 0)
                    {
                        for (int i = 0; i < objectCustomField.ObjectCustomFieldDatas.Count; i++)
                        {
                            objectCustomField.ObjectCustomFieldDatas[i].Delete();
                        }
                    }
                    ObjectCustomFieldData ocfd = new ObjectCustomFieldData(session);
                    ocfd.ObjectCustomFieldId = objectCustomField;
                    ocfd.CustomFieldDataId = customFieldDataString;
                    ocfd.Save();

                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                    {
                        ObjectCustomFieldId = objectCustomFieldId,
                        CustomFieldName = customFieldDataString.CustomFieldId.Name,
                        CustomFieldData = customFieldDataString.StringValue
                    };
                    objectBO.SetDynamicObjectListItem(session, objectCustomField.ObjectId.ObjectId, dataitem);
                }
                session.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

    }
}
