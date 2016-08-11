using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Warehouse.Command
{
    public partial class OutputSheet : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_OUTPUTINVENTORYCOMMAND_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        private Session session;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InventoryCommandXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(),
            //"RegisterStartupScript", "<script>function TestDemo(text){alert(text);}</script>");
        }

        protected override void OnPreRender(EventArgs e)
        {
            uEdittingOutputInventoryCommand1.SettingInit(grdInventoryCommand, "Add", "Edit", "Delete");
            base.OnPreRender(e);
        }

        protected void grdInventoryCommand_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnEmptyInventoryCommandRow_Load(object sender, EventArgs e)
        {
            ASPxButton btnEmptyInventoryCommandRow = sender as ASPxButton;
            uEdittingOutputInventoryCommand1.SettingInit(btnEmptyInventoryCommandRow);
        }
    }
}