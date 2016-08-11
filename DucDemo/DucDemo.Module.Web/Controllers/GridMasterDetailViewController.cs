using System;
using System.Web.UI;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace WebSolution.Module.Web {
    public class GridMasterDetailViewController : ViewController<ListView> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Model.MasterDetailMode == MasterDetailMode.ListViewAndDetailView) {
                ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
                if (listEditor != null) {
                    listEditor.Grid.SettingsDetail.ShowDetailButtons = true;
                    listEditor.Grid.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
                    listEditor.Grid.SettingsDetail.ShowDetailRow = true;
                    listEditor.Grid.Templates.DetailRow = new ASPxGridViewDetailRowTemplate(View, Application);
                }
            }
        }
        class ASPxGridViewDetailRowTemplate : ITemplate {
            private ListView masterListViewCore;
            XafApplication App;
            public ASPxGridViewDetailRowTemplate(ListView masterListView,XafApplication app) {
                masterListViewCore = masterListView;
                App = app;
            }
            public void InstantiateIn(Control container) {
                GridViewDetailRowTemplateContainer templateContainer = (GridViewDetailRowTemplateContainer)container;
                ASPxPageControl pageControl = RenderHelper.CreateASPxPageControl();
                object masterObject = masterListViewCore.ObjectSpace.GetObject(templateContainer.Grid.GetRow(templateContainer.VisibleIndex));
                pageControl.EnableCallBacks = true;
                pageControl.Width = Unit.Percentage(100);
                pageControl.ContentStyle.Paddings.Padding = Unit.Pixel(0);
                container.Controls.Add(pageControl);
                foreach (IMemberInfo mi in masterListViewCore.ObjectTypeInfo.Members) {
                    if (mi.IsList && mi.IsPublic) {
                        //IObjectSpace os = WebApplication.Instance.CreateObjectSpace();
                        IObjectSpace os = App.CreateObjectSpace();
                        string listViewId = DevExpress.ExpressApp.Model.NodeGenerators.ModelNestedListViewNodesGeneratorHelper.GetNestedListViewId(mi);
                        Type type = mi.ListElementType;
                        CollectionSourceBase cs = new PropertyCollectionSource(os, type, os.GetObject(masterObject), mi, CollectionSourceMode.Proxy);
                        ListView tempListView = WebApplication.Instance.CreateListView(listViewId, cs, false);
                        //CollectionSource collectionSource = new CollectionSource(os, mi.ListElementType, true);
                        //IObjectSpace os1 = App.CreateObjectSpace();
                        //string listViewId = DevExpress.ExpressApp.Model.NodeGenerators.ModelNestedListViewNodesGeneratorHelper.GetNestedListViewId(mi);
                        //PropertyCollectionSource cs =
                        //App.CreatePropertyCollectionSource(
                        //os, mi.ListElementType, os.GetObject(masterObject), mi, listViewId,
                        //CollectionSourceMode.Proxy);//GetCollectionMode(MemberInfo)
                      
                        //dynamic lst = os.GetObjects(type, new BinaryOperator(masterObject.GetType().Name, os.GetObject(masterObject), BinaryOperatorType.Equal));
                        //if (String.IsNullOrEmpty(listViewId))
                        //{
                        //    listViewId = App.GetListViewId(propertyCollectionSource.ObjectTypeInfo.Type);
                        //}



                        //IModelListView modelListView = App.FindModelClass(type).DefaultListView;
                        //modelListView.UseServerMode = true;
                        //ListView tempListView = App.CreateListView(modelListView, cs, false);//lst propertyCollectionSource
                        //tempListView.Model.UseServerMode = true;
                        //detailsListView.Model.UseServerMode = true;
                        Frame detailsFrame = WebApplication.Instance.CreateFrame(TemplateContext.NestedFrame);
                        detailsFrame.SetView(tempListView);//tempListView detailsListView
                        //detailsListView.LayoutManager
                        detailsFrame.CreateTemplate();
                        Control detailsTemplateControl = (Control)detailsFrame.Template;                    
                        detailsTemplateControl.ID = string.Format("detailsTemplateControl_{0}", mi.Name);
                        TabPage page = new TabPage(mi.Name);
                        page.Controls.Add(detailsTemplateControl);
                        pageControl.TabPages.Add(page);
                        //((Control)detailsFrame.Template).FindControl("ToolBar").Visible = false;
                    }
                }
            }

          
        }
    }
}
//IModelListView modelListView = mi.Model.View as IModelListView;
//if (modelListView == null)
//{
//    modelListView = application.FindModelClass(MemberInfo.MemberTypeInfo.Type).DefaultLookupListView;//DefaultLookupListView
//}
//ListView tempListView = this.application.CreateListView(modelListView, collectionSource, false);
//detailsListView.Model.
//string typemaster = masterObject.GetType().Name;
//CriteriaOperator criteria = new BinaryOperator(
//      new OperandProperty(typemaster), new OperandValue(masterObject),
//     BinaryOperatorType.Equal);
//CollectionSource collectionSource = null;
//collectionSource = new CollectionSource(os, mi.ListElementType, true);
//collectionSource.Criteria.Add("Mycriteria", criteria);