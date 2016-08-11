using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
//using DAL.NASID;

namespace DXApplication1.GUI
{
    public partial class chitiet_user : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {
                new {Phone = "01689939518", Type = "Di động"},
                new {Phone = "087766388"  , Type = "Nhà"},
                new {Phone = "083939393"  , Type = "Công ty"}
                };
            ASPxGridView1.DataBind();

            ASPxGridView2.DataSource = new[] {
                new {Email = "vnhatduc@yahoo.com.vn" , Server = "Yahoo"},
                new {Email = "ducvn256@gmail.com.vn" , Server = "Google"},
                new {Email = "vnhatduc@hotmail.com"  , Server = "Hotmail"}
                };
            ASPxGridView2.DataBind();
            lbl_hoten.Text = "Võ Nhật Đức";
            lbl_email.Text = "vnhatduc@yahoo.com.vn";
            lbl_tochuc.Text = "Phòng nhân sự";
            lbl_diachi.Text = "84/90D Tân Sơn Nhì, Q.Tân Phú, TP.HCM";
        }
    }
}