using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;

namespace WebModule.Purchasing
{
    public partial class ItemUnit : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        protected void Page_Load(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ProductUnitXDS.Session = session;
            ProductUnitXDS.Criteria = (new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)).ToString();
            grdUnit.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_ITEMUNIT; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_GROUPID; }
        }
    }
}