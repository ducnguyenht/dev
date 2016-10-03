using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace WinExample.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class WinExampleWindowsFormsModule : ModuleBase {
        public WinExampleWindowsFormsModule() {
            InitializeComponent();
        }
    }
}
