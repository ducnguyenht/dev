using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace XAF13_2_Demo.Module
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic.
    public sealed partial class DemoModule : ModuleBase
    {
        public DemoModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            var notificationUpdater = new DatabaseUpdate.NotificationUpdater(objectSpace, versionFromDB);
            ModuleUpdater updater = new DatabaseUpdate.Updater(Application, objectSpace, versionFromDB);
            return new ModuleUpdater[] { notificationUpdater, updater };
        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
