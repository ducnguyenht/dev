using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;

namespace GridViewBatchEdit
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e) {
            CultureInfo ci = new CultureInfo("de-DE");
                     
            if (HttpContext.Current.Session != null)
                ci = (CultureInfo)Session["Culture"];
                if (ci == null)
                {
                    string langName = "vi-VN";
                    ci = new CultureInfo(langName);
                    Session["Culture"] = ci;
                }
            
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

    }
}