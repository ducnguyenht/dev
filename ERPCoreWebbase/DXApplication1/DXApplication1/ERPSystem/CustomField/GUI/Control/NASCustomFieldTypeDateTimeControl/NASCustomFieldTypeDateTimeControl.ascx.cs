using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Web.ASPxEditors;
using WebModule.ERPSystem.CustomField.GUI.Control.State;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeDateTimeControl.State;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeDateTimeControl
{
    public partial class NASCustomFieldTypeDateTimeControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {

        public NASCustomFieldTypeDateTimeControl()
        {
        }

        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

        public void InitControlState()
        {
            GUIContext = new Context();
            GUIContext.State = new NASCustomFieldTypeDateTimeControlDataViewingState(this);
        }

        public Guid ObjectCustomFieldId
        {
            get
            {
                GridViewDataItemTemplateContainer itemContainer = (GridViewDataItemTemplateContainer)Parent;
                if (itemContainer == null)
                {
                    return Guid.Empty;
                }
                return (Guid)itemContainer.KeyValue;
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            InitClientScript();
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region State Pattern
        public Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_NASCustomFieldTypeDateTimeControl_" + ClientID]; }
            set { Session["GUIContext_NASCustomFieldTypeDateTimeControl_" + ClientID] = value; }
        }

        #region UpdateGUI
        public bool NASCustomFieldTypeDateTimeControlDataEditingState_UpdateGUI()
        {
            hyperlinkDateTimeDataViewing.Visible = false;
            calendar.ClientVisible = true;
            calendar.Focus();
            return true;
        }

        public bool NASCustomFieldTypeDateTimeControlDataViewingState_UpdateGUI()
        {
            hyperlinkDateTimeDataViewing.Visible = true;
            calendar.ClientVisible = false;
            return true;
        }
        #endregion

        #region CRUD
        public bool NASCustomFieldTypeDateTimeControlDataEditingState_CRUD()
        {

            //Get data of custom field by ObjectCustomFieldId in ObjectCustomField table
            CustomFieldDataDateTimeBO customFieldDataDateTimeBO = new CustomFieldDataDateTimeBO();
            CustomFieldDataDateTime customFieldDataDateTime =
                customFieldDataDateTimeBO.GetCustomFieldData(session, ObjectCustomFieldId);
            //Set data to GUI
            calendar.Date = customFieldDataDateTime.DateTimeValue;

            return true;
        }

        public bool NASCustomFieldTypeDateTimeControlDataViewingState_CRUD()
        {

            //Get data of custom field by ObjectCustomFieldId in ObjectCustomField table
            CustomFieldDataDateTimeBO customFieldDataDateTimeBO = new CustomFieldDataDateTimeBO();
            CustomFieldDataDateTime customFieldDataDateTime =
                customFieldDataDateTimeBO.GetCustomFieldData(session, ObjectCustomFieldId);
            //Set data to GUI
            string linkText = customFieldDataDateTime.DateTimeValue.Equals(DateTime.MinValue) ?
                                "Chỉnh sửa" : String.Format("{0:d}", customFieldDataDateTime.DateTimeValue);
            hyperlinkDateTimeDataViewing.Text = linkText;

            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool NASCustomFieldTypeDateTimeControlDataEditingState_PreTransitionCRUD(string transition)
        {
            if (transition.ToUpper()
                .Equals(NASCustomFieldTypeControlStateTransition.UpdateTransition.TransitionName))
            {

                //Update data of custom field by ObjectCustomFieldId
                CustomFieldDataDateTimeBO customFieldDataDateTimeBO = new CustomFieldDataDateTimeBO();
                DateTime dateTimeValue = calendar.Date;
                if (customFieldDataDateTimeBO.UpdateCustomFieldData(ObjectCustomFieldId, dateTimeValue))
                {
                    if (DataUpdated != null)
                    {
                        ObjectCustomField objectCustomField =
                            session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);
                        //Raise DataUpdated event
                        DataUpdated(this,
                            new CustomFieldControlEventArgs(
                                ObjectCustomFieldId,
                                objectCustomField.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId,
                                objectCustomField.ObjectId.ObjectId,
                                objectCustomField.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldId,
                                CustomFieldControlEventArgs.CustomFieldCategoryEnum.BASIC,
                                dateTimeValue,
                                BasicCustomFieldTypeEnum.DATETIME,
                                null,
                                null));
                    }
                }
            }
            return true;
        }

        public bool NASCustomFieldTypeDateTimeControlDataViewingState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        private void InitClientScript()
        {
            cpnNASCustomFieldTypeDateTimeControl.ClientInstanceName =
                ClientID + "_cpnNASCustomFieldTypeDateTimeControl";

            hyperlinkDateTimeDataViewing.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Edit');"
                + "{0}.PerformCallback('Edit');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeDateTimeControl.ClientInstanceName
                , calendar.ClientInstanceName);

            calendar.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeDateTimeControl.ClientInstanceName);

            calendar.ClientSideEvents.DateChanged =
                String.Format("function(s,e)"
                + "{{"
                + "if(!{0}.InCallback()) {{"
                + "console.log('Update');"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeDateTimeControl.ClientInstanceName, calendar.Date);

            calendar.ClientSideEvents.GotFocus =
                String.Format("function(s,e)"
                + "{{"
                + "s.ShowDropDown();"
                + "}}");

            calendar.ClientSideEvents.Init =
                String.Format("function(s,e)"
                + "{{"
                + "Utils.AttachShortcutTo(s.GetMainElement(), \"Enter\", function () {{"
                + "if(!{0}.InCallback()) {{"
                + "console.log('Update');"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}});"
                + "}}"
                , cpnNASCustomFieldTypeDateTimeControl.ClientInstanceName);

        }

        protected void cpnNASCustomFieldTypeDateTimeControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            if (command.ToUpper().Equals("EDIT"))
            {
                if (BeforeDataEditing != null)
                {
                    BeforeDataEditing(this, new EventArgs());
                }
            }
            GUIContext.Request(command, this);
        }

        protected void cpnNASCustomFieldTypeDateTimeControl_Init(object sender, EventArgs e)
        {
            calendar.ClientInstanceName =
                ClientID + "_calendar";
        }

    }
}