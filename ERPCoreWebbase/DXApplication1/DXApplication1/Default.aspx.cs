using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;

namespace WebModule.GUI
{
    public partial class Default : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_DEFAULT_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_DEFAULT_ID;
            } 
        }
        //Session session;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //session = XpoHelper.GetNewSession();
            Utils.ApplyTheme(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            ////NAS.DAL.Util.Populate();
            //string code = artifactCodeRuleBO.GetArtifactCode(Guid.Parse("b510f967-9d3d-4f0c-a930-c4d6f06d8c42"));

            //using (Session session = XpoHelper.GetNewSession())
            //{
            //    CustomField customField = session.FindObject<CustomField>(new BinaryOperator("Code",
            //        "PURCHASING_INVOICE"));
            //    customField.Delete();

            //    customField = session.FindObject<CustomField>(new BinaryOperator("Code",
            //        "OBJECT"));
            //    customField.Delete();

            //}
        }
    }
}