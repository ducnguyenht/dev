using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
namespace NAS.DAL.CMS.ObjectDocument
{

    public partial class CustomField
    {
        public CustomField(Session session) : base(session) { }
        public override void AfterConstruction() { 
            base.AfterConstruction();
            CustomFieldType = CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT;
        }
    }

}
