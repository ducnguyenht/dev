using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using NAS.BO.CMS.ObjectDocument;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy
{
    public abstract class NASCustomFieldTypeBuiltInSingleSelectionListStrategy
    {
        public abstract NASCustomFieldPredefinitionData GetPredefinitionDataOfObject(Guid objectCustomFieldId);

        public abstract NASCustomFieldPredefinitionData GetSelectedPredefinitionDataFromList(object source);

        public virtual bool UpdatePredefinitionDataForObject(Guid objectCustomFieldId, NASCustomFieldPredefinitionData data)
        {
            try
            {
                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                PredefinitionCustomFieldTypeEnum predefinitionType;
                List<Guid> IDList = null;
                if (data == null)
                {
                    string customfieldType;
                    using (Session session = XpoHelper.GetNewSession())
                    {
                        customfieldType =
                            objectCustomFieldBO.GetObjectCustomField(session, objectCustomFieldId)
                            .ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code;
                    }
                    predefinitionType = (PredefinitionCustomFieldTypeEnum)
                        Enum.Parse(typeof(PredefinitionCustomFieldTypeEnum), customfieldType);
                    return objectCustomFieldBO.UpdatePredefinitionData(
                        objectCustomFieldId, null, predefinitionType);
                }
                else
                {
                    predefinitionType =
                           (PredefinitionCustomFieldTypeEnum)Enum.Parse(
                               typeof(PredefinitionCustomFieldTypeEnum), data.PredefinitionType);
                    IDList = new List<Guid>();
                    IDList.Add(data.RefId);

                    return objectCustomFieldBO
                        .UpdatePredefinitionData(objectCustomFieldId, IDList, predefinitionType);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public abstract void ItemRequestedByValue(Session session,
                                  object source,
                                  DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e);
        public abstract void ItemsRequestedByFilterCondition(Session session,
                                             object source,
                                             DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e);
        public abstract void Init(object source);
    }
}