using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using Utility;

namespace NAS.BO.CMS.ObjectDocument
{
    public class ObjectCustomFieldDataFloatBO : BasicCustomFieldTypeBOBase
    {
        public CustomFieldDataFloat getCustomfiedData(Session session, Guid ObjectCustomFieldId)
        {
            try
            {
                ObjectCustomField ocf = session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);
                if (ocf == null)
                    throw new Exception("Input key is not exist in ObjectCustomField");

                if (ocf.ObjectTypeCustomFieldId == null || ocf.ObjectTypeCustomFieldId.CustomFieldId == null
                    || ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId == null)
                    throw new Exception("ObjectCustomField's configuration is wrong");

                if (!ocf.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code.Equals("FLOAT"))
                    throw new Exception("ObjectCustomField is not FLOAT type");

                if (ocf.ObjectCustomFieldDatas == null || ocf.ObjectCustomFieldDatas.Count == 0)
                {
                    return null;
                }

                return ocf.ObjectCustomFieldDatas.FirstOrDefault().CustomFieldDataId as CustomFieldDataFloat;
            } 
            catch(Exception e){
                throw e;
            }
        }

        public bool UpdateCustomFieldData(Guid objectCustomFieldId, float value)
        {
            try
            {
                return UpdateCustomFieldData(objectCustomFieldId, 
                    value, 
                    CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool UpdateCustomFieldData(Guid objectCustomFieldId, object value, CustomFieldTypeFlag flag)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Get ObjectCustomField by Id
                ObjectCustomField objectCustomField =
                    uow.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
                if (objectCustomField.ObjectCustomFieldDatas.Count > 0 && objectCustomField.ObjectCustomFieldDatas.FirstOrDefault() != null)
                {
                    objectCustomField.CustomFieldType = flag.Value;
                    uow.FlushChanges();
                    CustomFieldDataFloat customFieldData =
                        (CustomFieldDataFloat)objectCustomField.ObjectCustomFieldDatas
                        .FirstOrDefault().CustomFieldDataId;

                    customFieldData.FloatValue = (double)value;

                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                    {
                        ObjectCustomFieldId = objectCustomFieldId,
                        CustomFieldName = customFieldData.CustomFieldId.Name,
                        CustomFieldData = customFieldData.FloatValue.ToString()
                    };
                    objectBO.SetDynamicObjectListItem(uow, objectCustomField.ObjectId.ObjectId, dataitem);

                    uow.CommitChanges();
                }
                else
                {
                    throw new Exception();
                }
                return true;
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
