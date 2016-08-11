using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uEdit_ProductToSalesOrder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                cbo_dshanghoa.DataSource = new[] { 
                    new {productid = "SP00001", productname = "Hàng hóa 1", productgrpid = "Nhóm đặc trị 1"},
                    new {productid = "SP00002", productname = "Hàng hóa 2", productgrpid = "Nhóm đặc trị 2"},
                    new {productid = "SP00003", productname = "Hàng hóa 3", productgrpid = "Nhóm đặc trị 2"},
                    new {productid = "SP00004", productname = "Hàng hóa 4", productgrpid = "Nhóm đặc trị 3"},
                    new {productid = "SP00005", productname = "Hàng hóa 5", productgrpid = "Nhóm đặc trị 4"},
                };
                cbo_dshanghoa.DataBind();
                cbo_dshanghoa.SelectedIndex = 0;

                cbo_dslot.DataSource = new[] { 
                    new{lotid = "SL0001", lotname = "Lô hàng 1"},
                    new{lotid = "SL0002", lotname = "Lô hàng 2"},
                    new{lotid = "SL0003", lotname = "Lô hàng 3"}
                };
                cbo_dslot.DataBind();
                cbo_dslot.SelectedIndex = 0;

                lbl_productname.Text = "Hàng hóa 1";
                lbl_unit.Text = "Hộp";
                txt_number.Text = "100";
                lbl_priceunit.Text = "20.000";
                lbl_duedate.Text = "01/08/2014";       
        }
    }
}