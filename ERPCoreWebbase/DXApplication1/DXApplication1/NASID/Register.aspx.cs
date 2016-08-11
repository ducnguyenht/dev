using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
//using BLL.BO;
using System.Web.Security;
//using BLL.NASID;

namespace DXApplication1.NASID
{
    public partial class Register : System.Web.UI.Page
    {
        //private UserBLO userBLO;
        protected void Page_Init(object sender, EventArgs e)
        {
            //userBLO = new UserBLO();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
        protected void emailtb_Validation(object sender, ValidationEventArgs e)
        {
            //if (!userBLO.CheckEmailExist(e.Value.ToString()))
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
        protected void usernametb_Validation(object sender, ValidationEventArgs e)
        {
            //if (!userBLO.CheckUserNameExist(e.Value.ToString()))
            //{
            //    e.ErrorText = "";
            //    e.IsValid = true;
            //}
            //else
            //{
            //    String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgDuplicate").ToString();
            //    e.ErrorText = String.Format(msg, usernametb.Text);
            //    e.IsValid = false;
            //}
        }

        protected void signUp_Click(object sender, EventArgs e)
        {
            //if (IsPostBack && captcha.IsValid && ASPxEdit.ValidateEditorsInContainer(this))
            //{
            if (captcha.IsValid && emailtb.IsValid && usernametb.IsValid)
            {
                //UserEntity userEntity = new UserEntity();
                //userEntity.UserId = Guid.NewGuid();
                //userEntity.Username = usernametb.Text;
                //userEntity.Email = emailtb.Text;
                //userEntity.Email1 = " ";
                //userEntity.Gender = Char.Parse(genderRadioButtonList.SelectedItem.Value.ToString());
                //userEntity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordtb.Text, "MD5");
                //userEntity.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                //userEntity.FirstName = firstNametb.Text;
                //userEntity.LastName = lastNametb.Text;
                try
                {
                    //this.userBLO.registerUser(userEntity);
                    //Utility.SendMail.SendMailRegisterUser(userEntity.Email, userEntity.FirstName);
                    //String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgRegisterSuccessfull").ToString();
                    ////Direct to page register successfull

                }
                catch (Exception)
                {
                    String msg = HttpContext.GetGlobalResourceObject("MessageResource", "msgRegisterNotSuccessfull").ToString();
                    //Direct to page register not successfull
                }
            }
        }

    }
}