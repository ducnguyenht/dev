using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using MVC.Filters;
using MVC.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace MVC.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe.Value))
                {
                    return Redirect(returnUrl ?? "/");
                }
                ModelState.AddModelError("ValidateLogin", MVC.App_Resources.Translate.AccountController_LoginError);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]//dn1
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { Status=LoginStatus.Requested,Email=model.Email }, true);
                    string queryConfirmationToken = "select ConfirmationToken from webpages_Membership where UserId = (SELECT UserId FROM  UserProfile where UserName='" + model.UserName + "'  )";
                    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlDataReader rdr = null;
                    string confirmationToken = "";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(queryConfirmationToken, conn);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            confirmationToken += rdr[0];
                        }
                    }
                    finally
                    {
                        if (rdr != null)
                        {
                            rdr.Close();
                        }
                        conn.Close();
                    }
                    var fromAddress = new MailAddress("redstopped4@gmail.com", "Admin System");//noreply@vifuture.com
                    var toAddress = new MailAddress(model.Email, model.UserName);//lukezeng@live.com
                    //var toAddressRc = new MailAddress(model.UserName, model.UserName);
                    const string fromPassword = "84152422";
                    const string subject = "Email Confirm";
                    string body = "/account/confirmaccount?username=" + model.UserName + "&confirmToken=" + confirmationToken;


                    //Sending Email to greet the new registered user 
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",//mail.foxglove.arvixe.com smtp.gmail.com
                        Port = 587,//587
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }                   
                    return RedirectToAction("Registered", "Account", new { model.UserName });
                    //WebSecurity.Login(model.UserName, model.Password);
                    //return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //Account/Registered
        [AllowAnonymous]//dn1
        public ActionResult Registered(string userName)
        {
            ViewBag.UserName = userName;
            return View();

        }
        //Get: /Account/ComfirmAccount
        [HttpGet]//dn1
        [AllowAnonymous]
        public ActionResult ConfirmAccount(string username, string confirmToken)
        {
            string commandText = "UPDATE  [dbo].[UserProfile] Set [Status] =@Status FROM [dbo].[UserProfile] INNER JOIN dbo.webpages_Membership ON dbo.UserProfile.UserId = dbo.webpages_Membership.UserId" +
                " WHERE (dbo.UserProfile.UserName = @UserName) AND (dbo.webpages_Membership.ConfirmationToken = @ConfirmationToken)";
        //    string commandText = "UPDATE Sales.Store SET Demographics = @demographics "
        //+ "WHERE CustomerID = @ID;";

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UserName"].Value = username;
                command.Parameters.Add("@ConfirmationToken", System.Data.SqlDbType.NVarChar);
                command.Parameters["@ConfirmationToken"].Value = confirmToken;
                command.Parameters.Add("@Status", System.Data.SqlDbType.Int);
                command.Parameters["@Status"].Value = LoginStatus.EmailConfirmed;
                // Use AddWithValue to assign Demographics.
                // SQL Server will implicitly convert strings into XML.
                //command.Parameters.AddWithValue("@demographics", demoXml);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            return View();
            //if (WebSecurity.ConfirmAccount(username, confirmToken))
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            //    return RedirectPermanent("http://www.google.com");
            //}
            //return RedirectToAction("Login", "Account");
        }
        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }
                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
   
}