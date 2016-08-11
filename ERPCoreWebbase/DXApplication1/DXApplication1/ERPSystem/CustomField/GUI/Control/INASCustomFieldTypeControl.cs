using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using NAS.BO.CMS.ObjectDocument;

namespace WebModule.ERPSystem.CustomField.GUI.Control
{
    public delegate void CustomFieldControlDataUpdatedEventHandler(object sender, CustomFieldControlEventArgs args);
    public delegate void CustomFieldControlBeforeDataEditingEventHandler(object sender, EventArgs args);

    public class CustomFieldControlEventArgs : EventArgs
    {
        public enum CustomFieldCategoryEnum
        {
            BASIC,
            LIST,
            BUILT_IN
        }

        private Guid _ObjectCustomFieldId;
        private Guid _ObjectTypeCustomFieldId;
        private Guid _CMSObjectId;
        private Guid _CustomFieldId;
        private CustomFieldCategoryEnum _CustomFieldCategory;
        private object _NewBasicDataValue;
        private BasicCustomFieldTypeEnum _BasicCustomFieldTypeEnum;
        private List<NASCustomFieldPredefinitionData> _NewBuiltInData; 
        private List<Guid> _NewCustomFieldDataIds;

        public CustomFieldControlEventArgs
            (
                Guid objectCustomFieldId,
                Guid objectTypeCustomFieldId,
                Guid cmsObjectId, 
                Guid customFieldId,
                CustomFieldCategoryEnum customFieldCategory,
                object newBasicDataValue,
                BasicCustomFieldTypeEnum basicCustomFieldTypeEnum,
                List<NASCustomFieldPredefinitionData> newBuiltInData,
                List<Guid> newCustomFieldDataIds
            )
        {
            this._ObjectCustomFieldId = objectCustomFieldId;
            this._ObjectTypeCustomFieldId = objectTypeCustomFieldId;
            this._CMSObjectId = cmsObjectId;
            this._CustomFieldId = customFieldId;
            this._CustomFieldCategory = customFieldCategory;
            this._NewBasicDataValue = newBasicDataValue;
            this._BasicCustomFieldTypeEnum = basicCustomFieldTypeEnum;
            this._NewBuiltInData = newBuiltInData;
            this._NewCustomFieldDataIds = newCustomFieldDataIds;
        }

        public Guid ObjectCustomFieldId
        {
            get { return this._ObjectCustomFieldId; }
        }

        public Guid ObjectTypeCustomFieldId
        {
            get { return this._ObjectTypeCustomFieldId; }
        }

        public Guid CMSObjectId
        {
            get { return this._CMSObjectId; }
        }

        public Guid CustomFieldId
        {
            get { return this._CustomFieldId; }
        }

        public CustomFieldCategoryEnum CustomFieldCategory
        {
            get { return this._CustomFieldCategory; }
        }

        public object NewBasicDataValue
        {
            get { return this._NewBasicDataValue; }
        }

        public BasicCustomFieldTypeEnum BasicCustomFieldType
        {
            get { return this._BasicCustomFieldTypeEnum; }
        }

        public List<NASCustomFieldPredefinitionData> NewBuiltInData
        {
            get { return this._NewBuiltInData; }
        }

        public List<Guid> NewCustomFieldDataIds
        {
            get { return this._NewCustomFieldDataIds; }
        }

    }

    public interface INASCustomFieldTypeControl
    {
        event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;
        Guid ObjectCustomFieldId { get; }
        void InitControlState();
    }
}
