using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeMultiSelectionListControl.State;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeMultiSelectionListControl
{
    public partial class NASCustomFieldTypeMultiSelectionListControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {
        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

        public void InitControlState()
        {
            GUIContext = new NAS.GUI.Pattern.Context();
            GUIContext.State = new NASCustomFieldTypeMultiSelectionListControlDataViewingState(this);
        }

        private ObjectCustomFieldDataMultiSelectionListBO BO = new ObjectCustomFieldDataMultiSelectionListBO();

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

        private XPCollection<CustomFieldDataString> SelectedCustomFieldDataItems
        {
            get
            {
                return Session[this.ClientID + "SelectedCustomFieldDataItems"] as XPCollection<CustomFieldDataString>;
            }
            set
            {
                Session[this.ClientID + "SelectedCustomFieldDataItems"] = value;
            }
        }

        private Session session;

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
            CustomFieldDataAllItems = BO.GetCustomFieldAllDataItems(session, ObjectCustomFieldId);
            return true;
        }

        public bool CRUD_ViewingState()
        {
            SelectedCustomFieldDataItems = BO.GetSelecteCustomFieldAllDataItems(session, ObjectCustomFieldId);
            return true;
        }

        public bool UpdateGUI_EdittingState()
        {
            this.grdlookupItemMenu.Visible = true;
            this.grdlookupItemMenu.Focus();

            grdlookupItemMenu.DataSource = CustomFieldDataAllItems;
            grdlookupItemMenu.DataBind();

            foreach (CustomFieldDataString cfds in SelectedCustomFieldDataItems)
                grdlookupItemMenu.GridView.Selection.SetSelectionByKey(cfds.CustomFieldDataId, true);
            //CustomFieldDataId
            this.hyperlinkMultiSelectionListDataViewing.Visible = false;
            return true;
        }

        public bool UpdateGUI_ViewingState()
        {
            this.grdlookupItemMenu.Visible = false;
            if (SelectedCustomFieldDataItems != null && SelectedCustomFieldDataItems.Count > 0)
            {
                for (int i = 0; i < SelectedCustomFieldDataItems.Count; i++)
                {
                    if (i == 0)
                        this.hyperlinkMultiSelectionListDataViewing.Text = SelectedCustomFieldDataItems[i].StringValue;
                    else
                        this.hyperlinkMultiSelectionListDataViewing.Text += string.Format("; {0}", SelectedCustomFieldDataItems[i].StringValue);
                }
            }
            else
                this.hyperlinkMultiSelectionListDataViewing.Text = "Chỉnh sửa";
            this.hyperlinkMultiSelectionListDataViewing.Visible = true;
            return true;
        }

        public NASCustomFieldTypeMultiSelectionListControl()
        {

        }

        public void ClientSetting()
        {
            cpnNASCustomFieldTypeMultiSelectionListControl.ClientInstanceName = string.Format("{0}cpnNASCustomFieldTypeMultiSelectionListControl", this.ClientID);
            hyperlinkMultiSelectionListDataViewing.ClientInstanceName = string.Format("{0}hyperlinkMultiSelectionListDataViewing", this.ClientID);
            grdlookupItemMenu.ClientInstanceName = string.Format("{0}grdlookupItemMenu", this.ClientID);
            hyperlinkMultiSelectionListDataViewing.ClientSideEvents.Click = "function(s, e){ " +
                cpnNASCustomFieldTypeMultiSelectionListControl.ClientInstanceName + ".PerformCallback('EDIT'); "
                + "}";

            grdlookupItemMenu.ClientSideEvents.CloseUp =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "console.log('Cancel');"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeMultiSelectionListControl.ClientInstanceName);

        }

        protected void cpnNASCustomFieldTypeMultiSelectionListControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            if (para[0].Equals("Update"))
            {
                List<object> selectedRows = grdlookupItemMenu.GridView.GetSelectedFieldValues("CustomFieldDataId");
                List<Guid> selectedItems = new List<Guid>();
                foreach (object row in selectedRows)
                {
                    selectedItems.Add(Guid.Parse(row.ToString()));
                }

                if (BO.UpdateSelecteCustomFieldAllDataItems(ObjectCustomFieldId, selectedItems))
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
                                CustomFieldControlEventArgs.CustomFieldCategoryEnum.LIST,
                                null,
                                BasicCustomFieldTypeEnum.NONE,
                                null,
                                selectedItems));
                    }
                }
            }
            if (para[0].ToUpper().Equals("EDIT"))
            {
                if (BeforeDataEditing != null)
                {
                    BeforeDataEditing(this, new EventArgs());
                }
            }
            GUIContext.Request(para[0], this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GUIContext = new NAS.GUI.Pattern.Context();
            //    GUIContext.State = new NASCustomFieldTypeMultiSelectionListControlDataViewingState(this);
            //}
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            //ObjectCustomFieldId = Guid.Parse("23BB722D-D981-4A0F-88B8-344730E17195");
            ClientSetting();
            grdlookupItemMenu.DataSource = CustomFieldDataAllItems;
            grdlookupItemMenu.DataBind();
        }

        protected void btnApply_Init(object sender, EventArgs e)
        {
            ASPxButton btnApply = sender as ASPxButton;//btnApply = grdlookupItemMenu.GridView.FindStatusBarTemplateControl("btnApply") as ASPxButton;

            if (btnApply != null)
            {
                btnApply.ClientInstanceName = string.Format("{0}btnApply", this.ClientID);
                btnApply.ClientSideEvents.Click =
                    String.Format("function(s,e)"
                    + "{{ if(!{0}.InCallback())"
                    + "{{"
                    + "console.log('Cancel - lost focus');"
                    + "{0}.PerformCallback('Update'); {1}.HideDropDown();"
                    + "}}"
                    + "}}"
                    , cpnNASCustomFieldTypeMultiSelectionListControl.ClientInstanceName, grdlookupItemMenu.ClientInstanceName);
            }
        }

    }
}