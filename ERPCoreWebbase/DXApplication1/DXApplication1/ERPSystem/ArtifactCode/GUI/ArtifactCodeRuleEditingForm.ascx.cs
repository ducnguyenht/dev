using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.GUI.Pattern;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxFormLayout;
using NAS.DAL.System.ArtifactCode;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxEditors;
using NAS.BO.System.ArtifactCode;

namespace WebModule.ERPSystem.ArtifactCode.GUI
{
    public partial class ArtifactCodeRuleEditingForm : System.Web.UI.UserControl
    {

        private Session session;
        private CodeRuleDefinitionBO codeRuleDefinitionBO;
        private ArtifactCodeRuleBO artifactCodeRuleBO;

        private List<NAS.BO.System.ArtifactCode.CodeRuleDefinitionBO.CodeRuleDefinitionView> CodeRuleDefinitionViewData
        {
            get
            {
                return (List<NAS.BO.System.ArtifactCode.CodeRuleDefinitionBO.CodeRuleDefinitionView>)
                        Session["CodeRuleDefinitionViewData"];
            }
            set
            {
                Session["CodeRuleDefinitionViewData"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsArtifactCodeRule.Session = session;
            dsArtifactType.Session = session;
        }

        public Guid ArtifactCodeRuleId
        {
            get { return (Guid)Session["ArtifactCodeRuleId"]; }
            set { Session["ArtifactCodeRuleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            codeRuleDefinitionBO = new CodeRuleDefinitionBO();
            artifactCodeRuleBO = new ArtifactCodeRuleBO();
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                CodeRuleDefinitionViewData = null;
                GUIContext = new Context();
                ArtifactCodeRuleId = Guid.Empty;
            }
            BindArtifactTypeData();
            formlayoutArtifactCodeRuleEditingForm.DataBind();
            treelistCodeRuleData.DataSource = CodeRuleDefinitionViewData;
            treelistCodeRuleData.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
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
        }

        #region UpdateGUI

        public bool ArtifactCodeRuleCreating_UpdateGUI()
        {
            ClearForm();
            popupArtifactCodeRuleEditingForm.ShowOnPageLoad = true;
            popupArtifactCodeRuleEditingForm.HeaderText = "Phương pháp tạo mã - Thêm mới";
            //treelistCodeRuleData.Enabled = false;

            return true;
        }

        public bool ArtifactCodeRuleEditing_UpdateGUI()
        {
            ClearForm();
            popupArtifactCodeRuleEditingForm.ShowOnPageLoad = true;
            popupArtifactCodeRuleEditingForm.HeaderText =
               String.Format("Phương pháp tạo mã - {0}", CurrentArtifactCodeRule().Name);
            //treelistCodeRuleData.Enabled = true;
            treelistCodeRuleData.ExpandAll();

            return true;
        }

        public bool ArtifactCodeRuleCanceling_UpdateGUI()
        {
            popupArtifactCodeRuleEditingForm.ShowOnPageLoad = false;
            return true;
        }

        #endregion


        #region CRUD

        public bool ArtifactCodeRuleCreating_CRUD()
        {

            formlayoutArtifactCodeRuleEditingForm.DataSourceID = null;
            formlayoutArtifactCodeRuleEditingForm.DataBind();

            CodeRuleDefinitionViewData = new List<CodeRuleDefinitionBO.CodeRuleDefinitionView>();
            treelistCodeRuleData.DataSource = CodeRuleDefinitionViewData;
            treelistCodeRuleData.DataBind();
            
            BindArtifactTypeData();

            return true;
        }

        public bool ArtifactCodeRuleEditing_CRUD()
        {

            formlayoutArtifactCodeRuleEditingForm.DataSourceID = dsArtifactCodeRule.ID;
            dsArtifactCodeRule.CriteriaParameters["ArtifactCodeRuleId"].DefaultValue =
                                                    ArtifactCodeRuleId.ToString();

            //Bind data to treelist
            CodeRuleDefinitionViewData = codeRuleDefinitionBO.GetCodeRuleDefinitionView(ArtifactCodeRuleId);
            treelistCodeRuleData.DataSource = CodeRuleDefinitionViewData;
            treelistCodeRuleData.DataBind();

            BindArtifactTypeData();

            txtExample.Text = artifactCodeRuleBO.GetArtifactCode(ArtifactCodeRuleId, false);

            return true;
        }

        #endregion


        #region PreTransitionCRUD

        public bool ArtifactCodeRuleCreating_PreTransitionCRUD(string transition)
        {
            try
            {
                switch (transition)
                {
                    case "Save":

                        Guid artifactTypeId = Guid.Parse(cbbArtifactType.SelectedItem.Value.ToString());
                        string name = txtArtifactCodeRuleName.Text;
                        string description = txtArtifactCodeRuleDescription.Text;

                        ArtifactType artifactType = session.GetObjectByKey<ArtifactType>(artifactTypeId);

                        ArtifactCodeRule newArtifactCodeRule = new ArtifactCodeRule(session)
                        {
                            ArtifactCodeRuleId = Guid.NewGuid(),
                            ArtifactTypeId = artifactType,
                            CreateDate = DateTime.Now,
                            Description = description,
                            IssueDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now,
                            Name = name,
                            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                        };

                        newArtifactCodeRule.Save();

                        ArtifactCodeRuleId = newArtifactCodeRule.ArtifactCodeRuleId;

                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ArtifactCodeRuleEditing_PreTransitionCRUD(string transition)
        {
            try
            {
                switch (transition)
                {
                    case "Save":

                        Guid artifactTypeId = Guid.Parse(cbbArtifactType.SelectedItem.Value.ToString());
                        string name = txtArtifactCodeRuleName.Text;
                        string description = txtArtifactCodeRuleDescription.Text;

                        ArtifactType artifactType = session.GetObjectByKey<ArtifactType>(artifactTypeId);
                        Guid artifactCodeRuleId = ArtifactCodeRuleId;

                        ArtifactCodeRule artifactCodeRule =
                            session.GetObjectByKey<ArtifactCodeRule>(artifactCodeRuleId);
                        artifactCodeRule.ArtifactTypeId = artifactType;
                        artifactCodeRule.Description = description;
                        artifactCodeRule.LastUpdateDate = DateTime.Now;
                        artifactCodeRule.Name = name;
                        artifactCodeRule.Save();

                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        #endregion

        public void ClearForm()
        {
            treelistCodeRuleData.CancelEdit();
            ASPxEdit.ClearEditorsInContainer(formlayoutArtifactCodeRuleEditingForm);
            cbbArtifactType.IsValid = true;
            txtArtifactCodeRuleName.IsValid = true;
        }

        private ArtifactCodeRule CurrentArtifactCodeRule()
        {
            if (ArtifactCodeRuleId == null)
            {
                return null;
            }
            else
            {
                Guid objectId = ArtifactCodeRuleId;
                if (objectId.Equals(Guid.Empty))
                {
                    return null;
                }
                else
                {
                    return session.GetObjectByKey<ArtifactCodeRule>(objectId);
                }
            }
        }

        public void BindArtifactTypeData()
        {
            dsArtifactType.CriteriaParameters["OrganizationId"].DefaultValue =
                            Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();
            cbbArtifactType.DataBind();
        }

        protected void cpnArtifactCodeRuleEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string transition = null;
            bool isSuccess = false;
            try
            {
                string[] args = e.Parameter.Split('|');
                transition = args[0];
                if (transition.Equals("Refresh"))
                {
                    GUIContext.State =
                        new ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm.ArtifactCodeRuleEditing(this);
                }
                else if (transition.Equals("Edit"))
                {
                    if (args.Length > 1)
                    {
                        ArtifactCodeRuleId = Guid.Parse(args[1]);
                    }
                    else
                    {
                        throw new Exception();
                    }
                    GUIContext.State =
                        new ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm.ArtifactCodeRuleEditing(this);

                }
                else if (transition.Equals("Create"))
                {
                    ArtifactCodeRuleId = Guid.Empty;
                    GUIContext.State =
                        new ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm.ArtifactCodeRuleCreating(this);
                }
                else
                {
                    //Request change state
                    GUIContext.Request(transition, this);
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (transition != null)
                {
                    cpnArtifactCodeRuleEditingForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            transition, isSuccess.ToString().ToLower()));
                }
            }

        }

        protected void treelistCodeRuleData_InitNewNode(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

            if(GUIContext.State is ERPSystem.ArtifactCode.State.ArtifactCodeRuleForm.ArtifactCodeRuleCreating)
                throw new Exception("Phải lưu thông tin chung trước");

            Guid parentCodeRuleDefinitionId;
            CodeRuleDefinition parentCodeRuleDefinition;
            if (!Guid.TryParse(treelistCodeRuleData.NewNodeParentKey, out parentCodeRuleDefinitionId))
            {
                parentCodeRuleDefinitionId = Guid.Empty;
                parentCodeRuleDefinition = null;
            }
            else
            {
                parentCodeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(parentCodeRuleDefinitionId);
            }

            if (codeRuleDefinitionBO.isInsertingCodeRuleDefinitionValid(session, ArtifactCodeRuleId, parentCodeRuleDefinition))
            {
                treelistCodeRuleData.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"command\": \"{0}\", \"nodekey\": \"{1}\" }}",
                                                            "Create", parentCodeRuleDefinitionId.ToString()));
                treelistCodeRuleData.CancelEdit();
            }
            else
            {
                throw new Exception("Không thể chèn thêm định nghĩa ở vị trí này");
            }
        }

        protected void treelistCodeRuleData_StartNodeEditing(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeEditingEventArgs e)
        {
            treelistCodeRuleData.JSProperties.Add("cpCallbackArgs",
                            String.Format("{{ \"command\": \"{0}\", \"nodekey\": \"{1}\" }}",
                                                    "Edit", e.NodeKey));

            e.Cancel = true;
            treelistCodeRuleData.CancelEdit();
        }

        protected void treelistCodeRuleData_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            bool isSuccess = false;
            try
            {
                Guid codeRuleDefinitionId = (Guid)e.Keys["CodeRuleDefinitionId"];
                codeRuleDefinitionBO.Delete(session, codeRuleDefinitionId);
                isSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                if(isSuccess) 
                {
                    treelistCodeRuleData.JSProperties["cpEvent"] = "deleted";
                }
            }
        }

    }
}