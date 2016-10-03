using System;
using System.Collections.Generic;

using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.ExpressApp.Scheduler.Win;

namespace WinExample.Module.Win {
    public partial class FilterResourcesController : ViewController {
        private void editor_ResourceDataSourceCreated(Object sender, ResourceDataSourceCreatedEventArgs e) {
            SetResourcesFilter(e.DataSource, userChoiceAction.SelectedItem.Data);
        }
        private void userChoiceAction_Execute(Object sender, SingleChoiceActionExecuteEventArgs e) {
            SchedulerListEditor editor = ((ListView)View).Editor as SchedulerListEditor;
            SetResourcesFilter(editor.ResourcesDataSource, e.SelectedChoiceActionItem.Data);
        }
        private void SetResourcesFilter(Object dataSource, Object resourceId) {
            XPCollection resources = dataSource as XPCollection;
            if(resourceId == null) {
                resources.Criteria = null;
            }
            else {
                resources.Criteria = new BinaryOperator("Oid", resourceId);
            }
        }

        protected override void OnActivated() {
            base.OnActivated();

            userChoiceAction.Items.Clear();

            ChoiceActionItem choiceActionItem1 = new ChoiceActionItem();
            choiceActionItem1.Caption = "All Users";
            userChoiceAction.Items.Add(choiceActionItem1);

            userChoiceAction.Items.Add(new ChoiceActionItem("Current User", SecuritySystem.CurrentUserId));
            foreach(MyUser user in View.ObjectSpace.GetObjects<MyUser>()) {
                userChoiceAction.Items.Add(new ChoiceActionItem(user.UserName, user.Oid));
            }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            SchedulerListEditor editor = ((ListView)View).Editor as SchedulerListEditor;
            if(editor == null) {
                userChoiceAction.Active.SetItemValue("Scheduler", false);
            }
            else {
                userChoiceAction.Active.SetItemValue("Scheduler", true);
                userChoiceAction.SelectedItem = userChoiceAction.Items[0];
                editor.ResourceDataSourceCreated += new EventHandler<ResourceDataSourceCreatedEventArgs>(editor_ResourceDataSourceCreated);
            }
        }
        
        public FilterResourcesController() {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "MyEvent_ListView";
        }
    }
}
