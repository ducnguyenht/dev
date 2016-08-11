using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using DevExpress.Web.ASPxEditors;
using WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;
using NAS.BO.CMS.ObjectDocument;
using Utility;

namespace WebModule.ERPSystem.CustomField.GUI
{
    public partial class CustomFieldEditingForm : System.Web.UI.UserControl
    {

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCustomField.Session = session;
            dsCustomFieldType.Session = session;
            dsCustomFieldData.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                GUIContext = new NAS.GUI.Pattern.Context();
                CustomFieldId = Guid.Empty;
            }
            gridviewListData_BindData(false);
            //formlayoutGeneralInfo.DataBind();
        }

        public Guid ObjectTypeId
        {
            get 
            {
                return (Guid)Session["ObjectTypeId_" + ViewStateControlId]; 
            }
            set
            {
                Session["ObjectTypeId_" + ViewStateControlId] = value;
            }
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
            //get { return (NAS.GUI.Pattern.Context)HttpContext.Current.Items["GUIContext_CustomFieldEditingForm"]; }
            //set { HttpContext.Current.Items["GUIContext_CustomFieldEditingForm"] = value; }
        }

        #region UpdateGUI

        #region CustomFieldCreating
        public bool CustomFieldCreating_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = false;
            pagCustomFieldEditingForm.ActiveTabIndex = 0;
            SaveButton.Visible = false;
            BackwardButton.Visible = false;
            ForwardButton.Visible = false;
            FinishButton.Visible = false;
            return true;
        }

        public bool CustomFieldCreatingHasInitData_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = true;
            pagCustomFieldEditingForm.TabPages[1].ClientEnabled = false;
            pagCustomFieldEditingForm.ActiveTabIndex = 0;
            SaveButton.Visible = false;
            BackwardButton.Visible = false;
            ForwardButton.Visible = true;
            FinishButton.Visible = false;
            return true;
        }

        public bool CustomFieldCreatingHasNoInitData_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = false;
            pagCustomFieldEditingForm.ActiveTabIndex = 0;
            SaveButton.Visible = false;
            BackwardButton.Visible = false;
            ForwardButton.Visible = false;
            FinishButton.Visible = true;
            return true;
        }
        #endregion

        #region CustomFieldEditing
        public bool CustomFieldEditing_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = false;
            pagCustomFieldEditingForm.ActiveTabIndex = 0;
            BackwardButton.Visible = false;
            ForwardButton.Visible = false;
            FinishButton.Visible = false;
            cbbCustomFieldType.ClientEnabled = false;
            return true;
        }
        public bool CustomFieldEditingHasInitData_UpdateGUI()
        {
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = true;
            return true;
        }
        public bool CustomFieldEditingHasNoInitData_UpdateGUI()
        {
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = false;
            return true;
        }
        #endregion

        #region CustomFieldCreatingFinished
        public bool CustomFieldCreatingFinished_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = false;
            return true;
        }
        #endregion

        #region CustomFieldEditingCanceled
        public bool CustomFieldEditingCanceled_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = false;
            return true;
        }
        #endregion

        #region CustomFieldDataCreating
        public bool CustomFieldDataCreating_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            pagCustomFieldEditingForm.TabPages[0].ClientEnabled = false;
            pagCustomFieldEditingForm.TabPages[1].ClientVisible = true;
            pagCustomFieldEditingForm.ActiveTabIndex = 1;
            SaveButton.Visible = false;
            BackwardButton.Visible = true;
            ForwardButton.Visible = false;
            FinishButton.Visible = true;
            return true;
        }

        public bool CustomFieldDataCreatingMultiChoiceList_UpdateGUI()
        {
            return true;
        }

        public bool CustomFieldDataCreatingSingleChoiceList_UpdateGUI()
        {
            return true;
        }
        #endregion

        #region CustomFieldDataEditing
        public bool CustomFieldDataEditing_UpdateGUI()
        {
            popupCustomFieldEditingForm.ShowOnPageLoad = true;
            BackwardButton.Visible = false;
            ForwardButton.Visible = false;
            FinishButton.Visible = false;
            SaveButton.Visible = false;
            return true;
        }

        public bool CustomFieldDataEditingMultiChoiceList_UpdateGUI()
        {
            return true;
        }

        public bool CustomFieldDataEditingSingleChoiceList_UpdateGUI()
        {
            return true;
        }
        #endregion

        #endregion

        #region CRUD

        #region CustomFieldCreating
        public bool CustomFieldCreating_CRUD()
        {
            formlayoutGeneralInfo.DataSourceID = String.Empty;
            formlayoutGeneralInfo.DataBind();
            return true;
        }

        public bool CustomFieldCreatingHasInitData_CRUD()
        {
            return true;
        }

        public bool CustomFieldCreatingHasNoInitData_CRUD()
        {
            return true;
        }
        #endregion

        #region CustomFieldEditing
        public bool CustomFieldEditing_CRUD()
        {
            formlayoutGeneralInfo.DataSourceID = dsCustomField.ID;
            CriteriaOperator criteria = new BinaryOperator("CustomFieldId", CustomFieldId);
            dsCustomField.Criteria = criteria.ToString();
            formlayoutGeneralInfo.DataBind();
            return true;
        }
        public bool CustomFieldEditingHasInitData_CRUD()
        {
            return true;
        }
        public bool CustomFieldEditingHasNoInitData_CRUD()
        {
            return true;
        }
        #endregion

        #region CustomFieldCreatingFinished
        public bool CustomFieldCreatingFinished_CRUD()
        {
            return true;
        }
        #endregion

        #region CustomFieldEditingCanceled
        public bool CustomFieldEditingCanceled_CRUD()
        {
            return true;
        }
        #endregion

        #region CustomFieldDataCreating
        public bool CustomFieldDataCreating_CRUD()
        {
            return true;
        }

        public bool CustomFieldDataCreatingMultiChoiceList_CRUD()
        {
            gridviewListData_BindData(true);
            return true;
        }

        public bool CustomFieldDataCreatingSingleChoiceList_CRUD()
        {
            gridviewListData_BindData(true);
            return true;
        }
        #endregion

        #region CustomFieldDataEditing
        public bool CustomFieldDataEditing_CRUD()
        {
            return true;
        }

        public bool CustomFieldDataEditingMultiChoiceList_CRUD()
        {
            gridviewListData_BindData(true);
            return true;
        }

        public bool CustomFieldDataEditingSingleChoiceList_CRUD()
        {
            gridviewListData_BindData(true);
            return true;
        }
        #endregion

        #endregion

        #region PreTransitionCRUD

        #region CustomFieldCreating
        public bool CustomFieldCreating_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool CustomFieldCreatingHasInitData_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "NEXT":
                    if (ASPxEdit.ValidateEditorsInContainer(formlayoutGeneralInfo))
                    {
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {
                            Guid selectedCustomFieldTypeId =
                                Guid.Parse(cbbCustomFieldType.SelectedItem.Value.ToString());
                            CustomFieldType customFieldType =
                                uow.GetObjectByKey<CustomFieldType>(selectedCustomFieldTypeId);
                            //Create new CustomField
                            NAS.DAL.CMS.ObjectDocument.CustomField customField =
                                new NAS.DAL.CMS.ObjectDocument.CustomField(uow)
                                {
                                    CustomFieldId = Guid.NewGuid(),
                                    Name = txtCustomFieldName.Text,
                                    CustomFieldTypeId = customFieldType
                                };
                            //Set new Id to session variable
                            CustomFieldId = customField.CustomFieldId;
                            uow.CommitChanges();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        public bool CustomFieldCreatingHasNoInitData_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "ACCEPT":
                    if (ASPxEdit.ValidateEditorsInContainer(formlayoutGeneralInfo))
                    {
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {
                            Guid selectedCustomFieldTypeId =
                                Guid.Parse(cbbCustomFieldType.SelectedItem.Value.ToString());
                            CustomFieldType customFieldType =
                                uow.GetObjectByKey<CustomFieldType>(selectedCustomFieldTypeId);
                            //Create new CustomField
                            NAS.DAL.CMS.ObjectDocument.CustomField customField =
                                new NAS.DAL.CMS.ObjectDocument.CustomField(uow)
                            {
                                CustomFieldId = Guid.NewGuid(),
                                Name = txtCustomFieldName.Text,
                                CustomFieldTypeId = customFieldType
                            };
                            //Attach CustomField to ObjectTypeId
                            if (ObjectTypeId != null && !ObjectTypeId.Equals(Guid.Empty))
                            {
                                /*2013-12-12 Khoa.Truong DEL START
                                 * Decoupling with the client using this form
                                //Guid objectTypeId = ((ObjectTypeCustomFieldListing)Parent).ObjectTypeId;
                                 *2013-12-12 Khoa.Truong DEL END*/
                                ObjectType objectType = uow.GetObjectByKey<ObjectType>(ObjectTypeId);
                                ObjectTypeCustomField objectTypeCustomField = new ObjectTypeCustomField(uow)
                                {
                                    ObjectTypeCustomFieldId = Guid.NewGuid(),
                                    CustomFieldId = customField,
                                    ObjectTypeId = objectType
                                };
                            }
                            //Attach new custom fields for all object of the object type

                            /*These code is replace with lazy updating for custom fields of each object
                            //ObjectBO objectBO = new ObjectBO();
                            //objectBO.UpdateCMSObjects(uow, objectTypeId);
                            */

                            uow.CommitChanges();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        #endregion

        #region CustomFieldEditing
        public bool CustomFieldEditing_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "SAVE":
                    if (ASPxEdit.ValidateEditorsInContainer(formlayoutGeneralInfo))
                    {
                        using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                        {
                            Guid selectedCustomFieldTypeId =
                                Guid.Parse(cbbCustomFieldType.SelectedItem.Value.ToString());
                            CustomFieldType customFieldType =
                                uow.GetObjectByKey<CustomFieldType>(selectedCustomFieldTypeId);
                            //Get eidting CustomField
                            NAS.DAL.CMS.ObjectDocument.CustomField customField =
                                uow.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(CustomFieldId);
                            customField.Name = txtCustomFieldName.Text;
                            customField.CustomFieldTypeId = customFieldType;
                            //Set new Id to session variable
                            CustomFieldId = customField.CustomFieldId;
                            uow.CommitChanges();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
        public bool CustomFieldEditingHasInitData_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool CustomFieldEditingHasNoInitData_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion

        #region CustomFieldCreatingFinished
        public bool CustomFieldCreatingFinished_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion

        #region CustomFieldEditingCanceled
        public bool CustomFieldEditingCanceled_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion

        #region CustomFieldDataCreating
        public bool CustomFieldDataCreating_PreTransitionCRUD(string transition)
        {
            switch (transition.ToUpper())
            {
                case "ACCEPT":
                    using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                    {
                        //Get creating custom field
                        NAS.DAL.CMS.ObjectDocument.CustomField customField =
                            uow.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(CustomFieldId);
                        //Attach CustomField to ObjectTypeId
                        if (ObjectTypeId != null && !ObjectTypeId.Equals(Guid.Empty))
                        {
                            /*2013-12-12 Khoa.Truong DEL START
                             * Decoupling with the client using this form
                            //Guid objectTypeId = ((ObjectTypeCustomFieldListing)Parent).ObjectTypeId;
                             *2013-12-12 Khoa.Truong DEL END*/
                            ObjectType objectType = uow.GetObjectByKey<ObjectType>(ObjectTypeId);
                            ObjectTypeCustomField objectTypeCustomField = new ObjectTypeCustomField(uow)
                            {
                                ObjectTypeCustomFieldId = Guid.NewGuid(),
                                CustomFieldId = customField,
                                ObjectTypeId = objectType
                            };
                        }
                        /*These code is replace with lazy updating for custom fields of each object
                        ////Attach new custom fields for all object of the object type
                        //ObjectBO objectBO = new ObjectBO();
                        //objectBO.UpdateCMSObjects(uow, objectTypeId);
                        */
                        uow.CommitChanges();
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool CustomFieldDataCreatingMultiChoiceList_PreTransitionCRUD(string transition)
        {
            return true;
        }

        public bool CustomFieldDataCreatingSingleChoiceList_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion

        #region CustomFieldDataEditing
        public bool CustomFieldDataEditing_PreTransitionCRUD(string transition)
        {
            return true;
        }

        public bool CustomFieldDataEditingMultiChoiceList_PreTransitionCRUD(string transition)
        {
            return true;
        }

        public bool CustomFieldDataEditingSingleChoiceList_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion

        #endregion


        #endregion

        private void gridviewListData_BindData(bool isGetDataOfAnyState)
        {
            if (!isGetDataOfAnyState)
            {
                if (!(GUIContext.State is CustomFieldDataCreatingMultiChoiceList)
                && !(GUIContext.State is CustomFieldDataCreatingSingleChoiceList)
                && !(GUIContext.State is CustomFieldDataEditingMultiChoiceList)
                && !(GUIContext.State is CustomFieldDataEditingSingleChoiceList))
                {
                    return;
                }
            }

            dsCustomFieldData.TypeName = typeof(NAS.DAL.CMS.ObjectDocument.CustomFieldDataString).FullName;
            ((GridViewDataTextColumn)gridviewListData.Columns["DataValue"]).FieldName = "StringValue";
            CriteriaOperator criteria = new BinaryOperator("CustomFieldId.CustomFieldId", CustomFieldId);
            dsCustomFieldData.Criteria = criteria.ToString();
            gridviewListData.DataBind();

        }

        private Guid CustomFieldId
        {
            get { return (Guid)Session["CustomFieldId_" + ViewStateControlId]; }
            set { Session["CustomFieldId_" + ViewStateControlId] = value; }
        }

        private string GetCustomFieldCreatingTransition(string customFieldTypeCode)
        {
            string transition = null;
            if (customFieldTypeCode.Equals("SINGLE_CHOICE_LIST")
                || customFieldTypeCode.Equals("MULTI_CHOICE_LIST"))
            {
                transition = "HAS_INIT_DATA";
            }
            else
            {
                transition = "HAS_NO_INIT_DATA";
            }
            return transition;
        }

        private string GetCustomFieldEditingTransition(string customFieldTypeCode)
        {
            return GetCustomFieldCreatingTransition(customFieldTypeCode);
        }

        private string GetCustomFieldDataCreatingTransition(string customFieldTypeCode)
        {
            string transition = null;
            switch (customFieldTypeCode)
            {
                case "SINGLE_CHOICE_LIST":
                    transition = "CREATE_SINGLE_CHOICE_LIST_DATA";
                    break;
                case "MULTI_CHOICE_LIST":
                    transition = "CREATE_MULTI_CHOICE_LIST_DATA";
                    break;
                default:
                    break;
            }
            return transition;
        }

        private string GetCustomFieldDataEditingTransition(string customFieldTypeCode)
        {
            string transition = null;
            switch (customFieldTypeCode)
            {
                case "SINGLE_CHOICE_LIST":
                    transition = "EDIT_SINGLE_CHOICE_LIST_DATA";
                    break;
                case "MULTI_CHOICE_LIST":
                    transition = "EDIT_MULTI_CHOICE_LIST_DATA";
                    break;
                default:
                    break;
            }
            return transition;
        }

        private ASPxButton BackwardButton
        {
            get
            {
                return (ASPxButton)popupCustomFieldEditingForm.FindControl("btnBackward");
            }
        }

        private ASPxButton ForwardButton
        {
            get
            {
                return (ASPxButton)popupCustomFieldEditingForm.FindControl("btnForward");
            }
        }

        private ASPxButton SaveButton
        {
            get
            {
                return (ASPxButton)popupCustomFieldEditingForm.FindControl("btnSave");
            }
        }

        private ASPxButton FinishButton
        {
            get
            {
                return (ASPxButton)popupCustomFieldEditingForm.FindControl("btnFinish");
            }
        }

        private CustomFieldType GetSelectedCustomFieldType()
        {
            Guid selectedCustomFieldTypeId;
            //Get custom field type
            selectedCustomFieldTypeId = Guid.Parse(cbbCustomFieldType.SelectedItem.Value.ToString());
            return session.GetObjectByKey<CustomFieldType>(selectedCustomFieldTypeId);
        }

        private void ClearForm()
        {
            ASPxEdit.ClearEditorsInContainer(formlayoutGeneralInfo);
            txtCustomFieldName.IsValid = true;
            cbbCustomFieldType.IsValid = true;
        }

        protected void cpnCustomFieldEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            bool isSuccess = false;
            string transition = null;
            try
            {
                switch (command)
                {
                    case "Create":
                        ClearForm();
                        CustomFieldId = Guid.NewGuid();
                        GUIContext.State = new CustomFieldCreating(this);
                        transition = command;
                        isSuccess = true;
                        break;
                    case "Edit":
                        if (args.Length > 1)
                        {
                            CustomFieldId = Guid.Parse(args[1]);
                            //Check is system type
                            NAS.DAL.CMS.ObjectDocument.CustomField customField = 
                                session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.CustomField>(CustomFieldId);
                            if(customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_READONLY)
                                || customField.CustomFieldType.Equals(CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_MASTER_READONLY))
                            {
                                throw new Exception("Không thể chỉnh sửa trường dữ liệu được tạo ra bởi hệ thống");
                            }
                        }
                        else
                        {
                            throw new Exception("CustomFieldId cannot be null");
                        }
                        ClearForm();
                        GUIContext.State = new CustomFieldEditing(this);
                        GUIContext.Request(GetCustomFieldEditingTransition(GetSelectedCustomFieldType().Code), this);
                        transition = command;
                        isSuccess = true;
                        break;
                    case "TabChanged":
                        string editingTransition = null;
                        if (pagCustomFieldEditingForm.ActiveTabIndex == 0)
                        {
                            string customFieldEditingTransition = null;
                            editingTransition = "EDIT_FIELD";
                            //Transit to CustomFieldDataEditing state by transition 'EDIT_FIELD'
                            GUIContext.Request(editingTransition, this);
                            //Then transit to concrete CustomFieldCreating
                            customFieldEditingTransition = GetCustomFieldEditingTransition(GetSelectedCustomFieldType().Code);
                            GUIContext.Request(customFieldEditingTransition, this);
                        }
                        else if (pagCustomFieldEditingForm.ActiveTabIndex == 1)
                        {
                            editingTransition = "EDIT_DATA";
                            GUIContext.Request(editingTransition, this);
                            GUIContext.Request(GetCustomFieldDataEditingTransition(GetSelectedCustomFieldType().Code), this);
                        }
                        transition = editingTransition;
                        txtCustomFieldName.IsValid = true;
                        isSuccess = true;
                        break;
                    case "DataTypeChanged":
                        string customFieldTransition = null;
                        customFieldTransition = GetCustomFieldCreatingTransition(GetSelectedCustomFieldType().Code);
                        GUIContext.Request(customFieldTransition, this);
                        transition = customFieldTransition;
                        txtCustomFieldName.IsValid = true;
                        isSuccess = true;
                        break;
                    case "Next":
                        string customFieldDataCreatingTransition = null;
                        //Transit to CustomFieldDataCreating state by transition 'NEXT'
                        if (GUIContext.Request(command, this))
                        {
                            //Then transit to concrete CustomFieldDataCreating
                            customFieldDataCreatingTransition = GetCustomFieldDataCreatingTransition(GetSelectedCustomFieldType().Code);
                            GUIContext.Request(customFieldDataCreatingTransition, this);
                        }
                        isSuccess = true;
                        transition = command;
                        break;
                    default:
                        GUIContext.Request(command, this);
                        transition = command;
                        isSuccess = true;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (transition != null)
                {
                    cpnCustomFieldEditingForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            transition, isSuccess.ToString().ToLower()));
                }
            }
        }

        protected void gridviewListData_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["CustomFieldId!Key"] = CustomFieldId;
        }

    }
}