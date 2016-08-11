using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Utility;

namespace DXApplication1.GUI
{
    public partial class ModifyPrice : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grvModifyingPrice.DataSource = new[] {  new {id = "HC0001", createDate = "01/06/2013", ht = "01/06/2013",hd = "01/07/2013",diengiai = "Ví dụ 1" },
                                                new {id = "HC0002", createDate = "01/07/2013", ht = "01/07/2013",hd = "01/08/2013",diengiai = "Ví dụ 2" },
                                                new {id = "HC0003", createDate = "01/08/2013", ht = "01/08/2013",hd = "01/09/2013",diengiai = "Ví dụ 3" }};
            grvModifyingPrice.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_MODIFYPOLICY_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
    }
}