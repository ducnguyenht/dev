using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;

namespace WebModule.Warehouse
{
    public partial class ListInputComm : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_INPUTCOMM_ID;
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

            ImportCommandXDS.Session = session;
            ImportCommandXDS.Criteria = "[RowStatus] >= 0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (grdData.FocusedRowIndex > 0)
            {
               // Session["InventoryTransactionId"] = grdData.GetRowValues(grdData.FocusedRowIndex, "InventoryTransactionId");
            }
        }



       

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.JSProperties.Add("cpEdit", "edit");
            grdData.CancelEdit();           
        }

        protected void grdData_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            
        }

       
    }
}