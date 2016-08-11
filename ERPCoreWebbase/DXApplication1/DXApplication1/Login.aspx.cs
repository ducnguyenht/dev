using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.Messaging;
using Utility.OAuth;
using NAS.BO.Nomenclature.Organization;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
//using BLL.Authorization;
//using BLL.BO.Authorization;

namespace DXApplication1.GUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Populate();
            }

            #region Disable OpenId
            //OpenIdRelyingParty openid = new OpenIdRelyingParty();
            //var response = openid.GetResponse();
            //if (response != null)
            //{
            //    switch (response.Status)
            //    {
            //        case AuthenticationStatus.Authenticated:
            //            // This is where you would look for any OpenID extension responses included
            //            // in the authentication assertion.
            //            //var claimsResponse = response.GetExtension<ClaimsResponse>();
            //            //Database.ProfileFields = claimsResponse;

            //            //// Store off the "friendly" username to display -- NOT for username lookup
            //            //Database.FriendlyLoginName = response.FriendlyIdentifierForDisplay;

            //            // Use FormsAuthentication to tell ASP.NET that the user is now logged in,
            //            // with the OpenID Claimed Identifier as their username.

            //            //State.FriendlyLoginName = response.FriendlyIdentifierForDisplay;
            //            DevExpress.Xpo.Session session = null;
            //            try
            //            {
            //                session = XpoHelper.GetNewSession();
            //                State.ProfileFields = response.GetExtension<ClaimsResponse>();

            //                LoginAccountBO loginAccountBO = new LoginAccountBO();
            //                LoginAccount loginAccount;
            //                bool isAuthValid = loginAccountBO.AuthenticateByOpenID(session, State.ProfileFields.Email, out loginAccount);
            //                if (isAuthValid)
            //                {
            //                    Utility.CurrentSession.Instance.LoginEmail = State.ProfileFields.Email;
            //                    Utility.CurrentSession.Instance.FriendlyLoginName = loginAccount.PersonId.Name;
            //                    //Utility.CurrentSession.Instance.FriendlyLoginName = State.ProfileFields.FullName != null && State.ProfileFields.FullName.Trim().Length != 0
            //                    //                                                    ? State.ProfileFields.FullName : State.ProfileFields.Nickname;
            //                    FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);
            //                }
            //                else
            //                {
            //                    OAuthHelper.LogOff();
            //                    lblOpenIdStatus.Visible = true;
            //                }
            //            }
            //            catch (Exception)
            //            {
            //                throw;
            //            }
            //            finally
            //            {
            //                if (session != null) session.Dispose();
            //            }


            //            break;
            //        case AuthenticationStatus.Canceled:
            //            //this.loginCanceledLabel.Visible = true;
            //            break;
            //        case AuthenticationStatus.Failed:
            //            //this.loginFailedLabel.Visible = true;
            //            break;
            //    }
            //}
            #endregion
            
        }

        protected void btsubmit_Click1(object sender, EventArgs e)
        {
            //if (captcha.IsValid)
            //{

            /*2013-12-02 Khoa.Truong DEL START*/
            //if (tbemail.Text.Trim().Equals("user@naansolution.com") && tbPassword.Text.Trim().Equals("user"))
            //{
            //    //string returnUrl = Request.Params["ReturnUrl"].ToUpper();
            //    string returnUrl = null;
            //    if (Request.Params["ReturnUrl"] != null)
            //    {
            //        returnUrl = Request.Params["ReturnUrl"].ToUpper();
            //    }

            //    if (returnUrl == null || returnUrl.Equals("%2F") || returnUrl.Equals("/"))
            //    {
            //        FormsAuthentication.SetAuthCookie(tbemail.Text.Trim(), false);
            //        Response.Redirect(FormsAuthentication.DefaultUrl);
            //    }
            //    else
            //    {
            //        FormsAuthentication.RedirectFromLoginPage(tbemail.Text.Trim(), false);
            //    }
            //}
            //else
            //{
            //    //lblMessage.Text = "Sai email hoặc mật khẩu.";
            //}
            /*2013-12-02 Khoa.Truong DEL END*/

            //}
        }

        protected void OpenIdLogin_Click(object sender, CommandEventArgs e)
        {
            #region Disable OpenID
            //try
            //{
            //    using (OpenIdRelyingParty openid = new OpenIdRelyingParty())
            //    {
            //        IAuthenticationRequest request = openid.CreateRequest(e.CommandArgument.ToString());

            //        // This is where you would add any OpenID extensions you wanted
            //        // to include in the authentication request.
            //        request.AddExtension(new ClaimsRequest
            //        {
            //            Email = DemandLevel.Require,
            //            FullName = DemandLevel.Require,
            //            Nickname = DemandLevel.Require,
            //            Country = DemandLevel.Require,
            //            Gender = DemandLevel.Require
            //        });

            //        // Send your visitor to their Provider for authentication.
            //        request.RedirectToProvider();
            //    }
            //}
            //catch (ProtocolException ex)
            //{
            //    // The user probably entered an Identifier that
            //    // was not a valid OpenID endpoint.
            //    //this.openidValidator.Text = ex.Message;
            //    //this.openidValidator.IsValid = false;
            //}
            Utility.CurrentSession.Instance.LoginEmail = "guest@gmail.com";
            Utility.CurrentSession.Instance.FriendlyLoginName = "Guest";
            FormsAuthentication.RedirectFromLoginPage("Guest", false);
            #endregion
        }

        private void Populate()
        {
            DevExpress.Xpo.UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                if (!Util.isExistXpoObject<Person>("Code", "ADM1"))
                {
                    Person person = new Person(uow)
                    {
                        Code = "ADM1",
                        Name = "Admin1",
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    LoginAccount loginAccount = new LoginAccount(uow)
                    {
                        Email = "snl.qn.adm@gmail.com",                        
                        RowCreationTimeStamp = DateTime.Now,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                        PersonId = person
                    };
                }
                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }
    }
}