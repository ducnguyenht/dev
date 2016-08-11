using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.Warehouse.Command
{
    public partial class InputSheet : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_INPUTINVENTORYCOMMAND_ID;
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
        }

        protected override void OnPreRender(EventArgs e)
        {

            uEdittingInputInventoryCommand1.SettingInit(grdInventoryCommand, "Add", "Edit", "Delete");
            base.OnPreRender(e);
        }

        protected void btnEmptyInventoryCommandRow_Init(object sender, EventArgs e)
        {
            ASPxButton btnEmptyInventoryCommandRow = sender as ASPxButton;
            uEdittingInputInventoryCommand1.SettingInit(btnEmptyInventoryCommandRow);
        }
    }
}