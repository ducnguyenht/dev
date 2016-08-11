using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;

namespace WebModule.ERPSystem.CustomField.GUI
{
    public partial class ObjectTypeListing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsObjectType.Session = session;
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
                GUIContext = new NAS.GUI.Pattern.Context();
                GUIContext.State = new State.ObjectTypeListing.ObjectTypeListing(this);
            }
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
            //get { return (NAS.GUI.Pattern.Context)HttpContext.Current.Items["GUIContext_ObjectTypeListing"]; }
            //set { HttpContext.Current.Items["GUIContext_ObjectTypeListing"] = value; }
        }


        #region UpdateGUI

        public bool ObjectTypeListing_UpdateGUI()
        {
            return true;
        }

        #endregion


        #region CRUD

        public bool StateName_CRUD()
        {
            return true;
        }

        #endregion


        #region PreTransitionCRUD

        public bool StateName_PreTransitionCRUD(string transition)
        {
            return true;
        }

        #endregion


        #endregion

    }
}