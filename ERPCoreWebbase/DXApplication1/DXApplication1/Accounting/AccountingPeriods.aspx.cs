using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using Utility;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;

namespace WebModule.Accounting
{
    public partial class AccountingPeriods : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        #region *
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}