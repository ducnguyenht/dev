using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class ListInputComm : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
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
                return Constant.ACCESSOBJECT_IMEXPORT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            grdData.DataSource =
                new[] { 
                     new { key="123", code = "HDP001", date = "25/07/2013 10:35:40", name="Nhân viên A", status = "Đã nhập kho", type="Phiếu mua hàng"
                             
                    },
                    new { key="124", code = "HDP002", date = "02/08/2013 09:35:40", name="Nhân viên B", status = "Chưa nhập kho", type="Chuyển kho nội bộ"
                    },
                     new { key="125", code = "HDP003", date = "29/08/2013 09:35:40", name="Nguyễn Văn A", status = "Yêu cầu điều chỉnh", type="Điều chỉnh tồn kho"
                    }
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

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
            //switch (e.Parameter)
            //{
            //    case "refresh":
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}