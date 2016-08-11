using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

namespace ERPCore.Accounting
{
    public partial class BookAccount : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
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

        private class datasample
        {
            public int Stt { get; set; }            
            public string Name { get; set; }
            public string Description { get; set; }            
            public string Type { get; set; }
            public string Account { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
           
            data.Add(new datasample() { Stt = 1, Account = "Ví dụ 1", Description = "Ví dụ 1", Type = "Mua hàng", Name = "Mua hàng 1"});
            data.Add(new datasample() { Stt = 2, Account = "Ví dụ 2", Description = "Ví dụ 2", Type = "Mua hàng", Name = "Mua hàng 2" });
            data.Add(new datasample() { Stt = 3, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Bán hàng", Name = "Bán hàng 1" });
            data.Add(new datasample() { Stt = 4, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Thu", Name = "Thu 1" });
            data.Add(new datasample() { Stt = 5, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Chi", Name = "Chi 1" });
            data.Add(new datasample() { Stt = 6, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Ủy Nhiệm Thu", Name = "Ủy Nhiệm Thu 1" });
            data.Add(new datasample() { Stt = 7, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Ủy Nhiệm Chi", Name = "Ủy Nhiệm Chi 1" });
            data.Add(new datasample() { Stt = 8, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Xuất Kho", Name = "Phiếu Xuất Kho" });
            data.Add(new datasample() { Stt = 9, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Nhập Kho", Name = "Phiếu Nhập Kho" });
            data.Add(new datasample() { Stt =10, Account = "Ví dụ 3", Description = "Ví dụ 3", Type = "Chung", Name = "Sơ đồ chung 1" });

            grdData.DataSource = data;
            grdData.DataBind();

           
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

            String ss = grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Type").ToString();
            if (grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Type").ToString().Equals("Mua hàng") ||
                    grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Type").ToString().Equals("Bán hàng"))
            {
                grdData.JSProperties.Add("cpEdit", "1");
                grdData.JSProperties.Add("cpTypeName",grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Name").ToString());
            }
            else if (grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Type").ToString().Equals("Chung"))
            {
                grdData.JSProperties.Add("cpTypeName",grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Name").ToString());
                grdData.JSProperties.Add("cpEdit", "3");
            }
            else
            {
                grdData.JSProperties.Add("cpEdit", "2");
                grdData.JSProperties.Add("cpTypeName", grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Type").ToString() + "%%" + grdData.GetRowValues(grdData.EditingRowVisibleIndex, "Name").ToString());
            }

            e.Cancel = true;
            grdData.CancelEdit();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //grdData.CancelEdit();
            //grdData.JSProperties.Add("cpEdit", "");
        }
    }
}