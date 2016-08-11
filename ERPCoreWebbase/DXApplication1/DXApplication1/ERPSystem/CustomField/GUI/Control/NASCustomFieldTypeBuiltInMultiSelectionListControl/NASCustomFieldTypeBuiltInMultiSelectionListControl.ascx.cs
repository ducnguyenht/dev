using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.Strategy;
using DevExpress.Web.ASPxGridView;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.State;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using WebModule.ERPSystem.CustomField.GUI.Control.State;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl
{
    public partial class NASCustomFieldTypeBuiltInMultiSelectionListControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {
        private NASCustomFieldTypeBuiltInMultiSelectionListStrategy
            NASCustomFieldTypeBuiltInMultiSelectionListStrategy
        {
            get;
            set;
        }

        public void SetNASCustomFieldTypeBuiltInMultiSelectionListStrategy
            (NASCustomFieldTypeBuiltInMultiSelectionListStrategy strategy)
        {
            NASCustomFieldTypeBuiltInMultiSelectionListStrategy = strategy;
            NASCustomFieldTypeBuiltInMultiSelectionListStrategy.InitXpoDatasource(datasource);
            NASCustomFieldTypeBuiltInMultiSelectionListStrategy.InitGridLookup(grdlookupItemMenu);
        }

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
            GUIContext.State = new NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState(this);
        }

        private void InitClientScript()
        {
            cpnNASCustomFieldTypeBuiltInMultiSelectionListControl.ClientInstanceName =
                String.Format("{0}_cpnNASCustomFieldTypeMultiSelectionListControl", ClientID);

            grdlookupItemMenu.ClientInstanceName = string.Format("{0}_grdlookupItemMenu", ClientID);

            hyperlinkBuiltInMultiSelectionListDataViewing.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Edit');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInMultiSelectionListControl.ClientInstanceName);

            grdlookupItemMenu.ClientSideEvents.CloseUp =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInMultiSelectionListControl.ClientInstanceName);
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            datasource.Session = session;
            InitClientScript();
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (NASCustomFieldTypeBuiltInMultiSelectionListStrategy != null)
            {
                NASCustomFieldTypeBuiltInMultiSelectionListStrategy.InitXpoDatasource(datasource);
                NASCustomFieldTypeBuiltInMultiSelectionListStrategy.InitGridLookup(grdlookupItemMenu);
            }
        }

        #region State Pattern
        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ClientID]; }
            set { Session["GUIContext_" + ClientID] = value; }
        }

        #region UpdateGUI
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataEditingState_UpdateGUI()
        {
            hyperlinkBuiltInMultiSelectionListDataViewing.Visible = false;
            grdlookupItemMenu.Visible = true;
            grdlookupItemMenu.Focus();
            return true;
        }
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_UpdateGUI()
        {
            hyperlinkBuiltInMultiSelectionListDataViewing.Visible = true;
            grdlookupItemMenu.Visible = false;
            return true;
        }
        #endregion

        #region CRUD
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataEditingState_CRUD()
        {
            List<NASCustomFieldPredefinitionData> data =
                   NASCustomFieldTypeBuiltInMultiSelectionListStrategy.GetPredefinitionDataOfObject(ObjectCustomFieldId);
            grdlookupItemMenu.GridView.Selection.UnselectAll();
            if (data != null)
            {
                foreach (var item in data)
                {
                    grdlookupItemMenu.GridView.Selection.SetSelectionByKey(item.RefId, true);
                }
            }
            return true;
        }
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_CRUD()
        {
            List<NASCustomFieldPredefinitionData> data =
                NASCustomFieldTypeBuiltInMultiSelectionListStrategy.GetPredefinitionDataOfObject(ObjectCustomFieldId);
            string displayText = String.Empty;
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    string tempText = item.Name == null || item.Name.Trim().Length == 0 ?
                            item.Code : item.Name;
                    if (data.IndexOf(item) == 0)
                    {
                        displayText += tempText;
                    }
                    else
                    {
                        displayText += String.Format("; {0}", tempText);
                    }
                }
            }
            else
            {
                displayText = "Chỉnh sửa";
            }
            hyperlinkBuiltInMultiSelectionListDataViewing.Text = displayText;
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataEditingState_PreTransitionCRUD(string transition)
        {
            if (transition.ToUpper()
                .Equals(NASCustomFieldTypeControlStateTransition.UpdateTransition.TransitionName))
            {
                List<NASCustomFieldPredefinitionData> data =
                    NASCustomFieldTypeBuiltInMultiSelectionListStrategy.GetSelectedPredefinitionDataFromList(grdlookupItemMenu);
                //Update data of custom field by ObjectCustomFieldId
                if (NASCustomFieldTypeBuiltInMultiSelectionListStrategy.UpdatePredefinitionDataForObject(ObjectCustomFieldId, data))
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
                                CustomFieldControlEventArgs.CustomFieldCategoryEnum.BUILT_IN,
                                null,
                                BasicCustomFieldTypeEnum.NONE,
                                data,
                                null));
                    }
                }
            }
            return true;
        }
        public bool NASCustomFieldTypeBuiltInMultiSelectionListControlDataViewingState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        protected void cpnNASCustomFieldTypeBuiltInMultiSelectionListControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected void btnApply_Init(object sender, EventArgs e)
        {
            ASPxButton btnApply = sender as ASPxButton;
            if (btnApply != null)
            {
                btnApply.ClientInstanceName = string.Format("{0}_btnApply", this.ClientID);
                btnApply.ClientSideEvents.Click =
                    String.Format("function(s,e)"
                    + "{{ if(!{0}.InCallback())"
                    + "{{"
                    + "{0}.PerformCallback('Update'); {1}.HideDropDown();"
                    + "}}"
                    + "}}"
                    , cpnNASCustomFieldTypeBuiltInMultiSelectionListControl.ClientInstanceName, grdlookupItemMenu.ClientInstanceName);
            }
        }
    }
}