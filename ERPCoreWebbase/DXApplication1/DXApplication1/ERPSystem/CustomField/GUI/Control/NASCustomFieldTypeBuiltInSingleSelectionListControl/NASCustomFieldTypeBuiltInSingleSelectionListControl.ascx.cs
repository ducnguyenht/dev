using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Web.ASPxGridView;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.State;
using WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.Strategy;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.ERPSystem.CustomField.GUI.Control.State;
using WebModule.ERPSystem.CustomField.GUI.Control.Data;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;
using Utility;
using System.Drawing;

namespace WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl
{
    public partial class NASCustomFieldTypeBuiltInSingleSelectionListControl : System.Web.UI.UserControl, INASCustomFieldTypeControl
    {

        private NASCustomFieldTypeBuiltInSingleSelectionListStrategy 
            NASCustomFieldTypeBuiltInSingleSelectionListStrategy
        {
            get;
            set;
        }

        public void SetNASCustomFieldTypeBuiltInSingleSelectionListStrategy(NASCustomFieldTypeBuiltInSingleSelectionListStrategy strategy)
        {
            NASCustomFieldTypeBuiltInSingleSelectionListStrategy = strategy;
            NASCustomFieldTypeBuiltInSingleSelectionListStrategy.Init(cboBuiltInSingleSelectionList);
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
            GUIContext.State = new NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState(this);
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InitClientScript();
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        private void InitClientScript()
        {
            cpnNASCustomFieldTypeBuiltInSingleSelectionListControl.ClientInstanceName =
                string.Format("cpnNASCustomFieldTypeSingleSelectionListControl_{0}", ClientID);

            hyperlinkBuiltInSingleSelectionListDataViewing.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Edit');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInSingleSelectionListControl.ClientInstanceName);

            cboBuiltInSingleSelectionList.ClientSideEvents.LostFocus =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Cancel');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInSingleSelectionListControl.ClientInstanceName);

            cboBuiltInSingleSelectionList.ClientSideEvents.ValueChanged =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Update');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInSingleSelectionListControl.ClientInstanceName);

            imgRemove.ClientSideEvents.Click =
                String.Format("function(s,e)"
                + "{{ if(!{0}.InCallback())"
                + "{{"
                + "{0}.PerformCallback('Remove');"
                + "}}"
                + "}}"
                , cpnNASCustomFieldTypeBuiltInSingleSelectionListControl.ClientInstanceName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region State Pattern

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ClientID]; }
            set { Session["GUIContext_" + ClientID] = value; }
        }

        #region UpdateGUI
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_UpdateGUI()
        {
            hyperlinkBuiltInSingleSelectionListDataViewing.Visible = false;
            cboBuiltInSingleSelectionList.Visible = true;
            cboBuiltInSingleSelectionList.Focus();
            return true;
        }
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState_UpdateGUI()
        {
            hyperlinkBuiltInSingleSelectionListDataViewing.Visible = true;
            cboBuiltInSingleSelectionList.Visible = false;
            return true;
        }
        #endregion

        #region CRUD
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_CRUD()
        {
            NASCustomFieldPredefinitionData predefinitionData = 
                NASCustomFieldTypeBuiltInSingleSelectionListStrategy.GetPredefinitionDataOfObject(ObjectCustomFieldId);
            if (predefinitionData != null)
            {
                cboBuiltInSingleSelectionList.Value = predefinitionData.RefId;
                cboBuiltInSingleSelectionList.DataBindItems();
                //imgRemove.Visible = true;
            }
            else
            {
                cboBuiltInSingleSelectionList.SelectedIndex = -1;
                //imgRemove.Visible = false;
            }
            imgRemove.Visible = false;
            return true;
        }
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState_CRUD()
        {
            NASCustomFieldPredefinitionData predefinitionData =
                NASCustomFieldTypeBuiltInSingleSelectionListStrategy.GetPredefinitionDataOfObject(ObjectCustomFieldId);
            string text = String.Empty;
            if (predefinitionData != null)
            {
                text = predefinitionData.Name == null || predefinitionData.Name.Trim().Length == 0 ?
                    predefinitionData.Code : predefinitionData.Name;
                imgRemove.Visible = true;
            }
            else
            {
                text = "Chỉnh sửa";
                imgRemove.Visible = false;
            }
            hyperlinkBuiltInSingleSelectionListDataViewing.Text = text;

            ObjectCustomField objectCustomField =
                    session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);
            //////12/22/2013 Duc.Vo----START
            /// Dump - null object khi ko co du lieu cho CustomFieldType
            ////////////////////////////////
            if (objectCustomField.CustomFieldType == null)
            {
                objectCustomField.CustomFieldType = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT.Value;
                objectCustomField.Save();
            }
            //////12/22/2013 Duc.Vo----END
            if (objectCustomField.CustomFieldType.Equals(CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER.Value))
            {
                hyperlinkBuiltInSingleSelectionListDataViewing.Font.Bold = true;
                imgRemove.Visible = false;
            }
            else if (objectCustomField.CustomFieldType.Equals(CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY.Value))
            {
                hyperlinkBuiltInSingleSelectionListDataViewing.ClientSideEvents.Click = null;
                hyperlinkBuiltInSingleSelectionListDataViewing.Cursor = "default";
                hyperlinkBuiltInSingleSelectionListDataViewing.ForeColor = Color.Gray;
                imgRemove.Visible = false;
            }
            else if (objectCustomField.CustomFieldType.Equals(CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_MASTER_READONLY.Value))
            {
                hyperlinkBuiltInSingleSelectionListDataViewing.ClientSideEvents.Click = null;
                hyperlinkBuiltInSingleSelectionListDataViewing.Cursor = "default";
                hyperlinkBuiltInSingleSelectionListDataViewing.ForeColor = Color.Gray;
                hyperlinkBuiltInSingleSelectionListDataViewing.Font.Bold = true;
                imgRemove.Visible = false;
            }

            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataEditingState_PreTransitionCRUD(string transition)
        {
            if (transition.ToUpper()
                .Equals(NASCustomFieldTypeControlStateTransition.UpdateTransition.TransitionName))
            {
                //Validate flag
                ObjectCustomField objectCustomField =
                    session.GetObjectByKey<ObjectCustomField>(ObjectCustomFieldId);
                if (!objectCustomField.CustomFieldType.Equals(CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT.Value))
                {
                    return false;
                }
                NASCustomFieldPredefinitionData selectedItem = 
                    NASCustomFieldTypeBuiltInSingleSelectionListStrategy.GetSelectedPredefinitionDataFromList(cboBuiltInSingleSelectionList);
                //Update data of custom field by ObjectCustomFieldId
                if (NASCustomFieldTypeBuiltInSingleSelectionListStrategy.UpdatePredefinitionDataForObject(ObjectCustomFieldId, selectedItem))
                {
                    if (DataUpdated != null)
                    {
                        List<NASCustomFieldPredefinitionData> builtInData = new List<NASCustomFieldPredefinitionData>();
                        builtInData.Add(selectedItem);
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
                                builtInData,
                                null));
                    }
                }
            }
            return true;
        }
        public bool NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion
        
        protected void cpnNASCustomFieldTypeBuiltInSingleSelectionListControl_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            if (command.ToUpper().Equals("REMOVE"))
            {
                NASCustomFieldTypeBuiltInSingleSelectionListStrategy
                    .UpdatePredefinitionDataForObject(ObjectCustomFieldId, null);
                GUIContext.State = new NASCustomFieldTypeBuiltInSingleSelectionListControlDataViewingState(this);
                return;
            }
            if (command.ToUpper().Equals("EDIT"))
            {
                if (BeforeDataEditing != null)
                {
                    BeforeDataEditing(this, new EventArgs());
                }
            }
            GUIContext.Request(command, this);
        }

        protected void cboBuiltInSingleSelectionList_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            NASCustomFieldTypeBuiltInSingleSelectionListStrategy.ItemRequestedByValue(session, source, e);
        }

        protected void cboBuiltInSingleSelectionList_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            NASCustomFieldTypeBuiltInSingleSelectionListStrategy.ItemsRequestedByFilterCondition(session, source, e);
        }

        protected void cboBuiltInSingleSelectionList_Init(object sender, EventArgs e)
        {
            //NASCustomFieldTypeBuiltInSingleSelectionListStrategy.Init(cboBuiltInSingleSelectionList);
        }

    }
}