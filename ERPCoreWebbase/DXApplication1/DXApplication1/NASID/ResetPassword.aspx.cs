using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
//using BLL.NASID;
using System.Web.Security;

namespace DXApplication1.NASID
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        string userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string link = HttpContext.Current.Request.Url.AbsoluteUri;//lay url
                string[] list = link.Split('?'); // tach chuoi de lay ma md5
                string prameterInfo = Utility.Security.DecryptString(list[1], "123");
                list = prameterInfo.Split('&');
                userId = list[0].Split('=')[1];
                DateTime timeCreateLink = Convert.ToDateTime(list[1].Split('=')[1]);
                timeCreateLink = timeCreateLink.AddDays(1);
                if (timeCreateLink.Ticks < DateTime.Now.Ticks)
                {
                    Response.Redirect("~/");//direct page link expire
                }
                if (IsPostBack && captcha.IsValid && ASPxEdit.ValidateEditorsInContainer(this))
                {
                    //UserBLO userBLO = new UserBLO();
                    //if (userBLO.ResetPassword(Guid.Parse(userId),
                    //    FormsAuthentication.HashPasswordForStoringInConfigFile(passwordtb.Text, "MD5")))
                    //{
                
                    //    //Direct page reset successful
                    //}
                    //else
                    //{
                    //    //Direct page reset not successful
                    //}
                }
            }
            catch 
            {
            }
        }
    }
}