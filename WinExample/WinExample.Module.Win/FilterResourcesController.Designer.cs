namespace WinExample.Module.Win {
    partial class FilterResourcesController {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.userChoiceAction = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // userChoiceAction
            // 
            this.userChoiceAction.Caption = "Users";
            this.userChoiceAction.Id = "UserChoiceAction";
            this.userChoiceAction.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.userChoiceAction_Execute);
        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction userChoiceAction;
    }
}
