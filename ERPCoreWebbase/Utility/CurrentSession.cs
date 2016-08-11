using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Utility
{
    public class CurrentSession
    {
        //private constructor
        private CurrentSession()
        {
            LoginEmail = null;
            FriendlyLoginName = null;
            Login = 0;
        }

        // Gets the current session.
        public static CurrentSession Instance
        {
            get
            {
                CurrentSession session =
                (CurrentSession)HttpContext.Current.Session["__Session__"];
                if (session == null)
                {
                    session = new CurrentSession();
                    HttpContext.Current.Session["__Session__"] = session;
                }
                return session;
            }
        }

        public Guid UserId { get; set; }
        public string LoginEmail { get; set; }
        public int Login { get; set; }
        public string Lang { get; set; }
        public string FriendlyLoginName { get; set; }

        //Organization which user is accessing
        private Guid _AccessingOrganizationId;
        public Guid AccessingOrganizationId {
            get { return Guid.Parse("D52962C2-A75D-4F6E-BE0A-FF0C07D2B80B"); }
            set { _AccessingOrganizationId = value; }
        }

    }
}
