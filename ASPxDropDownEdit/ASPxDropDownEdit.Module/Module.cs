using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using ASPxDropDownEdit.Module.BusinessObjects;


namespace ASPxDropDownEdit.Module
{
    public sealed partial class ASPxDropDownEditModule : ModuleBase
    {
        public ASPxDropDownEditModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
    }
}
