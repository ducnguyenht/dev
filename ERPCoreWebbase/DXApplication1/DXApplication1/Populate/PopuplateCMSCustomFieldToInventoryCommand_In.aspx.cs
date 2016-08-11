using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.CMS.ObjectDocument;
using Utility;
using NAS.DAL.CMS.ObjectDocument;

namespace WebModule.Populate
{
    public partial class PopuplateCMSCustomFieldToInventoryCommand_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CustomFieldOption> fields = new List<CustomFieldOption>();
            CustomFieldOption field = new CustomFieldOption();
            field.FieldName = "Phiếu bán";
            field.CustomFieldType = NAS.DAL.CMS.ObjectDocument.CustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE;
            field.CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER;
            field.ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER;
            fields.Add(field);

            field = new CustomFieldOption();
            field.FieldName = "Người tạo";
            field.CustomFieldType = NAS.DAL.CMS.ObjectDocument.CustomFieldTypeEnum.SINGLE_CHOICE_LIST_PERSON;
            field.CustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER;
            field.ObjectTypeCustomFieldFlag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER;
            fields.Add(field);
            ObjectTypeCustomField.AttachCustomFieldsToObjectType(fields, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);

        }
    }
}