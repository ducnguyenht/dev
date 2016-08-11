using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Editors;
using ASPxDropDownEdit.Module.BusinessObjects;

namespace ASPxDropDownEdit.Web
{
    public partial class ASPxDropDownEditAspNetApplication : WebApplication
    {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private ASPxDropDownEdit.Module.ASPxDropDownEditModule module3;
        private ASPxDropDownEdit.Module.Web.ASPxDropDownEditAspNetModule module4;

        public ASPxDropDownEditAspNetApplication()
        {
            InitializeComponent();
        }

        protected override void OnLoggedOn(DevExpress.ExpressApp.LogonEventArgs e)
        {
            base.OnLoggedOn(e);
            ((ShowViewStrategy)base.ShowViewStrategy).CollectionsEditMode = ViewEditMode.Edit;
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProviderThreadSafe(args.ConnectionString, args.Connection);
        }

        private void ASPxDropDownEditAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
            e.Updater.Update();
            e.Handled = true;
        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new ASPxDropDownEdit.Module.ASPxDropDownEditModule();
            this.module4 = new ASPxDropDownEdit.Module.Web.ASPxDropDownEditAspNetModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ASPxDropDownEditAspNetApplication
            // 
            this.ApplicationName = "ASPxDropDownEdit";
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.ASPxDropDownEditAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
