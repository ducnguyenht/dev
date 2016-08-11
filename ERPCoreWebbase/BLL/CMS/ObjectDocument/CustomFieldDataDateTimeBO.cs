using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL;
using Utility;

namespace NAS.BO.CMS.ObjectDocument
{
    public class CustomFieldDataDateTimeBO : BasicCustomFieldTypeBOBase
    {
        public CustomFieldDataDateTime GetCustomFieldData(Session session, Guid objectCustomFieldId)
        {
            CustomFieldDataDateTime customFieldData = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get ObjectCustomField by Id
                ObjectCustomField objectCustomField =
                    session.GetObjectByKey<ObjectCustomField>(objectCustomFieldId);
                if (objectCustomField.ObjectCustomFieldDatas.FirstOrDefault() != null)
                {
                    //Return string data
                    return (CustomFieldDataDateTime)
                        objectCustomField.ObjectCustomFieldDatas.FirstOrDefault().CustomFieldDataId;
                }
                return customFieldData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCustomFieldData(Guid objectCustomFieldId, DateTime dateTimeValue)
        {
            try
            {
                return UpdateCustomFieldData(objectCustomFieldId, 
                    dateTimeValue, 
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
                if (objectCustomField.ObjectCustomFieldDatas.FirstOrDefault() != null)
                {
                    objectCustomField.CustomFieldType = flag.Value;
                    uow.FlushChanges();

                    CustomFieldDataDateTime customFieldData =
                        (CustomFieldDataDateTime)objectCustomField.ObjectCustomFieldDatas
                        .FirstOrDefault().CustomFieldDataId;

                    customFieldData.DateTimeValue = (DateTime)value;

                    ObjectBO objectBO = new ObjectBO();
                    DynamicObjectListSerializeDataItem dataitem = new DynamicObjectListSerializeDataItem()
                    {
                        ObjectCustomFieldId = objectCustomFieldId,
                        CustomFieldName = customFieldData.CustomFieldId.Name,
                        CustomFieldData = customFieldData.DateTimeValue.ToShortDateString()
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
