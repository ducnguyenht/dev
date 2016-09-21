using System.Web;
using System.Web.Mvc;

namespace dnGroupMVC01 {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}