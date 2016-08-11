using System;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Security;
using System.Text;

namespace HttpModuleAccessControl
{
    public class FirstClass : IHttpModule
    {

        private static object _initSyncRoot = new object();
        // By using AuthenticationSection, we can retrieve the properties in the
        // section of authentication under system.web in web.config
        private static AuthenticationSection _AutheSection = null;

        /// <summary>
        /// You will need to configure this module in the web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            //context.LogRequest += new EventHandler(OnLogRequest);

            // For authentication and authorization, detemine which authentication option goes which functionality
            System.Configuration.Configuration configuration =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/WebModule");
            _AutheSection = (AuthenticationSection)configuration.GetSection("system.web/authentication");
            AuthenticationMode _AutheMode = _AutheSection.Mode;

            switch (_AutheMode)
            {
                case AuthenticationMode.Forms:
                    //For custom authenticating
                    context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
                    //For custom authorization
                    context.AuthorizeRequest += new EventHandler(context_AuthorizeRequest);
                    //
                    context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);

                    break;

                // future : for windows domain case, refer to Active Directory programming later.
                case AuthenticationMode.Windows:

                    break;

            }

        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (HttpContext.Current.Handler.GetType().ToString().EndsWith("_aspx"))
            {   // Register events handler only on aspx pages.
                Page page = (Page)HttpContext.Current.Handler;
            }
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {
            
        }

        void context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpRequest httpReq = application.Context.Request;
            HttpResponse httpResp = application.Context.Response;

            // There has authentication cookie. Get the authentication cookie value.            
            FormsAuthenticationTicket authTicket = GetTicket(application.Context);

            // if cookie is empty, then whole process should be stopped.
            // if cookie is not empty, then 1. if username exists in Users object, go to AuthorizeRequest function.
            //                              2. if username doesn't exist in Users object, go to AccessDenied page.
            if (authTicket == null)
            {
                application.Context.User = null;
                // should be redirected to login.aspx or do nothing here because authTicket is null
                // Setting of FormsAuthentication will do the redirection work, so just *return* here.
                return;
            }

            try
            {
                if (authTicket.Expired)
                {
                    FormsAuthentication.SignOut();
                    StringBuilder sb = new StringBuilder(httpReq.Path);
                    sb.Append("?");

                    // There will always be something in the query string
                    sb.Append("SessionTimeout=").Append("1");

                    string CurQueryStringValue;
                    foreach (string QueryStringKey in httpReq.QueryString.Keys)
                    {
                        switch (QueryStringKey)
                        {
                            // Skip the time out
                            case "SessionTimeout":
                                break;
                            default:
                                CurQueryStringValue = httpReq.QueryString[QueryStringKey];
                                if (null != CurQueryStringValue)
                                    sb.Append("&").Append(QueryStringKey).Append("=").Append(CurQueryStringValue);
                                break;
                        }
                    }
                    httpResp.Redirect(sb.ToString(), true);
                    //Log.Write("HttpModule AuthenticateRequest exits because of FormsAuthenticationTicket Expired is true.");
                    return;
                }
                else
                {
                    RefreshTicketIfOld(application.Context, authTicket);
                }
            }
            catch
            {
                application.Context.User = null;
                //Log.Write("HttpModule AuthenticateRequest exits because of HttpApplication.Context.User is null.");
                return;
            }

        }

        private void  RefreshTicketIfOld(HttpContext context, FormsAuthenticationTicket tOld)
        {
            if ((DateTime.Now - tOld.IssueDate) > (tOld.Expiration - DateTime.Now))
            {
                DateTime refreshTime = DateTime.Now;

                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(tOld.Version,
                     tOld.Name, refreshTime, refreshTime.Add(GetAuthenticationSection().Forms.Timeout),
                     tOld.IsPersistent, tOld.UserData, tOld.CookiePath);


                //Db.NetBankUsersEntity.UserKeepAlive(new Guid(newTicket.Name),
                //    (int)AuthEventCode.Refresh, 0);

                string encryptedTicket = FormsAuthentication.Encrypt(newTicket);

                // Create a cookie and add the encrypted ticket to the
                // cookie as data.
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Secure = FormsAuthentication.RequireSSL;

                // RSU: ¤£¯à assign Expires,¦]¬°¨S¦³Expiresªº®É­Ô,lifetime¬° browser scope,·í¦³­Èªº®É­Ô, ·|³QPersistent // authCookie.Expires = newTicket.Expiration;
                authCookie.HttpOnly = true;

                if (!string.IsNullOrEmpty(FormsAuthentication.FormsCookiePath))
                    authCookie.Path = FormsAuthentication.FormsCookiePath;

                if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
                    authCookie.Domain = FormsAuthentication.CookieDomain;

                // Add the cookie to the outgoing cookies collection.
                context.Response.Cookies.Add(authCookie);
            }
        }

        private FormsAuthenticationTicket GetTicket(HttpContext context)
        {
            //string ErrorTitle = "FristClass class, GetTicket function = ";

            // There has authentication cookie.
            HttpRequest httpReq = context.Request;
            FormsAuthenticationTicket authTicket = null;

            try
            {
                // Extract the forms authentication cookie            
                HttpCookie authCookie = httpReq.Cookies[GetAuthenticationSection().Forms.Name];
                if (null == authCookie || String.IsNullOrEmpty(authCookie.Value))
                {
                    // There is no authentication cookie.
                    return null;
                }

                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (null == authTicket)
                {
                    // Cookie failed to decrypt.
                    //Log.Write(ErrorTitle + " Cookie failed to decrypt.");
                }
            }
            catch (Exception err)
            {
                err.Data["RawUrl"] = httpReq.RawUrl;
                //err.Data["CookieRawData"] = authCookie.Value;
                err.Data["UrlReferrer"] = httpReq.UrlReferrer;
                //Log.Write(ErrorTitle + " error= " + err.Message);
            }

            return authTicket;

        }

        /// <summary>
        ///  Web.Config system.web/authentication Configuration Section
        /// </summary>
        /// <returns></returns>
        public static AuthenticationSection GetAuthenticationSection()
        {
            if (_AutheSection == null)
            {
                lock (_initSyncRoot)
                {
                    if (_AutheSection == null)
                    {
                        _AutheSection = WebConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection;
                    }
                }
            }
            return _AutheSection;
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
    }
}
