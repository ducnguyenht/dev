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
        bool flag = true;
        ASPxGridViewDetailRowTemplate temp=null;
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (flag)
            {
                if (View.Model.MasterDetailMode == MasterDetailMode.ListViewAndDetailView)
                {
                    ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
                    if (listEditor != null)
                    {
                        listEditor.Grid.SettingsDetail.ShowDetailButtons = true;
                        listEditor.Grid.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
                        listEditor.Grid.SettingsDetail.ShowDetailRow = true;
                        if (temp==null)
	                    {
                            temp = new ASPxGridViewDetailRowTemplate(View, Application);
                           
                            
	                    }
                        listEditor.Grid.Templates.DetailRow = temp;
                        
                        //flag = false;
                    }
                   
                }
              
            }
            
        }
        class ASPxGridViewDetailRowTemplate : ITemplate {
            private ListView masterListViewCore;
            XafApplication App;
            CollectionSourceBase cs;
            bool isInit = false;
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
                        
                        if (!isInit)
                        {
                            cs = new PropertyCollectionSource(os, type, os.GetObject(masterObject), mi, CollectionSourceMode.Proxy);
                            cs.CollectionChanged += cs_CollectionChanged;
                            os.Committing += os_Committing;
                            isInit = true;
                        }

                        ListView tempListView = WebApplication.Instance.CreateListView(listViewId, cs, false);
                      
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

            void os_Committing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                //throw new NotImplementedException();
            }

            void cs_CollectionChanged(object sender, EventArgs e)
            {
                System.Diagnostics.Debug.WriteLine(cs.GetCount());
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