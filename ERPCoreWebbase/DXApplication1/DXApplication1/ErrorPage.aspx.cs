using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebModule
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Error"] == null)
            {
                string errorCode = Request.QueryString["Error"].ToString();
                if (errorCode.Equals("403"))
                {
                    ASPxLabel1.Text = "Chúng tôi rất tiết vì sự bất tiện này! <br/> Bạn không có quyền truy xuất vào trang này.";
                }
                else if (errorCode.Equals("401"))
                {
                    ASPxLabel1.Text = "Chúng tôi rất tiết vì sự bất tiện này! <br/> Bạn không có quyền truy xuất vào trang này.";
                }
                else if (errorCode.Equals("500"))
                {
                    ASPxLabel1.Text = "Chúng tôi rất tiết vì sự bất tiện này! <br/> Hiện tại máy chủ đang có sự cố. Xin vui lòng thử lại sau vài phút.";
                }
                else
                {
                    Response.Redirect(FormsAuthentication.DefaultUrl);
                }
            }
            else
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }
    }
}