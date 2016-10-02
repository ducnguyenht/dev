using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web.SystemModule;
using MainDemo.Module.BusinessObjects;
using System;

namespace MainDemo.Module.Web.Controllers {
	public partial class MyWebModificationsController : WebModificationsController {
		public MyWebModificationsController() {
			InitializeComponent();
			RegisterActions(components);
		}
		protected override void SaveAndClose(SimpleActionExecuteEventArgs args) {
			View view = View;
			base.SaveAndClose(args);
            if(!view.IsDisposed && (view is DetailView) && (((DetailView)view).ObjectTypeInfo.Type == typeof(Contact))) {
				view.Close();
			}
		}
	}
}
