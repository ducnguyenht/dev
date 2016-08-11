using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeFloatControl.State;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Web.ASPxGridView;
using NAS.GUI.Pattern;
using NAS.DAL;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeFloatControl
{
    public partial class NASCustomFieldTypeFloatControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {
        ObjectCustomFieldDataFloatBO BO = new ObjectCustomFieldDataFloatBO();

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

        private CustomFieldDataFloat CustomFieldData
        {
            get
            {
                return Session[this.ClientID + "CustomFieldData"] as CustomFieldDataFloat;
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
            //if (txtFloatValueEditing.Value == null)
            //    return BO.updateCustomFieldData(ObjectCustomFieldId, float.MinValue);
            //return BO.updateCustomFieldData(ObjectCustomFieldId, float.Parse(txtFloatValueEditing.Value.ToString()));
            float value = float.MinValue;
            if (txtFloatValueEditing.Value != null)
            {
                value = int.Parse(txtFloatValueEditing.Value.ToString());
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
                            BasicCustomFieldTypeEnum.FLOAT,
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
            return true;
            //BO.updateCustomFieldData(ObjectCustomFieldId, float.Parse(txtFloatValueEditing.Value.ToString()));
        }

        public bool CRUD_ViewingState()
        {
            CustomFieldData = BO.getCustomfiedData(session, ObjectCustomFieldId);
            return true;
        }

        public bool UpdateGUI_EdittingState()
        {
            this.txtFloatValueEditing.Visible = true;
            if (CustomFieldData.FloatValue == float.MinValue)
                this.txtFloatValueEditing.Value = null;
            else
                this.txtFloatValueEditing.Value = CustomFieldData.FloatValue;
            this.hyperlinkFloatDataViewing.Text = CustomFieldData.FloatValue.ToString();
            txtFloatValueEditing.Focus();
            this.hyperlinkFloatDataViewing.Visible = false;
            return true;
        }

        public bool UpdateGUI_ViewingState()
        {
            this.txtFloatValueEditing.Visible = false;
            this.txtFloatValueEditing.Value = CustomFieldData.FloatValue;
            if (CustomFieldData.FloatValue == float.MinValue)
                this.hyperlinkFloatDataViewing.Text = "Chỉnh sửa";
            else
                this.hyperlinkFloatDataViewing.Text = String.Format("{0:N2}", CustomFieldData.FloatValue);
            this.hyperlinkFloatDataViewing.Visible = true;
            return true;
        }

        public void ClientSetting()
        {
            cpnNASCustomFieldTypeFloatControl.ClientInstanceName = string.Format("{0}cpnNASCustomFieldTypeFloatControl", this.ClientID);
            hyperlinkFloatDataViewing.ClientInstanceName = string.Format("{0}hyperlinkFloatDataViewing", this.ClientID);
            txtFloatValueEditing.ClientInstanceName = string.Format("{0}txtFloatValueEditing", this.ClientID);
            hyperlinkFloatDataViewing.ClientSideEvents.Click = "function(s, e){ " +
                cpnNASCustomFieldTypeFloatControl.ClientInstanceName + ".PerformCallback('EDIT'); "
                + "}";

            txtFloatValueEditing.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeFloatControl.ClientInstanceName);

            txtFloatValueEditing.ClientSideEvents.Init =
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
                , cpnNASCustomFieldTypeFloatControl.ClientInstanceName);
        }

        #endregion

        public NASCustomFieldTypeFloatControl()
        {
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ClientSetting();
        }

        public void InitControlState()
        {
            GUIContext = new Context();
            GUIContext.State = new NASCustomFieldTypeFloatControlDataViewingState(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GUIContext = new NAS.GUI.Pattern.Context();
            //    GUIContext.State = new NASCustomFieldTypeFloatControlDataViewingState(this);
            //}
        }

        protected void cpnNASCustomFieldTypeFloatControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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