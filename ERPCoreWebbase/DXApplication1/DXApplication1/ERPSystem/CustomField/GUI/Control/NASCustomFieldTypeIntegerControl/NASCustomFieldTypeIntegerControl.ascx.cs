using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.State;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl
{
    public partial class NASCustomFieldTypeIntegerControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {
        private ObjectCustomFieldDataIntegerBO BO = new ObjectCustomFieldDataIntegerBO();

        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

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

        public void InitControlState()
        {
            GUIContext = new NAS.GUI.Pattern.Context();
            GUIContext.State = new NASCustomFieldTypeIntegerControlDataViewingState(this);
        }

        private CustomFieldDataInt CustomFieldData
        {
            get
            {
                return Session[this.ClientID + "CustomFieldData"] as CustomFieldDataInt;
            }
            set
            {
                Session[this.ClientID + "CustomFieldData"] = value;
            }
        }

        private Session session;

        #region State GUI

        public NAS.GUI.Pattern.Context GUIContext
        {
            get
            {
                return Session[this.ClientID + "GUIContext"] as NAS.GUI.Pattern.Context;
            }
            set
            {
                Session[this.ClientID + "GUIContext"] = value;
            }
        }

        public bool PreCRUD_EdittingState()
        {
            int value = int.MinValue;
            if (txtIntegerValueEditing.Value != null)
            {
                value = int.Parse(txtIntegerValueEditing.Value.ToString());
            }
            if (BO.UpdateCustomFieldData(ObjectCustomFieldId, value))
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
                            value,
                            BasicCustomFieldTypeEnum.INTEGER,
                            null,
                            null));
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CRUD_EditingState()
        {
            return true;//BO.updateCustomFieldData(ObjectCustomFieldId, int.Parse(txtIntegerValueEditing.Value.ToString()));
        }

        public bool CRUD_ViewingState()
        {
            CustomFieldData = BO.getCustomfiedData(session, ObjectCustomFieldId);
            return true;
        }

        public bool UpdateGUI_EdittingState()
        {
            this.txtIntegerValueEditing.Visible = true;
            txtIntegerValueEditing.Focus();
            this.hyperlinkIntegerDataViewing.Visible = false;
            if (CustomFieldData != null)
            {
                if (CustomFieldData.IntValue == int.MinValue)
                    this.txtIntegerValueEditing.Value = null;
                else
                    this.txtIntegerValueEditing.Value = CustomFieldData.IntValue;
                this.hyperlinkIntegerDataViewing.Text = CustomFieldData.IntValue.ToString();
            }
            return true;
        }

        public bool UpdateGUI_ViewingState()
        {

            this.txtIntegerValueEditing.Visible = false;
            this.hyperlinkIntegerDataViewing.Visible = true;
            if (CustomFieldData != null)
            {
                this.txtIntegerValueEditing.Value = CustomFieldData.IntValue;
                if (CustomFieldData.IntValue == int.MinValue)
                    this.hyperlinkIntegerDataViewing.Text = "Chỉnh sửa";
                else
                    this.hyperlinkIntegerDataViewing.Text = CustomFieldData.IntValue.ToString();
            }
            return true;
        }

        public void ClientSetting()
        {
            cpnNASCustomFieldTypeIntegerControl.ClientInstanceName = string.Format("{0}cpnNASCustomFieldTypeIntegerControl", this.ClientID);
            hyperlinkIntegerDataViewing.ClientInstanceName = string.Format("{0}hyperlinkIntegerDataViewing", this.ClientID);
            txtIntegerValueEditing.ClientInstanceName = string.Format("{0}txtIntegerValueEditing", this.ClientID);
            hyperlinkIntegerDataViewing.ClientSideEvents.Click = "function(s, e){ " +
                cpnNASCustomFieldTypeIntegerControl.ClientInstanceName + ".PerformCallback('EDIT'); "
                + "}";

            txtIntegerValueEditing.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeIntegerControl.ClientInstanceName);

            txtIntegerValueEditing.ClientSideEvents.Init =
                String.Format("function(s,e)"
                + "{{"
                + "Utils.AttachShortcutTo(s.GetMainElement(), \"Enter\", function () {{"
                + "if(!{0}.InCallback())"
                + "{{"
                + "console.log('Update');"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}});"
                + "}}"
                , cpnNASCustomFieldTypeIntegerControl.ClientInstanceName);
        }

        #endregion

        public NASCustomFieldTypeIntegerControl()
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ClientSetting();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GUIContext = new NAS.GUI.Pattern.Context();
            //    GUIContext.State = new NASCustomFieldTypeIntegerControlDataViewingState(this);
            //}
        }

        protected void cpnNASCustomFieldTypeIntegerControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            if (para[0].ToUpper().Equals("EDIT"))
            {
                if (BeforeDataEditing != null)
                {
                    BeforeDataEditing(this, new EventArgs());
                }
            }
            GUIContext.Request(para[0], this);
        }
    }
}