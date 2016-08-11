using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using NAS.DAL;
using DevExpress.Xpo;

namespace WebModule.Warehouse
{
    public partial class ListOutputComm : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_OUTPUTCOMM_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            SalesInvoiceInventoryTransactionXDS.Session = session;
            SalesInvoiceInventoryTransactionXDS.Criteria = "[RowStatus] >= 0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {           
        }
    }
}