using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;
using NAS.DAL.CMS.ObjectDocument;
using System.ComponentModel;

namespace WebModule.ERPSystem.CustomField.GUI.Control
{
    public partial class NASCustomFieldDataGridView : System.Web.UI.UserControl
    {
        [Browsable(true)]
        public event CustomFieldControlDataUpdatedEventHandler DataUpdated;
        [Browsable(true)]
        public event CustomFieldControlBeforeDataEditingEventHandler BeforeDataEditing;

        #region Public Method
        public void DataBind()
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
            }
            isAddCustomFieldControls = false;
            //Bind data and create custom field controls
            gridviewObjectCustmoField_BindData();
        }
        #endregion

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string ViewStateControlId
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
        
        #region Public Properties
        public ASPxGridViewPagerSettings SettingsPager
        {
            get
            {
                return gridviewObjectCustmoField.SettingsPager;
            }
        }
        #endregion

        public Guid CMSObjectId
        {
            get
            {
                if (Session[ViewStateControlId + "_CMSObjectId"] == null)
                    return Guid.Empty;
                return (Guid)Session[ViewStateControlId + "_CMSObjectId"];
            }
            set
            {
                if (ViewStateControlId == null)
                    GenerateViewStateControlId();
                Session[ViewStateControlId + "_CMSObjectId"] = value;
            }
        }

        public bool isAddCustomFieldControls
        {
            get
            {
                if (Session[ViewStateControlId + "_isAddCustomFieldControls"] == null)
                    return true;
                return (bool)Session[ViewStateControlId + "_isAddCustomFieldControls"];
            }
            set
            {
                Session[ViewStateControlId + "_isAddCustomFieldControls"] = value;
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsObjectCustomField.Session = session;
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
                CMSObjectId = Guid.Empty;
                isAddCustomFieldControls = false;
            }
            gridviewObjectCustmoField_BindData();
        }

        public void customFieldControl_DataUpdated(object sender, CustomFieldControlEventArgs args)
        {
            if (DataUpdated != null)
            {
                DataUpdated(sender, args);
            }
        }

        public void customFieldControl_BeforeDataEditing(object sender, EventArgs args)
        {
            if (BeforeDataEditing != null)
            {
                BeforeDataEditing(sender, args);
            }
        }

        private void gridviewObjectCustmoField_BindData()
        {
            if (!isAddCustomFieldControls)
            {
                //Attach new custom fields
                NAS.BO.CMS.ObjectDocument.ObjectBO objectBO = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                objectBO.UpdateCustomFields(CMSObjectId);
                //Set template to load custom field controls
                DataItemTemplate = new NASCustomFieldGridViewDataItemTemplate();
                (gridviewObjectCustmoField.Columns["ObjectCustomFieldId"] as GridViewDataColumn).DataItemTemplate =
                    DataItemTemplate;
                //Set criteria for data query
                dsObjectCustomField.CriteriaParameters["ObjectId"].DefaultValue = CMSObjectId.ToString();
                dsObjectCustomField.DefaultSorting = "ObjectTypeCustomFieldId.CustomFieldId.Name";
                //Bind data to gridview
                DataItemTemplate.NASCustomFieldTypeControls.Clear();
                 gridviewObjectCustmoField.DataBind();
                //Init GUI state for all custom field controls
                foreach (var item in DataItemTemplate.NASCustomFieldTypeControls)
                {
                    item.InitControlState();
                }
                //flag to prevent data binding when postback
                isAddCustomFieldControls = true;
            }
        }

        private NASCustomFieldGridViewDataItemTemplate DataItemTemplate
        {
            get
            {
                return (NASCustomFieldGridViewDataItemTemplate)Session[ViewStateControlId + "_DataItemTemplate"];
            }
            set
            {
                Session[ViewStateControlId + "_DataItemTemplate"] = value;
            }
        }

        protected void gridviewObjectCustmoField_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataItemTemplate = new NASCustomFieldGridViewDataItemTemplate();
                //(gridviewObjectCustmoField.Columns["ObjectCustomFieldId"] as GridViewDataColumn).DataItemTemplate =
                //    DataItemTemplate;
            }
            (gridviewObjectCustmoField.Columns["ObjectCustomFieldId"] as GridViewDataColumn).DataItemTemplate =
                DataItemTemplate;
        }

        protected void gridviewObjectCustmoField_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("Refresh"))
            {
                isAddCustomFieldControls = false;
                gridviewObjectCustmoField.JSProperties["cpEvent"] = "ForceRefresh";
            }
        }

        protected void gridviewObjectCustmoField_PageIndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }

    }
}