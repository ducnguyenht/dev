using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.OAuth
{
    public class OAuthHelper
    {
        public static void LogOff()
        {
            DotNetOpenAuth.OpenId.RelyingParty.OpenIdRelyingPartyControlBase.LogOff();
        }
    }
}
