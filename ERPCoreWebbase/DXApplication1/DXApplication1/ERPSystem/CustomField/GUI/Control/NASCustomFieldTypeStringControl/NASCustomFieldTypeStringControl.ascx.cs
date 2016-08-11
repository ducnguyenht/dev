using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeStringControl.State;
using DevExpress.Web.ASPxEditors;
using WebModule.ERPSystem.CustomField.GUI.Control.State;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeStringControl
{
    public partial class NASCustomFieldTypeStringControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {

        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

        public void InitControlState()
        {
            GUIContext = new Context();
            GUIContext.State = new NASCustomFieldTypeStringControlDataViewingState(this);
        }

        public NASCustomFieldTypeStringControl()
        {
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
            //if (!IsPostBack)
            //{
            //    GUIContext = new Context();
            //    GUIContext.State = new NASCustomFieldTypeStringControlDataViewingState(this);
            //}
        }

        #region State Pattern
        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_NASCustomFieldTypeStringControl_" + ClientID]; }
            set { Session["GUIContext_NASCustomFieldTypeStringControl_" + ClientID] = value; }
        }

        #region UpdateGUI
        public bool NASCustomFieldTypeStringControlDataEditingState_UpdateGUI()
        {
            hyperlinkStringDataViewing.Visible = false;
            txtStringValueEditing.Visible = true;
            return true;
        }

        public bool NASCustomFieldTypeStringControlDataViewingState_UpdateGUI()
        {
            hyperlinkStringDataViewing.Visible = true;
            txtStringValueEditing.Visible = false;
            return true;
        }
        #endregion

        #region CRUD
        public bool NASCustomFieldTypeStringControlDataEditingState_CRUD()
        {
            //Get data of custom field by ObjectCustomFieldId in ObjectCustomField table
            CustomFieldDataStringBO customFieldDataStringBO = new CustomFieldDataStringBO();
            CustomFieldDataString customFieldDataString =
                customFieldDataStringBO.GetCustomFieldData(session, ObjectCustomFieldId);
            //Set data to GUI
            txtStringValueEditing.Text = customFieldDataString.StringValue;
            txtStringValueEditing.Focus();
            return true;
        }

        public bool NASCustomFieldTypeStringControlDataViewingState_CRUD()
        {
            //Get data of custom field by ObjectCustomFieldId in ObjectCustomField table
            CustomFieldDataStringBO customFieldDataStringBO = new CustomFieldDataStringBO();
            CustomFieldDataString customFieldDataString =
                customFieldDataStringBO.GetCustomFieldData(session, ObjectCustomFieldId);
            //Set data to GUI
            string linkText = customFieldDataString.StringValue.Equals(String.Empty) ? 
                                "Chỉnh sửa" : customFieldDataString.StringValue;
            hyperlinkStringDataViewing.Text = linkText;
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool NASCustomFieldTypeStringControlDataEditingState_PreTransitionCRUD(string transition)
        {
            if (transition.ToUpper()
                .Equals(NASCustomFieldTypeControlStateTransition.UpdateTransition.TransitionName))
            {
                //Update data of custom field by ObjectCustomFieldId
                CustomFieldDataStringBO customFieldDataStringBO = new CustomFieldDataStringBO();
                string stringValue = txtStringValueEditing.Text;
                if (customFieldDataStringBO.UpdateCustomFieldData(ObjectCustomFieldId, stringValue))
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
                                stringValue,
                                BasicCustomFieldTypeEnum.STRING,
                                null,
                                null));
                    }
                }
            }
            return true;
        }

        public bool NASCustomFieldTypeStringControlDataViewingState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        private void InitClientScript()
        {
            cpnNASCustomFieldTypeStringControl.ClientInstanceName =
                ClientID + "_cpnNASCustomFieldTypeStringControl";

            hyperlinkStringDataViewing.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Edit');"
                + "{0}.PerformCallback('Edit');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeStringControl.ClientInstanceName);

            txtStringValueEditing.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeStringControl.ClientInstanceName);

            txtStringValueEditing.ClientSideEvents.Init =
                String.Format("function(s,e)"
                + "{{"
                + "Utils.AttachShortcutTo(s.GetMainElement(), \"Enter\", function () {{"
                + "if(!{0}.InCallback()) {{"
                + "console.log('Update');"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}});"
                + "}}"
                , cpnNASCustomFieldTypeStringControl.ClientInstanceName);
        }

        protected void cpnNASCustomFieldTypeStringControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

    }
}