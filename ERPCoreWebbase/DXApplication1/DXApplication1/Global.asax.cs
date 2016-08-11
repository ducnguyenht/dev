using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DevExpress.Web.ASPxClasses;
using DevExpress.Xpo;
//using DAL.NASID;
using Utility;
using System.Web.Routing;

namespace WebModule {
	public class Global_asax : System.Web.HttpApplication {

        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = Utils.CurrentTheme;
        }

		void Application_Start(object sender, EventArgs e) {

			DevExpress.Web.ASPxClasses.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues["name"] = "default";
            routeValues["Id"] = 1;
            RouteTable.Routes.MapPageRoute("Warehouse", "Warehouse/{Id}/{name}", "~/Warehouse/Warehouse.aspx", false, routeValues);
            /////2013-07-08 Khoa.Truong DEL START
            /////Description: Should using methods in DAL.NASID.XpoHelper
            //string connectionString = ConnectionHelper.ConnectionString;

            //DevExpress.Xpo.Metadata.XPDictionary dictionary =
            //            new DevExpress.Xpo.Metadata.ReflectionDictionary();

            //DevExpress.Xpo.DB.IDataStore store =
            //            DevExpress.Xpo.XpoDefault.GetConnectionProvider(connectionString,
            //            DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);

            //dictionary.GetDataStoreSchema(typeof(User).Assembly,
            //                              typeof(UserRole).Assembly,
            //                              typeof(Organization).Assembly,
            //                              typeof(OrganizationRole).Assembly,
            //                              typeof(OrganizationUser).Assembly,
            //                              typeof(Role).Assembly,
            //                              typeof(AccessObject).Assembly,
            //                              typeof(Permission).Assembly,
            //                              typeof(RolePermission).Assembly,
            //                              typeof(Operation).Assembly,
            //                              typeof(Session1).Assembly,
            //                              typeof(SessionRole).Assembly,
            //                              typeof(OrganizationHierachy).Assembly,
            //                              typeof(OrganizationRelationshipView).Assembly,
            //                              typeof(OrganizationTreeView).Assembly);

            //DevExpress.Xpo.XpoDefault.DataLayer =
            //            new DevExpress.Xpo.ThreadSafeDataLayer(dictionary, store);

            //DevExpress.Xpo.XpoDefault.Session = null;

            //Application["XPODataLayer"] = dictionary;
            /////2013-07-08 Khoa.Truong DEL END
        }

		void Application_End(object sender, EventArgs e) {
			// Code that runs on application shutdown
		}

		void Application_Error(object sender, EventArgs e) {
			// Code that runs when an unhandled error occurs
		}

		void Session_Start(object sender, EventArgs e) {
			// Code that runs when a new session is started
		}

		void Session_End(object sender, EventArgs e) {
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.
		}
	}
}