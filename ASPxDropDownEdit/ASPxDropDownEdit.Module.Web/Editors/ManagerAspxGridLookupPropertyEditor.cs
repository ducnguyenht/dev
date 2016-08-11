using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.Base;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace ASPxDropDownEdit.Module.Web.Editors
{
    [PropertyEditor(typeof(object), EditorAliases.LookupPropertyEditor, false)]
    public class ManagerAspxGridLookupPropertyEditor : AspxGridLookupPropertyEditorBase
    {
        public ManagerAspxGridLookupPropertyEditor(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

        protected override void AddGridViewColumns(DevExpress.Web.ASPxGridLookup.ASPxGridLookup control)
        {
            GridViewDataTextColumn colFirstName = new GridViewDataTextColumn();
            colFirstName.Caption = "First Name";
            colFirstName.FieldName = "FirstName";
            colFirstName.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            control.Columns.Add(colFirstName);

            GridViewDataTextColumn colLastName = new GridViewDataTextColumn();
            colLastName.Caption = "Last Name";
            colLastName.FieldName = "LastName";
            control.Columns.Add(colLastName);
        }
    }
    public abstract class AspxGridLookupPropertyEditorBase : ASPxObjectPropertyEditorBase, IComplexViewItem
    {
        // now inherited from ASPxObjectPropertyEditorBase
        //IObjectSpace _objectSpace;
        //XafApplication _application;

        private NewObjectViewController newObjectViewController;
        private PopupWindowShowAction newObjectWindowAction;
        private NestedFrame frame;
        private WebLookupEditorHelper helper;
        private object newObject;
        private IObjectSpace newObjectSpace;

        public AspxGridLookupPropertyEditorBase(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

        // joe 5/9/2011: must attache ValueChanged event handler or nothing happens when you change the value in detail view
        protected override WebControl CreateEditModeControlCore()
        {
            EnsureFrameObjects();
            XpoDataSource datasource = new XpoDataSource();
            datasource.TypeName = MemberInfo.MemberTypeInfo.FullName;
            datasource.ServerMode = true;
            datasource.Session = ((XPObjectSpace)objectSpace).Session;
            //datasource.Criteria = "IsGrainBosRecord = True";

            ASPxGridLookup control = new ASPxGridLookup();
            control.DataSource = datasource;
            control.GridView.SettingsPager.PageSize = 10;
            control.KeyFieldName = MemberInfo.MemberTypeInfo.KeyMember.Name;
            control.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            control.TextFormatString = "{0}";
            //string delayKeyValue = WebConfigurationManager.AppSettings["IncrementalFilteringDelay"];
            //if (!string.IsNullOrEmpty(delayKeyValue))
            //{
            //    control.IncrementalFilteringDelay = Convert.ToInt32(delayKeyValue);
            //}
            control.IncrementalFilteringDelay = 800;
            control.ValueChanged += EditValueChangedHandler;
            control.GridView.AutoGenerateColumns = true;
            control.GridView.Settings.ShowFilterRow = true;
            control.GridView.Settings.ShowFilterRowMenu = true;
            control.GridView.SettingsCookies.Enabled = true;
            control.GridView.Width = 600;
            control.SelectionMode = GridLookupSelectionMode.Single;
          //  AddGridViewColumns(control);
            AddButtons(control);
            AddClientSideEvents(control);
            control.GridView.CustomCallback += GridView_CustomCallback;
            control.Init += control_Init;
            control.PreRender += control_PreRender;

            return control;
        }

        private void control_Init(object sender, EventArgs e)
        {
            AddClientSideEvents((ASPxGridLookup)sender);
        }

        private void control_PreRender(object sender, EventArgs e)
        {
            AddClientSideEvents((ASPxGridLookup)sender);
        }

        protected void EnsureFrameObjects()
        {
            if (frame == null)
            {
                frame = helper.Application.CreateNestedFrame(this, TemplateContext.LookupControl);
                frame.SetView(helper.CreateListView(CurrentObject)); // joe 5/11/2011: MUST DO THIS
                newObjectViewController = frame.GetController<NewObjectViewController>();

                if (newObjectViewController != null)
                {
                    newObjectViewController.ObjectCreating += new EventHandler<ObjectCreatingEventArgs>(newObjectViewController_ObjectCreating);
                    newObjectViewController.ObjectCreated += new EventHandler<ObjectCreatedEventArgs>(newObjectViewController_ObjectCreated);
                }

                newObjectWindowAction = new PopupWindowShowAction(null, "New", PredefinedCategory.Unspecified.ToString());
                newObjectWindowAction.Execute += new PopupWindowShowActionExecuteEventHandler(newObjectWindowAction_OnExecute);
                newObjectWindowAction.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(newObjectWindowAction_OnCustomizePopupWindowParams);
                newObjectWindowAction.Application = helper.Application;
            }
        }

        private void AddClientSideEvents(ASPxGridLookup control)
        {
            string js = @"function(s,e) { 
                                // clear button
                                if (e.buttonIndex == 0) { 
                                    s.GetGridView().PerformCallback('clear'); 
                                    s.SetText(''); 
                                }
                                // add button
                                else if (e.buttonIndex == 1) { 
                                    // open new window and pass in script to run call back when finished
                                    " + ((WebApplication)application).PopupWindowManager.GenerateModalOpeningScript(control, newObjectWindowAction, 640, 480, false, GetNewObjectAddedCallbackScript(control)) + @"
                                }
                                // total hack, but after creating a new object we cause a button click and pass in bogus button id (9)
                                else if (e.buttonIndex == 9) {
                                   s.GetGridView().PerformCallback('newObjId=' + window.ddLookupResult); 
                                } 
                                e.processOnServer = false;
                            }";

            control.ClientSideEvents.ButtonClick = js;
        }

        private string GetNewObjectAddedCallbackScript(ASPxGridLookup control)
        {
            // joe 5/12/2011: could not get callback working the DevExpress way, so this is a total hack to invoke a button click for a bogus button of the grid view.
            // We pass in the button index of 9 as a signal that this is our event.  Event is handled by GridView_CustomCallback with param starting with "newObjId="
            return "aspxBEClick('" + control.ClientID + "',9);";
        }

        private void AddButtons(ASPxGridLookup control)
        {
            EditButton clearButton = new EditButton();
            clearButton.ToolTip = CaptionHelper.GetLocalizedText("DialogButtons", "Clear");
            ASPxImageHelper.SetImageProperties(clearButton.Image, "Editor_Clear");
            control.Buttons.Add(clearButton);

            if (newObjectViewController.NewObjectAction.Items.Count > 0)
            {
                EditButton addButton = new EditButton();
                addButton.ToolTip = CaptionHelper.GetLocalizedText("DialogButtons", "Add");
                ASPxImageHelper.SetImageProperties(addButton.Image, "Editor_Add");
                control.Buttons.Add(addButton);
            }
        }

        // joe 5/12/2011: descendents must add columns to gridview
        protected abstract void AddGridViewColumns(ASPxGridLookup control);

        void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var control = sender as ASPxGridView;
            if (e.Parameters == "clear")
            {
                control.Selection.UnselectAll();
                control.FocusedRowIndex = -1;
                ((ASPxGridLookup)this.Editor).Value = null;
                WriteValue();
            }
            else if (e.Parameters.StartsWith("newObjId="))
            {
                object newObject = helper.GetObjectByKey(this.CurrentObject, e.Parameters.Replace("newObjId=", ""));
                ((ASPxGridLookup)this.Editor).Value = newObject;
                WriteValue();
            }
        }

        // joe 5/9/2011: need to cast as double or will get invalidcastexception
        protected override object GetControlValueCore()
        {
            if (ViewEditMode == ViewEditMode.Edit && Editor != null)
            {
                var editor = this.Editor as ASPxGridLookup;

                if (editor.Value != null)
                {
                    return objectSpace.GetObjectByKey(MemberInfo.MemberType, editor.Value);
                }

                return null;
            }

            return MemberInfo.GetValue(CurrentObject);
        }

        private void newObjectViewController_ObjectCreated(object sender, DevExpress.ExpressApp.SystemModule.ObjectCreatedEventArgs e)
        {
            newObject = e.CreatedObject;
            newObjectSpace = e.ObjectSpace;
        }

        private void newObjectViewController_ObjectCreating(object sender, ObjectCreatingEventArgs e)
        {
            e.ShowDetailView = false;
            if (e.ObjectSpace is DevExpress.ExpressApp.Xpo.XPNestedObjectSpace)
            {
                e.ObjectSpace = application.CreateObjectSpace();
            }
        }

        private void newObjectWindowAction_OnCustomizePopupWindowParams(Object sender, CustomizePopupWindowParamsEventArgs args)
        {
            if (newObjectViewController != null)
            {
                newObjectViewController.NewObjectAction.DoExecute(newObjectViewController.NewObjectAction.Items[0]);
                args.View = application.CreateDetailView(newObjectSpace, newObject, newObjectSpace != objectSpace);
            }
        }

        private void newObjectWindowAction_OnExecute(Object sender, PopupWindowShowActionExecuteEventArgs args)
        {
            if (objectSpace != args.PopupWindow.View.ObjectSpace)
            {
                args.PopupWindow.View.ObjectSpace.CommitChanges();
            }
            //AddObjectToDataSource(helper.ObjectSpace.GetObject(((DetailView)args.PopupWindow.View).CurrentObject));
            ((PopupWindow)args.PopupWindow).ClosureScript = "if(window.opener != null) window.dialogOpener.ddLookupResult = '" + helper.GetObjectKey(((DetailView)args.PopupWindow.View).CurrentObject) + "';";
        }

        #region IComplexPropertyEditor Members

        public override void Setup(DevExpress.ExpressApp.IObjectSpace objectSpace, DevExpress.ExpressApp.XafApplication application)
        {
            //_objectSpace = objectSpace;
            //_application = application;
            base.Setup(objectSpace, application);
            helper = new WebLookupEditorHelper(application, objectSpace, MemberInfo.MemberTypeInfo, Model);
        }

        #endregion

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (Editor != null)
            {
                ASPxGridLookup control = (ASPxGridLookup)Editor;
                if (control != null)
                {
                    control.ValueChanged -= EditValueChangedHandler;
                    control.GridView.CustomCallback -= GridView_CustomCallback;
                    control.Init -= control_Init;
                    control.PreRender -= control_PreRender;
                }
            }

            base.BreakLinksToControl(unwireEventsOnly);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (newObjectWindowAction != null)
                    {
                        newObjectWindowAction.Execute -= new PopupWindowShowActionExecuteEventHandler(newObjectWindowAction_OnExecute);
                        newObjectWindowAction.CustomizePopupWindowParams -= new CustomizePopupWindowParamsEventHandler(newObjectWindowAction_OnCustomizePopupWindowParams);
                        DisposeAction(newObjectWindowAction);
                        newObjectWindowAction = null;
                    }
                    if (newObjectViewController != null)
                    {
                        newObjectViewController.ObjectCreating -= new EventHandler<ObjectCreatingEventArgs>(newObjectViewController_ObjectCreating);
                        newObjectViewController.ObjectCreated -= new EventHandler<ObjectCreatedEventArgs>(newObjectViewController_ObjectCreated);
                        newObjectViewController = null;
                    }
                    if (frame != null)
                    {
                        frame.SetView(null);
                        frame.Dispose();
                        frame = null;
                    }

                    if (newObjectSpace != null && !newObjectSpace.IsDisposed)
                        newObjectSpace.Dispose();

                    newObject = null;
                    newObjectSpace = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // joe 5/13/2011: now inherited from base
        //protected void DisposeAction(ActionBase action)
        //{
        //    try
        //    {
        //        action.Dispose();
        //    }
        //    catch (Exception e)
        //    {
        //        Tracing.Tracer.LogSubSeparator("Exception occurs on disposing an action");
        //        Tracing.Tracer.LogError(e);
        //    }
        //}
    }

}
