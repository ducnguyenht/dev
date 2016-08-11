using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.ProviderAuthenticationPolicy;

namespace Utility.OAuth
{
    /// <summary>
	/// Strong-typed bag of session state.
	/// </summary>
	public class State {

		public static ClaimsResponse ProfileFields {
			get { return HttpContext.Current.Session["ProfileFields"] as ClaimsResponse; }
			set { HttpContext.Current.Session["ProfileFields"] = value; }
		}

        public static string Username
        {
            get { return ProfileFields.Email; }
        }

		public static FetchResponse FetchResponse {
			get { return HttpContext.Current.Session["FetchResponse"] as FetchResponse; }
			set { HttpContext.Current.Session["FetchResponse"] = value; }
		}

		public static string FriendlyLoginName {
			get { return HttpContext.Current.Session["FriendlyUsername"] as string; }
			set { HttpContext.Current.Session["FriendlyUsername"] = value; }
		}

		public static PolicyResponse PapePolicies {
			get { return HttpContext.Current.Session["PapePolicies"] as PolicyResponse; }
			set { HttpContext.Current.Session["PapePolicies"] = value; }
		}

		public static string GoogleAccessToken {
			get { return HttpContext.Current.Session["GoogleAccessToken"] as string; }
			set { HttpContext.Current.Session["GoogleAccessToken"] = value; }
		}

		public static void Clear() {
			ProfileFields = null;
			FetchResponse = null;
			FriendlyLoginName = null;
			PapePolicies = null;
			GoogleAccessToken = null;
		}
    }
}

	