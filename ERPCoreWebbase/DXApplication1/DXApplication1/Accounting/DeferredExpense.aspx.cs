using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Accounting
{
    public partial class DeferredExpense : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingEntrylst1.Visible = false;
            btn_save.Visible = false;
            btn_cancel.Visible = false;
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_DEFFERED_EXP_PROCESS_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void btn_ketchuyen_Click(object sender, EventArgs e)
        {
            AccountingEntrylst1.Visible = true;
            btn_save.Visible = true;
            btn_cancel.Visible = true;
            btn_ketchuyen.Enabled = false;
        }
    }
}