using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using XAF13_2_Demo.Module.BusinessObjects;

namespace XAF13_2_Demo.Module.Controllers
{
    public class XPViewBusinessObjectProxyController : ViewController<ListView>
    {
        public SingleChoiceAction ChooseColumnAction { get; private set; }
        public XPViewBusinessObjectProxyController()
        {
            TargetObjectType = typeof(XPViewBusinessObjectProxy);
            TargetViewNesting = Nesting.Any;

            ChooseColumnAction = new SingleChoiceAction(this, "ChooseColumnAction", PredefinedCategory.Filters)
            {
                Caption = "Choose XPView Column",
                ItemType = SingleChoiceActionItemType.ItemIsMode,
            };

            foreach (var property in typeof(LargeBusinessObject).GetProperties().Where(m => m.Name.StartsWith("Property")))
            {
                ChooseColumnAction.Items.Add(new ChoiceActionItem(property.Name, new OperandProperty(property.Name)));
            }

            ChooseColumnAction.SelectedIndex = 0;
            ChooseColumnAction.Execute += ChooseColumnAction_Execute;
            Activated += XPViewBusinessObjectProxyController_Activated;
        }

        void XPViewBusinessObjectProxyController_Activated(object sender, System.EventArgs e)
        {
            if (ChooseColumnAction.SelectedItem == null)
                ChooseColumnAction.SelectedIndex = 0;

            ChooseColumnAction.DoExecute(ChooseColumnAction.SelectedItem);
        }

        void ChooseColumnAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var view = ObjectSpace.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>()
            {
                new DataViewExpression("Name", LargeBusinessObject.Field.GetOperand(m => m.Name)),
                new DataViewExpression("Property", (OperandProperty)e.SelectedChoiceActionItem.Data),
            }, null, new List<SortProperty>());

            foreach (ViewRecord item in view)
            {
                var name = item["Name"];
                var property = item["Property"];

                var obj = ObjectSpace.CreateObject<XPViewBusinessObjectProxy>();
                obj.Name = name as string;
                obj.Property = property as string;

                (View.CollectionSource as DevExpress.ExpressApp.CollectionSource).Add(obj);
            }
        }
    }
}