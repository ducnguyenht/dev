using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using Utility;

namespace WebModule.ERPSystem.CustomField.GUI
{
    public partial class CustomFieldListing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Utility.Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Utility.Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utility.Utils.ApplyTheme(this);
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCustomField.Session = session;
            dsCustomFieldItem.Session = session;
            dsObjectType.Session = session;
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                CustomFieldId = Guid.Empty;
            }
            customFieldEditingForm.ObjectTypeId = Guid.Empty;
            gridviewCustomField.DataBind();
            formlayoutCustomFieldAttachment_DataBind();
        }

        protected void gridviewCustomField_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("CustomFieldType"))
            {
                string customFieldType = (string)e.Value;
                switch (customFieldType)
                {
                    case CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT:
                        e.DisplayText = "Người dùng tạo";
                        break;
                    default:
                        e.DisplayText = "Hệ thống";
                        break;
                }
            }
        }

        protected void gridviewCustomField_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid customFieldId = (Guid)e.Keys["CustomFieldId"];
            //Check is system type
            NAS.DAL.CMS.ObjectDocument.CustomField customField =
                session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(customFieldId);
            if (customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY)
                || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY))
            {
                throw new Exception("Không thể xóa trường dữ liệu được tạo ra bởi hệ thống");
            }
        }

        protected void gridviewCustomField_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID.Equals("CustomField_Edit")
            || e.ButtonID.Equals("CustomField_Delete"))
            {
                var objectId = gridviewCustomField.GetRowValues(e.VisibleIndex, "CustomFieldId");
                if (objectId != null)
                {
                    Guid customFieldId = (Guid)gridviewCustomField.GetRowValues(e.VisibleIndex, "CustomFieldId");
                    NAS.DAL.CMS.ObjectDocument.CustomField customField =
                        session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(customFieldId);
                    if (customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY)
                        || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
                        || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER))
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                    else
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.Default;
                    }
                }
            }

        }

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Guid CustomFieldId
        {
            get
            {
                if (Session["CustomFieldId_" + ViewStateControlId] == null)
                    return Guid.Empty;
                return (Guid)Session["CustomFieldId_" + ViewStateControlId];
            }
            set
            {
                Session["CustomFieldId_" + ViewStateControlId] = value;
            }
        }

        private NAS.DAL.CMS.ObjectDocument.CustomField GetCurrentCustomField(Session _session)
        {
            NAS.DAL.CMS.ObjectDocument.CustomField ret = null;
            if (CustomFieldId.Equals(Guid.Empty))
            {
                return null;
            }
            ret = _session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(CustomFieldId);
            return ret;
        }

        private void formlayoutCustomFieldAttachment_DataBind()
        {
            if (CustomFieldId != Guid.Empty)
            {
                NAS.DAL.CMS.ObjectDocument.CustomField customField = null;
                customField = GetCurrentCustomField(session);
                if (customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER)
                                || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
                                || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
                {
                    lblType.Text = "Hệ thống";
                }
                else
                {
                    lblType.Text = "Mặc định";
                }
                //Bind data to form
                dsCustomFieldItem.Criteria = new BinaryOperator("CustomFieldId", CustomFieldId).ToString();
            }
        }

        protected void popupCustomFieldAttachment_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            try
            {
                string[] args = e.Parameter.Split('|');
                NAS.DAL.CMS.ObjectDocument.CustomField customField = null;
                string command = args[0];
                switch (command)
                {
                    #region AttachTo
                    case "AttachTo":
                        //Get CustomField 
                        if (args.Length > 1)
                        {
                            CustomFieldId = Guid.Parse(args[1]);
                        }
                        else
                        {
                            throw new Exception("Invalid parameter");
                        }
                        customField = GetCurrentCustomField(session);

                        //Bind data to form
                        formlayoutCustomFieldAttachment_DataBind();

                        /////Set Selection for gridview
                        gridviewObjectType.Selection.UnselectAll();
                        if (customField.ObjectTypeCustomFields != null)
                        {
                            foreach (var objectTypeCustomField in customField.ObjectTypeCustomFields)
                            {
                                gridviewObjectType.Selection
                                    .SetSelectionByKey(objectTypeCustomField.ObjectTypeId.ObjectTypeId, true);
                            }
                        }
                        break;
                    #endregion

                    #region Save
                    case "Save":
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {
                            //Delete all object type custom field which it's type is not master and system
                            customField = GetCurrentCustomField(uow);
                            var userDefinedObjectTypeCustomFields = customField.ObjectTypeCustomFields.Where(r =>
                                !r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER) &&
                                !r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY) &&
                                !r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY)).ToList();
                            uow.Delete(userDefinedObjectTypeCustomFields);
                            //Get selection to save
                            var selectedObjectTypeIds = gridviewObjectType.GetSelectedFieldValues("ObjectTypeId")
                                    .Select(r => Guid.Parse(r.ToString()));

                            //Update object custom field table
                            foreach (var selectedObjectTypeId in selectedObjectTypeIds)
                            {
                                //Check exist 
                                int count = customField.ObjectTypeCustomFields.Where(r =>
                                    r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER) ||
                                    r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY) ||
                                    r.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
                                        .Count(r => r.ObjectTypeId.ObjectTypeId == selectedObjectTypeId);
                                if (count != 0)
                                    continue;
                                //get ObjectType
                                ObjectType objectType = uow.GetObjectByKey<ObjectType>(selectedObjectTypeId);
                                //Create new ObjectTypeCustomField
                                ObjectTypeCustomField objectTypeCustomField = new ObjectTypeCustomField(uow)
                                {
                                    CustomFieldId = customField,
                                    ObjectTypeId = objectType
                                };
                                objectTypeCustomField.Save();
                            }
                            uow.CommitChanges();
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void gridviewObjectType_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                //Determine type of object type custom field
                //Get current object type
                var objectId = gridviewObjectType.GetRowValues(e.VisibleIndex, "ObjectTypeId");
                if (objectId != null)
                {
                    NAS.DAL.CMS.ObjectDocument.CustomField customField = GetCurrentCustomField(session);
                    if (customField == null)
                        return;
                    ObjectType objectType = session.GetObjectByKey<ObjectType>((Guid)objectId);
                    //Get ObjectTypeCustomField
                    CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("ObjectTypeId", objectType),
                        new BinaryOperator("CustomFieldId", customField),
                        CriteriaOperator.Or(
                            new BinaryOperator("CustomFieldType", CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER),
                            new BinaryOperator("CustomFieldType", CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY),
                            new BinaryOperator("CustomFieldType", CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY)
                        )
                    );
                    ObjectTypeCustomField objectTypeCustomField =
                        session.FindObject<ObjectTypeCustomField>(criteria);
                    if (objectTypeCustomField != null)
                    {
                        e.Enabled = false;
                    }
                }
            }
        }
    }
}