using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using DAL.NASID;
namespace DXApplication1.NASID
{
    public partial class Login : System.Web.UI.Page
    {
        //private BLL.NASID.UserBLO userBLO;
        protected void Page_Init(object sender, EventArgs e)
        {
            //userBLO = new BLL.NASID.UserBLO();
        } 
        protected void Page_Load(object sender, EventArgs e)
        { 
            //if (Membership.ValidateUser(tbemail.Text, tbPassword.Text)) {
            //    if(string.IsNullOrEmpty(Request.QueryString["ReturnUrl"])) {
            //        FormsAuthentication.SetAuthCookie(tbemail.Text, false);
            //        Response.Redirect("~/");
            //    }
            //    else
            //        FormsAuthentication.RedirectFromLoginPage(tbemail.Text, false);
            //}
            //else {
            //    tbemail.ErrorText = "Invalid user";
            //    tbemail.IsValid = false;
            //}
        }

        protected void btsubmit_Click(object sender, EventArgs e)
        {
            if (captcha.IsValid)
            {
                //string userEmail = tbemail.Text;
                //string userPassword = tbPassword.Text;
                //User user = this.userBLO.CheckExistByEmail(userEmail);
                //if (user != null)
                //{
                //    tbemail.IsValid = true;
                //    string userPasswordMD5 = FormsAuthentication.HashPasswordForStoringInConfigFile(userPassword, "MD5");
                //    if (userPasswordMD5 == user.Password)
                //    {
                //        //Login sucessfull
                //        tbPassword.IsValid = true;
                //    }
                //    else
                //    {

                //        tbPassword.ErrorText = "Password is not correct";
                //        tbPassword.IsValid = false;
                //    }
                //}
                //else
                //{
                //  String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgDuplicate").ToString();
                //  tbemail.ErrorText = String.Format(msg,userEmail);
                //  tbemail.IsValid = false;
                //}
            }
        }
    }
}