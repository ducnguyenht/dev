using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Collections.ObjectModel;

namespace WebModule.GUI.usercontrol
{
    public partial class uCreateConditionToReceiveReturnProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_responsibility.DataSource = new[] { 
                new{ productid = "SP00001", 
                    productname = "Hàng hóa 1", 
                    productgrpid = "Dược phẩm Châu Á",
                    manufacturergrpid = "Nhóm miền Nam",
                    manufacturerpid = "Cty dược Cần Thơ",
                    suppliergrppid = "Nhóm miền Trung",
                    supplierpid = "Nhà phân phối Minh Châu",
                    productunit = "Hộp", 
                    reponsibility = "Không được đổi lại sau 30 ngày", 
                    description = ""},
                new{ productid = "SP00002", 
                    productname = "Hàng hóa 2", 
                    productgrpid = "Dược phẩm Châu Á",
                    manufacturergrpid = "Nhóm miền Nam",
                    manufacturerpid = "Cty dược Cần Thơ",
                    suppliergrppid = "Nhóm miền Nam",
                    supplierpid = "Nhà phân phối Minh Châu",
                    productunit = "Hộp", 
                    reponsibility = "Không được đổi lại sau 10 ngày", 
                    description = ""},
                new{ productid = "SP00003", 
                    productname = "Hàng hóa 3", 
                    productgrpid = "Dược phẩm Châu Âu",
                    manufacturergrpid = "Nhóm miền Nam",
                    manufacturerpid = "Cty dược Hồ Chí Minh",
                    suppliergrppid = "Nhóm miền Trung",
                    supplierpid = "Nhà phân phối Minh Châu",
                    productunit = "Thùng", 
                    reponsibility = "Không được đổi lại sau 20 ngày", 
                    description = ""},
            };

            grv_responsibility.DataBind();
        }

        protected void grv_responsibility_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Group)
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxLabel groupname = grid.FindGroupRowTemplateControl(e.VisibleIndex, "lbl_groupname") as ASPxLabel;
                ReadOnlyCollection<GridViewDataColumn> groupcollection = grid.GetGroupedColumns();
                foreach (GridViewDataColumn column in groupcollection)
                {
                    groupname.Text = column.Caption + ": " + grid.GetRowValues(e.VisibleIndex, column.FieldName);
                }

                ASPxButton button = new ASPxButton();
            }
        }

        protected void grv_responsibility_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grv_responsibility.BeginUpdate();
            try
            {
                grv_responsibility.ClearSort();
                switch (e.Parameters)
                {
                    case "productgrpid":
                        grv_responsibility.GroupBy(grv_responsibility.Columns["productgrpid"]);
                        break;
                    case "manufacturergrpid":
                        grv_responsibility.GroupBy(grv_responsibility.Columns["manufacturergrpid"]);
                        break;
                    case "suppliergrppid":
                        grv_responsibility.GroupBy(grv_responsibility.Columns["suppliergrppid"]);
                        break;
                }
            }
            finally
            {
                grv_responsibility.EndUpdate();
            }
            grv_responsibility.ExpandAll();
        }

    }
}