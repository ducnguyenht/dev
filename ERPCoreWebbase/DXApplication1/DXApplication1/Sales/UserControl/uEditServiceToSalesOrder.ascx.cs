using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uEditServiceToSalesOrder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cbo_dsdichvu.DataSource = new[] { 
                    new {serviceid = "DV00001", servicename = "Dịch vụ 1", servicegrpid = "Nhóm dịch vụ 1"},
                    new {serviceid = "DV00002", servicename = "Dịch vụ 2", servicegrpid = "Nhóm dịch vụ 2"},
                    new {serviceid = "DV00003", servicename = "Dịch vụ 3", servicegrpid = "Nhóm dịch vụ 2"},
                    new {serviceid = "DV00004", servicename = "Dịch vụ 4", servicegrpid = "Nhóm dịch vụ 3"},
                    new {serviceid = "DV00005", servicename = "Dịch vụ 5", servicegrpid = "Nhóm dịch vụ 4"},
                };
            cbo_dsdichvu.DataBind();
            cbo_dsdichvu.SelectedIndex = 0;

            lbl_servicename.Text = "Dịch vụ 1";
            lbl_unit.Text = "Lần";
            txt_number.Text = "100";
            lbl_priceunit.Text = "20.000";    
        }
    }
}