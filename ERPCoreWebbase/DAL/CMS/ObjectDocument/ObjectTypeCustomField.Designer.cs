//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace NAS.DAL.CMS.ObjectDocument {

    public partial class ObjectTypeCustomField : XPCustomObject
    {
        Guid fObjectTypeCustomFieldId;
		[Key(true)]
        public Guid ObjectTypeCustomFieldId
        {
            get { return fObjectTypeCustomFieldId; }
            set { SetPropertyValue<Guid>("ObjectTypeCustomFieldId", ref fObjectTypeCustomFieldId, value); }
		}
        ObjectType fObjectTypeId;
        [Association(@"ObjectTypeCustomFieldReferencesObjectType")]
        public ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }
        CustomField fCustomFieldId;
        [Association(@"ObjectTypeCustomFieldReferencesCustomField")]
        public CustomField CustomFieldId
        {
            get { return fCustomFieldId; }
            set { SetPropertyValue<CustomField>("CustomFieldId", ref fCustomFieldId, value); }
        } 
        [Association(@"ObjectCustomFieldReferencesObjectTypeCustomField", typeof(ObjectCustomField)), Aggregated]
        public XPCollection<ObjectCustomField> ObjectCustomFields { get { return GetCollection<ObjectCustomField>("ObjectCustomFields"); } }

        string fCustomFieldType;
        [Size(2)]
        public string CustomFieldType
        {
            get { return fCustomFieldType; }
            set { SetPropertyValue<string>("CustomFieldType", ref fCustomFieldType, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

	}

}