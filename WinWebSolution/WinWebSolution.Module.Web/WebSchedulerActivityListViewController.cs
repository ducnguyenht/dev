using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Web.Editors;

namespace WinWebSolution.Module {
    public class WebSchedulerFilterResourcesListViewController : SchedulerActivityListViewController {
        protected override XPCollection GetResources(object resourcesDataSource) {
            return ((WebDataSource)resourcesDataSource).Collection as XPCollection;
        }
    }
}