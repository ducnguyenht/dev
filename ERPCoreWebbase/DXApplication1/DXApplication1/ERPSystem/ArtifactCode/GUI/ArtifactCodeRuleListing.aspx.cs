using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using NAS.BO.System.ArtifactCode;

namespace WebModule.ERPSystem.ArtifactCode.GUI
{
    public partial class ArtifactCodeRuleListing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Utility.Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Utility.Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utility.Utils.ApplyTheme(this);
        }

        private Session session;
        private ArtifactCodeRuleBO artifactCodeRuleBO; 

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsArtifactCodeRule.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            artifactCodeRuleBO = new ArtifactCodeRuleBO();
            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
            }
            //Context only has ArtifactCodeRuleListing state
            GUIContext.State =
                    new ERPSystem.ArtifactCode.State.ArtifactCodeRuleListing.ArtifactCodeRuleListing(this);
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
        #endregion


        #region CRUD

        public bool ArtifactCodeRuleListingCRUD()
        {
            dsArtifactCodeRule.CriteriaParameters["OrganizationId"].DefaultValue = 
                                    Utility.CurrentSession.Instance.AccessingOrganizationId.ToString();
            return true;
        }

        #endregion

        protected void gridArtifactCodeRule_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] args = e.Parameters.Split('|');
            string command = args[0];
            if (command.Equals("Delete"))
            {
                bool isSuccess = false;
                try
                {
                    if (args.Length < 2)
                    {
                        throw new Exception("Must be pass a key for deleting");
                    }
                    Guid artifactCodeRuleId = Guid.Parse(args[1]);
                    artifactCodeRuleBO.Delete(session, artifactCodeRuleId);
                    isSuccess = true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (isSuccess)
                    {
                        gridArtifactCodeRule.JSProperties["cpEvent"] = "deleted";
                    }
                }
            }
        }


        #region PreTransitionCRUD
        #endregion


        #endregion
       

    }
}