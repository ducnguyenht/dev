using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.CMS.ObjectDocument
{
    [Serializable]
    public class DynamicObjectListSerializeDataItem
    {
        public Guid ObjectCustomFieldId { get; set; }
        public string CustomFieldName { get; set; }
        public string CustomFieldData { get; set; }
        public string DisplayText { get { return String.Format("{0}: {1}", CustomFieldName, CustomFieldData); } }
        public string GetKey()
        {
            if (ObjectCustomFieldId != null && ObjectCustomFieldId != Guid.Empty)
                return ObjectCustomFieldId.ToString().Replace("-", "");
            else
                throw new Exception("Id is not valid");
        }
    }
}
