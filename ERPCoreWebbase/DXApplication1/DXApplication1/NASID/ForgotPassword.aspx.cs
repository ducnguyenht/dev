using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
//using DAL.NASID;
using System.Text;
using System.Security.Cryptography;
//using BLL.NASID;
namespace DXApplication1.NASID
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        //private BLL.NASID.UserBLO userBLO;
        protected void Page_Init(object sender, EventArgs e)
        {
            //userBLO = new BLL.NASID.UserBLO();
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void btsubmit_Click(object sender, EventArgs e)
        {
            //if (captcha.IsValid && emailtb.IsValid)
            //{
            //    User user = this.userBLO.CheckExistByEmail(emailtb.Text);
               
            //        string userId = user.UserId.ToString();
            //        //gui email
            //        string localhost = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.Url.Port.ToString()+"/NASID/ResetPassword.aspx?";
                   
            //        string message = "UserId=" + userId;
            //        string time1 = DateTime.Now.ToString();
            //          message = message + "&time=" + time1;
            //        string myPassword = "123";
            //        string userIdMD5 = Utility.Security.Encryptstring(message, myPassword);

            //        localhost = localhost + userIdMD5;
            //        string body = "<a href = " + '"' + localhost + '"' + "/>" + localhost + "</a>";
            //        Utility.SendMail.SendMailForgotPassword(emailtb.Text, "toan", body);
            //}
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //if (userBLO.CheckEmailExist(emailtb.Text))
            //{
            //    emailtb.ErrorText = "";
                
            //}
            //else
            //{
            //    String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgDuplicate").ToString();
            //    emailtb.ErrorText = String.Format(msg, emailtb.Text);
            //    emailtb.IsValid = false;
     
            //}

        }

        protected void emailtb_Validation(object sender, ValidationEventArgs e)
        {
            //if (userBLO.CheckEmailExist(e.Value.ToString()))
            //{
            //    e.ErrorText = "";
            //    e.IsValid = true;
            //}
            //else
            //{
            //    String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgDuplicate").ToString();
            //    e.ErrorText = String.Format(msg, emailtb.Text);
            //    e.IsValid = false;
            //}
        }
    }
}