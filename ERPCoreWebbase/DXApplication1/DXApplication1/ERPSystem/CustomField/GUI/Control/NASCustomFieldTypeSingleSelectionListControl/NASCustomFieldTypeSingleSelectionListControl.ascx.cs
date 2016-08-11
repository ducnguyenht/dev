using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeSingleSelectionListControl.State;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeSingleSelectionListControl
{
    public partial class NASCustomFieldTypeSingleSelectionListControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {
        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

        public void InitControlState()
        {
            GUIContext = new NAS.GUI.Pattern.Context();
            GUIContext.State = new NASCustomFieldTypeSingleSelectionListControlDataViewingState(this);
        }

        private ObjectCustomFieldDataSingleSelectionListBO BO = new ObjectCustomFieldDataSingleSelectionListBO();

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

        private XPCollection<CustomFieldDataString> CustomFieldDataAllItems
        {
            get
            {
                return Session[this.ClientID + "CustomFieldDataAllItems"] as XPCollection<CustomFieldDataString>;
            }
            set
            {
                Session[this.ClientID + "CustomFieldDataAllItems"] = value;
            }
        }

        private CustomFieldDataString SelectedCustomFieldData
        {
            get
            {
                return Session[this.ClientID + "SelectedCustomFieldData"] as CustomFieldDataString;
            }
            set
            {
                Session[this.ClientID + "SelectedCustomFieldData"] = value;
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

        public bool CRUD_EditingState()
        {
            CustomFieldDataAllItems = BO.GetCustomFieldAllData(session, ObjectCustomFieldId);
            return true;
        }

        public bool CRUD_ViewingState()
        {
            SelectedCustomFieldData = BO.GetCustomFielData(session, ObjectCustomFieldId);
            return true;
        }

        public bool UpdateGUI_EdittingState()
        {
            imgRemove.Visible = false;
            this.cboSingleSelectionList.Visible = true;
            if (SelectedCustomFieldData != null)
            {
                this.hyperlinkSingleSelectionListDataViewing.Text = SelectedCustomFieldData.StringValue;
                try
                {
                    cboSingleSelectionList.Value = SelectedCustomFieldData.CustomFieldDataId.ToString();
                }
                catch (Exception)
                { }
            }
            else
                this.hyperlinkSingleSelectionListDataViewing.Text = "Chỉnh sửa";
            this.cboSingleSelectionList.Focus();

            cboSingleSelectionList.DataSource = CustomFieldDataAllItems;
            cboSingleSelectionList.DataBind();

            this.hyperlinkSingleSelectionListDataViewing.Visible = false;
            return true;
        }

        public bool UpdateGUI_ViewingState()
        {
            this.cboSingleSelectionList.Visible = false;
            if (SelectedCustomFieldData != null)
            {
                this.hyperlinkSingleSelectionListDataViewing.Text = SelectedCustomFieldData.StringValue;
                imgRemove.Visible = true;
            }
            else
            {
                this.hyperlinkSingleSelectionListDataViewing.Text = "Chỉnh sửa";
                imgRemove.Visible = false;
            }
            this.hyperlinkSingleSelectionListDataViewing.Visible = true;
            return true;
        }

        public void ClientSetting()
        {
            cpnNASCustomFieldTypeSingleSelectionListControl.ClientInstanceName = string.Format("{0}cpnNASCustomFieldTypeSingleSelectionListControl", this.ClientID);
            hyperlinkSingleSelectionListDataViewing.ClientInstanceName = string.Format("{0}hyperlinkSingleSelectionListDataViewing", this.ClientID);
            cboSingleSelectionList.ClientInstanceName = string.Format("{0}cboSingleSelectionList", this.ClientID);
            hyperlinkSingleSelectionListDataViewing.ClientSideEvents.Click = "function(s, e){ " +
                cpnNASCustomFieldTypeSingleSelectionListControl.ClientInstanceName + ".PerformCallback('EDIT'); "
                + "}";

            cboSingleSelectionList.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel - lost focus');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeSingleSelectionListControl.ClientInstanceName);

            cboSingleSelectionList.ClientSideEvents.ValueChanged =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel - lost focus');"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeSingleSelectionListControl.ClientInstanceName);

            imgRemove.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Clear');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeSingleSelectionListControl.ClientInstanceName);
        }

        #endregion

        public NASCustomFieldTypeSingleSelectionListControl()
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            //ObjectCustomFieldId = Guid.Parse("23BB722D-D981-4A0F-88B8-344730E17195");
            ClientSetting();
            cboSingleSelectionList.DataSource = CustomFieldDataAllItems;
            cboSingleSelectionList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GUIContext = new NAS.GUI.Pattern.Context();
            //    GUIContext.State = new NASCustomFieldTypeSingleSelectionListControlDataViewingState(this);
            //}
        }

        protected void cpnNASCustomFieldTypeSingleSelectionListControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            if (para[0].ToUpper().Equals("CLEAR"))
            {
                BO.UpdateCustomFieldData(ObjectCustomFieldId, Guid.Empty);
                GUIContext.State = new NASCustomFieldTypeSingleSelectionListControlDataViewingState(this);
                return;
            }
            else if (para[0].ToUpper().Equals("UPDATE"))
            {
                Guid stringCustomFieldDataId = Guid.Parse(cboSingleSelectionList.Value.ToString());
                if (BO.UpdateCustomFieldData(ObjectCustomFieldId, stringCustomFieldDataId))
                {
                    if (DataUpdated != null)
                    {
                        ObjectCustomField objectCustomField =
                                session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);
                        List<Guid> IdList = new List<Guid>();
                        IdList.Add(Guid.Parse(cboSingleSelectionList.Value.ToString()));
                        //Raise DataUpdated event
                        DataUpdated(this,
                            new CustomFieldControlEventArgs(
                                ObjectCustomFieldId,
                                objectCustomField.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId,
                                objectCustomField.ObjectId.ObjectId,
                                objectCustomField.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldId,
                                CustomFieldControlEventArgs.CustomFieldCategoryEnum.LIST,
                                null,
                                BasicCustomFieldTypeEnum.NONE,
                                null,
                                IdList));
                    }
                }
            }
            else if (para[0].ToUpper().Equals("EDIT"))
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