using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Interfaces;

namespace WebModule.UserControls
{
    public partial class FunctionNavBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String currentAccessObjectGroupId = ((IERPCoreWebModuleBase)Page).AccessObjectGroupId;
                FunctionsXmlDataSource.XPath = String.Format("groups/{0}/*", currentAccessObjectGroupId);
            }
            catch (Exception)
            {
                
            }
        }
    }
}