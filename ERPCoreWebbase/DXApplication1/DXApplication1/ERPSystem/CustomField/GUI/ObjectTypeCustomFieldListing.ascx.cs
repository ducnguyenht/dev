using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using DevExpress.Data.Filtering;
using NAS.DAL.CMS.ObjectDocument;
using Utility;

namespace WebModule.ERPSystem.CustomField.GUI
{
    public partial class ObjectTypeCustomFieldListing : System.Web.UI.UserControl
    {

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsObjectType.Session = session;
            dsObjectTypeCustomFields.Session = session;
            dsCustomField.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                ObjectTypeId = Guid.NewGuid();
                GUIContext = new NAS.GUI.Pattern.Context();
                //GUIContext.State = new State.ObjectTypeEditingForm.ObjectTypeEditing(this);
            }
            gridviewCustomField_BindData();
        }

        #region State Pattern

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

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
            //get { return (NAS.GUI.Pattern.Context)HttpContext.Current.Items["GUIContext_ObjectTypeCustomFieldListing"]; }
            //set { HttpContext.Current.Items["GUIContext_ObjectTypeCustomFieldListing"] = value; }
        }


        #region UpdateGUI

        public bool ObjectTypeEditing_UpdateGUI()
        {
            popupObjectTypeEditing.ShowOnPageLoad = true;
            return true;
        }

        #endregion


        #region CRUD

        public bool ObjectTypeEditing_CRUD()
        {
            //Bind general information data
            CriteriaOperator objectTypeCriteria = 
                new BinaryOperator("ObjectTypeId", ObjectTypeId);
            dsObjectType.Criteria = objectTypeCriteria.ToString();
            formlayoutObjectTypeEditing.DataBind();
            gridviewCustomField_BindData();
            return true;
        }

        #endregion


        #region PreTransitionCRUD

        public bool StateName_PreTransitionCRUD(string transition)
        {
            return true;
        }

        #endregion


        #endregion

        private void gridviewCustomField_BindData()
        {
            //Bind object custom fields data
            CriteriaOperator objectTypeCustomFieldCriteria =
                new BinaryOperator("ObjectTypeId.ObjectTypeId", ObjectTypeId);
            dsObjectTypeCustomFields.Criteria = objectTypeCustomFieldCriteria.ToString();
            gridviewCustomField.DataBind();
        }

        public Guid ObjectTypeId
        {
            get { return (Guid)Session["ObjectTypeId_" + ViewStateControlId]; }
            set { Session["ObjectTypeId_" + ViewStateControlId] = value; }
        }

        protected void cpnObjectTypeEditForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string transition = null;
            bool isSuccess = false;
            try
            {
                string[] args = e.Parameter.Split('|');
                transition = args[0];
                if (transition.Equals("Refresh"))
                {
                    GUIContext.State =
                        new State.ObjectTypeEditingForm.ObjectTypeEditing(this);
                }
                else if (transition.Equals("Edit"))
                {
                    if (args.Length > 1)
                    {
                        ObjectTypeId = Guid.Parse(args[1]);
                        CustomFieldEditingForm1.ObjectTypeId = ObjectTypeId;
                    }
                    else
                    {
                        throw new Exception();
                    }
                    GUIContext.State =
                         new State.ObjectTypeEditingForm.ObjectTypeEditing(this);

                }
                //else if (transition.Equals("Delete"))
                //{
                //    if (args.Length > 1)
                //    {
                //        //Find and delete custom field of the objecttype
                //        Guid objectTypeCustomFieldId = Guid.Parse(args[1]);
                //        ObjectTypeCustomField objectTypeCustomField = session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);
                //        objectTypeCustomField.Delete();
                //    }
                //    else
                //    {
                //        throw new Exception();
                //    }
                //}
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (transition != null)
                {
                    cpnObjectTypeEditForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            transition, isSuccess.ToString().ToLower()));
                }
            }
        }

        protected void gridviewCustomField_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid objectTypeCustomFieldId = (Guid)e.Keys["ObjectTypeCustomFieldId"];
            ObjectTypeCustomField objectTypeCustomField = 
                session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);
            if (objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER)
                || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
                || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
            {
                throw new Exception("Không thể hủy bỏ trường dữ liệu được gắn vào bởi hệ thống");
            }
        }

        protected void gridviewCustomField_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs e)
        {
            //if (e.ButtonID.Equals("CustomField_Edit"))
            //{
            //    var objectTypeCustomFieldId = gridviewCustomField.GetRowValues(e.VisibleIndex, "ObjectTypeCustomFieldId");
            //    if (objectTypeCustomFieldId == null)
            //        return;
            //    ObjectTypeCustomField objectTypeCustomField =
            //        session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);
            //    if (objectTypeCustomField.CustomFieldId.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER)
            //        || objectTypeCustomField.CustomFieldId.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
            //        || objectTypeCustomField.CustomFieldId.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
            //else if (e.ButtonID.Equals("CustomField_Delete"))
            //{
            //    var objectTypeCustomFieldId = gridviewCustomField.GetRowValues(e.VisibleIndex, "ObjectTypeCustomFieldId");
            //    if (objectTypeCustomFieldId == null)
            //        return;
            //    ObjectTypeCustomField objectTypeCustomField =
            //        session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);
            //    if (objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER)
            //        || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
            //        || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
        }

        protected void gridviewCustomField_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Edit
                || e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.Delete)
            {
                var objectTypeCustomFieldId = gridviewCustomField.GetRowValues(e.VisibleIndex, "ObjectTypeCustomFieldId");
                if (objectTypeCustomFieldId == null)
                    return;
                ObjectTypeCustomField objectTypeCustomField =
                    session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);
                if (objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER)
                    || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY)
                    || objectTypeCustomField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY))
                {
                    e.Visible = false;
                }
            }
        }

        protected void gridviewCustomField_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["ObjectTypeId!Key"] = ObjectTypeId;
        }

        protected void gridviewCustomField_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (gridviewCustomField.IsNewRowEditing)
            {
                if (e.NewValues["CustomFieldId!Key"] == null)
                {
                    Helpers.AddErrorToGridViewColumn(
                        e.Errors,
                        gridviewCustomField.Columns["CustomFieldId!Key"],
                        (string)HttpContext.GetGlobalResourceObject("MessageResource", "Msg_Required_Select"));
                }
                else
                {
                    //Get object type
                    var customFieldId = e.NewValues["CustomFieldId!Key"];
                    ObjectType objectType = session.GetObjectByKey<ObjectType>(ObjectTypeId);
                    int countExist = objectType.ObjectTypeCustomFields
                        .Count(r => r.CustomFieldId.CustomFieldId == (Guid)customFieldId);
                    if (countExist > 0)
                    {
                        Helpers.AddErrorToGridViewColumn(
                            e.Errors,
                            gridviewCustomField.Columns["CustomFieldId!Key"],
                            "Trường động đã tồn tại");
                    }
                }
            }
            else if (gridviewCustomField.IsEditing)
            {
                if (e.NewValues["CustomFieldId!Key"] == null)
                {
                    Helpers.AddErrorToGridViewColumn(
                        e.Errors,
                        gridviewCustomField.Columns["CustomFieldId!Key"],
                        (string)HttpContext.GetGlobalResourceObject("MessageResource", "Msg_Required_Select"));
                }
                else if (!e.NewValues["CustomFieldId!Key"].Equals(e.OldValues["CustomFieldId!Key"]))
                {
                    //Get object type
                    var customFieldId = e.NewValues["CustomFieldId!Key"];
                    ObjectType objectType = session.GetObjectByKey<ObjectType>(ObjectTypeId);
                    int countExist = objectType.ObjectTypeCustomFields
                        .Count(r => r.CustomFieldId.CustomFieldId == (Guid)customFieldId);
                    if (countExist > 0)
                    {
                        Helpers.AddErrorToGridViewColumn(
                            e.Errors,
                            gridviewCustomField.Columns["CustomFieldId!Key"],
                            "Trường động đã tồn tại");
                    }
                }
            }
        }

    }
}